using ChatManager.Enums;
using ChatManager.Services;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ChatManager.Forms
{
    public partial class LicencesForm : Form
    {
        //public LicencesForm(bool isChangelog)
        //{
        //    InitializeComponent();
        //    Localize(isChangelog);
        //}
        
        public LicencesForm()
        {
            InitializeComponent();
            Localize();
        }

        //private void Localize(bool isChangelog)
        //{
        //    Logging.Write(LogEventEnum.Method, ProgramClassEnum.LicencesForm, "Localize entered");

        //    Localization localization = new(GetSetSettings.GetCurrentLocale);

        //    // TODO: Finish rewriting of dynamic change between
        //    // Licences and What's new page
        //    if (isChangelog)
        //    {
        //        Text = localization.GetString();
        //        lblLicencesHead.Text = localization.GetString();
        //        rtbLicences.Text = 
        //    }
        //    else
        //    {
        //        Text = localization.GetString();
        //        lblLicencesHead.Text = localization.GetString();
        //        rtbLicences.Text = GetSetSettings.GetLicences;
        //    }

        //    Text = localization.GetString(Name);
        //    lblLicencesHead.Text = localization.GetString(lblLicencesHead.Name);

        //    rtbLicences.Text = GetSetSettings.GetLicences;
        //}
        
        private void Localize()
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.LicencesForm, "Localize entered");

            Localization localization = new(GetSetSettings.GetCurrentLocale);

            Text = localization.GetString(Name);
            lblLicencesHead.Text = localization.GetString(lblLicencesHead.Name);

            rtbLicences.Text = GetSetSettings.GetLicences;
        }

        private void RtbLicences_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.LicencesForm, "rtbLicencesLinkClicked entered");

            if (!string.IsNullOrEmpty(e.LinkText))
            {
                OpenWindows.OpenLinksInBrowser(e.LinkText);
            }
            else
            {
                Logging.Write(LogEventEnum.Error, ProgramClassEnum.LicencesForm, "Link is null or empty!");
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
}
