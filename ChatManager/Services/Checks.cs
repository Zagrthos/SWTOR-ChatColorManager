using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ChatManager.Services
{
    internal class Checks
    {
        // Check if the String is a Hex Text
        public static bool CheckHexString(string input)
        {
            // Define Hex Regex
            var hexPattern = "^#?([a-fA-F0-9]{6})$";

            // Check the String
            return Regex.IsMatch(input, hexPattern);
        }

        // Check if SWTOR is running
        public static bool CheckSWTORprocessFound()
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

        // Associate server name to identifier
        public static string ServerNameIdentifier(string name, bool isServerName)
        {
            if (!string.IsNullOrEmpty(name) && isServerName)
            {
                return name switch
                {
                    "StarForge" => "he3000",
                    "SateleShan" => "he3001",
                    "DarthMalgus" => "he4000",
                    "TulakHord" => "he4001",
                    "TheLeviathan" => "he4002",
                    _ => string.Empty,
                };
            }
            else if (!string.IsNullOrEmpty(name) && !isServerName)
            {
                return name switch
                {
                    "he3000" => "StarForge",
                    "he3001" => "SateleShan",
                    "he4000" => "DarthMalgus",
                    "he4001" => "TulakHord",
                    "he4002" => "TheLeviathan",
                    _ => string.Empty,
                };
            }
            else
            {
                return string.Empty;
            }
        }

        public static bool BackupDirectory()
        {
            Logging.Write(LogEvent.Method, ProgramClass.Checks, "BackupDirectory entered");

            string backupPath = GetSetSettings.GetBackupPath;

            Logging.Write(LogEvent.Info, ProgramClass.Checks, "Checking if Backup Dir exists");
            if (!Directory.Exists(backupPath))
            {
                Logging.Write(LogEvent.Info, ProgramClass.Checks, "Backup does not exist, creating it");
                Directory.CreateDirectory(backupPath);

                Logging.Write(LogEvent.Info, ProgramClass.Checks, "Checking again if Backup Dir exists");
                if (Directory.Exists(backupPath))
                {
                    Logging.Write(LogEvent.Variable, ProgramClass.Checks, $"Backup Dir created at: {backupPath}");
                    GetSetSettings.SaveSettings(Setting.backupAvailability, true);
                    Logging.Write(LogEvent.Method, ProgramClass.Checks, $"Set backupDir to: {true}");
                }
                else
                {
                    Logging.Write(LogEvent.Warning, ProgramClass.Checks, $"Could not create backup dir!");
                    ShowMessageBox.ShowBug();
                }
            }
            else
            {
                Logging.Write(LogEvent.Variable, ProgramClass.Checks, $"Backup Dir exists at: {backupPath}");
                GetSetSettings.SaveSettings(Setting.backupAvailability, true);
                Logging.Write(LogEvent.Method, ProgramClass.Checks, $"Set backupDir to: {true}");
            }

            return GetSetSettings.GetBackupAvailability;
        }
    }
}
