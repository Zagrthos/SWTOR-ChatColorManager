using ChatManager.Enums;

namespace ChatManager.Services
{
    internal class Autosave
    {
        internal Autosave()
        {
            Logging.Write(LogEvent.Info, ProgramClass.Autosave, "Autosave Constructor created");
            if (pathChecked != true)
            {
                Logging.Write(LogEvent.Variable, ProgramClass.FileImport, $"pathChecked = {pathChecked}");
                pathChecked = Checks.DirectoryCheck(CheckFolderEnum.AutosaveFolder);
                Logging.Write(LogEvent.Variable, ProgramClass.FileImport, $"pathChecked = {pathChecked}");
            }
        }

        private static bool pathChecked = GetSetSettings.GetAutosaveAvailability;
        private static readonly string autosavePath = Path.Combine(GetSetSettings.GetAutosavePath, "autosave.txt");

        internal void DoAutosave(string charName, string serverName, string[] colorData)
        {
            Logging.Write(LogEvent.Method, ProgramClass.Autosave, "DoAutosave entered");

            string colorDataString = string.Join(";", colorData);
            string data = string.Join(";", serverName, charName, colorDataString);

            File.WriteAllText(autosavePath, data);

            Logging.Write(LogEvent.Info, ProgramClass.Autosave, "Autosave created");
        }
    }
}