using ChatManager.Properties;

namespace ChatManager.Services
{
    internal class GetSetSettings
    {
        private static readonly string localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SWTOR\\swtor\\settings");

        private static readonly string backupPath = Path.Combine(localPath, "Backups");
        private static readonly bool backupDir = false;

        private static readonly string logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Zagrthos\\SWTOR-ChatManager\\Logs");

        public static string GetLocalPath => Settings.Default.localPath;        
        public static string GetBackupPath => Settings.Default.backupPath;
        public static bool GetBackupAvailability => Settings.Default.backupAvailability;
        public static string GetLogPath => Settings.Default.logPath;
        public static string GetSupportPath => Settings.Default.supportPath;
        public static string GetBugPath => Settings.Default.bugPath;

        public static void InitSettings()
        {
            if (!Settings.Default._Initialized)
            {
                Settings.Default.localPath = localPath;
                Settings.Default.backupPath = backupPath;
                Settings.Default.backupAvailability = backupDir;
                Settings.Default.logPath = logPath;
                Settings.Default._Initialized = true;
                Settings.Default.Save();
            }
        }
        
        public static void SaveSettings(string settingName, bool value)
        {
            switch (settingName)
            {
                case "backupAvailability":
                    Settings.Default.backupAvailability = value;
                    break;
                default:
                    throw new NotImplementedException();
            }
            Settings.Default.Save();
        }

        public static void RestoreSettings()
        {
            Settings.Default.Reset();
            Settings.Default.Save();
        }
    }
}
