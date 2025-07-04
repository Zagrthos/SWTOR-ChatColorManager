using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ChatManager.Enums;
using ChatManager.Services;

namespace ChatManager;

internal sealed partial class MainForm : Form
{
    internal MainForm() => InitializeComponent();

    private System.Timers.Timer? _autosaveTimer;

    /// <summary>
    /// <seealso cref="Button"/> Click Handler for every <seealso cref="Button"/> next to the <seealso cref="TextBox"/>.
    /// </summary>
    private void ClickChangeColorButton(object sender, EventArgs e)
    {
        Logging.Write(LogEvent.Method, LogClass.MainForm, "ClickChangeColorButton entered");
        Logging.Write(LogEvent.Variable, LogClass.MainForm, $"Sender is: {sender}");

        // If the sender is a Button initialize it as button
        if (sender is Button button)
        {
            Logging.Write(LogEvent.Variable, LogClass.MainForm, $"Button is: {button.Name}");

            // If the button has a Tag initialize it as targetTextBox
            if (button.Tag is string targetTextBox)
            {
                Logging.Write(LogEvent.Variable, LogClass.MainForm, $"Button Tag is: {button.Tag}");

                // Find the TextBox...
                Control? control = null;

                // Yes we have to use the parent because somehow it focus itself on the damn button
                foreach (Control c in button.Parent!.Controls)
                {
                    if (c.Name == targetTextBox)
                    {
                        control = c;
                        break;
                    }
                }

                // ... and initialize it as textBox
                if (control is TextBox textBox)
                {
                    Logging.Write(LogEvent.Variable, LogClass.MainForm, $"TextBox is: {textBox.Name}");
                    Logging.Write(LogEvent.Variable, LogClass.MainForm, $"TextBox.Text is: {textBox.Text}");

                    // Check if text is Hex
                    string textBoxText = string.Empty;
                    if (Checks.CheckHexString(textBox.Text))
                    {
                        textBoxText = textBox.Text;
                        Logging.Write(LogEvent.Info, LogClass.MainForm, "TextBox.Text is Hex");
                        Logging.Write(LogEvent.Variable, LogClass.MainForm, $"Hex Text is: {textBoxText}");
                    }

                    // Get the Text from it, if it is not Empty
                    if (!string.IsNullOrWhiteSpace(textBoxText))
                    {
                        Color color = Converter.HexToRGB(textBoxText);
                        textBox.Text = OpenWindows.OpenColorPicker(button.Text, color);
                        Logging.Write(LogEvent.Variable, LogClass.MainForm, $"New hexColor is: {textBox.Text}");
                    }
                    else
                    {
                        Services.Localization localization = new(GetSetSettings.GetCurrentLocale);
                        Logging.Write(LogEvent.Warning, LogClass.MainForm, "String is empty! Not starting conversion process");
                        ShowMessageBox.Show(localization.GetString(Enums.LocalizationStrings.MessageBoxWarn), localization.GetString(Enums.LocalizationStrings.Warn_NoImportFound));
                    }
                }
                else
                {
                    Logging.Write(LogEvent.Warning, LogClass.MainForm, $"TextBox: {targetTextBox} not found!");
                    ShowMessageBox.ShowBug();
                }
            }
            else
            {
                Logging.Write(LogEvent.Warning, LogClass.MainForm, $"Button: {button.Name} has no Tag!");
                ShowMessageBox.ShowBug();
            }
        }
        else
        {
            Logging.Write(LogEvent.Warning, LogClass.MainForm, $"Sender: {sender} is not a Button!");
        }
    }

