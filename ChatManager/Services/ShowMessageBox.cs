using ChatManager.Enums;

namespace ChatManager.Services
{
    internal class ShowMessageBox
    {
        internal static void Show(string caption, string message)
        {
            Localization localization = new(GetSetSettings.GetCurrentLocale);

            MessageBoxIcon icon = MessageBoxIcon.None;

            switch (caption)
            {
                case var value when value == localization.GetString(LocalizationEnum.MessageBoxError):
                    icon = MessageBoxIcon.Error;
                    break;

                case var value when value == localization.GetString(LocalizationEnum.MessageBoxInfo):
                    icon = MessageBoxIcon.Information;
                    break;

                case var value when value == localization.GetString(LocalizationEnum.MessageBoxWarn):
                    icon = MessageBoxIcon.Warning;
                    break;

                case var value when value == localization.GetString(LocalizationEnum.MessageBoxUpdate):
                    icon = MessageBoxIcon.Information;
                    break;

                case var value when value == localization.GetString(LocalizationEnum.MessageBoxNoUpdate):
                    icon = MessageBoxIcon.Information;
                    break;
            }

            Logging.Write(LogEventEnum.BoxMessage, ProgramClassEnum.ShowMessageBox, "MessageBox shown");

            MessageBox.Show(message, caption, MessageBoxButtons.OK, icon);

            Logging.Write(LogEventEnum.BoxMessage, ProgramClassEnum.ShowMessageBox, "MessageBox accepted");
        }

        internal static bool ShowUpdate(string version)
        {
            Localization localization = new(GetSetSettings.GetCurrentLocale);
            DialogResult result = MessageBox.Show(localization.GetString(LocalizationEnum.Update_IsAvailable) + $" {version}", localization.GetString(LocalizationEnum.MessageBoxUpdate), MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            Logging.Write(LogEventEnum.BoxMessage, ProgramClassEnum.ShowMessageBox, "MessageBox shown");

            if (result == DialogResult.Yes)
            {
                Logging.Write(LogEventEnum.Info, ProgramClassEnum.ShowMessageBox, "DialogResult is yes");
                return true;
            }
            else
            {
                Logging.Write(LogEventEnum.Info, ProgramClassEnum.ShowMessageBox, "DialogResult is no");
                return false;
            }
        }

        internal static void ShowBug()
        {
            Localization localization = new(GetSetSettings.GetCurrentLocale);
            DialogResult result = MessageBox.Show(localization.GetString(LocalizationEnum.Error_IsDetected), localization.GetString(LocalizationEnum.MessageBoxError), MessageBoxButtons.YesNo, MessageBoxIcon.Error);

            Logging.Write(LogEventEnum.BoxMessage, ProgramClassEnum.ShowMessageBox, "MessageBox shown");

            if (result == DialogResult.Yes)
            {
                Logging.Write(LogEventEnum.Info, ProgramClassEnum.ShowMessageBox, "DialogResult is yes");
                OpenWindows.OpenLinksInBrowser(GetSetSettings.GetBugPath);
            }
            else
            {
                Logging.Write(LogEventEnum.Info, ProgramClassEnum.ShowMessageBox, "DialogResult is no");
            }
        }

        internal static void ShowLoggingBug(string message)
        {
            MessageBox.Show($"REPORT BUG WITH SCREENSHOT OF THIS!\n{message}", "CRITICAL ERROR");
            OpenWindows.OpenLinksInBrowser(GetSetSettings.GetBugPath);
        }

        internal static void ShowRestart()
        {
            Localization localization = new(GetSetSettings.GetCurrentLocale);

            Logging.Write(LogEventEnum.BoxMessage, ProgramClassEnum.ShowMessageBox, "MessageBox shown");

            MessageBox.Show(localization.GetString(LocalizationEnum.Inf_RestartRequired), localization.GetString(LocalizationEnum.MessageBoxInfo), MessageBoxButtons.OK, MessageBoxIcon.Information);

            Logging.Write(LogEventEnum.BoxMessage, ProgramClassEnum.ShowMessageBox, "MessageBox accepted");
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.ShowMessageBox, "Restart initated");

            Application.Restart();
        }
    }
}