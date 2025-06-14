﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using ChatManager.Enums;

namespace ChatManager.Services;

internal sealed class Localization
{
    private Dictionary<string, string> _strings = [];
    private readonly string _installPath = Application.StartupPath;

    internal Localization(string locale)
    {
        Logging.Write(LogEvent.Info, LogClass.Localization, $"Localization Constructor created with locale: {locale}");
        CheckLocale(locale);
    }

    private void CheckLocale(string locale)
    {
        Logging.Write(LogEvent.Method, LogClass.Localization, "CheckLocale entered");

        Logging.Write(LogEvent.Variable, LogClass.Localization, $"Localization path is: {Path.Combine(_installPath, "Localization", $"{locale}.json")}");
        string jsonString = File.ReadAllText(Path.Combine(_installPath, "Localization", $"{locale}.json"));

        // Check if file has content
        if (jsonString is not null)
        {
            // If yes decode the JSON
            Dictionary<string, string>? tempStrings = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);

            // Check if JSON has content
            if (tempStrings is not null)
            {
                _strings = tempStrings;
                GetSetSettings.SaveSettings(SettingsNames.locale, locale);
            }

            // If not log Warning
            else
            {
                Logging.Write(LogEvent.Warning, LogClass.Localization, "JSON file without content detected!");
            }
        }

        // If not log Warning
        else
        {
            Logging.Write(LogEvent.Warning, LogClass.Localization, "Localization file without content detected!");
        }
    }

    internal string GetString(string name)
    {
        if (_strings.TryGetValue(name, out string? result))
            return result;

        Logging.Write(LogEvent.Warning, LogClass.Localization, $"No localization found for string: {name}!");

        return string.Empty;
    }

    internal string GetString(Enums.LocalizationStrings localization)
    {
        if (_strings.TryGetValue(localization.ToString(), out string? result))
            return result;

        Logging.Write(LogEvent.Warning, LogClass.Localization, $"No localization found for string: {localization}!");

        return string.Empty;
    }

    internal static (string, string) GetLocalDateTime()
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

        DateTimeOffset now = DateTimeOffset.Now;
        string date = now.ToString("d", CultureInfo.InvariantCulture);
        string time = now.ToString("T", CultureInfo.InvariantCulture);

        culture = new(currentCulture);

        Application.CurrentCulture = culture;

        return (date, time);
    }
}
