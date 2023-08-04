using ChatManager.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChatManager.Services
{
    internal class FileImport
    {
        internal FileImport()
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.FileImport, "FileImport Constructor created");
            if (filesChecked != true)
            {
                Logging.Write(LogEventEnum.Info, ProgramClassEnum.FileImport, $"filesChecked = {filesChecked}");
                GetLocalFiles();
            }
        }

        private static bool filesChecked = false;
        private static readonly string filePath = GetSetSettings.GetLocalPath;
        private static readonly List<string> serverList = new();

        private static readonly string[,] starForgeArray = new string[1000, 2];
        private static readonly string[,] sateleShanArray = new string[1000, 2];
        private static readonly string[,] darthMalgusArray = new string[1000, 2];
        private static readonly string[,] tulakHordArray = new string[1000, 2];
        private static readonly string[,] theLeviathanArray = new string[1000, 2];

        // Get Methods to exchange data to other parts of the program
        internal string[,] GetArray(string name)
        {
            return name switch
            {
                "StarForge" => starForgeArray,
                "SateleShan" => sateleShanArray,
                "DarthMalgus" => darthMalgusArray,
                "TulakHord" => tulakHordArray,
                "TheLeviathan" => theLeviathanArray,
                _ => throw new NotImplementedException()
            };
        }

        internal List<string> GetServerList()
        {
            return serverList;
        }

        // Get the local files
        private static void GetLocalFiles()
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.FileImport, $"GetLocalFiles entered");
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileImport, $"filePath: {filePath}");

            // Search the given Path for files
            string[] charFilePaths = Directory.GetFiles(filePath);
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileImport, $"filePaths: {charFilePaths.Length}");

            // Convert all filePaths to fileNames
            string?[] charFileNames = charFilePaths.Select(Path.GetFileName).ToArray();
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileImport, $"files: {charFileNames.Length}");

            byte starForgeCounter = 0;
            byte sateleShanCounter = 0;
            byte darthMalgusCounter = 0;
            byte tulakHordCounter = 0;
            byte theLeviathanCounter = 0;

            // Categorize the files by servers
            if (charFileNames.Length > 0)
            {
                Logging.Write(LogEventEnum.Info, ProgramClassEnum.FileImport, "Starting to count files");

                for (int i = 0; i < charFileNames.Length; i++)
                {
                    //Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileImport, $"currentFile: {i}");
                    string[] fileParts = charFileNames[i]!.Split("_");

                    if (fileParts.Length == 3)
                    {
                        if (fileParts[2] == "PlayerGUIState.ini")
                        {
                            if (fileParts[0] == "he3000")
                            {
                                starForgeArray[starForgeCounter, 0] = fileParts[1];
                                starForgeArray[starForgeCounter, 1] = charFilePaths[i];
                                starForgeCounter++;
                            }
                            else if (fileParts[0] == "he3001")
                            {
                                sateleShanArray[sateleShanCounter, 0] = fileParts[1];
                                sateleShanArray[sateleShanCounter, 1] = charFilePaths[i];
                                sateleShanCounter++;
                            }
                            else if (fileParts[0] == "he4000")
                            {
                                darthMalgusArray[darthMalgusCounter, 0] = fileParts[1];
                                darthMalgusArray[darthMalgusCounter, 1] = charFilePaths[i];
                                darthMalgusCounter++;
                            }
                            else if (fileParts[0] == "he4001")
                            {
                                tulakHordArray[tulakHordCounter, 0] = fileParts[1];
                                tulakHordArray[tulakHordCounter, 1] = charFilePaths[i];
                                tulakHordCounter++;
                            }
                            else if (fileParts[0] == "he4002")
                            {
                                theLeviathanArray[theLeviathanCounter, 0] = fileParts[1];
                                theLeviathanArray[theLeviathanCounter, 1] = charFilePaths[i];
                                theLeviathanCounter++;
                            }
                        }
                    }
                }
            }

            Logging.Write(LogEventEnum.Info, ProgramClassEnum.FileImport, "Select the servers by files");

            // Select the servers by files
            if (starForgeArray[0, 0] != null && starForgeArray[0, 0] != string.Empty)
            {
                serverList.Add("StarForge");
            }
            if (sateleShanArray[0, 0] != null && sateleShanArray[0, 0] != string.Empty)
            {
                serverList.Add("SateleShan");
            }
            if (darthMalgusArray[0, 0] != null && darthMalgusArray[0, 0] != string.Empty)
            {
                serverList.Add("DarthMalgus");
            }
            if (tulakHordArray[0, 0] != null && tulakHordArray[0, 0] != string.Empty)
            {
                serverList.Add("TulakHord");
            }
            if (theLeviathanArray[0, 0] != null && theLeviathanArray[0, 0] != string.Empty)
            {
                serverList.Add("TheLeviathan");
            }

            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileImport, $"serverList.Count = {serverList.Count}");

            // Set the once runner true
            filesChecked = true;
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.FileImport, $"Set filesChecked = {filesChecked}");
        }

        // Get the colors from the given File
        internal string[] GetContentFromFile(string fileName, bool autosaveImport)
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.FileImport, $"GetContentFromFile entered");

            // Initialize Array for saving of colorIndexes
            string[] colorIndex = new string[23];

            // Read every Line in the File and save it to the variable
            string[] fileLines = File.ReadAllLines(fileName);

            string colorLine = string.Empty;

            // If file is NOT imported from Autosave
            if (!autosaveImport)
            {
                // Create a new array by the split and then override the array with another split
                string[] fileNameParts = fileName.Split("\\");
                fileNameParts = fileNameParts[8].Split("_");

                // Set position 0 to the server name and position 1 to the char name
                colorIndex[0] = fileNameParts[0];
                colorIndex[1] = fileNameParts[1];

                // Search for the correct line in the File
                foreach (string line in fileLines)
                {
                    if (line.StartsWith("ChatColors"))
                    {
                        colorLine = line.Substring(line.IndexOf('=') + 1).Trim();
                        break;
                    }
                }

                if (colorLine == string.Empty)
                {
                    Logging.Write(LogEventEnum.Error, ProgramClassEnum.FileImport, "Line ChatColors could not be found!");
                    ShowMessageBox.ShowBug();
                    return Array.Empty<string>();
                }
            }
            else
            {
                // Split the string to get the server name and char name
                string[] colorLines = fileLines[0].Split(";");
                colorIndex[0] = colorLines[0];
                colorIndex[1] = colorLines[1];

                // Rejoin the array to a string without the first two
                colorLine = string.Join(";", colorLines.Skip(2));
            }

            // Create new Array out of the line
            string[] colorLineIndex = colorLine.Split(";");

            // Set each colorIndex position to the correct colorLineIndex position
            colorIndex[2] = colorLineIndex[7]; // Trade
            colorIndex[3] = colorLineIndex[8]; // PvP
            colorIndex[4] = colorLineIndex[6]; // General
            colorIndex[5] = colorLineIndex[2]; // Emote
            colorIndex[6] = colorLineIndex[1]; // Yell
            colorIndex[7] = colorLineIndex[11]; // Officer
            colorIndex[8] = colorLineIndex[10]; // Guild
            colorIndex[9] = colorLineIndex[0]; // Say
            colorIndex[10] = colorLineIndex[3]; // Whisper
            colorIndex[11] = colorLineIndex[12]; // Ops
            colorIndex[12] = colorLineIndex[29]; // Ops Leader
            colorIndex[13] = colorLineIndex[9]; // Group
            colorIndex[14] = colorLineIndex[33]; // Ops Announcement
            colorIndex[15] = colorLineIndex[13]; // Ops Officer
            colorIndex[16] = colorLineIndex[37]; // Combat Information
            colorIndex[17] = colorLineIndex[19]; // Conversation
            colorIndex[18] = colorLineIndex[20]; // Character Login
            colorIndex[19] = colorLineIndex[34]; // Ops Information
            colorIndex[20] = colorLineIndex[18]; // System Feedback
            colorIndex[21] = colorLineIndex[36]; // Guild Information
            colorIndex[22] = colorLineIndex[35]; // Group Information

            // TODO: Decide if logging to be removed or not
            // Debug Purposes
            // Log every Index
            byte b = 0;
            foreach (string index in colorIndex)
            {
                Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileImport, $"colorIndex {b} = {colorIndex[b]}");
                b++;
            }

            return colorIndex;
        }
    }
}