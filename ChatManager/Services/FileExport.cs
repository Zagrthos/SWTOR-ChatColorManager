using ChatManager.Enums;
namespace ChatManager.Services
{
    internal class FileExport
    {
        internal FileExport(string[] servers, string[] files)
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.FileExport, "FileExport Constructor created");
            arrayCounter = 0;
            selectedServers = servers;
            fileNames = files;
        }

        private static readonly bool backupAvailability = GetSetSettings.GetBackupAvailability;
        private static readonly string backupPath = GetSetSettings.GetBackupPath;

        private readonly string[] selectedServers;
        private readonly string[] fileNames;

        // Is used for positioning the characters in the array
        private int arrayCounter;

        internal int GetNumberOfChangedFiles => arrayCounter;

        internal void BackupFilesAndWrite(string[] content)
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.FileExport, "BackupFilesAndWrite entered");

            // Check if backupDir exists and if not show a warning Box
            if (!backupAvailability)
            {
                Localization localization = new(GetSetSettings.GetCurrentLocale);
                ShowMessageBox.Show(localization.GetString(LocalizationEnum.MessageBoxWarn), localization.GetString(LocalizationEnum.Warn_BackupDirMissing));
            }

            // Check if the user selected any characters
            if (fileNames.Length != 0)
            {
                Logging.Write(LogEventEnum.Info, ProgramClassEnum.FileExport, "fileNames Array selected");

                string timestamp = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd_HH-mm-ss");

                string deeperBackup = Path.Combine(backupPath, timestamp);
                Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, $"Create new backup Folder with timestamp: {deeperBackup}");

                Directory.CreateDirectory(deeperBackup);

                if (Directory.Exists(deeperBackup))
                {
                    Logging.Write(LogEventEnum.Info, ProgramClassEnum.FileExport, "Backup Folder created");
                }
                else
                {
                    Logging.Write(LogEventEnum.Warning, ProgramClassEnum.FileExport, "Backup Folder could NOT be created!");
                    ShowMessageBox.ShowBug();
                }

                // Get the Array from the association
                string[,] name = AssociateFileWithServer();

                // Loop through the arrayCounter and Copy all files in the array to the backup position
                for (int i = 0; i < arrayCounter; i++)
                {
                    Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, $"Current i is: {i}");

                    if (!string.IsNullOrEmpty(name[i, 0]) && !string.IsNullOrEmpty(name[i, 1]))
                    {
                        string path = name[i, 1];
                        //Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, $"Current path is: {path}");

                        string fileName = Path.GetFileName(name[i, 1]);
                        //Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, $"Current fileName is: {fileName}");

                        string newPath = $"{deeperBackup}\\{fileName}";
                        //Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, $"Current newPath is: {newPath}");

                        // Copy only if the dir is present
                        if (backupAvailability)
                        {
                            File.Copy(path, newPath, true);
                            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, $"File {name[i, 0]} copied to: {newPath}");

                            string[] lines = File.ReadAllLines(path);

                            int lineNumber = 0;

                            // Search the correct line in the file
                            for (int line = 0; line < lines.Length; line++)
                            {
                                if (lines[line].StartsWith("ChatColors"))
                                {
                                    lineNumber = line;
                                    break;
                                }
                                else
                                {
                                    Logging.Write(LogEventEnum.Warning, ProgramClassEnum.FileExport, "No ChatColors line found!");
                                }
                            }

                            // Split the string to only get the wanted numbers
                            // It assumes it starts with a "ChatColors = "
                            string colorLine = lines[lineNumber].Split("=")[1].TrimStart();

                            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, $"Current ChatColors: {colorLine}");

                            // Split it again to the get colors in an array
                            string[] colorLines = colorLine.Split(";");

                            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, $"colorLines: {colorLines.Length}");

                            // Loop through the changed array and check if there's empty colors
                            // Check if the array is big enough else break
                            // If yes check if the old array has any value that can fill it
                            // If yes fill it with the old value
                            for (int color = 0; color < content.Length; color++)
                            {
                                if (content[color] == "")
                                {
                                    if (color >= colorLines.Length)
                                    {
                                        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, "End of content reached");
                                        break;
                                    }

                                    if (colorLines[color] != "")
                                    {
                                        content[color] = colorLines[color];
                                    }
                                }
                            }

                            // TODO: Calculate the difference how many ; are between the new and the old string array.
                            // Then start to remove the useless ; and only replace the numbers
                            // Maybe implement it in the code above?

                            // Put the array into a string
                            string colorIndexes = string.Join(";", content);

                            // Change the line to the new Array of colors
                            lines[lineNumber] = $"ChatColors = {colorIndexes}";

                            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, $"New ChatColors: {lines[lineNumber]}");

                            // Write it all back
                            File.WriteAllLines(path, lines);

                            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, $"File {name[i, 0]} written back");
                        }
                        else
                        {
                            Logging.Write(LogEventEnum.Warning, ProgramClassEnum.FileExport, $"File {name[i, 0]} NOT copied to: {newPath}!");
                            ShowMessageBox.ShowBug();
                        }
                    }
                    else
                    {
                        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, $"Current i: {i} is null or empty");
                    }
                }
            }
            else
            {
                Logging.Write(LogEventEnum.Warning, ProgramClassEnum.FileExport, "fileNames Array is empty!");
                ShowMessageBox.ShowBug();
            }
        }

        // Create a multidimensional Array that has all files associated with the servers
        private string[,] AssociateFileWithServer()
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.FileExport, "AssociateFileWithServer entered");

            FileImport fileImport = new();

            string[,] namesWithServers = new string[1000, 3];

            // Loop through all servers
            foreach (string server in selectedServers)
            {
                // If server is not filled stop it
                if (string.IsNullOrEmpty(server))
                {
                    break;
                }

                Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, $"Current server is: {server}");

                // Get the names from all characters on this server
                string[,] name = fileImport.GetArray(server);

                // Loop through and check them
                for (int i = 0; i < fileNames.Length; i++)
                {
                    // i is used to identify the name of the available characters

                    // Loop through all fileNames
                    for (int j = 0; j < name.Length / name.Rank; j++)
                    {
                        // j is used to identify the selected filename by the user

                        if (arrayCounter > fileNames.Length || arrayCounter == fileNames.Length)
                        {
                            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, $"arrayCounter is: {arrayCounter}");
                            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, $"fileNames.Length is: {fileNames.Length}");

                            break;
                        }

                        // Get the path
                        string file = fileNames[arrayCounter];
                        //Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, $"Current file is: {file}");

                        // Get the filename
                        string fileName = Path.GetFileName(name[j, 1]);
                        //Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, $"Current fileName is: {fileName}");

                        // If file or fileName is null or empty stop it
                        if (string.IsNullOrEmpty(name[j, 0]) && string.IsNullOrEmpty(fileName))
                        {
                            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, "Current name and fileName is empty!");
                            break;
                        }

                        // If the current file or fileName are equally with the ones in the array, skip this iteration
                        if (namesWithServers[j, 0] == file && namesWithServers[j, 1] == name[j, 1])
                        {
                            continue;
                        }
                        // Check if the name of the character is the same as the one that was selected
                        // and check if the fileName starts with the server prefix
                        if (name[j, 0] == file && fileName.StartsWith(Converter.ServerNameIdentifier(server, true)))
                        {
                            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, $"arrayCounter is: {arrayCounter}");

                            // If the entry in the array is not null or empty do it,
                            // insert the data in the array, set the counter one up
                            // and then stop if it was inserted in the array
                            if (!string.IsNullOrEmpty(name[j, 1]))
                            {
                                // the fileName
                                namesWithServers[arrayCounter, 0] = fileNames[arrayCounter];

                                // the filePath
                                namesWithServers[arrayCounter, 1] = name[j, 1];

                                // the server
                                namesWithServers[arrayCounter, 2] = server;

                                Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, $"{fileNames[arrayCounter]}");

                                arrayCounter++;

                                break;
                            }
                            else
                            {
                                Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, "Already done or null");
                            }
                        }
                    }
                }
            }
            return namesWithServers;
        }
    }
}