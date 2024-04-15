using System.Windows.Forms;
using ChatManager.Enums;

namespace ChatManager.Services;

internal static class ShowMessageBox
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

        Logging.Write(LogEventEnum.BoxMessage, ProgramClassEnum.ShowMessageBox, "MessageBox will be shown");
        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.ShowMessageBox, $"caption: {caption}");
        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.ShowMessageBox, $"message: {message}");

        MessageBox.Show(message, caption, MessageBoxButtons.OK, icon);

        Logging.Write(LogEventEnum.BoxMessage, ProgramClassEnum.ShowMessageBox, "MessageBox accepted");
    }

    internal static bool ShowUpdate(string version, double fileSize)
    {
        Localization localization = new(GetSetSettings.GetCurrentLocale);
        DialogResult result = MessageBox.Show($"{localization.GetString(LocalizationEnum.Update_IsAvailable)} {version}\n\n{localization.GetString(LocalizationEnum.Update_FileSize).Replace("FILESIZE", fileSize.ToString())} MB", localization.GetString(LocalizationEnum.MessageBoxUpdate), MessageBoxButtons.YesNo, MessageBoxIcon.Information);

        Logging.Write(LogEventEnum.BoxMessage, ProgramClassEnum.ShowMessageBox, "Update MessageBox shown");

        if (result == DialogResult.Yes)
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.ShowMessageBox, "Update DialogResult is yes");
            return true;
        }
        else
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.ShowMessageBox, "Update DialogResult is no");
            return false;
        }
    }

    internal static void ShowBug()
    {
        Localization localization = new(GetSetSettings.GetCurrentLocale);
        DialogResult result = MessageBox.Show(localization.GetString(LocalizationEnum.Error_IsDetected), localization.GetString(LocalizationEnum.MessageBoxError), MessageBoxButtons.YesNo, MessageBoxIcon.Error);

        Logging.Write(LogEventEnum.BoxMessage, ProgramClassEnum.ShowMessageBox, "Bug MessageBox shown");

        if (result == DialogResult.Yes)
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.ShowMessageBox, "Bug DialogResult is yes");
            OpenWindows.OpenLinksInBrowser(GetSetSettings.GetBugPath);
        }
        else
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.ShowMessageBox, "Bug DialogResult is no");
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

        Logging.Write(LogEventEnum.BoxMessage, ProgramClassEnum.ShowMessageBox, "Restart MessageBox shown");

        MessageBox.Show(localization.GetString(LocalizationEnum.Inf_RestartRequired), localization.GetString(LocalizationEnum.MessageBoxInfo), MessageBoxButtons.OK, MessageBoxIcon.Information);

        Logging.Write(LogEventEnum.BoxMessage, ProgramClassEnum.ShowMessageBox, "Restart accepted");
        Logging.Write(LogEventEnum.Info, ProgramClassEnum.ShowMessageBox, "Restart initated");

        Application.Restart();
    }

    internal static bool ShowQuestion(string message)
    {
        Localization localization = new(GetSetSettings.GetCurrentLocale);

        Logging.Write(LogEventEnum.BoxMessage, ProgramClassEnum.ShowMessageBox, "Question MessageBox shown");

        DialogResult result = MessageBox.Show(message, localization.GetString(LocalizationEnum.MessageBoxInfo), MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

        if (result == DialogResult.Yes)
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.ShowMessageBox, "Question DialogResult is yes");
            return true;
        }
        else
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.ShowMessageBox, "Question DialogResult is no");
            return false;
        }
    }
}
