namespace ChatManager.Forms
{
    partial class ColorPickerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorPickerForm));
            colorEditor = new Cyotek.Windows.Forms.ColorEditor();
            tlpColorPickerForm = new TableLayoutPanel();
            pnlExample = new Panel();
            lblExample = new Label();
            tlpColorPickerForm.SuspendLayout();
            pnlExample.SuspendLayout();
            SuspendLayout();
            // 
            // colorEditor
            // 
            colorEditor.Color = Color.FromArgb(255, 255, 255);
            colorEditor.Dock = DockStyle.Fill;
            colorEditor.Location = new Point(4, 3);
            colorEditor.Margin = new Padding(4, 3, 4, 3);
            colorEditor.Name = "colorEditor";
            colorEditor.ShowAlphaChannel = false;
            colorEditor.ShowHsl = false;
            colorEditor.Size = new Size(376, 137);
            colorEditor.TabIndex = 1;
            colorEditor.TabStop = false;
            colorEditor.ColorChanged += ColorChanged;
            // 
            // tlpColorPickerForm
            // 
            tlpColorPickerForm.ColumnCount = 1;
            tlpColorPickerForm.ColumnStyles.Add(new ColumnStyle());
            tlpColorPickerForm.Controls.Add(colorEditor, 0, 0);
            tlpColorPickerForm.Controls.Add(pnlExample, 0, 1);
            tlpColorPickerForm.Dock = DockStyle.Fill;
            tlpColorPickerForm.Location = new Point(0, 0);
            tlpColorPickerForm.Name = "tlpColorPickerForm";
            tlpColorPickerForm.RowCount = 2;
            tlpColorPickerForm.RowStyles.Add(new RowStyle());
            tlpColorPickerForm.RowStyles.Add(new RowStyle(SizeType.Percent, 95F));
            tlpColorPickerForm.Size = new Size(384, 311);
            tlpColorPickerForm.TabIndex = 0;
            // 
            // pnlExample
            // 
            pnlExample.Controls.Add(lblExample);
            pnlExample.Dock = DockStyle.Fill;
            pnlExample.Location = new Point(3, 146);
            pnlExample.Name = "pnlExample";
            pnlExample.Size = new Size(378, 162);
            pnlExample.TabIndex = 2;
            // 
            // lblExample
            // 
            lblExample.BackColor = Color.DimGray;
            lblExample.Dock = DockStyle.Fill;
            lblExample.Image = Properties.Resources.Chat_Layer;
            lblExample.Location = new Point(0, 0);
            lblExample.Name = "lblExample";
            lblExample.Size = new Size(378, 162);
            lblExample.TabIndex = 0;
            lblExample.Text = "This is a sample text in the selected color.";
            lblExample.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ColorPickerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 311);
            Controls.Add(tlpColorPickerForm);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(400, 215);
            Name = "ColorPickerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ColorPicker";
            FormClosing += ColorPickerForm_FormClosing;
            FormClosed += ColorPickerForm_FormClosed;
            Load += ColorPickerForm_Load;
            tlpColorPickerForm.ResumeLayout(false);
            pnlExample.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Cyotek.Windows.Forms.ColorEditor colorEditor;
        private TableLayoutPanel tlpColorPickerForm;
        private Label lblExample;
        private Panel pnlExample;
    }
}