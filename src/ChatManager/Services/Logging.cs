using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using ChatManager.Enums;

namespace ChatManager.Services;

internal static class Logging
{
    private static readonly string LogSession = DateTimeOffset.Now.ToString("yyyy-MM-dd_HH-mm-ss", CultureInfo.InvariantCulture);
    private static readonly string LogPath = GetSetSettings.GetLogPath;
    private static StreamWriter? LogWriter;
    private static System.Timers.Timer? Timer;

    internal static void Initialize()
    {
        if (!Directory.Exists(LogPath))
        {
            try
            {
                Directory.CreateDirectory(LogPath);
            }
            catch (UnauthorizedAccessException ex)
            {
                ShowMessageBox.ShowLoggingBug(ex.Message);
                Environment.Exit(0);
            }
        }

        string logfilePath = Path.Combine(LogPath, $"ChatManager_{LogSession}.log");
        LogWriter = new(logfilePath, true);
        Write(LogEvent.Info, LogClass.Logging, "Logging started");
        Write(LogEvent.Info, LogClass.Logging, $"Application version is: {Application.ProductVersion}");

        // Add Timer to write any second all open entries in the log
        Timer = new(10000);
        Timer.Elapsed += TimerElapsed;
#if !DEBUG
        Timer.Start();
#endif

        LogfilesCleaning();
    }

    private static void LogfilesCleaning()
    {
        Write(LogEvent.Method, LogClass.Logging, "LogfilesCleaning entered");

        string logPath = GetSetSettings.GetLogPath;

        DateTime dateSevenDaysAgo = DateTime.Today.AddDays(-7);

        string[] logFiles = Directory.GetFiles(logPath);

        int counter = 0;
        foreach (string file in logFiles.Where(f => File.GetLastWriteTime(f) < dateSevenDaysAgo))
        {
            File.Delete(file);
            counter++;
        }

        Write(LogEvent.Info, LogClass.Logging, $"{counter} logs deleted");
    }

    private static void TimerElapsed(object? sender, ElapsedEventArgs e)
    {
        if (LogWriter is null)
            return;

        LogWriter.Flush();
        LogWriter.Close();
        LogWriter = new(Path.Combine(LogPath, $"ChatManager_{LogSession}.log"), true);
    }

    internal static void Write(LogEvent Level, LogClass ProgramClass, string Message)
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

        if (LogWriter is not null)
        {
            LogWriter.WriteLine($"[{DateTime.Now:HH:mm:ss}] => {Event} on {ProgramClass}: {Message}");
        }
        else
        {
            Initialize();
        }
    }

    internal static void Dispose()
    {
        if (LogWriter is null)
            return;

        Timer?.Stop();
        Write(LogEvent.Info, LogClass.Logging, "Logging stopped");
        LogWriter.Flush();
        LogWriter.Close();
    }
}
