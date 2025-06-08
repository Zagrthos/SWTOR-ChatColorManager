using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ChatManager.Enums;

namespace ChatManager.Services;

internal static class WebRequests
{
    private static readonly HttpClient Client = new();

    internal static async Task<long?> GetLongAsync(Uri url)
    {
        Logging.Write(LogEvent.Method, LogClass.WebRequests, "GetLongAsync entered");

        long? getLong = 0;

        try
        {
            using HttpResponseMessage response = await Client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

            return response.Content.Headers.ContentLength;
        }
        catch (HttpRequestException ex)
        {
            Logging.Write(LogEvent.Error, LogClass.WebRequests, "Get long failed!");
            Logging.Write(LogEvent.ExMessage, LogClass.WebRequests, $"{ex.Message}");

            ShowMessageBox.ShowBug();

            return getLong;
        }
    }

    [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "We still need the object (sadly)")]
    internal static async Task<HttpResponseMessage> GetResponseMessageAsync(Uri url, string headers = "")
    {
        Logging.Write(LogEvent.Method, LogClass.WebRequests, "GetResponseMessageAsync entered");

        HttpResponseMessage response = new();

        AddHeaders(headers);

        try
        {
            return await Client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
        }
        catch (HttpRequestException ex)
        {
            Logging.Write(LogEvent.Error, LogClass.WebRequests, "Get string failed!");
            Logging.Write(LogEvent.ExMessage, LogClass.WebRequests, $"{ex.Message}");

            ShowMessageBox.ShowBug();

            response.StatusCode = HttpStatusCode.NotFound;

            return response;
        }
    }

    internal static async Task<string> GetStringAsync(Uri url, string headers = "")
    {
        Logging.Write(LogEvent.Method, LogClass.WebRequests, "GetStringAsync entered");

        string getString = string.Empty;

        AddHeaders(headers);

        try
        {
            return await Client.GetStringAsync(url);
        }
        catch (HttpRequestException ex)
        {
            Logging.Write(LogEvent.Error, LogClass.WebRequests, "Get string failed!");
            Logging.Write(LogEvent.ExMessage, LogClass.WebRequests, $"{ex.Message}");

            ShowMessageBox.ShowBug();

            return getString;
        }
    }

    internal static async Task<Version> GetVersionAsync(Uri url)
    {
        Logging.Write(LogEvent.Method, LogClass.WebRequests, "GetVersionAsync entered");

        Version getVersion = new(0, 0, 0);

        try
        {
            return new(await Client.GetStringAsync(url));
        }
        catch (HttpRequestException ex)
        {
            Logging.Write(LogEvent.Error, LogClass.WebRequests, "Get version failed!");
            Logging.Write(LogEvent.ExMessage, LogClass.WebRequests, $"{ex.Message}");

            ShowMessageBox.ShowBug();

            return getVersion;
        }
    }

    private static void AddHeaders(string headers)
    {
        if (string.IsNullOrWhiteSpace(headers))
            return;

        Client.DefaultRequestHeaders.Clear();
        Client.DefaultRequestHeaders.UserAgent.ParseAdd(headers);
    }
}