    /// <summary>
    /// <seealso cref="ToolStripMenuItem"/> Program Handler.
    /// </summary>
    private async void ToolStripMenuHandler(object sender, EventArgs e)
    {
        Logging.Write(LogEvent.Method, LogClass.MainForm, "ToolStripMainMenuHandler entered");

        // Check if sender is a ToolStripMenuItem
        if (sender is ToolStripMenuItem menuItem)
        {
            // Initialize variable and declare path based on the name of the menuItem
            string path = menuItem.Name switch
            {
                nameof(charFolderToolStripMenuItem) => GetSetSettings.GetLocalPath,
                nameof(backupToolStripMenuItem) => GetSetSettings.GetBackupPath,
                nameof(logFolderToolStripMenuItem) => GetSetSettings.GetLogPath,
                nameof(supportToolStripMenuItem) => GetSetSettings.GetSupportPath,
                nameof(bugToolStripMenuItem) => GetSetSettings.GetBugPath,
                nameof(bugMailToolStripMenuItem) => GetSetSettings.GetBugMailpath,
                _ => string.Empty,
            };

            switch (menuItem.Name)
            {
                case nameof(settingsToolStripMenuItem):
                    Logging.Write(LogEvent.Info, LogClass.MainForm, "Settings Form requested");
                    Hide();
                    (bool languageChanged, bool autosaveTimerChanged) = OpenWindows.OpenSettings();

                    if (autosaveTimerChanged)
                        ShowMessageBox.ShowRestart();

                    if (languageChanged)
                        Localize();

                    Show();
                    return;

                case nameof(supportToolStripMenuItem):
                    Logging.Write(LogEvent.Info, LogClass.MainForm, "Support Site requested");
                    OpenWindows.OpenLinksInBrowser(path);
                    return;

                case nameof(bugToolStripMenuItem):
                    Logging.Write(LogEvent.Info, LogClass.MainForm, "Bug report Site requested");
                    OpenWindows.OpenLinksInBrowser(path);
                    return;

                case nameof(bugMailToolStripMenuItem):
                    Logging.Write(LogEvent.Info, LogClass.MainForm, "Bug report Site requested");
                    OpenWindows.OpenProcess(path);
                    return;

                case nameof(aboutToolStripMenuItem):
                    Logging.Write(LogEvent.Info, LogClass.MainForm, "About Form requested");
                    OpenWindows.OpenAbout();
                    return;

                case nameof(updateToolStripMenuItem):
                    Logging.Write(LogEvent.Info, LogClass.MainForm, "Update Check requested");
                    if (Checks.CheckForInternetConnection(true))
                        await Updater.CheckForUpdatesAsync(true);

                    return;

                case nameof(changelogToolStripMenuItem):
                    Logging.Write(LogEvent.Info, LogClass.MainForm, "Changelog requested");
                    OpenWindows.OpenTextViewer(true);
                    return;

                case nameof(loadAutosaveToolStripMenuItem):
                    Logging.Write(LogEvent.Info, LogClass.MainForm, "Load Autosave requested");
                    ImportAutosave();
                    return;

                case nameof(restoreBackupToolStripMenuItem):
                    Logging.Write(LogEvent.Info, LogClass.MainForm, "Restore Backup requested");
                    OpenWindows.OpenBackupSelector();
                    return;

                case nameof(exitToolStripMenuItem):
                    Logging.Write(LogEvent.Info, LogClass.MainForm, "Application Exit requested");
                    Application.Exit();
                    return;

                default:
                    Logging.Write(LogEvent.Warning, LogClass.MainForm, $"MenuItem: {menuItem.Name} does not exist!");
                    break;
            }

            Logging.Write(LogEvent.Info, LogClass.MainForm, "Check if local Path exists");
            if (Directory.Exists(path))
            {
                // Open Explorer in given path
                Logging.Write(LogEvent.Info, LogClass.MainForm, "Local Path exists!");
                OpenWindows.OpenExplorer(path);
            }
            else
            {
                Logging.Write(LogEvent.Warning, LogClass.MainForm, "Local Path does not exist!");
                ShowMessageBox.ShowBug();
            }
        }
        else
        {
            Logging.Write(LogEvent.Warning, LogClass.MainForm, $"Sender: {sender} is not a ToolStripMenuItem!");
        }
    }

