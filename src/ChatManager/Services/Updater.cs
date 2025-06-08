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

        if (updateInterval == nameof(UpdateInterval.OnStartup))
        {
            updateSearch = true;
        }
        else if (updateInterval == nameof(UpdateInterval.Daily))
        {
            if (difference.Days >= 1)
                updateSearch = true;
        }
        else if (updateInterval == nameof(UpdateInterval.Weekly))
        {
            if (difference.Days >= 7)
                updateSearch = true;
        }
        else
        {
            Logging.Write(LogEvent.Error, LogClass.Updater, "updateInterval Setting not set!");
            ShowMessageBox.ShowBug();
        }

        Logging.Write(LogEvent.Variable, LogClass.Updater, $"updateInterval set to: {updateInterval}");
        Logging.Write(LogEvent.Variable, LogClass.Updater, $"lastCheck set to: {lastCheck}");
        Logging.Write(LogEvent.Variable, LogClass.Updater, $"today set to: {today}");
        Logging.Write(LogEvent.Variable, LogClass.Updater, $"difference set to: {difference}");
        Logging.Write(LogEvent.Variable, LogClass.Updater, $"updateSearch set to: {updateSearch}");

        if (!updateSearch)
            return;

        await CheckForUpdatesAsync();
    }

    internal static async Task CheckForUpdatesAsync(bool fromUser = false)
    {
        Logging.Write(LogEvent.Method, LogClass.Updater, "CheckForUpdatesAsync entered");

        OnlineVersion = await WebRequests.GetVersionAsync(new(GetSetSettings.GetUpdateCheckURL));

        Logging.Write(LogEvent.Variable, LogClass.Updater, $"onlineVersion is: {OnlineVersion}");

        if (OnlineVersion > CurrentVersion)
        {
            Logging.Write(LogEvent.Info, LogClass.Updater, "Update is available!");

            long fileSize = await GetFileSizeAsync();

            if (ShowMessageBox.ShowUpdate(OnlineVersion.ToString(), Converter.ConvertByteToMegabyte(fileSize)))
            {
                if (GetSetSettings.GetUpdateDownload)
                {
                    Logging.Write(LogEvent.Info, LogClass.Updater, "Manual download initiated");
                    OpenWindows.OpenLinksInBrowser($"{UpdateURL}/tag/v{OnlineVersion}/");
                }
                else
                {
                    Logging.Write(LogEvent.Info, LogClass.Updater, "Background download initiated");
                    await DownloadUpdateAsync();
                }
            }
        }
        else
        {
            Logging.Write(LogEvent.Info, LogClass.Updater, "No Update available!");

            if (fromUser)
            {
                Localization localization = new(GetSetSettings.GetCurrentLocale);
                ShowMessageBox.Show(localization.GetString(Enums.LocalizationStrings.MessageBoxNoUpdate), localization.GetString(Enums.LocalizationStrings.Update_IsNotAvailable));
            }
        }

        // Save the date of the last update Check but only if the user has NOT initiated it
        if (GetSetSettings.GetUpdateInterval == nameof(UpdateInterval.OnStartup) || fromUser)
            return;

        GetSetSettings.SaveSettings(SettingsNames.lastUpdateCheck, DateTime.Today);
        Logging.Write(LogEvent.Variable, LogClass.Updater, $"Last Update Check: {DateTime.Today}");
    }

    private static async Task<long> GetFileSizeAsync()
    {
        Logging.Write(LogEvent.Method, LogClass.Updater, "GetFileSizeAsync entered");

        if (OnlineVersion is not null && !UpdateName.Contains(OnlineVersion.ToString(), StringComparison.OrdinalIgnoreCase))
        {
            if (UpdateName.Contains(".exe", StringComparison.OrdinalIgnoreCase))
            {
                UpdateName = ReplaceVersionNumber().Replace(UpdateName, OnlineVersion.ToString());
                Logging.Write(LogEvent.Variable, LogClass.Updater, $"updateName set to: {UpdateName}");

                UpdateURL = ReplaceVersionNumber().Replace(UpdateURL, OnlineVersion.ToString());
                Logging.Write(LogEvent.Variable, LogClass.Updater, $"updateURL set to: {UpdateURL}");
            }

            UpdateName += $"v{OnlineVersion}.exe";
            Logging.Write(LogEvent.Variable, LogClass.Updater, $"updateName set to: {UpdateName}");

            UpdateURL += $"download/v{OnlineVersion}/{UpdateName}";
            Logging.Write(LogEvent.Variable, LogClass.Updater, $"updateURL set to: {UpdateURL}");
        }

        // Count the length of the to download file
        TotalBytesToDownload = await WebRequests.GetLongAsync(new(UpdateURL));

        return TotalBytesToDownload ?? 0;
    }

    private static async Task DownloadUpdateAsync()
    {
        Logging.Write(LogEvent.Method, LogClass.Updater, "DownloadUpdateAsync entered");

        HttpResponseMessage responseMessage = await WebRequests.GetResponseMessageAsync(new(UpdateURL));

        if (!responseMessage.IsSuccessStatusCode)
        {
            ShowMessageBox.ShowBug();
            return;
        }

        UpdatePath = Path.Combine(Path.GetTempPath(), UpdateName);
        Logging.Write(LogEvent.Variable, LogClass.Updater, $"Download Path: {UpdatePath}");

        Localization localization = new(GetSetSettings.GetCurrentLocale);
        GetUpdateDownloadText = localization.GetString(Enums.LocalizationStrings.downloadProgressToolStripMenuItem);

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
                        Logging.Write(LogEvent.Variable, LogClass.Updater, $"progress: {percent}%");
                        lastLoggedPercent = percent;
                    }
                }
            }
        }

        responseMessage.Dispose();

        Logging.Write(LogEvent.Variable, LogClass.Updater, $"Update downloaded to: {UpdatePath}");

        if (await VerifyUpdateHashAsync(UpdatePath, OnlineVersion!.ToString()))
        {
            Logging.Write(LogEvent.Info, LogClass.Updater, "Application update started!");
            ShowMessageBox.Show(localization.GetString(Enums.LocalizationStrings.MessageBoxUpdate), localization.GetString(Enums.LocalizationStrings.Update_IsInstallReady));
            InstallUpdate();
        }
        else
        {
            Logging.Write(LogEvent.Warning, LogClass.Updater, "Updating not started because of incorrect hashes!");
            ShowMessageBox.ShowBug();
        }
    }

    private static async Task<bool> VerifyUpdateHashAsync(string filePath, string version)
    {
        Logging.Write(LogEvent.Method, LogClass.Updater, "VerifyUpdateHashAsync entered");

        Logging.Write(LogEvent.Info, LogClass.Updater, "Downloading hash initiated");

        string hashURL = GetSetSettings.GetHashCheckURL.Replace("VERSION", version, StringComparison.OrdinalIgnoreCase);
        Logging.Write(LogEvent.Variable, LogClass.Updater, $"hashURL is: {hashURL}");

        string onlineHash = await WebRequests.GetStringAsync(new(hashURL));
        Logging.Write(LogEvent.Variable, LogClass.Updater, $"onlineHash is: {onlineHash}");

        string localHash = string.Empty;
        await using (FileStream stream = File.OpenRead(filePath))
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] byteHash = await sha256.ComputeHashAsync(stream);
            localHash = Convert.ToHexString(byteHash);

            Logging.Write(LogEvent.Variable, LogClass.Updater, $"localHash is: {localHash}");
        }

        if (!string.IsNullOrWhiteSpace(onlineHash) && !string.IsNullOrWhiteSpace(localHash))
        {
            if (onlineHash == localHash)
            {
                Logging.Write(LogEvent.Info, LogClass.Updater, "Hashes are equal!");
                return true;
            }

            Logging.Write(LogEvent.Warning, LogClass.Updater, "Hashes are not equal!");

            return false;
        }

        Logging.Write(LogEvent.Warning, LogClass.Updater, "Hashes are empty!");

        return false;
    }

    private static void InstallUpdate()
    {
        Logging.Write(LogEvent.Method, LogClass.Updater, "InstallUpdate entered");

        GetSetSettings.SaveSettings(SettingsNames.lastUpdatePath, UpdatePath);

        OpenWindows.OpenProcess(UpdatePath);

        Logging.Dispose();

        Environment.Exit(0);
    }

    [GeneratedRegex("v\\d+\\.\\d+\\.\\d+")]
    private static partial Regex ReplaceVersionNumber();
}

internal sealed class DownloadProgressEventArgs : EventArgs
{
    private readonly double _progress;

    internal double GetDownloadProgress() => _progress;

    internal DownloadProgressEventArgs(double progress) => _progress = progress;
}

internal static class DownloadProgressReporter
{
    internal static event EventHandler<DownloadProgressEventArgs>? DownloadProgressChanged;

    internal static void OnDownloadProgressChanged(double progress) => DownloadProgressChanged?.Invoke(null, new DownloadProgressEventArgs(progress));
}
