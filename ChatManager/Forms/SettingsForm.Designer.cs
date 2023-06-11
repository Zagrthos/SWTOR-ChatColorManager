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
            lblAutosaveInterval = new Label();
            numberAutosaveInterval = new NumericUpDown();
            chbAutosave = new CheckBox();
            chbSaveOnClose = new CheckBox();
            btnResetSettings = new Button();
            tlpSettings.SuspendLayout();
            gbLanguage.SuspendLayout();
            gbGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numberAutosaveInterval).BeginInit();
            SuspendLayout();
            // 
            // tlpSettings
            // 
            tlpSettings.ColumnCount = 2;
            tlpSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tlpSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67F));
            tlpSettings.Controls.Add(gbLanguage, 0, 0);
            tlpSettings.Controls.Add(gbGeneral, 1, 0);
            tlpSettings.Controls.Add(btnResetSettings, 0, 1);
            tlpSettings.Dock = DockStyle.Fill;
            tlpSettings.Location = new Point(0, 0);
            tlpSettings.Name = "tlpSettings";
            tlpSettings.RowCount = 2;
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpSettings.Size = new Size(509, 161);
            tlpSettings.TabIndex = 0;
            // 
            // gbLanguage
            // 
            gbLanguage.Controls.Add(cbLanguage);
            gbLanguage.Dock = DockStyle.Fill;
            gbLanguage.Location = new Point(3, 3);
            gbLanguage.Name = "gbLanguage";
            gbLanguage.Size = new Size(161, 122);
            gbLanguage.TabIndex = 0;
            gbLanguage.TabStop = false;
            gbLanguage.Text = "Sprachauswahl";
            // 
            // cbLanguage
            // 
            cbLanguage.Dock = DockStyle.Fill;
            cbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLanguage.FormattingEnabled = true;
            cbLanguage.Items.AddRange(new object[] { "English", "France", "German" });
            cbLanguage.Location = new Point(3, 19);
            cbLanguage.Name = "cbLanguage";
            cbLanguage.Size = new Size(155, 23);
            cbLanguage.TabIndex = 0;
            cbLanguage.SelectedIndexChanged += SwitchCurrentLocale;
            // 
            // gbGeneral
            // 
            gbGeneral.Controls.Add(lblAutosaveInterval);
            gbGeneral.Controls.Add(numberAutosaveInterval);
            gbGeneral.Controls.Add(chbAutosave);
            gbGeneral.Controls.Add(chbSaveOnClose);
            gbGeneral.Dock = DockStyle.Fill;
            gbGeneral.Location = new Point(170, 3);
            gbGeneral.Name = "gbGeneral";
            gbGeneral.Size = new Size(336, 122);
            gbGeneral.TabIndex = 1;
            gbGeneral.TabStop = false;
            gbGeneral.Text = "Allgemeine Einstellungen";
            // 
            // lblAutosaveInterval
            // 
            lblAutosaveInterval.AutoSize = true;
            lblAutosaveInterval.Location = new Point(53, 48);
            lblAutosaveInterval.Name = "lblAutosaveInterval";
            lblAutosaveInterval.Size = new Size(157, 15);
            lblAutosaveInterval.TabIndex = 3;
            lblAutosaveInterval.Text = "Autosave Intervall (Minuten)";
            // 
            // numberAutosaveInterval
            // 
            numberAutosaveInterval.InterceptArrowKeys = false;
            numberAutosaveInterval.Location = new Point(6, 46);
            numberAutosaveInterval.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numberAutosaveInterval.Name = "numberAutosaveInterval";
            numberAutosaveInterval.Size = new Size(41, 23);
            numberAutosaveInterval.TabIndex = 2;
            numberAutosaveInterval.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numberAutosaveInterval.ValueChanged += SetAutosaveInterval;
            // 
            // chbAutosave
            // 
            chbAutosave.AutoSize = true;
            chbAutosave.Location = new Point(6, 21);
            chbAutosave.Name = "chbAutosave";
            chbAutosave.Size = new Size(129, 19);
            chbAutosave.TabIndex = 1;
            chbAutosave.Text = "Autosave aktivieren";
            chbAutosave.UseVisualStyleBackColor = true;
            chbAutosave.CheckedChanged += ChangingCheckBoxes;
            // 
            // chbSaveOnClose
            // 
            chbSaveOnClose.AutoSize = true;
            chbSaveOnClose.Location = new Point(6, 75);
            chbSaveOnClose.Name = "chbSaveOnClose";
            chbSaveOnClose.Size = new Size(303, 19);
            chbSaveOnClose.TabIndex = 0;
            chbSaveOnClose.Text = "Speichern der letzten Farben beim schließen der App";
            chbSaveOnClose.UseVisualStyleBackColor = true;
            chbSaveOnClose.CheckedChanged += ChangingCheckBoxes;
            // 
            // btnResetSettings
            // 
            btnResetSettings.Dock = DockStyle.Fill;
            btnResetSettings.Location = new Point(3, 131);
            btnResetSettings.MinimumSize = new Size(161, 25);
            btnResetSettings.Name = "btnResetSettings";
            btnResetSettings.Size = new Size(161, 27);
            btnResetSettings.TabIndex = 2;
            btnResetSettings.Text = "Einstellungen zurücksetzen";
            btnResetSettings.UseVisualStyleBackColor = true;
            btnResetSettings.Click += ResetSettings;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(509, 161);
            Controls.Add(tlpSettings);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Einstellungen";
            Load += SettingsForm_Load;
            tlpSettings.ResumeLayout(false);
            gbLanguage.ResumeLayout(false);
            gbGeneral.ResumeLayout(false);
            gbGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numberAutosaveInterval).EndInit();
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
    }
}