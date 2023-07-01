namespace ChatManager.Forms
{
    partial class FileSelectorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileSelectorForm));
            tabsFileSelector = new TabControl();
            tpStarForge = new TabPage();
            tlpStarForge = new TableLayoutPanel();
            btnStarForgeSelectAll = new Button();
            btnStarForgeDeselectAll = new Button();
            btnStarForgeSelect = new Button();
            tpSateleShan = new TabPage();
            tlpSateleShan = new TableLayoutPanel();
            btnSateleShanSelect = new Button();
            btnSateleShanSelectAll = new Button();
            btnSateleShanDeselectAll = new Button();
            tpDarthMalgus = new TabPage();
            tlpDarthMalgus = new TableLayoutPanel();
            btnDarthMalgusSelect = new Button();
            btnDarthMalgusSelectAll = new Button();
            btnDarthMalgusDeselectAll = new Button();
            tpTulakHord = new TabPage();
            tlpTulakHord = new TableLayoutPanel();
            btnTulakHordSelect = new Button();
            btnTulakHordSelectAll = new Button();
            btnTulakHordDeselectAll = new Button();
            tpTheLeviathan = new TabPage();
            tlpTheLeviathan = new TableLayoutPanel();
            btnTheLeviathanSelect = new Button();
            btnTheLeviathanSelectAll = new Button();
            btnTheLeviathanDeselectAll = new Button();
            tabsFileSelector.SuspendLayout();
            tpStarForge.SuspendLayout();
            tlpStarForge.SuspendLayout();
            tpSateleShan.SuspendLayout();
            tlpSateleShan.SuspendLayout();
            tpDarthMalgus.SuspendLayout();
            tlpDarthMalgus.SuspendLayout();
            tpTulakHord.SuspendLayout();
            tlpTulakHord.SuspendLayout();
            tpTheLeviathan.SuspendLayout();
            tlpTheLeviathan.SuspendLayout();
            SuspendLayout();
            // 
            // tabsFileSelector
            // 
            tabsFileSelector.Controls.Add(tpDarthMalgus);
            tabsFileSelector.Controls.Add(tpSateleShan);
            tabsFileSelector.Controls.Add(tpStarForge);
            tabsFileSelector.Controls.Add(tpTheLeviathan);
            tabsFileSelector.Controls.Add(tpTulakHord);
            tabsFileSelector.Dock = DockStyle.Fill;
            tabsFileSelector.Location = new Point(0, 0);
            tabsFileSelector.Name = "tabsFileSelector";
            tabsFileSelector.SelectedIndex = 0;
            tabsFileSelector.Size = new Size(484, 261);
            tabsFileSelector.TabIndex = 1;
            // 
            // tpStarForge
            // 
            tpStarForge.Controls.Add(tlpStarForge);
            tpStarForge.Location = new Point(4, 24);
            tpStarForge.Name = "tpStarForge";
            tpStarForge.Padding = new Padding(3);
            tpStarForge.Size = new Size(476, 233);
            tpStarForge.TabIndex = 0;
            tpStarForge.Text = "Star Forge";
            tpStarForge.UseVisualStyleBackColor = true;
            // 
            // tlpStarForge
            // 
            tlpStarForge.ColumnCount = 3;
            tlpStarForge.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tlpStarForge.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tlpStarForge.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tlpStarForge.Controls.Add(btnStarForgeSelectAll, 0, 1);
            tlpStarForge.Controls.Add(btnStarForgeDeselectAll, 2, 1);
            tlpStarForge.Controls.Add(btnStarForgeSelect, 1, 1);
            tlpStarForge.Dock = DockStyle.Fill;
            tlpStarForge.Location = new Point(3, 3);
            tlpStarForge.Name = "tlpStarForge";
            tlpStarForge.RowCount = 2;
            tlpStarForge.RowStyles.Add(new RowStyle(SizeType.Percent, 85F));
            tlpStarForge.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tlpStarForge.Size = new Size(470, 227);
            tlpStarForge.TabIndex = 0;
            // 
            // btnStarForgeSelectAll
            // 
            btnStarForgeSelectAll.Dock = DockStyle.Fill;
            btnStarForgeSelectAll.Location = new Point(3, 195);
            btnStarForgeSelectAll.Name = "btnStarForgeSelectAll";
            btnStarForgeSelectAll.Size = new Size(150, 29);
            btnStarForgeSelectAll.TabIndex = 9;
            btnStarForgeSelectAll.Text = "Alle auswählen";
            btnStarForgeSelectAll.UseVisualStyleBackColor = true;
            btnStarForgeSelectAll.Click += SelectClick;
            // 
            // btnStarForgeDeselectAll
            // 
            btnStarForgeDeselectAll.Dock = DockStyle.Fill;
            btnStarForgeDeselectAll.Location = new Point(315, 195);
            btnStarForgeDeselectAll.Name = "btnStarForgeDeselectAll";
            btnStarForgeDeselectAll.Size = new Size(152, 29);
            btnStarForgeDeselectAll.TabIndex = 10;
            btnStarForgeDeselectAll.Text = "Alle abwählen";
            btnStarForgeDeselectAll.UseVisualStyleBackColor = true;
            btnStarForgeDeselectAll.Click += SelectClick;
            // 
            // btnStarForgeSelect
            // 
            btnStarForgeSelect.Dock = DockStyle.Fill;
            btnStarForgeSelect.Location = new Point(159, 195);
            btnStarForgeSelect.Name = "btnStarForgeSelect";
            btnStarForgeSelect.Size = new Size(150, 29);
            btnStarForgeSelect.TabIndex = 11;
            btnStarForgeSelect.Tag = "lbxStarForge";
            btnStarForgeSelect.Text = "Auswählen";
            btnStarForgeSelect.UseVisualStyleBackColor = true;
            btnStarForgeSelect.Click += ListBoxClick;
            // 
            // tpSateleShan
            // 
            tpSateleShan.Controls.Add(tlpSateleShan);
            tpSateleShan.Location = new Point(4, 24);
            tpSateleShan.Name = "tpSateleShan";
            tpSateleShan.Padding = new Padding(3);
            tpSateleShan.Size = new Size(476, 233);
            tpSateleShan.TabIndex = 1;
            tpSateleShan.Text = "Satele Shan";
            tpSateleShan.UseVisualStyleBackColor = true;
            // 
            // tlpSateleShan
            // 
            tlpSateleShan.ColumnCount = 3;
            tlpSateleShan.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpSateleShan.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpSateleShan.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpSateleShan.Controls.Add(btnSateleShanSelect, 0, 1);
            tlpSateleShan.Controls.Add(btnSateleShanSelectAll, 0, 1);
            tlpSateleShan.Controls.Add(btnSateleShanDeselectAll, 2, 1);
            tlpSateleShan.Dock = DockStyle.Fill;
            tlpSateleShan.Location = new Point(3, 3);
            tlpSateleShan.Name = "tlpSateleShan";
            tlpSateleShan.RowCount = 2;
            tlpSateleShan.RowStyles.Add(new RowStyle(SizeType.Percent, 85F));
            tlpSateleShan.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tlpSateleShan.Size = new Size(470, 227);
            tlpSateleShan.TabIndex = 0;
            // 
            // btnSateleShanSelect
            // 
            btnSateleShanSelect.Dock = DockStyle.Fill;
            btnSateleShanSelect.Location = new Point(159, 195);
            btnSateleShanSelect.Name = "btnSateleShanSelect";
            btnSateleShanSelect.Size = new Size(150, 29);
            btnSateleShanSelect.TabIndex = 8;
            btnSateleShanSelect.Tag = "lbxSateleShan";
            btnSateleShanSelect.Text = "Auswählen";
            btnSateleShanSelect.UseVisualStyleBackColor = true;
            btnSateleShanSelect.Click += ListBoxClick;
            // 
            // btnSateleShanSelectAll
            // 
            btnSateleShanSelectAll.Dock = DockStyle.Fill;
            btnSateleShanSelectAll.Location = new Point(3, 195);
            btnSateleShanSelectAll.Name = "btnSateleShanSelectAll";
            btnSateleShanSelectAll.Size = new Size(150, 29);
            btnSateleShanSelectAll.TabIndex = 6;
            btnSateleShanSelectAll.Text = "Alle auswählen";
            btnSateleShanSelectAll.UseVisualStyleBackColor = true;
            btnSateleShanSelectAll.Click += SelectClick;
            // 
            // btnSateleShanDeselectAll
            // 
            btnSateleShanDeselectAll.Dock = DockStyle.Fill;
            btnSateleShanDeselectAll.Location = new Point(315, 195);
            btnSateleShanDeselectAll.Name = "btnSateleShanDeselectAll";
            btnSateleShanDeselectAll.Size = new Size(152, 29);
            btnSateleShanDeselectAll.TabIndex = 7;
            btnSateleShanDeselectAll.Text = "Alle abwählen";
            btnSateleShanDeselectAll.UseVisualStyleBackColor = true;
            btnSateleShanDeselectAll.Click += SelectClick;
            // 
            // tpDarthMalgus
            // 
            tpDarthMalgus.Controls.Add(tlpDarthMalgus);
            tpDarthMalgus.Location = new Point(4, 24);
            tpDarthMalgus.Name = "tpDarthMalgus";
            tpDarthMalgus.Size = new Size(476, 233);
            tpDarthMalgus.TabIndex = 2;
            tpDarthMalgus.Text = "Darth Malgus";
            tpDarthMalgus.UseVisualStyleBackColor = true;
            // 
            // tlpDarthMalgus
            // 
            tlpDarthMalgus.ColumnCount = 3;
            tlpDarthMalgus.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpDarthMalgus.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpDarthMalgus.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpDarthMalgus.Controls.Add(btnDarthMalgusSelect, 0, 1);
            tlpDarthMalgus.Controls.Add(btnDarthMalgusSelectAll, 0, 1);
            tlpDarthMalgus.Controls.Add(btnDarthMalgusDeselectAll, 2, 1);
            tlpDarthMalgus.Dock = DockStyle.Fill;
            tlpDarthMalgus.Location = new Point(0, 0);
            tlpDarthMalgus.Name = "tlpDarthMalgus";
            tlpDarthMalgus.RowCount = 2;
            tlpDarthMalgus.RowStyles.Add(new RowStyle(SizeType.Percent, 85F));
            tlpDarthMalgus.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tlpDarthMalgus.Size = new Size(476, 233);
            tlpDarthMalgus.TabIndex = 0;
            // 
            // btnDarthMalgusSelect
            // 
            btnDarthMalgusSelect.Dock = DockStyle.Fill;
            btnDarthMalgusSelect.Location = new Point(161, 201);
            btnDarthMalgusSelect.Name = "btnDarthMalgusSelect";
            btnDarthMalgusSelect.Size = new Size(152, 29);
            btnDarthMalgusSelect.TabIndex = 5;
            btnDarthMalgusSelect.Tag = "lbxDarthMalgus";
            btnDarthMalgusSelect.Text = "Auswählen";
            btnDarthMalgusSelect.UseVisualStyleBackColor = true;
            btnDarthMalgusSelect.Click += ListBoxClick;
            // 
            // btnDarthMalgusSelectAll
            // 
            btnDarthMalgusSelectAll.Dock = DockStyle.Fill;
            btnDarthMalgusSelectAll.Location = new Point(3, 201);
            btnDarthMalgusSelectAll.Name = "btnDarthMalgusSelectAll";
            btnDarthMalgusSelectAll.Size = new Size(152, 29);
            btnDarthMalgusSelectAll.TabIndex = 3;
            btnDarthMalgusSelectAll.Text = "Alle auswählen";
            btnDarthMalgusSelectAll.UseVisualStyleBackColor = true;
            btnDarthMalgusSelectAll.Click += SelectClick;
            // 
            // btnDarthMalgusDeselectAll
            // 
            btnDarthMalgusDeselectAll.Dock = DockStyle.Fill;
            btnDarthMalgusDeselectAll.Location = new Point(319, 201);
            btnDarthMalgusDeselectAll.Name = "btnDarthMalgusDeselectAll";
            btnDarthMalgusDeselectAll.Size = new Size(154, 29);
            btnDarthMalgusDeselectAll.TabIndex = 4;
            btnDarthMalgusDeselectAll.Text = "Alle abwählen";
            btnDarthMalgusDeselectAll.UseVisualStyleBackColor = true;
            btnDarthMalgusDeselectAll.Click += SelectClick;
            // 
            // tpTulakHord
            // 
            tpTulakHord.Controls.Add(tlpTulakHord);
            tpTulakHord.Location = new Point(4, 24);
            tpTulakHord.Name = "tpTulakHord";
            tpTulakHord.Size = new Size(476, 233);
            tpTulakHord.TabIndex = 3;
            tpTulakHord.Text = "Tulak Hord";
            tpTulakHord.UseVisualStyleBackColor = true;
            // 
            // tlpTulakHord
            // 
            tlpTulakHord.ColumnCount = 3;
            tlpTulakHord.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpTulakHord.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpTulakHord.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpTulakHord.Controls.Add(btnTulakHordSelect, 0, 1);
            tlpTulakHord.Controls.Add(btnTulakHordSelectAll, 0, 1);
            tlpTulakHord.Controls.Add(btnTulakHordDeselectAll, 2, 1);
            tlpTulakHord.Dock = DockStyle.Fill;
            tlpTulakHord.Location = new Point(0, 0);
            tlpTulakHord.Name = "tlpTulakHord";
            tlpTulakHord.RowCount = 2;
            tlpTulakHord.RowStyles.Add(new RowStyle(SizeType.Percent, 85F));
            tlpTulakHord.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tlpTulakHord.Size = new Size(476, 233);
            tlpTulakHord.TabIndex = 0;
            // 
            // btnTulakHordSelect
            // 
            btnTulakHordSelect.Dock = DockStyle.Fill;
            btnTulakHordSelect.Location = new Point(161, 201);
            btnTulakHordSelect.Name = "btnTulakHordSelect";
            btnTulakHordSelect.Size = new Size(152, 29);
            btnTulakHordSelect.TabIndex = 17;
            btnTulakHordSelect.Tag = "lbxTulakHord";
            btnTulakHordSelect.Text = "Auswählen";
            btnTulakHordSelect.UseVisualStyleBackColor = true;
            btnTulakHordSelect.Click += ListBoxClick;
            // 
            // btnTulakHordSelectAll
            // 
            btnTulakHordSelectAll.Dock = DockStyle.Fill;
            btnTulakHordSelectAll.Location = new Point(3, 201);
            btnTulakHordSelectAll.Name = "btnTulakHordSelectAll";
            btnTulakHordSelectAll.Size = new Size(152, 29);
            btnTulakHordSelectAll.TabIndex = 15;
            btnTulakHordSelectAll.Text = "Alle auswählen";
            btnTulakHordSelectAll.UseVisualStyleBackColor = true;
            btnTulakHordSelectAll.Click += SelectClick;
            // 
            // btnTulakHordDeselectAll
            // 
            btnTulakHordDeselectAll.Dock = DockStyle.Fill;
            btnTulakHordDeselectAll.Location = new Point(319, 201);
            btnTulakHordDeselectAll.Name = "btnTulakHordDeselectAll";
            btnTulakHordDeselectAll.Size = new Size(154, 29);
            btnTulakHordDeselectAll.TabIndex = 16;
            btnTulakHordDeselectAll.Text = "Alle abwählen";
            btnTulakHordDeselectAll.UseVisualStyleBackColor = true;
            btnTulakHordDeselectAll.Click += SelectClick;
            // 
            // tpTheLeviathan
            // 
            tpTheLeviathan.Controls.Add(tlpTheLeviathan);
            tpTheLeviathan.Location = new Point(4, 24);
            tpTheLeviathan.Name = "tpTheLeviathan";
            tpTheLeviathan.Size = new Size(476, 233);
            tpTheLeviathan.TabIndex = 4;
            tpTheLeviathan.Text = "The Leviathan";
            tpTheLeviathan.UseVisualStyleBackColor = true;
            // 
            // tlpTheLeviathan
            // 
            tlpTheLeviathan.ColumnCount = 3;
            tlpTheLeviathan.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpTheLeviathan.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpTheLeviathan.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpTheLeviathan.Controls.Add(btnTheLeviathanSelect, 0, 1);
            tlpTheLeviathan.Controls.Add(btnTheLeviathanSelectAll, 0, 1);
            tlpTheLeviathan.Controls.Add(btnTheLeviathanDeselectAll, 2, 1);
            tlpTheLeviathan.Dock = DockStyle.Fill;
            tlpTheLeviathan.Location = new Point(0, 0);
            tlpTheLeviathan.Name = "tlpTheLeviathan";
            tlpTheLeviathan.RowCount = 2;
            tlpTheLeviathan.RowStyles.Add(new RowStyle(SizeType.Percent, 85F));
            tlpTheLeviathan.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tlpTheLeviathan.Size = new Size(476, 233);
            tlpTheLeviathan.TabIndex = 0;
            // 
            // btnTheLeviathanSelect
            // 
            btnTheLeviathanSelect.Dock = DockStyle.Fill;
            btnTheLeviathanSelect.Location = new Point(161, 201);
            btnTheLeviathanSelect.Name = "btnTheLeviathanSelect";
            btnTheLeviathanSelect.Size = new Size(152, 29);
            btnTheLeviathanSelect.TabIndex = 14;
            btnTheLeviathanSelect.Tag = "lbxTheLeviathan";
            btnTheLeviathanSelect.Text = "Auswählen";
            btnTheLeviathanSelect.UseVisualStyleBackColor = true;
            btnTheLeviathanSelect.Click += ListBoxClick;
            // 
            // btnTheLeviathanSelectAll
            // 
            btnTheLeviathanSelectAll.Dock = DockStyle.Fill;
            btnTheLeviathanSelectAll.Location = new Point(3, 201);
            btnTheLeviathanSelectAll.Name = "btnTheLeviathanSelectAll";
            btnTheLeviathanSelectAll.Size = new Size(152, 29);
            btnTheLeviathanSelectAll.TabIndex = 12;
            btnTheLeviathanSelectAll.Text = "Alle auswählen";
            btnTheLeviathanSelectAll.UseVisualStyleBackColor = true;
            btnTheLeviathanSelectAll.Click += SelectClick;
            // 
            // btnTheLeviathanDeselectAll
            // 
            btnTheLeviathanDeselectAll.Dock = DockStyle.Fill;
            btnTheLeviathanDeselectAll.Location = new Point(319, 201);
            btnTheLeviathanDeselectAll.Name = "btnTheLeviathanDeselectAll";
            btnTheLeviathanDeselectAll.Size = new Size(154, 29);
            btnTheLeviathanDeselectAll.TabIndex = 13;
            btnTheLeviathanDeselectAll.Text = "Alle abwählen";
            btnTheLeviathanDeselectAll.UseVisualStyleBackColor = true;
            btnTheLeviathanDeselectAll.Click += SelectClick;
            // 
            // FileSelectorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 261);
            Controls.Add(tabsFileSelector);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FileSelectorForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Charakter wählen";
            Load += FileSelectorForm_Load;
            tabsFileSelector.ResumeLayout(false);
            tpStarForge.ResumeLayout(false);
            tlpStarForge.ResumeLayout(false);
            tpSateleShan.ResumeLayout(false);
            tlpSateleShan.ResumeLayout(false);
            tpDarthMalgus.ResumeLayout(false);
            tlpDarthMalgus.ResumeLayout(false);
            tpTulakHord.ResumeLayout(false);
            tlpTulakHord.ResumeLayout(false);
            tpTheLeviathan.ResumeLayout(false);
            tlpTheLeviathan.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabsFileSelector;
        private TabPage tpSateleShan;
        private TabPage tpDarthMalgus;
        private TabPage tpTulakHord;
        private TabPage tpTheLeviathan;
        private TableLayoutPanel tlpDarthMalgus;
        private Button btnDarthMalgusSelectAll;
        private Button btnDarthMalgusDeselectAll;
        private TableLayoutPanel tlpTulakHord;
        private Button btnTulakHordSelectAll;
        private Button btnTulakHordDeselectAll;
        private TableLayoutPanel tlpTheLeviathan;
        private Button btnTheLeviathanSelectAll;
        private Button btnTheLeviathanDeselectAll;
        private TabPage tpStarForge;
        private TableLayoutPanel tlpStarForge;
        private Button btnStarForgeSelectAll;
        private Button btnStarForgeDeselectAll;
        private TableLayoutPanel tlpSateleShan;
        private Button btnSateleShanSelectAll;
        private Button btnSateleShanDeselectAll;
        private Button btnStarForgeSelect;
        private Button btnSateleShanSelect;
        private Button btnDarthMalgusSelect;
        private Button btnTulakHordSelect;
        private Button btnTheLeviathanSelect;
    }
}