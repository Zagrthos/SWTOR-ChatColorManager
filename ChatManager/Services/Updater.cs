namespace ChatManager.Services
{
    internal enum UpdateInterval
    {
        OnStartup,
        Daily,
        Weekly
    }

    internal static class Updater
    {
        private static readonly Version currentVersion = new(Application.ProductVersion);
        private static Version? onlineVersion;
        private static readonly string updateCheckURL = "https://raw.githubusercontent.com/Zagrthos/SWTOR-ChatColorManager/master/ChatManager/Update/version.txt";
        private static string updateURL = "https://github.com/Zagrthos/SWTOR-ChatColorManager/releases/";
        private static string updateName = "SWTOR-ChatManager-";
        private static string updatePath = string.Empty;

        public static async Task CheckForUpdateInterval()
        {
            string updateInterval = GetSetSettings.GetUpdateInterval;
            bool updateSearch = false;
            DateTime lastCheck = GetSetSettings.GetLastUpdateCheck;
            DateTime today = DateTime.Today;
            TimeSpan difference = today - lastCheck;

            if (updateInterval == UpdateInterval.OnStartup.ToString())
            {
                updateSearch = true;
            }
            else if (updateInterval == UpdateInterval.Daily.ToString())
            {
                if (difference.Days >= 1)
                {
                    updateSearch = true;
                }
            }
            else if (updateInterval == UpdateInterval.Weekly.ToString())
            {
                if (difference.Days >= 7)
                {
                    updateSearch = true;
                }
            }
            else
            {
                Logging.Write(LogEvent.Error, ProgramClass.Updater, "updateInterval Setting not set!");
                ShowMessageBox.ShowBug();
            }

            Logging.Write(LogEvent.Variable, ProgramClass.Updater, $"updateInterval set to: {updateInterval}");
            Logging.Write(LogEvent.Variable, ProgramClass.Updater, $"lastCheck set to: {lastCheck}");
            Logging.Write(LogEvent.Variable, ProgramClass.Updater, $"today set to: {today}");
            Logging.Write(LogEvent.Variable, ProgramClass.Updater, $"difference set to: {difference}");
            Logging.Write(LogEvent.Variable, ProgramClass.Updater, $"updateSearch set to: {updateSearch}");

            if (updateSearch)
            {
                await CheckForUpdates();
            }
        }

        public static async Task CheckForUpdates(bool fromUser = false)
        {
            Logging.Write(LogEvent.Method, ProgramClass.Updater, "CheckForUpdates entered");
            
            HttpClient client = new();
            Logging.Write(LogEvent.Info, ProgramClass.Updater, "HttpClient created");

            try
            {
                Logging.Write(LogEvent.Info, ProgramClass.Updater, "Check for Updates initiated");

                onlineVersion = new(await client.GetStringAsync(updateCheckURL));

                Logging.Write(LogEvent.Variable, ProgramClass.Updater, $"onlineVersion is: {onlineVersion}");

                if (onlineVersion > currentVersion)
                {
                    Logging.Write(LogEvent.Info, ProgramClass.Updater, "Update is available!");

                    if (ShowMessageBox.ShowUpdate(onlineVersion.ToString()))
                    {
                        if (GetSetSettings.GetUpdateDownload)
                        {
                            Logging.Write(LogEvent.Info, ProgramClass.Updater, "Manual download initiated");
                            OpenWindows.OpenLinksInBrowser($"{updateURL}/tag/v{onlineVersion}/");
                        }
                        else
                        {
                            Logging.Write(LogEvent.Info, ProgramClass.Updater, "Background download initiated");
                            await DownloadUpdate();
                        }

                        Logging.Write(LogEvent.Info, ProgramClass.Updater, "HttpClient disposed!");
                        client.Dispose();
                    }
                }
                else
                {
                    Logging.Write(LogEvent.Info, ProgramClass.Updater, "No Update available!");

                    if (fromUser)
                    {
                        Localization localization = new(GetSetSettings.GetCurrentLocale);
                        ShowMessageBox.Show(localization.GetString("MessageBoxNoUpdate"), localization.GetString("Update_IsNotAvailable"));
                    }

                    Logging.Write(LogEvent.Info, ProgramClass.Updater, "HttpClient disposed!");
                    client.Dispose();
                }

                // Save the date of the last update Check but only if the user has NOT initiated it
                if (GetSetSettings.GetUpdateInterval != UpdateInterval.OnStartup.ToString() && !fromUser)
                {
                    GetSetSettings.SaveSettings(Setting.lastUpdateCheck, DateTime.Today);
                    Logging.Write(LogEvent.Variable, ProgramClass.Updater, $"Last Update Check: {DateTime.Today}");
                }
            }
            catch (HttpRequestException ex)
            {
                Logging.Write(LogEvent.Error, ProgramClass.Updater, "Check for Updates failed!");
                Logging.Write(LogEvent.ExMessage, ProgramClass.Updater, $"{ex.Message}");

                Logging.Write(LogEvent.Info, ProgramClass.Updater, "HttpClient disposed!");
                client.Dispose();
            }
        }

        private static async Task DownloadUpdate()
        {
            Logging.Write(LogEvent.Method, ProgramClass.Updater, "DownloadUpdate entered");

            updateName += $"v{onlineVersion}.exe";
            Logging.Write(LogEvent.Variable, ProgramClass.Updater, $"updateName set to: {updateName}");

            updateURL += $"download/v{onlineVersion}/{updateName}";
            Logging.Write(LogEvent.Variable, ProgramClass.Updater, $"updateURL set to: {updateURL}");

            HttpClient client = new();
            Logging.Write(LogEvent.Info, ProgramClass.Updater, "HttpClient created");

            try
            {
                Logging.Write(LogEvent.Info, ProgramClass.Updater, "Connection Test initiated");

                HttpResponseMessage responseMessage = await client.GetAsync(updateURL, HttpCompletionOption.ResponseHeadersRead);

                if (!responseMessage.IsSuccessStatusCode)
                {
                    Logging.Write(LogEvent.Error, ProgramClass.Updater, "Connection Test failed!");
                    Logging.Write(LogEvent.ExMessage, ProgramClass.Updater, $"{responseMessage}");
                    return;
                }

                updatePath = Path.Combine(Path.GetTempPath(), updateName);
                Logging.Write(LogEvent.Variable, ProgramClass.Updater, $"Download Path: {updatePath}");

                FileStream fileStream = new(updatePath, FileMode.Create, FileAccess.Write, FileShare.None);
                Stream stream = await responseMessage.Content.ReadAsStreamAsync();
                await stream.CopyToAsync(fileStream);

                client.Dispose();
                responseMessage.Dispose();
                fileStream.Dispose();
                stream.Dispose();

                Logging.Write(LogEvent.Variable, ProgramClass.Updater, $"Update downloaded to: {updatePath}");

                Localization localization = new(GetSetSettings.GetCurrentLocale);
                ShowMessageBox.Show(localization.GetString("MessageBoxUpdate"), localization.GetString("Update_IsInstallReady"));

                InstallUpdate();
            }
            catch (HttpRequestException ex)
            {
                Logging.Write(LogEvent.Error, ProgramClass.Updater, "Update download failed!");
                Logging.Write(LogEvent.ExMessage, ProgramClass.Updater, $"{ex.Message}");
            }
        }

        private static void InstallUpdate()
        {
            Logging.Write(LogEvent.Method, ProgramClass.Updater, "InstallUpdate entered");

            OpenWindows.OpenProcess(updatePath);

            Environment.Exit(0);
        }
    }
}