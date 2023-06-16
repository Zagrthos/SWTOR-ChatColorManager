using System.Text.Json;

namespace ChatManager.Services
{
    internal class Localization
    {
        private Dictionary<string, string> strings = new();
        private readonly string installPath = Application.StartupPath;

        public Localization(string locale)
        {
            Logging.Write(LogEvent.Info, ProgramClass.Localization, $"Localization Constructor created with locale: {locale}");
            CheckLocale(locale);
        }

        private void CheckLocale(string locale)
        {
            Logging.Write(LogEvent.Method, ProgramClass.Localization, "CheckLocale Entered");

            Logging.Write(LogEvent.Variable, ProgramClass.Localization, $"Localization path is: {Path.Combine(installPath, "Localization", $"{locale}.json")}");
            var jsonString = File.ReadAllText(Path.Combine(installPath, "Localization", $"{locale}.json"));

            // Check if file has content
            if (jsonString != null)
            {
                // If yes decode the JSON
                var tempStrings = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);

                // Check if JSON has content
                if (tempStrings != null)
                {
                    strings = tempStrings;
                    GetSetSettings.SaveSettings(Setting.locale, locale);
                }

                // If not log Warning
                else
                {
                    Logging.Write(LogEvent.Warning, ProgramClass.Localization, "JSON file without content detected!");
                }
            }

            // If not log Warning
            else
            {
                Logging.Write(LogEvent.Warning, ProgramClass.Localization, "Localization file without content detected!");
            }
        }

        public string GetString(string name)
        {
            Logging.Write(LogEvent.Method, ProgramClass.Localization, "GetString Entered");

            if (strings.TryGetValue(name, out var result))
            {
                return result;
            }
            else
            {
                Logging.Write(LogEvent.Warning, ProgramClass.Localization, $"No localization found for string: {name}!");
                return string.Empty;
            }
        }
    }
}
