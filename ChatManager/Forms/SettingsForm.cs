using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ChatManager.Enums;
using ChatManager.Services;

namespace ChatManager.Forms;

internal partial class SettingsForm : Form
{
    internal SettingsForm()
    {
        InitializeComponent();
        GetAutosaveTimerChanged = false;
        GetLanguageChanged = false;
    }

    private bool _cbLanguageFalseAlarm;
    private bool _cbUpdaterIntervallFalseAlarm;
    private bool _checkBoxFalseAlarm;
    private decimal _currentAutosaveInterval;

    internal bool GetAutosaveTimerChanged { get; private set; }
    internal bool GetLanguageChanged { get; private set; }

    private void Localize()
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.SettingsForm, "Localize entered");

        Localization localization = new(GetSetSettings.GetCurrentLocale);

        // Change the Text of the Form
        Text = localization.GetString(Name);

        // Find all Controls of the desired Type and pack them in a Control List
        static List<T> GetControls<T>(Control parent) where T : Control
        {
            List<T> controls = [];

            foreach (Control control in parent.Controls)
            {
                if (control is T typedControl)
                    controls.Add(typedControl);

                controls.AddRange(GetControls<T>(control));
            }

            return controls;
        }

        List<GroupBox> groups = GetControls<GroupBox>(this);
        List<CheckBox> checkBoxes = GetControls<CheckBox>(this);
        List<Label> labels = GetControls<Label>(this);

        foreach (GroupBox group in groups)
        {
            group.Text = localization.GetString(group.Name);
        }

        foreach (CheckBox checkBox in checkBoxes)
        {
            checkBox.Text = localization.GetString(checkBox.Name);
        }

        foreach (Label label in labels)
        {
            label.Text = localization.GetString(label.Name);
        }

        btnResetSettings.Text = localization.GetString(btnResetSettings.Name);

        cbUpdateInterval.Items.Clear();
        cbUpdateInterval.Items.Add(localization.GetString(LocalizationEnum.UpdateIntervalOnStart));
        cbUpdateInterval.Items.Add(localization.GetString(LocalizationEnum.UpdateIntervalDaily));
        cbUpdateInterval.Items.Add(localization.GetString(LocalizationEnum.UpdateIntervalWeekly));
    }

    private void SettingsForm_Load(object sender, EventArgs e)
    {
        Localize();
        AdjustContentOnForm();
    }

    private void AdjustContentOnForm()
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.SettingsForm, "AdjustContentOnForm entered");

        string locale = GetSetSettings.GetCurrentLocale;
        switch (locale)
        {
            case "de":
                _cbLanguageFalseAlarm = true;
                cbLanguage.SelectedIndex = 2;
                break;

            case "en":
                _cbLanguageFalseAlarm = true;
                cbLanguage.SelectedIndex = 0;
                break;

            case "fr":
                _cbLanguageFalseAlarm = true;
                cbLanguage.SelectedIndex = 1;
                break;

            default:
                throw new InvalidOperationException($"{locale} is not implemented!");
        }

        if (GetSetSettings.GetAutosave)
        {
            _checkBoxFalseAlarm = true;
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
            _currentAutosaveInterval = GetSetSettings.GetAutosaveInterval / 60000;
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.SettingsForm, $"currentAutosaveInterval: {_currentAutosaveInterval}");

            if (_currentAutosaveInterval == 0)
            {
                numberAutosaveInterval.Value = 1;
                SetAutosaveInterval();
            }
            else
            {
                numberAutosaveInterval.Value = _currentAutosaveInterval;
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

        string updateInterval = GetSetSettings.GetUpdateInterval;
        switch (updateInterval)
        {
            case nameof(UpdateEnum.OnStartup):
                _cbUpdaterIntervallFalseAlarm = true;
                cbUpdateInterval.SelectedIndex = 0;
                break;

            case nameof(UpdateEnum.Daily):
                _cbUpdaterIntervallFalseAlarm = true;
                cbUpdateInterval.SelectedIndex = 1;
                break;

            case nameof(UpdateEnum.Weekly):
                _cbUpdaterIntervallFalseAlarm = true;
                cbUpdateInterval.SelectedIndex = 2;
                break;

            default:
                throw new InvalidOperationException($"{updateInterval} is not implemented!");
        }

        chbUpdateDownload.Checked = GetSetSettings.GetUpdateDownload;
    }

    private void SwitchCurrentLocale()
    {
        if (_cbLanguageFalseAlarm)
        {
            _cbLanguageFalseAlarm = false;
            return;
        }

        Logging.Write(LogEventEnum.Method, ProgramClassEnum.SettingsForm, "SwitchCurrentLocale entered");

        string currLocale = GetSetSettings.GetCurrentLocale;
        string selectedLanguage = cbLanguage.SelectedItem?.ToString() ?? string.Empty;

        string newLanguage = selectedLanguage switch
        {
            "English" => "en",
            "France" => "fr",
            "German" => "de",
            _ => throw new InvalidOperationException($"{selectedLanguage} is not implemented!"),
        };

        if (currLocale == newLanguage)
            return;

        Logging.Write(LogEventEnum.Setting, ProgramClassEnum.SettingsForm, $"Saving new locale: {newLanguage}");
        GetSetSettings.SaveSettings(SettingsEnum.locale, newLanguage);
        int updateIntervall = cbUpdateInterval.SelectedIndex;
        Localize();
        cbUpdateInterval.SelectedIndex = updateIntervall;
        GetLanguageChanged = true;
    }

    private void SwitchUpdateInterval()
    {
        if (_cbUpdaterIntervallFalseAlarm)
        {
            _cbUpdaterIntervallFalseAlarm = false;
            return;
        }

        Logging.Write(LogEventEnum.Method, ProgramClassEnum.SettingsForm, "SwitchUpdateInterval entered");

        if (cbUpdateInterval.SelectedIndex != -1)
        {
            if (cbUpdateInterval.SelectedIndex == 0)
            {
                GetSetSettings.SaveSettings(SettingsEnum.updateInterval, nameof(UpdateEnum.OnStartup));
            }
            else if (cbUpdateInterval.SelectedIndex == 1)
            {
                GetSetSettings.SaveSettings(SettingsEnum.updateInterval, nameof(UpdateEnum.Daily));
            }
            else if (cbUpdateInterval.SelectedIndex == 2)
            {
                GetSetSettings.SaveSettings(SettingsEnum.updateInterval, nameof(UpdateEnum.Weekly));
            }

            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.SettingsForm, $"updateInterval set to: {GetSetSettings.GetUpdateInterval}");
        }
        else
        {
            Logging.Write(LogEventEnum.Error, ProgramClassEnum.SettingsForm, "cbUpdateInterval has no value!");
            ShowMessageBox.ShowBug();
        }
    }

    private void ComboBoxHandler(object sender, EventArgs e)
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.SettingsForm, "ComboBoxHandler triggered");

        if (sender is ComboBox comboBox)
        {
            switch (comboBox.Name)
            {
                case nameof(cbLanguage):
                    SwitchCurrentLocale();
                    break;

                case nameof(cbUpdateInterval):
                    SwitchUpdateInterval();
                    break;

                default:
                    throw new InvalidOperationException($"{comboBox.Name} was not expected!");
            }
        }
        else
        {
            Logging.Write(LogEventEnum.Warning, ProgramClassEnum.SettingsForm, $"Sender: {sender} is not a ComboBox!");
        }
    }

    private void ChangingCheckBoxes(object sender, EventArgs e)
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.SettingsForm, "ChangingSettings entered");

        if (sender is CheckBox checkBox)
        {
            if (checkBox.Name == "chbSaveOnClose")
            {
                if (checkBox.Checked)
                {
                    GetSetSettings.SaveSettings(SettingsEnum.saveOnClose, true);
                    Logging.Write(LogEventEnum.Setting, ProgramClassEnum.SettingsForm, "chbSaveOnClose = true");
                    if (!chbAutosave.Checked)
                    {
                        chbAutosave.Checked = true;
                        SetAutosaveInterval();
                    }

                    chbAutosave.Enabled = false;
                }
                else
                {
                    GetSetSettings.SaveSettings(SettingsEnum.saveOnClose, false);
                    Logging.Write(LogEventEnum.Setting, ProgramClassEnum.SettingsForm, "chbSaveOnClose = false");
                    chbAutosave.Enabled = true;
                }
            }
            else if (checkBox.Name == "chbReloadOnStartup")
            {
                if (checkBox.Checked)
                {
                    GetSetSettings.SaveSettings(SettingsEnum.reloadOnStartup, true);
                    Logging.Write(LogEventEnum.Setting, ProgramClassEnum.SettingsForm, "chbReloadOnStartup = true");
                    if (!chbAutosave.Checked)
                    {
                        chbAutosave.Checked = true;
                        SetAutosaveInterval();
                    }

                    chbAutosave.Enabled = false;

                    if (!chbSaveOnClose.Checked)
                        chbSaveOnClose.Checked = true;

                    chbSaveOnClose.Enabled = false;
                }
                else
                {
                    GetSetSettings.SaveSettings(SettingsEnum.reloadOnStartup, false);
                    Logging.Write(LogEventEnum.Setting, ProgramClassEnum.SettingsForm, "chbReloadOnStartup = false");
                    chbSaveOnClose.Enabled = true;
                }
            }
            else if (checkBox.Name == "chbAutosave")
            {
                if (checkBox.Checked)
                {
                    GetSetSettings.SaveSettings(SettingsEnum.autosave, true);
                    Logging.Write(LogEventEnum.Setting, ProgramClassEnum.SettingsForm, "Autosave = true");
                    numberAutosaveInterval.Enabled = true;
                    numberAutosaveInterval.Visible = true;
                    lblAutosaveInterval.Enabled = true;
                    lblAutosaveInterval.Visible = true;
                    if (!_checkBoxFalseAlarm)
                    {
                        SetAutosaveInterval();
                    }
                }
                else
                {
                    GetSetSettings.SaveSettings(SettingsEnum.autosave, false);
                    Logging.Write(LogEventEnum.Setting, ProgramClassEnum.SettingsForm, "Autosave = false");
                    numberAutosaveInterval.Enabled = false;
                    numberAutosaveInterval.Visible = false;
                    lblAutosaveInterval.Enabled = false;
                    lblAutosaveInterval.Visible = false;
                    GetAutosaveTimerChanged = false;
                }
            }
            else if (checkBox.Name == "chbUpdateDownload")
            {
                if (checkBox.Checked)
                {
                    GetSetSettings.SaveSettings(SettingsEnum.updateDownload, true);
                    Logging.Write(LogEventEnum.Setting, ProgramClassEnum.SettingsForm, "updateDownload = true");
                }
                else
                {
                    GetSetSettings.SaveSettings(SettingsEnum.updateDownload, false);
                    Logging.Write(LogEventEnum.Setting, ProgramClassEnum.SettingsForm, "updateDownload = false");
                }
            }
            else
            {
                Logging.Write(LogEventEnum.Warning, ProgramClassEnum.SettingsForm, $"CheckBox: {checkBox.Name} is not listed!");
            }
        }
        else
        {
            Logging.Write(LogEventEnum.Warning, ProgramClassEnum.SettingsForm, $"Sender: {sender} is not a CheckBox!");
        }
    }

    /// <summary>
    /// Triggered programmatically.
    /// </summary>
    private void SetAutosaveInterval()
    {
        GetSetSettings.SaveSettings(SettingsEnum.autosaveInterval, numberAutosaveInterval.Value * 60000);
        Logging.Write(LogEventEnum.Setting, ProgramClassEnum.SettingsForm, $"AutosaveInterval = {numberAutosaveInterval.Value}");

        if (numberAutosaveInterval.Value == _currentAutosaveInterval)
            return;

        GetAutosaveTimerChanged = true;
        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.SettingsForm, $"autosaveTimerChanged = {GetAutosaveTimerChanged}");
    }

    /// <summary>
    /// Triggered by the <seealso cref="NumericUpDown"/>.
    /// </summary>
    private void SetAutosaveInterval(object sender, EventArgs e)
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.SettingsForm, "SetAutosaveInterval entered");

        if (sender is NumericUpDown)
        {
            SetAutosaveInterval();
        }
        else
        {
            Logging.Write(LogEventEnum.Warning, ProgramClassEnum.SettingsForm, $"Sender: {sender} is not a NumericUpDown!");
        }
    }

    private void ResetSettings(object sender, EventArgs e)
    {
        Logging.Write(LogEventEnum.Method, ProgramClassEnum.SettingsForm, "ResetSettings entered");

        if (sender is Button)
        {
            string currentLocale = GetSetSettings.GetCurrentLocale;
            GetSetSettings.RestoreSettings();
            Localize();
            AdjustContentOnForm();

            if (currentLocale != GetSetSettings.GetCurrentLocale)
                GetLanguageChanged = true;
        }
        else
        {
            Logging.Write(LogEventEnum.Warning, ProgramClassEnum.SettingsForm, $"Sender: {sender} is not a Button!");
        }
    }
}
