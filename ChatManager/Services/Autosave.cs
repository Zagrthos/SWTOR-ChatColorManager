using System.Diagnostics.CodeAnalysis;
using System.IO;
using ChatManager.Enums;

namespace ChatManager.Services;

[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Right now there is no static needed.")]
internal sealed class Autosave
{
    internal Autosave()
    {
        Logging.Write(LogEventEnum.Info, ProgramClassEnum.Autosave, "Autosave Constructor created");

        if (PathChecked)
        {
            return;
        }

        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileImport, $"pathChecked = {PathChecked}");
        PathChecked = Checks.DirectoryCheck(CheckFolderEnum.AutosaveFolder);
        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileImport, $"pathChecked = {PathChecked}");
    }

    private static bool PathChecked = GetSetSettings.GetAutosaveAvailability;
    private static readonly string AutosavePath = Path.Combine(GetSetSettings.GetAutosavePath, "autosave.txt");

    internal void DoAutosave(string charName, string serverName, string[] colorData)
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.Autosave, "DoAutosave entered");

        string colorDataString = string.Join(";", colorData);
        string data = string.Join(";", serverName, charName, colorDataString);

        File.WriteAllText(AutosavePath, data);

        Logging.Write(LogEventEnum.Info, ProgramClassEnum.Autosave, "Autosave created");
    }
}
