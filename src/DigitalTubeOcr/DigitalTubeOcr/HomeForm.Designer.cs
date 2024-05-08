namespace DigitalTubeOcr
{
    partial class HomeForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            pic3 = new PictureBox();
            pic2 = new PictureBox();
            pic1 = new PictureBox();
            tabPage2 = new TabPage();
            txtLog = new TextBox();
            btnFushi = new Button();
            btnOpen = new Button();
            btnOcr = new Button();
            txtImgPath = new TextBox();
            btnPengzhang = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pic3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic1).BeginInit();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel2;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(btnPengzhang);
            splitContainer1.Panel2.Controls.Add(btnFushi);
            splitContainer1.Panel2.Controls.Add(btnOpen);
            splitContainer1.Panel2.Controls.Add(btnOcr);
            splitContainer1.Panel2.Controls.Add(txtImgPath);
            splitContainer1.Size = new Size(1070, 496);
            splitContainer1.SplitterDistance = 438;
            splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1070, 438);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(tableLayoutPanel1);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1062, 408);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "识别结果";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Controls.Add(pic3, 2, 0);
            tableLayoutPanel1.Controls.Add(pic2, 1, 0);
            tableLayoutPanel1.Controls.Add(pic1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1056, 402);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // pic3
            // 
            pic3.Dock = DockStyle.Fill;
            pic3.Location = new Point(707, 3);
            pic3.Name = "pic3";
            pic3.Size = new Size(346, 396);
            pic3.SizeMode = PictureBoxSizeMode.StretchImage;
            pic3.TabIndex = 2;
            pic3.TabStop = false;
            // 
            // pic2
            // 
            pic2.Dock = DockStyle.Fill;
            pic2.Location = new Point(355, 3);
            pic2.Name = "pic2";
            pic2.Size = new Size(346, 396);
            pic2.SizeMode = PictureBoxSizeMode.StretchImage;
            pic2.TabIndex = 1;
            pic2.TabStop = false;
            // 
            // pic1
            // 
            pic1.Dock = DockStyle.Fill;
            pic1.Location = new Point(3, 3);
            pic1.Name = "pic1";
            pic1.Size = new Size(346, 396);
            pic1.SizeMode = PictureBoxSizeMode.StretchImage;
            pic1.TabIndex = 0;
            pic1.TabStop = false;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(txtLog);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1062, 408);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "日志";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            txtLog.Dock = DockStyle.Fill;
            txtLog.Location = new Point(3, 3);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(1056, 402);
            txtLog.TabIndex = 0;
            // 
            // btnFushi
            // 
            btnFushi.Location = new Point(739, 19);
            btnFushi.Name = "btnFushi";
            btnFushi.Size = new Size(75, 23);
            btnFushi.TabIndex = 3;
            btnFushi.Text = "腐蚀";
            btnFushi.UseVisualStyleBackColor = true;
            btnFushi.Click += btnFushi_Click;
            // 
            // btnOpen
            // 
            btnOpen.Location = new Point(658, 19);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(75, 23);
            btnOpen.TabIndex = 2;
            btnOpen.Text = "打开";
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += btnOpen_Click;
            // 
            // btnOcr
            // 
            btnOcr.Location = new Point(973, 19);
            btnOcr.Name = "btnOcr";
            btnOcr.Size = new Size(75, 23);
            btnOcr.TabIndex = 1;
            btnOcr.Text = "识别";
            btnOcr.UseVisualStyleBackColor = true;
            btnOcr.Click += btnOcr_Click;
            // 
            // txtImgPath
            // 
            txtImgPath.Location = new Point(21, 19);
            txtImgPath.Name = "txtImgPath";
            txtImgPath.Size = new Size(621, 23);
            txtImgPath.TabIndex = 0;
            txtImgPath.MouseDown += txtImgPath_MouseDown;
            // 
            // btnPengzhang
            // 
            btnPengzhang.Location = new Point(820, 19);
            btnPengzhang.Name = "btnPengzhang";
            btnPengzhang.Size = new Size(75, 23);
            btnPengzhang.TabIndex = 4;
            btnPengzhang.Text = "膨胀";
            btnPengzhang.UseVisualStyleBackColor = true;
            btnPengzhang.Click += btnPengzhang_Click;
            // 
            // HomeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1070, 496);
            Controls.Add(splitContainer1);
            Name = "HomeForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "OCR";
            Load += HomeForm_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pic3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic1).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private PictureBox pic1;
        private TabPage tabPage2;
        private Button btnOcr;
        private TextBox txtImgPath;
        private TextBox txtLog;
        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pic3;
        private PictureBox pic2;
        private Button btnOpen;
        private Button btnFushi;
        private Button btnPengzhang;
    }
}
