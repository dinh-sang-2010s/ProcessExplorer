using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace ProcessExplorer
{
    public partial class PropertiesForm : Form
    {
        
        public ProcessExplorer _pe;


        public PropertiesForm(ProcessExplorer processExplorer)
        {
            InitializeComponent();
            _pe = processExplorer;
            
            this.Load += onPropertiesFormLoad;
        }

        private void onPropertiesFormLoad(object sender, EventArgs e)
        {
            txtDescription.Text = _pe.Description;
            txtPath.Text = _pe.ExecutablePath;
            txtUser.Text = _pe.UserName;
            txtCommadLine.Text = _pe.CommandLine;
        }

        private void btnExplore(object sender, EventArgs e)
        {
            Process.Start(txtPath.Text.Substring(0, txtPath.Text.IndexOf(_pe.Name)));
        }       
    }
}
