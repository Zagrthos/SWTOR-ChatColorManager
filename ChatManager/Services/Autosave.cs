using System.Text.Json;

namespace ChatManager.Services
{
    internal class Autosave
    {
        public Autosave()
        {
            Logging.Write(LogEvent.Info, ProgramClass.Autosave, "Autosave Constructor created");
            if (pathChecked != true)
            {
                Logging.Write(LogEvent.Variable, ProgramClass.FileImport, $"pathChecked = {pathChecked}");
                pathChecked = Checks.DirectoryCheck(CheckFolder.AutosaveFolder);
                Logging.Write(LogEvent.Variable, ProgramClass.FileImport, $"pathChecked = {pathChecked}");
            }
        }

        private static bool pathChecked = GetSetSettings.GetAutosaveAvailability;
        private static readonly string autosavePath = Path.Combine(GetSetSettings.GetAutosavePath, "autosave.json");

        public void DoAutosave(string charName, string serverName, string[] colorData)
        {
            Logging.Write(LogEvent.Method, ProgramClass.Autosave, "DoAutosave entered");

            var jsonData = new
            {
                CharName = charName,
                ServerName = serverName,
                ColorData = colorData
            };

            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var json = JsonSerializer.Serialize(jsonData, jsonOptions);

            File.WriteAllText(autosavePath, json);
        }
    }
}
