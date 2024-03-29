using ChatManager.Enums;
using ChatManager.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ChatManager
{
    internal partial class MainForm : Form
    {
        internal MainForm()
        {
            InitializeComponent();
        }

        private System.Timers.Timer? autosaveTimer;

        // Button Click Handler for every button next to the TextBox
        private void ClickChangeColorButton(object sender, EventArgs e)
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.MainForm, "ClickChangeColorButton entered");
            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.MainForm, $"Sender is: {sender}");

            // If the sender is a Button initialize it as button
            if (sender is Button button)
            {
                Logging.Write(LogEventEnum.Variable, ProgramClassEnum.MainForm, $"Button is: {button.Name}");

                // If the button has a Tag initialize it as targetTextBox
                if (button.Tag is string targetTextBox)
                {
                    Logging.Write(LogEventEnum.Variable, ProgramClassEnum.MainForm, $"Button Tag is: {button.Tag}");

                    // Find the TextBox...
                    Control? control = Controls.Find(targetTextBox, true).FirstOrDefault();

                    // ... and initialize it as textBox
                    if (control is TextBox textBox)
                    {
                        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.MainForm, $"TextBox is: {textBox.Name}");
                        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.MainForm, $"TextBox.Text is: {textBox.Text}");

                        // Check if text is Hex
                        string textBoxText = string.Empty;
                        if (Checks.CheckHexString(textBox.Text))
                        {
                            textBoxText = textBox.Text;
                            Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, $"TextBox.Text is Hex");
                            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.MainForm, $"Hex Text is: {textBoxText}");
                        }

                        // Get the Text from it, if it is not Empty
                        if (!string.IsNullOrEmpty(textBoxText))
                        {
                            Color color = Converter.HexToRGBAsync(textBoxText);
                            textBox.Text = OpenWindows.OpenColorPicker(button.Text, color);
                            Logging.Write(LogEventEnum.Variable, ProgramClassEnum.MainForm, $"New hexColor is: {textBox.Text}");
                        }
                        else
                        {
                            Localization localization = new(GetSetSettings.GetCurrentLocale);
                            Logging.Write(LogEventEnum.Warning, ProgramClassEnum.MainForm, "String is empty! Not starting conversion process");
                            ShowMessageBox.Show(localization.GetString(LocalizationEnum.MessageBoxWarn), localization.GetString(LocalizationEnum.Warn_NoImportFound));
                        }
                    }
                    else
                    {
                        Logging.Write(LogEventEnum.Warning, ProgramClassEnum.MainForm, $"TextBox: {targetTextBox} not found!");
                        ShowMessageBox.ShowBug();
                    }
                }
                else
                {
                    Logging.Write(LogEventEnum.Warning, ProgramClassEnum.MainForm, $"Button: {button.Name} has no Tag!");
                    ShowMessageBox.ShowBug();
                }
            }
            else
            {
                Logging.Write(LogEventEnum.Warning, ProgramClassEnum.MainForm, $"Sender: {sender} is not a Button!");
            }
        }

        // ToolStripMenu Program Handler
        private async void ToolStripMenuHandler(object sender, EventArgs e)
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.MainForm, "ToolStripMainMenuHandler entered");

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
                        Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "Settings Form requested");
                        Hide();
                        (bool languageChanged, bool autosaveTimerChanged) = OpenWindows.OpenSettings();

                        if (autosaveTimerChanged)
                        {
                            ShowMessageBox.ShowRestart();
                        }

                        if (languageChanged)
                        {
                            Localize();
                        }

                        Show();
                        return;

                    case nameof(supportToolStripMenuItem):
                        Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "Support Site requested");
                        OpenWindows.OpenLinksInBrowser(path);
                        return;

                    case nameof(bugToolStripMenuItem):
                        Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "Bug report Site requested");
                        OpenWindows.OpenLinksInBrowser(path);
                        return;

                    case nameof(bugMailToolStripMenuItem):
                        Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "Bug report Site requested");
                        OpenWindows.OpenProcess(path);
                        return;

                    case nameof(aboutToolStripMenuItem):
                        Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "About Form requested");
                        OpenWindows.OpenAbout();
                        return;

                    case nameof(updateToolStripMenuItem):
                        Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "Update Check requested");
                        if (Checks.CheckForInternetConnection(true))
                        {
                            await Updater.CheckForUpdates(true);
                        }
                        return;

                    case nameof(changelogToolStripMenuItem):
                        Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "Changelog requested");
                        OpenWindows.OpenTextViewer(true);
                        return;

                    case nameof(loadAutosaveToolStripMenuItem):
                        Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "Load Autosave requested");
                        ImportAutosave();
                        return;

                    case nameof(restoreBackupToolStripMenuItem):
                        Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "Restore Backup requested");
                        OpenWindows.OpenBackupSelector();
                        return;

                    case nameof(exitToolStripMenuItem):
                        Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "Application Exit requested");
                        Application.Exit();
                        return;
                }

                Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "Check if local Path exists");
                if (Directory.Exists(path))
                {
                    // Open Explorer in given path
                    Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "Local Path exists!");
                    OpenWindows.OpenExplorer(path);
                }
                else
                {
                    Logging.Write(LogEventEnum.Warning, ProgramClassEnum.MainForm, "Local Path does not exist!");
                    ShowMessageBox.ShowBug();
                }
            }
            else
            {
                Logging.Write(LogEventEnum.Warning, ProgramClassEnum.MainForm, $"Sender: {sender} is not a ToolStripMenuItem!");
            }
        }

        // Import the colorIndexes from the given File into all textBoxes
        private void ImportFile(object sender, EventArgs e)
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.MainForm, "ImportFile entered");
            FileImport fileImport = new();

            // Get the selectedFile and the selectedListBox from the tuple
            (string selectedFile, string selectedListBox) = OpenWindows.OpenFileImportSelector();
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, $"selectedFile: {selectedFile}");
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, $"selectedListBox: {selectedListBox}");

            if (!string.IsNullOrEmpty(selectedFile))
            {
                // Get the whole Character List for the requested name
                string[,] filePaths = fileImport.GetArray($"{selectedListBox.Substring(3)}");

                // Loop through and set the colors to the corresponding textBox
                for (int i = 0; i < 1000; i++)
                {
                    if (selectedFile == filePaths[i, 0])
                    {
                        // But get the correct Colors from the right character file
                        string filePath = filePaths[i, 1];

                        GetFileColors(filePath, false);

                        byte counter = CheckForEmptyTextboxes();

                        if (counter != 0)
                        {
                            break;
                        }

                        Localization localization = new(GetSetSettings.GetCurrentLocale);
                        string message = localization.GetString(LocalizationEnum.Inf_AutosaveImport).Replace("CHARNAME", Converter.LabelToString(lblCharName.Text)).Replace("SERVERNAME", Converter.LabelToString(lblServerName.Text)).Replace("TIMESTAMP", File.GetLastWriteTime(filePath).ToString());
                        ShowMessageBox.Show(localization.GetString(LocalizationEnum.MessageBoxInfo), message);

                        break;
                    }
                }
            }
        }

        private void ImportAutosave()
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "ImportAutosave entered");
            Localization localization = new(GetSetSettings.GetCurrentLocale);
            if (Directory.Exists(GetSetSettings.GetAutosavePath))
            {
                string filePath = Path.Combine(GetSetSettings.GetAutosavePath, "autosave.txt");

                if (File.Exists(filePath))
                {
                    GetFileColors(filePath, true);

                    Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "Autosave data imported");
                    Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "ReloadOnStartup set");

                    string message = localization.GetString(LocalizationEnum.Inf_AutosaveImport).Replace("CHARNAME", Converter.LabelToString(lblCharName.Text)).Replace("SERVERNAME", Converter.LabelToString(lblServerName.Text)).Replace("TIMESTAMP", File.GetLastWriteTime(filePath).ToString());
                    ShowMessageBox.Show(localization.GetString(LocalizationEnum.MessageBoxInfo), message);
                }
                else
                {
                    Logging.Write(LogEventEnum.Warning, ProgramClassEnum.MainForm, "No Autosave data found!");
                    ShowMessageBox.Show(localization.GetString(LocalizationEnum.MessageBoxError), localization.GetString(LocalizationEnum.Err_AutosaveImport));
                }
            }
            else
            {
                Logging.Write(LogEventEnum.Warning, ProgramClassEnum.MainForm, "No Autosave Directory found!");
                ShowMessageBox.Show(localization.GetString(LocalizationEnum.MessageBoxError), localization.GetString(LocalizationEnum.Err_AutosaveImport));
            }
        }

        private void GetFileColors(string filePath, bool autosaveIntitiated)
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.MainForm, "GetFileColors entered");

            FileImport fileImport = new();

            string[] content = fileImport.GetContentFromFile(filePath, autosaveIntitiated);

            if (content == Array.Empty<string>())
            {
                return;
            }

            SetCharServerText(content[1], content[0], autosaveIntitiated);

            SetAllColorData(content, autosaveIntitiated);

            btnResetColors.Visible = true;

            if (GetSetSettings.GetAutosave)
            {
                // Stop previously intialized Timer
                if (autosaveTimer != null)
                {
                    autosaveTimer.Stop();
                    autosaveTimer.Elapsed -= AutosaveTimer_Elapsed;
                }

                // Initialize Autosave Timer
                autosaveTimer = new(Convert.ToDouble(GetSetSettings.GetAutosaveInterval));
                autosaveTimer.Elapsed += AutosaveTimer_Elapsed;
                autosaveTimer.Start();

                Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "autosaveTimer set");
            }
        }

        private void SetCharServerText(string charText, string serverText, bool autosaveIntitiated)
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.MainForm, "SetCharServerText entered");

            Localization localization = new(GetSetSettings.GetCurrentLocale);

            lblServerName.Visible = true;

            if (autosaveIntitiated)
            {
                lblServerName.Text = $"{localization.GetString(lblServerName.Name)} {serverText}";
            }
            else
            {
                lblServerName.Text = $"{localization.GetString(lblServerName.Name)} {Converter.AddWhitespace(Converter.ServerNameIdentifier(serverText, false))}";
            }

            lblCharName.Visible = true;
            lblCharName.Text = $"{localization.GetString(lblCharName.Name)} {charText}"; ;
        }

        private void SetAllColorData(string[] colorIndexes, bool autosaveIntitiated)
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.MainForm, "SetAllColorData entered");

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

        private string[] GetAllColorData()
        {
            string[] colorData = new string[100];

            colorData[0] = tbSay.Text;
            colorData[1] = tbYell.Text;
            colorData[2] = tbEmote.Text;
            colorData[3] = tbWhisper.Text;
            colorData[4] = "";
            colorData[5] = "";
            colorData[6] = tbGeneral.Text;
            colorData[7] = tbTrade.Text;
            colorData[8] = tbPvP.Text;
            colorData[9] = tbGroup.Text;
            colorData[10] = tbGuild.Text;
            colorData[11] = tbOfficer.Text;
            colorData[12] = tbOps.Text;
            colorData[13] = tbOpsOfficer.Text;
            colorData[14] = "";
            colorData[15] = "";
            colorData[16] = "";
            colorData[17] = "";
            colorData[18] = tbSystem.Text;
            colorData[19] = tbConv.Text;
            colorData[20] = tbLogin.Text;
            colorData[21] = "";
            colorData[22] = "";
            colorData[23] = "";
            colorData[24] = "";
            colorData[25] = "";
            colorData[26] = "";
            colorData[27] = "";
            colorData[28] = "";
            colorData[29] = tbOpsLead.Text;
            colorData[30] = "";
            colorData[31] = "";
            colorData[32] = "";
            colorData[33] = tbOpsAnnou.Text;
            colorData[34] = tbOpsInfo.Text;
            colorData[35] = tbGroupInfo.Text;
            colorData[36] = tbGuildInfo.Text;
            colorData[37] = tbCombat.Text;
            colorData[38] = "";
            colorData[39] = "";
            colorData[40] = "";
            colorData[41] = "";
            colorData[42] = "";
            colorData[43] = "";
            colorData[44] = "";
            colorData[45] = "";
            colorData[46] = "";
            colorData[47] = "";
            colorData[48] = "";
            colorData[49] = "";
            colorData[50] = "";
            colorData[51] = "";
            colorData[52] = "";
            colorData[53] = "";
            colorData[54] = "";
            colorData[55] = "";
            colorData[56] = "";
            colorData[57] = "";
            colorData[58] = "";
            colorData[59] = "";
            colorData[60] = "";
            colorData[61] = "";
            colorData[62] = "";
            colorData[63] = "";
            colorData[64] = "";
            colorData[65] = "";
            colorData[66] = "";
            colorData[67] = "";
            colorData[68] = "";
            colorData[69] = "";
            colorData[70] = "";
            colorData[71] = "";
            colorData[72] = "";
            colorData[73] = "";
            colorData[74] = "";
            colorData[75] = "";
            colorData[76] = "";
            colorData[77] = "";
            colorData[78] = "";
            colorData[79] = "";
            colorData[80] = "";
            colorData[81] = "";
            colorData[82] = "";
            colorData[83] = "";
            colorData[84] = "";
            colorData[85] = "";
            colorData[86] = "";
            colorData[87] = "";
            colorData[88] = "";
            colorData[89] = "";
            colorData[90] = "";
            colorData[91] = "";
            colorData[92] = "";
            colorData[93] = "";
            colorData[94] = "";
            colorData[95] = "";
            colorData[96] = "";
            colorData[97] = "";
            colorData[98] = "";
            colorData[99] = "";

            return colorData;
        }

        private void ExportFiles(object sender, EventArgs e)
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.MainForm, "ExportFiles entered");

            byte counter = CheckForEmptyTextboxes();

            if (counter != 0)
            {
                Logging.Write(LogEventEnum.Warning, ProgramClassEnum.MainForm, $"{counter} empty textBoxes found!");
                Localization localization = new(GetSetSettings.GetCurrentLocale);

                string exportedFilesInfo = localization.GetString(LocalizationEnum.Warn_TextBoxEmpty);
                exportedFilesInfo = exportedFilesInfo.Replace("TEXTBOXCOUNTER", counter.ToString());

                ShowMessageBox.Show(localization.GetString(LocalizationEnum.MessageBoxError), exportedFilesInfo);
                return;
            }
            else
            {
                Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "0 empty textboxes found!");
            }

            string[] colorIndexes = GetAllColorData();

            OpenWindows.OpenFileExportSelector(colorIndexes);
        }

        private void ResetColors(object sender, EventArgs e)
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.MainForm, "ResetColors entered");

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

            SetAllColorData(colorIndexes, false);

            Localization localization = new(GetSetSettings.GetCurrentLocale);

            ShowMessageBox.Show(localization.GetString(LocalizationEnum.MessageBoxInfo), localization.GetString(LocalizationEnum.Inf_ColorsReset));
        }

        private byte CheckForEmptyTextboxes()
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.MainForm, "CheckForEmptyTextboxes entered");

            var textBoxes = GetControls(this, typeof(TextBox));
            byte counter = 0;

            Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "Checking for empty textBoxes...");
            foreach (Control control in textBoxes)
            {
                if (control is TextBox textBox)
                {
                    if (string.IsNullOrEmpty(textBox.Text))
                    {
                        counter++;
                        Logging.Write(LogEventEnum.Variable, ProgramClassEnum.MainForm, $"counter: {counter}");
                    }
                }
            }

            return counter;
        }

        private void Localize()
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.MainForm, "Localize entered");

            Localization localization = new(GetSetSettings.GetCurrentLocale);

            if (GetSetSettings.GetCurrentLocale == "fr")
            {
                tabsMainForm.ItemSize = new Size(50, 100);
            }
            else
            {
                tabsMainForm.ItemSize = new Size(25, 100);
            }

            var tabs = GetControls(this, typeof(TabControl));
            var buttons = GetControls(this, typeof(Button));
            var labels = GetControls(this, typeof(Label));

            // Needed because it's disabled by default and does not change the localization
            // It will be enabled, the state will be saved and downwards it will be disabled again
            bool loadAutosaveEnabled = false;
            if (!loadAutosaveToolStripMenuItem.Enabled)
            {
                loadAutosaveToolStripMenuItem.Enabled = true;
                loadAutosaveEnabled = true;
            }

            foreach (var item in menuMainForm.Items)
            {
                if (item is ToolStripMenuItem menuItem && menuItem.Enabled)
                {
                    menuItem.Text = localization.GetString(menuItem.Name);

                    foreach (var moreItems in menuItem.DropDownItems)
                    {
                        if (moreItems is ToolStripMenuItem moreItem && moreItem.Enabled)
                        {
                            moreItem.Text = localization.GetString(moreItem.Name);
                        }
                    }
                }
            }

            // Disable the control again if the state is true
            if (loadAutosaveEnabled)
            {
                loadAutosaveToolStripMenuItem.Enabled = false;
            }

            foreach (TabControl tabControl in tabs.Cast<TabControl>())
            {
                foreach (TabPage tab in tabControl.Controls)
                {
                    if (tab != null)
                    {
                        tab.Text = localization.GetString(tab.Name);
                    }
                }
            }

            foreach (Control control in buttons)
            {
                if (control is Button button)
                {
                    button.Text = localization.GetString(button.Name);
                }
            }

            foreach (Control control in labels)
            {
                if (control is Label label)
                {
                    label.Text = localization.GetString(label.Name);
                }
            }
        }

        private void InitializeCustomSettings()
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.MainForm, "InitializeCustomSettings entered");

            if (GetSetSettings.GetAutosave)
            {
                Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "Autosave set to true");
                loadAutosaveToolStripMenuItem.Enabled = true;
            }
            else
            {
                Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "Autosave set to false");
                loadAutosaveToolStripMenuItem.Enabled = false;
            }

            if (GetSetSettings.GetSaveOnClose)
            {
                Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "SaveOnClose set");

                if (GetSetSettings.GetReloadOnStartup)
                {
                    ImportAutosave();
                }
            }
        }

        private void DoSave()
        {
            Logging.Write(LogEventEnum.Method, ProgramClassEnum.MainForm, "DoSave entered");

            string[] colorIndexes = GetAllColorData();

            Autosave autosave = new();
            autosave.DoAutosave(Converter.LabelToString(lblCharName.Text), Converter.LabelToString(lblServerName.Text), colorIndexes);
        }

        // Find all Controls of the desired Type and pack them in a Control List
        private IEnumerable<Control> GetControls(Control parent, Type type)
        {
            var controls = parent.Controls.Cast<Control>();

            return controls
                .Where(c => c.GetType() == type)
                .Concat(controls.SelectMany(c => GetControls(c, type)));
        }

        private void AutosaveTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "AutosaveTimer elapsed");

            DoSave();
        }

        // Perform basic Checks when the Form is loading
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

        private async void MainForm_Load(object sender, EventArgs e)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            if (string.IsNullOrEmpty(GetSetSettings.GetCurrentLocale))
            {
                Logging.Write(LogEventEnum.Variable, ProgramClassEnum.MainForm, $"CurrentCulture set to: {CultureInfo.CurrentCulture.TwoLetterISOLanguageName}");
                GetSetSettings.SaveSettings(SettingsEnum.locale, CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            }

            Localize();

#if !DEBUG
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "Check if SWTOR is running");
            if (Checks.CheckSWTORprocessFound())
            {
                Localization localization = new(GetSetSettings.GetCurrentLocale);
                Logging.Write(LogEventEnum.Warning, ProgramClassEnum.MainForm, "SWTOR is running!");
                ShowMessageBox.Show(localization.GetString("MessageBoxWarn"), localization.GetString("Warn_SWTORrunning"));
            }
            else
            {
                Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "SWTOR is not running!");
            }