    /// <summary>
    /// Import the colorIndexes from the given File into all <seealso cref="TextBox"/>es.
    /// </summary>
    /// <exception cref="InvalidOperationException">Throws when the file name is empty.</exception>
    private void ImportFile(object sender, EventArgs e)
    {
        Logging.Write(LogEvent.Method, LogClass.MainForm, "ImportFile entered");

        // Get the selectedFile and the selectedListBox from the tuple
        (string selectedFile, string selectedListBox) = OpenWindows.OpenFileImportSelector();

        if (string.IsNullOrWhiteSpace(selectedFile))
        {
            Logging.Write(LogEvent.Warning, LogClass.MainForm, "No file was selected!");
            return;
        }

        Logging.Write(LogEvent.Info, LogClass.MainForm, $"selectedFile: {selectedFile}");
        Logging.Write(LogEvent.Info, LogClass.MainForm, $"selectedListBox: {selectedListBox}");

        // Get the whole Character List for the requested name
        string[,] filePaths = FileImport.GetArray($"{selectedListBox.Substring(3)}");

        // Loop through and set the colors to the corresponding textBox
        for (int i = 0; i < filePaths.Length / 2; i++)
        {
            if (selectedFile == filePaths[i, 0])
            {
                // But get the correct Colors from the right character file
                string filePath = filePaths[i, 1];

                GetFileColors(filePath, false);

                if (CheckForEmptyTextboxes() != 0)
                    break;

                Services.Localization localization = new(GetSetSettings.GetCurrentLocale);
                string message = localization.GetString(Enums.LocalizationStrings.Inf_AutosaveImport).Replace("CHARNAME", Converter.LabelToString(lblCharName.Text), StringComparison.OrdinalIgnoreCase).Replace("SERVERNAME", Converter.LabelToString(lblServerName.Text), StringComparison.OrdinalIgnoreCase).Replace("TIMESTAMP", File.GetLastWriteTime(filePath).ToString(CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase);
                ShowMessageBox.Show(localization.GetString(Enums.LocalizationStrings.MessageBoxInfo), message);

                break;
            }
        }
    }

    private void ImportAutosave()
    {
        Logging.Write(LogEvent.Info, LogClass.MainForm, "ImportAutosave entered");
        Services.Localization localization = new(GetSetSettings.GetCurrentLocale);
        if (Directory.Exists(GetSetSettings.GetAutosavePath))
        {
            string filePath = Path.Combine(GetSetSettings.GetAutosavePath, "autosave.txt");

            if (File.Exists(filePath))
            {
                GetFileColors(filePath, true);

                Logging.Write(LogEvent.Info, LogClass.MainForm, "Autosave data imported");
                Logging.Write(LogEvent.Info, LogClass.MainForm, "ReloadOnStartup set");

                string message = localization.GetString(Enums.LocalizationStrings.Inf_AutosaveImport).Replace("CHARNAME", Converter.LabelToString(lblCharName.Text), StringComparison.OrdinalIgnoreCase).Replace("SERVERNAME", Converter.LabelToString(lblServerName.Text), StringComparison.OrdinalIgnoreCase).Replace("TIMESTAMP", File.GetLastWriteTime(filePath).ToString(CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase);
                ShowMessageBox.Show(localization.GetString(Enums.LocalizationStrings.MessageBoxInfo), message);
            }
            else
            {
                Logging.Write(LogEvent.Warning, LogClass.MainForm, "No Autosave data found!");
                ShowMessageBox.Show(localization.GetString(Enums.LocalizationStrings.MessageBoxError), localization.GetString(Enums.LocalizationStrings.Err_AutosaveImport));
            }
        }
        else
        {
            Logging.Write(LogEvent.Warning, LogClass.MainForm, "No Autosave Directory found!");
            ShowMessageBox.Show(localization.GetString(Enums.LocalizationStrings.MessageBoxError), localization.GetString(Enums.LocalizationStrings.Err_AutosaveImport));
        }
    }

    private void GetFileColors(string filePath, bool autosaveInitiated)
    {
        Logging.Write(LogEvent.Method, LogClass.MainForm, "GetFileColors entered");

        string[] content = FileImport.GetContentFromFile(filePath, autosaveInitiated);

        if (content == Array.Empty<string>())
            return;

        SetCharServerText(content[1], content[0], autosaveInitiated);

        SetAllColorData(content/*, autosaveInitiated*/);

        btnResetColors.Visible = true;

        if (!GetSetSettings.GetAutosave)
            return;

        // Stop previously initialized Timer
        if (_autosaveTimer is not null)
        {
            _autosaveTimer.Stop();
            _autosaveTimer.Elapsed -= AutosaveTimer_Elapsed;
        }

        // Initialize Autosave Timer
        _autosaveTimer = new(Convert.ToDouble(GetSetSettings.GetAutosaveInterval));
        _autosaveTimer.Elapsed += AutosaveTimer_Elapsed;
        _autosaveTimer.Start();

        Logging.Write(LogEvent.Info, LogClass.MainForm, "autosaveTimer set");
    }

    private void SetCharServerText(string charText, string serverText, bool autosaveInitiated)
    {
        Logging.Write(LogEvent.Method, LogClass.MainForm, "SetCharServerText entered");

        Services.Localization localization = new(GetSetSettings.GetCurrentLocale);

        lblServerName.Visible = true;

        lblServerName.Text = (autosaveInitiated)
            ? $"{localization.GetString(lblServerName.Name)} {serverText}"
            : $"{localization.GetString(lblServerName.Name)} {Converter.AddWhitespace(Converter.ServerNameIdentifier(serverText, false))}";

        lblCharName.Visible = true;
        lblCharName.Text = $"{localization.GetString(lblCharName.Name)} {charText}";
    }

    // TODO: Why is this there???
    private void SetAllColorData(string[] colorIndexes/*, bool autosaveInitiated*/)
    {
        Logging.Write(LogEvent.Method, LogClass.MainForm, "SetAllColorData entered");

        tbTrade.Text = colorIndexes[2];
        tbPvP.Text = colorIndexes[3];
        tbGeneral.Text = colorIndexes[4];
        tbEmote.Text = colorIndexes[5];
        tbYell.Text = colorIndexes[6];
        tbOfficer.Text = colorIndexes[7];
        tbGuild.Text = colorIndexes[8];
        tbSay.Text = colorIndexes[9];
        tbWhisper.Text = colorIndexes[10];
        tbOps.Text = colorIndexes[11];
        tbOpsLead.Text = colorIndexes[12];
        tbGroup.Text = colorIndexes[13];
        tbOpsAnnou.Text = colorIndexes[14];
        tbOpsOfficer.Text = colorIndexes[15];
        tbCombat.Text = colorIndexes[16];
        tbConv.Text = colorIndexes[17];
        tbLogin.Text = colorIndexes[18];
        tbOpsInfo.Text = colorIndexes[19];
        tbSystem.Text = colorIndexes[20];
        tbGuildInfo.Text = colorIndexes[21];
        tbGroupInfo.Text = colorIndexes[22];
    }

    [SuppressMessage("Style", "IDE0300:Use collection expression for array", Justification = "This is better for readability.")]
    private string[] GetAllColorData()
    {
        string[] colorData = new string[100];

        colorData[0] = tbSay.Text;
        colorData[1] = tbYell.Text;
        colorData[2] = tbEmote.Text;
        colorData[3] = tbWhisper.Text;
        colorData[4] = string.Empty;
        colorData[5] = string.Empty;
        colorData[6] = tbGeneral.Text;
        colorData[7] = tbTrade.Text;
        colorData[8] = tbPvP.Text;
        colorData[9] = tbGroup.Text;
        colorData[10] = tbGuild.Text;
        colorData[11] = tbOfficer.Text;
        colorData[12] = tbOps.Text;
        colorData[13] = tbOpsOfficer.Text;
        colorData[14] = string.Empty;
        colorData[15] = string.Empty;
        colorData[16] = string.Empty;
        colorData[17] = string.Empty;
        colorData[18] = tbSystem.Text;
        colorData[19] = tbConv.Text;
        colorData[20] = tbLogin.Text;
        colorData[21] = string.Empty;
        colorData[22] = string.Empty;
        colorData[23] = string.Empty;
        colorData[24] = string.Empty;
        colorData[25] = string.Empty;
        colorData[26] = string.Empty;
        colorData[27] = string.Empty;
        colorData[28] = string.Empty;
        colorData[29] = tbOpsLead.Text;
        colorData[30] = string.Empty;
        colorData[31] = string.Empty;
        colorData[32] = string.Empty;
        colorData[33] = tbOpsAnnou.Text;
        colorData[34] = tbOpsInfo.Text;
        colorData[35] = tbGroupInfo.Text;
        colorData[36] = tbGuildInfo.Text;
        colorData[37] = tbCombat.Text;
        colorData[38] = string.Empty;
        colorData[39] = string.Empty;
        colorData[40] = string.Empty;
        colorData[41] = string.Empty;
        colorData[42] = string.Empty;
        colorData[43] = string.Empty;
        colorData[44] = string.Empty;
        colorData[45] = string.Empty;
        colorData[46] = string.Empty;
        colorData[47] = string.Empty;
        colorData[48] = string.Empty;
        colorData[49] = string.Empty;
        colorData[50] = string.Empty;
        colorData[51] = string.Empty;
        colorData[52] = string.Empty;
        colorData[53] = string.Empty;
        colorData[54] = string.Empty;
        colorData[55] = string.Empty;
        colorData[56] = string.Empty;
        colorData[57] = string.Empty;
        colorData[58] = string.Empty;
        colorData[59] = string.Empty;
        colorData[60] = string.Empty;
        colorData[61] = string.Empty;
        colorData[62] = string.Empty;
        colorData[63] = string.Empty;
        colorData[64] = string.Empty;
        colorData[65] = string.Empty;
        colorData[66] = string.Empty;
        colorData[67] = string.Empty;
        colorData[68] = string.Empty;
        colorData[69] = string.Empty;
        colorData[70] = string.Empty;
        colorData[71] = string.Empty;
        colorData[72] = string.Empty;
        colorData[73] = string.Empty;
        colorData[74] = string.Empty;
        colorData[75] = string.Empty;
        colorData[76] = string.Empty;
        colorData[77] = string.Empty;
        colorData[78] = string.Empty;
        colorData[79] = string.Empty;
        colorData[80] = string.Empty;
        colorData[81] = string.Empty;
        colorData[82] = string.Empty;
        colorData[83] = string.Empty;
        colorData[84] = string.Empty;
        colorData[85] = string.Empty;
        colorData[86] = string.Empty;
        colorData[87] = string.Empty;
        colorData[88] = string.Empty;
        colorData[89] = string.Empty;
        colorData[90] = string.Empty;
        colorData[91] = string.Empty;
        colorData[92] = string.Empty;
        colorData[93] = string.Empty;
        colorData[94] = string.Empty;
        colorData[95] = string.Empty;
        colorData[96] = string.Empty;
        colorData[97] = string.Empty;
        colorData[98] = string.Empty;
        colorData[99] = string.Empty;

        return colorData;
    }

    private void ExportFiles(object sender, EventArgs e)
    {
        Logging.Write(LogEvent.Method, LogClass.MainForm, "ExportFiles entered");

        int counter = CheckForEmptyTextboxes();

        if (counter != 0)
        {
            Logging.Write(LogEvent.Warning, LogClass.MainForm, $"{counter} empty textBoxes found!");
            Services.Localization localization = new(GetSetSettings.GetCurrentLocale);

            string exportedFilesInfo = localization.GetString(Enums.LocalizationStrings.Warn_TextBoxEmpty);
            exportedFilesInfo = exportedFilesInfo.Replace("TEXTBOXCOUNTER", counter.ToString(CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase);

            ShowMessageBox.Show(localization.GetString(Enums.LocalizationStrings.MessageBoxError), exportedFilesInfo);
            return;
        }

        Logging.Write(LogEvent.Info, LogClass.MainForm, "0 empty textboxes found!");

        string[] colorIndexes = GetAllColorData();

        OpenWindows.OpenFileExportSelector(colorIndexes);
    }

    private void ResetColors(object sender, EventArgs e)
    {
        Logging.Write(LogEvent.Method, LogClass.MainForm, "ResetColors entered");

        string[] tempIndexes = GetSetSettings.GetDefaultColors.Split(";");

        string[] colorIndexes = new string[23];

        // Set each colorIndex position to the correct colorLineIndex position
        colorIndexes[2] = tempIndexes[7]; // Trade
        colorIndexes[3] = tempIndexes[8]; // PvP
        colorIndexes[4] = tempIndexes[6]; // General
        colorIndexes[5] = tempIndexes[2]; // Emote
        colorIndexes[6] = tempIndexes[1]; // Yell
        colorIndexes[7] = tempIndexes[11]; // Officer
        colorIndexes[8] = tempIndexes[10]; // Guild
        colorIndexes[9] = tempIndexes[0]; // Say
        colorIndexes[10] = tempIndexes[3]; // Whisper
        colorIndexes[11] = tempIndexes[12]; // Ops
        colorIndexes[12] = tempIndexes[29]; // Ops Leader
        colorIndexes[13] = tempIndexes[9]; // Group
        colorIndexes[14] = tempIndexes[33]; // Ops Announcement
        colorIndexes[15] = tempIndexes[13]; // Ops Officer
        colorIndexes[16] = tempIndexes[37]; // Combat Information
        colorIndexes[17] = tempIndexes[19]; // Conversation
        colorIndexes[18] = tempIndexes[20]; // Character Login
        colorIndexes[19] = tempIndexes[34]; // Ops Information
        colorIndexes[20] = tempIndexes[18]; // System Feedback
        colorIndexes[21] = tempIndexes[36]; // Guild Information
        colorIndexes[22] = tempIndexes[35]; // Group Information

        SetAllColorData(colorIndexes/*, false*/);

        Services.Localization localization = new(GetSetSettings.GetCurrentLocale);

        ShowMessageBox.Show(localization.GetString(Enums.LocalizationStrings.MessageBoxInfo), localization.GetString(Enums.LocalizationStrings.Inf_ColorsReset));
    }

    private int CheckForEmptyTextboxes()
    {
        Logging.Write(LogEvent.Method, LogClass.MainForm, "CheckForEmptyTextboxes entered");

        List<TextBox> textBoxes = GetControls<TextBox>(this);
        int counter = 0;

        Logging.Write(LogEvent.Info, LogClass.MainForm, "Checking for empty textBoxes...");
        foreach (TextBox textBox in textBoxes.Where(static t => string.IsNullOrWhiteSpace(t.Text)))
        {
            counter++;
            Logging.Write(LogEvent.Variable, LogClass.MainForm, $"{textBox.Name} is empty, counter: {counter}");
        }

        return counter;
    }

    private void Localize()
    {
        Logging.Write(LogEvent.Method, LogClass.MainForm, "Localize entered");

        Services.Localization localization = new(GetSetSettings.GetCurrentLocale);

        List<TabControl> tabs = GetControls<TabControl>(this);
        List<Button> buttons = GetControls<Button>(this);
        List<Label> labels = GetControls<Label>(this);

        // Needed because it's disabled by default and does not change the localization
        // It will be enabled, the state will be saved and downwards it will be disabled again
        bool loadAutosaveEnabled = false;
        if (!loadAutosaveToolStripMenuItem.Enabled)
        {
            loadAutosaveToolStripMenuItem.Enabled = true;
            loadAutosaveEnabled = true;
        }

        foreach (object? item in menuMainForm.Items)
        {
            if (item is ToolStripMenuItem menuItem && menuItem.Enabled)
            {
                menuItem.Text = localization.GetString(menuItem.Name ?? throw new InvalidOperationException("MenuItem.Name is null"));

                foreach (object? moreItems in menuItem.DropDownItems)
                {
                    if (moreItems is ToolStripMenuItem moreItem && moreItem.Enabled)
                        moreItem.Text = localization.GetString(moreItem.Name ?? throw new InvalidOperationException("MenuItem.Name is null"));
                }
            }
        }

        // Disable the control again if the state is true
        if (loadAutosaveEnabled)
            loadAutosaveToolStripMenuItem.Enabled = false;

        foreach (TabControl tabControl in tabs)
        {
            foreach (Control control in tabControl.Controls)
            {
                if (control is TabPage tab)
                    tab.Text = localization.GetString(tab.Name);
            }
        }

        foreach (Button button in buttons)
        {
            button.Text = localization.GetString(button.Name);
        }

        foreach (Label label in labels)
        {
            label.Text = localization.GetString(label.Name);
        }
    }

    private void InitializeCustomSettings()
    {
        Logging.Write(LogEvent.Method, LogClass.MainForm, "InitializeCustomSettings entered");

        if (GetSetSettings.GetAutosave)
        {
            Logging.Write(LogEvent.Info, LogClass.MainForm, "Autosave set to true");
            loadAutosaveToolStripMenuItem.Enabled = true;
        }
        else
        {
            Logging.Write(LogEvent.Info, LogClass.MainForm, "Autosave set to false");
            loadAutosaveToolStripMenuItem.Enabled = false;
        }

        if (!GetSetSettings.GetSaveOnClose)
            return;

        Logging.Write(LogEvent.Info, LogClass.MainForm, "SaveOnClose set");

        if (!GetSetSettings.GetReloadOnStartup)
            return;

        ImportAutosave();
    }

    private void DoSave()
    {
        Logging.Write(LogEvent.Method, LogClass.MainForm, "DoSave entered");

        string[] colorIndexes = GetAllColorData();

        Autosave.DoAutosave(Converter.LabelToString(lblCharName.Text), Converter.LabelToString(lblServerName.Text), colorIndexes);
    }

    /// <summary>
    /// Find all <seealso cref="Control"/>s of the desired <seealso cref="Type"/>.
    /// </summary>
    /// <typeparam name="T">The <seealso cref="Control"/> <seealso cref="Type"/>.</typeparam>
    /// <param name="parent">The parent <seealso cref="Control"/>.</param>
    /// <returns>A <seealso cref="List{T}"/>.</returns>
    private List<T> GetControls<T>(Control parent) where T : Control
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

    private void AutosaveTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        Logging.Write(LogEvent.Info, LogClass.MainForm, "AutosaveTimer elapsed");

        DoSave();
    }

    /// <summary>
    /// Perform basic Checks when the <seealso cref="Form"/> is loading.
    /// </summary>
#if !DEBUG
    private async void MainForm_Load(object sender, EventArgs e)
#else
    private void MainForm_Load(object? sender, EventArgs e)
#endif
    {
        if (string.IsNullOrWhiteSpace(GetSetSettings.GetCurrentLocale))
        {
            Logging.Write(LogEvent.Variable, LogClass.MainForm, $"CurrentCulture set to: {CultureInfo.CurrentCulture.TwoLetterISOLanguageName}");
            GetSetSettings.SaveSettings(SettingsNames.locale, CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
        }

        Localize();

#if !DEBUG
        Logging.Write(LogEvent.Info, LogClass.MainForm, "Check if SWTOR is running");
        if (Checks.CheckSwtorProcessFound)
        {
            Localization localization = new(GetSetSettings.GetCurrentLocale);
            Logging.Write(LogEvent.Warning, LogClass.MainForm, "SWTOR is running!");
            ShowMessageBox.Show(localization.GetString("MessageBoxWarn"), localization.GetString("Warn_SWTORrunning"));
        }
        else
        {
            Logging.Write(LogEvent.Info, LogClass.MainForm, "SWTOR is not running!");
        }
#endif

        Logging.Write(LogEvent.Info, LogClass.MainForm, "Set BackupOption in Menu");
        if (!Checks.DirectoryCheck(CheckFolder.BackupFolder))
        {
            backupToolStripMenuItem.Enabled = false;
            restoreBackupToolStripMenuItem.Enabled = false;
            Logging.Write(LogEvent.Warning, LogClass.MainForm, "BackupOption is not available!");
        }
        else
        {
            Logging.Write(LogEvent.Info, LogClass.MainForm, "BackupOption is available!");
        }

        Logging.Write(LogEvent.Info, LogClass.MainForm, "Set CharacterOption in Menu");
        if (string.IsNullOrWhiteSpace(GetSetSettings.GetLocalPath))
        {
            charFolderToolStripMenuItem.Enabled = false;
            Logging.Write(LogEvent.Warning, LogClass.MainForm, "CharacterOption is not available!");
            Services.Localization localization = new(GetSetSettings.GetCurrentLocale);
            ShowMessageBox.Show(localization.GetString(Enums.LocalizationStrings.MessageBoxWarn), localization.GetString(Enums.LocalizationStrings.Warn_SWTORpathNotFound));
        }
        else
        {
            Logging.Write(LogEvent.Info, LogClass.MainForm, "CharacterOption is available!");
        }

        InitializeCustomSettings();

        Logging.Write(LogEvent.Info, LogClass.MainForm, "MainForm is loading");

        DownloadProgressReporter.DownloadProgressChanged += DownloadProgressReporter_DownloadProgressChanged;

#if !DEBUG
        if (!Checks.CheckForInternetConnection())
            return;

        await Updater.CheckForUpdateIntervalAsync();
#endif
    }

    /// <summary>
    /// Receive the download progress from the Updater.
    /// </summary>
    private void DownloadProgressReporter_DownloadProgressChanged(object? sender, DownloadProgressEventArgs e)
    {
        if (InvokeRequired)
        {
            Invoke(new Action(() => UpdateDownloadProgress(e.GetDownloadProgress())));
            return;
        }

        UpdateDownloadProgress(e.GetDownloadProgress());
    }

    /// <summary>
    /// Display the download progress on the screen.
    /// </summary>
    /// <param name="progress">The download progress.</param>
    private void UpdateDownloadProgress(double progress)
    {
        if (!downloadProgressToolStripMenuItem.Visible)
            downloadProgressToolStripMenuItem.Visible = true;

        downloadProgressToolStripMenuItem.Text = Updater.GetUpdateDownloadText.Replace("PROGRESS", progress.ToString(CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// When the <seealso cref="Form"/> is closing, log it, remove the DownloadProgressReporter, stop the autosaveTimer and do an autosave.
    /// </summary>
    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        Logging.Write(LogEvent.Info, LogClass.MainForm, "MainForm closing");

        DownloadProgressReporter.DownloadProgressChanged -= DownloadProgressReporter_DownloadProgressChanged;

        _autosaveTimer?.Stop();

        if (!GetSetSettings.GetSaveOnClose)
            return;

        DoSave();
    }

    /// <summary>
    /// When the <seealso cref="Form"/> is closed, log it and stop the logger.
    /// </summary>
    private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        Logging.Write(LogEvent.Info, LogClass.MainForm, "MainForm closed");
        Logging.Dispose();
    }

    /// <summary>
    /// Draw the <seealso cref="TabPage"/>s on the left side.
    /// </summary>
    [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Needed by WinForms.")]
    private void TabsMainForm_DrawItem(object sender, DrawItemEventArgs e)
    {
        Graphics g = e.Graphics;
        Brush _textBrush;

        // Get the item from the collection
        TabPage _tabPage = tabsMainForm.TabPages[e.Index];

        // Get the real bounds for the tab rectangle
        Rectangle _tabBounds = tabsMainForm.GetTabRect(e.Index);

        if (e.State == DrawItemState.Selected)
        {
            // Draw a different background color, and don't paint a focus rectangle
            _textBrush = new SolidBrush(Color.Black);
            g.FillRectangle(Brushes.LightGray, e.Bounds);
        }
        else
        {
            _textBrush = new SolidBrush(e.ForeColor);
            e.DrawBackground();
        }

        // Use our own font
        Font _tabFont = new("Segoe UI", 9f);

        // Draw string. Center the text
        StringFormat _stringFlags = new()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
    }
}
