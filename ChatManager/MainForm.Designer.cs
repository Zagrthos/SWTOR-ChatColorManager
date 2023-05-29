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
            newToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator = new ToolStripSeparator();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            printToolStripMenuItem = new ToolStripMenuItem();
            printPreviewToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            cutToolStripMenuItem = new ToolStripMenuItem();
            copyToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            selectAllToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            charFolderToolStripMenuItem = new ToolStripMenuItem();
            backupToolStripMenuItem = new ToolStripMenuItem();
            logFolderToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            supportToolStripMenuItem = new ToolStripMenuItem();
            bugToolStripMenuItem = new ToolStripMenuItem();
            bugMailToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            updateToolStripMenuItem = new ToolStripMenuItem();
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
            tlpMainForm.ColumnCount = 1;
            tlpMainForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMainForm.Controls.Add(menuMainForm, 0, 0);
            tlpMainForm.Controls.Add(tabsMainForm, 0, 1);
            tlpMainForm.Dock = DockStyle.Fill;
            tlpMainForm.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tlpMainForm.Location = new Point(0, 0);
            tlpMainForm.Name = "tlpMainForm";
            tlpMainForm.RowCount = 2;
            tlpMainForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tlpMainForm.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMainForm.Size = new Size(584, 261);
            tlpMainForm.TabIndex = 0;
            // 
            // menuMainForm
            // 
            menuMainForm.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, toolsToolStripMenuItem, helpToolStripMenuItem });
            menuMainForm.Location = new Point(0, 0);
            menuMainForm.Name = "menuMainForm";
            menuMainForm.RenderMode = ToolStripRenderMode.Professional;
            menuMainForm.Size = new Size(584, 24);
            menuMainForm.TabIndex = 44;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, toolStripSeparator, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparator1, printToolStripMenuItem, printPreviewToolStripMenuItem, toolStripSeparator2, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 20);
            fileToolStripMenuItem.Text = "Datei";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Enabled = false;
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(153, 22);
            newToolStripMenuItem.Text = "Neu";
            newToolStripMenuItem.Visible = false;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(153, 22);
            openToolStripMenuItem.Text = "Öffnen";
            openToolStripMenuItem.Click += ImportFile;
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(150, 6);
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(153, 22);
            saveToolStripMenuItem.Text = "Speichern";
            saveToolStripMenuItem.Click += ExportFiles;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(153, 22);
            saveAsToolStripMenuItem.Text = "Speichern als...";
            saveAsToolStripMenuItem.Visible = false;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(150, 6);
            // 
            // printToolStripMenuItem
            // 
            printToolStripMenuItem.Enabled = false;
            printToolStripMenuItem.Name = "printToolStripMenuItem";
            printToolStripMenuItem.Size = new Size(153, 22);
            printToolStripMenuItem.Text = "Drucken";
            printToolStripMenuItem.Visible = false;
            // 
            // printPreviewToolStripMenuItem
            // 
            printPreviewToolStripMenuItem.Enabled = false;
            printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            printPreviewToolStripMenuItem.Size = new Size(153, 22);
            printPreviewToolStripMenuItem.Text = "Druckvorschau";
            printPreviewToolStripMenuItem.Visible = false;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(150, 6);
            toolStripSeparator2.Visible = false;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(153, 22);
            exitToolStripMenuItem.Text = "Beenden";
            exitToolStripMenuItem.Click += CloseApplication;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem, redoToolStripMenuItem, toolStripSeparator3, cutToolStripMenuItem, copyToolStripMenuItem, pasteToolStripMenuItem, toolStripSeparator4, selectAllToolStripMenuItem });
            editToolStripMenuItem.Enabled = false;
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(58, 20);
            editToolStripMenuItem.Text = "Ändern";
            editToolStripMenuItem.Visible = false;
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.Size = new Size(155, 22);
            undoToolStripMenuItem.Text = "Rückgängig";
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.Size = new Size(155, 22);
            redoToolStripMenuItem.Text = "Wiederholen";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(152, 6);
            // 
            // cutToolStripMenuItem
            // 
            cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            cutToolStripMenuItem.Size = new Size(155, 22);
            cutToolStripMenuItem.Text = "Ausschneiden";
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new Size(155, 22);
            copyToolStripMenuItem.Text = "Kopieren";
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.Size = new Size(155, 22);
            pasteToolStripMenuItem.Text = "Einfügen";
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(152, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            selectAllToolStripMenuItem.Size = new Size(155, 22);
            selectAllToolStripMenuItem.Text = "Alles markieren";
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { charFolderToolStripMenuItem, backupToolStripMenuItem, logFolderToolStripMenuItem, optionsToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(46, 20);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // charFolderToolStripMenuItem
            // 
            charFolderToolStripMenuItem.Name = "charFolderToolStripMenuItem";
            charFolderToolStripMenuItem.Size = new Size(209, 22);
            charFolderToolStripMenuItem.Text = "Charakter Ordner öffnen";
            charFolderToolStripMenuItem.Click += ToolStripMenuHandler;
            // 
            // backupToolStripMenuItem
            // 
            backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            backupToolStripMenuItem.Size = new Size(209, 22);
            backupToolStripMenuItem.Text = "Backupverzeichnis öffnen";
            backupToolStripMenuItem.Click += ToolStripMenuHandler;
            // 
            // logFolderToolStripMenuItem
            // 
            logFolderToolStripMenuItem.Name = "logFolderToolStripMenuItem";
            logFolderToolStripMenuItem.Size = new Size(209, 22);
            logFolderToolStripMenuItem.Text = "Log Dateien öffnen";
            logFolderToolStripMenuItem.Click += ToolStripMenuHandler;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.Enabled = false;
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(209, 22);
            optionsToolStripMenuItem.Text = "Einstellungen";
            optionsToolStripMenuItem.Visible = false;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { supportToolStripMenuItem, bugToolStripMenuItem, bugMailToolStripMenuItem, toolStripSeparator5, aboutToolStripMenuItem, updateToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Hilfe";
            // 
            // supportToolStripMenuItem
            // 
            supportToolStripMenuItem.Name = "supportToolStripMenuItem";
            supportToolStripMenuItem.Size = new Size(187, 22);
            supportToolStripMenuItem.Text = "Support";
            supportToolStripMenuItem.Click += ToolStripMenuHandler;
            // 
            // bugToolStripMenuItem
            // 
            bugToolStripMenuItem.Name = "bugToolStripMenuItem";
            bugToolStripMenuItem.Size = new Size(187, 22);
            bugToolStripMenuItem.Text = "Bug melden (GitHub)";
            bugToolStripMenuItem.Click += ToolStripMenuHandler;
            // 
            // bugMailToolStripMenuItem
            // 
            bugMailToolStripMenuItem.Name = "bugMailToolStripMenuItem";
            bugMailToolStripMenuItem.Size = new Size(187, 22);
            bugMailToolStripMenuItem.Text = "Bug melden (E-Mail)";
            bugMailToolStripMenuItem.Click += ToolStripMenuHandler;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(184, 6);
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(187, 22);
            aboutToolStripMenuItem.Text = "Über...";
            aboutToolStripMenuItem.Click += ToolStripMenuHandler;
            // 
            // updateToolStripMenuItem
            // 
            updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            updateToolStripMenuItem.Size = new Size(187, 22);
            updateToolStripMenuItem.Text = "Auf Updates prüfen";
            updateToolStripMenuItem.Click += ToolStripMenuHandler;
            // 
            // tabsMainForm
            // 
            tabsMainForm.Alignment = TabAlignment.Left;
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
            tabsMainForm.Size = new Size(578, 230);
            tabsMainForm.SizeMode = TabSizeMode.Fixed;
            tabsMainForm.TabIndex = 43;
            tabsMainForm.DrawItem += TabsMainForm_DrawItem;
            // 
            // tpGlobal
            // 
            tpGlobal.Controls.Add(tlpGlobal);
            tpGlobal.Location = new Point(104, 4);
            tpGlobal.Name = "tpGlobal";
            tpGlobal.Size = new Size(470, 222);
            tpGlobal.TabIndex = 0;
            tpGlobal.Text = "Standardkanäle";
            tpGlobal.UseVisualStyleBackColor = true;
            // 
            // tlpGlobal
            // 
            tlpGlobal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tlpGlobal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33334F));
            tlpGlobal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33334F));
            tlpGlobal.Controls.Add(btnGeneral, 2, 2);
            tlpGlobal.Controls.Add(btnPvP, 2, 1);
            tlpGlobal.Controls.Add(btnTrade, 2, 0);
            tlpGlobal.Controls.Add(tbTrade, 1, 0);
            tlpGlobal.Controls.Add(tbPvP, 1, 1);
            tlpGlobal.Controls.Add(tbGeneral, 1, 2);
            tlpGlobal.Dock = DockStyle.Fill;
            tlpGlobal.Location = new Point(0, 0);
            tlpGlobal.Name = "tlpGlobal";
            tlpGlobal.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33334F));
            tlpGlobal.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33332F));
            tlpGlobal.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333F));
            tlpGlobal.Size = new Size(470, 222);
            tlpGlobal.TabIndex = 0;
            // 
            // btnGeneral
            // 
            btnGeneral.Dock = DockStyle.Fill;
            btnGeneral.Location = new Point(315, 150);
            btnGeneral.Name = "btnGeneral";
            btnGeneral.Size = new Size(152, 69);
            btnGeneral.TabIndex = 2;
            btnGeneral.Tag = "tbGeneral";
            btnGeneral.Text = "Allgemein";
            btnGeneral.UseVisualStyleBackColor = true;
            btnGeneral.Click += ClickChangeColorButton;
            // 
            // btnPvP
            // 
            btnPvP.Dock = DockStyle.Fill;
            btnPvP.Location = new Point(315, 77);
            btnPvP.Name = "btnPvP";
            btnPvP.Size = new Size(152, 67);
            btnPvP.TabIndex = 1;
            btnPvP.Tag = "tbPvP";
            btnPvP.Text = "PvP";
            btnPvP.UseVisualStyleBackColor = true;
            btnPvP.Click += ClickChangeColorButton;
            // 
            // btnTrade
            // 
            btnTrade.Dock = DockStyle.Fill;
            btnTrade.Location = new Point(315, 3);
            btnTrade.Name = "btnTrade";
            btnTrade.Size = new Size(152, 68);
            btnTrade.TabIndex = 0;
            btnTrade.Tag = "tbTrade";
            btnTrade.Text = "Handel";
            btnTrade.UseVisualStyleBackColor = true;
            btnTrade.Click += ClickChangeColorButton;
            // 
            // tbTrade
            // 
            tbTrade.Dock = DockStyle.Fill;
            tbTrade.Location = new Point(159, 3);
            tbTrade.Name = "tbTrade";
            tbTrade.Size = new Size(150, 23);
            tbTrade.TabIndex = 21;
            // 
            // tbPvP
            // 
            tbPvP.Dock = DockStyle.Fill;
            tbPvP.Location = new Point(159, 77);
            tbPvP.Name = "tbPvP";
            tbPvP.Size = new Size(150, 23);
            tbPvP.TabIndex = 22;
            // 
            // tbGeneral
            // 
            tbGeneral.Dock = DockStyle.Fill;
            tbGeneral.Location = new Point(159, 150);
            tbGeneral.Name = "tbGeneral";
            tbGeneral.Size = new Size(150, 23);
            tbGeneral.TabIndex = 23;
            // 
            // tpPlayer
            // 
            tpPlayer.Controls.Add(tlpPlayer);
            tpPlayer.Location = new Point(104, 4);
            tpPlayer.Name = "tpPlayer";
            tpPlayer.Size = new Size(470, 222);
            tpPlayer.TabIndex = 1;
            tpPlayer.Text = "Spielerkanäle";
            tpPlayer.UseVisualStyleBackColor = true;
            // 
            // tlpPlayer
            // 
            tlpPlayer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tlpPlayer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tlpPlayer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tlpPlayer.Controls.Add(btnWhisper, 2, 5);
            tlpPlayer.Controls.Add(btnSay, 2, 4);
            tlpPlayer.Controls.Add(btnGuild, 2, 3);
            tlpPlayer.Controls.Add(btnOfficer, 2, 2);
            tlpPlayer.Controls.Add(btnYell, 2, 1);
            tlpPlayer.Controls.Add(btnEmote, 2, 0);
            tlpPlayer.Controls.Add(tbEmote, 1, 0);
            tlpPlayer.Controls.Add(tbYell, 1, 1);
            tlpPlayer.Controls.Add(tbOfficer, 1, 2);
            tlpPlayer.Controls.Add(tbGuild, 1, 3);
            tlpPlayer.Controls.Add(tbSay, 1, 4);
            tlpPlayer.Controls.Add(tbWhisper, 1, 5);
            tlpPlayer.Dock = DockStyle.Fill;
            tlpPlayer.Location = new Point(0, 0);
            tlpPlayer.Name = "tlpPlayer";
            tlpPlayer.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66667F));
            tlpPlayer.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66667F));
            tlpPlayer.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66667F));
            tlpPlayer.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66667F));
            tlpPlayer.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66667F));
            tlpPlayer.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66667F));
            tlpPlayer.Size = new Size(470, 222);
            tlpPlayer.TabIndex = 0;
            // 
            // btnWhisper
            // 
            btnWhisper.Dock = DockStyle.Fill;
            btnWhisper.Location = new Point(315, 188);
            btnWhisper.Name = "btnWhisper";
            btnWhisper.Size = new Size(152, 31);
            btnWhisper.TabIndex = 8;
            btnWhisper.Tag = "tbWhisper";
            btnWhisper.Text = "Flüstern";
            btnWhisper.UseVisualStyleBackColor = true;
            btnWhisper.Click += ClickChangeColorButton;
            // 
            // btnSay
            // 
            btnSay.Dock = DockStyle.Fill;
            btnSay.Location = new Point(315, 151);
            btnSay.Name = "btnSay";
            btnSay.Size = new Size(152, 31);
            btnSay.TabIndex = 7;
            btnSay.Tag = "tbSay";
            btnSay.Text = "Sagen";
            btnSay.UseVisualStyleBackColor = true;
            btnSay.Click += ClickChangeColorButton;
            // 
            // btnGuild
            // 
            btnGuild.Dock = DockStyle.Fill;
            btnGuild.Location = new Point(315, 114);
            btnGuild.Name = "btnGuild";
            btnGuild.Size = new Size(152, 31);
            btnGuild.TabIndex = 6;
            btnGuild.Tag = "tbGuild";
            btnGuild.Text = "Gilde";
            btnGuild.UseVisualStyleBackColor = true;
            btnGuild.Click += ClickChangeColorButton;
            // 
            // btnOfficer
            // 
            btnOfficer.Dock = DockStyle.Fill;
            btnOfficer.Location = new Point(315, 77);
            btnOfficer.Name = "btnOfficer";
            btnOfficer.Size = new Size(152, 31);
            btnOfficer.TabIndex = 5;
            btnOfficer.Tag = "tbOfficer";
            btnOfficer.Text = "Offizier";
            btnOfficer.UseVisualStyleBackColor = true;
            btnOfficer.Click += ClickChangeColorButton;
            // 
            // btnYell
            // 
            btnYell.Dock = DockStyle.Fill;
            btnYell.Location = new Point(315, 40);
            btnYell.Name = "btnYell";
            btnYell.Size = new Size(152, 31);
            btnYell.TabIndex = 4;
            btnYell.Tag = "tbYell";
            btnYell.Text = "Brüllen";
            btnYell.UseVisualStyleBackColor = true;
            btnYell.Click += ClickChangeColorButton;
            // 
            // btnEmote
            // 
            btnEmote.Dock = DockStyle.Fill;
            btnEmote.Location = new Point(315, 3);
            btnEmote.Name = "btnEmote";
            btnEmote.Size = new Size(152, 31);
            btnEmote.TabIndex = 3;
            btnEmote.Tag = "tbEmote";
            btnEmote.Text = "Emote";
            btnEmote.UseVisualStyleBackColor = true;
            btnEmote.Click += ClickChangeColorButton;
            // 
            // tbEmote
            // 
            tbEmote.Dock = DockStyle.Fill;
            tbEmote.Location = new Point(159, 3);
            tbEmote.Name = "tbEmote";
            tbEmote.Size = new Size(150, 23);
            tbEmote.TabIndex = 24;
            // 
            // tbYell
            // 
            tbYell.Dock = DockStyle.Fill;
            tbYell.Location = new Point(159, 40);
            tbYell.Name = "tbYell";
            tbYell.Size = new Size(150, 23);
            tbYell.TabIndex = 25;
            // 
            // tbOfficer
            // 
            tbOfficer.Dock = DockStyle.Fill;
            tbOfficer.Location = new Point(159, 77);
            tbOfficer.Name = "tbOfficer";
            tbOfficer.Size = new Size(150, 23);
            tbOfficer.TabIndex = 26;
            // 
            // tbGuild
            // 
            tbGuild.Dock = DockStyle.Fill;
            tbGuild.Location = new Point(159, 114);
            tbGuild.Name = "tbGuild";
            tbGuild.Size = new Size(150, 23);
            tbGuild.TabIndex = 27;
            // 
            // tbSay
            // 
            tbSay.Dock = DockStyle.Fill;
            tbSay.Location = new Point(159, 151);
            tbSay.Name = "tbSay";
            tbSay.Size = new Size(150, 23);
            tbSay.TabIndex = 28;
            // 
            // tbWhisper
            // 
            tbWhisper.Dock = DockStyle.Fill;
            tbWhisper.Location = new Point(159, 188);
            tbWhisper.Name = "tbWhisper";
            tbWhisper.Size = new Size(150, 23);
            tbWhisper.TabIndex = 29;
            // 
            // tpGroup
            // 
            tpGroup.Controls.Add(tlpGroup);
            tpGroup.Location = new Point(104, 4);
            tpGroup.Name = "tpGroup";
            tpGroup.Size = new Size(470, 222);
            tpGroup.TabIndex = 2;
            tpGroup.Text = "Gruppenkanäle";
            tpGroup.UseVisualStyleBackColor = true;
            // 
            // tlpGroup
            // 
            tlpGroup.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tlpGroup.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tlpGroup.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tlpGroup.Controls.Add(btnOpsOfficer, 2, 4);
            tlpGroup.Controls.Add(btnOpsAnnou, 2, 3);
            tlpGroup.Controls.Add(btnGroup, 2, 2);
            tlpGroup.Controls.Add(btnOpsLead, 2, 1);
            tlpGroup.Controls.Add(btnOps, 2, 0);
            tlpGroup.Controls.Add(tbOps, 1, 0);
            tlpGroup.Controls.Add(tbOpsLead, 1, 1);
            tlpGroup.Controls.Add(tbGroup, 1, 2);
            tlpGroup.Controls.Add(tbOpsAnnou, 1, 3);
            tlpGroup.Controls.Add(tbOpsOfficer, 1, 4);
            tlpGroup.Dock = DockStyle.Fill;
            tlpGroup.Location = new Point(0, 0);
            tlpGroup.Name = "tlpGroup";
            tlpGroup.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpGroup.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpGroup.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpGroup.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpGroup.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpGroup.Size = new Size(470, 222);
            tlpGroup.TabIndex = 0;
            // 
            // btnOpsOfficer
            // 
            btnOpsOfficer.Dock = DockStyle.Fill;
            btnOpsOfficer.Location = new Point(315, 179);
            btnOpsOfficer.Name = "btnOpsOfficer";
            btnOpsOfficer.Size = new Size(152, 40);
            btnOpsOfficer.TabIndex = 13;
            btnOpsOfficer.Tag = "tbOpsOfficer";
            btnOpsOfficer.Text = "Ops Offizier";
            btnOpsOfficer.UseVisualStyleBackColor = true;
            btnOpsOfficer.Click += ClickChangeColorButton;
            // 
            // btnOpsAnnou
            // 
            btnOpsAnnou.Dock = DockStyle.Fill;
            btnOpsAnnou.Location = new Point(315, 135);
            btnOpsAnnou.Name = "btnOpsAnnou";
            btnOpsAnnou.Size = new Size(152, 38);
            btnOpsAnnou.TabIndex = 12;
            btnOpsAnnou.Tag = "tbOpsAnnou";
            btnOpsAnnou.Text = "Ops Ankündigung";
            btnOpsAnnou.UseVisualStyleBackColor = true;
            btnOpsAnnou.Click += ClickChangeColorButton;
            // 
            // btnGroup
            // 
            btnGroup.Dock = DockStyle.Fill;
            btnGroup.Location = new Point(315, 91);
            btnGroup.Name = "btnGroup";
            btnGroup.Size = new Size(152, 38);
            btnGroup.TabIndex = 11;
            btnGroup.Tag = "tbGroup";
            btnGroup.Text = "Gruppe";
            btnGroup.UseVisualStyleBackColor = true;
            btnGroup.Click += ClickChangeColorButton;
            // 
            // btnOpsLead
            // 
            btnOpsLead.Dock = DockStyle.Fill;
            btnOpsLead.Location = new Point(315, 47);
            btnOpsLead.Name = "btnOpsLead";
            btnOpsLead.Size = new Size(152, 38);
            btnOpsLead.TabIndex = 10;
            btnOpsLead.Tag = "tbOpsLead";
            btnOpsLead.Text = "Ops Anführer";
            btnOpsLead.UseVisualStyleBackColor = true;
            btnOpsLead.Click += ClickChangeColorButton;
            // 
            // btnOps
            // 
            btnOps.Dock = DockStyle.Fill;
            btnOps.Location = new Point(315, 3);
            btnOps.Name = "btnOps";
            btnOps.Size = new Size(152, 38);
            btnOps.TabIndex = 9;
            btnOps.Tag = "tbOps";
            btnOps.Text = "Ops";
            btnOps.UseVisualStyleBackColor = true;
            btnOps.Click += ClickChangeColorButton;
            // 
            // tbOps
            // 
            tbOps.Dock = DockStyle.Fill;
            tbOps.Location = new Point(159, 3);
            tbOps.Name = "tbOps";
            tbOps.Size = new Size(150, 23);
            tbOps.TabIndex = 30;
            // 
            // tbOpsLead
            // 
            tbOpsLead.Dock = DockStyle.Fill;
            tbOpsLead.Location = new Point(159, 47);
            tbOpsLead.Name = "tbOpsLead";
            tbOpsLead.Size = new Size(150, 23);
            tbOpsLead.TabIndex = 31;
            // 
            // tbGroup
            // 
            tbGroup.Dock = DockStyle.Fill;
            tbGroup.Location = new Point(159, 91);
            tbGroup.Name = "tbGroup";
            tbGroup.Size = new Size(150, 23);
            tbGroup.TabIndex = 32;
            // 
            // tbOpsAnnou
            // 
            tbOpsAnnou.Dock = DockStyle.Fill;
            tbOpsAnnou.Location = new Point(159, 135);
            tbOpsAnnou.Name = "tbOpsAnnou";
            tbOpsAnnou.Size = new Size(150, 23);
            tbOpsAnnou.TabIndex = 33;
            // 
            // tbOpsOfficer
            // 
            tbOpsOfficer.Dock = DockStyle.Fill;
            tbOpsOfficer.Location = new Point(159, 179);
            tbOpsOfficer.Name = "tbOpsOfficer";
            tbOpsOfficer.Size = new Size(150, 23);
            tbOpsOfficer.TabIndex = 34;
            // 
            // tpSystem
            // 
            tpSystem.Controls.Add(tlpSystem);
            tpSystem.Location = new Point(104, 4);
            tpSystem.Name = "tpSystem";
            tpSystem.Size = new Size(470, 222);
            tpSystem.TabIndex = 3;
            tpSystem.Text = "Systemkanäle";
            tpSystem.UseVisualStyleBackColor = true;
            // 
            // tlpSystem
            // 
            tlpSystem.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tlpSystem.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tlpSystem.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tlpSystem.Controls.Add(btnGroupInfo, 2, 6);
            tlpSystem.Controls.Add(btnGuildInfo, 2, 5);
            tlpSystem.Controls.Add(tbGroupInfo, 1, 6);
            tlpSystem.Controls.Add(btnSystem, 2, 4);
            tlpSystem.Controls.Add(btnOpsInfo, 2, 3);
            tlpSystem.Controls.Add(tbGuildInfo, 1, 5);
            tlpSystem.Controls.Add(btnLogin, 2, 2);
            tlpSystem.Controls.Add(btnConv, 2, 1);
            tlpSystem.Controls.Add(tbSystem, 1, 4);
            tlpSystem.Controls.Add(btnCombat, 2, 0);
            tlpSystem.Controls.Add(tbCombat, 1, 0);
            tlpSystem.Controls.Add(tbOpsInfo, 1, 3);
            tlpSystem.Controls.Add(tbConv, 1, 1);
            tlpSystem.Controls.Add(tbLogin, 1, 2);
            tlpSystem.Dock = DockStyle.Fill;
            tlpSystem.Location = new Point(0, 0);
            tlpSystem.Name = "tlpSystem";
            tlpSystem.RowStyles.Add(new RowStyle(SizeType.Percent, 14.4302216F));
            tlpSystem.RowStyles.Add(new RowStyle(SizeType.Percent, 14.4302187F));
            tlpSystem.RowStyles.Add(new RowStyle(SizeType.Percent, 14.4302187F));
            tlpSystem.RowStyles.Add(new RowStyle(SizeType.Percent, 14.4302187F));
            tlpSystem.RowStyles.Add(new RowStyle(SizeType.Percent, 14.4302187F));
            tlpSystem.RowStyles.Add(new RowStyle(SizeType.Percent, 14.4302216F));
            tlpSystem.RowStyles.Add(new RowStyle(SizeType.Percent, 13.4186754F));
            tlpSystem.Size = new Size(470, 222);
            tlpSystem.TabIndex = 0;
            // 
            // btnGroupInfo
            // 
            btnGroupInfo.Dock = DockStyle.Fill;
            btnGroupInfo.Location = new Point(315, 195);
            btnGroupInfo.Name = "btnGroupInfo";
            btnGroupInfo.Size = new Size(152, 24);
            btnGroupInfo.TabIndex = 20;
            btnGroupInfo.Tag = "tbGroupInfo";
            btnGroupInfo.Text = "Gruppeninfos";
            btnGroupInfo.UseVisualStyleBackColor = true;
            btnGroupInfo.Click += ClickChangeColorButton;
            // 
            // btnGuildInfo
            // 
            btnGuildInfo.Dock = DockStyle.Fill;
            btnGuildInfo.Location = new Point(315, 163);
            btnGuildInfo.Name = "btnGuildInfo";
            btnGuildInfo.Size = new Size(152, 26);
            btnGuildInfo.TabIndex = 19;
            btnGuildInfo.Tag = "tbGuildInfo";
            btnGuildInfo.Text = "Gildeninfos";
            btnGuildInfo.UseVisualStyleBackColor = true;
            btnGuildInfo.Click += ClickChangeColorButton;
            // 
            // tbGroupInfo
            // 
            tbGroupInfo.Dock = DockStyle.Fill;
            tbGroupInfo.Location = new Point(159, 195);
            tbGroupInfo.Name = "tbGroupInfo";
            tbGroupInfo.Size = new Size(150, 23);
            tbGroupInfo.TabIndex = 41;
            // 
            // btnSystem
            // 
            btnSystem.Dock = DockStyle.Fill;
            btnSystem.Location = new Point(315, 131);
            btnSystem.Name = "btnSystem";
            btnSystem.Size = new Size(152, 26);
            btnSystem.TabIndex = 18;
            btnSystem.Tag = "tbSystem";
            btnSystem.Text = "System-Rückmeldung";
            btnSystem.UseVisualStyleBackColor = true;
            btnSystem.Click += ClickChangeColorButton;
            // 
            // btnOpsInfo
            // 
            btnOpsInfo.Dock = DockStyle.Fill;
            btnOpsInfo.Location = new Point(315, 99);
            btnOpsInfo.Name = "btnOpsInfo";
            btnOpsInfo.Size = new Size(152, 26);
            btnOpsInfo.TabIndex = 17;
            btnOpsInfo.Tag = "tbOpsInfo";
            btnOpsInfo.Text = "Ops Infos";
            btnOpsInfo.UseVisualStyleBackColor = true;
            btnOpsInfo.Click += ClickChangeColorButton;
            // 
            // tbGuildInfo
            // 
            tbGuildInfo.Dock = DockStyle.Fill;
            tbGuildInfo.Location = new Point(159, 163);
            tbGuildInfo.Name = "tbGuildInfo";
            tbGuildInfo.Size = new Size(150, 23);
            tbGuildInfo.TabIndex = 40;
            // 
            // btnLogin
            // 
            btnLogin.Dock = DockStyle.Fill;
            btnLogin.Location = new Point(315, 67);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(152, 26);
            btnLogin.TabIndex = 16;
            btnLogin.Tag = "tbLogin";
            btnLogin.Text = "Charakter Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += ClickChangeColorButton;
            // 
            // btnConv
            // 
            btnConv.Dock = DockStyle.Fill;
            btnConv.Location = new Point(315, 35);
            btnConv.Name = "btnConv";
            btnConv.Size = new Size(152, 26);
            btnConv.TabIndex = 15;
            btnConv.Tag = "tbConv";
            btnConv.Text = "Gespräch";
            btnConv.UseVisualStyleBackColor = true;
            btnConv.Click += ClickChangeColorButton;
            // 
            // tbSystem
            // 
            tbSystem.Dock = DockStyle.Fill;
            tbSystem.Location = new Point(159, 131);
            tbSystem.Name = "tbSystem";
            tbSystem.Size = new Size(150, 23);
            tbSystem.TabIndex = 39;
            // 
            // btnCombat
            // 
            btnCombat.Dock = DockStyle.Fill;
            btnCombat.Location = new Point(315, 3);
            btnCombat.Name = "btnCombat";
            btnCombat.Size = new Size(152, 26);
            btnCombat.TabIndex = 14;
            btnCombat.Tag = "tbCombat";
            btnCombat.Text = "Kampfinfos";
            btnCombat.UseVisualStyleBackColor = true;
            btnCombat.Click += ClickChangeColorButton;
            // 
            // tbCombat
            // 
            tbCombat.Dock = DockStyle.Fill;
            tbCombat.Location = new Point(159, 3);
            tbCombat.Name = "tbCombat";
            tbCombat.Size = new Size(150, 23);
            tbCombat.TabIndex = 35;
            // 
            // tbOpsInfo
            // 
            tbOpsInfo.Dock = DockStyle.Fill;
            tbOpsInfo.Location = new Point(159, 99);
            tbOpsInfo.Name = "tbOpsInfo";
            tbOpsInfo.Size = new Size(150, 23);
            tbOpsInfo.TabIndex = 38;
            // 
            // tbConv
            // 
            tbConv.Dock = DockStyle.Fill;
            tbConv.Location = new Point(159, 35);
            tbConv.Name = "tbConv";
            tbConv.Size = new Size(150, 23);
            tbConv.TabIndex = 36;
            // 
            // tbLogin
            // 
            tbLogin.Dock = DockStyle.Fill;
            tbLogin.Location = new Point(159, 67);
            tbLogin.Name = "tbLogin";
            tbLogin.Size = new Size(150, 23);
            tbLogin.TabIndex = 37;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 261);
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
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem printToolStripMenuItem;
        private ToolStripMenuItem printPreviewToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem selectAllToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem charFolderToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem logFolderToolStripMenuItem;
        private ToolStripMenuItem supportToolStripMenuItem;
        private ToolStripMenuItem backupToolStripMenuItem;
        private ToolStripMenuItem bugToolStripMenuItem;
        private ToolStripMenuItem bugMailToolStripMenuItem;
        private ToolStripMenuItem updateToolStripMenuItem;
    }
}