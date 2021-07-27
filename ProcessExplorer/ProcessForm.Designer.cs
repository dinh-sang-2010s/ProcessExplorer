namespace ProcessExplorer
{
    partial class ProcessForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessForm));
            this.ProcessName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ProcessId = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.PrivatePageCount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.WorkingSetSize = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Description = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.UserName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.processListView = new BrightIdeasSoftware.TreeListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.killProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.killProcessToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.suspendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.processListView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProcessName
            // 
            this.ProcessName.AspectName = "Name";
            this.ProcessName.CellPadding = null;
            this.ProcessName.Text = "Process Name";
            this.ProcessName.Width = 400;
            // 
            // ProcessId
            // 
            this.ProcessId.AspectName = "ProcessId";
            this.ProcessId.CellPadding = null;
            this.ProcessId.DisplayIndex = 1;
            this.ProcessId.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ProcessId.Text = "PID";
            this.ProcessId.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ProcessId.Width = 80;
            // 
            // PrivatePageCount
            // 
            this.PrivatePageCount.AspectName = "PrivatePageCount";
            this.PrivatePageCount.CellPadding = null;
            this.PrivatePageCount.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PrivatePageCount.Text = "Private Bytes";
            this.PrivatePageCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PrivatePageCount.Width = 100;
            // 
            // WorkingSetSize
            // 
            this.WorkingSetSize.AspectName = "WorkingSetSize";
            this.WorkingSetSize.CellPadding = null;
            this.WorkingSetSize.DisplayIndex = 3;
            this.WorkingSetSize.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.WorkingSetSize.Text = "Working Set";
            this.WorkingSetSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.WorkingSetSize.Width = 100;
            // 
            // Description
            // 
            this.Description.AspectName = "Description";
            this.Description.CellPadding = null;
            this.Description.Text = "Description";
            this.Description.Width = 350;
            // 
            // UserName
            // 
            this.UserName.AspectName = "UserName";
            this.UserName.CellPadding = null;
            this.UserName.Text = "User Name";
            this.UserName.Width = 250;
            // 
            // processListView
            // 
            this.processListView.AllColumns.Add(this.ProcessName);
            this.processListView.AllColumns.Add(this.WorkingSetSize);
            this.processListView.AllColumns.Add(this.PrivatePageCount);
            this.processListView.AllColumns.Add(this.ProcessId);
            this.processListView.AllColumns.Add(this.Description);
            this.processListView.AllColumns.Add(this.UserName);
            this.processListView.AlternateRowBackColor = System.Drawing.Color.White;
            this.processListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.processListView.BackColor = System.Drawing.SystemColors.Window;
            this.processListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ProcessName,
            this.WorkingSetSize,
            this.PrivatePageCount,
            this.ProcessId,
            this.Description,
            this.UserName});
            this.processListView.ContextMenuStrip = this.contextMenuStrip1;
            this.processListView.FullRowSelect = true;
            this.processListView.HideSelection = false;
            this.processListView.LargeImageList = this.imageList1;
            this.processListView.Location = new System.Drawing.Point(2, 2);
            this.processListView.Name = "processListView";
            this.processListView.OwnerDraw = true;
            this.processListView.ShowGroups = false;
            this.processListView.Size = new System.Drawing.Size(954, 447);
            this.processListView.SmallImageList = this.imageList1;
            this.processListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.processListView.TabIndex = 0;
            this.processListView.UseCompatibleStateImageBehavior = false;
            this.processListView.View = System.Windows.Forms.View.Details;
            this.processListView.VirtualMode = true;
            this.processListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.processListView_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.SystemColors.Window;
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.killProcessToolStripMenuItem,
            this.killProcessToolStripMenuItem1,
            this.restartToolStripMenuItem,
            this.suspendToolStripMenuItem,
            this.toolStripSeparator1,
            this.propertiesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(214, 120);
            // 
            // killProcessToolStripMenuItem
            // 
            this.killProcessToolStripMenuItem.Name = "killProcessToolStripMenuItem";
            this.killProcessToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.killProcessToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.killProcessToolStripMenuItem.Text = "Kill Process";
            this.killProcessToolStripMenuItem.Click += new System.EventHandler(this.killProcess_Click);
            // 
            // killProcessToolStripMenuItem1
            // 
            this.killProcessToolStripMenuItem1.Name = "killProcessToolStripMenuItem1";
            this.killProcessToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)));
            this.killProcessToolStripMenuItem1.Size = new System.Drawing.Size(213, 22);
            this.killProcessToolStripMenuItem1.Text = "Kill Process Tree";
            this.killProcessToolStripMenuItem1.Click += new System.EventHandler(this.killProcessTree_Click);
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartProcess_Click);
            // 
            // suspendToolStripMenuItem
            // 
            this.suspendToolStripMenuItem.Name = "suspendToolStripMenuItem";
            this.suspendToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.suspendToolStripMenuItem.Text = "Suspend";
            this.suspendToolStripMenuItem.Click += new System.EventHandler(this.suspendAndResumeProcess_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(210, 6);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.propertiesToolStripMenuItem.Text = "Properties...";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "user");
            this.imageList1.Images.SetKeyName(1, "folder");
            // 
            // ProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 450);
            this.Controls.Add(this.processListView);
            this.Name = "ProcessForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ProcessForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.onProcessFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.processListView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.OLVColumn ProcessName;
        private BrightIdeasSoftware.OLVColumn ProcessId;
        private BrightIdeasSoftware.OLVColumn PrivatePageCount;
        private BrightIdeasSoftware.OLVColumn WorkingSetSize;
        private BrightIdeasSoftware.OLVColumn Description;
        private BrightIdeasSoftware.OLVColumn UserName;
        private BrightIdeasSoftware.TreeListView processListView;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem killProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem suspendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem killProcessToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

