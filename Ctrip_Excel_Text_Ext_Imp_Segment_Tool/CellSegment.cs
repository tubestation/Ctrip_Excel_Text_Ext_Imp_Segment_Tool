using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ctrip_Excel_Text_Ext_Imp_Segment_Tool
{
    public class CellSegment
    {
        /// <summary>
        /// 文字
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 该段所在sheet 的名称
        /// </summary>
        public string ShtName { get; set; }

        /// <summary>
        /// cell 的位置 如 "a1","zz109"
        /// </summary>
        public string CellPosition { get; set; }
        /// <summary>
        /// 如果结束符是\r\n,\t, 空格等就需要设置此属性
        /// </summary>
        public string EndWhiteSpaceSign { get; set; }
        /// <summary>
        /// 该segment的序号
        /// </summary>
        public int SegmentIndex { get; set; }



        public CellSegment()
        { }

        public CellSegment(string text, string shtName, string cellPosition) : this()
        {
            this.Text = text;
            this.ShtName = shtName;
            this.CellPosition = cellPosition;
        }

        /// <summary>
        /// 克隆本CellSegment
        /// </summary>
        /// <returns></returns>
        public CellSegment Clone()
        {
            CellSegment cs = new CellSegment();
            cs.Text = this.Text;
            cs.ShtName = this.ShtName;
            cs.CellPosition = this.CellPosition;
            cs.EndWhiteSpaceSign = this.EndWhiteSpaceSign;
            cs.SegmentIndex = this.SegmentIndex;

            return cs;
        }

        /// <summary>
        /// 自动分段
        /// </summary>
        /// <param name="segText"></param>
        public static List<CellSegment> SegmentSplitBySigns(CellSegment seg, string signText)
        {
            List<CellSegment> csList = new List<CellSegment>();
            csList.Add(seg.Clone());
            List<CellSegment> tmpCsList = new List<CellSegment>();

            string[] signs = signText.Split(',');
            foreach (string sign in signs)
            {
                for (int i = 0; i < csList.Count; i++)
                {
                    List<CellSegment> subList = SegmentSplitBySingleSign(csList[i], sign);
                    tmpCsList.AddRange(subList);
                }
                csList = ListClone(tmpCsList);
                tmpCsList.Clear();
            }
            //排序号
            return csList;
        }
        /// <summary>
        /// 使用单独sign分割
        /// </summary>
        /// <param name="segText"></param>
        /// <param name="orgSign"></param>
        /// <returns></returns>
        public static List<CellSegment> SegmentSplitBySingleSign(CellSegment mainSeg, string orgSign)
        {
            string lastSign = mainSeg.EndWhiteSpaceSign;
            List<CellSegment> csList = new List<CellSegment>();
            string sign = orgSign;

            sign = StaticValues.WSText2Symbol(orgSign);

            if (!mainSeg.Text.Contains(sign))
            {
                csList.Add(mainSeg.Clone());
                return csList;
            }

            string[] splitTexts = mainSeg.Text.Split(new string[] { sign }, StringSplitOptions.None);

            for (int i = 0; i < splitTexts.Length - 1; i++)
            {
                if (sign == "\n" || sign == "\t")
                {
                    CellSegment cs = new CellSegment(splitTexts[i], mainSeg.ShtName, mainSeg.CellPosition);
                    cs.EndWhiteSpaceSign = orgSign;
                    csList.Add(cs);
                }
                else
                {
                    CellSegment cs = new CellSegment(splitTexts[i] + orgSign, mainSeg.ShtName, mainSeg.CellPosition);
                    //cs.EndWhiteSpaceSign = " ";
                    csList.Add(cs);
                }
            }
            //添加最后那个
            CellSegment lastCS = new CellSegment(splitTexts[splitTexts.Length - 1], mainSeg.ShtName, mainSeg.CellPosition);
            lastCS.EndWhiteSpaceSign = lastSign;
            csList.Add(lastCS);
            return csList;
        }
        /// <summary>
        /// 克隆list CellSegment
        /// </summary>
        /// <param name="orgList"></param>
        /// <returns></returns>
        public static List<CellSegment> ListClone(List<CellSegment> orgList)
        {
            List<CellSegment> cloneList = new List<CellSegment>();
            for (int i = 0; i < orgList.Count; i++)
            {
                cloneList.Add(orgList[i].Clone());
            }
            return cloneList;
        }

        /// <summary>
        /// 使用正则表达式作为 分隔符 分割
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="ptn"></param>
        /// <returns></returns>
        public static List<CellSegment> SegmentSplitByRegExp(CellSegment mainSeg, string ptn)
        {
            List<CellSegment> csList = new List<CellSegment>();

            string endSign = mainSeg.EndWhiteSpaceSign;

            Regex regx = new Regex(ptn);
            if (!regx.IsMatch(mainSeg.Text))
            {
                csList.Add(mainSeg.Clone());
                return csList;
            }

            //string[] splitTexts = mainSeg.Text.Split(new string[] { sign }, StringSplitOptions.None);
            string[] splitTexts = regx.Split(mainSeg.Text);

            for (int i = 0; i < splitTexts.Length; i++)
            {
                CellSegment cs = new CellSegment(splitTexts[i], mainSeg.ShtName, mainSeg.CellPosition);
                //cs.EndWhiteSpaceSign = " ";
                csList.Add(cs);
            }
            csList.Last().EndWhiteSpaceSign = endSign; //还原最后一个元素的结束符
            return csList;
        }
    }

    /// <summary>
    /// CellSegment的比较器
    /// </summary>
    public class CellSegmentComparer : IComparer<CellSegment>
    {
        public CellSegmentComapreBy CompareBy { get; set; }

        public CellSegmentComparer()
        { }

        public CellSegmentComparer(CellSegmentComapreBy compareBy)
        {
            this.CompareBy=compareBy;
        }

        public int Compare(CellSegment x, CellSegment y)
        {
            if (this.CompareBy == CellSegmentComapreBy.SegmentIndex)
            {
                return x.SegmentIndex - y.SegmentIndex;
            }

            return x.SegmentIndex - y.SegmentIndex;
        }
    }
}
