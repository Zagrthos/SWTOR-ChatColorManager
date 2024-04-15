using ChatManager.Enums;
using ChatManager.Services;
using System;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows.Forms;

namespace ChatManager.Forms;

public partial class TextViewerForm : Form
{
    public TextViewerForm(bool isChangelog = false)
    {
        InitializeComponent();
        Localize(isChangelog);
    }

    private async void Localize(bool isChangelog = false)
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.TextViewerForm, "Localize entered");

        Localization localization = new(GetSetSettings.GetCurrentLocale);

        if (isChangelog)
        {
            if (!Checks.CheckForInternetConnection(true))
            {
                return;
            }
            
            Text = localization.GetString(LocalizationEnum.ChangelogFormName);
            lblLicencesHead.Text = localization.GetString(LocalizationEnum.ChangelogLabelName);
            
            HttpResponseMessage response = await WebRequests.GetResponseMessageAsync($"{GetSetSettings.GetReleaseApiPath}v{await WebRequests.GetVersionAsync(GetSetSettings.GetUpdateCheckURL)}", $"{Application.ProductName}/{Application.ProductVersion}");

            if (!response.IsSuccessStatusCode)
            {
                ShowMessageBox.ShowBug();
                return;
            }

            string content = await response.Content.ReadAsStringAsync();
            response.Dispose();

            using JsonDocument jsonDoc = JsonDocument.Parse(content);

            string? body = jsonDoc.RootElement.GetProperty("body").GetString();

            if(!string.IsNullOrEmpty(body))
            {
                rtbLicences.Text = body;
            }
            else
            {
                rtbLicences.Text = localization.GetString(LocalizationEnum.ChangelogTryAgainLater);
            }

            jsonDoc.Dispose();
        }
        else
        {
            Text = localization.GetString(LocalizationEnum.LicenceFormName);
            lblLicencesHead.Text = localization.GetString(LocalizationEnum.LicenceLabelName);
            rtbLicences.Text = GetSetSettings.GetLicences;
        }
    }

    private void RtbLicences_LinkClicked(object sender, LinkClickedEventArgs e)
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.TextViewerForm, "rtbLicencesLinkClicked entered");

        if (!string.IsNullOrEmpty(e.LinkText))
        {
            OpenWindows.OpenLinksInBrowser(e.LinkText);
        }
        else
        {
            Logging.Write(LogEventEnum.Error, ProgramClassEnum.TextViewerForm, "Link is null or empty!");
            ShowMessageBox.ShowBug();
        }
    }

    [DllImport("user32.dll", EntryPoint = "HideCaret")]
    private static extern bool HideCaret(IntPtr hWnd);

    private void RtbLicences_GotFocus(object sender, EventArgs e)
    {
        HideCaret(rtbLicences.Handle);
    }
}
