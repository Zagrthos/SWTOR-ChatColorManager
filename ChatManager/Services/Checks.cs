using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using ChatManager.Enums;
using Windows.Networking.Connectivity;

namespace ChatManager.Services;

internal static class Checks
{
    /// <summary>
    /// Check if the <see langword="string"/> is a Hex Text.
    /// </summary>
    /// <param name="input">The <seealso langword="string"/> to be checked.</param>
    /// <returns><see langword="true"/> if succeeded, <see langword="false"/> if not.</returns>
    internal static bool CheckHexString(string input)
    {
        // Define Hex Regex
        const string hexPattern = "^#?([a-fA-F0-9]{6})$";

        // Check the String
        return Regex.IsMatch(input, hexPattern);
    }

    /// <summary>
    /// Check if SWTOR is running.
    /// </summary>
    /// <returns><see langword="true"/> if yes, <see langword="false"/> if not.</returns>
    internal static bool CheckSWTORprocessFound()
    {
        bool found = false;

        // Check all Processes for SWTOR
        foreach (Process process in Process.GetProcesses())
        {
            // If found set true and stop loop
            if (process.ProcessName == "swtor")
            {
                found = true;
                break;
            }
        }

        return found;
    }

    internal static bool DirectoryCheck(CheckFolderEnum folder)
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.Checks, "DirectoryCheck entered");

        string path = folder switch
        {
            CheckFolderEnum.AutosaveFolder => GetSetSettings.GetAutosavePath,
            CheckFolderEnum.BackupFolder => GetSetSettings.GetBackupPath,
            _ => throw new InvalidOperationException($"{folder} does not exist!"),
        };

        SettingsEnum setting = folder switch
        {
            CheckFolderEnum.AutosaveFolder => SettingsEnum.autosaveAvailability,
            CheckFolderEnum.BackupFolder => SettingsEnum.backupAvailability,
            _ => throw new InvalidOperationException($"{folder} does not exist!"),
        };

        // Check if SWTOR is installed
        bool localPath = false;
        if (!string.IsNullOrEmpty(GetSetSettings.GetLocalPath))
        {
            localPath = true;
        }

        Logging.Write(LogEventEnum.Info, ProgramClassEnum.Checks, $"Checking if {folder} exists");

        if (!Directory.Exists(path) && localPath)
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.Checks, $"{folder} does not exist, creating it");
            Directory.CreateDirectory(path);
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.Checks, $"Checking again if {folder} exists");

            if (Directory.Exists(path))
            {
                Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Checks, $"{folder} created at: {path}");
                GetSetSettings.SaveSettings(setting, true);
                Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Checks, $"Set {setting} to: {true}");
            }
            else
            {
                Logging.Write(LogEventEnum.Error, ProgramClassEnum.Checks, $"Could not create {folder}!");
                ShowMessageBox.ShowBug();
            }
        }
        else if (!localPath)
        {
            Logging.Write(LogEventEnum.Warning, ProgramClassEnum.Checks, $"Could not create {folder} because SWTOR is not installed!");
        }
        else
        {
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Checks, $"{folder} exists at: {path}");
            GetSetSettings.SaveSettings(setting, true);
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Checks, $"Set {setting} to: {true}");
        }

        return folder switch
        {
            CheckFolderEnum.AutosaveFolder => GetSetSettings.GetAutosaveAvailability,
            CheckFolderEnum.BackupFolder => GetSetSettings.GetBackupAvailability,
            _ => throw new InvalidOperationException($"{folder} does not exist!"),
        };
    }

    internal static bool IsBackupDirEmpty() => Directory.GetDirectories(GetSetSettings.GetBackupPath).Length > 0;

    internal static bool CheckForInternetConnection(bool fromUser = false)
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.Checks, "CheckForInternetConnection entered");

        bool isConnected;

        Localization localization = new(GetSetSettings.GetCurrentLocale);

        // Returns null if not connected to anything!
        if (NetworkInformation.GetInternetConnectionProfile() == null)
        {
            isConnected = false;
            Logging.Write(LogEventEnum.Warning, ProgramClassEnum.Checks, "User is not connected to the internet!");

            if (fromUser)
            {
                ShowMessageBox.Show(localization.GetString(LocalizationEnum.MessageBoxWarn), localization.GetString(LocalizationEnum.Warn_NoInternetConnection));
            }

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
            Logging.Write(LogEventEnum.Warning, ProgramClassEnum.Checks, "User is not connected to the internet!");

            if (fromUser)
            {
                ShowMessageBox.Show(localization.GetString(LocalizationEnum.MessageBoxWarn), localization.GetString(LocalizationEnum.Warn_NoInternetConnection));
            }

            return isConnected;
        }

        if (isConnected)
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.Checks, "User is connected to the internet");

            // Check if connection is metered
            if (NetworkInformation.GetInternetConnectionProfile().GetConnectionCost().NetworkCostType != NetworkCostType.Unrestricted)
            {
                Logging.Write(LogEventEnum.Warning, ProgramClassEnum.Checks, "Connection is metered!");
                // If yes ask the user if he wants to continue
                if (!ShowMessageBox.ShowQuestion(localization.GetString(LocalizationEnum.Question_MeteredConnection)))
                {
                    Logging.Write(LogEventEnum.Warning, ProgramClassEnum.Checks, "User does not agree to download over metered connection!");
                    isConnected = false;
                }
                else
                {
                    Logging.Write(LogEventEnum.Info, ProgramClassEnum.Checks, "User agree to download over metered connection");
                    isConnected = true;
                }
            }
            else
            {
                Logging.Write(LogEventEnum.Info, ProgramClassEnum.Checks, "Connection is not metered");
                isConnected = true;
            }
        }

        return isConnected;
    }
}
