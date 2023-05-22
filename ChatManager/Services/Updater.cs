namespace ChatManager.Services
{
    internal class Updater
    {
        private static readonly string currentVersion = Application.ProductVersion;
        private static readonly string updateCheckURL = "https://raw.githubusercontent.com/Zagrthos/SWTOR-ChatColorManager/master/ChatManager/Update/version.txt";
        private static string updateURL = "https://github.com/Zagrthos/SWTOR-ChatColorManager/releases/tag/";

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

                    updateURL += $"v{onlineVersion}";
                    await Logging.Write(LogEvent.Variable, ProgramClass.Updater, $"updateURL set to: {updateURL}");

                    if (await ShowMessageBox.ShowUpdate(onlineVersion))
                    {
                        await OpenWindows.OpenLinksInBrowser(updateURL);
                    }
                }
                else
                {
                    await Logging.Write(LogEvent.Info, ProgramClass.Updater, "No Update available!");
                }
            }
            catch (HttpRequestException ex)
            {
                await Logging.Write(LogEvent.Error, ProgramClass.Updater, "Check for Updates failed!");
                await Logging.Write(LogEvent.ExMessage, ProgramClass.Updater, $"{ex.Message}");
            }
        }
    }
}