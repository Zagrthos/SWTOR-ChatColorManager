using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using ChatManager.Enums;
using Windows.Networking.Connectivity;

namespace ChatManager.Services;

internal static partial class Checks
{
    [GeneratedRegex("^#?([a-fA-F0-9]{6})$")]
    private static partial Regex CheckHexRegex();

    /// <summary>
    /// Check if the <see langword="string"/> is a Hex Text.
    /// </summary>
    /// <param name="input">The <seealso langword="string"/> to be checked.</param>
    /// <returns><see langword="true"/> if succeeded, <see langword="false"/> if not.</returns>
    internal static bool CheckHexString(string input) => CheckHexRegex().IsMatch(input);

    /// <summary>
    /// Check if SWTOR is running.
    /// </summary>
    /// <returns><see langword="true"/> if yes, <see langword="false"/> if not.</returns>
    internal static bool CheckSwtorProcessFound
        => Process.GetProcessesByName("swtor").Length is >= 1;

    internal static bool DirectoryCheck(CheckFolder folder)
    {
        Logging.Write(LogEvent.Method, LogClass.Checks, "DirectoryCheck entered");

        string path = folder switch
        {
            CheckFolder.AutosaveFolder => GetSetSettings.GetAutosavePath,
            CheckFolder.BackupFolder => GetSetSettings.GetBackupPath,
            _ => throw new InvalidOperationException($"{folder} does not exist!"),
        };

        SettingsNames setting = folder switch
        {
            CheckFolder.AutosaveFolder => SettingsNames.autosaveAvailability,
            CheckFolder.BackupFolder => SettingsNames.backupAvailability,
            _ => throw new InvalidOperationException($"{folder} does not exist!"),
        };

        // Check if SWTOR is installed
        bool localPath = false;
        if (!string.IsNullOrWhiteSpace(GetSetSettings.GetLocalPath))
            localPath = true;

        Logging.Write(LogEvent.Info, LogClass.Checks, $"Checking if {folder} exists");

        if (!Directory.Exists(path) && localPath)
        {
            Logging.Write(LogEvent.Info, LogClass.Checks, $"{folder} does not exist, creating it");
            Directory.CreateDirectory(path);
            Logging.Write(LogEvent.Info, LogClass.Checks, $"Checking again if {folder} exists");

            if (Directory.Exists(path))
            {
                Logging.Write(LogEvent.Variable, LogClass.Checks, $"{folder} created at: {path}");
                GetSetSettings.SaveSettings(setting, true);
                Logging.Write(LogEvent.Variable, LogClass.Checks, $"Set {setting} to: {true}");
            }
            else
            {
                Logging.Write(LogEvent.Error, LogClass.Checks, $"Could not create {folder}!");
                ShowMessageBox.ShowBug();
            }
        }
        else if (!localPath)
        {
            Logging.Write(LogEvent.Warning, LogClass.Checks, $"Could not create {folder} because SWTOR is not installed!");
        }
        else
        {
            Logging.Write(LogEvent.Variable, LogClass.Checks, $"{folder} exists at: {path}");
            GetSetSettings.SaveSettings(setting, true);
            Logging.Write(LogEvent.Variable, LogClass.Checks, $"Set {setting} to: {true}");
        }

        return folder switch
        {
            CheckFolder.AutosaveFolder => GetSetSettings.GetAutosaveAvailability,
            CheckFolder.BackupFolder => GetSetSettings.GetBackupAvailability,
            _ => throw new InvalidOperationException($"{folder} does not exist!"),
        };
    }

    internal static bool IsBackupDirEmpty() => Directory.GetDirectories(GetSetSettings.GetBackupPath).Length > 0;

    internal static bool CheckForInternetConnection(bool fromUser = false)
    {
        Logging.Write(LogEvent.Method, LogClass.Checks, "CheckForInternetConnection entered");

        bool isConnected;

        Localization localization = new(GetSetSettings.GetCurrentLocale);

        // Returns null if not connected to anything!
        if (NetworkInformation.GetInternetConnectionProfile() is null)
        {
            isConnected = false;
            Logging.Write(LogEvent.Warning, LogClass.Checks, "User is not connected to the internet!");

            if (fromUser)
                ShowMessageBox.Show(localization.GetString(Enums.LocalizationStrings.MessageBoxWarn), localization.GetString(Enums.LocalizationStrings.Warn_NoInternetConnection));

            return isConnected;
        }

        NetworkConnectivityLevel level = NetworkInformation.GetInternetConnectionProfile().GetNetworkConnectivityLevel();
        isConnected = level switch
        {
            NetworkConnectivityLevel.None => false,
            NetworkConnectivityLevel.LocalAccess => false,
            NetworkConnectivityLevel.ConstrainedInternetAccess => false,
            NetworkConnectivityLevel.InternetAccess => true,
            _ => throw new InvalidOperationException($"{level} is not implemented!"),
        };

        if (!isConnected)
        {
            Logging.Write(LogEvent.Warning, LogClass.Checks, "User is not connected to the internet!");

            if (fromUser)
                ShowMessageBox.Show(localization.GetString(Enums.LocalizationStrings.MessageBoxWarn), localization.GetString(Enums.LocalizationStrings.Warn_NoInternetConnection));

            return isConnected;
        }

        if (isConnected)
        {
            Logging.Write(LogEvent.Info, LogClass.Checks, "User is connected to the internet");

            // Check if connection is metered
            if (NetworkInformation.GetInternetConnectionProfile().GetConnectionCost().NetworkCostType != NetworkCostType.Unrestricted)
            {
                Logging.Write(LogEvent.Warning, LogClass.Checks, "Connection is metered!");
                // If yes ask the user if he wants to continue
                if (!ShowMessageBox.ShowQuestion(localization.GetString(Enums.LocalizationStrings.Question_MeteredConnection)))
                {
                    Logging.Write(LogEvent.Warning, LogClass.Checks, "User does not agree to download over metered connection!");
                    isConnected = false;
                }
                else
                {
                    Logging.Write(LogEvent.Info, LogClass.Checks, "User agree to download over metered connection");
                    isConnected = true;
                }
            }
            else
            {
                Logging.Write(LogEvent.Info, LogClass.Checks, "Connection is not metered");
                isConnected = true;
            }
        }

        return isConnected;
    }
}
