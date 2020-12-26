namespace FolderComparer2.Forms
{
    partial class MainView
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelSecond = new System.Windows.Forms.TableLayoutPanel();
            this.buttonChooseFolder2 = new System.Windows.Forms.Button();
            this.richTextBoxFolder2 = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanelFirst = new System.Windows.Forms.TableLayoutPanel();
            this.buttonChooseFolder1 = new System.Windows.Forms.Button();
            this.richTextBoxFolder1 = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanelThird = new System.Windows.Forms.TableLayoutPanel();
            this.buttonCompare = new System.Windows.Forms.Button();
            this.tableLayoutPanelThirdSmall = new System.Windows.Forms.TableLayoutPanel();
            this.labelFolder2Subfolders = new System.Windows.Forms.Label();
            this.labelFolder2Files = new System.Windows.Forms.Label();
            this.labelFolder2Size = new System.Windows.Forms.Label();
            this.labelFolder1Caption = new System.Windows.Forms.Label();
            this.labelFolder2Caption = new System.Windows.Forms.Label();
            this.labelSize = new System.Windows.Forms.Label();
            this.labelFiles = new System.Windows.Forms.Label();
            this.labelSubfolders = new System.Windows.Forms.Label();
            this.labelFolder1Size = new System.Windows.Forms.Label();
            this.labelFolder1Files = new System.Windows.Forms.Label();
            this.labelFolder1Subfolders = new System.Windows.Forms.Label();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelSecond.SuspendLayout();
            this.tableLayoutPanelFirst.SuspendLayout();
            this.tableLayoutPanelThird.SuspendLayout();
            this.tableLayoutPanelThirdSmall.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelMain.ColumnCount = 3;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelSecond, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelFirst, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelThird, 2, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 1;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1103, 461);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // tableLayoutPanelSecond
            // 
            this.tableLayoutPanelSecond.ColumnCount = 1;
            this.tableLayoutPanelSecond.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSecond.Controls.Add(this.buttonChooseFolder2, 0, 0);
            this.tableLayoutPanelSecond.Controls.Add(this.richTextBoxFolder2, 0, 1);
            this.tableLayoutPanelSecond.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelSecond.Location = new System.Drawing.Point(370, 3);
            this.tableLayoutPanelSecond.Name = "tableLayoutPanelSecond";
            this.tableLayoutPanelSecond.RowCount = 2;
            this.tableLayoutPanelSecond.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelSecond.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanelSecond.Size = new System.Drawing.Size(361, 455);
            this.tableLayoutPanelSecond.TabIndex = 3;
            // 
            // buttonChooseFolder2
            // 
            this.buttonChooseFolder2.Location = new System.Drawing.Point(3, 3);
            this.buttonChooseFolder2.Name = "buttonChooseFolder2";
            this.buttonChooseFolder2.Size = new System.Drawing.Size(100, 23);
            this.buttonChooseFolder2.TabIndex = 0;
            this.buttonChooseFolder2.Text = "Ordner 2 wählen";
            this.buttonChooseFolder2.UseVisualStyleBackColor = true;
            this.buttonChooseFolder2.Click += new System.EventHandler(this.ChooseFolder2Click);
            // 
            // richTextBoxFolder2
            // 
            this.richTextBoxFolder2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxFolder2.Location = new System.Drawing.Point(3, 48);
            this.richTextBoxFolder2.Name = "richTextBoxFolder2";
            this.richTextBoxFolder2.ReadOnly = true;
            this.richTextBoxFolder2.Size = new System.Drawing.Size(355, 404);
            this.richTextBoxFolder2.TabIndex = 2;
            this.richTextBoxFolder2.Text = "";
            // 
            // tableLayoutPanelFirst
            // 
            this.tableLayoutPanelFirst.ColumnCount = 1;
            this.tableLayoutPanelFirst.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelFirst.Controls.Add(this.buttonChooseFolder1, 0, 0);
            this.tableLayoutPanelFirst.Controls.Add(this.richTextBoxFolder1, 0, 1);
            this.tableLayoutPanelFirst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelFirst.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelFirst.Name = "tableLayoutPanelFirst";
            this.tableLayoutPanelFirst.RowCount = 2;
            this.tableLayoutPanelFirst.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelFirst.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanelFirst.Size = new System.Drawing.Size(361, 455);
            this.tableLayoutPanelFirst.TabIndex = 2;
            // 
            // buttonChooseFolder1
            // 
            this.buttonChooseFolder1.Location = new System.Drawing.Point(3, 3);
            this.buttonChooseFolder1.Name = "buttonChooseFolder1";
            this.buttonChooseFolder1.Size = new System.Drawing.Size(100, 23);
            this.buttonChooseFolder1.TabIndex = 0;
            this.buttonChooseFolder1.Text = "Ordner 1 wählen";
            this.buttonChooseFolder1.UseVisualStyleBackColor = true;
            this.buttonChooseFolder1.Click += new System.EventHandler(this.ChooseFolder1Click);
            // 
            // richTextBoxFolder1
            // 
            this.richTextBoxFolder1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxFolder1.Location = new System.Drawing.Point(3, 48);
            this.richTextBoxFolder1.Name = "richTextBoxFolder1";
            this.richTextBoxFolder1.ReadOnly = true;
            this.richTextBoxFolder1.Size = new System.Drawing.Size(355, 404);
            this.richTextBoxFolder1.TabIndex = 1;
            this.richTextBoxFolder1.Text = "";
            // 
            // tableLayoutPanelThird
            // 
            this.tableLayoutPanelThird.ColumnCount = 1;
            this.tableLayoutPanelThird.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelThird.Controls.Add(this.tableLayoutPanelThirdSmall, 0, 1);
            this.tableLayoutPanelThird.Controls.Add(this.comboBoxLanguage, 0, 0);
            this.tableLayoutPanelThird.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelThird.Location = new System.Drawing.Point(737, 3);
            this.tableLayoutPanelThird.Name = "tableLayoutPanelThird";
            this.tableLayoutPanelThird.RowCount = 2;
            this.tableLayoutPanelThird.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelThird.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanelThird.Size = new System.Drawing.Size(363, 455);
            this.tableLayoutPanelThird.TabIndex = 4;
            // 
            // buttonCompare
            // 
            this.buttonCompare.Location = new System.Drawing.Point(6, 6);
            this.buttonCompare.Name = "buttonCompare";
            this.buttonCompare.Size = new System.Drawing.Size(109, 23);
            this.buttonCompare.TabIndex = 2;
            this.buttonCompare.Text = "Ordner vergleichen";
            this.buttonCompare.UseVisualStyleBackColor = true;
            this.buttonCompare.Click += new System.EventHandler(this.CompareClick);
            // 
            // tableLayoutPanelThirdSmall
            // 
            this.tableLayoutPanelThirdSmall.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanelThirdSmall.ColumnCount = 3;
            this.tableLayoutPanelThirdSmall.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelThirdSmall.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelThirdSmall.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelThirdSmall.Controls.Add(this.buttonCompare, 0, 0);
            this.tableLayoutPanelThirdSmall.Controls.Add(this.labelFolder2Subfolders, 2, 3);
            this.tableLayoutPanelThirdSmall.Controls.Add(this.labelFolder2Files, 2, 2);
            this.tableLayoutPanelThirdSmall.Controls.Add(this.labelFolder2Size, 2, 1);
            this.tableLayoutPanelThirdSmall.Controls.Add(this.labelFolder1Caption, 1, 0);
            this.tableLayoutPanelThirdSmall.Controls.Add(this.labelFolder2Caption, 2, 0);
            this.tableLayoutPanelThirdSmall.Controls.Add(this.labelSize, 0, 1);
            this.tableLayoutPanelThirdSmall.Controls.Add(this.labelFiles, 0, 2);
            this.tableLayoutPanelThirdSmall.Controls.Add(this.labelSubfolders, 0, 3);
            this.tableLayoutPanelThirdSmall.Controls.Add(this.labelFolder1Size, 1, 1);
            this.tableLayoutPanelThirdSmall.Controls.Add(this.labelFolder1Files, 1, 2);
            this.tableLayoutPanelThirdSmall.Controls.Add(this.labelFolder1Subfolders, 1, 3);
            this.tableLayoutPanelThirdSmall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelThirdSmall.Location = new System.Drawing.Point(3, 48);
            this.tableLayoutPanelThirdSmall.Name = "tableLayoutPanelThirdSmall";
            this.tableLayoutPanelThirdSmall.RowCount = 4;
            this.tableLayoutPanelThirdSmall.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelThirdSmall.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelThirdSmall.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelThirdSmall.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelThirdSmall.Size = new System.Drawing.Size(357, 404);
            this.tableLayoutPanelThirdSmall.TabIndex = 3;
            // 
            // labelFolder2Subfolders
            // 
            this.labelFolder2Subfolders.AutoSize = true;
            this.labelFolder2Subfolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelFolder2Subfolders.Location = new System.Drawing.Point(242, 303);
            this.labelFolder2Subfolders.Name = "labelFolder2Subfolders";
            this.labelFolder2Subfolders.Size = new System.Drawing.Size(109, 98);
            this.labelFolder2Subfolders.TabIndex = 11;
            // 
            // labelFolder2Files
            // 
            this.labelFolder2Files.AutoSize = true;
            this.labelFolder2Files.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelFolder2Files.Location = new System.Drawing.Point(242, 203);
            this.labelFolder2Files.Name = "labelFolder2Files";
            this.labelFolder2Files.Size = new System.Drawing.Size(109, 97);
            this.labelFolder2Files.TabIndex = 10;
            // 
            // labelFolder2Size
            // 
            this.labelFolder2Size.AutoSize = true;
            this.labelFolder2Size.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelFolder2Size.Location = new System.Drawing.Point(242, 103);
            this.labelFolder2Size.Name = "labelFolder2Size";
            this.labelFolder2Size.Size = new System.Drawing.Size(109, 97);
            this.labelFolder2Size.TabIndex = 8;
            // 
            // labelFolder1Caption
            // 
            this.labelFolder1Caption.AutoSize = true;
            this.labelFolder1Caption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelFolder1Caption.Location = new System.Drawing.Point(124, 3);
            this.labelFolder1Caption.Name = "labelFolder1Caption";
            this.labelFolder1Caption.Size = new System.Drawing.Size(109, 97);
            this.labelFolder1Caption.TabIndex = 0;
            this.labelFolder1Caption.Text = "Ordner 1:";
            // 
            // labelFolder2Caption
            // 
            this.labelFolder2Caption.AutoSize = true;
            this.labelFolder2Caption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelFolder2Caption.Location = new System.Drawing.Point(242, 3);
            this.labelFolder2Caption.Name = "labelFolder2Caption";
            this.labelFolder2Caption.Size = new System.Drawing.Size(109, 97);
            this.labelFolder2Caption.TabIndex = 1;
            this.labelFolder2Caption.Text = "Ordner 2:";
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSize.Location = new System.Drawing.Point(6, 103);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(109, 97);
            this.labelSize.TabIndex = 2;
            this.labelSize.Text = "Größe:";
            // 
            // labelFiles
            // 
            this.labelFiles.AutoSize = true;
            this.labelFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelFiles.Location = new System.Drawing.Point(6, 203);
            this.labelFiles.Name = "labelFiles";
            this.labelFiles.Size = new System.Drawing.Size(109, 97);
            this.labelFiles.TabIndex = 3;
            this.labelFiles.Text = "Dateien:";
            // 
            // labelSubfolders
            // 
            this.labelSubfolders.AutoSize = true;
            this.labelSubfolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSubfolders.Location = new System.Drawing.Point(6, 303);
            this.labelSubfolders.Name = "labelSubfolders";
            this.labelSubfolders.Size = new System.Drawing.Size(109, 98);
            this.labelSubfolders.TabIndex = 4;
            this.labelSubfolders.Text = "Unterordner:";
            // 
            // labelFolder1Size
            // 
            this.labelFolder1Size.AutoSize = true;
            this.labelFolder1Size.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelFolder1Size.Location = new System.Drawing.Point(124, 103);
            this.labelFolder1Size.Name = "labelFolder1Size";
            this.labelFolder1Size.Size = new System.Drawing.Size(109, 97);
            this.labelFolder1Size.TabIndex = 5;
            // 
            // labelFolder1Files
            // 
            this.labelFolder1Files.AutoSize = true;
            this.labelFolder1Files.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelFolder1Files.Location = new System.Drawing.Point(124, 203);
            this.labelFolder1Files.Name = "labelFolder1Files";
            this.labelFolder1Files.Size = new System.Drawing.Size(109, 97);
            this.labelFolder1Files.TabIndex = 6;
            // 
            // labelFolder1Subfolders
            // 
            this.labelFolder1Subfolders.AutoSize = true;
            this.labelFolder1Subfolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelFolder1Subfolders.Location = new System.Drawing.Point(124, 303);
            this.labelFolder1Subfolders.Name = "labelFolder1Subfolders";
            this.labelFolder1Subfolders.Size = new System.Drawing.Size(109, 98);
            this.labelFolder1Subfolders.TabIndex = 7;
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Location = new System.Drawing.Point(3, 3);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(180, 21);
            this.comboBoxLanguage.TabIndex = 4;
            this.comboBoxLanguage.SelectedIndexChanged += new System.EventHandler(this.ComboBoxLanguageSelectedIndexChanged);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1103, 461);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(742, 441);
            this.Name = "MainView";
            this.Text = "FolderComparer";
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelSecond.ResumeLayout(false);
            this.tableLayoutPanelFirst.ResumeLayout(false);
            this.tableLayoutPanelThird.ResumeLayout(false);
            this.tableLayoutPanelThirdSmall.ResumeLayout(false);
            this.tableLayoutPanelThirdSmall.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Button buttonChooseFolder1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSecond;
        private System.Windows.Forms.Button buttonChooseFolder2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFirst;
        private System.Windows.Forms.RichTextBox richTextBoxFolder2;
        private System.Windows.Forms.RichTextBox richTextBoxFolder1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelThird;
        private System.Windows.Forms.Button buttonCompare;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelThirdSmall;
        private System.Windows.Forms.Label labelFolder1Caption;
        private System.Windows.Forms.Label labelFolder2Caption;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.Label labelFiles;
        private System.Windows.Forms.Label labelSubfolders;
        private System.Windows.Forms.Label labelFolder2Subfolders;
        private System.Windows.Forms.Label labelFolder2Files;
        private System.Windows.Forms.Label labelFolder2Size;
        private System.Windows.Forms.Label labelFolder1Size;
        private System.Windows.Forms.Label labelFolder1Files;
        private System.Windows.Forms.Label labelFolder1Subfolders;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
    }
}

