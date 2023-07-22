namespace ChatManager.Forms
{
    partial class LicencesForm
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
            tableLayoutPanel1 = new TableLayoutPanel();
            lblLicencesHead = new Label();
            rtbLicences = new RichTextBox();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(lblLicencesHead, 0, 0);
            tableLayoutPanel1.Controls.Add(rtbLicences, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tableLayoutPanel1.Size = new Size(634, 261);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // lblLicencesHead
            // 
            lblLicencesHead.AutoSize = true;
            lblLicencesHead.Dock = DockStyle.Fill;
            lblLicencesHead.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblLicencesHead.Location = new Point(3, 3);
            lblLicencesHead.Margin = new Padding(3);
            lblLicencesHead.Name = "lblLicencesHead";
            lblLicencesHead.Size = new Size(628, 20);
            lblLicencesHead.TabIndex = 0;
            lblLicencesHead.Text = "Licences from every software part which is used in this application";
            lblLicencesHead.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // rtbLicences
            // 
            rtbLicences.BorderStyle = BorderStyle.None;
            rtbLicences.CausesValidation = false;
            rtbLicences.Dock = DockStyle.Fill;
            rtbLicences.Location = new Point(5, 31);
            rtbLicences.Margin = new Padding(5);
            rtbLicences.MaxLength = 0;
            rtbLicences.Name = "rtbLicences";
            rtbLicences.ReadOnly = true;
            rtbLicences.Size = new Size(624, 225);
            rtbLicences.TabIndex = 0;
            rtbLicences.TabStop = false;
            rtbLicences.Text = "Placeholder";
            rtbLicences.LinkClicked += RtbLicences_LinkClicked;
            rtbLicences.Click += RtbLicences_GotFocus;
            rtbLicences.GotFocus += RtbLicences_GotFocus;
            // 
            // LicencesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(634, 261);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LicencesForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Licences";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label lblLicencesHead;
        private RichTextBox rtbLicences;
    }
}