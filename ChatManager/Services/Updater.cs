using System.Diagnostics;

namespace ChatManager.Services
{
    internal class Updater
    {
        private static readonly string currentVersion = Application.ProductVersion;
        private static readonly string updateCheckURL = "https://raw.githubusercontent.com/Zagrthos/SWTOR-ChatColorManager/master/ChatManager/Update/version.txt";
        private static string updateURL = "https://github.com/Zagrthos/SWTOR-ChatColorManager/releases/download/";
        private static readonly string tempPath = Path.GetTempPath();
        private static bool updateAvailable = false;

        public static async Task CheckForUpdates()
        {
            await Logging.Write(LogEvent.Info, ProgramClass.Updater, "CheckForUpdates entered");
            
            HttpClient client = new();
            await Logging.Write(LogEvent.Info, ProgramClass.Updater, "HttpClient created");

            try
            {
                await Logging.Write(LogEvent.Info, ProgramClass.Updater, "Check for Updates initiated");

                string onlineVersion = await client.GetStringAsync(updateCheckURL);
                await Logging.Write(LogEvent.Variable, ProgramClass.Updater, $"onlineVersion is: {onlineVersion}");

                if (onlineVersion != currentVersion)
                {
                    await Logging.Write(LogEvent.Info, ProgramClass.Updater, "Update is available!");
                    updateAvailable = true;

                    updateURL += $"v{onlineVersion}/SWTOR-ChatManager-v{onlineVersion}";
                    await Logging.Write(LogEvent.Variable, ProgramClass.Updater, $"updateURL set to: {updateURL}");

                    await DownloadUpdate();
                }
                else
                {
                    await Logging.Write(LogEvent.Info, ProgramClass.Updater, "No Update available!");
                    updateAvailable = false;
                }
            }
            catch (HttpRequestException ex)
            {
                await Logging.Write(LogEvent.Error, ProgramClass.Updater, "Check for Updates failed!");
                await Logging.Write(LogEvent.ExMessage, ProgramClass.Updater, $"{ex.Message}");
            }
        }

        private static async Task DownloadUpdate()
        {
            await Logging.Write(LogEvent.Info, ProgramClass.Updater, "DownloadUpdate entered");

            if (updateAvailable)
            {
                await Logging.Write(LogEvent.Info, ProgramClass.Updater, "Update is available");

                HttpClient client = new();
                await Logging.Write(LogEvent.Info, ProgramClass.Updater, "HttpClient created");

                try
                {
                    await Logging.Write(LogEvent.Info, ProgramClass.Updater, "Update download initiated");

                    var bytes = await client.GetByteArrayAsync(updateCheckURL);
                    await File.WriteAllBytesAsync(tempPath, bytes);

                    await Logging.Write(LogEvent.Variable, ProgramClass.Updater, $"File downloaded to: {tempPath}");

                    await InstallUpdate();
                }
                catch (HttpRequestException ex)
                {
                    await Logging.Write(LogEvent.Error, ProgramClass.Updater, "Update download failed!");
                    await Logging.Write(LogEvent.ExMessage, ProgramClass.Updater, $"{ex.Message}");
                    await ShowMessageBox.ShowBug();
                }
            }
        }

        private static async Task InstallUpdate()
        {
            await Logging.Write(LogEvent.Info, ProgramClass.Updater, "InstallUpdate entered");
            
            try
            {
                await Logging.Write(LogEvent.Info, ProgramClass.Updater, "Trying to start Update Installer");
                Process.Start($"{tempPath}\\SWTOR-ChatManager-v1.0.0");

                await Logging.Write(LogEvent.Info, ProgramClass.Updater, "Update installer started");
                await Logging.Write(LogEvent.Info, ProgramClass.Updater, "Main Process will be killed");

                Application.Exit();
            }
            catch (Exception ex)
            {
                await Logging.Write(LogEvent.Error, ProgramClass.Updater, "Update installation failed!");
                await Logging.Write(LogEvent.ExMessage, ProgramClass.Updater, $"{ex.Message}");
                await ShowMessageBox.ShowBug();
            }
        }
    }
}