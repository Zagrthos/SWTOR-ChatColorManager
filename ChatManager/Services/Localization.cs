using ChatManager.Enums;
using System.Globalization;
using System.Text.Json;

namespace ChatManager.Services
{
    internal class Localization
    {
        private Dictionary<string, string> strings = new();
        private readonly string installPath = Application.StartupPath;

        internal Localization(string locale)
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.Localization, $"Localization Constructor created with locale: {locale}");
            CheckLocale(locale);
        }

        private void CheckLocale(string locale)
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.Localization, "CheckLocale entered");

            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Localization, $"Localization path is: {Path.Combine(installPath, "Localization", $"{locale}.json")}");
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
                    GetSetSettings.SaveSettings(SettingsEnum.locale, locale);
                }

                // If not log Warning
                else
                {
                    Logging.Write(LogEventEnum.Warning, ProgramClassEnum.Localization, "JSON file without content detected!");
                }
            }

            // If not log Warning
            else
            {
                Logging.Write(LogEventEnum.Warning, ProgramClassEnum.Localization, "Localization file without content detected!");
            }
        }

        internal string GetString(string name)
        {
            if (strings.TryGetValue(name, out var result))
            {
                return result;
            }
            else
            {
                Logging.Write(LogEventEnum.Warning, ProgramClassEnum.Localization, $"No localization found for string: {name}!");
                return string.Empty;
            }
        }

        internal string GetString(LocalizationEnum localization)
        {
            if (strings.TryGetValue(localization.ToString(), out var result))
            {
                return result;
            }
            else
            {
                Logging.Write(LogEventEnum.Warning, ProgramClassEnum.Localization, $"No localization found for string: {localization}!");
                return string.Empty;
            }
        }

        internal (string, string) GetLocalDateTime()
        {
            string currentCulture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

            CultureInfo? culture = GetSetSettings.GetCurrentLocale switch
            {
                "de" => new("de"),
                "en" => new("en"),
                "fr" => new("fr"),
                _ => throw new NotImplementedException(),
            };

            Application.CurrentCulture = culture!;

            string date = DateTime.Now.ToString("d");
            string time = DateTime.Now.ToString("T");

            culture = new(currentCulture);

            Application.CurrentCulture = culture;

            return (date, time);
        }
    }
}