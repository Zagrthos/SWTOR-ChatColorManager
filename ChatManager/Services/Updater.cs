using ChatManager.Properties;

namespace ChatManager.Services
{
    internal class Updater
    {
        private static readonly Version currentVersion = new(Application.ProductVersion);
        private static Version? onlineVersion;
        private static readonly string updateCheckURL = "https://raw.githubusercontent.com/Zagrthos/SWTOR-ChatColorManager/master/ChatManager/Update/version.txt";
        private static string updateURL = "https://github.com/Zagrthos/SWTOR-ChatColorManager/releases/download/";
        private static string updateName = "SWTOR-ChatManager-";

        public static async Task CheckForUpdates(bool fromUser = false)
        {
            Logging.Write(LogEvent.Info, ProgramClass.Updater, "CheckForUpdates entered");
            
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
                        await DownloadUpdate();

                        Logging.Write(LogEvent.Info, ProgramClass.Updater, "HttpClient disposed!");
                        client.Dispose();
                    }
                }
                else
                {
                    Logging.Write(LogEvent.Info, ProgramClass.Updater, "No Update available!");

                    if (fromUser)
                    {
                        ShowMessageBox.Show(Resources.MessageBoxNoUpdate, Resources.Update_IsNotAvailable);
                    }

                    Logging.Write(LogEvent.Info, ProgramClass.Updater, "HttpClient disposed!");
                    client.Dispose();
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
            Logging.Write(LogEvent.Info, ProgramClass.Updater, "DownloadUpdate entered");

            updateName += $"v{onlineVersion}.exe";
            Logging.Write(LogEvent.Variable, ProgramClass.Updater, $"updateName set to: {updateName}");

            updateURL += $"v{onlineVersion}/{updateName}";
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

                string tempPath = Path.Combine(Path.GetTempPath(), updateName);
                Logging.Write(LogEvent.Variable, ProgramClass.Updater, $"Download Path: {tempPath}");

                FileStream fileStream = new(tempPath, FileMode.Create, FileAccess.Write, FileShare.None);
                Stream stream = await responseMessage.Content.ReadAsStreamAsync();
                await stream.CopyToAsync(fileStream);

                Logging.Write(LogEvent.Variable, ProgramClass.Updater, $"Update downloaded to: {tempPath}");

                ShowMessageBox.Show(Resources.MessageBoxUpdate, Resources.Update_IsInstallReady);
            }
            catch (HttpRequestException ex)
            {
                Logging.Write(LogEvent.Error, ProgramClass.Updater, "Update download failed!");
                Logging.Write(LogEvent.ExMessage, ProgramClass.Updater, $"{ex.Message}");
            }
        }
    }
}