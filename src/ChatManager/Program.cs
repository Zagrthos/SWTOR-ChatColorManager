using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ChatManager.Enums;
using ChatManager.Services;

namespace ChatManager;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Needed by WinForms.")]
    private static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        GetSetSettings.InitSettings();
        Logging.Initialize();

        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += Application_ThreadException;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

        if (!string.IsNullOrWhiteSpace(GetSetSettings.GetLastUpdatePath))
        {
            File.Delete(GetSetSettings.GetLastUpdatePath);
            GetSetSettings.SaveSettings(SettingsNames.lastUpdatePath, string.Empty);
        }

        Application.Run(new MainForm());
    }

    private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
    {
        Logging.Write(LogEvent.Error, LogClass.ProgramConfig, "Global Application_ThreadExeption occured!");
        Logging.Write(LogEvent.ExMessage, LogClass.ProgramConfig, e.Exception.Message);
        ShowMessageBox.ShowBug();
    }

    private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        Logging.Write(LogEvent.Error, LogClass.ProgramConfig, "Global CurrentDomain_UnhandledException occured!");
        Logging.Write(LogEvent.ExMessage, LogClass.ProgramConfig, e.ExceptionObject.ToString()!);
        ShowMessageBox.ShowBug();
    }
}
