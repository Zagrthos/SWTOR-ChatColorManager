﻿using System.Drawing;
using System.Windows.Forms;

namespace ChatManager.Forms
{
    partial class TextViewerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private readonly System.ComponentModel.IContainer components = null;

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
            tlpLicences = new TableLayoutPanel();
            lblLicencesHead = new Label();
            rtbLicences = new RichTextBox();
            tlpLicences.SuspendLayout();
            SuspendLayout();
            // 
            // tlpLicences
            // 
            tlpLicences.ColumnCount = 1;
            tlpLicences.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpLicences.Controls.Add(lblLicencesHead, 0, 0);
            tlpLicences.Controls.Add(rtbLicences, 0, 1);
            tlpLicences.Dock = DockStyle.Fill;
            tlpLicences.Location = new Point(0, 0);
            tlpLicences.Name = "tlpLicences";
            tlpLicences.RowCount = 2;
            tlpLicences.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlpLicences.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tlpLicences.Size = new Size(634, 261);
            tlpLicences.TabIndex = 0;
            // 
            // lblLicencesHead
            // 
            lblLicencesHead.AutoSize = true;
            lblLicencesHead.Dock = DockStyle.Fill;
            lblLicencesHead.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
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
            // TextViewerForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(634, 261);
            Controls.Add(tlpLicences);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TextViewerForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Licences";
            tlpLicences.ResumeLayout(false);
            tlpLicences.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpLicences;
        private Label lblLicencesHead;
        private RichTextBox rtbLicences;
    }
}
