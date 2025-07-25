﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using ChatManager.Enums;
using ChatManager.Forms;

namespace ChatManager.Services;

internal static class OpenWindows
{
    internal static string OpenColorPicker(string text, in Color color)
    {
        Logging.Write(LogEvent.Method, LogClass.OpenWindows, "OpenColorPicker entered");

        // Create new Form with the Text of the sender Button
        ColorPickerForm colorPicker = new(text, color);
        Logging.Write(LogEvent.Info, LogClass.OpenWindows, $"Form {colorPicker.Text} created");
        colorPicker.ShowDialog();
        string hexColor = colorPicker.GetHexColor;

        colorPicker.Dispose();

        return hexColor;
    }

    internal static void OpenAbout()
    {
        Logging.Write(LogEvent.Method, LogClass.OpenWindows, "OpenAbout entered");

        AboutForm aboutForm = new();
        aboutForm.ShowDialog();

        aboutForm.Dispose();
    }

    internal static (bool, bool) OpenSettings()
    {
        Logging.Write(LogEvent.Method, LogClass.OpenWindows, "OpenSettings entered");

        SettingsForm settingsForm = new();
        settingsForm.ShowDialog();

        // Check if Language and AutosaveTimer was changed
        if (settingsForm.GetLanguageChanged)
        {
            // Language was changed
            if (settingsForm.GetAutosaveTimerChanged)
            {
                // AND if AutosaveTimer was changed
                settingsForm.Dispose();
                return (true, true);
            }

            // AND if AutosaveTimer was NOT changed
            settingsForm.Dispose();

            return (true, false);
        }

        // If Language was NOT changed
        if (settingsForm.GetAutosaveTimerChanged)
        {
            // AND if AutosaveTimer was changed
            settingsForm.Dispose();
            return (false, true);
        }

        // AND if AutosaveTimer was NOT changed
        settingsForm.Dispose();

        return (false, false);
    }

    internal static (string, string) OpenFileImportSelector()
    {
        Logging.Write(LogEvent.Method, LogClass.OpenWindows, "OpenFileImportSelector entered");

        FileSelectorForm fileSelector = new(FileImport.GetServerList(), false);
        fileSelector.ShowDialog();

        string listBoxString = fileSelector.GetListBoxString;
        string listBoxName = fileSelector.GetListBoxName;

        fileSelector.Dispose();

        return (listBoxString, listBoxName);
    }

    internal static void OpenFileExportSelector(string[] values)
    {
        Logging.Write(LogEvent.Method, LogClass.OpenWindows, "OpenFileExportSelector entered");

        FileSelectorForm fileSelector = new(FileImport.GetServerList(), true);
        DialogResult dialogResult = fileSelector.ShowDialog();

        int fileCount = 0;

        if (dialogResult != DialogResult.Cancel)
        {
            Logging.Write(LogEvent.Variable, LogClass.OpenWindows, $"dialogResult = {dialogResult}");
            FileExport fileExport = new(fileSelector.GetSelectedServers, [.. fileSelector.GetListBoxMulti]);
            fileExport.BackupFilesAndWrite(values);
            fileCount = fileExport.GetNumberOfChangedFiles;
            Logging.Write(LogEvent.Variable, LogClass.OpenWindows, $"fileCount = {fileCount}");
        }
        else
        {
            Logging.Write(LogEvent.Warning, LogClass.OpenWindows, $"dialogResult = {dialogResult}");
        }

        fileSelector.Dispose();

        if (fileCount == 0)
            return;

        Localization localization = new(GetSetSettings.GetCurrentLocale);
        string exportedFilesInfo = localization.GetString(Enums.LocalizationStrings.Inf_ExportedFiles);
        exportedFilesInfo = exportedFilesInfo.Replace("FILECOUNT", fileCount.ToString(CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase);

        ShowMessageBox.Show(localization.GetString(Enums.LocalizationStrings.MessageBoxInfo), exportedFilesInfo);
    }

    internal static void OpenBackupSelector()
    {
        Logging.Write(LogEvent.Method, LogClass.OpenWindows, "OpenBackupSelector entered");

        if (!Checks.IsBackupDirEmpty())
        {
            Localization localization = new(GetSetSettings.GetCurrentLocale);

            ShowMessageBox.Show(localization.GetString(Enums.LocalizationStrings.MessageBoxInfo), localization.GetString(Enums.LocalizationStrings.Inf_NoFilesInBackupDir));
            return;
        }

        BackupSelectorForm backupSelector = new();
        backupSelector.ShowDialog();

        backupSelector.Dispose();
    }

    internal static void OpenTextViewer(bool isChangelog = false)
    {
        Logging.Write(LogEvent.Method, LogClass.OpenWindows, "OpenTextViewer entered");
        Logging.Write(LogEvent.Variable, LogClass.OpenWindows, $"isChangelog = {isChangelog}");

        TextViewerForm textViewer = new(isChangelog);
        textViewer.ShowDialog();

        textViewer.Dispose();
    }

    internal static void OpenExplorer(string path)
    {
        Logging.Write(LogEvent.Info, LogClass.OpenWindows, $"Trying to start explorer.exe with path: {path}");

        try
        {
            Process.Start("explorer.exe", path);
            Logging.Write(LogEvent.Info, LogClass.OpenWindows, $"explorer.exe started with path: {path}");
        }
        catch (Win32Exception ex)
        {
            Logging.Write(LogEvent.Error, LogClass.OpenWindows, "explorer.exe failed to start!");
            Logging.Write(LogEvent.ExMessage, LogClass.OpenWindows, ex.Message);
            ShowMessageBox.ShowBug();
        }
    }

    internal static void OpenLinksInBrowser(string url)
    {
        Logging.Write(LogEvent.Info, LogClass.OpenWindows, $"Trying to start default Browser with url: {url}");

        try
        {
            ProcessStartInfo info = new(url) { UseShellExecute = true };
            Process.Start(info);

            Logging.Write(LogEvent.Info, LogClass.OpenWindows, $"Browser started with url: {url}");
        }
        catch (Win32Exception ex)
        {
            Logging.Write(LogEvent.Error, LogClass.OpenWindows, "Browser failed to start!");
            Logging.Write(LogEvent.ExMessage, LogClass.OpenWindows, ex.Message);
            ShowMessageBox.ShowBug();
        }
    }

    internal static void OpenProcess(string path)
    {
        Logging.Write(LogEvent.Info, LogClass.OpenWindows, $"Trying to start process with path: {path}");

        try
        {
            ProcessStartInfo info = new(path) { UseShellExecute = true };
            Process.Start(info);

            Logging.Write(LogEvent.Info, LogClass.OpenWindows, $"Process started with path: {path}");
        }
        catch (Win32Exception ex)
        {
            Logging.Write(LogEvent.Error, LogClass.OpenWindows, "Process failed to start!");
            Logging.Write(LogEvent.ExMessage, LogClass.OpenWindows, ex.Message);
            ShowMessageBox.ShowBug();
        }
    }
}
