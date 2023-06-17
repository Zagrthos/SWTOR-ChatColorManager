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

            string[] fileNames = new string[dirContent.Length];

            for (int i = 0;i < dirContent.Length;i++)
            {
                string fileName = Path.GetFileName(dirContent[i]);
                string[] parts = fileName.Split("_");
                fileNames[i] = $"{Path.GetFileName(parts[1])} ()";
                Logging.Write(LogEvent.Variable, ProgramClass.BackupSelector, fileNames[i]);
            }

            clbxBackupFiles.DataSource = fileNames;
        }

        private void RestoreBackupFiles()
        {

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
