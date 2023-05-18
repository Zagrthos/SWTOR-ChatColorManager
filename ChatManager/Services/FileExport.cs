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

        public async void WriteContentToFile(/*string content*/)
        {
            await Logging.Write(LogEvent.Method, ProgramClass.FileExport, "WriteContentToFile entered").ConfigureAwait(false);

            if (fileNames.Length != 0)
            {
                await Logging.Write(LogEvent.Info, ProgramClass.FileExport, "fileNames Array selected").ConfigureAwait(false);
                string[,] name = AssociateFileWithServer();

                for (int i = 0; i < arrayCounter; i++)
                {
                    if (name[i, 0] != null && name[i, 1] != null)
                    {
                        string path = name[i, 1];
                        string fileName = Path.GetFileName(name[i, 1]);
                        string newPath = $"{backupPath}\\{fileName}";
                        File.Copy(path, newPath, true);
                        await Logging.Write(LogEvent.Variable, ProgramClass.FileExport, $"File {name[i, 0]} copied to: {newPath}").ConfigureAwait(false);
                    }
                }
            }
            else
            {
                await Logging.Write(LogEvent.Warning, ProgramClass.FileExport, "fileNames Array is empty").ConfigureAwait(false);
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

                // Create a counter how many files are imported
                int counter = 0;

                // Get the names from all characters on this server
                string[,] name = fileImport.GetArray(server);

                // Loop through and check them
                for (int i = 0; i < name.Length; i++)
                {
                    // If name is not filled and counter is same length as selected chars stop it
                    if (string.IsNullOrEmpty(name[i, 0]) && counter == fileNames.Length)
                    {
                        break;
                    }

                    foreach (string file in fileNames)
                    {
                        if (name[i, 0] == file)
                        {
                            namesWithServers[counter, 0] = fileNames[counter];
                            namesWithServers[counter, 1] = name[counter, 1];
                            namesWithServers[counter, 2] = server;
                            counter++;
                        }
                    }
                }

                // Set the counter so we don't have to loop through every entry
                arrayCounter = counter;
            }

            return namesWithServers;
        }
    }
}
