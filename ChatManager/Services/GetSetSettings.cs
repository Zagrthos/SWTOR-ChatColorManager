using System;
using System.Globalization;
using System.IO;
using ChatManager.Enums;
using ChatManager.Properties;

namespace ChatManager.Services;

internal static class GetSetSettings
{
    private static readonly string LocalPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SWTOR\\swtor\\settings");
    private static readonly string BackupPath = Path.Combine(LocalPath, "Backups");
    private static readonly bool BackupDir;
    private static readonly string LogPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Zagrthos\\SWTOR-ChatManager\\Logs");
    private static readonly string AutosavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Zagrthos\\SWTOR-ChatManager\\Autosave");
    private static readonly bool AutosaveDir;

    internal static string GetCurrentLocale => Settings.Default._locale;
    internal static string GetLocalPath => Settings.Default.localPath;
    internal static string GetBackupPath => Settings.Default.backupPath;
    internal static bool GetBackupAvailability => Settings.Default.backupAvailability;
    internal static string GetLogPath => Settings.Default.logPath;
    internal static string GetAutosavePath => Settings.Default.autosavePath;
    internal static bool GetAutosaveAvailability => Settings.Default.autosaveAvailability;
    internal static string GetSupportPath => Resources.supportPath;
    internal static string GetBugPath => Resources.bugPath;
    internal static string GetBugMailpath => Resources.bugMailPath;
    internal static string GetLicences => Resources.Licences;
    internal static bool GetSaveOnClose => Settings.Default._saveOnClose;
    internal static bool GetReloadOnStartup => Settings.Default._reloadOnStartup;
    internal static bool GetAutosave => Settings.Default._autosave;
    internal static decimal GetAutosaveInterval => Settings.Default._autosaveInterval;
    internal static string GetUpdateInterval => Settings.Default.updateInterval;
    internal static DateTime GetLastUpdateCheck => Settings.Default.lastUpdateCheck;
    internal static bool GetUpdateDownload => Settings.Default.updateDownload;
    internal static string GetLastUpdatePath => Settings.Default.lastUpdatePath;
    internal static string GetGitHubPath => Resources.githubPath;
    internal static string GetReleaseApiPath => Resources.releaseApiPath;
    internal static string GetUpdateCheckURL => Resources.updateCheckURL;
    internal static string GetHashCheckURL => Resources.hashCheckURL;
    internal static string GetDefaultColors => Resources.defaultChatColors;

    internal static void InitSettings()
    {
        if (!Settings.Default._Initialized)
        {
            if (!Directory.Exists(LocalPath))
            {
                Settings.Default.localPath = string.Empty;
                Settings.Default.backupPath = string.Empty;
            }
            else
            {
                Settings.Default.localPath = LocalPath;
                Settings.Default.backupPath = BackupPath;
            }

            Settings.Default.backupAvailability = BackupDir;
            Settings.Default.logPath = LogPath;
            Settings.Default.autosavePath = AutosavePath;
            Settings.Default.autosaveAvailability = AutosaveDir;
            Settings.Default._autosaveInterval = 0;
            Settings.Default.updateInterval = nameof(UpdateEnum.OnStartup);
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

    internal static void SaveSettings(SettingsEnum settingName, string value)
    {
        switch (settingName)
        {
            case SettingsEnum.lastUpdatePath:
                Settings.Default.lastUpdatePath = value;
                break;

            case SettingsEnum.locale:
                Settings.Default._locale = value;
                break;

            case SettingsEnum.updateInterval:
                Settings.Default.updateInterval = value;
                break;

            default:
                throw new InvalidOperationException($"{settingName} does not exist!");
        }

        Settings.Default.Save();
    }

    internal static void SaveSettings(SettingsEnum settingName, bool value)
    {
        switch (settingName)
        {
            case SettingsEnum.autosaveAvailability:
                Settings.Default.autosaveAvailability = value;
                break;

            case SettingsEnum.autosave:
                Settings.Default._autosave = value;
                break;

            case SettingsEnum.backupAvailability:
                Settings.Default.backupAvailability = value;
                break;

            case SettingsEnum.reloadOnStartup:
                Settings.Default._reloadOnStartup = value;
                break;

            case SettingsEnum.saveOnClose:
                Settings.Default._saveOnClose = value;
                break;

            case SettingsEnum.settingsUpgradeRequired:
                Settings.Default._upgradeRequired = value;
                break;

            case SettingsEnum.updateDownload:
                Settings.Default.updateDownload = value;
                break;

            default:
                throw new InvalidOperationException($"{settingName} does not exist!");
        }

        Settings.Default.Save();
    }

    internal static void SaveSettings(SettingsEnum settingName, decimal value)
    {
        if (settingName == SettingsEnum.autosaveInterval)
        {
            Settings.Default._autosaveInterval = value;
        }

        Settings.Default.Save();
    }

    internal static void SaveSettings(SettingsEnum settingName, DateTime value)
    {
        if (settingName == SettingsEnum.lastUpdateCheck)
        {
            Settings.Default.lastUpdateCheck = value;
        }

        Settings.Default.Save();
    }

    internal static void RestoreSettings()
    {
        Settings.Default.Reset();

        SaveSettings(SettingsEnum.settingsUpgradeRequired, false);

        if (string.IsNullOrEmpty(GetCurrentLocale))
        {
            SaveSettings(SettingsEnum.locale, CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
        }

        InitSettings();
    }
}
