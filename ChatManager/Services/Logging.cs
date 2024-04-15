using System;
using System.IO;
using System.Timers;
using System.Windows.Forms;
using ChatManager.Enums;

namespace ChatManager.Services;

internal static class Logging
{
    private static readonly string LogSession = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
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
            catch (Exception ex)
            {
                ShowMessageBox.ShowLoggingBug(ex.Message);
                Environment.Exit(0);
            }
        }

        string logfilePath = Path.Combine(LogPath, $"ChatManager_{LogSession}.log");
        LogWriter = new(logfilePath, true);
        Write(LogEventEnum.Info, ProgramClassEnum.Logging, "Logging started");
        Write(LogEventEnum.Info, ProgramClassEnum.Logging, $"Application version is: {Application.ProductVersion}");

        // Add Timer to write any second all open entries in the log
        Timer = new(10000);
        Timer.Elapsed += TimerElapsed;
        Timer.Start();

        LogfilesCleaning();
    }

    private static void LogfilesCleaning()
    {
        Write(LogEventEnum.Method, ProgramClassEnum.Logging, "LogfilesCleaning entered");

        string logPath = GetSetSettings.GetLogPath;

        DateTime dateSevenDaysAgo = DateTime.Today.AddDays(-7);

        string[] logFiles = Directory.GetFiles(logPath);

        int counter = 0;

        foreach (string file in logFiles)
        {
            if (File.GetLastWriteTime(file) < dateSevenDaysAgo)
            {
                File.Delete(file);
                counter++;
            }
        }

        Write(LogEventEnum.Info, ProgramClassEnum.Logging, $"{counter} logs deleted");
    }

    private static void TimerElapsed(object? sender, ElapsedEventArgs e)
    {
        if (LogWriter != null)
        {
            lock (LogWriter)
            {
                LogWriter.Flush();
                LogWriter.Close();
                LogWriter = new(Path.Combine(LogPath, $"ChatManager_{LogSession}.log"), true);
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

        if (LogWriter != null)
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
        if (LogWriter != null)
        {
            Timer?.Stop();
            Write(LogEventEnum.Info, ProgramClassEnum.Logging, "Logging stopped");
            LogWriter.Flush();
            LogWriter.Close();
        }
    }
}
