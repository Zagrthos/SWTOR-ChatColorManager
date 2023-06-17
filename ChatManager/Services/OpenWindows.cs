using ChatManager.Forms;
using System.Diagnostics;

namespace ChatManager.Services
{
    internal class OpenWindows
    {
        public static string OpenColorPicker(string text, Color color)
        {
            Logging.Write(LogEvent.Method, ProgramClass.OpenWindows, "OpenColorPicker Entered");

            // Create new Form with the Text of the sender Button
            ColorPickerForm colorPicker = new(text, color);
            Logging.Write(LogEvent.Info, ProgramClass.OpenWindows, $"Form {colorPicker.Text} created");
            colorPicker.ShowDialog();
            string hexColor = colorPicker.GetHexColor;

            colorPicker.Dispose();

            return hexColor;
        }

        public static void OpenAbout()
        {
            Logging.Write(LogEvent.Method, ProgramClass.OpenWindows, "OpenAbout Entered");

            AboutForm aboutForm = new();
            aboutForm.ShowDialog();

            aboutForm.Dispose();
        }

        public static (bool, bool) OpenSettings()
        {
            Logging.Write(LogEvent.Method, ProgramClass.OpenWindows, "OpenSettings Entered");

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
        public static (string, string) OpenFileImportSelector()
        {
            Logging.Write(LogEvent.Method, ProgramClass.OpenWindows, "OpenFileImportSelector Entered");

            FileImport fileImport = new();
            FileSelectorForm fileSelector = new(fileImport.GetServerList(), false);
            fileSelector.ShowDialog();

            string listBoxString = fileSelector.GetListBoxString;
            string listBoxName = fileSelector.GetListBoxName;

            fileSelector.Dispose();

            return (listBoxString, listBoxName);
        }

        // Open the FileSelector but with the export Settings
        public static void OpenFileExportSelector(string[] values)
        {
            Logging.Write(LogEvent.Method, ProgramClass.OpenWindows, "OpenFileExportSelector Entered");

            FileImport fileImport = new();
            FileSelectorForm fileSelector = new(fileImport.GetServerList(), true);
            DialogResult dialogResult = fileSelector.ShowDialog();

            int fileCount = 0;

            if (dialogResult != DialogResult.Cancel)
            {
                Logging.Write(LogEvent.Variable, ProgramClass.OpenWindows, $"dialogResult = {dialogResult}");
                FileExport fileExport = new(fileSelector.GetSelectedServers, fileSelector.GetListBoxMulti.ToArray());
                fileExport.BackupFilesAndWrite(values);
                fileCount = fileExport.GetNumberOfChangedFiles;
                Logging.Write(LogEvent.Variable, ProgramClass.OpenWindows, $"fileCount = {fileCount}");
            }
            else
            {
                Logging.Write(LogEvent.Warning, ProgramClass.OpenWindows, $"dialogResult = {dialogResult}");
            }

            fileSelector.Dispose();

            Localization localization = new(GetSetSettings.GetCurrentLocale);

            string exportedFilesInfo = localization.GetString("Inf_ExportedFiles");
            exportedFilesInfo = exportedFilesInfo.Replace("FILECOUNT", fileCount.ToString());

            ShowMessageBox.Show(localization.GetString("MessageBoxInfo"), exportedFilesInfo);
        }

        public static void OpenBackupSelector()
        {
            Logging.Write(LogEvent.Method, ProgramClass.OpenWindows, "OpenBackupSelector Entered");

            BackupSelectorForm backupSelector = new();
            backupSelector.ShowDialog();

            backupSelector.Dispose();
        }

        // Open Explorer Window with a specified path
        public static void OpenExplorer(string path)
        {
            Logging.Write(LogEvent.Info, ProgramClass.OpenWindows, $"Trying to start explorer.exe with path: {path}");

            try
            {
                Process.Start("explorer.exe", path);
                Logging.Write(LogEvent.Info, ProgramClass.OpenWindows, $"explorer.exe started with path: {path}");
            }
            catch (Exception ex)
            {
                Logging.Write(LogEvent.Error, ProgramClass.OpenWindows, "explorer.exe failed to start!");
                Logging.Write(LogEvent.ExMessage, ProgramClass.OpenWindows, ex.Message);
                ShowMessageBox.ShowBug();
            }
        }

        public static void OpenLinksInBrowser(string url)
        {
            Logging.Write(LogEvent.Info, ProgramClass.OpenWindows, $"Trying to start default Browser with url: {url}");

            try
            {
                Process.Start(new ProcessStartInfo(url)
                {
                    UseShellExecute = true
                });
                Logging.Write(LogEvent.Info, ProgramClass.OpenWindows, $"Browser started with url: {url}");
            }
            catch (Exception ex)
            {
                Logging.Write(LogEvent.Error, ProgramClass.OpenWindows, "Browser failed to start!");
                Logging.Write(LogEvent.ExMessage, ProgramClass.OpenWindows, ex.Message);
                ShowMessageBox.ShowBug();
            }
        }

        public static void OpenProcess(string path)
        {
            Logging.Write(LogEvent.Info, ProgramClass.OpenWindows, $"Trying to start process with path: {path}");

            try
            {
                Process.Start(new ProcessStartInfo(path)
                {
                    UseShellExecute = true
                });
                Logging.Write(LogEvent.Info, ProgramClass.OpenWindows, $"Process started with path: {path}");
            }
            catch (Exception ex)
            {
                Logging.Write(LogEvent.Error, ProgramClass.OpenWindows, "Process failed to start!");
                Logging.Write(LogEvent.ExMessage, ProgramClass.OpenWindows, ex.Message);
                ShowMessageBox.ShowBug();
            }
        }
    }
}
