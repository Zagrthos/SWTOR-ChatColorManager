using ChatManager.Enums;
using System.Diagnostics;
using System.Text.RegularExpressions;

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

            Logging.Write(LogEventEnum.Method, ProgramClassEnum.Checks, "DirectoryCheck entered");
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
                    Logging.Write(LogEventEnum.Warning, ProgramClassEnum.Checks, $"Could not create {folder}!");
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
            if (Directory.GetDirectories(GetSetSettings.GetBackupPath).Length >= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}