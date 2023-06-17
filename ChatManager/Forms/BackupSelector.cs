using ChatManager.Services;

namespace ChatManager.Forms
{
    public partial class BackupSelector : Form
    {
        public BackupSelector()
        {
            InitializeComponent();
            DisplayBackupDirs();
        }

        private static readonly string backupPath = GetSetSettings.GetBackupPath;
        private string[,] filesInDir = new string[1000, 2];

        private void DisplayBackupDirs()
        {
            Logging.Write(LogEvent.Method, ProgramClass.BackupSelector, "DisplayBackupDirs entered");
            string[] backupDirs = Directory.GetDirectories(backupPath);
            Logging.Write(LogEvent.Variable, ProgramClass.BackupSelector, $"backupDirs: {backupDirs.Length}");

            string[] backupDirsName = new string[backupDirs.Length];
            Logging.Write(LogEvent.Variable, ProgramClass.BackupSelector, $"backupDirsName: {backupDirsName.Length}");

            for (int i = 0; i < backupDirs.Length; i++)
            {
                backupDirsName[i] = Path.GetFileName(backupDirs[i]);
                Logging.Write(LogEvent.Variable, ProgramClass.BackupSelector, backupDirsName[i]);
            }

            Array.Reverse(backupDirsName);

            lbxBackupDir.DataSource = backupDirsName;
        }

        private void DisplayBackupFiles(string dirName)
        {
            Logging.Write(LogEvent.Method, ProgramClass.BackupSelector, "DisplayBackupDirs entered");
            string[] dirContent = Directory.GetFiles(dirName);
            Logging.Write(LogEvent.Variable, ProgramClass.BackupSelector, $"fileNames: {dirContent.Length}");

            AssociateFilesWithPaths(dirContent);

            string[] files = new string[dirContent.Length];

            for (int i = 0; i < filesInDir.Length / 2; i++)
            {
                Logging.Write(LogEvent.Variable, ProgramClass.BackupSelector, $"{i}");

                if (!string.IsNullOrEmpty(filesInDir[i, 0]) && !string.IsNullOrEmpty(filesInDir[i, 1]))
                {
                    Logging.Write(LogEvent.Variable, ProgramClass.BackupSelector, $"{filesInDir[i, 0]} & {filesInDir[i, 1]}");

                    Logging.Write(LogEvent.Variable, ProgramClass.BackupSelector, $"file[{i}]");
                    files[i] = $"{filesInDir[i, 0]} - {Converter.AddWhitespace(Converter.ServerNameIdentifier(filesInDir[i, 1], false))}";
                    Logging.Write(LogEvent.Variable, ProgramClass.BackupSelector, files[i]);
                }
                else
                {
                    Logging.Write(LogEvent.Variable, ProgramClass.BackupSelector, "Already done or null");
                    break;
                }
            }

            clbxBackupFiles.DataSource = files;
            }

            clbxBackupFiles.DataSource = fileNames;
        }

        private void AssociateFilesWithPaths(string[] paths)
        {
            // Clear the Array
            filesInDir = new string[1000, 3];

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
    }
}
