using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrip_Excel_Text_Ext_Imp_Segment_Tool
{
    public static class StaticValues
    {
        public static string ToolName = "Ctrip_Excel_Text_Ext_Imp_Segment_Tool";
        public static string Version = "v1.4";


        public static ExcelOperator ExcelOperator { get; set; }

        /// <summary>
        /// 符号转text  "\n" -> "\\n"
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public static string WSSymbol2Text(string symbol)
        {
            string text = symbol;
            if (symbol == "\n")
                text = "\\n";
            else if (symbol == "\t")
                text = "\\t";
            return text;
        }
        /// <summary>
        /// text转符号   "\\n" -> "\n"
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string WSText2Symbol(string text)
        {
            string sb = text;
            if (text == "\\n")
                sb = "\n";
            else if (text == "\\t")
                sb = "\t";

            return sb;
        }

    }
}
