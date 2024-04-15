using System.IO;
using ChatManager.Enums;

namespace ChatManager.Services;

internal class Autosave
{
    internal Autosave()
    {
        Logging.Write(LogEventEnum.Info, ProgramClassEnum.Autosave, "Autosave Constructor created");
        if (PathChecked != true)
        {
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileImport, $"pathChecked = {PathChecked}");
            PathChecked = Checks.DirectoryCheck(CheckFolderEnum.AutosaveFolder);
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.FileImport, $"pathChecked = {PathChecked}");
        }
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
