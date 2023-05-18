using ChatManager.Forms;

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
        private static int arrayCounter = 0;

        private static void BackupDirectory()
        {
            Logging.Write(LogEvent.Method, ProgramClass.FileExport, "BackupDirectory entered").ConfigureAwait(false);

            Logging.Write(LogEvent.Info, ProgramClass.FileExport, "Checking if Backup Dir exists").ConfigureAwait(false);
            if (!Directory.Exists(backupPath))
            {
                Logging.Write(LogEvent.Info, ProgramClass.FileExport, "Backup does not exist, creating it").ConfigureAwait(false);
                Directory.CreateDirectory(backupPath);

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

            if (fileNames.Length != 0)
            {
                Logging.Write(LogEvent.Info, ProgramClass.FileExport, "fileNames Array selected").ConfigureAwait(false);
                string[,] name = AssociateFileWithServer();

                for (int i = 0; i < 100; i++)
                {
                    if (!string.IsNullOrEmpty(name[i, 0]) && !string.IsNullOrEmpty(name[i, 1]))
                    {
                        break;
                    }

                    Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"Current name[{i}, 0] is: {name[i, 0]}").ConfigureAwait(false);
                    Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"Current name[{i}, 1] is: {name[i, 1]}").ConfigureAwait(false);
                }

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

                        File.Copy(path, newPath, true);
                        Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"File {name[i, 0]} copied to: {newPath}").ConfigureAwait(false);
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
                    // If name is not filled and counter is same length as selected chars stop it
                    if (string.IsNullOrEmpty(name[arrayCounter, 0]))
                    {
                        break;
                    }

                    Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"name[{arrayCounter}, 0] is: {name[arrayCounter, 0]}").ConfigureAwait(false);

                    // Compare the current fileName with the current Name of the Array
                    for (int j = 0; j < fileNames.Length; j++)
                    {
                        string file = fileNames[j];
                        Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"Current file is: {file}").ConfigureAwait(false);

                        string fileName = Path.GetFileName(name[i, 1]);
                        Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"Current fileName is: {fileName}").ConfigureAwait(false);

                        if (string.IsNullOrEmpty(name[i, 0]) && string.IsNullOrEmpty(fileName))
                        {
                            break;
                        }

                        if (name[j, 0] == file && fileName.StartsWith(Checks.ServerNameIdentifier(server)))
                        {
                            Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"arrayCounter is: {arrayCounter}").ConfigureAwait(false);

                            if (!string.IsNullOrEmpty(name[i, 1]))
                            {
                                namesWithServers[arrayCounter, 0] = fileNames[arrayCounter];
                                namesWithServers[arrayCounter, 1] = name[i, 1];
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
