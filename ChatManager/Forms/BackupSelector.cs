using ChatManager.Services;

namespace ChatManager.Forms
{
    internal partial class BackupSelectorForm : Form
    {
        internal BackupSelectorForm()
        {
            InitializeComponent();
            Localize();
            DisplayBackupDirs();
        }

        private static readonly string backupPath = GetSetSettings.GetBackupPath;
        private string[,] filesInDir = new string[1000, 2];

        private void DisplayBackupDirs()
        {
            Logging.Write(LogEvent.Method, ProgramClass.BackupSelector, "DisplayBackupDirs entered");

            // Search the given Path for Directories
            string[] backupDirs = Directory.GetDirectories(backupPath);
            Logging.Write(LogEvent.Variable, ProgramClass.BackupSelector, $"backupDirs: {backupDirs.Length}");

            // Create new Array with the size of the found Paths
            string[] backupDirsName = new string[backupDirs.Length];
            Logging.Write(LogEvent.Variable, ProgramClass.BackupSelector, $"backupDirsName: {backupDirsName.Length}");

            // Fill the Array with the Directory names
            for (int i = 0; i < backupDirs.Length; i++)
            {
                backupDirsName[i] = Path.GetFileName(backupDirs[i]);
                Logging.Write(LogEvent.Variable, ProgramClass.BackupSelector, backupDirsName[i]);
            }

            // Reverse the names so the newest is first
            Array.Reverse(backupDirsName);

            // Set the DataSource
            lbxBackupDir.DataSource = backupDirsName;
        }

        private void DisplayBackupFiles(string dirName)
        {
            Logging.Write(LogEvent.Method, ProgramClass.BackupSelector, "DisplayBackupFiles entered");

            // Clear the DataSource so the checked Items get unchecked
            clbxBackupFiles.DataSource = null;

            // Search the given Path for Files
            string[] dirContent = Directory.GetFiles(dirName);
            Logging.Write(LogEvent.Variable, ProgramClass.BackupSelector, $"fileNames: {dirContent.Length}");

            // Associate the Files with their Paths
            AssociateFilesWithPaths(dirContent);

            // Create new Array with the size of the found Files
            string[] files = new string[dirContent.Length];

            for (int i = 0; i < filesInDir.Length / 2; i++)
            {
                Logging.Write(LogEvent.Variable, ProgramClass.BackupSelector, $"{i}");

                // Check if all parts in array are NOT empty or null
                if (!string.IsNullOrEmpty(filesInDir[i, 0]) && !string.IsNullOrEmpty(filesInDir[i, 1]) && !string.IsNullOrEmpty(filesInDir[i, 2]))
                {
                    Logging.Write(LogEvent.Variable, ProgramClass.BackupSelector, $"{filesInDir[i, 0]} & {filesInDir[i, 1]} & {filesInDir[i, 2]}");
                    Logging.Write(LogEvent.Variable, ProgramClass.BackupSelector, $"file[{i}]");

                    // If not null or empty add the name and the server to the list
                    files[i] = $"{filesInDir[i, 0]} - {Converter.AddWhitespace(Converter.ServerNameIdentifier(filesInDir[i, 1], false))}";
                    Logging.Write(LogEvent.Variable, ProgramClass.BackupSelector, files[i]);
                }
                else
                {
                    Logging.Write(LogEvent.Variable, ProgramClass.BackupSelector, "Already done or null");
                    break;
                }
            }

            // Set the DataSource
            clbxBackupFiles.DataSource = files;
        }

        private void AssociateFilesWithPaths(string[] paths)
        {
            Logging.Write(LogEvent.Method, ProgramClass.BackupSelector, "AssociateFilesWithPaths entered");

            // Clear the Array
            filesInDir = new string[1000, 3];

            // Now get the FileName, split it into parts and then
            // Set the name to pos 0
            // Set the server to pos 1
            // Set the path to pos 2
            for (int i = 0; i < paths.Length; i++)
            {
                string fileName = Path.GetFileName(paths[i]);
                string[] parts = fileName.Split("_");
                filesInDir[i, 0] = parts[1];
                filesInDir[i, 1] = parts[0];
                filesInDir[i, 2] = paths[i];
                Logging.Write(LogEvent.Variable, ProgramClass.BackupSelector, filesInDir[i, 0]);
                Logging.Write(LogEvent.Variable, ProgramClass.BackupSelector, filesInDir[i, 1]);
                Logging.Write(LogEvent.Variable, ProgramClass.BackupSelector, filesInDir[i, 2]);
            }
        }

