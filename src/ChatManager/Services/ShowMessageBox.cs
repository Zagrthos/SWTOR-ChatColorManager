using System;
using System.Globalization;
using System.Windows.Forms;
using ChatManager.Enums;

namespace ChatManager.Services;

internal static class ShowMessageBox
{
    internal static void Show(string caption, string message)
    {
        Localization localization = new(GetSetSettings.GetCurrentLocale);
        MessageBoxIcon icon = caption switch
        {
            string value when value == localization.GetString(Enums.LocalizationStrings.MessageBoxError) => MessageBoxIcon.Error,
            string value when value == localization.GetString(Enums.LocalizationStrings.MessageBoxInfo) => MessageBoxIcon.Information,
            string value when value == localization.GetString(Enums.LocalizationStrings.MessageBoxWarn) => MessageBoxIcon.Warning,
            string value when value == localization.GetString(Enums.LocalizationStrings.MessageBoxUpdate) => MessageBoxIcon.Information,
            string value when value == localization.GetString(Enums.LocalizationStrings.MessageBoxNoUpdate) => MessageBoxIcon.Information,
            _ => MessageBoxIcon.None,
        };

        Logging.Write(LogEvent.BoxMessage, LogClass.ShowMessageBox, "MessageBox will be shown");
        Logging.Write(LogEvent.Variable, LogClass.ShowMessageBox, $"caption: {caption}");
        Logging.Write(LogEvent.Variable, LogClass.ShowMessageBox, $"message: {message}");

        MessageBox.Show(message, caption, MessageBoxButtons.OK, icon);

        Logging.Write(LogEvent.BoxMessage, LogClass.ShowMessageBox, "MessageBox accepted");
    }

    internal static bool ShowUpdate(string version, double fileSize)
    {
        Localization localization = new(GetSetSettings.GetCurrentLocale);
        DialogResult result = MessageBox.Show($"{localization.GetString(Enums.LocalizationStrings.Update_IsAvailable)} {version}\n\n{localization.GetString(Enums.LocalizationStrings.Update_FileSize).Replace("FILESIZE", fileSize.ToString(CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase)} MB", localization.GetString(Enums.LocalizationStrings.MessageBoxUpdate), MessageBoxButtons.YesNo, MessageBoxIcon.Information);

        Logging.Write(LogEvent.BoxMessage, LogClass.ShowMessageBox, "Update MessageBox shown");

        if (result == DialogResult.Yes)
        {
            Logging.Write(LogEvent.Info, LogClass.ShowMessageBox, "Update DialogResult is yes");
            return true;
        }

        Logging.Write(LogEvent.Info, LogClass.ShowMessageBox, "Update DialogResult is no");

        return false;
    }

    internal static void ShowBug()
    {
        Localization localization = new(GetSetSettings.GetCurrentLocale);
        DialogResult result = MessageBox.Show(localization.GetString(Enums.LocalizationStrings.Error_IsDetected), localization.GetString(Enums.LocalizationStrings.MessageBoxError), MessageBoxButtons.YesNo, MessageBoxIcon.Error);

        Logging.Write(LogEvent.BoxMessage, LogClass.ShowMessageBox, "Bug MessageBox shown");

        if (result == DialogResult.Yes)
        {
            Logging.Write(LogEvent.Info, LogClass.ShowMessageBox, "Bug DialogResult is yes");
            OpenWindows.OpenLinksInBrowser(GetSetSettings.GetBugPath);
        }
        else
        {
            Logging.Write(LogEvent.Info, LogClass.ShowMessageBox, "Bug DialogResult is no");
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

        Logging.Write(LogEvent.BoxMessage, LogClass.ShowMessageBox, "Restart MessageBox shown");

        MessageBox.Show(localization.GetString(Enums.LocalizationStrings.Inf_RestartRequired), localization.GetString(Enums.LocalizationStrings.MessageBoxInfo), MessageBoxButtons.OK, MessageBoxIcon.Information);

        Logging.Write(LogEvent.BoxMessage, LogClass.ShowMessageBox, "Restart accepted");
        Logging.Write(LogEvent.Info, LogClass.ShowMessageBox, "Restart initated");

        Application.Restart();
    }

    internal static bool ShowQuestion(string message)
    {
        Localization localization = new(GetSetSettings.GetCurrentLocale);

        Logging.Write(LogEvent.BoxMessage, LogClass.ShowMessageBox, "Question MessageBox shown");

        DialogResult result = MessageBox.Show(message, localization.GetString(Enums.LocalizationStrings.MessageBoxInfo), MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

        if (result == DialogResult.Yes)
        {
            Logging.Write(LogEvent.Info, LogClass.ShowMessageBox, "Question DialogResult is yes");
            return true;
        }

        Logging.Write(LogEvent.Info, LogClass.ShowMessageBox, "Question DialogResult is no");

        return false;
    }
}
