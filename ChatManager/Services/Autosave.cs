using System.IO;
using ChatManager.Enums;

namespace ChatManager.Services;

internal class Autosave
{
    internal Autosave()
    {
        Logging.Write(LogEventEnum.Info, ProgramClassEnum.Autosave, "Autosave Constructor created");
        if (pathChecked != true)
        {
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileImport, $"pathChecked = {pathChecked}");
            pathChecked = Checks.DirectoryCheck(CheckFolderEnum.AutosaveFolder);
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileImport, $"pathChecked = {pathChecked}");
        }
    }

    private static bool pathChecked = GetSetSettings.GetAutosaveAvailability;
    private static readonly string autosavePath = Path.Combine(GetSetSettings.GetAutosavePath, "autosave.txt");

    internal void DoAutosave(string charName, string serverName, string[] colorData)
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.Autosave, "DoAutosave entered");

        string colorDataString = string.Join(";", colorData);
        string data = string.Join(";", serverName, charName, colorDataString);

        File.WriteAllText(autosavePath, data);

        Logging.Write(LogEventEnum.Info, ProgramClassEnum.Autosave, "Autosave created");
    }
}
