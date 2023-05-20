using ChatManager.Forms;
using ChatManager.Properties;

namespace ChatManager.Services
{
    internal class FileExport
    {
        public FileExport()
        {
            Logging.Write(LogEvent.Info, ProgramClass.FileExport, "FileExport Constructor created").ConfigureAwait(false);
        }

        private static readonly bool backupAvailability = GetSetSettings.GetBackupAvailability;
        private static readonly string backupPath = GetSetSettings.GetBackupPath;

        private static readonly string[] selectedServers = FileSelectorForm.GetSelectedServers;
        private static readonly string[] fileNames = FileSelectorForm.GetListBoxMulti.ToArray();
        
        // Is used for positioning the characters in the array
        private static int arrayCounter = 0;

        public void BackupFilesAndWrite(string content)
        {
            Logging.Write(LogEvent.Method, ProgramClass.FileExport, "BackupFilesAndWrite entered").ConfigureAwait(false);

            // Check if backupDir exists and if not show a warning Box
            if (!backupAvailability)
            {
                ShowMessageBox.Show(Resources.MessageBoxWarn, Resources.Warn_BackupDirMissing);
            }

            // Check if the user selected any characters
            if (fileNames.Length != 0)
            {
                Logging.Write(LogEvent.Info, ProgramClass.FileExport, "fileNames Array selected").ConfigureAwait(false);

                string timestamp = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd_HH-mm-ss");

                string deeperBackup = Path.Combine(backupPath, timestamp);
                Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"Create new backup Folder with timestamp: {deeperBackup}").ConfigureAwait(false);

                Directory.CreateDirectory(deeperBackup);

                if (Directory.Exists(deeperBackup))
                {
                    Logging.Write(LogEvent.Info, ProgramClass.FileExport, "Backup Folder created").ConfigureAwait(false);
                }
                else
                {
                    Logging.Write(LogEvent.Warning, ProgramClass.FileExport, "Backup Folder could NOT be created!").ConfigureAwait(false);
                }

                // Get the Array from the association
                string[,] name = AssociateFileWithServer();

                // Debug Purposes only
                // Log every entry if it's not null or empty
                for (int i = 0; i < 100; i++)
                {
                    if (!string.IsNullOrEmpty(name[i, 0]) && !string.IsNullOrEmpty(name[i, 1]))
                    {
                        Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"Current name[{i}, 0] is: {name[i, 0]}").ConfigureAwait(false);
                        Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"Current name[{i}, 1] is: {name[i, 1]}").ConfigureAwait(false);
                    }
                    else
                    {
                        break;
                    }
                }

                // Loop through the arrayCounter and Copy all files in the array to the backup position
                for (int i = 0; i < arrayCounter; i++)
                {
                    Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"Current i is: {i}").ConfigureAwait(false);

                    if (!string.IsNullOrEmpty(name[i, 0]) && !string.IsNullOrEmpty(name[i, 1]))
                    {
                        string path = name[i, 1];
                        Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"Current path is: {path}").ConfigureAwait(false);

                        string fileName = Path.GetFileName(name[i, 1]);
                        Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"Current fileName is: {fileName}").ConfigureAwait(false);

                        string newPath = $"{deeperBackup}\\{fileName}";
                        Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"Current newPath is: {newPath}").ConfigureAwait(false);

                        // Copy only if the dir is present
                        if (backupAvailability)
                        {
                            File.Copy(path, newPath, true);
                            Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"File {name[i, 0]} copied to: {newPath}").ConfigureAwait(false);

                            string[] lines = File.ReadAllLines(path);

                            Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"Current ChatColors: {lines[1]}").ConfigureAwait(false);

                            string searchLine = "ChatColors = ";
                            int lineNumber = 0;
                            
                            for (int line  = 0; line < lines.Length; line++)
                            {
                                if (lines[line].StartsWith(searchLine))
                                {
                                    lineNumber = line;
                                    break;
                                }
                            }

                            lines[lineNumber] = $"ChatColors = {content}";

                            Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"New ChatColors: {lines[1]}").ConfigureAwait(false);

                            File.WriteAllLines(path, lines);

                            Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"File {name[i, 0]} written back").ConfigureAwait(false);
                        }
                        else
                        {
                            Logging.Write(LogEvent.Warning, ProgramClass.FileExport, $"File {name[i, 0]} NOT copied to: {newPath}!").ConfigureAwait(false);
                        }
                    }
                    else
                    {
                        Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"Current i: {i} is null or empty").ConfigureAwait(false);
                    }
                }
            }
            else
            {
                Logging.Write(LogEvent.Warning, ProgramClass.FileExport, "fileNames Array is empty!").ConfigureAwait(false);
            }
        }

        // Create a multidimensional Array that has all files associated with the servers
        private static string[,] AssociateFileWithServer()
        {
            Logging.Write(LogEvent.Method, ProgramClass.FileExport, "AssociateFileWithServer entered").ConfigureAwait(false);

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

                Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"Current server is: {server}").ConfigureAwait(false);

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
                            Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"arrayCounter is: {arrayCounter}").ConfigureAwait(false);
                            Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"fileNames.Length is: {fileNames.Length}").ConfigureAwait(false);

                            break;
                        }

                        // Get the path
                        string file = fileNames[arrayCounter];
                        Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"Current file is: {file}").ConfigureAwait(false);

                        // Get the filename
                        string fileName = Path.GetFileName(name[j, 1]);
                        Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"Current fileName is: {fileName}").ConfigureAwait(false);

                        // If file or fileName is null or empty stop it
                        if (string.IsNullOrEmpty(name[j, 0]) && string.IsNullOrEmpty(fileName))
                        {
                            Logging.Write(LogEvent.Variable, ProgramClass.FileExport, "Current name and fileName is empty!").ConfigureAwait(false);
                            break;
                        }

                        // If the current file or fileName are equally with the ones in the array, skip this iteration
                        if (namesWithServers[j, 0] == file && namesWithServers[j, 1] == name[j, 1])
                        {
                            continue;
                        }
                        // Check if the name of the character is the same as the one that was selected
                        // and check if the fileName starts with the server prefix
                        if (name[j, 0] == file && fileName.StartsWith(Checks.ServerNameIdentifier(server)))
                        {
                            Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"arrayCounter is: {arrayCounter}").ConfigureAwait(false);

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

                                Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"{fileNames[arrayCounter]}").ConfigureAwait(false);

                                arrayCounter++;

                                break;
                            }
                            else
                            {
                                Logging.Write(LogEvent.Variable, ProgramClass.FileExport, "Already done or null").ConfigureAwait(false);
                            }
                        }
                    }
                }
            }
            return namesWithServers;
        }
    }
}