#endif

            Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "Set BackupOption in Menu");
            if (!Checks.DirectoryCheck(CheckFolderEnum.BackupFolder))
            {
                backupToolStripMenuItem.Enabled = false;
                restoreBackupToolStripMenuItem.Enabled = false;
                Logging.Write(LogEventEnum.Warning, ProgramClassEnum.MainForm, "BackupOption is not available!");
            }
            else
            {
                Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "BackupOption is available!");
            }

            Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "Set CharacterOption in Menu");
            if (string.IsNullOrEmpty(GetSetSettings.GetLocalPath))
            {
                charFolderToolStripMenuItem.Enabled = false;
                Logging.Write(LogEventEnum.Warning, ProgramClassEnum.MainForm, "CharacterOption is not available!");
                Localization localization = new(GetSetSettings.GetCurrentLocale);
                ShowMessageBox.Show(localization.GetString(LocalizationEnum.MessageBoxWarn), localization.GetString(LocalizationEnum.Warn_SWTORpathNotFound));
            }
            else
            {
                Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "CharacterOption is available!");
            }

            InitializeCustomSettings();

            Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "MainForm is loading");

            DownloadProgressReporter.DownloadProgressChanged += DownloadProgressReporter_DownloadProgressChanged;

#if !DEBUG
            if (Checks.CheckForInternetConnection())
            {
                await Updater.CheckForUpdateInterval();
            }
