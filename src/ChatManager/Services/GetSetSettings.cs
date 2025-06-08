using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using ChatManager.Enums;
using ChatManager.Properties;

namespace ChatManager.Services;

internal static class GetSetSettings
{
    private static readonly string LocalPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SWTOR\\swtor\\settings");
    private static readonly string BackupPath = Path.Combine(LocalPath, "Backups");
    private static readonly string LogPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Zagrthos\\SWTOR-ChatManager\\Logs");
    private static readonly string AutosavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Zagrthos\\SWTOR-ChatManager\\Autosave");

    internal static string GetCurrentLocale => Properties.Settings.Default._locale;
    internal static string GetLocalPath => Properties.Settings.Default.localPath;
    internal static string GetBackupPath => Properties.Settings.Default.backupPath;
    internal static bool GetBackupAvailability => Properties.Settings.Default.backupAvailability;
    internal static string GetLogPath => Properties.Settings.Default.logPath;
    internal static string GetAutosavePath => Properties.Settings.Default.autosavePath;
    internal static bool GetAutosaveAvailability => Properties.Settings.Default.autosaveAvailability;
    internal static string GetSupportPath => Resources.supportPath;
    internal static string GetBugPath => Resources.bugPath;
    internal static string GetBugMailpath => Resources.bugMailPath;
    internal static string GetLicences => Resources.Licences;
    internal static bool GetSaveOnClose => Properties.Settings.Default._saveOnClose;
    internal static bool GetReloadOnStartup => Properties.Settings.Default._reloadOnStartup;
    internal static bool GetAutosave => Properties.Settings.Default._autosave;
    internal static decimal GetAutosaveInterval => Properties.Settings.Default._autosaveInterval;
    internal static string GetUpdateInterval => Properties.Settings.Default.updateInterval;
    internal static DateTime GetLastUpdateCheck => Properties.Settings.Default.lastUpdateCheck;
    internal static bool GetUpdateDownload => Properties.Settings.Default.updateDownload;
    internal static string GetLastUpdatePath => Properties.Settings.Default.lastUpdatePath;
    internal static string GetGitHubPath => Resources.githubPath;
    internal static string GetReleaseApiPath => Resources.releaseApiPath;
    internal static string GetUpdateCheckURL => Resources.updateCheckURL;
    internal static string GetHashCheckURL => Resources.hashCheckURL;
    internal static string GetDefaultColors => Resources.defaultChatColors;

    internal static void InitSettings()
    {
        if (!Properties.Settings.Default._Initialized)
        {
            if (!Directory.Exists(LocalPath))
            {
                Properties.Settings.Default.localPath = string.Empty;
                Properties.Settings.Default.backupPath = string.Empty;
            }
            else
            {
                Properties.Settings.Default.localPath = LocalPath;
                Properties.Settings.Default.backupPath = BackupPath;
            }

            Properties.Settings.Default.backupAvailability = false;
            Properties.Settings.Default.logPath = LogPath;
            Properties.Settings.Default.autosavePath = AutosavePath;
            Properties.Settings.Default.autosaveAvailability = false;
            Properties.Settings.Default._autosaveInterval = 0;
            Properties.Settings.Default.updateInterval = nameof(UpdateInterval.OnStartup);
            Properties.Settings.Default._Initialized = true;
            Properties.Settings.Default.Save();
        }

        if (!Properties.Settings.Default._upgradeRequired)
            return;

        Properties.Settings.Default.Upgrade();
        Properties.Settings.Default._upgradeRequired = false;
        Properties.Settings.Default.Save();
    }

    [SuppressMessage("Style", "IDE0010:Add missing cases", Justification = "Missing cases are not needed here.")]
    internal static void SaveSettings(Enums.SettingsNames settingName, string value)
    {
        switch (settingName)
        {
            case Enums.SettingsNames.lastUpdatePath:
                Properties.Settings.Default.lastUpdatePath = value;
                break;

            case Enums.SettingsNames.locale:
                Properties.Settings.Default._locale = value;
                break;

            case Enums.SettingsNames.updateInterval:
                Properties.Settings.Default.updateInterval = value;
                break;

            default:
                throw new InvalidOperationException($"{settingName} does not exist!");
        }

        Properties.Settings.Default.Save();
    }

    [SuppressMessage("Style", "IDE0010:Add missing cases", Justification = "Missing cases are not needed here.")]
    internal static void SaveSettings(Enums.SettingsNames settingName, bool value)
    {
        switch (settingName)
        {
            case Enums.SettingsNames.autosaveAvailability:
                Properties.Settings.Default.autosaveAvailability = value;
                break;

            case Enums.SettingsNames.autosave:
                Properties.Settings.Default._autosave = value;
                break;

            case Enums.SettingsNames.backupAvailability:
                Properties.Settings.Default.backupAvailability = value;
                break;

            case Enums.SettingsNames.reloadOnStartup:
                Properties.Settings.Default._reloadOnStartup = value;
                break;

            case Enums.SettingsNames.saveOnClose:
                Properties.Settings.Default._saveOnClose = value;
                break;

            case Enums.SettingsNames.settingsUpgradeRequired:
                Properties.Settings.Default._upgradeRequired = value;
                break;

            case Enums.SettingsNames.updateDownload:
                Properties.Settings.Default.updateDownload = value;
                break;

            default:
                throw new InvalidOperationException($"{settingName} does not exist!");
        }

        Properties.Settings.Default.Save();
    }

    internal static void SaveSettings(Enums.SettingsNames settingName, decimal value)
    {
        if (settingName == Enums.SettingsNames.autosaveInterval)
            Properties.Settings.Default._autosaveInterval = value;

        Properties.Settings.Default.Save();
    }

    internal static void SaveSettings(Enums.SettingsNames settingName, in DateTime value)
    {
        if (settingName == Enums.SettingsNames.lastUpdateCheck)
            Properties.Settings.Default.lastUpdateCheck = value;

        Properties.Settings.Default.Save();
    }

    internal static void RestoreSettings()
    {
        Properties.Settings.Default.Reset();

        SaveSettings(Enums.SettingsNames.settingsUpgradeRequired, false);

        if (string.IsNullOrWhiteSpace(GetCurrentLocale))
            SaveSettings(Enums.SettingsNames.locale, CultureInfo.CurrentCulture.TwoLetterISOLanguageName);

        InitSettings();
    }
}
