namespace ChatManager.Forms
{
    partial class BackupSelectorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupSelectorForm));
            tlpMain = new TableLayoutPanel();
            lbxBackupDir = new ListBox();
            clbxBackupFiles = new CheckedListBox();
            lblBackupDir = new Label();
            lblBackupFiles = new Label();
            tlpBackupFiles = new TableLayoutPanel();
            btnBackupSelectAll = new Button();
            btnBackupDeselectAll = new Button();
            btnRestore = new Button();
            lblDateConvertion = new Label();
            tlpMain.SuspendLayout();
            tlpBackupFiles.SuspendLayout();
            SuspendLayout();
            // 
            // tlpMain
            // 
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tlpMain.Controls.Add(lbxBackupDir, 0, 1);
            tlpMain.Controls.Add(clbxBackupFiles, 1, 1);
            tlpMain.Controls.Add(lblBackupDir, 0, 0);
            tlpMain.Controls.Add(lblBackupFiles, 1, 0);
            tlpMain.Controls.Add(tlpBackupFiles, 1, 2);
            tlpMain.Controls.Add(lblDateConvertion, 0, 2);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(0, 0);
            tlpMain.Name = "tlpMain";
            tlpMain.RowCount = 3;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 37F));
            tlpMain.Size = new Size(584, 261);
            tlpMain.TabIndex = 0;
            // 
            // lbxBackupDir
            // 
            lbxBackupDir.Dock = DockStyle.Fill;
            lbxBackupDir.FormattingEnabled = true;
            lbxBackupDir.ItemHeight = 15;
            lbxBackupDir.Location = new Point(3, 28);
            lbxBackupDir.Name = "lbxBackupDir";
            lbxBackupDir.Size = new Size(256, 193);
            lbxBackupDir.TabIndex = 0;
            lbxBackupDir.SelectedIndexChanged += SelectBackupDir;
            // 
            // clbxBackupFiles
            // 
            clbxBackupFiles.CheckOnClick = true;
            clbxBackupFiles.Dock = DockStyle.Fill;
            clbxBackupFiles.FormattingEnabled = true;
            clbxBackupFiles.Location = new Point(265, 28);
            clbxBackupFiles.Name = "clbxBackupFiles";
            clbxBackupFiles.Size = new Size(316, 193);
            clbxBackupFiles.TabIndex = 1;
            // 
            // lblBackupDir
            // 
            lblBackupDir.AutoSize = true;
            lblBackupDir.Dock = DockStyle.Fill;
            lblBackupDir.Location = new Point(3, 0);
            lblBackupDir.Name = "lblBackupDir";
            lblBackupDir.Size = new Size(256, 25);
            lblBackupDir.TabIndex = 2;
            lblBackupDir.Text = "Backupverzeichnis auswählen";
            lblBackupDir.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblBackupFiles
            // 
            lblBackupFiles.AutoSize = true;
            lblBackupFiles.Dock = DockStyle.Fill;
            lblBackupFiles.Location = new Point(265, 0);
            lblBackupFiles.Name = "lblBackupFiles";
            lblBackupFiles.Size = new Size(316, 25);
            lblBackupFiles.TabIndex = 3;
            lblBackupFiles.Text = "Backup Datei(en) auswählen";
            lblBackupFiles.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tlpBackupFiles
            // 
            tlpBackupFiles.ColumnCount = 3;
            tlpBackupFiles.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36F));
            tlpBackupFiles.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            tlpBackupFiles.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            tlpBackupFiles.Controls.Add(btnBackupSelectAll, 1, 0);
            tlpBackupFiles.Controls.Add(btnBackupDeselectAll, 2, 0);
            tlpBackupFiles.Controls.Add(btnRestore, 0, 0);
            tlpBackupFiles.Dock = DockStyle.Fill;
            tlpBackupFiles.Location = new Point(265, 227);
            tlpBackupFiles.Name = "tlpBackupFiles";
            tlpBackupFiles.RowCount = 1;
            tlpBackupFiles.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBackupFiles.Size = new Size(316, 31);
            tlpBackupFiles.TabIndex = 4;
            // 
            // btnBackupSelectAll
            // 
            btnBackupSelectAll.Dock = DockStyle.Fill;
            btnBackupSelectAll.Location = new Point(116, 3);
            btnBackupSelectAll.Name = "btnBackupSelectAll";
            btnBackupSelectAll.Size = new Size(95, 25);
            btnBackupSelectAll.TabIndex = 0;
            btnBackupSelectAll.Text = "Alle auswählen";
            btnBackupSelectAll.UseVisualStyleBackColor = true;
            btnBackupSelectAll.Click += SelectClick;
            // 
            // btnBackupDeselectAll
            // 
            btnBackupDeselectAll.Dock = DockStyle.Fill;
            btnBackupDeselectAll.Location = new Point(217, 3);
            btnBackupDeselectAll.Name = "btnBackupDeselectAll";
            btnBackupDeselectAll.Size = new Size(96, 25);
            btnBackupDeselectAll.TabIndex = 1;
            btnBackupDeselectAll.Text = "Alle abwählen";
            btnBackupDeselectAll.UseVisualStyleBackColor = true;
            btnBackupDeselectAll.Click += SelectClick;
            // 
            // btnRestore
            // 
            btnRestore.Dock = DockStyle.Fill;
            btnRestore.Location = new Point(3, 3);
            btnRestore.Name = "btnRestore";
            btnRestore.Size = new Size(107, 25);
            btnRestore.TabIndex = 2;
            btnRestore.Text = "Wiederherstellen";
            btnRestore.UseVisualStyleBackColor = true;
            btnRestore.Click += Restore;
            // 
            // lblDateConvertion
            // 
            lblDateConvertion.AutoSize = true;
            lblDateConvertion.Dock = DockStyle.Fill;
            lblDateConvertion.Location = new Point(3, 224);
            lblDateConvertion.Name = "lblDateConvertion";
            lblDateConvertion.Size = new Size(256, 37);
            lblDateConvertion.TabIndex = 5;
            lblDateConvertion.Text = "Date Placeholder";
            lblDateConvertion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BackupSelectorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 261);
            Controls.Add(tlpMain);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "BackupSelectorForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Backup wiederherstellen";
            tlpMain.ResumeLayout(false);
            tlpMain.PerformLayout();
            tlpBackupFiles.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpMain;
        private ListBox lbxBackupDir;
        private CheckedListBox clbxBackupFiles;
        private Label lblBackupDir;
        private Label lblBackupFiles;
        private TableLayoutPanel tlpBackupFiles;
        private Button btnBackupSelectAll;
        private Button btnBackupDeselectAll;
        private Label lblDateConvertion;
        private Button btnRestore;
    }
}