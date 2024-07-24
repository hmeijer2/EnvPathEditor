namespace EnvPathEditor
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnApply = new Button();
            listView1 = new ListView();
            menuStrip1 = new MenuStrip();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            menuItemBackup = new ToolStripMenuItem();
            menuItemLoad = new ToolStripMenuItem();
            menuItemValidatePaths = new ToolStripMenuItem();
            menuItemAutoBackup = new ToolStripMenuItem();
            menuItemSwitch = new ToolStripMenuItem();
            menuItemHelp = new ToolStripMenuItem();
            btnAdd = new Button();
            txtAdd = new TextBox();
            btnBrowse = new Button();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // btnApply
            // 
            btnApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnApply.Location = new Point(570, 529);
            btnApply.Margin = new Padding(6);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(271, 44);
            btnApply.TabIndex = 0;
            btnApply.Text = "Apply Changes";
            btnApply.UseVisualStyleBackColor = true;
            btnApply.Click += btnApply_Click;
            // 
            // listView1
            // 
            listView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView1.Location = new Point(22, 113);
            listView1.Margin = new Padding(6);
            listView1.Name = "listView1";
            listView1.Size = new Size(816, 400);
            listView1.TabIndex = 3;
            listView1.Tag = "";
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.KeyDown += listView1_KeyDown;
            listView1.MouseUp += listView1_MouseUp;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.White;
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolsToolStripMenuItem, menuItemSwitch, menuItemHelp });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(11, 4, 0, 4);
            menuStrip1.Size = new Size(864, 37);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuItemBackup, menuItemLoad, menuItemValidatePaths, menuItemAutoBackup });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(67, 29);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // menuItemBackup
            // 
            menuItemBackup.Name = "menuItemBackup";
            menuItemBackup.Size = new Size(301, 30);
            menuItemBackup.Text = "Backup";
            menuItemBackup.Click += menuItemBackup_Click;
            // 
            // menuItemLoad
            // 
            menuItemLoad.Name = "menuItemLoad";
            menuItemLoad.Size = new Size(301, 30);
            menuItemLoad.Text = "Load backup";
            menuItemLoad.Click += menuItemLoad_Click;
            // 
            // menuItemValidatePaths
            // 
            menuItemValidatePaths.Name = "menuItemValidatePaths";
            menuItemValidatePaths.Size = new Size(301, 30);
            menuItemValidatePaths.Text = "Validate paths";
            menuItemValidatePaths.Click += menuItemValidatePaths_Click;
            // 
            // menuItemAutoBackup
            // 
            menuItemAutoBackup.Checked = true;
            menuItemAutoBackup.CheckState = CheckState.Checked;
            menuItemAutoBackup.Name = "menuItemAutoBackup";
            menuItemAutoBackup.Size = new Size(301, 30);
            menuItemAutoBackup.Text = "Auto backup before apply";
            menuItemAutoBackup.Click += menuItemAutoBackup_Click;
            // 
            // menuItemSwitch
            // 
            menuItemSwitch.Name = "menuItemSwitch";
            menuItemSwitch.Size = new Size(144, 29);
            menuItemSwitch.Tag = "Global";
            menuItemSwitch.Text = "Switch to User";
            menuItemSwitch.Click += menuItemSwitch_Click;
            // 
            // menuItemHelp
            // 
            menuItemHelp.Name = "menuItemHelp";
            menuItemHelp.Size = new Size(63, 29);
            menuItemHelp.Text = "Help";
            menuItemHelp.Click += menuItemHelp_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(22, 58);
            btnAdd.Margin = new Padding(6);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(138, 44);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtAdd
            // 
            txtAdd.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtAdd.Location = new Point(170, 62);
            txtAdd.Margin = new Padding(6);
            txtAdd.Name = "txtAdd";
            txtAdd.PlaceholderText = "Path text, or search path text";
            txtAdd.Size = new Size(589, 33);
            txtAdd.TabIndex = 6;
            txtAdd.TextChanged += txtAdd_TextChanged;
            // 
            // btnBrowse
            // 
            btnBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBrowse.Location = new Point(774, 58);
            btnBrowse.Margin = new Padding(6);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(68, 44);
            btnBrowse.TabIndex = 7;
            btnBrowse.Text = "...";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(864, 587);
            Controls.Add(btnBrowse);
            Controls.Add(txtAdd);
            Controls.Add(btnAdd);
            Controls.Add(listView1);
            Controls.Add(btnApply);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(6);
            Name = "MainForm";
            Text = "Environment Path Editor";
            Load += LoadMain;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private Button btnAdd;
        private TextBox txtAdd;
        private Button btnBrowse;
        private ListView listView1;
        private Button btnApply;

        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem switchToUserToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;

        private ToolStripMenuItem menuItemSwitch;
        private ToolStripMenuItem menuItemBackup;
        private ToolStripMenuItem menuItemAutoBackup;
        private ToolStripMenuItem menuItemValidatePaths;
        private ToolStripMenuItem menuItemHelp;
        private ToolStripMenuItem menuItemLoad;

    }
}
