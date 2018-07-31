using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrip_Excel_Text_Ext_Imp_Segment_Tool
{
    public class ReadingOptions
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// sheet 名
        /// </summary>
        public string SheetName { get; set; }

        /// <summary>
        /// 不读取表头
        /// </summary>
        public bool NotReadHeader { get; set; }

        /// <summary>
        /// 那些Row是表头
        /// </summary>
        public List<int> HeaderRowList { get; set; }

        /// <summary>
        /// 不读取隐藏单元格
        /// </summary>
        public bool NotReadHiddenCells { get; set; }

        /// <summary>
        /// 不读取背景色 的单元格
        /// </summary>
        public bool NotReadBackColor { get; set; }

        /// <summary>
        /// 背景色值
        /// </summary>
        public double BackColorValue { get; set; }

        /// <summary>
        /// 不读取字体颜色
        /// </summary>
        public bool NotReadFontColor { get; set; }

        /// <summary>
        /// 字体颜色值
        /// </summary>
        public double FontColorValue { get; set; }

        /// <summary>
        /// 是否合并同类项
        /// </summary>
        public bool MergeSameText { get; set; }

        public ReadingOptions()
        { }

        public ReadingOptions(string fileName, string sheetName,
            bool notReadHeader, List<int> headerRowList,
            bool notReadHiddenCells,
            bool notReadBackColor, double backColorValue,
            bool notReadFontColor, double fontColorValue,
            bool mergeSameText)
        {
            this.FileName = fileName;
            this.SheetName = sheetName;
            this.NotReadHeader = notReadHeader;
            this.HeaderRowList = headerRowList;
            this.NotReadHiddenCells = notReadHiddenCells;
            this.NotReadBackColor = notReadBackColor;
            this.BackColorValue = backColorValue;
            this.NotReadFontColor = notReadFontColor;
            this.FontColorValue = fontColorValue;
            this.MergeSameText = mergeSameText;
        }

    }
}
