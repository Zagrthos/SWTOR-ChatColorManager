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
                CheckAutosavePath();
            }
        }

        private static bool pathChecked = false;
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

        private static void CheckAutosavePath()
        {
            if (!Directory.Exists(GetSetSettings.GetAutosavePath))
            {
                Logging.Write(LogEvent.Warning, ProgramClass.Autosave, "autosavePath does not exist, creating it!");
                Directory.CreateDirectory(GetSetSettings.GetAutosavePath);

                Logging.Write(LogEvent.Info, ProgramClass.Checks, "Checking again if autosavePath exists");
                if (Directory.Exists(GetSetSettings.GetAutosavePath))
                {
                    Logging.Write(LogEvent.Variable, ProgramClass.Autosave, $"autosavePath created at: {GetSetSettings.GetAutosavePath}");
                }
                else
                {
                    Logging.Write(LogEvent.Warning, ProgramClass.Autosave, $"Could not create autosavePath!");
                    ShowMessageBox.ShowBug();
                }
            }
            else
            {
                Logging.Write(LogEvent.Variable, ProgramClass.Autosave, $"autosavePath exists at: {GetSetSettings.GetAutosavePath}");
            }

            pathChecked = true;
            Logging.Write(LogEvent.Variable, ProgramClass.FileImport, $"pathChecked = {pathChecked}");
        }
    }
}
