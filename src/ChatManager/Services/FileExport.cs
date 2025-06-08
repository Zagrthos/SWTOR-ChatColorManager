using System;
using System.Globalization;
using System.IO;
using ChatManager.Enums;

namespace ChatManager.Services;

internal sealed class FileExport
{
    internal FileExport(string[] servers, string[] files)
    {
        Logging.Write(LogEvent.Info, LogClass.FileExport, "FileExport Constructor created");
        GetNumberOfChangedFiles = 0;
        _selectedServers = servers;
        _fileNames = files;
    }

    private static readonly bool BackupAvailability = GetSetSettings.GetBackupAvailability;
    private static readonly string BackupPath = GetSetSettings.GetBackupPath;
    private readonly string[] _selectedServers;
    private readonly string[] _fileNames;

    /// <summary>
    /// Is used for positioning the characters in the array
    /// </summary>
    internal int GetNumberOfChangedFiles { get; private set; }

    internal void BackupFilesAndWrite(string[] content)
    {
        Logging.Write(LogEvent.Method, LogClass.FileExport, "BackupFilesAndWrite entered");

        // Check if backupDir exists and if not show a warning Box
        if (!BackupAvailability)
        {
            Localization localization = new(GetSetSettings.GetCurrentLocale);
            ShowMessageBox.Show(localization.GetString(Enums.LocalizationStrings.MessageBoxWarn), localization.GetString(Enums.LocalizationStrings.Warn_BackupDirMissing));
        }

        // Check if the user selected any characters
        if (_fileNames.Length != 0)
        {
            Logging.Write(LogEvent.Info, LogClass.FileExport, "fileNames Array selected");

            string timestamp = DateTimeOffset.Now.ToString("yyyy-MM-dd_HH-mm-ss", CultureInfo.InvariantCulture);

            string deeperBackup = Path.Combine(BackupPath, timestamp);
            Logging.Write(LogEvent.Variable, LogClass.FileExport, $"Create new backup Folder with timestamp: {deeperBackup}");

            Directory.CreateDirectory(deeperBackup);

            if (Directory.Exists(deeperBackup))
            {
                Logging.Write(LogEvent.Info, LogClass.FileExport, "Backup Folder created");
            }
            else
            {
                Logging.Write(LogEvent.Error, LogClass.FileExport, "Backup Folder could NOT be created!");
                ShowMessageBox.ShowBug();
            }

            // Get the Array from the association
            string[,] name = AssociateFileWithServer();

            // Loop through the arrayCounter and Copy all files in the array to the backup position
            for (int i = 0; i < GetNumberOfChangedFiles; i++)
            {
                Logging.Write(LogEvent.Variable, LogClass.FileExport, $"Current i is: {i}");

                if (!string.IsNullOrWhiteSpace(name[i, 0]) && !string.IsNullOrWhiteSpace(name[i, 1]))
                {
                    string path = name[i, 1];
#if DEBUG
                    Logging.Write(LogEvent.Variable, LogClass.FileExport, $"Current path is: {path}");
#endif

                    string fileName = Path.GetFileName(name[i, 1]);
#if DEBUG
                    Logging.Write(LogEvent.Variable, LogClass.FileExport, $"Current fileName is: {fileName}");
#endif

                    string newPath = $"{deeperBackup}\\{fileName}";
#if DEBUG
                    Logging.Write(LogEvent.Variable, LogClass.FileExport, $"Current newPath is: {newPath}");
#endif

                    // Copy only if the dir is present
                    if (BackupAvailability)
                    {
                        File.Copy(path, newPath, true);
                        Logging.Write(LogEvent.Variable, LogClass.FileExport, $"File {name[i, 0]} copied to: {newPath}");

                        string[] lines = File.ReadAllLines(path);

                        int lineNumber = 0;

                        // Search the correct line in the file
                        for (int line = 0; line < lines.Length; line++)
                        {
                            if (lines[line].StartsWith("ChatColors", StringComparison.OrdinalIgnoreCase))
                            {
                                lineNumber = line;
                                break;
                            }
                        }

                        if (lineNumber == 0)
                        {
                            Logging.Write(LogEvent.Warning, LogClass.FileExport, "No ChatColors line found!");
                            GetNumberOfChangedFiles--;
                            ShowMessageBox.ShowBug();
                            return;
                        }

                        // Split the string to only get the wanted numbers
                        // It assumes it starts with a "ChatColors = "
                        string colorLine = lines[lineNumber].Split("=")[1].TrimStart();

                        Logging.Write(LogEvent.Variable, LogClass.FileExport, $"Current ChatColors: {colorLine}");

                        // Split it again to the get colors in an array
                        string[] colorLines = colorLine.Split(";");

                        Logging.Write(LogEvent.Variable, LogClass.FileExport, $"colorLines: {colorLines.Length}");

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
                                    Logging.Write(LogEvent.Variable, LogClass.FileExport, "End of content reached");
                                    break;
                                }

                                if (!string.IsNullOrWhiteSpace(colorLines[color]))
                                    content[color] = colorLines[color];
                            }
                        }

                        // Put the array into a string
                        string colorIndexes = string.Join(";", content);

                        // Change the line to the new Array of colors
                        lines[lineNumber] = $"ChatColors = {colorIndexes}";

                        Logging.Write(LogEvent.Variable, LogClass.FileExport, $"New ChatColors: {lines[lineNumber]}");

                        // Write it all back
                        File.WriteAllLines(path, lines);

                        Logging.Write(LogEvent.Variable, LogClass.FileExport, $"File {name[i, 0]} written back");
                    }
                    else
                    {
                        Logging.Write(LogEvent.Warning, LogClass.FileExport, $"File {name[i, 0]} NOT copied to: {newPath}!");
                        ShowMessageBox.ShowBug();
                    }
                }
                else
                {
                    Logging.Write(LogEvent.Variable, LogClass.FileExport, $"Current i: {i} is null or empty");
                }
            }
        }
        else
        {
            Logging.Write(LogEvent.Warning, LogClass.FileExport, "fileNames Array is empty!");
            ShowMessageBox.ShowBug();
        }
    }

    /// <summary>
    /// Create a multidimensional Array that has all files associated with the servers
    /// </summary>
    /// <returns>The multidimensional <see langword="string"/> <seealso cref="Array"/>.</returns>
    private string[,] AssociateFileWithServer()
    {
        Logging.Write(LogEvent.Method, LogClass.FileExport, "AssociateFileWithServer entered");

        string[,] namesWithServers = new string[1000, 3];

        // Loop through all servers
        foreach (string server in _selectedServers)
        {
            // If server is not filled stop it
            if (string.IsNullOrWhiteSpace(server))
                break;

            Logging.Write(LogEvent.Variable, LogClass.FileExport, $"Current server is: {server}");

            // Get the names from all characters on this server
            string[,] name = FileImport.GetArray(server);

            // Loop through and check them
            for (int i = 0; i < _fileNames.Length; i++)
            {
                // i is used to identify the name of the available characters

                // Loop through all fileNames
                for (int j = 0; j < name.Length / name.Rank; j++)
                {
                    // j is used to identify the selected filename by the user

                    if (GetNumberOfChangedFiles > _fileNames.Length || GetNumberOfChangedFiles == _fileNames.Length)
                    {
                        Logging.Write(LogEvent.Variable, LogClass.FileExport, $"arrayCounter is: {GetNumberOfChangedFiles}");
                        Logging.Write(LogEvent.Variable, LogClass.FileExport, $"fileNames.Length is: {_fileNames.Length}");

                        break;
                    }

                    // Get the path
                    string file = _fileNames[GetNumberOfChangedFiles];
#if DEBUG
                    Logging.Write(LogEvent.Variable, LogClass.FileExport, $"Current file is: {file}");
#endif

                    // Get the filename
                    string fileName = Path.GetFileName(name[j, 1]);
#if DEBUG
                    Logging.Write(LogEvent.Variable, LogClass.FileExport, $"Current fileName is: {fileName}");
#endif

                    // If file or fileName is null or empty stop it
                    if (string.IsNullOrWhiteSpace(name[j, 0]) && string.IsNullOrWhiteSpace(fileName))
                    {
                        Logging.Write(LogEvent.Variable, LogClass.FileExport, "Current name and fileName is empty!");
                        break;
                    }

                    // If the current file or fileName are equally with the ones in the array, skip this iteration
                    if (namesWithServers[j, 0] == file && namesWithServers[j, 1] == name[j, 1])
                        continue;

                    // Check if the name of the character is the same as the one that was selected
                    // and check if the fileName starts with the server prefix
                    if (name[j, 0] == file && fileName.StartsWith(Converter.ServerNameIdentifier(server, true), StringComparison.OrdinalIgnoreCase))
                    {
                        Logging.Write(LogEvent.Variable, LogClass.FileExport, $"arrayCounter is: {GetNumberOfChangedFiles}");

                        // If the entry in the array is not null or empty do it,
                        // insert the data in the array, set the counter one up
                        // and then stop if it was inserted in the array
                        if (!string.IsNullOrWhiteSpace(name[j, 1]))
                        {
                            // the fileName
                            namesWithServers[GetNumberOfChangedFiles, 0] = _fileNames[GetNumberOfChangedFiles];

                            // the filePath
                            namesWithServers[GetNumberOfChangedFiles, 1] = name[j, 1];

                            // the server
                            namesWithServers[GetNumberOfChangedFiles, 2] = server;

                            Logging.Write(LogEvent.Variable, LogClass.FileExport, $"{_fileNames[GetNumberOfChangedFiles]}");

                            GetNumberOfChangedFiles++;

                            break;
                        }

                        Logging.Write(LogEvent.Variable, LogClass.FileExport, "Already done or null");
                    }
                }
            }
        }

        return namesWithServers;
    }
}
