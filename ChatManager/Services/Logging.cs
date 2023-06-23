using ChatManager.Enums;
using System.Timers;

namespace ChatManager.Services
{
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
            Write(LogEventEnum.Info, ProgramClassEnum.Logging, "Logging started");
            Write(LogEventEnum.Info, ProgramClassEnum.Logging, $"Application version is: {Application.ProductVersion}");

            // Add Timer to write any second all open entries in the log
            timer = new(10000);
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

        internal static void Write(LogEventEnum Level, ProgramClassEnum ProgramClass, string Message)
        {
            string Event = Level switch
            {
                LogEventEnum.Info => "INFO",
                LogEventEnum.Warning => "WARNING",
                LogEventEnum.Error => "ERROR",
                LogEventEnum.Variable => "VARIABLE",
                LogEventEnum.Method => "METHOD",
                LogEventEnum.Control => "CONTROL",
                LogEventEnum.ExMessage => "EXECPTION",
                LogEventEnum.BoxMessage => "MESSAGE-BOX",
                LogEventEnum.Setting => "SETTING",
                _ => "UNCATEGORIZED"
            };

            if (logWriter != null)
            {
                logWriter.WriteLine($"[{DateTime.Now:HH:mm:ss}] => {Event} on {ProgramClass}: {Message}");
            }
            else
            {
                Initialize();
            }
        }

        internal static void Dispose()
        {
            if (logWriter != null)
            {
                timer?.Stop();
                Write(LogEventEnum.Info, ProgramClassEnum.Logging, "Logging stopped");
                logWriter.Flush();
                logWriter.Close();
            }
        }
    }
}