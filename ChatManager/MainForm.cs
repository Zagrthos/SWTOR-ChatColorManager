using ChatManager.Properties;
using ChatManager.Services;

namespace ChatManager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            GetSetSettings.InitSettings();
            InitializeComponent();
        }

        // Button Click Handler for every button next to the TextBox
        private async void ClickChangeColorButton(object sender, EventArgs e)
        {
            await Logging.Write(LogEvent.Method, ProgramClass.MainForm, "ClickChangeColorButton Entered");
            await Logging.Write(LogEvent.Variable, ProgramClass.MainForm, $"Sender is: {sender}");

            // If the sender is a Button initialize it as button
            if (sender is Button button)
            {
                await Logging.Write(LogEvent.Variable, ProgramClass.MainForm, $"Button is: {button.Name}");

                // If the button has a Tag initialize it as targetTextBox
                if (button.Tag is string targetTextBox)
                {
                    await Logging.Write(LogEvent.Variable, ProgramClass.MainForm, $"Button Tag is: {button.Tag}");

                    // Find the TextBox...
                    Control? control = Controls.Find(targetTextBox, true).FirstOrDefault();

                    // ... and initialize it as textBox
                    if (control is TextBox textBox)
                    {
                        await Logging.Write(LogEvent.Variable, ProgramClass.MainForm, $"TextBox is: {textBox.Name}");
                        await Logging.Write(LogEvent.Variable, ProgramClass.MainForm, $"TextBox.Text is: {textBox.Text}");

                        // Check if text is Hex
                        string textBoxText = string.Empty;
                        if (Checks.CheckHexString(textBox.Text))
                        {
                            textBoxText = textBox.Text;
                            await Logging.Write(LogEvent.Info, ProgramClass.MainForm, $"TextBox.Text is Hex");
                            await Logging.Write(LogEvent.Variable, ProgramClass.MainForm, $"Hex Text is: {textBoxText}");
                        }

                        // Get the Text from it, if it is not Empty
                        if (!string.IsNullOrEmpty(textBoxText))
                        {
                            Color color = await Converter.HexToRGBAsync(textBoxText);
                            textBox.Text = await OpenWindows.OpenColorPicker(button.Text, color);
                            await Logging.Write(LogEvent.Variable, ProgramClass.MainForm, $"New hexColor is: {textBox.Text}");
                        }
                        else
                        {
                            await Logging.Write(LogEvent.Warning, ProgramClass.MainForm, "String is empty! Not starting conversion process");
                            await ShowMessageBox.Show(Resources.MessageBoxWarn, Resources.Warn_TextBoxEmpty);
                        }
                    }
                    else
                    {
                        await Logging.Write(LogEvent.Warning, ProgramClass.MainForm, $"TextBox: {targetTextBox} not found!");
                        await ShowMessageBox.ShowBug();
                    }
                }
                else
                {
                    await Logging.Write(LogEvent.Warning, ProgramClass.MainForm, $"Button: {button.Name} has no Tag!");
                    await ShowMessageBox.ShowBug();
                }
            }
            else
            {
                await Logging.Write(LogEvent.Warning, ProgramClass.MainForm, $"Sender: {sender} is not a Button!");
            }
        }

        // ToolStripMenu Program Handler
        private async void ToolStripMenuHandler(object sender, EventArgs e)
        {
            await Logging.Write(LogEvent.Method, ProgramClass.MainForm, "ToolStripMainMenuHandler entered");

            // Check if sender is a ToolStripMenuItem
            if (sender is ToolStripMenuItem menuItem)
            {
                // Initialize variable and declare path based on the name of the menuItem
                string path = menuItem.Name switch
                {
                    "charFolderToolStripMenuItem" => GetSetSettings.GetLocalPath,
                    "logFolderToolStripMenuItem" => GetSetSettings.GetLogPath,
                    "backupToolStripMenuItem" => GetSetSettings.GetBackupPath,
                    "supportToolStripMenuItem" => GetSetSettings.GetSupportPath,
                    "bugToolStripMenuItem" => GetSetSettings.GetBugPath,
                    "bugMailToolStripMenuItem" => GetSetSettings.GetBugMailpath,
                    _ => "%SYSTEMDRIVE%",
                };

                switch (menuItem.Name)
                {
                    case "supportToolStripMenuItem":
                        await Logging.Write(LogEvent.Method, ProgramClass.MainForm, "Support Site requested");
                        await OpenWindows.OpenLinksInBrowser(path);
                        return;

                    case "bugToolStripMenuItem":
                        await Logging.Write(LogEvent.Method, ProgramClass.MainForm, "Bug report Site requested");
                        await OpenWindows.OpenLinksInBrowser(path);
                        return;

                    case "bugMailToolStripMenuItem":
                        Logging.Write(LogEvent.Method, ProgramClass.MainForm, "Bug report Site requested");
                        OpenWindows.OpenProcess(path);
                        return;

                    case "aboutToolStripMenuItem":
                        await Logging.Write(LogEvent.Method, ProgramClass.MainForm, "About Form requested");
                        await OpenWindows.OpenAbout();
                        return;
                }

                await Logging.Write(LogEvent.Info, ProgramClass.MainForm, "Check if local Path exists");
                if (Checks.CheckIfPathExists(path))
                {
                    // Open Explorer in given path
                    await Logging.Write(LogEvent.Info, ProgramClass.MainForm, "Local Path exists!");
                    await OpenWindows.OpenExplorer(path);
                }
                else
                {
                    await Logging.Write(LogEvent.Warning, ProgramClass.MainForm, "Local Path does not exist!");
                    await ShowMessageBox.ShowBug();
                }

            }
            else
            {
                await Logging.Write(LogEvent.Warning, ProgramClass.MainForm, $"Sender: {sender} is not a ToolStripMenuItem!");
            }
        }

        // Import the colorIndexes from the given File into all textBoxes
        private async void ImportFile(object sender, EventArgs e)
        {
            await Logging.Write(LogEvent.Method, ProgramClass.MainForm, "ImportFile entered");
            FileImport fileImport = new();

            // Get the selectedFile and the selectedListBox from the tuple
            (string selectedFile, string selectedListBox) = OpenWindows.OpenFileImportSelector();
            await Logging.Write(LogEvent.Info, ProgramClass.MainForm, $"selectedFile: {selectedFile}");
            await Logging.Write(LogEvent.Info, ProgramClass.MainForm, $"selectedListBox: {selectedListBox}");

            if (!string.IsNullOrEmpty(selectedFile))
            {
                // Get the whole Character List for the requested name
                string[,] filePaths = fileImport.GetArray($"{selectedListBox.Substring(3)}");
                string[] colorIndexes;

                // Loop through and set the colors to the corresponding textBox
                for (int i = 0; i < 100; i++)
                {
                    if (selectedFile == filePaths[i, 0])
                    {
                        // But get the correct Colors from the right character file
                        string filePath = filePaths[i, 1];
                        colorIndexes = await fileImport.GetContentFromFile(filePath);

                        tbTrade.Text = colorIndexes[1];
                        tbPvP.Text = colorIndexes[2];
                        tbGeneral.Text = colorIndexes[3];
                        tbEmote.Text = colorIndexes[4];
                        tbYell.Text = colorIndexes[5];
                        tbOfficer.Text = colorIndexes[6];
                        tbGuild.Text = colorIndexes[7];
                        tbSay.Text = colorIndexes[8];
                        tbWhisper.Text = colorIndexes[9];
                        tbOps.Text = colorIndexes[10];
                        tbOpsLead.Text = colorIndexes[11];
                        tbGroup.Text = colorIndexes[12];
                        tbOpsAnnou.Text = colorIndexes[13];
                        tbOpsOfficer.Text = colorIndexes[14];
                        tbCombat.Text = colorIndexes[15];
                        tbConv.Text = colorIndexes[16];
                        tbLogin.Text = colorIndexes[17];
                        tbOpsInfo.Text = colorIndexes[18];
                        tbSystem.Text = colorIndexes[19];
                        tbGuildInfo.Text = colorIndexes[20];
                        tbGroupInfo.Text = colorIndexes[21];

                        break;
                    }
                }
            }
        }

        private async void ExportFiles(object sender, EventArgs e)
        {
            await Logging.Write(LogEvent.Method, ProgramClass.MainForm, "ExportFiles entered");

            string[] colorIndexes = new string[100];
            colorIndexes[0] = tbSay.Text;
            colorIndexes[1] = tbYell.Text;
            colorIndexes[2] = tbEmote.Text;
            colorIndexes[3] = tbWhisper.Text;
            colorIndexes[4] = "";
            colorIndexes[5] = "";
            colorIndexes[6] = tbGeneral.Text;
            colorIndexes[7] = tbTrade.Text;
            colorIndexes[8] = tbPvP.Text;
            colorIndexes[9] = tbGroup.Text;
            colorIndexes[10] = tbGuild.Text;
            colorIndexes[11] = tbOfficer.Text;
            colorIndexes[12] = tbOps.Text;
            colorIndexes[13] = tbOpsOfficer.Text;
            colorIndexes[14] = "";
            colorIndexes[15] = "";
            colorIndexes[16] = "";
            colorIndexes[17] = "";
            colorIndexes[18] = tbSystem.Text;
            colorIndexes[19] = tbConv.Text;
            colorIndexes[20] = tbLogin.Text;
            colorIndexes[21] = "";
            colorIndexes[22] = "";
            colorIndexes[23] = "";
            colorIndexes[24] = "";
            colorIndexes[25] = "";
            colorIndexes[26] = "";
            colorIndexes[27] = "";
            colorIndexes[28] = "";
            colorIndexes[29] = tbOpsLead.Text;
            colorIndexes[30] = "";
            colorIndexes[31] = "";
            colorIndexes[32] = tbOpsAnnou.Text;
            colorIndexes[33] = tbOpsInfo.Text;
            colorIndexes[34] = tbGroupInfo.Text;
            colorIndexes[35] = tbGuildInfo.Text;
            colorIndexes[36] = tbCombat.Text;
            colorIndexes[37] = "";
            colorIndexes[38] = "";
            colorIndexes[39] = "";
            colorIndexes[40] = "";
            colorIndexes[41] = "";
            colorIndexes[42] = "";
            colorIndexes[43] = "";
            colorIndexes[44] = "";
            colorIndexes[45] = "";
            colorIndexes[46] = "";
            colorIndexes[47] = "";
            colorIndexes[48] = "";
            colorIndexes[49] = "";
            colorIndexes[50] = "";
            colorIndexes[51] = "";
            colorIndexes[52] = "";
            colorIndexes[53] = "";
            colorIndexes[54] = "";
            colorIndexes[55] = "";
            colorIndexes[56] = "";
            colorIndexes[57] = "";
            colorIndexes[58] = "";
            colorIndexes[59] = "";
            colorIndexes[60] = "";
            colorIndexes[61] = "";
            colorIndexes[62] = "";
            colorIndexes[63] = "";
            colorIndexes[64] = "";
            colorIndexes[65] = "";
            colorIndexes[66] = "";
            colorIndexes[67] = "";
            colorIndexes[68] = "";
            colorIndexes[69] = "";
            colorIndexes[70] = "";
            colorIndexes[71] = "";
            colorIndexes[72] = "";
            colorIndexes[73] = "";
            colorIndexes[74] = "";
            colorIndexes[75] = "";
            colorIndexes[76] = "";
            colorIndexes[77] = "";
            colorIndexes[78] = "";
            colorIndexes[79] = "";
            colorIndexes[80] = "";
            colorIndexes[81] = "";
            colorIndexes[82] = "";
            colorIndexes[83] = "";
            colorIndexes[84] = "";
            colorIndexes[85] = "";
            colorIndexes[86] = "";
            colorIndexes[87] = "";
            colorIndexes[88] = "";
            colorIndexes[89] = "";
            colorIndexes[90] = "";
            colorIndexes[91] = "";
            colorIndexes[92] = "";
            colorIndexes[93] = "";
            colorIndexes[94] = "";
            colorIndexes[95] = "";
            colorIndexes[96] = "";
            colorIndexes[97] = "";
            colorIndexes[98] = "";
            colorIndexes[99] = "";

            await OpenWindows.OpenFileExportSelector(colorIndexes);
        }

        private async void CloseApplication(object sender, EventArgs e)
        {
            await Logging.Write(LogEvent.Method, ProgramClass.MainForm, "Application exit requested");
            Application.Exit();
        }

        // When the Form is loading, initialize the logger and log it
        private async void MainForm_Load(object sender, EventArgs e)
        {
            await Logging.Initialize();
            await Logging.Write(LogEvent.Info, ProgramClass.MainForm, "Check if SWTOR is running");
            if (Checks.CheckSWTORprocessFound())
            {
                await Logging.Write(LogEvent.Warning, ProgramClass.MainForm, "SWTOR is running!");
                await ShowMessageBox.Show(Resources.MessageBoxWarn, Resources.Warn_SWTORrunning);
            }
            else
            {
                await Logging.Write(LogEvent.Info, ProgramClass.MainForm, "SWTOR is not running!");
            }

            await Logging.Write(LogEvent.Info, ProgramClass.MainForm, "Set BackupOption in Menu");
            if (!await Checks.BackupDirectory())
            {
                backupToolStripMenuItem.Enabled = false;
                await Logging.Write(LogEvent.Warning, ProgramClass.MainForm, "BackupOption is not available!");
            }
            else
            {
                await Logging.Write(LogEvent.Info, ProgramClass.MainForm, "BackupOption is available!");
            }

            await Logging.Write(LogEvent.Info, ProgramClass.MainForm, "MainForm is loading");

            await Updater.CheckForUpdates();
        }

        // When the Form is closing, log it
        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            await Logging.Write(LogEvent.Info, ProgramClass.MainForm, "MainForm is closing");
        }

        // When the Form is closed, log it and stop the logger
        private async void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            await Logging.Write(LogEvent.Info, ProgramClass.MainForm, "MainForm closed");
            await Logging.Finalize();
        }

        // Draw the Tabs on the left side
        private void TabsMainForm_DrawItem(object sender, DrawItemEventArgs e)
        {
            // TODO: Review Logging
            //Logging.Write(LogEvent.Method, ProgramClass.MainForm, "TabsMainForm_DrawItem entered").ConfigureAwait(false);
            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = tabsMainForm.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tabsMainForm.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {
                // Draw a different background color, and don't paint a focus rectangle.
                _textBrush = new SolidBrush(Color.Black);
                g.FillRectangle(Brushes.LightGray, e.Bounds);
            }
            else
            {
                _textBrush = new SolidBrush(e.ForeColor);
                e.DrawBackground();
            }

            // Use our own font.
            Font _tabFont = new("Segoe UI", 9f);

            // Draw string. Center the text.
            StringFormat _stringFlags = new()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        }
    }
}