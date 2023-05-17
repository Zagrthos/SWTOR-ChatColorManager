using ChatManager.Properties;

namespace ChatManager.Services
{
    internal class ShowMessageBox
    {
        public static async void Show(string caption, string message)
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
    }
}