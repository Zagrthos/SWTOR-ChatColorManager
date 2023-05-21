namespace ChatManager.Services
{
    internal class FileImport
    {
        public FileImport()
        {
            Logging.Write(LogEvent.Info, ProgramClass.FileImport, "FileImport Constructor created").ConfigureAwait(false);
            if (filesChecked != true)
            {
                Logging.Write(LogEvent.Info, ProgramClass.FileImport, $"filesChecked = {filesChecked}").ConfigureAwait(false);
                GetLocalFiles();
            }
        }

        private static bool filesChecked = false;
        private static readonly string filePath = GetSetSettings.GetLocalPath;
        private static readonly List<string> serverList = new();

        private static readonly string[,] starForgeArray = new string[100, 2];
        private static readonly string[,] sateleShanArray = new string[100, 2];
        private static readonly string[,] darthMalgusArray = new string[100, 2];
        private static readonly string[,] tulakHordArray = new string[100, 2];
        private static readonly string[,] theLeviathanArray = new string[100, 2];

        // Get Methods to exchange data to other parts of the program
        public string[,] GetArray(string name)
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

        public List<string> GetServerList()
        {
            return serverList;
        }

        // Get the local files
        private static void GetLocalFiles()
        {
            Logging.Write(LogEvent.Method, ProgramClass.FileImport, $"GetLocalFiles entered").ConfigureAwait(false);
            Logging.Write(LogEvent.Variable, ProgramClass.FileImport, $"filePath: {filePath}").ConfigureAwait(false);

            // Search the given Path for files
            string[] charFilePaths = Directory.GetFiles(filePath);
            Logging.Write(LogEvent.Variable, ProgramClass.FileImport, $"filePaths: {charFilePaths.Length}").ConfigureAwait(false);

            // Convert all filePaths to fileNames
            string?[] charFileNames = charFilePaths.Select(Path.GetFileName).ToArray();
            Logging.Write(LogEvent.Variable, ProgramClass.FileImport, $"files: {charFileNames.Length}").ConfigureAwait(false);

            byte starForgeCounter = 0;
            byte sateleShanCounter = 0;
            byte darthMalgusCounter = 0;
            byte tulakHordCounter = 0;
            byte theLeviathanCounter = 0;

            // Categorize the files by servers
            if (charFileNames.Length > 0)
            {
                // TODO: Decide if logging to be removed or not
                for (int i = 0; i < charFileNames.Length; i++)
                {
                    //await Logging.Write(LogEvent.Variable, ProgramClass.FileImport, $"currentFile: {charFileNames[i]}").ConfigureAwait(false);
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
                                //await Logging.Write(LogEvent.Variable, ProgramClass.FileImport, $"darthMalgusArray: {darthMalgusArray[darthMalgusCounter, 0]}").ConfigureAwait(false);
                                darthMalgusArray[darthMalgusCounter, 1] = charFilePaths[i];
                                //await Logging.Write(LogEvent.Variable, ProgramClass.FileImport, $"darthMalgusArray: {darthMalgusArray[darthMalgusCounter, 1]}").ConfigureAwait(false);
                                darthMalgusCounter++;
                                //await Logging.Write(LogEvent.Variable, ProgramClass.FileImport, $"darthMalgusCounter: {darthMalgusCounter}").ConfigureAwait(false);
                            }
                            else if (fileParts[0] == "he4001")
                            {
                                tulakHordArray[tulakHordCounter, 0] = fileParts[1];
                                //await Logging.Write(LogEvent.Variable, ProgramClass.FileImport, $"tulakHordArray: {tulakHordArray[tulakHordCounter, 0]}").ConfigureAwait(false);
                                tulakHordArray[tulakHordCounter, 1] = charFilePaths[i];
                                //await Logging.Write(LogEvent.Variable, ProgramClass.FileImport, $"tulakHordArray: {tulakHordArray[tulakHordCounter, 1]}").ConfigureAwait(false);
                                tulakHordCounter++;
                                //await Logging.Write(LogEvent.Variable, ProgramClass.FileImport, $"tulakHordCounter: {tulakHordCounter}").ConfigureAwait(false);
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

            Logging.Write(LogEvent.Info, ProgramClass.FileImport, "Select the servers by files").ConfigureAwait(false);

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

            Logging.Write(LogEvent.Variable, ProgramClass.FileImport, $"serverList.Count = {serverList.Count}").ConfigureAwait(false);

            // Set the once runner true
            filesChecked = true;
            Logging.Write(LogEvent.Info, ProgramClass.FileImport, $"Set filesChecked = {filesChecked}").ConfigureAwait(false);
        }

        // Get the colors from the given File
        public async Task<string[]> GetContentFromFile(string fileName)
        {
            await Logging.Write(LogEvent.Method, ProgramClass.FileImport, $"GetContentFromFile entered");

            // Initialize Array for saving of colorIndexes
            string[] colorIndex = new string[22];

            // Set position 21 to the original filepath
            colorIndex[0] = fileName;

            // Read every Line in the File and save it to the variable
            string[] fileLines = await File.ReadAllLinesAsync(colorIndex[0]);

            // Initialize new Array and search for the correct line in the File
            string colorLine = String.Empty;
            foreach (string line in fileLines)
            {
                if (line.StartsWith("ChatColors"))
                {
                    colorLine = line.Substring(line.IndexOf('=') + 1).Trim();
                    break;
                }
            }

            // Create new Array out of the line
            string[] colorLineIndex = colorLine.Split(";");

            // Set each colorIndex position to the correct colorLineIndex position
            colorIndex[1] = colorLineIndex[7]; // Trade
            colorIndex[2] = colorLineIndex[8]; // PvP
            colorIndex[3] = colorLineIndex[6]; // General
            colorIndex[4] = colorLineIndex[2]; // Emote
            colorIndex[5] = colorLineIndex[1]; // Yell
            colorIndex[6] = colorLineIndex[11]; // Officer
            colorIndex[7] = colorLineIndex[10]; // Guild
            colorIndex[8] = colorLineIndex[0]; // Say
            colorIndex[9] = colorLineIndex[3]; // Whisper
            colorIndex[10] = colorLineIndex[12]; // Ops
            colorIndex[11] = colorLineIndex[29]; // Ops Leader
            colorIndex[12] = colorLineIndex[9]; // Group
            colorIndex[13] = colorLineIndex[32]; // Ops Announcement
            colorIndex[14] = colorLineIndex[13]; // Ops Officer
            colorIndex[15] = colorLineIndex[36]; // Combat Information
            colorIndex[16] = colorLineIndex[19]; // Conversation
            colorIndex[17] = colorLineIndex[20]; // Character Login
            colorIndex[18] = colorLineIndex[33]; // Ops Information
            colorIndex[19] = colorLineIndex[18]; // System Feedback
            colorIndex[20] = colorLineIndex[35]; // Guild Information
            colorIndex[21] = colorLineIndex[34]; // Group Information

            // TODO: Decide if logging to be removed or not
            // Debug Purposes
            // Log every Index
            byte b = 0;
            foreach (string index in colorIndex)
            {
                await Logging.Write(LogEvent.Variable, ProgramClass.FileImport, $"colorIndex {b} = {colorIndex[b]}");
                b++;
            }

            return colorIndex;
        } 
    }
}