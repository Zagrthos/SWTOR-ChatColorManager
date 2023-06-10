using ChatManager.Properties;

namespace ChatManager.Services
{
    internal class GetSetSettings
    {
        private static readonly string localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SWTOR\\swtor\\settings");

        private static readonly string backupPath = Path.Combine(localPath, "Backups");
        private static readonly bool backupDir = false;

        private static readonly string logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Zagrthos\\SWTOR-ChatManager\\Logs");
        private static readonly string autosavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Zagrthos\\SWTOR-ChatManager\\Autosave");

        public static string GetCurrentLocale => Settings.Default._locale;
        public static string GetLocalPath => Settings.Default.localPath;
        public static string GetBackupPath => Settings.Default.backupPath;
        public static bool GetBackupAvailability => Settings.Default.backupAvailability;
        public static string GetLogPath => Settings.Default.logPath;
        public static string GetAutosavePath => Settings.Default.autosavePath;
        public static string GetSupportPath => Settings.Default.supportPath;
        public static string GetBugPath => Settings.Default.bugPath;
        public static string GetBugMailpath => Settings.Default.bugMailPath;
        public static string GetAboutPictureLink => Settings.Default.copyrightPicture;
        public static bool GetSaveOnClose => Settings.Default._saveOnClose;
        public static bool GetAutosave => Settings.Default._autosave;
        public static decimal GetAutosaveInterval => Settings.Default._autosaveInterval;

        public static void InitSettings()
        {
            if (!Settings.Default._Initialized)
            {
                Settings.Default.Upgrade();
                Settings.Default.localPath = localPath;
                Settings.Default.backupPath = backupPath;
                Settings.Default.backupAvailability = backupDir;
                Settings.Default.logPath = logPath;
                Settings.Default.autosavePath = autosavePath;
                Settings.Default._autosaveInterval = 0;
                Settings.Default._Initialized = true;
                Settings.Default.Save();
            }
        }

        // This is for setting strings
        public static void SaveSettings(string settingName, string settingValue)
        {
            switch (settingName)
            {
                case "_locale":
                    Settings.Default._locale = settingValue;
                    break;

                default:
                    throw new NotImplementedException();
            }
            Settings.Default.Save();
        }
        
        // This is for setting booleans
        public static void SaveSettings(string settingName, bool value)
        {
            switch (settingName)
            {
                case "backupAvailability":
                    Settings.Default.backupAvailability = value;
                    break;

                case "_autosave":
                    Settings.Default._autosave = value;
                    break;

                case "_saveOnClose":
                    Settings.Default._saveOnClose = value;
                    break;

                default:
                    throw new NotImplementedException();
            }
            Settings.Default.Save();
        }

        public static void SaveSettings(string settingName, decimal value)
        {
            switch (settingName)
            {
                case "_autosaveInterval":
                    Settings.Default._autosaveInterval = value;
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
