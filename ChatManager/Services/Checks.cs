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

        // Check if Path exists
        public static bool CheckIfPathExists(string path)
        {
            if (Directory.Exists(path))
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
