using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

// TODO: thread, thread pool, task, parallel, background worker, async/await, timer 1s change workingsetsize & privatepagecount   
namespace ProcessExplorer
{
    public partial class ProcessForm : Form
    {
        private List<ProcessExplorer> _processes;
        private System.Windows.Forms.Timer _timer;

        public ProcessForm()
        {
            InitializeComponent();
            this.Load += onProcessFormLoad;

            _timer = new System.Windows.Forms.Timer();
            _timer.Enabled = true;
            _timer.Interval = 1000;
            _timer.Tick += new EventHandler(this.onTimerStart);
        }

        private void onProcessFormLoad(object sender, EventArgs e)
        {
            setupTree();
            refeshData();
        }

        private void setupTree()
        {
            this.processListView.UseCellFormatEvents = true;

            this.processListView.FormatCell += (sender, args) =>
            {
                var p = args.Model as ProcessExplorer;

                if (string.Equals(p.Name, @"explorer.exe", StringComparison.OrdinalIgnoreCase))
                {
                    args.SubItem.BackColor = (Color)ColorTranslator.FromHtml("#00eaea");
                }

                if (args.Column.Text == "Working Set")
                {
                    args.SubItem.BackColor = (Color)ColorTranslator.FromHtml("#fff1ea");
                    return;
                }

                if (args.Column.Text == "Private Bytes")
                {
                    args.SubItem.BackColor = (Color)ColorTranslator.FromHtml("#fffbea");
                    return;
                }
            };

            this.processListView.CanExpandGetter = delegate (object x)
            {
                var p = (x as ProcessExplorer);
                if (p.ProcessId == 0)
                {
                    return false;
                }

                if (p.ProcessId == 4)
                {
                    return false;
                }

                return _processes.Count(y => y.ParentProcessId == p.ProcessId) > 0;
            };

            this.processListView.ChildrenGetter = delegate (object x)
            {
                var p = (x as ProcessExplorer);
                var childrenProcesses = _processes.FindAll(z => z.ParentProcessId == p.ProcessId);
                return childrenProcesses;
            };

            //TODO:format icon
            this.ProcessName.ImageGetter = delegate (object x)
            {
                if (x == null)
                {
                    return 1;
                }

                var p = (ProcessExplorer)x;
                if (string.IsNullOrEmpty(p.ExecutablePath))
                {
                    var ic = Icon.ToBitmap();
                    var ic2 = new Bitmap(ic, 16, 16);
                    return ic2; ;
                }

                if (!File.Exists(p.ExecutablePath))
                {
                    return 1;
                }

                //TODO: format icon 16x16
                var bm = Icon.ExtractAssociatedIcon(p.ExecutablePath).ToBitmap();
                var bm2 = new Bitmap(bm, 16, 16);
                return bm2;
            };

            this.WorkingSetSize.AspectGetter = delegate (object x)
            {
                if (x == null)
                {
                    return String.Empty;
                }

                var p = (ProcessExplorer)x;

                return ($"{(p.WorkingSetSize / 1024):N0} K");
            }; 
            //TODO:format $
            this.PrivatePageCount.AspectGetter = delegate (object x)
            {
                if (x == null)
                {
                    return String.Empty;
                }

                var p = (ProcessExplorer)x;

                return ($"{(p.PrivatePageCount / 1024):N0} K");
            };
        }

        private void refeshData()
        {
            List<ProcessExplorer> roots = new List<ProcessExplorer>();
            _processes = ProcessExplorer.GetProcesses();
            foreach (var item in _processes)
            {
                var nonPersistentProcess = (_processes.FindIndex(x => x.ProcessId == item.ParentProcessId) == -1) || item.ProcessId == 0 || item.ProcessId == 4;
                if (nonPersistentProcess)
                {
                    roots.Add(item);
                }
            }

            processListView.Roots = roots;
            processListView.ExpandAll();
        }

        private void processListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            showData();
        }

        private void showData()
        {
            var p = (processListView.SelectedObject as ProcessExplorer);
            PropertiesForm f = new PropertiesForm(p);
            f.ShowDialog();
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showData();
        }

        private void killProcess_Click(object sender, EventArgs e)
        {
            var z = (processListView.SelectedObject as ProcessExplorer);
            killProcess(z.ProcessId);
            processListView.HighlightBackgroundColor = (Color)ColorTranslator.FromHtml("#EA3711");
            refeshData();
            processListView.HighlightBackgroundColor = default(Color);
        }

