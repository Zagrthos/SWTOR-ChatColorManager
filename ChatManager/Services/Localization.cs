using System.Text.Json;

namespace ChatManager.Services
{
    internal class Localization
    {
        private readonly Dictionary<string, string> strings = new();
        private readonly string installPath = Application.StartupPath;

        public Localization(string locale)
        {
            Logging.Write(LogEvent.Info, ProgramClass.Localization, $"Localization Constructor created with locale: {locale}");

            var jsonString = File.ReadAllText(Path.Combine(installPath, "Localization", $"{locale}.json"));
            if (jsonString != null)
            {
                var tempStrings = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
                if (tempStrings != null)
                {
                    strings = tempStrings;
                }
            }
        }

        public string GetString(string name)
        {
            Logging.Write(LogEvent.Method, ProgramClass.Localization, $"GetString Entered");

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
