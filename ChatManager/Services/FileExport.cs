using ChatManager.Forms;
using ChatManager.Properties;

namespace ChatManager.Services
{
    internal class FileExport
    {
        public FileExport()
        {
            Logging.Write(LogEvent.Info, ProgramClass.FileExport, "FileExport Constructor created").ConfigureAwait(false);

            if (backupDir != true)
            {
                Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"backupDir = {backupDir}").ConfigureAwait(false);
                BackupDirectory();
            }
        }

        private static bool backupDir = false;
        private static readonly string filePath = FileImport.GetCharPath;
        private static readonly string backupPath = filePath + "\\Backups";

        private static readonly string[] selectedServers = FileSelectorForm.GetSelectedServers;
        private static readonly string[] fileNames = FileSelectorForm.GetListBoxMulti.ToArray();
        
        // Is used for positioning the characters in the array
        private static int arrayCounter = 0;

        public static bool GetBackupAvailable => backupDir;
        public static string GetBackupPath => backupPath;

        private static void BackupDirectory()
        {
            Logging.Write(LogEvent.Method, ProgramClass.FileExport, "BackupDirectory entered").ConfigureAwait(false);

            Logging.Write(LogEvent.Info, ProgramClass.FileExport, "Checking if Backup Dir exists").ConfigureAwait(false);
            if (!Directory.Exists(backupPath))
            {
                Logging.Write(LogEvent.Info, ProgramClass.FileExport, "Backup does not exist, creating it").ConfigureAwait(false);
                Directory.CreateDirectory(backupPath);

                Logging.Write(LogEvent.Info, ProgramClass.FileExport, "Checking again if Backup Dir exists").ConfigureAwait(false);
                if (Directory.Exists(backupPath))
                {
                    Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"Backup Dir created at: {backupPath}").ConfigureAwait(false);
                    backupDir = true;
                    Logging.Write(LogEvent.Method, ProgramClass.FileExport, $"Set backupDir to: {backupDir}").ConfigureAwait(false);
                }
                else
                {
                    Logging.Write(LogEvent.Warning, ProgramClass.FileExport, $"Could not create backup dir!").ConfigureAwait(false);
                }
            }
            else
            {
                Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"Backup Dir exists at: {backupPath}").ConfigureAwait(false);
                backupDir = true;
                Logging.Write(LogEvent.Method, ProgramClass.FileExport, $"Set backupDir to: {backupDir}").ConfigureAwait(false);
            }
        }

        public void WriteContentToFile(/*string content*/)
        {
            Logging.Write(LogEvent.Method, ProgramClass.FileExport, "WriteContentToFile entered").ConfigureAwait(false);

            // Check if backupDir exists and if not show a warning Box
            if (!backupDir)
            {
                ShowMessageBox.Show(Resources.MessageBoxWarn, Resources.Warn_BackupDirMissing);
            }

            // Check if the user selected any characters
            if (fileNames.Length != 0)
            {
                Logging.Write(LogEvent.Info, ProgramClass.FileExport, "fileNames Array selected").ConfigureAwait(false);
                
                // Get the Array from the association
                string[,] name = AssociateFileWithServer();

                // Debug Purposes only
                // Log every entry if it's not null or empty
                for (int i = 0; i < 100; i++)
                {
                    if (!string.IsNullOrEmpty(name[i, 0]) && !string.IsNullOrEmpty(name[i, 1]))
                    {
                        break;
                    }

                    Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"Current name[{i}, 0] is: {name[i, 0]}").ConfigureAwait(false);
                    Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"Current name[{i}, 1] is: {name[i, 1]}").ConfigureAwait(false);
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

                        string newPath = $"{backupPath}\\{fileName}";
                        Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"Current newPath is: {newPath}").ConfigureAwait(false);

                        // Copy only if the dir is present
                        if (backupDir)
                        {
                            File.Copy(path, newPath, true);
                            Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"File {name[i, 0]} copied to: {newPath}").ConfigureAwait(false);
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
                for (int i = 0; i < fileNames.Length-1; i++)
                {
                    // i is used to identify the name of the available characters

                    // If name is not filled stop it
                    if (string.IsNullOrEmpty(name[arrayCounter, 0]))
                    {
                        break;
                    }

                    Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"name[{arrayCounter}, 0] is: {name[arrayCounter, 0]}").ConfigureAwait(false);

                    // Loop through all fileNames
                    for (int j = 0; j < fileNames.Length; j++)
                    {
                        // j is used to identify the selected filename by the user

                        // Get the path
                        string file = fileNames[j];
                        Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"Current file is: {file}").ConfigureAwait(false);

                        // Get the filename
                        string fileName = Path.GetFileName(name[i, 1]);
                        Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"Current fileName is: {fileName}").ConfigureAwait(false);

                        // If file or fileName is null or empty stop it
                        if (string.IsNullOrEmpty(name[i, 0]) && string.IsNullOrEmpty(fileName))
                        {
                            break;
                        }

                        // Check if the name of the character is the same as the one that was selected
                        // and check if the fileName starts with the server prefix
                        if (name[j, 0] == file && fileName.StartsWith(Checks.ServerNameIdentifier(server)))
                        {
                            Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"arrayCounter is: {arrayCounter}").ConfigureAwait(false);

                            // If the entry in the array is not null or empty do it,
                            // insert the data in the array, set the counter one up
                            // and then stop if it was inserted in the array
                            if (!string.IsNullOrEmpty(name[i, 1]))
                            {
                                // the fileName
                                namesWithServers[arrayCounter, 0] = fileNames[arrayCounter];

                                // the filePath
                                namesWithServers[arrayCounter, 1] = name[i, 1];

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
