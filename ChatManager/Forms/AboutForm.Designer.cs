using System.Drawing;
using System.Windows.Forms;

namespace ChatManager.Forms
{
    partial class AboutForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            tableLayoutPanel = new TableLayoutPanel();
            logoPictureBox = new PictureBox();
            labelProductName = new Label();
            labelVersion = new Label();
            labelCopyright = new Label();
            okButton = new Button();
            licencesButton = new Button();
            gitHubLinkButton = new Button();
            rtbCompany = new RichTextBox();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel.Controls.Add(logoPictureBox, 0, 0);
            tableLayoutPanel.Controls.Add(labelProductName, 1, 0);
            tableLayoutPanel.Controls.Add(labelVersion, 1, 1);
            tableLayoutPanel.Controls.Add(labelCopyright, 1, 2);
            tableLayoutPanel.Controls.Add(okButton, 1, 5);
            tableLayoutPanel.Controls.Add(licencesButton, 0, 5);
            tableLayoutPanel.Controls.Add(gitHubLinkButton, 1, 4);
            tableLayoutPanel.Controls.Add(rtbCompany, 1, 3);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(10, 10);
            tableLayoutPanel.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 6;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66667F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel.Size = new Size(484, 216);
            tableLayoutPanel.TabIndex = 0;
            // 
            // logoPictureBox
            // 
            logoPictureBox.Dock = DockStyle.Fill;
            logoPictureBox.Image = Properties.Resources.Logo_small;
            logoPictureBox.Location = new Point(4, 3);
            logoPictureBox.Margin = new Padding(4, 3, 4, 3);
            logoPictureBox.Name = "logoPictureBox";
            tableLayoutPanel.SetRowSpan(logoPictureBox, 5);
            logoPictureBox.Size = new Size(234, 174);
            logoPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            logoPictureBox.TabIndex = 12;
            logoPictureBox.TabStop = false;
            // 
            // labelProductName
            // 
            labelProductName.Dock = DockStyle.Fill;
            labelProductName.Location = new Point(249, 0);
            labelProductName.Margin = new Padding(7, 0, 4, 0);
            labelProductName.MaximumSize = new Size(0, 20);
            labelProductName.Name = "labelProductName";
            labelProductName.Size = new Size(231, 20);
            labelProductName.TabIndex = 0;
            labelProductName.Text = "Product Name";
            labelProductName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelVersion
            // 
            labelVersion.Dock = DockStyle.Fill;
            labelVersion.Location = new Point(249, 36);
            labelVersion.Margin = new Padding(7, 0, 4, 0);
            labelVersion.MaximumSize = new Size(0, 20);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new Size(231, 20);
            labelVersion.TabIndex = 0;
            labelVersion.Text = "Version";
            labelVersion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelCopyright
            // 
            labelCopyright.Dock = DockStyle.Fill;
            labelCopyright.Location = new Point(249, 72);
            labelCopyright.Margin = new Padding(7, 0, 4, 0);
            labelCopyright.MaximumSize = new Size(0, 20);
            labelCopyright.Name = "labelCopyright";
            labelCopyright.Size = new Size(231, 20);
            labelCopyright.TabIndex = 0;
            labelCopyright.Text = "Copyright";
            labelCopyright.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.DialogResult = DialogResult.Cancel;
            okButton.Location = new Point(392, 186);
            okButton.Margin = new Padding(4, 3, 4, 3);
            okButton.Name = "okButton";
            okButton.Size = new Size(88, 27);
            okButton.TabIndex = 1;
            okButton.Text = "Ok";
            // 
            // licencesButton
            // 
            licencesButton.Dock = DockStyle.Fill;
            licencesButton.Location = new Point(3, 183);
            licencesButton.Name = "licencesButton";
            licencesButton.Size = new Size(236, 30);
            licencesButton.TabIndex = 3;
            licencesButton.Text = "Licences";
            licencesButton.UseVisualStyleBackColor = true;
            licencesButton.Click += LicencesButton_Click;
            // 
            // gitHubLinkButton
            // 
            gitHubLinkButton.Dock = DockStyle.Fill;
            gitHubLinkButton.Location = new Point(245, 147);
            gitHubLinkButton.Name = "gitHubLinkButton";
            gitHubLinkButton.Size = new Size(236, 30);
            gitHubLinkButton.TabIndex = 2;
            gitHubLinkButton.Text = "Link to GitHub";
            gitHubLinkButton.UseVisualStyleBackColor = true;
            gitHubLinkButton.Click += GitHubLinkButton_Click;
            // 
            // rtbCompany
            // 
            rtbCompany.BackColor = SystemColors.Control;
            rtbCompany.BorderStyle = BorderStyle.None;
            rtbCompany.CausesValidation = false;
            rtbCompany.DetectUrls = false;
            rtbCompany.Dock = DockStyle.Fill;
            rtbCompany.Location = new Point(249, 108);
            rtbCompany.Margin = new Padding(7, 0, 4, 0);
            rtbCompany.Multiline = false;
            rtbCompany.Name = "rtbCompany";
            rtbCompany.ReadOnly = true;
            rtbCompany.ScrollBars = RichTextBoxScrollBars.None;
            rtbCompany.Size = new Size(231, 36);
            rtbCompany.TabIndex = 0;
            rtbCompany.TabStop = false;
            rtbCompany.Text = "Placeholder";
            rtbCompany.SelectionChanged += RtbCompany_SelectionChanged;
            rtbCompany.Click += RtbCompany_GotFocus;
            rtbCompany.GotFocus += RtbCompany_GotFocus;
            // 
            // AboutForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(504, 236);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutForm";
            Padding = new Padding(10);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "About this app";
            tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel;
        private PictureBox logoPictureBox;
        private Label labelProductName;
        private Label labelVersion;
        private Label labelCopyright;
        private Button okButton;
        private Button licencesButton;
        private Button gitHubLinkButton;
        private RichTextBox rtbCompany;
    }
}
