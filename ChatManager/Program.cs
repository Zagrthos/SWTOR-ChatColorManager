using System;
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
            GetSetSettings.SaveSettings(SettingsEnum.lastUpdatePath, string.Empty);
        }

        Application.Run(new MainForm());
    }

    private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
    {
        Logging.Write(LogEventEnum.Error, ProgramClassEnum.ProgramConfig, "Global Application_ThreadExeption occured!");
        Logging.Write(LogEventEnum.ExMessage, ProgramClassEnum.ProgramConfig, e.Exception.Message);
        ShowMessageBox.ShowBug();
    }

    private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        Logging.Write(LogEventEnum.Error, ProgramClassEnum.ProgramConfig, "Global CurrentDomain_UnhandledException occured!");
        Logging.Write(LogEventEnum.ExMessage, ProgramClassEnum.ProgramConfig, e.ExceptionObject.ToString()!);
        ShowMessageBox.ShowBug();
    }
}
