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
            btnOcr = new Button();
            txtImgPath = new TextBox();
            labResult = new Label();
            tabPage2 = new TabPage();
            txtLog = new TextBox();
            tabPage1 = new TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            pic1 = new PictureBox();
            pic2 = new PictureBox();
            pic3 = new PictureBox();
            tabControl1 = new TabControl();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pic1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic3).BeginInit();
            tabControl1.SuspendLayout();
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
            splitContainer1.Panel2.Controls.Add(labResult);
            splitContainer1.Panel2.Controls.Add(btnOcr);
            splitContainer1.Panel2.Controls.Add(txtImgPath);
            splitContainer1.Size = new Size(1070, 520);
            splitContainer1.SplitterDistance = 473;
            splitContainer1.TabIndex = 0;
            // 
            // btnOcr
            // 
            btnOcr.Location = new Point(648, 8);
            btnOcr.Name = "btnOcr";
            btnOcr.Size = new Size(75, 23);
            btnOcr.TabIndex = 1;
            btnOcr.Text = "识别";
            btnOcr.UseVisualStyleBackColor = true;
            btnOcr.Click += btnOcr_Click;
            // 
            // txtImgPath
            // 
            txtImgPath.Location = new Point(21, 8);
            txtImgPath.Name = "txtImgPath";
            txtImgPath.Size = new Size(621, 23);
            txtImgPath.TabIndex = 0;
            txtImgPath.MouseDown += txtImgPath_MouseDown;
            // 
            // labResult
            // 
            labResult.AutoSize = true;
            labResult.Location = new Point(742, 11);
            labResult.Name = "labResult";
            labResult.Size = new Size(68, 17);
            labResult.TabIndex = 2;
            labResult.Text = "识别结果：";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(txtLog);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1062, 443);
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
            txtLog.Size = new Size(1056, 437);
            txtLog.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(tableLayoutPanel1);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1062, 443);
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
            tableLayoutPanel1.Size = new Size(1056, 437);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // pic1
            // 
            pic1.Dock = DockStyle.Fill;
            pic1.Location = new Point(3, 3);
            pic1.Name = "pic1";
            pic1.Size = new Size(346, 431);
            pic1.SizeMode = PictureBoxSizeMode.StretchImage;
            pic1.TabIndex = 0;
            pic1.TabStop = false;
            // 
            // pic2
            // 
            pic2.Dock = DockStyle.Fill;
            pic2.Location = new Point(355, 3);
            pic2.Name = "pic2";
            pic2.Size = new Size(346, 431);
            pic2.SizeMode = PictureBoxSizeMode.StretchImage;
            pic2.TabIndex = 1;
            pic2.TabStop = false;
            // 
            // pic3
            // 
            pic3.Dock = DockStyle.Fill;
            pic3.Location = new Point(707, 3);
            pic3.Name = "pic3";
            pic3.Size = new Size(346, 431);
            pic3.SizeMode = PictureBoxSizeMode.StretchImage;
            pic3.TabIndex = 2;
            pic3.TabStop = false;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1070, 473);
            tabControl1.TabIndex = 0;
            // 
            // HomeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1070, 520);
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
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pic1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic3).EndInit();
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Button btnOcr;
        private TextBox txtImgPath;
        private Label labResult;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pic3;
        private PictureBox pic2;
        private PictureBox pic1;
        private TabPage tabPage2;
        private TextBox txtLog;
    }
}
