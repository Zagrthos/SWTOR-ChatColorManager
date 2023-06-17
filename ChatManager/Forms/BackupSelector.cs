using ChatManager.Services;

namespace ChatManager.Forms
{
    public partial class BackupSelectorForm : Form
    {
        public BackupSelectorForm()
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
            Logging.Write(LogEvent.Method, ProgramClass.BackupSelector, "DisplayBackupDirs entered");
            
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

            clbxBackupFiles.DataSource = fileNames;
        }

        private void AssociateFilesWithPaths(string[] paths)
        {
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

        private void lbxBackupDir_SelectedIndexChanged(object sender, EventArgs e)
        {
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
