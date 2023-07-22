using ChatManager.Enums;
using ChatManager.Properties;
using ChatManager.Services;

namespace ChatManager.Forms
{
    public partial class LicencesForm : Form
    {
        public LicencesForm()
        {
            InitializeComponent();
            Localize();
        }

        private void Localize()
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.LicencesForm, "Localize entered");

            Localization localization = new(GetSetSettings.GetCurrentLocale);

            Text = localization.GetString(Name);
            lblLicencesHead.Text = localization.GetString(lblLicencesHead.Name);

            rtbLicences.Text = GetSetSettings.GetLicences;
        }
    }
}
