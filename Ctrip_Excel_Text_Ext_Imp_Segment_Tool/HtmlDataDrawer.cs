using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;

namespace Ctrip_Excel_Text_Ext_Imp_Segment_Tool
{
    public class HtmlDataDrawer
    {
        public string FilePath { get; set; }

        public HtmlDocument hdoc { get; set; }

        public HtmlDataDrawer()
        { }

        public HtmlDataDrawer(string filePath) : this()
        {
            this.FilePath = filePath;
            this.hdoc = new HtmlAgilityPack.HtmlDocument();
            hdoc.Load(this.FilePath,Encoding.UTF8);
        }
        /// <summary>
        /// body node下的div 数量
        /// </summary>
        public int DivCount
        {
            get
            {
                if (this.hdoc == null)
                    throw new Exception("文件是空");
                return hdoc.DocumentNode.SelectNodes("//body/div").Count;
            }
        }
        /// <summary>
        /// 本文件的title
        /// </summary>
        public string HtmlDocTitle
        {
            get
            {
                if (this.hdoc == null)
                { return ""; }
                else
                    return hdoc.DocumentNode.SelectSingleNode("//title").InnerText;
            }
        }
        /// <summary>
        /// 获取html中所有数据
        /// </summary>
        /// <returns></returns>
        public List<CellSegment> GetAllData()
        {
            //先处理csoftph标签 还原标签内容
            string xpathPH = "//csoftph";
            HtmlNodeCollection phNL = hdoc.DocumentNode.SelectNodes(xpathPH);
            if (phNL != null)
            {
                if (phNL.Count > 0)
                {
                    foreach (HtmlNode ph in phNL)
                    {
                        HtmlTextNode tn = hdoc.CreateTextNode(ph.Attributes["text"].Value);
                        ph.ParentNode.ReplaceChild(tn, ph);
                    }
                }
            }

            string xpath = @"//body/div";
            HtmlNodeCollection nl = hdoc.DocumentNode.SelectNodes(xpath);
            List<CellSegment> csList = new List<CellSegment>();
            //提取数据
            foreach (HtmlNode node in nl)
            {
                CellSegment cs = new CellSegment(node.InnerHtml, node.GetAttributeValue("ShtName", ""), node.GetAttributeValue("CellPosition", ""));
                cs.EndWhiteSpaceSign = node.GetAttributeValue("EndWhiteSpaceSign", "");
                cs.SegmentIndex = Convert.ToInt32(node.GetAttributeValue("SegmentIndex", "0"));
                csList.Add(cs);
            }

            return csList;
        }
        /// <summary>
        /// 获取HTML中所有数据 HTML形式
        /// </summary>
        /// <returns></returns>
        public string GetAllHtmlData()
        {
            string xpath = @"//body/div";
            HtmlNodeCollection nl = hdoc.DocumentNode.SelectNodes(xpath);
            StringBuilder sb = new StringBuilder();
            foreach (HtmlNode node in nl)
                sb.AppendLine("\t"+node.OuterHtml);
            return sb.ToString();
        }

        /// <summary>
        /// 数据合并 根据CellPosition 最终是一个单元格一条数据
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="sentenceTail">在拼接成一个条数据时，句尾添加的字符串， 默认不添加，如果是英文可以添加空格</param>
        /// <returns></returns>
        public List<CellSegment> DataMergeByCellPosition(List<CellSegment> dataList,string addTail="")
        {
            Dictionary<string, List<CellSegment>> csDic = new Dictionary<string, List<CellSegment>>();
            foreach (CellSegment cs in dataList)
            {
                string keyText = cs.ShtName + "/" + cs.CellPosition;
                if (csDic.ContainsKey(keyText))
                {
                    csDic[keyText].Add(cs);
                }
                else
                {
                    csDic.Add(keyText, new List<CellSegment>() { cs });
                }
            }

            List<CellSegment> csList = new List<CellSegment>();//创建新的 list
            foreach (KeyValuePair<string, List<CellSegment>> kvp in csDic)
            {
                //处理每个字典 value 的 text 拼接
                List<CellSegment> subList = kvp.Value;
                subList.Sort(new CellSegmentComparer(CellSegmentComapreBy.SegmentIndex));   //按照SegmentIndex 排序

                string shtName = subList[0].ShtName;
                string cellPos = subList[0].CellPosition;
                string endSign = subList.Last().EndWhiteSpaceSign;
                string texts = "";

                foreach (CellSegment cs in subList)
                {
                    texts += cs.Text + StaticValues.WSText2Symbol(cs.EndWhiteSpaceSign);    //拼接字符串
                    if (!string.IsNullOrEmpty(cs.Text.Trim()))
                    {
                        texts += addTail;
                    }
                }

                CellSegment mergedCS = new CellSegment(texts, shtName, cellPos);
                mergedCS.EndWhiteSpaceSign = endSign;

                csList.Add(mergedCS);
            }
            return csList;
        }

        /// <summary>
        /// 根据指定文件个数拆分
        /// </summary>
        /// <param name="fileCount"></param>
        public void SplitBySpecFileCount(int fileCount)
        {
            if (fileCount > this.DivCount)
                throw new Exception("文件个数比div个数还多，你脑子抽了吧");
            if (fileCount == 0)
                throw new Exception("文件个数是0，你告诉我怎么分");
            //计算每个文件div个数
            int eachDivNum =Convert.ToInt32( Math.Ceiling((double)DivCount / (double)fileCount));

            HtmlNodeCollection divs = hdoc.DocumentNode.SelectNodes("//body/div");
            int eachDivCount = 0;
            int fileIdx = 1;
            List<HtmlNode> subList=new List<HtmlNode>();

            for (int i = 0; i < divs.Count; i++)
            {
                subList.Add(divs[i]);
                eachDivCount++;
                if (eachDivCount >= eachDivNum || i==divs.Count-1)
                {
                    //收尾
                    CreateNewDataHtml(fileIdx,subList);
                    eachDivCount = 0;
                    fileIdx++;
                    subList.Clear();
                }
            }
        }
        /// <summary>
        /// 根据properies中的文件 生成新的数据文件
        /// </summary>
        /// <param name="fileIdx"></param>
        /// <param name="subList"></param>
        public void CreateNewDataHtml(int fileIdx, List<HtmlNode> subList)
        {
            string hstr = Properties.Resources.TemplateHtml;
            string title = hdoc.DocumentNode.SelectSingleNode("//title").InnerText+"_"+fileIdx.ToString("D2");
            string subFilePath = Path.Combine(Path.GetDirectoryName(this.FilePath), Path.GetFileNameWithoutExtension(this.FilePath) + "_" + fileIdx.ToString("D2") + ".html");
            StringBuilder sb = new StringBuilder();
            foreach (HtmlNode subDiv in subList)
                sb.AppendLine("\t" + subDiv.OuterHtml);
            hstr = hstr.Replace("%title%", title);
            hstr = hstr.Replace("%body%", sb.ToString());
            File.WriteAllText(subFilePath, hstr, Encoding.UTF8);
        }
    }
}
