using Windows.Media.Protection.PlayReady;

namespace ChatManager.Services
{
    internal class Updater
    {
        private static readonly string currentVersion = Application.ProductVersion;
        private static readonly string updateCheckURL = "https://github.com/Zagrthos/SWTOR-ChatManager/blob/master/ChatManager/Update/version.txt";
        private static string updateURL = "https://github.com/Zagrthos/SWTOR-ChatManager/releases/download/";
        private static readonly string tempPath = Path.GetTempPath();
        private static bool updateAvailable = false;

        public static async Task CheckForUpdates()
        {
            await Logging.Write(LogEvent.Info, ProgramClass.Updater, "CheckForUpdates entered").ConfigureAwait(false);
            
            HttpClient client = new();
            await Logging.Write(LogEvent.Info, ProgramClass.Updater, "HttpClient created").ConfigureAwait(false);

            try
            {
                await Logging.Write(LogEvent.Info, ProgramClass.Updater, "Check for Updates initiated").ConfigureAwait(false);

                string onlineVersion = await client.GetStringAsync(updateCheckURL);
                await Logging.Write(LogEvent.Variable, ProgramClass.Updater, $"onlineVersion is: {onlineVersion}").ConfigureAwait(false);

                if (onlineVersion != currentVersion)
                {
                    await Logging.Write(LogEvent.Info, ProgramClass.Updater, "Update is available!").ConfigureAwait(false);
                    updateAvailable = true;

                    updateURL += $"{onlineVersion}/SWTOR-ChatManager-{onlineVersion}";
                    await Logging.Write(LogEvent.Variable, ProgramClass.Updater, $"updateURL set to: {updateURL}").ConfigureAwait(false);

                    await DownloadUpdate();
                }
            }
            catch (HttpRequestException ex)
            {
                await Logging.Write(LogEvent.Error, ProgramClass.Updater, "Check for Updates failed!").ConfigureAwait(false);
                await Logging.Write(LogEvent.ExMessage, ProgramClass.Updater, $"{ex.Message}").ConfigureAwait(false);
            }
        }

        private static async Task DownloadUpdate()
        {
            await Logging.Write(LogEvent.Info, ProgramClass.Updater, "DownloadUpdate entered").ConfigureAwait(false);

            if (updateAvailable)
            {
                await Logging.Write(LogEvent.Info, ProgramClass.Updater, "Update is available").ConfigureAwait(false);

                HttpClient client = new();
                await Logging.Write(LogEvent.Info, ProgramClass.Updater, "HttpClient created").ConfigureAwait(false);

                try
                {
                    await Logging.Write(LogEvent.Info, ProgramClass.Updater, "Update download initiated").ConfigureAwait(false);

                    var bytes = await client.GetByteArrayAsync(updateCheckURL);
                    await File.WriteAllBytesAsync(tempPath, bytes);

                    await Logging.Write(LogEvent.Variable, ProgramClass.Updater, $"File downloaded to: {tempPath}").ConfigureAwait(false);
                }
                catch (HttpRequestException ex)
                {
                    await Logging.Write(LogEvent.Error, ProgramClass.Updater, "Update download failed!").ConfigureAwait(false);
                    await Logging.Write(LogEvent.ExMessage, ProgramClass.Updater, $"{ex.Message}").ConfigureAwait(false);
                }
            }
        }

        //private static async Task InstallUpdate()
        //{

        //}
    }
}