using ChatManager.Services;

namespace ChatManager
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            Logging.Initialize();

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.Run(new MainForm());
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Logging.Write(LogEvent.Error, ProgramClass.ProgramConfig, "Global Application_ThreadExeption occured!");
            Logging.Write(LogEvent.ExMessage, ProgramClass.ProgramConfig, e.Exception.Message);
            ShowMessageBox.ShowBug();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logging.Write(LogEvent.Error, ProgramClass.ProgramConfig, "Global CurrentDomain_UnhandledException occured!");
            Logging.Write(LogEvent.ExMessage, ProgramClass.ProgramConfig, e.ExceptionObject.ToString()!);
            ShowMessageBox.ShowBug();
        }
    }
}