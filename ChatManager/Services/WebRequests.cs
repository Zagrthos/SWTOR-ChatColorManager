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
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.WebRequests, "GetLongAsync entered");

        long? getLong = 0;

        try
        {
            using HttpResponseMessage response = await Client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            getLong = response.Content.Headers.ContentLength;

            return getLong;
        }
        catch (HttpRequestException ex)
        {
            Logging.Write(LogEventEnum.Error, ProgramClassEnum.WebRequests, "Get long failed!");
            Logging.Write(LogEventEnum.ExMessage, ProgramClassEnum.WebRequests, $"{ex.Message}");

            ShowMessageBox.ShowBug();

            return getLong;
        }
    }

    [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "We still need the object (sadly)")]
    internal static async Task<HttpResponseMessage> GetResponseMessageAsync(Uri url, string headers = "")
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.WebRequests, "GetResponseMessageAsync entered");

        HttpResponseMessage response = new();

        AddHeaders(headers);

        try
        {
            response = await Client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

            return response;
        }
        catch (HttpRequestException ex)
        {
            Logging.Write(LogEventEnum.Error, ProgramClassEnum.WebRequests, "Get string failed!");
            Logging.Write(LogEventEnum.ExMessage, ProgramClassEnum.WebRequests, $"{ex.Message}");

            ShowMessageBox.ShowBug();

            response.StatusCode = HttpStatusCode.NotFound;

            return response;
        }
    }

    internal static async Task<string> GetStringAsync(Uri url, string headers = "")
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.WebRequests, "GetStringAsync entered");

        string getString = string.Empty;

        AddHeaders(headers);

        try
        {
            getString = await Client.GetStringAsync(url);

            return getString;
        }
        catch (HttpRequestException ex)
        {
            Logging.Write(LogEventEnum.Error, ProgramClassEnum.WebRequests, "Get string failed!");
            Logging.Write(LogEventEnum.ExMessage, ProgramClassEnum.WebRequests, $"{ex.Message}");

            ShowMessageBox.ShowBug();

            return getString;
        }
    }

    internal static async Task<Version> GetVersionAsync(Uri url)
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.WebRequests, "GetVersionAsync entered");

        Version getVersion = new(0, 0, 0);

        try
        {
            getVersion = new(await Client.GetStringAsync(url));

            return getVersion;
        }
        catch (HttpRequestException ex)
        {
            Logging.Write(LogEventEnum.Error, ProgramClassEnum.WebRequests, "Get version failed!");
            Logging.Write(LogEventEnum.ExMessage, ProgramClassEnum.WebRequests, $"{ex.Message}");

            ShowMessageBox.ShowBug();

            return getVersion;
        }
    }

    private static void AddHeaders(string headers)
    {
        if (!string.IsNullOrWhiteSpace(headers))
        {
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.UserAgent.ParseAdd(headers);
        }
    }
}