        private void killProcess(int pid)
        {
            // Cannot close 'system idle process id=0'.
            if (pid == 0)
            {
                return;
            }

            Process process = Process.GetProcessById(pid);
            DialogResult result = MessageBox.Show($"Are you sure you want to kill {process.ProcessName}", "ProcessExploere", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            // If the no button was pressed ...
            if (result == DialogResult.OK)
            {
                process.Kill();
            }
        }

        private void killProcessTree_Click(object sender, EventArgs e)
        {
            var z = (processListView.SelectedObject as ProcessExplorer);
            killProcessTree(z.ProcessId);
            processListView.HighlightBackgroundColor = (Color)ColorTranslator.FromHtml("#EA3711");
            refeshData();
            processListView.HighlightBackgroundColor = default(Color);
        }

        private void killProcessTree(int pid)
        {
            if (pid == 0)
            {
                return;
            }

            var toBeKillProcesses = _processes.FindAll(x => x.ParentProcessId == pid);

            foreach (var item in toBeKillProcesses)
            {
                killProcessTree(item.ProcessId);
            }

            Process proc = Process.GetProcessById(pid);
            proc.Kill();
        }

        private void restartProcess_Click(object sender, EventArgs e)
            {
            var z = (processListView.SelectedObject as ProcessExplorer);
            killProcess(z.ProcessId);
            var path = z.ExecutablePath;
            var commad = z.CommandLine;
            var arguments = "";
            int index = 0;

            #region
            //var comanSplit = arguments.Split('"').ToList();
            //if (comanSplit.Count == 2)
            //{
            //    var a = comanSplit.LastOrDefault().Trim();
            //}
            //else
            //{
            //    var parts = arguments.Split(' ');
            //    var lastName = parts;
            //    var c = "";
            //    for (int i = 1; i < parts.Count(); i++)
            //    {
            //        c += " " + parts[i];
            //    }
            //    c.Trim();
            //}
            #endregion

            bool isSpace = z.CommandLine[0] == '\"';
            if (isSpace)
            {
                index = path.Length + 3; 
            }
            else
            {
                index = path.Length + 1;
            }
            arguments = z.CommandLine.Substring(index,z.CommandLine.Length-index);
           
            Process restart = Process.Start(path, arguments);
            processListView.HighlightBackgroundColor = (Color)ColorTranslator.FromHtml("#99ff66");
            refeshData();
            processListView.HighlightBackgroundColor = default;
        }

        #region suspend & resume
        [Flags]
        public enum ThreadAccess : int
        {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002),
            GET_CONTEXT = (0x0008),
            SET_CONTEXT = (0x0010),
            SET_INFORMATION = (0x0020),
            QUERY_INFORMATION = (0x0040),
            SET_THREAD_TOKEN = (0x0080),
            IMPERSONATE = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        static extern int ResumeThread(IntPtr hThread);
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool CloseHandle(IntPtr handle);
        #endregion

        //TODO: Notes: NTsuspend
        private void suspendProcess(int pid)
        {
            var process = Process.GetProcessById(pid);

            if (process.ProcessName == string.Empty)
                return;

            foreach (ProcessThread pT in process.Threads)
            {
                IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                if (pOpenThread == IntPtr.Zero)
                {
                    continue;
                }

                SuspendThread(pOpenThread);

                CloseHandle(pOpenThread);
            }
        }

        private void resumeProcess(int pid)
        {
            var process = Process.GetProcessById(pid);

            if (process.ProcessName == string.Empty)
                return;

            foreach (ProcessThread pT in process.Threads)
            {
                IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                if (pOpenThread == IntPtr.Zero)
                {
                    continue;
                }

                var suspendCount = 0;
                do
                {
                    suspendCount = ResumeThread(pOpenThread);
                } while (suspendCount > 0);

                CloseHandle(pOpenThread);
            }
        }

        private void suspendAndResumeProcess_Click(object sender, EventArgs e)
        {
            var z = (processListView.SelectedObject as ProcessExplorer);
            if ((sender as ToolStripMenuItem).Text == "Suspend")
            {
                suspendProcess(z.ProcessId);
                (sender as ToolStripMenuItem).Text = "Resume";
            }
            else
            {
                (sender as ToolStripMenuItem).Text = "Suspend";
                resumeProcess(z.ProcessId);
            }
        }

        private void onTimerStart(object sender, EventArgs e)
        {
            try
            {
                _timer.Stop();

                List<ProcessExplorer> currentProcesses = ProcessExplorer.GetProcesses();

                bool rebuild = false;

                foreach (var item in currentProcesses)
                {
                    int idx = _processes.FindIndex(x => x.ProcessId == item.ProcessId);
                    bool exists = idx >= 0;

                    if (exists)
                    {
                        var p = _processes[idx];
                        p.WorkingSetSize = item.WorkingSetSize;
                        p.PrivatePageCount = item.PrivatePageCount;
                    }
                    else
                    {
                        _processes.Add(item);
                        rebuild = true;
                    }

                }

                var deleteCount = _processes.RemoveAll(x => currentProcesses.FindIndex(xx => xx.ProcessId == x.ProcessId) < 0);


                if (rebuild || deleteCount > 0)

                {
                    processListView.RebuildAll(true);
                }
                else
                {
                    processListView.Refresh();
                }
            }
            catch /*(Exception)*/
            {
            }
            finally
            {
                _timer.Start();
            }
        }
    }
}

