using System.Drawing;
using System.Windows.Forms;

namespace ChatManager.Forms
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            tlpSettings = new TableLayoutPanel();
            gbLanguage = new GroupBox();
            cbLanguage = new ComboBox();
            gbGeneral = new GroupBox();
            chbReloadOnStartup = new CheckBox();
            lblAutosaveInterval = new Label();
            numberAutosaveInterval = new NumericUpDown();
            chbAutosave = new CheckBox();
            chbSaveOnClose = new CheckBox();
            btnResetSettings = new Button();
            gbUpdater = new GroupBox();
            chbUpdateDownload = new CheckBox();
            cbUpdateInterval = new ComboBox();
            lblUpdateIntervall = new Label();
            tlpSettings.SuspendLayout();
            gbLanguage.SuspendLayout();
            gbGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numberAutosaveInterval).BeginInit();
            gbUpdater.SuspendLayout();
            SuspendLayout();
            // 
            // tlpSettings
            // 
            tlpSettings.ColumnCount = 2;
            tlpSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tlpSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tlpSettings.Controls.Add(gbLanguage, 0, 0);
            tlpSettings.Controls.Add(gbGeneral, 1, 0);
            tlpSettings.Controls.Add(btnResetSettings, 0, 7);
            tlpSettings.Controls.Add(gbUpdater, 0, 2);
            tlpSettings.Dock = DockStyle.Fill;
            tlpSettings.Location = new Point(0, 0);
            tlpSettings.Name = "tlpSettings";
            tlpSettings.RowCount = 8;
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpSettings.Size = new Size(684, 211);
            tlpSettings.TabIndex = 0;
            // 
            // gbLanguage
            // 
            gbLanguage.Controls.Add(cbLanguage);
            gbLanguage.Dock = DockStyle.Fill;
            gbLanguage.Location = new Point(3, 3);
            gbLanguage.Name = "gbLanguage";
            tlpSettings.SetRowSpan(gbLanguage, 2);
            gbLanguage.Size = new Size(267, 46);
            gbLanguage.TabIndex = 1;
            gbLanguage.TabStop = false;
            gbLanguage.Text = "Language selection";
            // 
            // cbLanguage
            // 
            cbLanguage.Dock = DockStyle.Fill;
            cbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLanguage.FormattingEnabled = true;
            cbLanguage.Items.AddRange(new object[] { "English", "France", "German" });
            cbLanguage.Location = new Point(3, 19);
            cbLanguage.Name = "cbLanguage";
            cbLanguage.Size = new Size(261, 23);
            cbLanguage.TabIndex = 2;
            cbLanguage.SelectedIndexChanged += ComboBoxHandler;
            // 
            // gbGeneral
            // 
            gbGeneral.Controls.Add(chbReloadOnStartup);
            gbGeneral.Controls.Add(lblAutosaveInterval);
            gbGeneral.Controls.Add(numberAutosaveInterval);
            gbGeneral.Controls.Add(chbAutosave);
            gbGeneral.Controls.Add(chbSaveOnClose);
            gbGeneral.Dock = DockStyle.Fill;
            gbGeneral.Location = new Point(276, 3);
            gbGeneral.Name = "gbGeneral";
            tlpSettings.SetRowSpan(gbGeneral, 5);
            gbGeneral.Size = new Size(405, 124);
            gbGeneral.TabIndex = 2;
            gbGeneral.TabStop = false;
            gbGeneral.Text = "General settings";
            // 
            // chbReloadOnStartup
            // 
            chbReloadOnStartup.AutoSize = true;
            chbReloadOnStartup.Location = new Point(6, 100);
            chbReloadOnStartup.Name = "chbReloadOnStartup";
            chbReloadOnStartup.Size = new Size(236, 19);
            chbReloadOnStartup.TabIndex = 6;
            chbReloadOnStartup.Text = "Reload last colors when starting the app";
            chbReloadOnStartup.UseVisualStyleBackColor = true;
            chbReloadOnStartup.CheckedChanged += ChangingCheckBoxes;
            // 
            // lblAutosaveInterval
            // 
            lblAutosaveInterval.AutoSize = true;
            lblAutosaveInterval.Location = new Point(53, 48);
            lblAutosaveInterval.Name = "lblAutosaveInterval";
            lblAutosaveInterval.Size = new Size(152, 15);
            lblAutosaveInterval.TabIndex = 3;
            lblAutosaveInterval.Text = "Autosave interval (minutes)";
            // 
            // numberAutosaveInterval
            // 
            numberAutosaveInterval.InterceptArrowKeys = false;
            numberAutosaveInterval.Location = new Point(6, 46);
            numberAutosaveInterval.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numberAutosaveInterval.Name = "numberAutosaveInterval";
            numberAutosaveInterval.Size = new Size(41, 23);
            numberAutosaveInterval.TabIndex = 4;
            numberAutosaveInterval.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numberAutosaveInterval.ValueChanged += SetAutosaveInterval;
            // 
            // chbAutosave
            // 
            chbAutosave.AutoSize = true;
            chbAutosave.Location = new Point(6, 21);
            chbAutosave.Name = "chbAutosave";
            chbAutosave.Size = new Size(111, 19);
            chbAutosave.TabIndex = 3;
            chbAutosave.Text = "Enable autosave";
            chbAutosave.UseVisualStyleBackColor = true;
            chbAutosave.CheckedChanged += ChangingCheckBoxes;
            // 
            // chbSaveOnClose
            // 
            chbSaveOnClose.AutoSize = true;
            chbSaveOnClose.Location = new Point(6, 75);
            chbSaveOnClose.Name = "chbSaveOnClose";
            chbSaveOnClose.Size = new Size(222, 19);
            chbSaveOnClose.TabIndex = 5;
            chbSaveOnClose.Text = "Save last colors when closing the app";
            chbSaveOnClose.UseVisualStyleBackColor = true;
            chbSaveOnClose.CheckedChanged += ChangingCheckBoxes;
            // 
            // btnResetSettings
            // 
            btnResetSettings.Dock = DockStyle.Fill;
            btnResetSettings.Location = new Point(3, 185);
            btnResetSettings.MinimumSize = new Size(161, 25);
            btnResetSettings.Name = "btnResetSettings";
            btnResetSettings.Size = new Size(267, 25);
            btnResetSettings.TabIndex = 7;
            btnResetSettings.Text = "Reset Settings";
            btnResetSettings.UseVisualStyleBackColor = true;
            btnResetSettings.Click += ResetSettings;
            // 
            // gbUpdater
            // 
            gbUpdater.Controls.Add(chbUpdateDownload);
            gbUpdater.Controls.Add(cbUpdateInterval);
            gbUpdater.Controls.Add(lblUpdateIntervall);
            gbUpdater.Dock = DockStyle.Fill;
            gbUpdater.Location = new Point(3, 55);
            gbUpdater.Name = "gbUpdater";
            tlpSettings.SetRowSpan(gbUpdater, 4);
            gbUpdater.Size = new Size(267, 98);
            gbUpdater.TabIndex = 2;
            gbUpdater.TabStop = false;
            gbUpdater.Text = "Update settings";
            // 
            // chbUpdateDownload
            // 
            chbUpdateDownload.AutoSize = true;
            chbUpdateDownload.Location = new Point(3, 63);
            chbUpdateDownload.Name = "chbUpdateDownload";
            chbUpdateDownload.Size = new Size(177, 19);
            chbUpdateDownload.TabIndex = 4;
            chbUpdateDownload.Text = "Download updates manually";
            chbUpdateDownload.UseVisualStyleBackColor = true;
            chbUpdateDownload.CheckedChanged += ChangingCheckBoxes;
            // 
            // cbUpdateInterval
            // 
            cbUpdateInterval.Dock = DockStyle.Top;
            cbUpdateInterval.DropDownStyle = ComboBoxStyle.DropDownList;
            cbUpdateInterval.FormattingEnabled = true;
            cbUpdateInterval.Location = new Point(3, 34);
            cbUpdateInterval.Name = "cbUpdateInterval";
            cbUpdateInterval.Size = new Size(261, 23);
            cbUpdateInterval.TabIndex = 3;
            cbUpdateInterval.SelectedIndexChanged += ComboBoxHandler;
            // 
            // lblUpdateIntervall
            // 
            lblUpdateIntervall.AutoSize = true;
            lblUpdateIntervall.Dock = DockStyle.Top;
            lblUpdateIntervall.Location = new Point(3, 19);
            lblUpdateIntervall.Name = "lblUpdateIntervall";
            lblUpdateIntervall.Size = new Size(84, 15);
            lblUpdateIntervall.TabIndex = 0;
            lblUpdateIntervall.Text = "Search interval";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 211);
            Controls.Add(tlpSettings);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Settings";
            Load += SettingsForm_Load;
            tlpSettings.ResumeLayout(false);
            gbLanguage.ResumeLayout(false);
            gbGeneral.ResumeLayout(false);
            gbGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numberAutosaveInterval).EndInit();
            gbUpdater.ResumeLayout(false);
            gbUpdater.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpSettings;
        private GroupBox gbLanguage;
        private ComboBox cbLanguage;
        private GroupBox gbGeneral;
        private CheckBox chbSaveOnClose;
        private Label lblAutosaveInterval;
        private NumericUpDown numberAutosaveInterval;
        private CheckBox chbAutosave;
        private Button btnResetSettings;
        private CheckBox chbReloadOnStartup;
        private GroupBox gbUpdater;
        private ComboBox cbUpdateInterval;
        private Label lblUpdateIntervall;
        private CheckBox chbUpdateDownload;
    }
}