#endif
        }

        // Receive the DownloadProgress from the Updater
        private void DownloadProgressReporter_DownloadProgressChanged(object? sender, DownloadProgressEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateDownloadProgress(e.GetDownloadProgress())));
                return;
            }

            UpdateDownloadProgress(e.GetDownloadProgress());
        }

        // Display the Download Progress on the screen
        private void UpdateDownloadProgress(double progress)
        {
            if (!downloadProgressToolStripMenuItem.Visible)
            {
                downloadProgressToolStripMenuItem.Visible = true;
            }

            string text = Updater.GetUpdateDownloadText.Replace("PROGRESS", progress.ToString());
            downloadProgressToolStripMenuItem.Text = text;
        }

        // When the Form is closing, log it, remove the DownloadProgressReporter, stop the autosaveTimer and do a autosave
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "MainForm closing");

            DownloadProgressReporter.DownloadProgressChanged -= DownloadProgressReporter_DownloadProgressChanged;

            autosaveTimer?.Stop();

            if (GetSetSettings.GetSaveOnClose)
            {
                DoSave();
            }
        }

        // When the Form is closed, log it and stop the logger
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Logging.Write(LogEventEnum.Info, ProgramClassEnum.MainForm, "MainForm closed");
            Logging.Dispose();
        }

        // Draw the Tabs on the left side
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
}