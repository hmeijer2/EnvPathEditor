namespace EnvPathEditor
{
    public partial class MainForm : Form
    {
        private ContextMenuStrip contextMenu;

        private Context _currentContext { get; set; }
        private bool _backupBeforeApply { get; set; }

        public MainForm()
        {
            InitializeComponent();
            InitializeContextMenu();
        }

        #region Controls

        private void btnApply_Click(object sender, EventArgs e)
        {
            ApplyPrompt();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewVar(txtAdd.Text);
        }

        private void menuItemSwitch_Click(object sender, EventArgs e)
        {
            ToggleContext();
        }

        private void menuItemHelp_Click(object sender, System.EventArgs e)
        {
            using (var help = new HelpForm())
                help.ShowDialog();
        }

        private void menuItemValidatePaths_Click(object sender, EventArgs e)
        {
            ValidatePaths(_currentContext);
        }

        private void menuItemAutoBackup_Click(object sender, EventArgs e)
        {
            menuItemAutoBackup.Checked = !menuItemAutoBackup.Checked;
            _backupBeforeApply = menuItemAutoBackup.Checked;
        }

        private void menuItemBackup_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.DefaultExt = "txt";
                dialog.FileName = GetBackupName();

                dialog.ShowDialog();

                CreateBackup(_currentContext, dialog.FileName);
            }
        }

        private void menuItemLoad_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.CheckFileExists = true;
                dialog.Multiselect = false;

                dialog.ShowDialog();

                LoadBackup(dialog.FileName);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                var result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (Directory.Exists(dialog.SelectedPath))
                    {
                        txtAdd.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        txtAdd.BackColor = Color.Red;
                    }

                    txtAdd.Text = dialog.SelectedPath;
                }
            }
        }

        #endregion

        private void LoadMain(object sender, EventArgs e)
        {
            _currentContext = Context.Global;
            _backupBeforeApply = menuItemAutoBackup.Checked;

            RefreshList();
        }

        private void ApplyPrompt()
        {
            var backup = _backupBeforeApply
                ? "\nA backup of the existing path variable will be automatically created.\n"
                : "\nIt is recommended to create a backup before making changes.\n";

            DialogResult confirm = MessageBox.Show(
                string.Format("You are about to apply new changes to the [{0}] PATH variable.\n{1}\nDo you wish to continue?",
                    _currentContext.ToString(), backup),
                "Do you wish to continue?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

            if (confirm == DialogResult.Yes)
            {
                var success = _backupBeforeApply
                    ? AutoBackup(_currentContext)
                    : true;

                if (success)
                {
                    var pathVariable = ConstructPathFromList();
                    SetPathVariable(_currentContext, pathVariable);
                }
                else
                {
                    MessageBox.Show("Could not create backup\n\nAborting.",
                       "Error",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Exclamation);
                }
            }
        }

        private void ToggleContext()
        {
            _currentContext = _currentContext == Context.Global
                ? Context.User
                : Context.Global;

            var title = _currentContext == Context.Global
                ? "Switch to User"
                : "Switch to Global";

            menuItemSwitch.Text = title;

            menuItemSwitch.Tag = _currentContext == Context.Global
                ? "Global"
                : "User";

            RefreshList();
        }

        private void RefreshList()
        {
            var variables = GetPathVariable(_currentContext);
            InitListView(variables, txtAdd.Text);
            ValidatePaths(_currentContext);
        }

        private void InitListView(List<string> vars, string text = "")
        {
            listView1.Clear();

            listView1.Scrollable = true;
            listView1.CheckBoxes = true;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;
            listView1.Sorting = SortOrder.Ascending;

            var header = new ColumnHeader();
            header.Text = $"{menuItemSwitch.Tag} Path (" + vars.Count.ToString() + ")";
            header.Name = "col1";
            header.Width = 600;
            listView1.Columns.Add(header);

            foreach (var v in vars.Where(i => i.ToLower().Contains(text.ToLower())))
            {
                var item = new ListViewItem(v);
                item.Checked = true;
                listView1.Items.Add(item);
            }
        }

        private void ValidatePaths(Context ctx)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                var exists = Directory.Exists(item.Text);

                item.BackColor = exists ? Color.LightGreen : Color.Red;
            }
        }

        private enum Context
        {
            Global = EnvironmentVariableTarget.Machine,
            User = EnvironmentVariableTarget.User
        }

        private void LoadBackup(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show("File does not exists: " + path,
                        "Error: File does not exists",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);

                    return;
                }

                var content = File.ReadAllText(path);

                var vars = content.Split(';')
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToList<string>();

                InitListView(vars);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(
                    "An error occured trying to load a backup\n\n{0}", ex.Message),
                    "Error");
            }
        }

        private bool AutoBackup(Context ctx)
        {
            var pathVar = Environment.GetEnvironmentVariable("path",
                (EnvironmentVariableTarget)ctx);

            var date = DateTime.Now.ToString("dd.MM.yy-HH.mm.ss");
            var name = string.Format("path-editor-auto-backup-{0}-{1}.txt",
                    _currentContext.ToString(), date);

            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), name);

            if (File.Exists(path))
            {
                MessageBox.Show("Could not create backup file. File already exists.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);

                return false;
            }

            File.WriteAllText(path, pathVar);

            return true;
        }

        private void CreateBackup(Context ctx, string filename)
        {
            var pathVar = Environment.GetEnvironmentVariable("path",
                (EnvironmentVariableTarget)ctx);

            if (File.Exists(filename))
            {
                MessageBox.Show("File already exists: " + filename,
                    "Error: File already exists",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                return;
            }

            File.WriteAllText(filename, pathVar);
        }

        private string GetBackupName()
        {
            var date = DateTime.Now.ToString("dd.MM.yy-HH.mm.ss");
            return string.Format("path-editor-backup-{0}-{1}",
                    _currentContext.ToString(), date);
        }

        private List<string> GetPathVariable(Context ctx)
        {
            var path = Environment.GetEnvironmentVariable("path",
                (EnvironmentVariableTarget)ctx);

            return path.Split(';')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList<string>();
        }

        private void SetPathVariable(Context ctx, string var)
        {
            var pathVar = ConstructPathFromList();

            try
            {
                Environment.SetEnvironmentVariable("path", var,
                    (EnvironmentVariableTarget)ctx);
            }
            catch (System.Security.SecurityException ex)
            {
                MessageBox.Show(string.Format("An error has occured trying to set the environment variable:\n"
                    + "\n{0}\n\nMake sure the program is run using \"Run as Administrator\"", ex.Message),
                    "Error - Are you sure you are running as an administrator?",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string ConstructPathFromList()
        {
            List<string> vars = listView1.Items
               .Cast<ListViewItem>()
               .Where(x => x.Checked)
               .Select(x => x.Text)
               .ToList<string>();

            return string.Join(";", vars);
        }

        private bool AddNewVar(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            if (!Directory.Exists(text))
            {
                txtAdd.BackColor = Color.Red;
                return false;
            }
            else
                txtAdd.BackColor = DefaultBackColor;

            var item = new ListViewItem();
            item.Checked = true;
            item.BackColor = Color.LightGreen;
            item.Text = text
                .TrimEnd()
                .TrimStart();

            listView1.Items.Add(item);

            return true;
        }

        private void txtAdd_TextChanged(object sender, EventArgs e)
        {
            var variables = GetPathVariable(_currentContext);
            InitListView(variables, txtAdd.Text);
        }

        private void InitializeContextMenu()
        {
            contextMenu = new ContextMenuStrip();
            var copyMenuItem = new ToolStripMenuItem("Copy");
            copyMenuItem.Click += CopyMenuItem_Click;
            contextMenu.Items.Add(copyMenuItem);
        }

        private void CopyMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedItemsToClipboard();
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var listView = sender as ListView;
                if (listView != null && listView.FocusedItem != null && listView.FocusedItem.Bounds.Contains(e.Location))
                {
                    contextMenu.Show(Cursor.Position);
                }
            }
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopySelectedItemsToClipboard();
                e.Handled = true;
            }
        }

        private void CopySelectedItemsToClipboard()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string copiedText = "";
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    copiedText += item.Text + ((item.SubItems.Count == 1) ? "": "\t" + string.Join("\t", item.SubItems.Cast<ListViewItem.ListViewSubItem>().Skip(1).Select(subItem => subItem.Text))) + ((listView1.SelectedItems.Count == 1) ? "" : Environment.NewLine);
                }
                Clipboard.SetText(copiedText);
            }
        }
    }
}
