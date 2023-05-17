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
            colorEditor = new Cyotek.Windows.Forms.ColorEditor();
            tlpColorPickerForm = new TableLayoutPanel();
            lblExample = new Label();
            tlpColorPickerForm.SuspendLayout();
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
            colorEditor.TabIndex = 0;
            colorEditor.TabStop = false;
            colorEditor.ColorChanged += ColorChanged;
            // 
            // tlpColorPickerForm
            // 
            tlpColorPickerForm.ColumnCount = 1;
            tlpColorPickerForm.ColumnStyles.Add(new ColumnStyle());
            tlpColorPickerForm.Controls.Add(colorEditor, 0, 0);
            tlpColorPickerForm.Controls.Add(lblExample, 0, 1);
            tlpColorPickerForm.Dock = DockStyle.Fill;
            tlpColorPickerForm.Location = new Point(0, 0);
            tlpColorPickerForm.Name = "tlpColorPickerForm";
            tlpColorPickerForm.RowCount = 2;
            tlpColorPickerForm.RowStyles.Add(new RowStyle());
            tlpColorPickerForm.RowStyles.Add(new RowStyle());
            tlpColorPickerForm.Size = new Size(384, 176);
            tlpColorPickerForm.TabIndex = 1;
            // 
            // lblExample
            // 
            lblExample.AutoSize = true;
            lblExample.Dock = DockStyle.Fill;
            lblExample.Location = new Point(3, 143);
            lblExample.Name = "lblExample";
            lblExample.Size = new Size(378, 33);
            lblExample.TabIndex = 1;
            lblExample.Text = "Das ist ein Beispieltext in der ausgewählten Farbe.";
            // 
            // ColorPickerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 176);
            Controls.Add(tlpColorPickerForm);
            FormBorderStyle = FormBorderStyle.FixedSingle;
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
            ResumeLayout(false);
        }

        #endregion

        private Cyotek.Windows.Forms.ColorEditor colorEditor;
        private TableLayoutPanel tlpColorPickerForm;
        private Label lblExample;
    }
}