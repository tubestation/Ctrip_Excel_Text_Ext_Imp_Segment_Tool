using System;
using System.Collections.Generic;
using System.IO;

namespace Ctrip_Excel_Text_Ext_Imp_Segment_Tool
{
    public class FileinfoComparer : IComparer<FileInfo>
    {
        public FileInfoCompareBy FileInfoComapreBy { get; set; }

        public FileinfoComparer(FileInfoCompareBy compareBy)
        {
            this.FileInfoComapreBy = compareBy;
        }
        public int Compare(FileInfo x, FileInfo y)
        {
            if (this.FileInfoComapreBy == FileInfoCompareBy.Name)
                return string.Compare(x.Name, y.Name);
            else if (this.FileInfoComapreBy == FileInfoCompareBy.FullName)
                return string.Compare(x.Name, y.Name);
            else
                return string.Compare(x.Name, y.Name);
        }
    }
}