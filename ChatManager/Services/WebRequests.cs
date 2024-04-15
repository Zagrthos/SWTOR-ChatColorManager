using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ChatManager.Enums;

namespace ChatManager.Services;

internal static class WebRequests
{
    internal static async Task<long?> GetLongAsync(string url)
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.WebRequests, "GetLongAsync entered");

        long? getLong = 0;

        HttpClient client = new();
        Logging.Write(LogEventEnum.Info, ProgramClassEnum.WebRequests, "HttpClient created");

        try
        {
            HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            getLong = response.Content.Headers.ContentLength;

            Logging.Write(LogEventEnum.Info, ProgramClassEnum.WebRequests, "HttpClient disposed!");
            client.Dispose();
            response.Dispose();

            return getLong;
        }
        catch (HttpRequestException ex)
        {
            Logging.Write(LogEventEnum.Error, ProgramClassEnum.WebRequests, "Get long failed!");
            Logging.Write(LogEventEnum.ExMessage, ProgramClassEnum.WebRequests, $"{ex.Message}");

            Logging.Write(LogEventEnum.Info, ProgramClassEnum.WebRequests, "HttpClient disposed!");
            client.Dispose();

            ShowMessageBox.ShowBug();

            return getLong;
        }
    }

    internal static async Task<HttpResponseMessage> GetResponseMessageAsync(string url, string headers = "")
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.WebRequests, "GetResponseMessageAsync entered");

        HttpResponseMessage response = new();

        HttpClient client = new();
        Logging.Write(LogEventEnum.Info, ProgramClassEnum.WebRequests, "HttpClient created");

        if (!string.IsNullOrEmpty(headers))
        {
            client.DefaultRequestHeaders.UserAgent.ParseAdd(headers);
        }

        try
        {
            response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

            Logging.Write(LogEventEnum.Info, ProgramClassEnum.WebRequests, "HttpClient disposed!");
            client.Dispose();

            return response;
        }
        catch (HttpRequestException ex)
        {
            Logging.Write(LogEventEnum.Error, ProgramClassEnum.WebRequests, "Get string failed!");
            Logging.Write(LogEventEnum.ExMessage, ProgramClassEnum.WebRequests, $"{ex.Message}");

            Logging.Write(LogEventEnum.Info, ProgramClassEnum.WebRequests, "HttpClient disposed!");
            client.Dispose();

            ShowMessageBox.ShowBug();

            response.StatusCode = HttpStatusCode.NotFound;

            return response;
        }
    }

    internal static async Task<string> GetStringAsync(string url)
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.WebRequests, "GetStringAsync entered");

        string getString = string.Empty;

        HttpClient client = new();
        Logging.Write(LogEventEnum.Info, ProgramClassEnum.WebRequests, "HttpClient created");

        try
        {
            getString = await client.GetStringAsync(url);

            Logging.Write(LogEventEnum.Info, ProgramClassEnum.WebRequests, "HttpClient disposed!");
            client.Dispose();

            return getString;
        }
        catch (HttpRequestException ex)
        {
            Logging.Write(LogEventEnum.Error, ProgramClassEnum.WebRequests, "Get string failed!");
            Logging.Write(LogEventEnum.ExMessage, ProgramClassEnum.WebRequests, $"{ex.Message}");

            Logging.Write(LogEventEnum.Info, ProgramClassEnum.WebRequests, "HttpClient disposed!");
            client.Dispose();

            ShowMessageBox.ShowBug();

            return getString;
        }
    }

    internal static async Task<Version> GetVersionAsync(string url)
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.WebRequests, "GetVersionAsync entered");

        Version getVersion = new(0, 0, 0);

        HttpClient client = new();
        Logging.Write(LogEventEnum.Info, ProgramClassEnum.WebRequests, "HttpClient created");

        try
        {
            getVersion = new(await client.GetStringAsync(url));

            Logging.Write(LogEventEnum.Info, ProgramClassEnum.WebRequests, "HttpClient disposed!");
            client.Dispose();

            return getVersion;
        }
        catch (HttpRequestException ex)
        {
            Logging.Write(LogEventEnum.Error, ProgramClassEnum.WebRequests, "Get version failed!");
            Logging.Write(LogEventEnum.ExMessage, ProgramClassEnum.WebRequests, $"{ex.Message}");

            Logging.Write(LogEventEnum.Info, ProgramClassEnum.WebRequests, "HttpClient disposed!");
            client.Dispose();

            ShowMessageBox.ShowBug();

            return getVersion;
        }
    }
}
