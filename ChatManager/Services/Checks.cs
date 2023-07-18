﻿using ChatManager.Enums;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Windows.Networking.Connectivity;

namespace ChatManager.Services
{
    internal class Checks
    {
        // Check if the String is a Hex Text
        internal static bool CheckHexString(string input)
        {
            // Define Hex Regex
            var hexPattern = "^#?([a-fA-F0-9]{6})$";

            // Check the String
            return Regex.IsMatch(input, hexPattern);
        }

        // Check if SWTOR is running
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
                _ => throw new NotImplementedException(),
            };

            SettingsEnum setting = folder switch
            {
                CheckFolderEnum.AutosaveFolder => SettingsEnum.autosaveAvailability,
                CheckFolderEnum.BackupFolder => SettingsEnum.backupAvailability,
                _ => throw new NotImplementedException(),
            };

            Logging.Write(LogEventEnum.Info, ProgramClassEnum.Checks, $"Checking if {folder} exists");

            if (!Directory.Exists(path))
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
            else
            {
                Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Checks, $"{folder} exists at: {path}");
                GetSetSettings.SaveSettings(setting, true);
                Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Checks, $"Set {setting} to: {true}");
            }

            bool getSettings = folder switch
            {
                CheckFolderEnum.AutosaveFolder => GetSetSettings.GetAutosaveAvailability,
                CheckFolderEnum.BackupFolder => GetSetSettings.GetBackupAvailability,
                _ => throw new NotImplementedException(),
            };

            return getSettings;
        }

        internal static bool IsBackupDirEmpty()
        {
            if (Directory.GetDirectories(GetSetSettings.GetBackupPath).Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static bool CheckForInternetConnection()
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.Checks, "CheckForInternetConnection entered");

            bool isConnected;

            Localization localization = new(GetSetSettings.GetCurrentLocale);

            switch (NetworkInformation.GetInternetConnectionProfile().GetNetworkConnectivityLevel())
            {
                case NetworkConnectivityLevel.None:
                    isConnected = false;
                    Logging.Write(LogEventEnum.Warning, ProgramClassEnum.Checks, "User is not connected to the internet!");
                    ShowMessageBox.Show(localization.GetString(LocalizationEnum.MessageBoxWarn), localization.GetString(LocalizationEnum.Warn_NoInternetConnection));
                    return isConnected;

                case NetworkConnectivityLevel.LocalAccess:
                    isConnected = false;
                    Logging.Write(LogEventEnum.Warning, ProgramClassEnum.Checks, "User is not connected to the internet!");
                    ShowMessageBox.Show(localization.GetString(LocalizationEnum.MessageBoxWarn), localization.GetString(LocalizationEnum.Warn_NoInternetConnection));
                    return isConnected;

                case NetworkConnectivityLevel.ConstrainedInternetAccess:
                    isConnected = false;
                    Logging.Write(LogEventEnum.Warning, ProgramClassEnum.Checks, "User is not connected to the internet!");
                    ShowMessageBox.Show(localization.GetString(LocalizationEnum.MessageBoxWarn), localization.GetString(LocalizationEnum.Warn_NoInternetConnection));
                    return isConnected;

                case NetworkConnectivityLevel.InternetAccess:
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

                    return isConnected;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}