using ChatManager.Properties;
using System.Globalization;

namespace ChatManager.Services
{
    internal enum Setting
    {
        autosave,
        autosaveAvailability,
        autosaveInterval,
        backupAvailability,
        locale,
        reloadOnStartup,
        reset,
        saveOnClose
    }

    internal class GetSetSettings
    {
        private static readonly string localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SWTOR\\swtor\\settings");

        private static readonly string backupPath = Path.Combine(localPath, "Backups");
        private static readonly bool backupDir = false;

        private static readonly string logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Zagrthos\\SWTOR-ChatManager\\Logs");

        private static readonly string autosavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Zagrthos\\SWTOR-ChatManager\\Autosave");
        private static readonly bool autosaveDir = false;

        public static string GetCurrentLocale => Settings.Default._locale;
        public static string GetLocalPath => Settings.Default.localPath;
        public static string GetBackupPath => Settings.Default.backupPath;
        public static bool GetBackupAvailability => Settings.Default.backupAvailability;
        public static string GetLogPath => Settings.Default.logPath;
        public static string GetAutosavePath => Settings.Default.autosavePath;
        public static bool GetAutosaveAvailability => Settings.Default.autosaveAvailability;
        public static string GetSupportPath => Settings.Default.supportPath;
        public static string GetBugPath => Settings.Default.bugPath;
        public static string GetBugMailpath => Settings.Default.bugMailPath;
        public static string GetAboutPictureLink => Settings.Default.copyrightPicture;
        public static bool GetSaveOnClose => Settings.Default._saveOnClose;
        public static bool GetReloadOnStartup => Settings.Default._reloadOnStartup;
        public static bool GetAutosave => Settings.Default._autosave;
        public static decimal GetAutosaveInterval => Settings.Default._autosaveInterval;
        public static bool GetReset => Settings.Default._reset;

        public static void InitSettings()
        {
            if (!Settings.Default._Initialized)
            {
                Settings.Default.localPath = localPath;
                Settings.Default.backupPath = backupPath;
                Settings.Default.backupAvailability = backupDir;
                Settings.Default.logPath = logPath;
                Settings.Default.autosavePath = autosavePath;
                Settings.Default.autosaveAvailability = autosaveDir;
                Settings.Default._autosaveInterval = 0;
                Settings.Default._Initialized = true;
                Settings.Default.Save();
            }

            if (Settings.Default._upgradeRequired)
            {
                Settings.Default.Upgrade();
                Settings.Default._upgradeRequired = false;
                Settings.Default.Save();
            }
        }

        // This is for setting strings
        public static void SaveSettings(Setting settingName, string settingValue)
        {
            switch (settingName)
            {
                case Setting.locale:
                    Settings.Default._locale = settingValue;
                    break;

                default:
                    throw new NotImplementedException();
            }
            Settings.Default.Save();
        }
        
        // This is for setting booleans
        public static void SaveSettings(Setting settingName, bool value)
        {
            switch (settingName)
            {
                case Setting.backupAvailability:
                    Settings.Default.backupAvailability = value;
                    break;

                case Setting.autosaveAvailability:
                    Settings.Default.autosaveAvailability = value;
                    break;

                case Setting.autosave:
                    Settings.Default._autosave = value;
                    break;

                case Setting.saveOnClose:
                    Settings.Default._saveOnClose = value;
                    break;

                case Setting.reloadOnStartup:
                    Settings.Default._reloadOnStartup = value;
                    break;

                case Setting.reset:
                    Settings.Default._reset = value;
                    break;

                default:
                    throw new NotImplementedException();
            }
            Settings.Default.Save();
        }

        // This is for setting decimals
        public static void SaveSettings(Setting settingName, decimal value)
        {
            switch (settingName)
            {
                case Setting.autosaveInterval:
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

            SaveSettings(Setting.reset, true);

            if (string.IsNullOrEmpty(GetCurrentLocale))
            {
                SaveSettings(Setting.locale, CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            }

            Settings.Default._upgradeRequired = false;

            Settings.Default.Save();
        }
    }
}
