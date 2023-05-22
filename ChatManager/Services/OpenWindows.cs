using ChatManager.Forms;
using System.Diagnostics;

namespace ChatManager.Services
{
    internal class OpenWindows
    {
        // Debug Test Method
        /*
        private async void StartColorPicker(object sender, EventArgs e)
        {
            await Logging.Write(LogEvent.Method, ProgramClass.OpenWindows, "StartColorPicker Entered");
            ColorPickerForm colorPicker = new("Test", Color.Black);
            await Logging.Write(LogEvent.Info, ProgramClass.OpenWindows, $"Form {colorPicker.Text} created");
            colorPicker.ShowDialog();
            colorPicker.Dispose();
        }*/

        // Production Method
        // ??? Color Picker must be opened from an non-async method, due to the ShowDialog Method which is synchronous
        public static async Task<string> OpenColorPicker(string text, Color color)
        {
            await Logging.Write(LogEvent.Method, ProgramClass.OpenWindows, "OpenColorPicker Entered");

            // Create new Form with the Text of the sender Button
            ColorPickerForm colorPicker = new(text, color);
            await Logging.Write(LogEvent.Info, ProgramClass.OpenWindows, $"Form {colorPicker.Text} created");
            colorPicker.ShowDialog();
            string hexColor = colorPicker.GetHexColor;

            colorPicker.Dispose();

            return hexColor;
        }

        public static async Task OpenAbout()
        {
            await Logging.Write(LogEvent.Method, ProgramClass.OpenWindows, "OpenAbout Entered");

            AboutForm aboutForm = new();
            aboutForm.ShowDialog();

            aboutForm.Dispose();
        }

        // Open the FileSelector but with the import Settings
        public static (string, string) OpenFileImportSelector()
        {
            Logging.Write(LogEvent.Method, ProgramClass.OpenWindows, "OpenFileImportSelector Entered").ConfigureAwait(false);

            FileImport fileImport = new();
            FileSelectorForm fileSelector = new(fileImport.GetServerList(), false);
            fileSelector.ShowDialog();

            string listBoxString = FileSelectorForm.GetListBoxString;
            string listBoxName = FileSelectorForm.GetListBoxName;

            fileSelector.Dispose();

            return (listBoxString, listBoxName);
        }

        // Open the FileSelector but with the export Settings
        public static async Task OpenFileExportSelector(string[] values)
        {
            await Logging.Write(LogEvent.Method, ProgramClass.OpenWindows, "OpenFileExportSelector Entered");

            FileImport fileImport = new();
            FileSelectorForm fileSelector = new(fileImport.GetServerList(), true);
            fileSelector.ShowDialog();

            FileExport fileExport = new();
            await fileExport.BackupFilesAndWrite(values);

            fileSelector.Dispose();
        }

        // Open Explorer Window with a specified path
        public static async Task OpenExplorer(string path)
        {
            await Logging.Write(LogEvent.Info, ProgramClass.OpenWindows, $"Trying to start explorer.exe with path: {path}");

            try
            {
                Process.Start("explorer.exe", path);
                await Logging.Write(LogEvent.Info, ProgramClass.OpenWindows, $"explorer.exe started with path: {path}");
            }
            catch (Exception ex)
            {
                await Logging.Write(LogEvent.Error, ProgramClass.OpenWindows, "explorer.exe failed to start!");
                await Logging.Write(LogEvent.ExMessage, ProgramClass.OpenWindows, ex.Message);
                await ShowMessageBox.ShowBug();
            }
        }

        public static async Task OpenLinksInBrowser(string url)
        {
            await Logging.Write(LogEvent.Info, ProgramClass.OpenWindows, $"Trying to start default Browser with url: {url}");

            try
            {
                Process.Start(new ProcessStartInfo(url)
                {
                    UseShellExecute = true
                });
                await Logging.Write(LogEvent.Info, ProgramClass.OpenWindows, $"Browser started with url: {url}");
            }
            catch (Exception ex)
            {
                await Logging.Write(LogEvent.Error, ProgramClass.OpenWindows, "Browser failed to start!");
                await Logging.Write(LogEvent.ExMessage, ProgramClass.OpenWindows, ex.Message);
                await ShowMessageBox.ShowBug();
            }
        }
    }
}
