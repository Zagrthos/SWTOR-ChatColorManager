using ChatManager.Properties;

namespace ChatManager.Services
{
    internal class ShowMessageBox
    {
        public static async Task Show(string caption, string message)
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

            await Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, "MessageBox shown.");
            await Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, $"Caption: {caption}");
            await Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, $"Message: {message}");
            await Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, $"Icon: {icon}");

            MessageBox.Show(message, caption, MessageBoxButtons.OK, icon);

            await Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, "MessageBox accepted.");
        }

        public static async Task<bool> ShowUpdate(string version)
        {
            DialogResult result = MessageBox.Show(Resources.Update_IsAvailable + $" {version}", Resources.MessageBoxUpdate, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            await Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, "MessageBox shown.");
            await Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, $"Caption: {Resources.MessageBoxUpdate}");
            await Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, $"Message: {Resources.Update_IsAvailable}");
            await Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, $"Icon: {MessageBoxIcon.Information}");

            if (result == DialogResult.Yes)
            {
                await Logging.Write(LogEvent.Info, ProgramClass.ShowMessageBox, "DialogResult is yes");
                return true;
            } else
            {
                await Logging.Write(LogEvent.Info, ProgramClass.ShowMessageBox, "DialogResult is no");
                return false;
            }
        }

        public static async Task ShowBug()
        {
            DialogResult result = MessageBox.Show(Resources.Error_IsDetected, Resources.MessageBoxError, MessageBoxButtons.YesNo, MessageBoxIcon.Error);

            await Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, "MessageBox shown.");
            await Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, $"Caption: {Resources.MessageBoxError}");
            await Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, $"Message: {Resources.Error_IsDetected}");
            await Logging.Write(LogEvent.BoxMessage, ProgramClass.ShowMessageBox, $"Icon: {MessageBoxIcon.Information}");

            if (result == DialogResult.Yes)
            {
                await Logging.Write(LogEvent.Info, ProgramClass.ShowMessageBox, "DialogResult is yes");
                await OpenWindows.OpenLinksInBrowser(GetSetSettings.GetBugPath);
            }
            else
            {
                await Logging.Write(LogEvent.Info, ProgramClass.ShowMessageBox, "DialogResult is no");
            }
        }
    }
}