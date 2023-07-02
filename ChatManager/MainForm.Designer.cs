namespace ChatManager
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tlpMainForm = new TableLayoutPanel();
            menuMainForm = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator = new ToolStripSeparator();
            saveToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            loadAutosaveToolStripMenuItem = new ToolStripMenuItem();
            restoreBackupToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            charFolderToolStripMenuItem = new ToolStripMenuItem();
            backupToolStripMenuItem = new ToolStripMenuItem();
            logFolderToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            supportToolStripMenuItem = new ToolStripMenuItem();
            bugToolStripMenuItem = new ToolStripMenuItem();
            bugMailToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            updateToolStripMenuItem = new ToolStripMenuItem();
            downloadProgressToolStripMenuItem = new ToolStripMenuItem();
            tabsMainForm = new TabControl();
            tpGlobal = new TabPage();
            tlpGlobal = new TableLayoutPanel();
            btnGeneral = new Button();
            btnPvP = new Button();
            btnTrade = new Button();
            tbTrade = new TextBox();
            tbPvP = new TextBox();
            tbGeneral = new TextBox();
            tpPlayer = new TabPage();
            tlpPlayer = new TableLayoutPanel();
            btnWhisper = new Button();
            btnSay = new Button();
            btnGuild = new Button();
            btnOfficer = new Button();
            btnYell = new Button();
            btnEmote = new Button();
            tbEmote = new TextBox();
            tbYell = new TextBox();
            tbOfficer = new TextBox();
            tbGuild = new TextBox();
            tbSay = new TextBox();
            tbWhisper = new TextBox();
            tpGroup = new TabPage();
            tlpGroup = new TableLayoutPanel();
            btnOpsOfficer = new Button();
            btnOpsAnnou = new Button();
            btnGroup = new Button();
            btnOpsLead = new Button();
            btnOps = new Button();
            tbOps = new TextBox();
            tbOpsLead = new TextBox();
            tbGroup = new TextBox();
            tbOpsAnnou = new TextBox();
            tbOpsOfficer = new TextBox();
            tpSystem = new TabPage();
            tlpSystem = new TableLayoutPanel();
            btnGroupInfo = new Button();
            btnGuildInfo = new Button();
            tbGroupInfo = new TextBox();
            btnSystem = new Button();
            btnOpsInfo = new Button();
            tbGuildInfo = new TextBox();
            btnLogin = new Button();
            btnConv = new Button();
            tbSystem = new TextBox();
            btnCombat = new Button();
            tbCombat = new TextBox();
            tbOpsInfo = new TextBox();
            tbConv = new TextBox();
            tbLogin = new TextBox();
            lblCharName = new Label();
            lblServerName = new Label();
            tlpMainForm.SuspendLayout();
            menuMainForm.SuspendLayout();
            tabsMainForm.SuspendLayout();
            tpGlobal.SuspendLayout();
            tlpGlobal.SuspendLayout();
            tpPlayer.SuspendLayout();
            tlpPlayer.SuspendLayout();
            tpGroup.SuspendLayout();
            tlpGroup.SuspendLayout();
            tpSystem.SuspendLayout();
            tlpSystem.SuspendLayout();
            SuspendLayout();
            // 
            // tlpMainForm
            // 
            tlpMainForm.ColumnCount = 2;
            tlpMainForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpMainForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpMainForm.Controls.Add(menuMainForm, 0, 0);
            tlpMainForm.Controls.Add(tabsMainForm, 0, 1);
            tlpMainForm.Controls.Add(lblCharName, 0, 2);
            tlpMainForm.Controls.Add(lblServerName, 1, 2);
            tlpMainForm.Dock = DockStyle.Fill;
            tlpMainForm.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tlpMainForm.Location = new Point(0, 0);
            tlpMainForm.Name = "tlpMainForm";
            tlpMainForm.RowCount = 3;
            tlpMainForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tlpMainForm.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMainForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tlpMainForm.Size = new Size(484, 286);
            tlpMainForm.TabIndex = 0;
            // 
            // menuMainForm
            // 
            tlpMainForm.SetColumnSpan(menuMainForm, 2);
            menuMainForm.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, toolsToolStripMenuItem, helpToolStripMenuItem, downloadProgressToolStripMenuItem });
            menuMainForm.Location = new Point(0, 0);
            menuMainForm.Name = "menuMainForm";
            menuMainForm.RenderMode = ToolStripRenderMode.Professional;
            menuMainForm.Size = new Size(484, 24);
            menuMainForm.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, toolStripSeparator, saveToolStripMenuItem, toolStripSeparator1, loadAutosaveToolStripMenuItem, restoreBackupToolStripMenuItem, toolStripSeparator2, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(155, 22);
            openToolStripMenuItem.Text = "Import";
            openToolStripMenuItem.Click += ImportFile;
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(152, 6);
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(155, 22);
            saveToolStripMenuItem.Text = "Export";
            saveToolStripMenuItem.Click += ExportFiles;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(152, 6);
            // 
            // loadAutosaveToolStripMenuItem
            // 
            loadAutosaveToolStripMenuItem.Name = "loadAutosaveToolStripMenuItem";
            loadAutosaveToolStripMenuItem.Size = new Size(155, 22);
            loadAutosaveToolStripMenuItem.Text = "Load autosave";
            loadAutosaveToolStripMenuItem.Click += ToolStripMenuHandler;
            // 
            // restoreBackupToolStripMenuItem
            // 
            restoreBackupToolStripMenuItem.Name = "restoreBackupToolStripMenuItem";
            restoreBackupToolStripMenuItem.Size = new Size(155, 22);
            restoreBackupToolStripMenuItem.Text = "Restore backup";
            restoreBackupToolStripMenuItem.Click += ToolStripMenuHandler;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(152, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(155, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += ToolStripMenuHandler;
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { charFolderToolStripMenuItem, backupToolStripMenuItem, logFolderToolStripMenuItem, settingsToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(46, 20);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // charFolderToolStripMenuItem
            // 
            charFolderToolStripMenuItem.Name = "charFolderToolStripMenuItem";
            charFolderToolStripMenuItem.Size = new Size(189, 22);
            charFolderToolStripMenuItem.Text = "Open character folder";
            charFolderToolStripMenuItem.Click += ToolStripMenuHandler;
            // 
            // backupToolStripMenuItem
            // 
            backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            backupToolStripMenuItem.Size = new Size(189, 22);
            backupToolStripMenuItem.Text = "Open backup folder";
            backupToolStripMenuItem.Click += ToolStripMenuHandler;
            // 
            // logFolderToolStripMenuItem
            // 
            logFolderToolStripMenuItem.Name = "logFolderToolStripMenuItem";
            logFolderToolStripMenuItem.Size = new Size(189, 22);
            logFolderToolStripMenuItem.Text = "Open log files";
            logFolderToolStripMenuItem.Click += ToolStripMenuHandler;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(189, 22);
            settingsToolStripMenuItem.Text = "Settings";
            settingsToolStripMenuItem.Click += ToolStripMenuHandler;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { supportToolStripMenuItem, bugToolStripMenuItem, bugMailToolStripMenuItem, toolStripSeparator5, aboutToolStripMenuItem, updateToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // supportToolStripMenuItem
            // 
            supportToolStripMenuItem.Name = "supportToolStripMenuItem";
            supportToolStripMenuItem.Size = new Size(182, 22);
            supportToolStripMenuItem.Text = "Support";
            supportToolStripMenuItem.Click += ToolStripMenuHandler;
            // 
            // bugToolStripMenuItem
            // 
            bugToolStripMenuItem.Name = "bugToolStripMenuItem";
            bugToolStripMenuItem.Size = new Size(182, 22);
            bugToolStripMenuItem.Text = "Report bug (GitHub)";
            bugToolStripMenuItem.Click += ToolStripMenuHandler;
            // 
            // bugMailToolStripMenuItem
            // 
            bugMailToolStripMenuItem.Name = "bugMailToolStripMenuItem";
            bugMailToolStripMenuItem.Size = new Size(182, 22);
            bugMailToolStripMenuItem.Text = "Report bug (E-Mail)";
            bugMailToolStripMenuItem.Click += ToolStripMenuHandler;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(179, 6);
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(182, 22);
            aboutToolStripMenuItem.Text = "About...";
            aboutToolStripMenuItem.Click += ToolStripMenuHandler;
            // 
            // updateToolStripMenuItem
            // 
            updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            updateToolStripMenuItem.Size = new Size(182, 22);
            updateToolStripMenuItem.Text = "Check for updates";
            updateToolStripMenuItem.Click += ToolStripMenuHandler;
            // 
            // downloadProgressToolStripMenuItem
            // 
            downloadProgressToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            downloadProgressToolStripMenuItem.Name = "downloadProgressToolStripMenuItem";
            downloadProgressToolStripMenuItem.Size = new Size(193, 20);
            downloadProgressToolStripMenuItem.Text = "Download progress: PROGRESS%";
            downloadProgressToolStripMenuItem.Visible = false;
            // 
            // tabsMainForm
            // 
            tabsMainForm.Alignment = TabAlignment.Left;
            tlpMainForm.SetColumnSpan(tabsMainForm, 2);
            tabsMainForm.Controls.Add(tpGlobal);
            tabsMainForm.Controls.Add(tpPlayer);
            tabsMainForm.Controls.Add(tpGroup);
            tabsMainForm.Controls.Add(tpSystem);
            tabsMainForm.Dock = DockStyle.Fill;
            tabsMainForm.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabsMainForm.ItemSize = new Size(25, 100);
            tabsMainForm.Location = new Point(3, 28);
            tabsMainForm.Multiline = true;
            tabsMainForm.Name = "tabsMainForm";
            tabsMainForm.SelectedIndex = 0;
            tabsMainForm.Size = new Size(478, 230);
            tabsMainForm.SizeMode = TabSizeMode.Fixed;
            tabsMainForm.TabIndex = 1;
            tabsMainForm.DrawItem += TabsMainForm_DrawItem;
            // 
            // tpGlobal
            // 
            tpGlobal.Controls.Add(tlpGlobal);
            tpGlobal.Location = new Point(104, 4);
            tpGlobal.Name = "tpGlobal";
            tpGlobal.Size = new Size(370, 222);
            tpGlobal.TabIndex = 0;
            tpGlobal.Text = "Global channels";
            tpGlobal.UseVisualStyleBackColor = true;
            // 
            // tlpGlobal
            // 
            tlpGlobal.ColumnCount = 2;
            tlpGlobal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpGlobal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpGlobal.Controls.Add(btnGeneral, 1, 2);
            tlpGlobal.Controls.Add(btnPvP, 1, 1);
            tlpGlobal.Controls.Add(btnTrade, 1, 0);
            tlpGlobal.Controls.Add(tbTrade, 0, 0);
            tlpGlobal.Controls.Add(tbPvP, 0, 1);
            tlpGlobal.Controls.Add(tbGeneral, 0, 2);
            tlpGlobal.Dock = DockStyle.Fill;
            tlpGlobal.Location = new Point(0, 0);
            tlpGlobal.Name = "tlpGlobal";
            tlpGlobal.RowCount = 3;
            tlpGlobal.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33334F));
            tlpGlobal.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333244F));
            tlpGlobal.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tlpGlobal.Size = new Size(370, 222);
            tlpGlobal.TabIndex = 0;
            // 
            // btnGeneral
            // 
            btnGeneral.Dock = DockStyle.Fill;
            btnGeneral.Location = new Point(188, 150);
            btnGeneral.Name = "btnGeneral";
            btnGeneral.Size = new Size(179, 69);
            btnGeneral.TabIndex = 7;
            btnGeneral.Tag = "tbGeneral";
            btnGeneral.Text = "General";
            btnGeneral.UseVisualStyleBackColor = true;
            btnGeneral.Click += ClickChangeColorButton;
            // 
            // btnPvP
            // 
            btnPvP.Dock = DockStyle.Fill;
            btnPvP.Location = new Point(188, 77);
            btnPvP.Name = "btnPvP";
            btnPvP.Size = new Size(179, 67);
            btnPvP.TabIndex = 5;
            btnPvP.Tag = "tbPvP";
            btnPvP.Text = "PvP";
            btnPvP.UseVisualStyleBackColor = true;
            btnPvP.Click += ClickChangeColorButton;
            // 
            // btnTrade
            // 
            btnTrade.Dock = DockStyle.Fill;
            btnTrade.Location = new Point(188, 3);
            btnTrade.Name = "btnTrade";
            btnTrade.Size = new Size(179, 68);
            btnTrade.TabIndex = 3;
            btnTrade.Tag = "tbTrade";
            btnTrade.Text = "Trade";
            btnTrade.UseVisualStyleBackColor = true;
            btnTrade.Click += ClickChangeColorButton;
            // 
            // tbTrade
            // 
            tbTrade.Dock = DockStyle.Fill;
            tbTrade.Location = new Point(3, 3);
            tbTrade.Name = "tbTrade";
            tbTrade.Size = new Size(179, 23);
            tbTrade.TabIndex = 2;
            // 
            // tbPvP
            // 
            tbPvP.Dock = DockStyle.Fill;
            tbPvP.Location = new Point(3, 77);
            tbPvP.Name = "tbPvP";
            tbPvP.Size = new Size(179, 23);
            tbPvP.TabIndex = 4;
            // 
            // tbGeneral
            // 
            tbGeneral.Dock = DockStyle.Fill;
            tbGeneral.Location = new Point(3, 150);
            tbGeneral.Name = "tbGeneral";
            tbGeneral.Size = new Size(179, 23);
            tbGeneral.TabIndex = 6;
            // 
            // tpPlayer
            // 
            tpPlayer.Controls.Add(tlpPlayer);
            tpPlayer.Location = new Point(104, 4);
            tpPlayer.Name = "tpPlayer";
            tpPlayer.Size = new Size(370, 222);
            tpPlayer.TabIndex = 1;
            tpPlayer.Text = "Player channels";
            tpPlayer.UseVisualStyleBackColor = true;
            // 
            // tlpPlayer
            // 
            tlpPlayer.ColumnCount = 2;
            tlpPlayer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpPlayer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpPlayer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tlpPlayer.Controls.Add(btnWhisper, 1, 5);
            tlpPlayer.Controls.Add(btnSay, 1, 4);
            tlpPlayer.Controls.Add(btnGuild, 1, 3);
            tlpPlayer.Controls.Add(btnOfficer, 1, 2);
            tlpPlayer.Controls.Add(btnYell, 1, 1);
            tlpPlayer.Controls.Add(btnEmote, 1, 0);
            tlpPlayer.Controls.Add(tbEmote, 0, 0);
            tlpPlayer.Controls.Add(tbYell, 0, 1);
            tlpPlayer.Controls.Add(tbOfficer, 0, 2);
            tlpPlayer.Controls.Add(tbGuild, 0, 3);
            tlpPlayer.Controls.Add(tbSay, 0, 4);
            tlpPlayer.Controls.Add(tbWhisper, 0, 5);
            tlpPlayer.Dock = DockStyle.Fill;
            tlpPlayer.Location = new Point(0, 0);
            tlpPlayer.Name = "tlpPlayer";
            tlpPlayer.RowCount = 6;
            tlpPlayer.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tlpPlayer.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tlpPlayer.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tlpPlayer.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tlpPlayer.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tlpPlayer.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tlpPlayer.Size = new Size(370, 222);
            tlpPlayer.TabIndex = 0;
            // 
            // btnWhisper
            // 
            btnWhisper.Dock = DockStyle.Fill;
            btnWhisper.Location = new Point(188, 188);
            btnWhisper.Name = "btnWhisper";
            btnWhisper.Size = new Size(179, 31);
            btnWhisper.TabIndex = 19;
            btnWhisper.Tag = "tbWhisper";
            btnWhisper.Text = "Whisper";
            btnWhisper.UseVisualStyleBackColor = true;
            btnWhisper.Click += ClickChangeColorButton;
            // 
            // btnSay
            // 
            btnSay.Dock = DockStyle.Fill;
            btnSay.Location = new Point(188, 151);
            btnSay.Name = "btnSay";
            btnSay.Size = new Size(179, 31);
            btnSay.TabIndex = 17;
            btnSay.Tag = "tbSay";
            btnSay.Text = "Say";
            btnSay.UseVisualStyleBackColor = true;
            btnSay.Click += ClickChangeColorButton;
            // 
            // btnGuild
            // 
            btnGuild.Dock = DockStyle.Fill;
            btnGuild.Location = new Point(188, 114);
            btnGuild.Name = "btnGuild";
            btnGuild.Size = new Size(179, 31);
            btnGuild.TabIndex = 15;
            btnGuild.Tag = "tbGuild";
            btnGuild.Text = "Guild";
            btnGuild.UseVisualStyleBackColor = true;
            btnGuild.Click += ClickChangeColorButton;
            // 
            // btnOfficer
            // 
            btnOfficer.Dock = DockStyle.Fill;
            btnOfficer.Location = new Point(188, 77);
            btnOfficer.Name = "btnOfficer";
            btnOfficer.Size = new Size(179, 31);
            btnOfficer.TabIndex = 13;
            btnOfficer.Tag = "tbOfficer";
            btnOfficer.Text = "Officer";
            btnOfficer.UseVisualStyleBackColor = true;
            btnOfficer.Click += ClickChangeColorButton;
            // 
            // btnYell
            // 
            btnYell.Dock = DockStyle.Fill;
            btnYell.Location = new Point(188, 40);
            btnYell.Name = "btnYell";
            btnYell.Size = new Size(179, 31);
            btnYell.TabIndex = 11;
            btnYell.Tag = "tbYell";
            btnYell.Text = "Yell";
            btnYell.UseVisualStyleBackColor = true;
            btnYell.Click += ClickChangeColorButton;
            // 
            // btnEmote
            // 
            btnEmote.Dock = DockStyle.Fill;
            btnEmote.Location = new Point(188, 3);
            btnEmote.Name = "btnEmote";
            btnEmote.Size = new Size(179, 31);
            btnEmote.TabIndex = 9;
            btnEmote.Tag = "tbEmote";
            btnEmote.Text = "Emote";
            btnEmote.UseVisualStyleBackColor = true;
            btnEmote.Click += ClickChangeColorButton;
            // 
            // tbEmote
            // 
            tbEmote.Dock = DockStyle.Fill;
            tbEmote.Location = new Point(3, 3);
            tbEmote.Name = "tbEmote";
            tbEmote.Size = new Size(179, 23);
            tbEmote.TabIndex = 8;
            // 
            // tbYell
            // 
            tbYell.Dock = DockStyle.Fill;
            tbYell.Location = new Point(3, 40);
            tbYell.Name = "tbYell";
            tbYell.Size = new Size(179, 23);
            tbYell.TabIndex = 10;
            // 
            // tbOfficer
            // 
            tbOfficer.Dock = DockStyle.Fill;
            tbOfficer.Location = new Point(3, 77);
            tbOfficer.Name = "tbOfficer";
            tbOfficer.Size = new Size(179, 23);
            tbOfficer.TabIndex = 12;
            // 
            // tbGuild
            // 
            tbGuild.Dock = DockStyle.Fill;
            tbGuild.Location = new Point(3, 114);
            tbGuild.Name = "tbGuild";
            tbGuild.Size = new Size(179, 23);
            tbGuild.TabIndex = 14;
            // 
            // tbSay
            // 
            tbSay.Dock = DockStyle.Fill;
            tbSay.Location = new Point(3, 151);
            tbSay.Name = "tbSay";
            tbSay.Size = new Size(179, 23);
            tbSay.TabIndex = 16;
            // 
            // tbWhisper
            // 
            tbWhisper.Dock = DockStyle.Fill;
            tbWhisper.Location = new Point(3, 188);
            tbWhisper.Name = "tbWhisper";
            tbWhisper.Size = new Size(179, 23);
            tbWhisper.TabIndex = 18;
            // 
            // tpGroup
            // 
            tpGroup.Controls.Add(tlpGroup);
            tpGroup.Location = new Point(104, 4);
            tpGroup.Name = "tpGroup";
            tpGroup.Size = new Size(370, 222);
            tpGroup.TabIndex = 2;
            tpGroup.Text = "Group channels";
            tpGroup.UseVisualStyleBackColor = true;
            // 
            // tlpGroup
            // 
            tlpGroup.ColumnCount = 2;
            tlpGroup.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpGroup.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpGroup.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tlpGroup.Controls.Add(btnOpsOfficer, 1, 4);
            tlpGroup.Controls.Add(btnOpsAnnou, 1, 3);
            tlpGroup.Controls.Add(btnGroup, 1, 2);
            tlpGroup.Controls.Add(btnOpsLead, 1, 1);
            tlpGroup.Controls.Add(btnOps, 1, 0);
            tlpGroup.Controls.Add(tbOps, 0, 0);
            tlpGroup.Controls.Add(tbOpsLead, 0, 1);
            tlpGroup.Controls.Add(tbGroup, 0, 2);
            tlpGroup.Controls.Add(tbOpsAnnou, 0, 3);
            tlpGroup.Controls.Add(tbOpsOfficer, 0, 4);
            tlpGroup.Dock = DockStyle.Fill;
            tlpGroup.Location = new Point(0, 0);
            tlpGroup.Name = "tlpGroup";
            tlpGroup.RowCount = 5;
            tlpGroup.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpGroup.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpGroup.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpGroup.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpGroup.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpGroup.Size = new Size(370, 222);
            tlpGroup.TabIndex = 0;
            // 
            // btnOpsOfficer
            // 
            btnOpsOfficer.Dock = DockStyle.Fill;
            btnOpsOfficer.Location = new Point(188, 179);
            btnOpsOfficer.Name = "btnOpsOfficer";
            btnOpsOfficer.Size = new Size(179, 40);
            btnOpsOfficer.TabIndex = 29;
            btnOpsOfficer.Tag = "tbOpsOfficer";
            btnOpsOfficer.Text = "Ops officer";
            btnOpsOfficer.UseVisualStyleBackColor = true;
            btnOpsOfficer.Click += ClickChangeColorButton;
            // 
            // btnOpsAnnou
            // 
            btnOpsAnnou.Dock = DockStyle.Fill;
            btnOpsAnnou.Location = new Point(188, 135);
            btnOpsAnnou.Name = "btnOpsAnnou";
            btnOpsAnnou.Size = new Size(179, 38);
            btnOpsAnnou.TabIndex = 27;
            btnOpsAnnou.Tag = "tbOpsAnnou";
            btnOpsAnnou.Text = "Ops announce";
            btnOpsAnnou.UseVisualStyleBackColor = true;
            btnOpsAnnou.Click += ClickChangeColorButton;
            // 
            // btnGroup
            // 
            btnGroup.Dock = DockStyle.Fill;
            btnGroup.Location = new Point(188, 91);
            btnGroup.Name = "btnGroup";
            btnGroup.Size = new Size(179, 38);
            btnGroup.TabIndex = 25;
            btnGroup.Tag = "tbGroup";
            btnGroup.Text = "Group";
            btnGroup.UseVisualStyleBackColor = true;
            btnGroup.Click += ClickChangeColorButton;
            // 
            // btnOpsLead
            // 
            btnOpsLead.Dock = DockStyle.Fill;
            btnOpsLead.Location = new Point(188, 47);
            btnOpsLead.Name = "btnOpsLead";
            btnOpsLead.Size = new Size(179, 38);
            btnOpsLead.TabIndex = 23;
            btnOpsLead.Tag = "tbOpsLead";
            btnOpsLead.Text = "Ops lead";
            btnOpsLead.UseVisualStyleBackColor = true;
            btnOpsLead.Click += ClickChangeColorButton;
            // 
            // btnOps
            // 
            btnOps.Dock = DockStyle.Fill;
            btnOps.Location = new Point(188, 3);
            btnOps.Name = "btnOps";
            btnOps.Size = new Size(179, 38);
            btnOps.TabIndex = 21;
            btnOps.Tag = "tbOps";
            btnOps.Text = "Ops";
            btnOps.UseVisualStyleBackColor = true;
            btnOps.Click += ClickChangeColorButton;
            // 
            // tbOps
            // 
            tbOps.Dock = DockStyle.Fill;
            tbOps.Location = new Point(3, 3);
            tbOps.Name = "tbOps";
            tbOps.Size = new Size(179, 23);
            tbOps.TabIndex = 20;
            // 
            // tbOpsLead
            // 
            tbOpsLead.Dock = DockStyle.Fill;
            tbOpsLead.Location = new Point(3, 47);
            tbOpsLead.Name = "tbOpsLead";
            tbOpsLead.Size = new Size(179, 23);
            tbOpsLead.TabIndex = 22;
            // 
            // tbGroup
            // 
            tbGroup.Dock = DockStyle.Fill;
            tbGroup.Location = new Point(3, 91);
            tbGroup.Name = "tbGroup";
            tbGroup.Size = new Size(179, 23);
            tbGroup.TabIndex = 24;
            // 
            // tbOpsAnnou
            // 
            tbOpsAnnou.Dock = DockStyle.Fill;
            tbOpsAnnou.Location = new Point(3, 135);
            tbOpsAnnou.Name = "tbOpsAnnou";
            tbOpsAnnou.Size = new Size(179, 23);
            tbOpsAnnou.TabIndex = 26;
            // 
            // tbOpsOfficer
            // 
            tbOpsOfficer.Dock = DockStyle.Fill;
            tbOpsOfficer.Location = new Point(3, 179);
            tbOpsOfficer.Name = "tbOpsOfficer";
            tbOpsOfficer.Size = new Size(179, 23);
            tbOpsOfficer.TabIndex = 28;
            // 
            // tpSystem
            // 
            tpSystem.Controls.Add(tlpSystem);
            tpSystem.Location = new Point(104, 4);
            tpSystem.Name = "tpSystem";
            tpSystem.Size = new Size(370, 222);
            tpSystem.TabIndex = 3;
            tpSystem.Text = "System channels";
            tpSystem.UseVisualStyleBackColor = true;
            // 
            // tlpSystem
            // 
            tlpSystem.ColumnCount = 2;
            tlpSystem.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.0000076F));
            tlpSystem.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpSystem.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tlpSystem.Controls.Add(btnGroupInfo, 1, 6);
            tlpSystem.Controls.Add(btnGuildInfo, 1, 5);
            tlpSystem.Controls.Add(tbGroupInfo, 0, 6);
            tlpSystem.Controls.Add(btnSystem, 1, 4);
            tlpSystem.Controls.Add(btnOpsInfo, 1, 3);
            tlpSystem.Controls.Add(tbGuildInfo, 0, 5);
            tlpSystem.Controls.Add(btnLogin, 1, 2);
            tlpSystem.Controls.Add(btnConv, 1, 1);
            tlpSystem.Controls.Add(tbSystem, 0, 4);
            tlpSystem.Controls.Add(btnCombat, 1, 0);
            tlpSystem.Controls.Add(tbCombat, 0, 0);
            tlpSystem.Controls.Add(tbOpsInfo, 0, 3);
            tlpSystem.Controls.Add(tbConv, 0, 1);
            tlpSystem.Controls.Add(tbLogin, 0, 2);
            tlpSystem.Dock = DockStyle.Fill;
            tlpSystem.Location = new Point(0, 0);
            tlpSystem.Name = "tlpSystem";
            tlpSystem.RowCount = 7;
            tlpSystem.RowStyles.Add(new RowStyle(SizeType.Percent, 14.4302235F));
            tlpSystem.RowStyles.Add(new RowStyle(SizeType.Percent, 14.43022F));
            tlpSystem.RowStyles.Add(new RowStyle(SizeType.Percent, 14.43022F));
            tlpSystem.RowStyles.Add(new RowStyle(SizeType.Percent, 14.43022F));
            tlpSystem.RowStyles.Add(new RowStyle(SizeType.Percent, 14.43022F));
            tlpSystem.RowStyles.Add(new RowStyle(SizeType.Percent, 14.4302235F));
            tlpSystem.RowStyles.Add(new RowStyle(SizeType.Percent, 13.4186764F));
            tlpSystem.Size = new Size(370, 222);
            tlpSystem.TabIndex = 0;
            // 
            // btnGroupInfo
            // 
            btnGroupInfo.Dock = DockStyle.Fill;
            btnGroupInfo.Location = new Point(188, 195);
            btnGroupInfo.Name = "btnGroupInfo";
            btnGroupInfo.Size = new Size(179, 24);
            btnGroupInfo.TabIndex = 43;
            btnGroupInfo.Tag = "tbGroupInfo";
            btnGroupInfo.Text = "Group info";
            btnGroupInfo.UseVisualStyleBackColor = true;
            btnGroupInfo.Click += ClickChangeColorButton;
            // 
            // btnGuildInfo
            // 
            btnGuildInfo.Dock = DockStyle.Fill;
            btnGuildInfo.Location = new Point(188, 163);
            btnGuildInfo.Name = "btnGuildInfo";
            btnGuildInfo.Size = new Size(179, 26);
            btnGuildInfo.TabIndex = 41;
            btnGuildInfo.Tag = "tbGuildInfo";
            btnGuildInfo.Text = "Guild info";
            btnGuildInfo.UseVisualStyleBackColor = true;
            btnGuildInfo.Click += ClickChangeColorButton;
            // 
            // tbGroupInfo
            // 
            tbGroupInfo.Dock = DockStyle.Fill;
            tbGroupInfo.Location = new Point(3, 195);
            tbGroupInfo.Name = "tbGroupInfo";
            tbGroupInfo.Size = new Size(179, 23);
            tbGroupInfo.TabIndex = 42;
            // 
            // btnSystem
            // 
            btnSystem.Dock = DockStyle.Fill;
            btnSystem.Location = new Point(188, 131);
            btnSystem.Name = "btnSystem";
            btnSystem.Size = new Size(179, 26);
            btnSystem.TabIndex = 39;
            btnSystem.Tag = "tbSystem";
            btnSystem.Text = "System feedback";
            btnSystem.UseVisualStyleBackColor = true;
            btnSystem.Click += ClickChangeColorButton;
            // 
            // btnOpsInfo
            // 
            btnOpsInfo.Dock = DockStyle.Fill;
            btnOpsInfo.Location = new Point(188, 99);
            btnOpsInfo.Name = "btnOpsInfo";
            btnOpsInfo.Size = new Size(179, 26);
            btnOpsInfo.TabIndex = 37;
            btnOpsInfo.Tag = "tbOpsInfo";
            btnOpsInfo.Text = "Ops info";
            btnOpsInfo.UseVisualStyleBackColor = true;
            btnOpsInfo.Click += ClickChangeColorButton;
            // 
            // tbGuildInfo
            // 
            tbGuildInfo.Dock = DockStyle.Fill;
            tbGuildInfo.Location = new Point(3, 163);
            tbGuildInfo.Name = "tbGuildInfo";
            tbGuildInfo.Size = new Size(179, 23);
            tbGuildInfo.TabIndex = 40;
            // 
            // btnLogin
            // 
            btnLogin.Dock = DockStyle.Fill;
            btnLogin.Location = new Point(188, 67);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(179, 26);
            btnLogin.TabIndex = 35;
            btnLogin.Tag = "tbLogin";
            btnLogin.Text = "Character login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += ClickChangeColorButton;
            // 
            // btnConv
            // 
            btnConv.Dock = DockStyle.Fill;
            btnConv.Location = new Point(188, 35);
            btnConv.Name = "btnConv";
            btnConv.Size = new Size(179, 26);
            btnConv.TabIndex = 33;
            btnConv.Tag = "tbConv";
            btnConv.Text = "Conversation";
            btnConv.UseVisualStyleBackColor = true;
            btnConv.Click += ClickChangeColorButton;
            // 
            // tbSystem
            // 
            tbSystem.Dock = DockStyle.Fill;
            tbSystem.Location = new Point(3, 131);
            tbSystem.Name = "tbSystem";
            tbSystem.Size = new Size(179, 23);
            tbSystem.TabIndex = 38;
            // 
            // btnCombat
            // 
            btnCombat.Dock = DockStyle.Fill;
            btnCombat.Location = new Point(188, 3);
            btnCombat.Name = "btnCombat";
            btnCombat.Size = new Size(179, 26);
            btnCombat.TabIndex = 31;
            btnCombat.Tag = "tbCombat";
            btnCombat.Text = "Combat info";
            btnCombat.UseVisualStyleBackColor = true;
            btnCombat.Click += ClickChangeColorButton;
            // 
            // tbCombat
            // 
            tbCombat.Dock = DockStyle.Fill;
            tbCombat.Location = new Point(3, 3);
            tbCombat.Name = "tbCombat";
            tbCombat.Size = new Size(179, 23);
            tbCombat.TabIndex = 30;
            // 
            // tbOpsInfo
            // 
            tbOpsInfo.Dock = DockStyle.Fill;
            tbOpsInfo.Location = new Point(3, 99);
            tbOpsInfo.Name = "tbOpsInfo";
            tbOpsInfo.Size = new Size(179, 23);
            tbOpsInfo.TabIndex = 36;
            // 
            // tbConv
            // 
            tbConv.Dock = DockStyle.Fill;
            tbConv.Location = new Point(3, 35);
            tbConv.Name = "tbConv";
            tbConv.Size = new Size(179, 23);
            tbConv.TabIndex = 32;
            // 
            // tbLogin
            // 
            tbLogin.Dock = DockStyle.Fill;
            tbLogin.Location = new Point(3, 67);
            tbLogin.Name = "tbLogin";
            tbLogin.Size = new Size(179, 23);
            tbLogin.TabIndex = 34;
            // 
            // lblCharName
            // 
            lblCharName.AutoSize = true;
            lblCharName.Dock = DockStyle.Fill;
            lblCharName.Location = new Point(3, 261);
            lblCharName.Name = "lblCharName";
            lblCharName.Size = new Size(236, 25);
            lblCharName.TabIndex = 0;
            lblCharName.Text = "Current character:";
            lblCharName.TextAlign = ContentAlignment.MiddleLeft;
            lblCharName.Visible = false;
            // 
            // lblServerName
            // 
            lblServerName.AutoSize = true;
            lblServerName.Dock = DockStyle.Fill;
            lblServerName.Location = new Point(245, 261);
            lblServerName.Name = "lblServerName";
            lblServerName.Size = new Size(236, 25);
            lblServerName.TabIndex = 0;
            lblServerName.Text = "Current server:";
            lblServerName.TextAlign = ContentAlignment.MiddleLeft;
            lblServerName.Visible = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 286);
            Controls.Add(tlpMainForm);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuMainForm;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SWTOR Chat Color Manager";
            FormClosing += MainForm_FormClosing;
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            tlpMainForm.ResumeLayout(false);
            tlpMainForm.PerformLayout();
            menuMainForm.ResumeLayout(false);
            menuMainForm.PerformLayout();
            tabsMainForm.ResumeLayout(false);
            tpGlobal.ResumeLayout(false);
            tlpGlobal.ResumeLayout(false);
            tlpGlobal.PerformLayout();
            tpPlayer.ResumeLayout(false);
            tlpPlayer.ResumeLayout(false);
            tlpPlayer.PerformLayout();
            tpGroup.ResumeLayout(false);
            tlpGroup.ResumeLayout(false);
            tlpGroup.PerformLayout();
            tpSystem.ResumeLayout(false);
            tlpSystem.ResumeLayout(false);
            tlpSystem.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpMainForm;
        private Button btnTrade;
        private Button btnPvP;
        private Button btnGeneral;
        private Button btnEmote;
        private Button btnYell;
        private Button btnOfficer;
        private Button btnGuild;
        private Button btnSay;
        private Button btnWhisper;
        private Button btnOps;
        private Button btnOpsLead;
        private Button btnGroup;
        private Button btnOpsAnnou;
        private Button btnOpsOfficer;
        private Button btnCombat;
        private Button btnConv;
        private Button btnLogin;
        private Button btnOpsInfo;
        private Button btnSystem;
        private Button btnGuildInfo;
        private Button btnGroupInfo;
        private TextBox tbTrade;
        private TextBox tbPvP;
        private TextBox tbGeneral;
        private TextBox tbEmote;
        private TextBox tbYell;
        private TextBox tbOfficer;
        private TextBox tbGuild;
        private TextBox tbSay;
        private TextBox tbWhisper;
        private TextBox tbOps;
        private TextBox tbOpsLead;
        private TextBox tbGroup;
        private TextBox tbOpsAnnou;
        private TextBox tbOpsOfficer;
        private TextBox tbCombat;
        private TextBox tbConv;
        private TextBox tbLogin;
        private TextBox tbOpsInfo;
        private TextBox tbSystem;
        private TextBox tbGuildInfo;
        private TextBox tbGroupInfo;
        private TabControl tabsMainForm;
        private TabPage tpGlobal;
        private TabPage tpPlayer;
        private TabPage tpGroup;
        private TabPage tpSystem;
        private TableLayoutPanel tlpGlobal;
        private TableLayoutPanel tlpPlayer;
        private TableLayoutPanel tlpGroup;
        private TableLayoutPanel tlpSystem;
        private MenuStrip menuMainForm;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem loadAutosaveToolStripMenuItem;
        private ToolStripMenuItem restoreBackupToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem charFolderToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem logFolderToolStripMenuItem;
        private ToolStripMenuItem supportToolStripMenuItem;
        private ToolStripMenuItem backupToolStripMenuItem;
        private ToolStripMenuItem bugToolStripMenuItem;
        private ToolStripMenuItem bugMailToolStripMenuItem;
        private ToolStripMenuItem updateToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private Label lblCharName;
        private Label lblServerName;
        private ToolStripMenuItem downloadProgressToolStripMenuItem;
    }
}