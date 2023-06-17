using System.Timers;

namespace ChatManager.Services
{
    internal enum LogEvent
    {
        Info,
        Warning,
        Error,
        Variable,
        Method,
        Control,
        ExMessage,
        BoxMessage,
        Setting
    }

    internal enum ProgramClass
    {
        MainForm,
        ProgramConfig,
        AboutForm,
        BackupSelector,
        ColorPickerForm,
        FileSelectorForm,
        SettingsForm,
        Autosave,
        Checks,
        Converter,
        FileExport,
        FileImport,
        Localization,
        Logging,
        OpenWindows,
        ShowMessageBox,
        Updater
    }

    internal static class Logging
    {
        private static readonly string LogSession = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        private static readonly string LogPath = GetSetSettings.GetLogPath;
        private static StreamWriter? logWriter;
        private static System.Timers.Timer? timer;

        internal static void Initialize()
        {
            if (!Directory.Exists(LogPath))
            {
                try
                {
                    Directory.CreateDirectory(LogPath);
                }
                catch (Exception ex)
                {
                    ShowMessageBox.ShowLoggingBug(ex.Message);
                    Environment.Exit(0);
                }
            }

            string logfilePath = Path.Combine(LogPath, $"ChatManager_{LogSession}.log");
            logWriter = new(logfilePath, true);
            Write(LogEvent.Info, ProgramClass.Logging, "Logging started");
            Write(LogEvent.Info, ProgramClass.Logging, $"Application version is: {Application.ProductVersion}");

            // Add Timer to write any second all open entries in the log
            timer = new(5000);
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }

        // Stop logWriter and restart it
        private static void TimerElapsed(object? sender, ElapsedEventArgs e)
        {
            if (logWriter != null)
            {
                lock (logWriter)
                {
                    logWriter.Flush();
                    logWriter.Close();
                    logWriter = new(Path.Combine(LogPath, $"ChatManager_{LogSession}.log"), true);
                }
            }
        }

        internal static void Write(LogEvent Level, ProgramClass programClass, string Message)
        {
            string Event = Level switch
            {
                LogEvent.Info => "INFO",
                LogEvent.Warning => "WARNING",
                LogEvent.Error => "ERROR",
                LogEvent.Variable => "VARIABLE",
                LogEvent.Method => "METHOD",
                LogEvent.Control => "CONTROL",
                LogEvent.ExMessage => "EXECPTION",
                LogEvent.BoxMessage => "MESSAGE-BOX",
                LogEvent.Setting => "SETTING",
                _ => "UNCATEGORIZED"
            };

            if (logWriter != null)
            {
                logWriter.WriteLine($"[{DateTime.Now:HH:mm:ss}] => {Event} on {programClass}: {Message}");
            } else
            {
                Initialize();
            }
        }

        internal static void Dispose()
        {
            if (logWriter != null)
            {
                timer?.Stop();
                Write(LogEvent.Info, ProgramClass.Logging, "Logging stopped");
                logWriter.Flush();
                logWriter.Close();
            }
        }
    }
}