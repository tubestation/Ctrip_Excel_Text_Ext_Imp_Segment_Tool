using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Ctrip_Excel_Text_Ext_Imp_Segment_Tool
{
    public partial class frm_contentInfos : Form
    {
        public List<CellSegment> DataList = new List<CellSegment>();
        public ReadingOptions ReadingOption { get; set; }

        public frm_contentInfos(List<CellSegment> dataList,ReadingOptions ros) : this()
        {
            this.DataList = dataList;
            this.ReadingOption = ros;
        }

        public frm_contentInfos()
        {
            InitializeComponent();
        }

        private void frm_contentInfos_Shown(object sender, EventArgs e)
        {
            LoadDataList();
            LoadDelimeters();
        }
        /// <summary>
        /// 装载数据 到 lv_data
        /// </summary>
        public void LoadDataList()
        {
            if (this.DataList == null || this.DataList.Count <= 0)
                return;
            lv_data.Items.Clear();
            for (int i = 0; i < this.DataList.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = DataList[i].ShtName;
                lvi.SubItems.Add(DataList[i].CellPosition);
                lvi.SubItems.Add(DataList[i].SegmentIndex.ToString());
                lvi.SubItems.Add(DataList[i].EndWhiteSpaceSign);
                lvi.SubItems.Add(DataList[i].Text);
                lv_data.Items.Add(lvi);
            }
            ShowTotalCount();
        }
        /// <summary>
        /// 读取分隔符
        /// </summary>
        public void LoadDelimeters()
        {
            string delPath = Path.Combine(Environment.CurrentDirectory, "Delimeters.json");
            if (!File.Exists(delPath))
                return;
            string jsonStr = File.ReadAllText(delPath);
            List<DelimeterDef> delList = JsonConvert.DeserializeObject<List<DelimeterDef>>(jsonStr);
            this.cbb_Dels.DisplayMember = "GroupName";
            this.cbb_Dels.DataSource = delList;
        }
        /// <summary>
        /// 显示列表中总个数
        /// </summary>
        public void ShowTotalCount()
        {
            lbl_Total.Text = lv_data.Items.Count.ToString();
        }
        /// <summary>
        /// 选择项变更事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lv_data_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_data.SelectedItems == null || lv_data.SelectedItems.Count <= 0)
                return;
            //foreach (ListViewItem lvi in lv_data.Items)
            //{
            //    if (lvi.BackColor != Color.White)
            //        lvi.BackColor = Color.White;
            //}
            //lv_data.BackColor = Color.White;

            lv_data.SelectedItems[0].BackColor = Color.LightBlue;
            rtb_SelText.Text = lv_data.SelectedItems[0].SubItems[4].Text;
        }
        /// <summary>
        /// combobox 选项更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbb_Dels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_Dels.SelectedItem != null)
            {
                DelimeterDef def = cbb_Dels.SelectedItem as DelimeterDef;
                tb_Signs.Text = def.Delimeters.ToString();
            }
        }
        /// <summary>
        /// 自动分段 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AutoSegment_Click(object sender, EventArgs e)
        {
            string detText = tb_Signs.Text;

            List<CellSegment> tmpList = new List<CellSegment>();

            foreach (CellSegment cs in this.DataList)
            {
                tmpList.AddRange(CellSegment.SegmentSplitBySigns(cs, detText));
            }
            this.DataList.Clear();
            this.DataList = CellSegment.ListClone(tmpList);
            tmpList.Clear();

            LoadDataList();
        }

        /// <summary>
        /// 手动分段 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ManalSegment_Click(object sender, EventArgs e)
        {
            if (rtb_SelText.Lines.Count() <= 1)
            {
                return;
            }

            List<CellSegment> repList = new List<CellSegment>();
            int idx = lv_data.SelectedIndices[0];
            string shtName = lv_data.SelectedItems[0].SubItems[0].Text;
            string cellPosition = lv_data.SelectedItems[0].SubItems[1].Text;
            string endSign = lv_data.SelectedItems[0].SubItems[3].Text;
            foreach (string line in rtb_SelText.Lines)
            {
                CellSegment cs = new CellSegment(line, shtName, cellPosition);
                repList.Add(cs);
            }
            repList.Last().EndWhiteSpaceSign = endSign; //最后一个的结束符
            this.DataList.RemoveAt(idx);
            this.DataList.InsertRange(idx, repList);

            LoadDataList();
            lv_data.Items[idx+rtb_SelText.Lines.Count()-1].Selected = true;
            lv_data.EnsureVisible(idx + rtb_SelText.Lines.Count()+1);
            lv_data.Focus();
        }
        /// <summary>
        /// 自动编号 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AutoNumber_Click(object sender, EventArgs e)
        {
            Dictionary<string, List<CellSegment>> csDic = new Dictionary<string, List<CellSegment>>();
            foreach (CellSegment cs in this.DataList)
            {
                if (csDic.ContainsKey(cs.CellPosition))
                {
                    csDic[cs.CellPosition].Add(cs);
                }
                else
                {
                    csDic.Add(cs.CellPosition, new List<CellSegment>() { cs });
                }
            }
            //编号 并存到tmpList里
            List<CellSegment> tmpList = new List<CellSegment>();

            foreach (KeyValuePair<string, List<CellSegment>> kvp in csDic)
            {
                for (int i=0;i<kvp.Value.Count;i++)
                {
                    kvp.Value[i].SegmentIndex = i;
                    tmpList.Add(kvp.Value[i]);
                }
            }
            this.DataList.Clear();
            this.DataList = tmpList;

            LoadDataList();
        }
        /// <summary>
        /// 保存成Html 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SaveAsHtml_Click(object sender, EventArgs e)
        {
            frm_HtmlSaveOptions frmHSO = new frm_HtmlSaveOptions();
            string ptn = "";
            if (frmHSO.ShowDialog() == DialogResult.OK)
            {
                ptn = frmHSO.tb_Pattern2CsoftPH.Text;
            }
            
            SaveAsHtml(ptn);
        }
        /// <summary>
        /// 保存成html，ptn是正则表达式，就是pattern，将pattern转换成<csoftph text='ptn' />
        /// </summary>
        /// <param name="ptn"></param>
        public void SaveAsHtml(string ptn="")
        {
            string title = Path.GetFileNameWithoutExtension(this.ReadingOption.FileName);
            string htmlPath = Path.Combine(Path.GetDirectoryName(this.ReadingOption.FileName), title + ".html");

            StringBuilder sb = new StringBuilder();
            foreach (CellSegment cs in this.DataList)
            {
                //sb.Append(string.Format("\t<div ShtName='{0}' CellPosition='{1}' EndWhiteSpaceSign='{2}' SegmentIndex='{3}'>", cs.ShtName, cs.CellPosition, string.IsNullOrEmpty(cs.EndWhiteSpaceSign) ? " " : cs.EndWhiteSpaceSign, cs.SegmentIndex.ToString()));
                sb.Append(string.Format("\t<div ShtName='{0}' CellPosition='{1}' EndWhiteSpaceSign='{2}' SegmentIndex='{3}'>", cs.ShtName, cs.CellPosition, cs.EndWhiteSpaceSign, cs.SegmentIndex.ToString()));
                string text = cs.Text;
                if (ptn.Length > 0)
                {
                    text = Regex.Replace(text,ptn, "<csoftph text='$1' />");
                }
                sb.Append(text);
                sb.Append("</div>\r\n");
            }
            string htmlText = Properties.Resources.TemplateHtml;
            htmlText = htmlText.Replace("%title%", title);
            htmlText = htmlText.Replace("%body%", sb.ToString());
            //保存
            File.WriteAllText(htmlPath, htmlText);

            MessageBox.Show("完成");
        }
        /// <summary>
        /// 选择项变更 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lv_data_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //MessageBox.Show(e.ItemIndex.ToString()+","+e.IsSelected.ToString());
            if (!e.IsSelected)
                e.Item.BackColor = Color.White;
            else
            {
                e.Item.BackColor = Color.LightBlue;
                rtb_SelText.Text = e.Item.SubItems[4].Text;
            }
        }

        private void btn_RegxSegment_Click(object sender, EventArgs e)
        {
            string ptn = tb_RegExpText.Text;
            if (string.IsNullOrEmpty(ptn.Trim()))
                return;

            List<CellSegment> tmpList = new List<CellSegment>();

            foreach (CellSegment cs in this.DataList)
            {
                List<CellSegment> splittedList = CellSegment.SegmentSplitByRegExp(cs, ptn);
                tmpList.AddRange(splittedList);
            }
            this.DataList.Clear();
            this.DataList = CellSegment.ListClone(tmpList);
            tmpList.Clear();

            LoadDataList();
        }
        /// <summary>
        /// 合并分段 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MergeText_Click(object sender, EventArgs e)
        {
            if (lv_data.SelectedItems.Count <= 1)
                return;
            if (!InSameCell())
            {
                MessageBox.Show("选择的项目不在同一个单元格内，无法合并。");
                return;
            }
            if (!IsContinuosIndices())
            {
                MessageBox.Show("不是连续索引，无法合并。");
                return;
            }

            
            int idx = lv_data.SelectedIndices[0];
            string shtName = lv_data.SelectedItems[0].SubItems[0].Text;
            string cellPosition = lv_data.SelectedItems[0].SubItems[1].Text;

            string newTxt = "";
            foreach (ListViewItem lvi in lv_data.SelectedItems)
            {
                if(lvi.SubItems[3].Text=="\\n")
                    newTxt += lvi.SubItems[4].Text+"\n";
                else if(lvi.SubItems[3].Text=="\\t")
                    newTxt += lvi.SubItems[4].Text + "\t";
                else
                    newTxt += lvi.SubItems[4].Text + lvi.SubItems[3].Text;
            }
            CellSegment cs = new CellSegment(newTxt, lv_data.SelectedItems[0].SubItems[0].Text, lv_data.SelectedItems[0].SubItems[1].Text);
            //删掉选中项目
            this.DataList.RemoveRange(idx, lv_data.SelectedItems.Count);
            //添加
            this.DataList.Insert(idx, cs);

            LoadDataList();
            lv_data.Items[idx].Selected = true;
            lv_data.EnsureVisible(idx + 1);
            lv_data.Focus();
        }
        /// <summary>
        /// 判断 lv_data 所选的项目是否连续
        /// </summary>
        /// <returns></returns>
        public bool IsContinuosIndices()
        {
            if (lv_data.SelectedItems.Count <= 1)
                return true;

            bool foundUncontinuous = false;
            List<int> indces = new List<int>();
            for (int i = 0; i < lv_data.SelectedIndices.Count; i++)
            {
                indces.Add(lv_data.SelectedIndices[i]);
            }
            indces.Sort();
            for (int i = 1; i < indces.Count; i++)
            {
                if (indces[i] - indces[i - 1] != 1)
                {
                    foundUncontinuous = true;
                    break;
                }
            }
            if (foundUncontinuous)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断 lv_data中所选中的项目是否在同一个cell中
        /// </summary>
        /// <returns></returns>
        public bool InSameCell()
        {
            if (lv_data.SelectedItems.Count <= 1)
                return true;

            bool foundDiff = false;
            string shtName = lv_data.SelectedItems[0].SubItems[0].Text;
            string cellPos = lv_data.SelectedItems[0].SubItems[1].Text;

            foreach (ListViewItem lvi in lv_data.SelectedItems)
            {
                if (lvi.SubItems[0].Text != shtName || lvi.SubItems[1].Text != cellPos)
                {
                    foundDiff = true;
                    break;
                }
            }
            if (foundDiff)
                return false;
            else
                return true;
        }
    }
}
