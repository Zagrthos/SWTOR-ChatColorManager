using System;
using System.Diagnostics;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;
using ChatManager.Enums;
using ChatManager.Forms;

namespace ChatManager.Services;

internal static class OpenWindows
{
    internal static string OpenColorPicker(string text, Color color)
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.OpenWindows, "OpenColorPicker entered");

        // Create new Form with the Text of the sender Button
        ColorPickerForm colorPicker = new(text, color);
        Logging.Write(LogEventEnum.Info, ProgramClassEnum.OpenWindows, $"Form {colorPicker.Text} created");
        colorPicker.ShowDialog();
        string hexColor = colorPicker.GetHexColor;

        colorPicker.Dispose();

        return hexColor;
    }

    internal static void OpenAbout()
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.OpenWindows, "OpenAbout entered");

        AboutForm aboutForm = new();
        aboutForm.ShowDialog();

        aboutForm.Dispose();
    }

    internal static (bool, bool) OpenSettings()
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.OpenWindows, "OpenSettings entered");

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
            else
            {
                // AND if AutosaveTimer was NOT changed
                settingsForm.Dispose();
                return (true, false);
            }
        }
        else
        {
            // If Language was NOT changed
            if (settingsForm.GetAutosaveTimerChanged)
            {
                // AND if AutosaveTimer was changed
                settingsForm.Dispose();
                return (false, true);
            }
            else
            {
                // AND if AutosaveTimer was NOT changed
                settingsForm.Dispose();
                return (false, false);
            }
        }
    }

    internal static (string, string) OpenFileImportSelector()
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.OpenWindows, "OpenFileImportSelector entered");

        FileImport fileImport = new();
        FileSelectorForm fileSelector = new(fileImport.GetServerList(), false);
        fileSelector.ShowDialog();

        string listBoxString = fileSelector.GetListBoxString;
        string listBoxName = fileSelector.GetListBoxName;

        fileSelector.Dispose();

        return (listBoxString, listBoxName);
    }

    internal static void OpenFileExportSelector(string[] values)
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.OpenWindows, "OpenFileExportSelector entered");

        FileImport fileImport = new();
        FileSelectorForm fileSelector = new(fileImport.GetServerList(), true);
        DialogResult dialogResult = fileSelector.ShowDialog();

        int fileCount = 0;

        if (dialogResult != DialogResult.Cancel)
        {
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.OpenWindows, $"dialogResult = {dialogResult}");
            FileExport fileExport = new(fileSelector.GetSelectedServers, [.. fileSelector.GetListBoxMulti]);
            fileExport.BackupFilesAndWrite(values);
            fileCount = fileExport.GetNumberOfChangedFiles;
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.OpenWindows, $"fileCount = {fileCount}");
        }
        else
        {
            Logging.Write(LogEventEnum.Warning, ProgramClassEnum.OpenWindows, $"dialogResult = {dialogResult}");
        }

        fileSelector.Dispose();

        if (fileCount == 0)
        {
            return;
        }

        Localization localization = new(GetSetSettings.GetCurrentLocale);
        string exportedFilesInfo = localization.GetString(LocalizationEnum.Inf_ExportedFiles);
        exportedFilesInfo = exportedFilesInfo.Replace("FILECOUNT", fileCount.ToString());

        ShowMessageBox.Show(localization.GetString(LocalizationEnum.MessageBoxInfo), exportedFilesInfo);
    }

    internal static void OpenBackupSelector()
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.OpenWindows, "OpenBackupSelector entered");

        if (!Checks.IsBackupDirEmpty())
        {
            Localization localization = new(GetSetSettings.GetCurrentLocale);

            ShowMessageBox.Show(localization.GetString(LocalizationEnum.MessageBoxInfo), localization.GetString(LocalizationEnum.Inf_NoFilesInBackupDir));
            return;
        }

        BackupSelectorForm backupSelector = new();
        backupSelector.ShowDialog();

        backupSelector.Dispose();
    }

    internal static void OpenTextViewer(bool isChangelog = false)
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.OpenWindows, "OpenTextViewer entered");
        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.OpenWindows, $"isChangelog = {isChangelog}");

        TextViewerForm textViewer = new(isChangelog);
        textViewer.ShowDialog();

        textViewer.Dispose();
    }

    internal static void OpenExplorer(string path)
    {
        Logging.Write(LogEventEnum.Info, ProgramClassEnum.OpenWindows, $"Trying to start explorer.exe with path: {path}");

        try
        {
            Process.Start("explorer.exe", path);
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.OpenWindows, $"explorer.exe started with path: {path}");
        }
        catch (Exception ex)
        {
            Logging.Write(LogEventEnum.Error, ProgramClassEnum.OpenWindows, "explorer.exe failed to start!");
            Logging.Write(LogEventEnum.ExMessage, ProgramClassEnum.OpenWindows, ex.Message);
            ShowMessageBox.ShowBug();
        }
    }

    internal static void OpenLinksInBrowser(string url)
    {
        Logging.Write(LogEventEnum.Info, ProgramClassEnum.OpenWindows, $"Trying to start default Browser with url: {url}");

        try
        {
            ProcessStartInfo info = new(url) { UseShellExecute = true };
            Process.Start(info);

            Logging.Write(LogEventEnum.Info, ProgramClassEnum.OpenWindows, $"Browser started with url: {url}");
        }
        catch (Exception ex)
        {
            Logging.Write(LogEventEnum.Error, ProgramClassEnum.OpenWindows, "Browser failed to start!");
            Logging.Write(LogEventEnum.ExMessage, ProgramClassEnum.OpenWindows, ex.Message);
            ShowMessageBox.ShowBug();
        }
    }

    internal static void OpenProcess(string path)
    {
        Logging.Write(LogEventEnum.Info, ProgramClassEnum.OpenWindows, $"Trying to start process with path: {path}");

        try
        {
            ProcessStartInfo info = new(path) { UseShellExecute = true };
            Process.Start(info);

            Logging.Write(LogEventEnum.Info, ProgramClassEnum.OpenWindows, $"Process started with path: {path}");
        }
        catch (Exception ex)
        {
            Logging.Write(LogEventEnum.Error, ProgramClassEnum.OpenWindows, "Process failed to start!");
            Logging.Write(LogEventEnum.ExMessage, ProgramClassEnum.OpenWindows, ex.Message);
            ShowMessageBox.ShowBug();
        }
    }
}
