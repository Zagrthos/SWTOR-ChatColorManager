using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using ChatManager.Enums;

namespace ChatManager.Services;

internal class Localization
{
    private Dictionary<string, string> Strings = [];
    private readonly string InstallPath = Application.StartupPath;

    internal Localization(string locale)
    {
        Logging.Write(LogEventEnum.Info, ProgramClassEnum.Localization, $"Localization Constructor created with locale: {locale}");
        CheckLocale(locale);
    }

    private void CheckLocale(string locale)
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.Localization, "CheckLocale entered");

        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Localization, $"Localization path is: {Path.Combine(InstallPath, "Localization", $"{locale}.json")}");
        string jsonString = File.ReadAllText(Path.Combine(InstallPath, "Localization", $"{locale}.json"));

        // Check if file has content
        if (jsonString is not null)
        {
            // If yes decode the JSON
            Dictionary<string, string>? tempStrings = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);

            // Check if JSON has content
            if (tempStrings is not null)
            {
                Strings = tempStrings;
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
        if (Strings.TryGetValue(name, out string? result))
        {
            return result;
        }

        Logging.Write(LogEventEnum.Warning, ProgramClassEnum.Localization, $"No localization found for string: {name}!");

        return string.Empty;
    }

    internal string GetString(LocalizationEnum localization)
    {
        if (Strings.TryGetValue(localization.ToString(), out string? result))
        {
            return result;
        }

        Logging.Write(LogEventEnum.Warning, ProgramClassEnum.Localization, $"No localization found for string: {localization}!");

        return string.Empty;
    }

    internal (string, string) GetLocalDateTime()
    {
        string currentCulture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

        string currentLocale = GetSetSettings.GetCurrentLocale;
        CultureInfo? culture = currentLocale switch
        {
            "de" => new("de"),
            "en" => new("en"),
            "fr" => new("fr"),
            _ => throw new InvalidOperationException($"{currentLocale} is not implemented!"),
        };

        Application.CurrentCulture = culture!;

        string date = DateTime.Now.ToString("d");
        string time = DateTime.Now.ToString("T");

        culture = new(currentCulture);

        Application.CurrentCulture = culture;

        return (date, time);
    }
}
