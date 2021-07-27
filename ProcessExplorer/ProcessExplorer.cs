using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProcessExplorer
{
    public class ProcessExplorer
    {
        //public Color BaseColor { get; set; } = Color.GreenYellow;

        [DllImport("Wtsapi32.dll")]
        private static extern bool WTSQuerySessionInformation(IntPtr hServer, int sessionId, WtsInfoClass wtsInfoClass, out IntPtr ppBuffer, out int pBytesReturned);
        [DllImport("Wtsapi32.dll")]
        private static extern void WTSFreeMemory(IntPtr pointer);

        private enum WtsInfoClass
        {
            WTSUserName = 5,
            WTSDomainName = 7,
        }

        private string _name;

        private int _processId;

        private int _parentProcessId;

        private long _workingSetSize;
        
        private long _privatePageCount;

        private string _executablePath;

        private string _userName; 

        private int _sessionId;

        private string commandLine;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public int ProcessId
        {
            get { return _processId; }
            set { _processId = value; }
        }

        public long WorkingSetSize { get => _workingSetSize; set => _workingSetSize = value; }
        public long PrivatePageCount { get => _privatePageCount; set => _privatePageCount = value; }
        public string ExecutablePath { get => _executablePath; set => _executablePath = value; }
        public string Description { get; set; }
        public string UserName { get => _userName; set => _userName = value; }
        public int SessionId { get => _sessionId; set => _sessionId = value; }
        public int ParentProcessId { get => _parentProcessId; set => _parentProcessId = value; }
        public string CommandLine { get => commandLine; set => commandLine = value; }

        ProcessExplorer(string name, int processid, long workingSetSize, long privatePageCount, string executablePath, string commandLine, string description, int parentProcessId)
        {
            Name = name;
            ProcessId = processid;
            ParentProcessId = parentProcessId;
            WorkingSetSize = workingSetSize;
            PrivatePageCount = privatePageCount;
            ExecutablePath = executablePath;
            Description = description;
            CommandLine = commandLine;
        }

        #region GetProcesses

        static protected internal List<ProcessExplorer> GetProcesses()
        {
            List<ProcessExplorer> processes = new List<ProcessExplorer>();
            string query = "Select * From Win32_Process";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection processList = searcher.Get();

            foreach (ManagementObject obj in processList)
            {
                string name = obj["Name"]?.ToString();

                int processId = Convert.ToInt32(obj["ProcessId"]);

                int parentProcessId = Convert.ToInt32(obj["ParentProcessId"]);

                long workingSetSize =Convert.ToInt64(obj["WorkingSetSize"]);

                long privatePageCount =Convert.ToInt64(obj["PrivatePageCount"]);
                
                string executablePath = obj["ExecutablePath"]?.ToString();

                string commandLine = obj["CommandLine"]?.ToString();

                string description = "";

                var isFileExists = File.Exists(executablePath);
                if (isFileExists)
                {
                    var fileVersionInfo = FileVersionInfo.GetVersionInfo(executablePath);
                    description = fileVersionInfo?.FileDescription;
                }

                ProcessExplorer ProcessesList = new ProcessExplorer(name, processId, workingSetSize, privatePageCount, executablePath, commandLine, description, parentProcessId);
                processes.Add(ProcessesList);
                getUsername(Int32.Parse(obj["SessionId"]?.ToString()), ProcessesList, true);
            }

            return processes;
        }

        #endregion

        #region  GetUserName

        private static string getUsername(int sessionId, ProcessExplorer process, bool prependDomain = true)
        {
            IntPtr buffer;
            int len;

            bool isQuerySuccess = WTSQuerySessionInformation(IntPtr.Zero, sessionId, WtsInfoClass.WTSUserName, out buffer, out len);
            bool isValid = isQuerySuccess && len > 1;

            if (isValid)
            {
                process.UserName = Marshal.PtrToStringAnsi(buffer);
                WTSFreeMemory(buffer);

                if (prependDomain)
                {
                    if (WTSQuerySessionInformation(IntPtr.Zero, sessionId, WtsInfoClass.WTSDomainName, out buffer, out len) && len > 1)
                    {
                        process.UserName = ($"{Marshal.PtrToStringAnsi(buffer)}\\{process.UserName}");
                        WTSFreeMemory(buffer);
                    }
                }
            }

            return process.UserName;
        }

        #endregion
    }
}
