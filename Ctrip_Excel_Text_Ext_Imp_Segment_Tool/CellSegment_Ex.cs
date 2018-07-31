using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrip_Excel_Text_Ext_Imp_Segment_Tool
{
    public class CellSegment_Ex:CellSegment
    {
        public string RelativeFilePath { get; set; }

        public CellSegment_Ex()
        { }

        public CellSegment_Ex(string text, string shtName, string cellPosition, string relativeFilePath) : base(text,shtName,cellPosition)
        {
            this.RelativeFilePath = relativeFilePath;
        }

    }
}
