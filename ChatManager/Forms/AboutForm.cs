using ChatManager.Services;
using System.Reflection;

namespace ChatManager.Forms
{
    partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            Text = string.Format("About {0}", AssemblyTitle);
            labelProductName.Text = AssemblyProduct;
            labelVersion.Text = string.Format("Version {0}", ProductVersion);
            labelCopyright.Text = AssemblyCopyright;
            labelCompanyName.Text = AssemblyCompany;
            Localize(GetSetSettings.GetCurrentLocale);
        }

        #region Assembly Attribute Accessors

        public static string AssemblyTitle
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

        public static string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version!.ToString();

        public static string AssemblyProduct
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

        public static string AssemblyCopyright
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

        public static string AssemblyCompany
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
        #endregion

        private void CopyrightButton_Click(object sender, EventArgs e)
        {
            Logging.Write(LogEvent.Method, ProgramClass.AboutForm, "CopyrightButtonClick entered");
            OpenWindows.OpenLinksInBrowser(GetSetSettings.GetAboutPictureLink);
        }

        private void Localize(string locale)
        {
            Logging.Write(LogEvent.Method, ProgramClass.AboutForm, "Localize entered");

            Localization localization = new(locale);

            Text = localization.GetString(Name);
            Logging.Write(LogEvent.Variable, ProgramClass.AboutForm, $"FormText set to {Text}");

            copyrightButton.Text = localization.GetString(copyrightButton.Name);
            Logging.Write(LogEvent.Variable, ProgramClass.AboutForm, $"copyrightButton set to {copyrightButton.Text}");
        }
    }
}
