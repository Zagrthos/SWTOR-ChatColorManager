using System;
using System.IO;
using ChatManager.Enums;

namespace ChatManager.Services;

internal class FileExport
{
    internal FileExport(string[] servers, string[] files)
    {
        Logging.Write(LogEventEnum.Info, ProgramClassEnum.FileExport, "FileExport Constructor created");
        GetNumberOfChangedFiles = 0;
        SelectedServers = servers;
        FileNames = files;
    }

    private static readonly bool BackupAvailability = GetSetSettings.GetBackupAvailability;
    private static readonly string BackupPath = GetSetSettings.GetBackupPath;
    private readonly string[] SelectedServers;
    private readonly string[] FileNames;

    /// <summary>
    /// Is used for positioning the characters in the array
    /// </summary>
    internal int GetNumberOfChangedFiles { get; private set; }

    internal void BackupFilesAndWrite(string[] content)
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.FileExport, "BackupFilesAndWrite entered");

        // Check if backupDir exists and if not show a warning Box
        if (!BackupAvailability)
        {
            Localization localization = new(GetSetSettings.GetCurrentLocale);
            ShowMessageBox.Show(localization.GetString(LocalizationEnum.MessageBoxWarn), localization.GetString(LocalizationEnum.Warn_BackupDirMissing));
        }

        // Check if the user selected any characters
        if (FileNames.Length != 0)
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.FileExport, "fileNames Array selected");

            string timestamp = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd_HH-mm-ss");

            string deeperBackup = Path.Combine(BackupPath, timestamp);
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, $"Create new backup Folder with timestamp: {deeperBackup}");

            Directory.CreateDirectory(deeperBackup);

            if (Directory.Exists(deeperBackup))
            {
                Logging.Write(LogEventEnum.Info, ProgramClassEnum.FileExport, "Backup Folder created");
            }
            else
            {
                Logging.Write(LogEventEnum.Error, ProgramClassEnum.FileExport, "Backup Folder could NOT be created!");
                ShowMessageBox.ShowBug();
            }

            // Get the Array from the association
            string[,] name = AssociateFileWithServer();

            // Loop through the arrayCounter and Copy all files in the array to the backup position
            for (int i = 0; i < GetNumberOfChangedFiles; i++)
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
                    if (BackupAvailability)
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
                        }

                        if (lineNumber == 0)
                        {
                            Logging.Write(LogEventEnum.Warning, ProgramClassEnum.FileExport, "No ChatColors line found!");
                            GetNumberOfChangedFiles--;
                            ShowMessageBox.ShowBug();
                            return;
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
                            if (content[color].Length == 0)
                            {
                                if (color >= colorLines.Length)
                                {
                                    Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, "End of content reached");
                                    break;
                                }

                                if (colorLines[color] != string.Empty)
                                {
                                    content[color] = colorLines[color];
                                }
                            }
                        }

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

    /// <summary>
    /// Create a multidimensional Array that has all files associated with the servers
    /// </summary>
    /// <returns>The multidimensional <see langword="string"/> <seealso cref="Array"/>.</returns>
    private string[,] AssociateFileWithServer()
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.FileExport, "AssociateFileWithServer entered");

        FileImport fileImport = new();

        string[,] namesWithServers = new string[1000, 3];

        // Loop through all servers
        foreach (string server in SelectedServers)
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
            for (int i = 0; i < FileNames.Length; i++)
            {
                // i is used to identify the name of the available characters

                // Loop through all fileNames
                for (int j = 0; j < name.Length / name.Rank; j++)
                {
                    // j is used to identify the selected filename by the user

                    if (GetNumberOfChangedFiles > FileNames.Length || GetNumberOfChangedFiles == FileNames.Length)
                    {
                        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, $"arrayCounter is: {GetNumberOfChangedFiles}");
                        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, $"fileNames.Length is: {FileNames.Length}");

                        break;
                    }

                    // Get the path
                    string file = FileNames[GetNumberOfChangedFiles];
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
                        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, $"arrayCounter is: {GetNumberOfChangedFiles}");

                        // If the entry in the array is not null or empty do it,
                        // insert the data in the array, set the counter one up
                        // and then stop if it was inserted in the array
                        if (!string.IsNullOrEmpty(name[j, 1]))
                        {
                            // the fileName
                            namesWithServers[GetNumberOfChangedFiles, 0] = FileNames[GetNumberOfChangedFiles];

                            // the filePath
                            namesWithServers[GetNumberOfChangedFiles, 1] = name[j, 1];

                            // the server
                            namesWithServers[GetNumberOfChangedFiles, 2] = server;

                            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileExport, $"{FileNames[GetNumberOfChangedFiles]}");

                            GetNumberOfChangedFiles++;

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
