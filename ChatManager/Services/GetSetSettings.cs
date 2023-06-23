using ChatManager.Enums;
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
        lastUpdateCheck,
        locale,
        reloadOnStartup,
        saveOnClose,
        settingsUpgradeRequired,
        updateDownload,
        updateInterval
    }

    internal class GetSetSettings
    {
        private static readonly string localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SWTOR\\swtor\\settings");

        private static readonly string backupPath = Path.Combine(localPath, "Backups");
        private static readonly bool backupDir = false;

        private static readonly string logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Zagrthos\\SWTOR-ChatManager\\Logs");

        private static readonly string autosavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Zagrthos\\SWTOR-ChatManager\\Autosave");
        private static readonly bool autosaveDir = false;

        internal static string GetCurrentLocale => Settings.Default._locale;
        internal static string GetLocalPath => Settings.Default.localPath;
        internal static string GetBackupPath => Settings.Default.backupPath;
        internal static bool GetBackupAvailability => Settings.Default.backupAvailability;
        internal static string GetLogPath => Settings.Default.logPath;
        internal static string GetAutosavePath => Settings.Default.autosavePath;
        internal static bool GetAutosaveAvailability => Settings.Default.autosaveAvailability;
        internal static string GetSupportPath => Settings.Default.supportPath;
        internal static string GetBugPath => Settings.Default.bugPath;
        internal static string GetBugMailpath => Settings.Default.bugMailPath;
        internal static string GetAboutPictureLink => Settings.Default.copyrightPicture;
        internal static bool GetSaveOnClose => Settings.Default._saveOnClose;
        internal static bool GetReloadOnStartup => Settings.Default._reloadOnStartup;
        internal static bool GetAutosave => Settings.Default._autosave;
        internal static decimal GetAutosaveInterval => Settings.Default._autosaveInterval;
        internal static string GetUpdateInterval => Settings.Default.updateInterval;
        internal static DateTime GetLastUpdateCheck => Settings.Default.lastUpdateCheck;
        internal static bool GetUpdateDownload => Settings.Default.updateDownload;

        internal static void InitSettings()
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
                Settings.Default.updateInterval = UpdateEnum.OnStartup.ToString();
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
        internal static void SaveSettings(Setting settingName, string settingValue)
        {
            switch (settingName)
            {
                case Setting.locale:
                    Settings.Default._locale = settingValue;
                    break;

                case Setting.updateInterval:
                    Settings.Default.updateInterval = settingValue;
                    break;

                default:
                    throw new NotImplementedException();
            }
            Settings.Default.Save();
        }

        // This is for setting booleans
        internal static void SaveSettings(Setting settingName, bool value)
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

                case Setting.settingsUpgradeRequired:
                    Settings.Default._upgradeRequired = value;
                    break;

                case Setting.updateDownload:
                    Settings.Default.updateDownload = value;
                    break;

                default:
                    throw new NotImplementedException();
            }
            Settings.Default.Save();
        }

        // This is for setting decimals
        internal static void SaveSettings(Setting settingName, decimal value)
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

        // This is for setting DateTimes
        internal static void SaveSettings(Setting settingName, DateTime value)
        {
            switch (settingName)
            {
                case Setting.lastUpdateCheck:
                    Settings.Default.lastUpdateCheck = value;
                    break;

                default:
                    throw new NotImplementedException();
            }
            Settings.Default.Save();
        }

        internal static void RestoreSettings()
        {
            Settings.Default.Reset();

            SaveSettings(Setting.settingsUpgradeRequired, false);

            if (string.IsNullOrEmpty(GetCurrentLocale))
            {
                SaveSettings(Setting.locale, CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            }

            InitSettings();
        }
    }
}