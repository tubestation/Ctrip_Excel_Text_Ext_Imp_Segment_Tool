using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace Ctrip_Excel_Text_Ext_Imp_Segment_Tool
{
    public delegate void DelShowStatus(string msg);

    public partial class frm_main : CsoftAuth_Base_Frm
    {

        public List<FileInfo> HtmlFileList { get; set; }

        public frm_main()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        #region TextBox拖拽功能
        /// <summary>
        /// 1.修改窗体和 TextBox 的属性 AllowDrop=true
        /// 2.注册 TextBox 的 3个事件， DragEnter,DragDrop,DragLeave
        /// </summary>
        public void tb_DragEnter(object sender, DragEventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.BackColor = Color.Violet;
            e.Effect = DragDropEffects.Copy;
        }
        public void tb_DragDrop(object sender, DragEventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.BackColor = Color.White;
            string fPath = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            tb.Text = fPath;
        }
        public void tb_DragLeave(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.BackColor = Color.White;
        }
        #endregion

        //显示Listbox tooltip （防闪烁）
        #region Listbox 鼠标移动时 显示tooltip
        /// <summary>
        /// 1.添加一个ToolTip
        /// 2.ListBox 注册2个事件， MouseMove,MouseLeave
        /// </summary>
        private void lb_MouseMove(object sender, MouseEventArgs e)
        {
            ListBox lb = sender as ListBox;
            int idx = lb.IndexFromPoint(e.Location);
            if (idx >= 0 && idx < lb.Items.Count)
            {
                if (toolTip1.GetToolTip(lb) != (lb.Items[idx] as FileInfo).FullName)
                {
                    toolTip1.SetToolTip(lb, (lb.Items[idx] as FileInfo).FullName);
                }
            }
        }

        #region ListBox 拖拽功能
        /// <summary>
        /// 1.修改窗体 和 ListBox 的属性 AllowDrop=true
        /// 2.ListBox 3个事件 DragEnter,DragDrop,DrageLeave
        /// </summary>
        public void lb_DragEnter(object sender, DragEventArgs e)
        {
            ListBox lb = sender as ListBox;
            lb.BackColor = Color.Violet;
            e.Effect = DragDropEffects.Copy;
        }
        public void lb_DragDrop(object sender, DragEventArgs e)
        {
            ListBox lb = sender as ListBox;
            lb.BackColor = Color.White;

            var fileObjs = (Array)e.Data.GetData(DataFormats.FileDrop);

            lb.DisplayMember = "Name";
            List<FileInfo> fiList = new List<FileInfo>();

            foreach (var fileObj in fileObjs)
            {
                FileInfo fi = new FileInfo(fileObj.ToString());
                fiList.Add(fi);
            }

            this.HtmlFileList = fiList;
            lb.DataSource = this.HtmlFileList;
        }
        public void lb_DragLeave(object sender, EventArgs e)
        {
            ListBox lb = sender as ListBox;
            lb.BackColor = Color.White;
        } 
        #endregion

        private void lb_MouseLeave(object sender, EventArgs e)
        {
            ListBox lb = sender as ListBox;
            toolTip1.Hide(lb);
        } 
        #endregion

        public void ShowStatus(string msg)
        {
            this.tssi_Status.Text = msg;
        }

        private void btn_OpenFile_Click(object sender, EventArgs e)
        {
            string filePath = this.tb_FilePath.Text;
            if (!File.Exists(filePath))
            {
                MessageBox.Show("文件不存在。");
                return;
            }
            if (StaticValues.ExcelOperator == null)
            {
                ShowStatus("正在打开...");
                StaticValues.ExcelOperator = new ExcelOperator(filePath);
            }
            StaticValues.ExcelOperator.OpenFile(filePath);
            //重新载入sheetname名称
            LoadCbbSheetNames(StaticValues.ExcelOperator.GetAllSheetNames());
            ShowStatus("文件已打开");
            //StaticValues.ExcelOperator.Wbk.SheetActivate += Wbk_SheetActivate;

        }

        private void Wbk_SheetActivate(object Sh)
        {
            this.cbb_ShtNames.Text = (Sh as Microsoft.Office.Interop.Excel.Worksheet).Name;
        }



        /// <summary>
        /// 重新载入sheetname名称
        /// </summary>
        /// <param name="shtNameList"></param>
        public void LoadCbbSheetNames(List<string> shtNameList)
        {
            this.cbb_ShtNames.Items.Clear();
            this.cbb_ShtNames.Items.Add("<<全部>>");
            foreach (string shtName in shtNameList)
            {
                this.cbb_ShtNames.Items.Add(shtName);
            }
        }
        /// <summary>
        /// 切换sheetname时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbb_ShtNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbb_ShtNames.Text == "<<全部>>")
                return;
            StaticValues.ExcelOperator.ActivateSheet(cbb_ShtNames.Text);
        }
        /// <summary>
        /// 窗体关闭 进行时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //关闭excel
            StaticValues.ExcelOperator.QuitApp();
        }
        /// <summary>
        /// 获取当前选择的cell的背景色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_GetCurBackColor_Click(object sender, EventArgs e)
        {
            tb_BackColorValue.Text = StaticValues.ExcelOperator.GetActiveCellBackColorValue();
        }
        /// <summary>
        /// 给选中的单元格着色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_FillColor_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_BackColorValue.Text.Trim()))
                return;
            double colVal = Convert.ToDouble(tb_BackColorValue.Text.Trim());
            StaticValues.ExcelOperator.FillColor2ActiveRange(colVal);
        }
        /// <summary>
        /// 获取当前单元格的 字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_GetCurFontColor_Click(object sender, EventArgs e)
        {
            tb_FontColorValue.Text = StaticValues.ExcelOperator.GetActiveCellFontColorValue();
        }
        /// <summary>
        /// 读取文字内容 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Read_Click(object sender, EventArgs e)
        {
            ShowStatus("正在读取数据...");
            //获取读取设置
            ReadingOptions ros = GetReadingOptions();
            //获取读取文字信息列表
            List<CellSegment> csList = StaticValues.ExcelOperator.ExtractContentInfo(ros);
            //打开新窗体
            frm_contentInfos frmci = new frm_contentInfos(csList, ros);
            frmci.Show();
            ShowStatus("数据已读取");
        }
        /// <summary>
        /// 获取读取设置
        /// </summary>
        /// <returns></returns>
        public ReadingOptions GetReadingOptions()
        {
            //获取header row list
            string headerRow = tb_HeaderRows.Text;
            List<int> rowList = null;
            if (string.IsNullOrEmpty(headerRow.Trim()))
            {
                rowList = null;
            }
            rowList = new List<int>();
            List<string> rowStrList = headerRow.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (string str in rowStrList)
                rowList.Add(Convert.ToInt32(str));

            //获取 背景色值
            double backColorVal = 0;
            string backColorStr = tb_BackColorValue.Text;
            if (!string.IsNullOrEmpty(backColorStr.Trim()))
                backColorVal = Convert.ToDouble(backColorStr);

            //获取 字体颜色
            double fontColorVal = 0;
            string fontColorStr = tb_FontColorValue.Text;
            if (!string.IsNullOrEmpty(fontColorStr.Trim()))
                fontColorVal = Convert.ToDouble(fontColorStr);


            //创建实例
            ReadingOptions ros = new ReadingOptions(tb_FilePath.Text, this.cbb_ShtNames.Text,
                this.cb_NotReadTableHeader.Checked, rowList,
                this.cb_NotReadHiddenCells.Checked,
                this.cb_NotReadBackColor.Checked, backColorVal,
                this.cb_NotReadFontColor.Checked, fontColorVal,
                this.cb_MergeSameText.Checked);
            return ros;
        }

        /// <summary>
        /// 导入时打开 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_OpenExcel_Click(object sender, EventArgs e)
        {
            string filePath = this.tb_MainExcel.Text;
            if (!File.Exists(filePath))
            {
                MessageBox.Show("文件不存在。");
                return;
            }
            if (StaticValues.ExcelOperator == null)
            {
                ShowStatus("正在打开...");
                StaticValues.ExcelOperator = new ExcelOperator(filePath);
            }
            StaticValues.ExcelOperator.OpenFile(filePath);
            //重新载入sheetname名称
            LoadCbbSheetNames(StaticValues.ExcelOperator.GetAllSheetNames());
            ShowStatus("文件已打开");
        }

        /// <summary>
        /// 导入 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ImportText_Click(object sender, EventArgs e)
        {
            if (StaticValues.ExcelOperator == null || StaticValues.ExcelOperator.App.ActiveWorkbook == null)
            {
                MessageBox.Show("请先打开Excel...");
                return;
            }
            string htmlPath = tb_DataFile.Text;
            if (!File.Exists(htmlPath))
            {
                MessageBox.Show("请拖入数据文件...");
                return;
            }
            HtmlDataDrawer hd = new HtmlDataDrawer(htmlPath);
            List<CellSegment> csList = hd.DataMergeByCellPosition(hd.GetAllData(),cb_AddSpaceTail.Checked?" ":"");     //获取所有数据，并根据单元格归档
            //导入数据
            StaticValues.ExcelOperator.ImportData(csList);
            MessageBox.Show("完成");
        }

        private void frm_main_FormClosed(object sender, FormClosedEventArgs e)
        {
            //关闭excel
            if(StaticValues.ExcelOperator!=null)
                StaticValues.ExcelOperator.QuitApp();
        }
        /// <summary>
        /// 合并成一个 Excel 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Merge_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(MergeIntoExcel);
            th.IsBackground = true;
            th.Start();
        }
        /// <summary>
        /// 合并实际方法
        /// </summary>
        public void MergeIntoExcel()
        {
            string fdPath = tb_ExcelsFolderPath.Text.Trim();
            if (!Directory.Exists(fdPath))
            {
                MessageBox.Show("文件夹不存在...");
                return;
            }
            string[] files = Directory.GetFiles(fdPath, "*", SearchOption.AllDirectories).Where(x => Regex.IsMatch(x.ToLower(), @".*?\.xls|.*?\.xlsx")).ToArray();
            if (files.Length <= 0)
            {
                MessageBox.Show("文件夹内没有 excel 文件");
                return;
            }

            if (StaticValues.ExcelOperator == null)
            {
                ExcelOperator eo = new ExcelOperator(this.ShowStatus);
                StaticValues.ExcelOperator = eo;
            }
            if(StaticValues.ExcelOperator.App==null)
                StaticValues.ExcelOperator.OpenApplication();

            string mainFile = Path.Combine(fdPath, "_Merged.xlsx");
            string diffFile = Path.Combine(fdPath, "_Report_DiffernetFont.xlsx");
            StaticValues.ExcelOperator.MergeIntoMainExcel(mainFile, files, string.IsNullOrEmpty(tb_PosInfo.Text.Trim()) ? "" : tb_PosInfo.Text.Trim(),cb_GenerateDifferFont.Checked?diffFile:"");
        }

        private void btn_Restore_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(RestoreData);
            th.IsBackground = true;
            th.Start();
        }

        public void RestoreData()
        {
            string resFD = tb_RestoreFolder.Text.Trim();
            string resDataFile = tb_RestoreDataFile.Text.Trim();
            if (!Directory.Exists(resFD))
            {
                MessageBox.Show("文件夹不存在。");
                return;
            }
            if (!File.Exists(resDataFile))
            {
                MessageBox.Show("数据文件不存在。");
                return;
            }
            if (StaticValues.ExcelOperator == null)
            {
                ExcelOperator eo = new ExcelOperator(this.ShowStatus);
                StaticValues.ExcelOperator = eo;
            }

            if (StaticValues.ExcelOperator.App == null)
            {
                ShowStatus("正在开启Excel...");
                StaticValues.ExcelOperator.OpenApplication();
            }

            StaticValues.ExcelOperator.RestoreBackData(resFD, resDataFile);
            ShowStatus("导回数据完成");
        }

        private void btn_readHtml_Click(object sender, EventArgs e)
        {
            string filePath = this.tb_htmlPath2Split.Text;
            if (!File.Exists(filePath))
            {
                MessageBox.Show("文件不存在");
                return;
            }
            HtmlDataDrawer hd = new HtmlDataDrawer(filePath);
            lbl_TotalDivs.Text = hd.DivCount.ToString();
        }

        private void btn_SplitHtml_Click(object sender, EventArgs e)
        {
            string filePath = this.tb_htmlPath2Split.Text;
            if (!File.Exists(filePath))
            {
                MessageBox.Show("文件不存在");
                return;
            }
            HtmlDataDrawer hd = new HtmlDataDrawer(filePath);
            int fileCount = Convert.ToInt32(tb_fileCount.Text);
            try
            {
                hd.SplitBySpecFileCount(fileCount);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("完成");
        }
        /// <summary>
        /// 重载 HtmlfilelIst
        /// </summary>
        public void ReloadHtmlFileList()
        {
            lb_subHtmls.DataSource = null;
            if (this.HtmlFileList == null)
                return;
            lb_subHtmls.DataSource = this.HtmlFileList;
            lb_subHtmls.DisplayMember = "Name";
        }
        /// <summary>
        /// 上 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_up_Click(object sender, EventArgs e)
        {
            if (lb_subHtmls.SelectedIndex <= 0)
                return;
            //和前一个项目交换
            int selIdx = lb_subHtmls.SelectedIndex;
            FileInfo selFile = HtmlFileList[selIdx];
            HtmlFileList[selIdx] = HtmlFileList[selIdx - 1];
            HtmlFileList[selIdx - 1] = selFile;
            ReloadHtmlFileList();
            lb_subHtmls.SelectedIndex = selIdx - 1;
        }
        /// <summary>
        /// 下 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_down_Click(object sender, EventArgs e)
        {
            if (lb_subHtmls.SelectedIndex < 0 || lb_subHtmls.SelectedIndex>=lb_subHtmls.Items.Count-1)
                return;
            //和前一个项目交换
            int selIdx = lb_subHtmls.SelectedIndex;
            FileInfo selFile = HtmlFileList[selIdx];
            HtmlFileList[selIdx] = HtmlFileList[selIdx + 1];
            HtmlFileList[selIdx + 1] = selFile;
            ReloadHtmlFileList();
            lb_subHtmls.SelectedIndex = selIdx + 1;
        }
        /// <summary>
        /// 自动排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AutoSort_Click(object sender, EventArgs e)
        {
            if (this.HtmlFileList == null)
                return;
            HtmlFileList.Sort(new FileinfoComparer(FileInfoCompareBy.Name));
            ReloadHtmlFileList();
        }
        /// <summary>
        /// 合并 按钮   
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MergeHtmls_Click(object sender, EventArgs e)
        {
            if (this.HtmlFileList == null)
                return;

            string hstr = Properties.Resources.TemplateHtml;
            HtmlDataDrawer hdFirst = new HtmlDataDrawer(HtmlFileList[0].FullName);
            string title = hdFirst.HtmlDocTitle;
            title = title.Substring(0, title.LastIndexOf("_"));
            string baseName = Path.GetFileNameWithoutExtension(HtmlFileList[0].FullName);
            baseName = baseName.Substring(0, baseName.LastIndexOf("_"));
            string mergedFilePath=Path.Combine(Path.GetDirectoryName(HtmlFileList[0].FullName),baseName+"_Merged.html");

            string bodyStr = "";

            foreach (FileInfo fi in this.HtmlFileList)
            {
                HtmlDataDrawer hd = new HtmlDataDrawer(fi.FullName);
                bodyStr+=hd.GetAllHtmlData();
            }
            hstr = hstr.Replace("%title%", title);
            hstr = hstr.Replace("%body%", bodyStr);
            File.WriteAllText(mergedFilePath, hstr, Encoding.UTF8);

            MessageBox.Show("完成");
        }
    }
}
