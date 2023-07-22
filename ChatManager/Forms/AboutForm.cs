using ChatManager.Enums;
using ChatManager.Services;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ChatManager.Forms
{
    internal partial class AboutForm : Form
    {
        internal AboutForm()
        {
            InitializeComponent();
            Text = string.Format("About {0}", AssemblyTitle);
            labelProductName.Text = AssemblyProduct;
            labelVersion.Text = string.Format("Version {0}", ProductVersion);
            labelCopyright.Text = AssemblyCopyright;
            SetRichTextBox(AssemblyCompany);
            Localize();
        }

        #region Assembly Attribute Accessors

        internal static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);
            }
        }

        internal static string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        internal static string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        internal static string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        #endregion Assembly Attribute Accessors

        private void LicencesButton_Click(object sender, EventArgs e)
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.AboutForm, "LicensesButtonClick entered");
            OpenWindows.OpenLicences();
        }

        private void GitHubLinkButton_Click(object sender, EventArgs e)
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.AboutForm, "GitHubLinkButtonClick entered");
            OpenWindows.OpenLinksInBrowser(GetSetSettings.GetGitHubPath);
        }

        private void Localize()
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.AboutForm, "Localize entered");

            Localization localization = new(GetSetSettings.GetCurrentLocale);

            Text = localization.GetString(Name);
            licencesButton.Text = localization.GetString(licencesButton.Name);
            gitHubLinkButton.Text = localization.GetString(gitHubLinkButton.Name);
        }

        private void SetRichTextBox(string company)
        {
            rtbCompany.Text = string.Empty;

            rtbCompany.SelectionColor = Color.Black;
            rtbCompany.AppendText("Made with ");
            rtbCompany.SelectionColor = Color.Red;
            rtbCompany.AppendText("\u2764");
            rtbCompany.SelectionColor = Color.Black;
            rtbCompany.AppendText($" by {company}");
        }

        private void RtbCompany_SelectionChanged(object sender, EventArgs e)
        {
            RichTextBox rtb = (RichTextBox)sender;

            if (rtb.SelectionLength > 0)
            {
                rtb.SelectionLength = 0;
            }
        }

        [DllImport("user32.dll", EntryPoint = "HideCaret")]
        private static extern bool HideCaret(IntPtr hWnd);

        private void RtbCompany_GotFocus(object sender, EventArgs e)
        {
            HideCaret(rtbCompany.Handle);
        }
    }
}