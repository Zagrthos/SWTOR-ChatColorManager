namespace ChatManager.Forms
{
    partial class BackupSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupSelector));
            tlpMain = new TableLayoutPanel();
            lbxBackupDir = new ListBox();
            clbxBackupFiles = new CheckedListBox();
            lblBackupDir = new Label();
            lblBackupFiles = new Label();
            tlpBackupFiles = new TableLayoutPanel();
            btnSelectAll = new Button();
            btnDeselectAll = new Button();
            tlpMain.SuspendLayout();
            tlpBackupFiles.SuspendLayout();
            SuspendLayout();
            // 
            // tlpMain
            // 
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpMain.Controls.Add(lbxBackupDir, 0, 1);
            tlpMain.Controls.Add(clbxBackupFiles, 1, 1);
            tlpMain.Controls.Add(lblBackupDir, 0, 0);
            tlpMain.Controls.Add(lblBackupFiles, 1, 0);
            tlpMain.Controls.Add(tlpBackupFiles, 1, 2);
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
            lbxBackupDir.Size = new Size(286, 193);
            lbxBackupDir.TabIndex = 0;
            lbxBackupDir.SelectedIndexChanged += lbxBackupDir_SelectedIndexChanged;
            // 
            // clbxBackupFiles
            // 
            clbxBackupFiles.CheckOnClick = true;
            clbxBackupFiles.Dock = DockStyle.Fill;
            clbxBackupFiles.FormattingEnabled = true;
            clbxBackupFiles.Location = new Point(295, 28);
            clbxBackupFiles.Name = "clbxBackupFiles";
            clbxBackupFiles.Size = new Size(286, 193);
            clbxBackupFiles.TabIndex = 1;
            // 
            // lblBackupDir
            // 
            lblBackupDir.AutoSize = true;
            lblBackupDir.Dock = DockStyle.Fill;
            lblBackupDir.Location = new Point(3, 0);
            lblBackupDir.Name = "lblBackupDir";
            lblBackupDir.Size = new Size(286, 25);
            lblBackupDir.TabIndex = 2;
            lblBackupDir.Text = "Backupverzeichnis auswählen";
            lblBackupDir.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblBackupFiles
            // 
            lblBackupFiles.AutoSize = true;
            lblBackupFiles.Dock = DockStyle.Fill;
            lblBackupFiles.Location = new Point(295, 0);
            lblBackupFiles.Name = "lblBackupFiles";
            lblBackupFiles.Size = new Size(286, 25);
            lblBackupFiles.TabIndex = 3;
            lblBackupFiles.Text = "Backup Datei(en) auswählen";
            lblBackupFiles.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tlpBackupFiles
            // 
            tlpBackupFiles.ColumnCount = 2;
            tlpBackupFiles.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBackupFiles.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBackupFiles.Controls.Add(btnSelectAll, 0, 0);
            tlpBackupFiles.Controls.Add(btnDeselectAll, 1, 0);
            tlpBackupFiles.Dock = DockStyle.Fill;
            tlpBackupFiles.Location = new Point(295, 227);
            tlpBackupFiles.Name = "tlpBackupFiles";
            tlpBackupFiles.RowCount = 1;
            tlpBackupFiles.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBackupFiles.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpBackupFiles.Size = new Size(286, 31);
            tlpBackupFiles.TabIndex = 4;
            // 
            // btnSelectAll
            // 
            btnSelectAll.Dock = DockStyle.Fill;
            btnSelectAll.Location = new Point(3, 3);
            btnSelectAll.Name = "btnSelectAll";
            btnSelectAll.Size = new Size(137, 25);
            btnSelectAll.TabIndex = 0;
            btnSelectAll.Text = "Alle auswählen";
            btnSelectAll.UseVisualStyleBackColor = true;
            // 
            // btnDeselectAll
            // 
            btnDeselectAll.Dock = DockStyle.Fill;
            btnDeselectAll.Location = new Point(146, 3);
            btnDeselectAll.Name = "btnDeselectAll";
            btnDeselectAll.Size = new Size(137, 25);
            btnDeselectAll.TabIndex = 1;
            btnDeselectAll.Text = "Alle abwählen";
            btnDeselectAll.UseVisualStyleBackColor = true;
            // 
            // BackupSelector
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 261);
            Controls.Add(tlpMain);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "BackupSelector";
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
        private Button btnSelectAll;
        private Button btnDeselectAll;
    }
}