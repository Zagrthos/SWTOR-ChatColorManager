using ChatManager.Enums;
using ChatManager.Forms;
using System.Diagnostics;

namespace ChatManager.Services
{
    internal class OpenWindows
    {
        internal static string OpenColorPicker(string text, Color color)
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.OpenWindows, "OpenColorPicker Entered");

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
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.OpenWindows, "OpenAbout Entered");

            AboutForm aboutForm = new();
            aboutForm.ShowDialog();

            aboutForm.Dispose();
        }

        internal static (bool, bool) OpenSettings()
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.OpenWindows, "OpenSettings Entered");

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

        // Open the FileSelector but with the import Settings
        internal static (string, string) OpenFileImportSelector()
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.OpenWindows, "OpenFileImportSelector Entered");

            FileImport fileImport = new();
            FileSelectorForm fileSelector = new(fileImport.GetServerList(), false);
            fileSelector.ShowDialog();

            string listBoxString = fileSelector.GetListBoxString;
            string listBoxName = fileSelector.GetListBoxName;

            fileSelector.Dispose();

            return (listBoxString, listBoxName);
        }

        // Open the FileSelector but with the export Settings
        internal static void OpenFileExportSelector(string[] values)
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.OpenWindows, "OpenFileExportSelector Entered");

            FileImport fileImport = new();
            FileSelectorForm fileSelector = new(fileImport.GetServerList(), true);
            DialogResult dialogResult = fileSelector.ShowDialog();

            int fileCount = 0;

            if (dialogResult != DialogResult.Cancel)
            {
                Logging.Write(LogEventEnum.Variable, ProgramClassEnum.OpenWindows, $"dialogResult = {dialogResult}");
                FileExport fileExport = new(fileSelector.GetSelectedServers, fileSelector.GetListBoxMulti.ToArray());
                fileExport.BackupFilesAndWrite(values);
                fileCount = fileExport.GetNumberOfChangedFiles;
                Logging.Write(LogEventEnum.Variable, ProgramClassEnum.OpenWindows, $"fileCount = {fileCount}");
            }
            else
            {
                Logging.Write(LogEventEnum.Warning, ProgramClassEnum.OpenWindows, $"dialogResult = {dialogResult}");
            }

            fileSelector.Dispose();

            Localization localization = new(GetSetSettings.GetCurrentLocale);

            string exportedFilesInfo = localization.GetString(LocalizationEnum.Inf_ExportedFiles);
            exportedFilesInfo = exportedFilesInfo.Replace("FILECOUNT", fileCount.ToString());

            ShowMessageBox.Show(localization.GetString(LocalizationEnum.MessageBoxInfo), exportedFilesInfo);
        }

        internal static void OpenBackupSelector()
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.OpenWindows, "OpenBackupSelector Entered");

            BackupSelectorForm backupSelector = new();
            backupSelector.ShowDialog();

            backupSelector.Dispose();
        }

        // Open Explorer Window with a specified path
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
                Process.Start(new ProcessStartInfo(url)
                {
                    UseShellExecute = true
                });
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
                Process.Start(new ProcessStartInfo(path)
                {
                    UseShellExecute = true
                });
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
}