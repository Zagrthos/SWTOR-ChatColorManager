using ChatManager.Services;

namespace ChatManager.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            autosaveTimerChanged = false;
            languageChanged = false;
        }

        private bool autosaveTimerChanged = false;
        private bool languageChanged = false;
        private bool cbLanguageFalseAlarm = false;
        private bool cbUpdaterIntervallFalseAlarm = false;
        private bool checkBoxFalseAlarm = false;
        private decimal currentAutosaveInterval = 0;

        public bool GetAutosaveTimerChanged => autosaveTimerChanged;
        public bool GetLanguageChanged => languageChanged;

        private void Localize()
        {
            Logging.Write(LogEvent.Method, ProgramClass.SettingsForm, "Localize entered");

            Localization localization = new(GetSetSettings.GetCurrentLocale);

            // Change the Text of the Form
            Text = localization.GetString(Name);

            // Find all Controls of the desired Type and pack them in a Control List
            IEnumerable<Control> GetControls(Control parent, Type type)
            {
                var controls = parent.Controls.Cast<Control>();

                return controls
                    .Where(c => c.GetType() == type)
                    .Concat(controls.SelectMany(c => GetControls(c, type)));
            }

            var groups = GetControls(this, typeof(GroupBox));
            var checkBoxes = GetControls(this, typeof(CheckBox));
            var labels = GetControls(this, typeof(Label));

            foreach (Control control in groups)
            {
                if (control is GroupBox group)
                {
                    group.Text = localization.GetString(group.Name);
                }
            }

            foreach (Control control in checkBoxes)
            {
                if (control is CheckBox checkBox)
                {
                    checkBox.Text = localization.GetString(checkBox.Name);
                }
            }

            foreach (Control control in labels)
            {
                if (control is Label label)
                {
                    label.Text = localization.GetString(label.Name);
                }
            }

            btnResetSettings.Text = localization.GetString(btnResetSettings.Name);

            cbUpdateInterval.Items.Clear();
            cbUpdateInterval.Items.Add(localization.GetString("UpdateIntervalOnStart"));
            cbUpdateInterval.Items.Add(localization.GetString("UpdateIntervalDaily"));
            cbUpdateInterval.Items.Add(localization.GetString("UpdateIntervalWeekly"));
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            Localize();
            AdjustContentOnForm();
        }

        private void AdjustContentOnForm()
        {
            Logging.Write(LogEvent.Method, ProgramClass.SettingsForm, "AdjustContentOnForm entered");

            switch (GetSetSettings.GetCurrentLocale)
            {
                case "de":
                    cbLanguageFalseAlarm = true;
                    cbLanguage.SelectedIndex = 2;
                    break;

                case "en":
                    cbLanguageFalseAlarm = true;
                    cbLanguage.SelectedIndex = 0;
                    break;

                case "fr":
                    cbLanguageFalseAlarm = true;
                    cbLanguage.SelectedIndex = 1;
                    break;

                default:
                    throw new NotImplementedException();
            }

            if (GetSetSettings.GetAutosave)
            {
                checkBoxFalseAlarm = true;
                chbAutosave.Checked = true;
                numberAutosaveInterval.Enabled = true;
                numberAutosaveInterval.Visible = true;
                lblAutosaveInterval.Enabled = true;
                lblAutosaveInterval.Visible = true;
            }
            else
            {
                chbAutosave.Checked = false;
                numberAutosaveInterval.Enabled = false;
                numberAutosaveInterval.Visible = false;
                lblAutosaveInterval.Enabled = false;
                lblAutosaveInterval.Visible = false;
            }

            if (numberAutosaveInterval.Enabled)
            {
                currentAutosaveInterval = GetSetSettings.GetAutosaveInterval / 60000;
                Logging.Write(LogEvent.Variable, ProgramClass.SettingsForm, $"currentAutosaveInterval: {currentAutosaveInterval}");

                if (currentAutosaveInterval == 0)
                {
                    numberAutosaveInterval.Value = 1;
                    SetAutosaveInterval();
                }
                else
                {
                    numberAutosaveInterval.Value = currentAutosaveInterval;
                }
            }

            if (GetSetSettings.GetSaveOnClose)
            {
                chbSaveOnClose.Checked = true;
                chbAutosave.Enabled = false;
            }
            else
            {
                chbSaveOnClose.Checked = false;
                chbAutosave.Enabled = true;
            }

            if (GetSetSettings.GetReloadOnStartup)
            {
                chbReloadOnStartup.Checked = true;
                chbSaveOnClose.Enabled = false;
            }
            else
            {
                chbReloadOnStartup.Checked = false;
                chbSaveOnClose.Enabled = true;
            }

            switch (GetSetSettings.GetUpdateInterval)
            {
                case "OnStartup":
                    cbUpdaterIntervallFalseAlarm = true;
                    cbUpdateInterval.SelectedIndex = 0;
                    break;

                case "Daily":
                    cbUpdaterIntervallFalseAlarm = true;
                    cbUpdateInterval.SelectedIndex = 1;
                    break;

                case "Weekly":
                    cbUpdaterIntervallFalseAlarm = true;
                    cbUpdateInterval.SelectedIndex = 2;
                    break;

                default:
                    throw new NotImplementedException();
            }

            if (GetSetSettings.GetUpdateDownload)
            {
                chbUpdateDownload.Checked = true;
            }
            else
            {
                chbUpdateDownload.Checked = false;
            }
        }

        private void SwitchCurrentLocale()
        {
            if (cbLanguageFalseAlarm)
            {
                cbLanguageFalseAlarm = false;
                return;
            }

            Logging.Write(LogEvent.Method, ProgramClass.SettingsForm, "SwitchCurrentLocale entered");

            string currLocale = GetSetSettings.GetCurrentLocale;
            string newLanguage = cbLanguage.SelectedItem.ToString()!;

            switch (newLanguage)
            {
                case "English":
                    newLanguage = "en";
                    break;

                case "France":
                    newLanguage = "fr";
                    break;

                case "German":
                    newLanguage = "de";
                    break;

                default:
                    throw new NotImplementedException();
            }

            if (currLocale != newLanguage)
            {
                Logging.Write(LogEvent.Setting, ProgramClass.SettingsForm, $"Saving new locale: {newLanguage}");
                GetSetSettings.SaveSettings(Setting.locale, newLanguage);
                int updateIntervall = cbUpdateInterval.SelectedIndex;
                Localize();
                cbUpdateInterval.SelectedIndex = updateIntervall;
                languageChanged = true;
            }
        }

        private async void SwitchUpdateInterval()
        {
            if (cbUpdaterIntervallFalseAlarm)
            {
                cbUpdaterIntervallFalseAlarm = false;
                return;
            }

            Logging.Write(LogEvent.Method, ProgramClass.SettingsForm, "SwitchUpdateInterval entered");

            if (cbUpdateInterval.SelectedIndex != -1)
            {
                if (cbUpdateInterval.SelectedIndex == 0)
                {
                    GetSetSettings.SaveSettings(Setting.updateInterval, UpdateInterval.OnStartup.ToString());
                }
                else if (cbUpdateInterval.SelectedIndex == 1)
                {
                    GetSetSettings.SaveSettings(Setting.updateInterval, UpdateInterval.Daily.ToString());
                }
                else if (cbUpdateInterval.SelectedIndex == 2)
                {
                    GetSetSettings.SaveSettings(Setting.updateInterval, UpdateInterval.Weekly.ToString());
                }

                Logging.Write(LogEvent.Variable, ProgramClass.SettingsForm, $"updateInterval set to: {GetSetSettings.GetUpdateInterval}");

                await Updater.CheckForUpdateInterval();
            }
            else
            {
                Logging.Write(LogEvent.Error, ProgramClass.SettingsForm, "cbUpdateInterval has no value!");
                ShowMessageBox.ShowBug();
            }
        }

        private void ComboBoxHandler(object sender, EventArgs e)
        {
            Logging.Write(LogEvent.Method, ProgramClass.SettingsForm, "ComboBoxHandler triggered");

            if (sender is ComboBox comboBox)
            {
                switch (comboBox.Name)
                {
                    case "cbLanguage":
                        SwitchCurrentLocale();
                        break;

                    case "cbUpdateInterval":
                        SwitchUpdateInterval();
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }
            else
            {
                Logging.Write(LogEvent.Warning, ProgramClass.SettingsForm, $"Sender: {sender} is not a ComboBox!");
            }
        }

        private void ChangingCheckBoxes(object sender, EventArgs e)
        {
            Logging.Write(LogEvent.Method, ProgramClass.SettingsForm, "ChangingSettings entered");

            if (sender is CheckBox checkBox)
            {
                if (checkBox.Name == "chbSaveOnClose")
                {
                    if (checkBox.Checked)
                    {
                        GetSetSettings.SaveSettings(Setting.saveOnClose, true);
                        Logging.Write(LogEvent.Setting, ProgramClass.SettingsForm, "chbSaveOnClose = true");
                        if (!chbAutosave.Checked)
                        {
                            chbAutosave.Checked = true;
                            SetAutosaveInterval();
                        }
                        chbAutosave.Enabled = false;
                        return;
                    }
                    else
                    {
                        GetSetSettings.SaveSettings(Setting.saveOnClose, false);
                        Logging.Write(LogEvent.Setting, ProgramClass.SettingsForm, "chbSaveOnClose = false");
                        chbAutosave.Enabled = true;
                        return;
                    }
                }
                else if (checkBox.Name == "chbReloadOnStartup")
                {
                    if (checkBox.Checked)
                    {
                        GetSetSettings.SaveSettings(Setting.reloadOnStartup, true);
                        Logging.Write(LogEvent.Setting, ProgramClass.SettingsForm, "chbReloadOnStartup = true");
                        if (!chbAutosave.Checked)
                        {
                            chbAutosave.Checked = true;
                            SetAutosaveInterval();
                        }
                        chbAutosave.Enabled = false;

                        if (!chbSaveOnClose.Checked)
                        {
                            chbSaveOnClose.Checked = true;
                        }
                        chbSaveOnClose.Enabled = false;
                        return;
                    }
                    else
                    {
                        GetSetSettings.SaveSettings(Setting.reloadOnStartup, false);
                        Logging.Write(LogEvent.Setting, ProgramClass.SettingsForm, "chbReloadOnStartup = false");
                        chbSaveOnClose.Enabled = true;
                        return;
                    }
                }
                else if (checkBox.Name == "chbAutosave")
                {
                    if (checkBox.Checked)
                    {
                        GetSetSettings.SaveSettings(Setting.autosave, true);
                        Logging.Write(LogEvent.Setting, ProgramClass.SettingsForm, "Autosave = true");
                        numberAutosaveInterval.Enabled = true;
                        numberAutosaveInterval.Visible = true;
                        lblAutosaveInterval.Enabled = true;
                        lblAutosaveInterval.Visible = true;
                        if (!checkBoxFalseAlarm)
                        {
                            SetAutosaveInterval();
                        }
                        return;
                    }
                    else
                    {
                        GetSetSettings.SaveSettings(Setting.autosave, false);
                        Logging.Write(LogEvent.Setting, ProgramClass.SettingsForm, "Autosave = false");
                        numberAutosaveInterval.Enabled = false;
                        numberAutosaveInterval.Visible = false;
                        lblAutosaveInterval.Enabled = false;
                        lblAutosaveInterval.Visible = false;
                        autosaveTimerChanged = false;
                        return;
                    }
                }
                else if (checkBox.Name == "chbUpdateDownload")
                {
                    if (checkBox.Checked)
                    {
                        GetSetSettings.SaveSettings(Setting.updateDownload, true);
                        Logging.Write(LogEvent.Setting, ProgramClass.SettingsForm, "updateDownload = true");
                        return;
                    }
                    else
                    {
                        GetSetSettings.SaveSettings(Setting.updateDownload, false);
                        Logging.Write(LogEvent.Setting, ProgramClass.SettingsForm, "updateDownload = false");
                        return;
                    }
                }
                else
                {
                    Logging.Write(LogEvent.Warning, ProgramClass.SettingsForm, $"CheckBox: {checkBox.Name} is not listed!");
                }
            }
            else
            {
                Logging.Write(LogEvent.Warning, ProgramClass.SettingsForm, $"Sender: {sender} is not a CheckBox!");
            }
        }

        // Triggered programmatically
        private void SetAutosaveInterval()
        {
            GetSetSettings.SaveSettings(Setting.autosaveInterval, numberAutosaveInterval.Value * 60000);
            Logging.Write(LogEvent.Setting, ProgramClass.SettingsForm, $"AutosaveInterval = {numberAutosaveInterval.Value}");

            if (numberAutosaveInterval.Value != currentAutosaveInterval)
            {
                autosaveTimerChanged = true;
                Logging.Write(LogEvent.Variable, ProgramClass.SettingsForm, $"autosaveTimerChanged = {autosaveTimerChanged}");
            }
        }

        // Triggered by the NumericUpDown
        private void SetAutosaveInterval(object sender, EventArgs e)
        {
            Logging.Write(LogEvent.Method, ProgramClass.SettingsForm, "SetAutosaveInterval entered");

            if (sender is NumericUpDown)
            {
                SetAutosaveInterval();
            }
            else
            {
                Logging.Write(LogEvent.Warning, ProgramClass.SettingsForm, $"Sender: {sender} is not a NumericUpDown!");
            }
        }

        private void ResetSettings(object sender, EventArgs e)
        {
            Logging.Write(LogEvent.Method, ProgramClass.SettingsForm, "ResetSettings entered");

            if (sender is Button)
            {
                string currentLocale = GetSetSettings.GetCurrentLocale;
                GetSetSettings.RestoreSettings();
                Localize();
                AdjustContentOnForm();

                if (currentLocale != GetSetSettings.GetCurrentLocale)
                {
                    languageChanged = true;
                }
            }
            else
            {
                Logging.Write(LogEvent.Warning, ProgramClass.SettingsForm, $"Sender: {sender} is not a Button!");
            }
        }
    }
}
