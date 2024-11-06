using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatManager.Enums;

namespace ChatManager.Services;

internal static partial class Updater
{
    private static readonly Version CurrentVersion = new(Application.ProductVersion);
    private static Version? OnlineVersion;
    private static string UpdateURL = "https://github.com/Zagrthos/SWTOR-ChatColorManager/releases/";
    private static string UpdateName = "SWTOR-ChatManager-";
    private static string UpdatePath = string.Empty;
    private static long? TotalBytesToDownload = 0;

    internal static string GetUpdateDownloadText { get; private set; } = string.Empty;

    internal static async Task CheckForUpdateIntervalAsync()
    {
        string updateInterval = GetSetSettings.GetUpdateInterval;
        bool updateSearch = false;
        DateTime lastCheck = GetSetSettings.GetLastUpdateCheck;
        DateTime today = DateTime.Today;
        TimeSpan difference = today - lastCheck;

        if (updateInterval == nameof(UpdateEnum.OnStartup))
        {
            updateSearch = true;
        }
        else if (updateInterval == nameof(UpdateEnum.Daily))
        {
            if (difference.Days >= 1)
            {
                updateSearch = true;
            }
        }
        else if (updateInterval == nameof(UpdateEnum.Weekly))
        {
            if (difference.Days >= 7)
            {
                updateSearch = true;
            }
        }
        else
        {
            Logging.Write(LogEventEnum.Error, ProgramClassEnum.Updater, "updateInterval Setting not set!");
            ShowMessageBox.ShowBug();
        }

        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"updateInterval set to: {updateInterval}");
        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"lastCheck set to: {lastCheck}");
        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"today set to: {today}");
        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"difference set to: {difference}");
        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"updateSearch set to: {updateSearch}");

        if (!updateSearch)
        {
            return;
        }

        await CheckForUpdatesAsync();
    }

    internal static async Task CheckForUpdatesAsync(bool fromUser = false)
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.Updater, "CheckForUpdatesAsync entered");

        OnlineVersion = await WebRequests.GetVersionAsync(new(GetSetSettings.GetUpdateCheckURL));

        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"onlineVersion is: {OnlineVersion}");

        if (OnlineVersion > CurrentVersion)
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.Updater, "Update is available!");

            long fileSize = await GetFileSizeAsync();

            if (ShowMessageBox.ShowUpdate(OnlineVersion.ToString(), Converter.ConvertByteToMegabyte(fileSize)))
            {
                if (GetSetSettings.GetUpdateDownload)
                {
                    Logging.Write(LogEventEnum.Info, ProgramClassEnum.Updater, "Manual download initiated");
                    OpenWindows.OpenLinksInBrowser($"{UpdateURL}/tag/v{OnlineVersion}/");
                }
                else
                {
                    Logging.Write(LogEventEnum.Info, ProgramClassEnum.Updater, "Background download initiated");
                    await DownloadUpdateAsync();
                }
            }
        }
        else
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.Updater, "No Update available!");

            if (fromUser)
            {
                Localization localization = new(GetSetSettings.GetCurrentLocale);
                ShowMessageBox.Show(localization.GetString(LocalizationEnum.MessageBoxNoUpdate), localization.GetString(LocalizationEnum.Update_IsNotAvailable));
            }
        }

        // Save the date of the last update Check but only if the user has NOT initiated it
        if (GetSetSettings.GetUpdateInterval == nameof(UpdateEnum.OnStartup) || fromUser)
        {
            return;
        }

        GetSetSettings.SaveSettings(SettingsEnum.lastUpdateCheck, DateTime.Today);
        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"Last Update Check: {DateTime.Today}");
    }

    private static async Task<long> GetFileSizeAsync()
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.Updater, "GetFileSizeAsync entered");

        if (OnlineVersion is not null && !UpdateName.Contains(OnlineVersion.ToString(), StringComparison.OrdinalIgnoreCase))
        {
            if (UpdateName.Contains(".exe", StringComparison.OrdinalIgnoreCase))
            {
                UpdateName = ReplaceVersionNumber().Replace(UpdateName, OnlineVersion.ToString());
                Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"updateName set to: {UpdateName}");

                UpdateURL = ReplaceVersionNumber().Replace(UpdateURL, OnlineVersion.ToString());
                Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"updateURL set to: {UpdateURL}");
            }

            UpdateName += $"v{OnlineVersion}.exe";
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"updateName set to: {UpdateName}");

            UpdateURL += $"download/v{OnlineVersion}/{UpdateName}";
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"updateURL set to: {UpdateURL}");
        }

        // Count the length of the to download file
        TotalBytesToDownload = await WebRequests.GetLongAsync(new(UpdateURL));

        return TotalBytesToDownload ?? 0;
    }

    private static async Task DownloadUpdateAsync()
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.Updater, "DownloadUpdateAsync entered");

        HttpResponseMessage responseMessage = await WebRequests.GetResponseMessageAsync(new(UpdateURL));

        if (!responseMessage.IsSuccessStatusCode)
        {
            ShowMessageBox.ShowBug();
            return;
        }

        UpdatePath = Path.Combine(Path.GetTempPath(), UpdateName);
        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"Download Path: {UpdatePath}");

        Localization localization = new(GetSetSettings.GetCurrentLocale);
        GetUpdateDownloadText = localization.GetString(LocalizationEnum.downloadProgressToolStripMenuItem);

        // Download the file and then log the progress
        await using (FileStream filestream = new(UpdatePath, FileMode.Create, FileAccess.Write, FileShare.None))
        await using (Stream stream = await responseMessage.Content.ReadAsStreamAsync())
        {
            byte[] buffer = new byte[65536];
            long totalBytesRead = 0;
            int bytesRead;
            double lastLoggedPercent = 0;

            while ((bytesRead = await stream.ReadAsync(buffer)) != 0)
            {
                await filestream.WriteAsync(buffer.AsMemory(0, bytesRead));
                totalBytesRead += bytesRead;
                double percent = (TotalBytesToDownload.HasValue) ? (double)totalBytesRead / TotalBytesToDownload.Value * 100 : -1;

                if (percent > -1)
                {
                    // Remove all ,xx
                    percent = Math.Round(percent);

                    // But only log if the progress has changed 1%
                    if (Math.Abs(percent - lastLoggedPercent) >= 1)
                    {
                        DownloadProgressReporter.OnDownloadProgressChanged(percent);
                        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"progress: {percent}%");
                        lastLoggedPercent = percent;
                    }
                }
            }
        }

        responseMessage.Dispose();

        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"Update downloaded to: {UpdatePath}");

        if (await VerifyUpdateHashAsync(UpdatePath, OnlineVersion!.ToString()))
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.Updater, "Application update started!");
            ShowMessageBox.Show(localization.GetString(LocalizationEnum.MessageBoxUpdate), localization.GetString(LocalizationEnum.Update_IsInstallReady));
            InstallUpdate();
        }
        else
        {
            Logging.Write(LogEventEnum.Warning, ProgramClassEnum.Updater, "Updating not started because of incorrect hashes!");
            ShowMessageBox.ShowBug();
        }
    }

    private static async Task<bool> VerifyUpdateHashAsync(string filePath, string version)
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.Updater, "VerifyUpdateHashAsync entered");

        Logging.Write(LogEventEnum.Info, ProgramClassEnum.Updater, "Downloading hash initiated");

        string hashURL = GetSetSettings.GetHashCheckURL.Replace("VERSION", version, StringComparison.OrdinalIgnoreCase);
        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"hashURL is: {hashURL}");

        string onlineHash = await WebRequests.GetStringAsync(new(hashURL));
        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"onlineHash is: {onlineHash}");

        string localHash = string.Empty;
        await using (FileStream stream = File.OpenRead(filePath))
        {
            using var sha256 = SHA256.Create();
            byte[] byteHash = await sha256.ComputeHashAsync(stream);
            localHash = Convert.ToHexString(byteHash);

            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.Updater, $"localHash is: {localHash}");
        }

        if (!string.IsNullOrWhiteSpace(onlineHash) && !string.IsNullOrWhiteSpace(localHash))
        {
            if (onlineHash == localHash)
            {
                Logging.Write(LogEventEnum.Info, ProgramClassEnum.Updater, "Hashes are equal!");
                return true;
            }

            Logging.Write(LogEventEnum.Warning, ProgramClassEnum.Updater, "Hashes are not equal!");

            return false;
        }

        Logging.Write(LogEventEnum.Warning, ProgramClassEnum.Updater, "Hashes are empty!");

        return false;
    }

    private static void InstallUpdate()
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.Updater, "InstallUpdate entered");

        GetSetSettings.SaveSettings(SettingsEnum.lastUpdatePath, UpdatePath);

        OpenWindows.OpenProcess(UpdatePath);

        Logging.Dispose();

        Environment.Exit(0);
    }

    [GeneratedRegex("v\\d+\\.\\d+\\.\\d+")]
    private static partial Regex ReplaceVersionNumber();
}

internal sealed class DownloadProgressEventArgs : EventArgs
{
    private readonly double Progress;

    internal double GetDownloadProgress() => Progress;
    internal DownloadProgressEventArgs(double progress) => Progress = progress;
}

internal static class DownloadProgressReporter
{
    internal static event EventHandler<DownloadProgressEventArgs>? DownloadProgressChanged;

    internal static void OnDownloadProgressChanged(double progress) => DownloadProgressChanged?.Invoke(null, new DownloadProgressEventArgs(progress));
}