        private void SelectBackupDir(object sender, EventArgs e)
        {
            Logging.Write(LogEvent.Method, ProgramClass.BackupSelector, "SelectBackupDir entered");

            if (sender is ListBox listBox)
            {
                string? dirName = listBox.SelectedItem?.ToString();

                if (!string.IsNullOrEmpty(dirName))
                {
                    DisplayBackupFiles(Path.Combine(backupPath, dirName));
                }
                else
                {
                    Logging.Write(LogEvent.Warning, ProgramClass.BackupSelector, "dirName is null or empty!");
                }
            }
            else
            {
                Logging.Write(LogEvent.Warning, ProgramClass.BackupSelector, $"Sender: {sender} is not a ListBox!");
            }
        }

        private void SelectClick(object sender, EventArgs e)
        {
            Logging.Write(LogEvent.Method, ProgramClass.BackupSelector, "SelectClick entered");

            if (sender is Button button)
            {
                bool isChecked = !button.Name.Contains("Deselect");
                for (int i = 0; i < clbxBackupFiles.Items.Count; i++)
                {
                    clbxBackupFiles.SetItemChecked(i, isChecked);
                }
                Logging.Write(LogEvent.Info, ProgramClass.BackupSelector, $"All Checks set to: {isChecked}");
            }
            else
            {
                Logging.Write(LogEvent.Warning, ProgramClass.BackupSelector, $"Sender: {sender} is not a Button!");
            }
        }

        private void Restore(object sender, EventArgs e)
        {
            Logging.Write(LogEvent.Method, ProgramClass.BackupSelector, "Restore entered");

            if (sender is Button button && button.Name == btnRestore.Name)
            {
                // Get all the checked items in an array
                string[] checkedItems = clbxBackupFiles.CheckedItems.Cast<string>().ToArray();
                Logging.Write(LogEvent.Variable, ProgramClass.BackupSelector, $"checkedItems: {checkedItems.Length}");

                string localPath = GetSetSettings.GetLocalPath;

                // Now loop the array
                for (int i = 0; i < checkedItems.Length; i++)
                {
                    // Split the name in two parts
                    string[] parts = checkedItems[i].Split(" - ");

                    // Generate the fileName
                    string fileName = Converter.ServerNameIdentifier(Converter.RemoveWhitespace(parts[1]), true) + $"_{parts[0]}_PlayerGUIState.ini";

                    // Generate the filePath
                    string path = Path.Combine(backupPath, lbxBackupDir.SelectedItem!.ToString()!, fileName);

                    // Replace the file
                    File.Copy(path, Path.Combine(localPath, fileName), true);
                }

                Localization localization = new(GetSetSettings.GetCurrentLocale);

                string changedFiles = localization.GetString("Inf_ExportedFiles");
                changedFiles = changedFiles.Replace("FILECOUNT", checkedItems.Length.ToString());

                ShowMessageBox.Show(localization.GetString("MessageBoxInfo"), changedFiles);
            }
            else
            {
                Logging.Write(LogEvent.Warning, ProgramClass.BackupSelector, $"Sender: {sender} is not a Button or {btnRestore.Name}!");
            }
        }

        private void Localize()
        {
            Logging.Write(LogEvent.Method, ProgramClass.BackupSelector, "Localize entered");

            Localization localization = new(GetSetSettings.GetCurrentLocale);

            Text = localization.GetString(Name);

            IEnumerable<Control> GetControls(Control parent, Type type)
            {
                var controls = parent.Controls.Cast<Control>();

                return controls
                    .Where(c => c.GetType() == type)
                    .Concat(controls.SelectMany(c => GetControls(c, type)));
            }

            var buttons = GetControls(this, typeof(Button));
            var labels = GetControls(this, typeof(Label));

            foreach (Control control in buttons)
            {
                if (control is Button button)
                {
                    button.Text = localization.GetString(button.Name);
                }
            }

            foreach (Control control in labels)
            {
                if (control is Label label && label.Name != lblDateConvertion.Name)
                {
                    label.Text = localization.GetString(label.Name);
                }
            }

            (string date, string time) = localization.GetLocalDateTime();

            lblDateConvertion.Text = $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss} = {date} - {time}";
        }
    }
}