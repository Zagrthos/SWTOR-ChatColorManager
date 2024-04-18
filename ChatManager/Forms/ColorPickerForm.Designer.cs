using System.Drawing;
using System.Windows.Forms;

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
            lblExample = new Label();
            lblFontSize = new Label();
            nbFontSize = new NumericUpDown();
            tlpColorPickerForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nbFontSize).BeginInit();
            SuspendLayout();
            // 
            // colorEditor
            // 
            colorEditor.Color = Color.FromArgb(255, 255, 255);
            tlpColorPickerForm.SetColumnSpan(colorEditor, 2);
            colorEditor.Dock = DockStyle.Fill;
            colorEditor.Location = new Point(4, 3);
            colorEditor.Margin = new Padding(4, 3, 4, 3);
            colorEditor.Name = "colorEditor";
            colorEditor.ShowAlphaChannel = false;
            colorEditor.ShowHsl = false;
            colorEditor.Size = new Size(401, 137);
            colorEditor.TabIndex = 1;
            colorEditor.ColorChanged += ColorChanged;
            // 
            // tlpColorPickerForm
            // 
            tlpColorPickerForm.ColumnCount = 2;
            tlpColorPickerForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpColorPickerForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpColorPickerForm.Controls.Add(lblExample, 0, 2);
            tlpColorPickerForm.Controls.Add(colorEditor, 0, 0);
            tlpColorPickerForm.Controls.Add(lblFontSize, 0, 1);
            tlpColorPickerForm.Controls.Add(nbFontSize, 1, 1);
            tlpColorPickerForm.Dock = DockStyle.Fill;
            tlpColorPickerForm.Location = new Point(0, 0);
            tlpColorPickerForm.Name = "tlpColorPickerForm";
            tlpColorPickerForm.RowCount = 3;
            tlpColorPickerForm.RowStyles.Add(new RowStyle());
            tlpColorPickerForm.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpColorPickerForm.RowStyles.Add(new RowStyle(SizeType.Percent, 87.5F));
            tlpColorPickerForm.Size = new Size(409, 321);
            tlpColorPickerForm.TabIndex = 0;
            // 
            // lblExample
            // 
            lblExample.BackColor = Color.DimGray;
            tlpColorPickerForm.SetColumnSpan(lblExample, 2);
            lblExample.Image = Properties.Resources.Chat_Layer;
            lblExample.Location = new Point(3, 165);
            lblExample.Name = "lblExample";
            lblExample.Size = new Size(403, 156);
            lblExample.TabIndex = 5;
            lblExample.Text = "This is a sample text in the selected color.";
            lblExample.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblFontSize
            // 
            lblFontSize.AutoSize = true;
            lblFontSize.Dock = DockStyle.Fill;
            lblFontSize.Location = new Point(3, 143);
            lblFontSize.Name = "lblFontSize";
            lblFontSize.Size = new Size(198, 22);
            lblFontSize.TabIndex = 3;
            lblFontSize.Text = "Font size";
            lblFontSize.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // nbFontSize
            // 
            nbFontSize.Dock = DockStyle.Fill;
            nbFontSize.Location = new Point(207, 146);
            nbFontSize.Maximum = new decimal(new int[] { 26, 0, 0, 0 });
            nbFontSize.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nbFontSize.Name = "nbFontSize";
            nbFontSize.Size = new Size(199, 23);
            nbFontSize.TabIndex = 0;
            nbFontSize.TabStop = false;
            nbFontSize.TextAlign = HorizontalAlignment.Center;
            nbFontSize.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nbFontSize.ValueChanged += FontSizeChanged;
            // 
            // ColorPickerForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(409, 321);
            Controls.Add(tlpColorPickerForm);
            FormBorderStyle = FormBorderStyle.FixedDialog;
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
            tlpColorPickerForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nbFontSize).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Cyotek.Windows.Forms.ColorEditor colorEditor;
        private TableLayoutPanel tlpColorPickerForm;
        private Label lblFontSize;
        private NumericUpDown nbFontSize;
        private Label lblExample;
    }
}
