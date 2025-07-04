﻿using System;
using System.Collections.Generic;
using System.IO;
using ChatManager.Enums;

namespace ChatManager.Services;

internal static class FileImport
{
    static FileImport()
    {
        Logging.Write(LogEvent.Info, LogClass.FileImport, "FileImport Constructor created");

        if (FilesChecked)
            return;

        Logging.Write(LogEvent.Info, LogClass.FileImport, $"filesChecked = {FilesChecked}");
        GetLocalFiles();
    }

    private static bool FilesChecked;
    private static readonly string FilePath = GetSetSettings.GetLocalPath;
    private static readonly List<string> ServerList = [];
    private static readonly string[,] StarForgeArray = new string[1000, 2];
    private static readonly string[,] SateleShanArray = new string[1000, 2];
    private static readonly string[,] DarthMalgusArray = new string[1000, 2];
    private static readonly string[,] TulakHordArray = new string[1000, 2];
    private static readonly string[,] TheLeviathanArray = new string[1000, 2];

    internal static string[,] GetArray(string name)
    {
        return name switch
        {
            "StarForge" => StarForgeArray,
            "SateleShan" => SateleShanArray,
            "DarthMalgus" => DarthMalgusArray,
            "TulakHord" => TulakHordArray,
            "TheLeviathan" => TheLeviathanArray,
            _ => throw new InvalidOperationException($"{name} does not exist!")
        };
    }

    internal static List<string> GetServerList() => ServerList;

    private static void GetLocalFiles()
    {
        Logging.Write(LogEvent.Method, LogClass.FileImport, "GetLocalFiles entered");
        Logging.Write(LogEvent.Variable, LogClass.FileImport, $"filePath: {FilePath}");

        // Search the given Path for files
        string[] charFilePaths = Directory.GetFiles(FilePath);
        Logging.Write(LogEvent.Variable, LogClass.FileImport, $"filePaths: {charFilePaths.Length}");

        // Convert all filePaths to fileNames
        string[] charFileNames = new string[charFilePaths.Length];
        for (int i = 0; i < charFilePaths.Length; i++)
        {
            charFileNames[i] = Path.GetFileName(charFilePaths[i]);
        }

        Logging.Write(LogEvent.Variable, LogClass.FileImport, $"files: {charFileNames.Length}");

        int starForgeCounter = 0;
        int sateleShanCounter = 0;
        int darthMalgusCounter = 0;
        int tulakHordCounter = 0;
        int theLeviathanCounter = 0;

        // Categorize the files by servers
        if (charFileNames.Length > 0)
        {
            Logging.Write(LogEvent.Info, LogClass.FileImport, "Starting to count files");

            for (int i = 0; i < charFileNames.Length; i++)
            {
#if DEBUG
                Logging.Write(LogEvent.Variable, LogClass.FileImport, $"currentFile: {i}");
#endif

                string[] fileParts = charFileNames[i]!.Split("_");

                if (fileParts.Length == 3 && fileParts[2] == "PlayerGUIState.ini")
                {
                    if (fileParts[0] == "he3000")
                    {
                        StarForgeArray[starForgeCounter, 0] = fileParts[1];
                        StarForgeArray[starForgeCounter, 1] = charFilePaths[i];
                        starForgeCounter++;
                    }
                    else if (fileParts[0] == "he3001")
                    {
                        SateleShanArray[sateleShanCounter, 0] = fileParts[1];
                        SateleShanArray[sateleShanCounter, 1] = charFilePaths[i];
                        sateleShanCounter++;
                    }
                    else if (fileParts[0] == "he4000")
                    {
                        DarthMalgusArray[darthMalgusCounter, 0] = fileParts[1];
                        DarthMalgusArray[darthMalgusCounter, 1] = charFilePaths[i];
                        darthMalgusCounter++;
                    }
                    else if (fileParts[0] == "he4001")
                    {
                        TulakHordArray[tulakHordCounter, 0] = fileParts[1];
                        TulakHordArray[tulakHordCounter, 1] = charFilePaths[i];
                        tulakHordCounter++;
                    }
                    else if (fileParts[0] == "he4002")
                    {
                        TheLeviathanArray[theLeviathanCounter, 0] = fileParts[1];
                        TheLeviathanArray[theLeviathanCounter, 1] = charFilePaths[i];
                        theLeviathanCounter++;
                    }
                }
            }
        }

        Logging.Write(LogEvent.Info, LogClass.FileImport, "Select the servers by files");

        // Select the servers by files
        if (!string.IsNullOrWhiteSpace(StarForgeArray[0, 0]))
            ServerList.Add("StarForge");

        if (!string.IsNullOrWhiteSpace(SateleShanArray[0, 0]))
            ServerList.Add("SateleShan");

        if (!string.IsNullOrWhiteSpace(DarthMalgusArray[0, 0]))
            ServerList.Add("DarthMalgus");

        if (!string.IsNullOrWhiteSpace(TulakHordArray[0, 0]))
            ServerList.Add("TulakHord");

        if (!string.IsNullOrWhiteSpace(TheLeviathanArray[0, 0]))
            ServerList.Add("TheLeviathan");

        Logging.Write(LogEvent.Variable, LogClass.FileImport, $"serverList.Count = {ServerList.Count}");

        // Set the once runner true
        FilesChecked = true;
        Logging.Write(LogEvent.Info, LogClass.FileImport, $"Set filesChecked = {FilesChecked}");
    }

    /// <summary>
    /// Get the colors from the given File
    /// </summary>
    /// <param name="fileName">The file name as <see langword="string"/>.</param>
    /// <param name="autosaveImport"><see langword="true"/> if it's an autosave import, otherwise <see langword="false"/>.</param>
    /// <returns>The <see langword="string"/> <seealso cref="Array"/> with the content colors.</returns>
    internal static string[] GetContentFromFile(string fileName, bool autosaveImport)
    {
        Logging.Write(LogEvent.Method, LogClass.FileImport, "GetContentFromFile entered");

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
            for (int i = 0; i < fileLines.Length; i++)
            {
                string line = fileLines[i];
                if (line.StartsWith("ChatColors", StringComparison.OrdinalIgnoreCase))
                {
                    int idx = line.IndexOf('=', StringComparison.OrdinalIgnoreCase);
                    if (idx >= 0 && idx + 1 < line.Length)
                        colorLine = line.Substring(idx + 1).Trim();

                    break;
                }
            }

            if (colorLine.Length == 0)
            {
                Logging.Write(LogEvent.Error, LogClass.FileImport, "Line ChatColors could not be found!");
                ShowMessageBox.ShowBug();
                return [];
            }
        }
        else
        {
            // Split the string to get the server name and char name
            string[] colorLines = fileLines[0].Split(";");
            colorIndex[0] = colorLines[0];
            colorIndex[1] = colorLines[1];

            // Rejoin the array to a string without the first two
            string[] remainingColorLines = new string[colorLines.Length - 2];
            Array.Copy(colorLines, 2, remainingColorLines, 0, remainingColorLines.Length);
            colorLine = string.Join(";", remainingColorLines);
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
        for (int i = 0; i < colorIndex.Length; i++)
        {
            Logging.Write(LogEvent.Variable, LogClass.FileImport, $"colorIndex {i} = {colorIndex[i]}");
        }

        return colorIndex;
    }
}
