using System.IO;
using ChatManager.Enums;

namespace ChatManager.Services;

internal static class Autosave
{
    private static readonly string AutosavePath = Path.Combine(GetSetSettings.GetAutosavePath, "autosave.txt");

    internal static void DoAutosave(string charName, string serverName, string[] colorData)
    {
        Logging.Write(LogEvent.Method, LogClass.Autosave, "DoAutosave entered");

        string colorDataString = string.Join(";", colorData);
        string data = string.Join(";", serverName, charName, colorDataString);

        File.WriteAllText(AutosavePath, data);

        Logging.Write(LogEvent.Info, LogClass.Autosave, "Autosave created");
    }
}
