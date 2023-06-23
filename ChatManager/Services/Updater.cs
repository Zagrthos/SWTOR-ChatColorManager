using ChatManager.Enums;

namespace ChatManager.Services
{
    internal static class Updater
    {
        private static readonly Version currentVersion = new(Application.ProductVersion);
        private static Version? onlineVersion;
        private static readonly string updateCheckURL = "https://raw.githubusercontent.com/Zagrthos/SWTOR-ChatColorManager/master/ChatManager/Update/version.txt";
        private static string updateURL = "https://github.com/Zagrthos/SWTOR-ChatColorManager/releases/";
        private static string updateName = "SWTOR-ChatManager-";
        private static string updatePath = string.Empty;
        private static string updateDownloadText = string.Empty;

        internal static string GetUpdateDownloadText => updateDownloadText;

        internal static async Task CheckForUpdateInterval()
        {
            string updateInterval = GetSetSettings.GetUpdateInterval;
            bool updateSearch = false;
            DateTime lastCheck = GetSetSettings.GetLastUpdateCheck;
            DateTime today = DateTime.Today;
            TimeSpan difference = today - lastCheck;

            if (updateInterval == UpdateEnum.OnStartup.ToString())
            {
                updateSearch = true;
            }
            else if (updateInterval == UpdateEnum.Daily.ToString())
            {
                if (difference.Days >= 1)
                {
                    updateSearch = true;
                }
            }
            else if (updateInterval == UpdateEnum.Weekly.ToString())
            {
                if (difference.Days >= 7)
                {
                    updateSearch = true;
                }
            }
            else
            {
                Logging.Write(LogEventEnum.Error, ProgramClassEnum.Updater, "updateInterval Setting not set!");
                ShowMessageBox.ShowBug();
            }

            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"updateInterval set to: {updateInterval}");
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"lastCheck set to: {lastCheck}");
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"today set to: {today}");
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"difference set to: {difference}");
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"updateSearch set to: {updateSearch}");

            if (updateSearch)
            {
                await CheckForUpdates();
            }
        }

        internal static async Task CheckForUpdates(bool fromUser = false)
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.Updater, "CheckForUpdates entered");

            HttpClient client = new();
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.Updater, "HttpClient created");

            try
            {
                Logging.Write(LogEventEnum.Info, ProgramClassEnum.Updater, "Check for Updates initiated");

                onlineVersion = new(await client.GetStringAsync(updateCheckURL));

                Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"onlineVersion is: {onlineVersion}");

                if (onlineVersion > currentVersion)
                {
                    Logging.Write(LogEventEnum.Info, ProgramClassEnum.Updater, "Update is available!");

                    if (ShowMessageBox.ShowUpdate(onlineVersion.ToString()))
                    {
                        if (GetSetSettings.GetUpdateDownload)
                        {
                            Logging.Write(LogEventEnum.Info, ProgramClassEnum.Updater, "Manual download initiated");
                            OpenWindows.OpenLinksInBrowser($"{updateURL}/tag/v{onlineVersion}/");
                        }
                        else
                        {
                            Logging.Write(LogEventEnum.Info, ProgramClassEnum.Updater, "Background download initiated");
                            await DownloadUpdate();
                        }

                        Logging.Write(LogEventEnum.Info, ProgramClassEnum.Updater, "HttpClient disposed!");
                        client.Dispose();
                    }
                }
                else
                {
                    Logging.Write(LogEventEnum.Info, ProgramClassEnum.Updater, "No Update available!");

                    if (fromUser)
                    {
                        Localization localization = new(GetSetSettings.GetCurrentLocale);
                        ShowMessageBox.Show(localization.GetString("MessageBoxNoUpdate"), localization.GetString("Update_IsNotAvailable"));
                    }

                    Logging.Write(LogEventEnum.Info, ProgramClassEnum.Updater, "HttpClient disposed!");
                    client.Dispose();
                }

                // Save the date of the last update Check but only if the user has NOT initiated it
                if (GetSetSettings.GetUpdateInterval != UpdateEnum.OnStartup.ToString() && !fromUser)
                {
                    GetSetSettings.SaveSettings(SettingsEnum.lastUpdateCheck, DateTime.Today);
                    Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"Last Update Check: {DateTime.Today}");
                }
            }
            catch (HttpRequestException ex)
            {
                Logging.Write(LogEventEnum.Error, ProgramClassEnum.Updater, "Check for Updates failed!");
                Logging.Write(LogEventEnum.ExMessage, ProgramClassEnum.Updater, $"{ex.Message}");

                Logging.Write(LogEventEnum.Info, ProgramClassEnum.Updater, "HttpClient disposed!");
                client.Dispose();
            }
        }

        private static async Task DownloadUpdate()
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.Updater, "DownloadUpdate entered");

            updateName += $"v{onlineVersion}.exe";
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"updateName set to: {updateName}");

            updateURL += $"download/v{onlineVersion}/{updateName}";
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"updateURL set to: {updateURL}");

            HttpClient client = new();
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.Updater, "HttpClient created");

            try
            {
                Logging.Write(LogEventEnum.Info, ProgramClassEnum.Updater, "Connection Test initiated");

                HttpResponseMessage responseMessage = await client.GetAsync(updateURL, HttpCompletionOption.ResponseHeadersRead);

                if (!responseMessage.IsSuccessStatusCode)
                {
                    Logging.Write(LogEventEnum.Error, ProgramClassEnum.Updater, "Connection Test failed!");
                    Logging.Write(LogEventEnum.ExMessage, ProgramClassEnum.Updater, $"{responseMessage}");
                    return;
                }

                updatePath = Path.Combine(Path.GetTempPath(), updateName);
                Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"Download Path: {updatePath}");

                Localization localization = new(GetSetSettings.GetCurrentLocale);
                updateDownloadText = localization.GetString("downloadProgressToolStripMenuItem");

                // Count the length of the to download file
                long? totalBytes = responseMessage.Content.Headers.ContentLength;

                // Download the file and then log the progress
                using (FileStream filestream = new(updatePath, FileMode.Create, FileAccess.Write, FileShare.None))
                using (Stream stream = await responseMessage.Content.ReadAsStreamAsync())
                {
                    byte[] buffer = new byte[65536];
                    long totalBytesRead = 0;
                    int bytesRead;
                    double lastLoggedPercent = 0;

                    while ((bytesRead = await stream.ReadAsync(buffer)) != 0)
                    {
                        await filestream.WriteAsync(buffer.AsMemory(0, bytesRead));
                        totalBytesRead += bytesRead;
                        double percent = totalBytes.HasValue ? ((double)totalBytesRead /  totalBytes.Value) * 100 : -1;

                        if (percent > -1)
                        {
                            // Remove all ,xx
                            percent = Math.Round(percent);

                            // But only log if the progress has changed 1%
                            if (Math.Abs(percent - lastLoggedPercent) >= 1)
                            {
                                DownloadProgressReporter.OnDownloadProgressChanged(percent);
                                Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"progress: {percent}%");
                                lastLoggedPercent = percent;
                            }
                        }
                    }
                }

                client.Dispose();
                responseMessage.Dispose();

                Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"Update downloaded to: {updatePath}");

                ShowMessageBox.Show(localization.GetString("MessageBoxUpdate"), localization.GetString("Update_IsInstallReady"));

                InstallUpdate();
            }
            catch (HttpRequestException ex)
            {
                Logging.Write(LogEventEnum.Error, ProgramClassEnum.Updater, "Update download failed!");
                Logging.Write(LogEventEnum.ExMessage, ProgramClassEnum.Updater, $"{ex.Message}");
            }
        }

        private static void InstallUpdate()
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.Updater, "InstallUpdate entered");

            OpenWindows.OpenProcess(updatePath);

            Logging.Dispose();

            Environment.Exit(0);
        }
    }

    internal class DownloadProgressEventArgs : EventArgs
    {
        private readonly double progress;

        internal double GetDownloadProgress()
        {
            return progress;
        }

        internal DownloadProgressEventArgs(double progress)
        {
            this.progress = progress;
        }
    }

    internal static class DownloadProgressReporter
    {
        internal static event EventHandler<DownloadProgressEventArgs>? DownloadProgressChanged;

        internal static void OnDownloadProgressChanged(double progress)
        {
            DownloadProgressChanged?.Invoke(null, new DownloadProgressEventArgs(progress));
        }
    }
}