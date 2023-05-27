using ChatManager.Properties;

namespace ChatManager.Services
{
    internal class ShowMessageBox
    {
        public static void Show(string caption, string message)
        {
            MessageBoxIcon icon = MessageBoxIcon.None;

            switch (caption)
            {
                case var value when value == Resources.MessageBoxError:
                    icon = MessageBoxIcon.Error;
                    break;

                case var value when value == Resources.MessageBoxInfo:
                    icon = MessageBoxIcon.Information;
                    break;

                case var value when value == Resources.MessageBoxWarn:
                    icon = MessageBoxIcon.Warning;
                    break;
            }

            Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, "MessageBox shown.");
            Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, $"Caption: {caption}");
            Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, $"Message: {message}");
            Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, $"Icon: {icon}");

            MessageBox.Show(message, caption, MessageBoxButtons.OK, icon);

            Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, "MessageBox accepted.");
        }

        public static bool ShowUpdate(string version)
        {
            DialogResult result = MessageBox.Show(Resources.Update_IsAvailable + $" {version}", Resources.MessageBoxUpdate, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, "MessageBox shown.");
            Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, $"Caption: {Resources.MessageBoxUpdate}");
            Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, $"Message: {Resources.Update_IsAvailable}");
            Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, $"Icon: {MessageBoxIcon.Information}");

            if (result == DialogResult.Yes)
            {
                Logging.Write(LogEvent.Info, ProgramClass.ShowMessageBox, "DialogResult is yes");
                return true;
            } else
            {
                Logging.Write(LogEvent.Info, ProgramClass.ShowMessageBox, "DialogResult is no");
                return false;
            }
        }

        public static void ShowBug()
        {
            DialogResult result = MessageBox.Show(Resources.Error_IsDetected, Resources.MessageBoxError, MessageBoxButtons.YesNo, MessageBoxIcon.Error);

            Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, "MessageBox shown.");
            Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, $"Caption: {Resources.MessageBoxError}");
            Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, $"Message: {Resources.Error_IsDetected}");
            Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, $"Icon: {MessageBoxIcon.Information}");

            if (result == DialogResult.Yes)
            {
                Logging.Write(LogEvent.Info, ProgramClass.ShowMessageBox, "DialogResult is yes");
                OpenWindows.OpenLinksInBrowser(GetSetSettings.GetBugPath);
            }
            else
            {
                Logging.Write(LogEvent.Info, ProgramClass.ShowMessageBox, "DialogResult is no");
            }
        }
    }
}