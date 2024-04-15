using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ChatManager.Enums;
using ChatManager.Services;

namespace ChatManager.Forms;

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
                if (titleAttribute.Title != string.Empty)
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
                return string.Empty;
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
                return string.Empty;
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
                return string.Empty;
            }

            return ((AssemblyCompanyAttribute)attributes[0]).Company;
        }
    }

    #endregion Assembly Attribute Accessors

    private void LicencesButton_Click(object sender, EventArgs e)
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.AboutForm, "LicensesButtonClick entered");
        OpenWindows.OpenTextViewer();
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

        if (rtb.SelectionLength <= 0)
        {
            return;
        }

        rtb.SelectionLength = 0;
    }

    [DllImport("user32.dll", EntryPoint = "HideCaret")]
    [SuppressMessage("Interoperability", "SYSLIB1054:Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time", Justification = "No unsafe Code.")]
    private static extern bool HideCaret(IntPtr hWnd);

    private void RtbCompany_GotFocus(object sender, EventArgs e) => HideCaret(rtbCompany.Handle);
}
