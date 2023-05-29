namespace ChatManager.Services
{
    internal class Updater
    {
        private static readonly Version currentVersion = new(Application.ProductVersion);
        private static readonly string updateCheckURL = "https://raw.githubusercontent.com/Zagrthos/SWTOR-ChatColorManager/master/ChatManager/Update/version.txt";
        private static string updateURL = "https://github.com/Zagrthos/SWTOR-ChatColorManager/releases/tag/";

        public static async Task CheckForUpdates()
        {
            Logging.Write(LogEvent.Info, ProgramClass.Updater, "CheckForUpdates entered");
            
            HttpClient client = new();
            Logging.Write(LogEvent.Info, ProgramClass.Updater, "HttpClient created");

            try
            {
                Logging.Write(LogEvent.Info, ProgramClass.Updater, "Check for Updates initiated");

                Version onlineVersion = new(await client.GetStringAsync(updateCheckURL));

                Logging.Write(LogEvent.Variable, ProgramClass.Updater, $"onlineVersion is: {onlineVersion}");

                if (onlineVersion > currentVersion)
                {
                    Logging.Write(LogEvent.Info, ProgramClass.Updater, "Update is available!");

                    updateURL += $"v{onlineVersion}";
                    Logging.Write(LogEvent.Variable, ProgramClass.Updater, $"updateURL set to: {updateURL}");

                    if (ShowMessageBox.ShowUpdate(onlineVersion.ToString()))
                    {
                        OpenWindows.OpenLinksInBrowser(updateURL);
                    }
                }
                else
                {
                    Logging.Write(LogEvent.Info, ProgramClass.Updater, "No Update available!");
                }
            }
            catch (HttpRequestException ex)
            {
                Logging.Write(LogEvent.Error, ProgramClass.Updater, "Check for Updates failed!");
                Logging.Write(LogEvent.ExMessage, ProgramClass.Updater, $"{ex.Message}");
            }

            Logging.Write(LogEvent.Info, ProgramClass.Updater, "HttpClient disposed!");
            client.Dispose();
        }
    }
}