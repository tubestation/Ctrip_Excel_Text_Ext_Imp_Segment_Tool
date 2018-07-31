namespace Ctrip_Excel_Text_Ext_Imp_Segment_Tool
{
    partial class frm_contentInfos
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lv_data = new System.Windows.Forms.ListView();
            this.SheetName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CellPosition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SegmentIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EndWhiteSpaceSign = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ContentText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbl_Total = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tb_Signs = new System.Windows.Forms.TextBox();
            this.btn_AutoSegment = new System.Windows.Forms.Button();
            this.cbb_Dels = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rtb_SelText = new System.Windows.Forms.RichTextBox();
            this.btn_ManalSegment = new System.Windows.Forms.Button();
            this.btn_AutoNumber = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_SaveAsHtml = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_RegExpText = new System.Windows.Forms.TextBox();
            this.btn_RegxSegment = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.rb_AtStart = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.btn_MergeText = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1178, 577);
            this.splitContainer1.SplitterDistance = 714;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.groupBox4);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.lbl_Total);
            this.splitContainer4.Panel2.Controls.Add(this.label4);
            this.splitContainer4.Size = new System.Drawing.Size(714, 577);
            this.splitContainer4.SplitterDistance = 533;
            this.splitContainer4.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lv_data);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(714, 533);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "文字数据";
            // 
            // lv_data
            // 
            this.lv_data.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SheetName,
            this.CellPosition,
            this.SegmentIndex,
            this.EndWhiteSpaceSign,
            this.ContentText});
            this.lv_data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_data.FullRowSelect = true;
            this.lv_data.Location = new System.Drawing.Point(3, 16);
            this.lv_data.Name = "lv_data";
            this.lv_data.Size = new System.Drawing.Size(708, 514);
            this.lv_data.TabIndex = 0;
            this.lv_data.UseCompatibleStateImageBehavior = false;
            this.lv_data.View = System.Windows.Forms.View.Details;
            this.lv_data.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lv_data_ItemSelectionChanged);
            // 
            // SheetName
            // 
            this.SheetName.Text = "Sheet Name";
            this.SheetName.Width = 72;
            // 
            // CellPosition
            // 
            this.CellPosition.Text = "Cell Position";
            this.CellPosition.Width = 70;
            // 
            // SegmentIndex
            // 
            this.SegmentIndex.Text = "Segment Index";
            this.SegmentIndex.Width = 84;
            // 
            // EndWhiteSpaceSign
            // 
            this.EndWhiteSpaceSign.Text = "End Sign";
            this.EndWhiteSpaceSign.Width = 58;
            // 
            // ContentText
            // 
            this.ContentText.Text = "Content Text";
            this.ContentText.Width = 399;
            // 
            // lbl_Total
            // 
            this.lbl_Total.AutoSize = true;
            this.lbl_Total.Location = new System.Drawing.Point(52, 0);
            this.lbl_Total.Name = "lbl_Total";
            this.lbl_Total.Size = new System.Drawing.Size(19, 13);
            this.lbl_Total.TabIndex = 0;
            this.lbl_Total.Text = "    ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "总数：";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(460, 577);
            this.splitContainer2.SplitterDistance = 136;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton1);
            this.groupBox3.Controls.Add(this.rb_AtStart);
            this.groupBox3.Controls.Add(this.tb_RegExpText);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.tb_Signs);
            this.groupBox3.Controls.Add(this.btn_RegxSegment);
            this.groupBox3.Controls.Add(this.btn_AutoSegment);
            this.groupBox3.Controls.Add(this.cbb_Dels);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(460, 136);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "自动分段";
            // 
            // tb_Signs
            // 
            this.tb_Signs.Location = new System.Drawing.Point(288, 24);
            this.tb_Signs.Name = "tb_Signs";
            this.tb_Signs.Size = new System.Drawing.Size(137, 20);
            this.tb_Signs.TabIndex = 3;
            // 
            // btn_AutoSegment
            // 
            this.btn_AutoSegment.Location = new System.Drawing.Point(9, 51);
            this.btn_AutoSegment.Name = "btn_AutoSegment";
            this.btn_AutoSegment.Size = new System.Drawing.Size(149, 23);
            this.btn_AutoSegment.TabIndex = 2;
            this.btn_AutoSegment.Text = "自动分段";
            this.btn_AutoSegment.UseVisualStyleBackColor = true;
            this.btn_AutoSegment.Click += new System.EventHandler(this.btn_AutoSegment_Click);
            // 
            // cbb_Dels
            // 
            this.cbb_Dels.FormattingEnabled = true;
            this.cbb_Dels.Location = new System.Drawing.Point(112, 24);
            this.cbb_Dels.Name = "cbb_Dels";
            this.cbb_Dels.Size = new System.Drawing.Size(121, 21);
            this.cbb_Dels.TabIndex = 1;
            this.cbb_Dels.SelectedIndexChanged += new System.EventHandler(this.cbb_Dels_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(239, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "符号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择分段符号组：";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer3.Size = new System.Drawing.Size(460, 437);
            this.splitContainer3.SplitterDistance = 384;
            this.splitContainer3.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.splitContainer5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(460, 384);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "手动分段";
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(3, 16);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.groupBox5);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.btn_MergeText);
            this.splitContainer5.Panel2.Controls.Add(this.btn_ManalSegment);
            this.splitContainer5.Size = new System.Drawing.Size(454, 365);
            this.splitContainer5.SplitterDistance = 351;
            this.splitContainer5.TabIndex = 3;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rtb_SelText);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(351, 365);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "选中文字";
            // 
            // rtb_SelText
            // 
            this.rtb_SelText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_SelText.Location = new System.Drawing.Point(3, 16);
            this.rtb_SelText.Name = "rtb_SelText";
            this.rtb_SelText.Size = new System.Drawing.Size(345, 346);
            this.rtb_SelText.TabIndex = 0;
            this.rtb_SelText.Text = "";
            // 
            // btn_ManalSegment
            // 
            this.btn_ManalSegment.Location = new System.Drawing.Point(3, 16);
            this.btn_ManalSegment.Name = "btn_ManalSegment";
            this.btn_ManalSegment.Size = new System.Drawing.Size(85, 49);
            this.btn_ManalSegment.TabIndex = 1;
            this.btn_ManalSegment.Text = "手动分段";
            this.btn_ManalSegment.UseVisualStyleBackColor = true;
            this.btn_ManalSegment.Click += new System.EventHandler(this.btn_ManalSegment_Click);
            // 
            // btn_AutoNumber
            // 
            this.btn_AutoNumber.Location = new System.Drawing.Point(9, 20);
            this.btn_AutoNumber.Name = "btn_AutoNumber";
            this.btn_AutoNumber.Size = new System.Drawing.Size(130, 23);
            this.btn_AutoNumber.TabIndex = 2;
            this.btn_AutoNumber.Text = "自动编号";
            this.toolTip1.SetToolTip(this.btn_AutoNumber, "在一切都没有问题后才可以点击自动编号");
            this.btn_AutoNumber.UseVisualStyleBackColor = true;
            this.btn_AutoNumber.Click += new System.EventHandler(this.btn_AutoNumber_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_SaveAsHtml);
            this.groupBox1.Controls.Add(this.btn_AutoNumber);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(460, 49);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "保存";
            // 
            // btn_SaveAsHtml
            // 
            this.btn_SaveAsHtml.Location = new System.Drawing.Point(162, 20);
            this.btn_SaveAsHtml.Name = "btn_SaveAsHtml";
            this.btn_SaveAsHtml.Size = new System.Drawing.Size(120, 23);
            this.btn_SaveAsHtml.TabIndex = 0;
            this.btn_SaveAsHtml.Text = "保存成 HTML";
            this.btn_SaveAsHtml.UseVisualStyleBackColor = true;
            this.btn_SaveAsHtml.Click += new System.EventHandler(this.btn_SaveAsHtml_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(187, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "自定义正则表达式作为分段符号：";
            // 
            // tb_RegExpText
            // 
            this.tb_RegExpText.Location = new System.Drawing.Point(199, 85);
            this.tb_RegExpText.Name = "tb_RegExpText";
            this.tb_RegExpText.Size = new System.Drawing.Size(226, 20);
            this.tb_RegExpText.TabIndex = 5;
            this.toolTip1.SetToolTip(this.tb_RegExpText, "例如：1、保险 2、自行车和头盔 3、游船 4、小团游\r\n... \r\n分段正则：\\d、\r\n分段结果是：\r\n保险\r\n自行车和头盔\r\n游船\r\n小团游\r\n\r\n如果希望保" +
        "留分段符号信息，那么\r\n分段正则：(\\d、)\r\n分段结果是：\r\n 1、\r\n保险 \r\n2、\r\n自行车和头盔\r\n3、\r\n游船\r\n 4、\r\n小团游\r\n");
            // 
            // btn_RegxSegment
            // 
            this.btn_RegxSegment.Location = new System.Drawing.Point(276, 110);
            this.btn_RegxSegment.Name = "btn_RegxSegment";
            this.btn_RegxSegment.Size = new System.Drawing.Size(149, 23);
            this.btn_RegxSegment.TabIndex = 2;
            this.btn_RegxSegment.Text = "正则分段";
            this.btn_RegxSegment.UseVisualStyleBackColor = true;
            this.btn_RegxSegment.Click += new System.EventHandler(this.btn_RegxSegment_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 50;
            this.toolTip1.AutoPopDelay = 50000;
            this.toolTip1.InitialDelay = 50;
            this.toolTip1.ReshowDelay = 10;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.UseAnimation = false;
            this.toolTip1.UseFading = false;
            // 
            // rb_AtStart
            // 
            this.rb_AtStart.AutoSize = true;
            this.rb_AtStart.Checked = true;
            this.rb_AtStart.Location = new System.Drawing.Point(9, 113);
            this.rb_AtStart.Name = "rb_AtStart";
            this.rb_AtStart.Size = new System.Drawing.Size(97, 17);
            this.rb_AtStart.TabIndex = 6;
            this.rb_AtStart.TabStop = true;
            this.rb_AtStart.Text = "该符号在句首";
            this.rb_AtStart.UseVisualStyleBackColor = true;
            this.rb_AtStart.Visible = false;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(123, 113);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(97, 17);
            this.radioButton1.TabIndex = 6;
            this.radioButton1.Text = "该符号在句尾";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Visible = false;
            // 
            // btn_MergeText
            // 
            this.btn_MergeText.Location = new System.Drawing.Point(3, 95);
            this.btn_MergeText.Name = "btn_MergeText";
            this.btn_MergeText.Size = new System.Drawing.Size(85, 49);
            this.btn_MergeText.TabIndex = 1;
            this.btn_MergeText.Text = "合并分段";
            this.btn_MergeText.UseVisualStyleBackColor = true;
            this.btn_MergeText.Click += new System.EventHandler(this.btn_MergeText_Click);
            // 
            // frm_contentInfos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 577);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frm_contentInfos";
            this.Text = "文字内容信息";
            this.Shown += new System.EventHandler(this.frm_contentInfos_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tb_Signs;
        private System.Windows.Forms.Button btn_AutoSegment;
        private System.Windows.Forms.ComboBox cbb_Dels;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_ManalSegment;
        private System.Windows.Forms.RichTextBox rtb_SelText;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListView lv_data;
        private System.Windows.Forms.ColumnHeader SheetName;
        private System.Windows.Forms.ColumnHeader CellPosition;
        private System.Windows.Forms.ColumnHeader SegmentIndex;
        private System.Windows.Forms.ColumnHeader ContentText;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Label lbl_Total;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColumnHeader EndWhiteSpaceSign;
        private System.Windows.Forms.Button btn_AutoNumber;
        private System.Windows.Forms.Button btn_SaveAsHtml;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox tb_RegExpText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_RegxSegment;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton rb_AtStart;
        private System.Windows.Forms.Button btn_MergeText;
    }
}