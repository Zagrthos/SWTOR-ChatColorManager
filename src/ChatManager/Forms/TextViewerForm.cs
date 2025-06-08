using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows.Forms;
using ChatManager.Enums;
using ChatManager.Services;

namespace ChatManager.Forms;

internal sealed partial class TextViewerForm : Form
{
    internal TextViewerForm(bool isChangelog = false)
    {
        InitializeComponent();
        Localize(isChangelog);
    }

    private async void Localize(bool isChangelog = false)
    {
        Logging.Write(LogEvent.Method, LogClass.TextViewerForm, "Localize entered");

        Services.Localization localization = new(GetSetSettings.GetCurrentLocale);

        if (isChangelog)
        {
            if (!Checks.CheckForInternetConnection(true))
                return;

            Text = localization.GetString(Enums.LocalizationStrings.ChangelogFormName);
            lblLicencesHead.Text = localization.GetString(Enums.LocalizationStrings.ChangelogLabelName);

            string content = await WebRequests.GetStringAsync(new($"{GetSetSettings.GetReleaseApiPath}v{await WebRequests.GetVersionAsync(new(GetSetSettings.GetUpdateCheckURL))}"), $"{Application.ProductName}/{Application.ProductVersion}");
            using JsonDocument jsonDoc = JsonDocument.Parse(content);

            string? body = jsonDoc.RootElement.GetProperty("body").GetString();

            rtbLicences.Text = (!string.IsNullOrWhiteSpace(body)) ? body : localization.GetString(Enums.LocalizationStrings.ChangelogTryAgainLater);

            jsonDoc.Dispose();
        }
        else
        {
            Text = localization.GetString(Enums.LocalizationStrings.LicenceFormName);
            lblLicencesHead.Text = localization.GetString(Enums.LocalizationStrings.LicenceLabelName);
            rtbLicences.Text = GetSetSettings.GetLicences;
        }
    }

    private void RtbLicences_LinkClicked(object sender, LinkClickedEventArgs e)
    {
        Logging.Write(LogEvent.Method, LogClass.TextViewerForm, "rtbLicencesLinkClicked entered");

        if (!string.IsNullOrWhiteSpace(e.LinkText))
        {
            OpenWindows.OpenLinksInBrowser(e.LinkText);
        }
        else
        {
            Logging.Write(LogEvent.Error, LogClass.TextViewerForm, "Link is null or empty!");
            ShowMessageBox.ShowBug();
        }
    }

    [SuppressMessage("Interoperability", "SYSLIB1054:Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time", Justification = "No unsafe Code.")]
    [DllImport("user32.dll", EntryPoint = "HideCaret", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    private static extern bool HideCaret(IntPtr hWnd);

    private void RtbLicences_GotFocus(object sender, EventArgs e) => HideCaret(rtbLicences.Handle);
}
