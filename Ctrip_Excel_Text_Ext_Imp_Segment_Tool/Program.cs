using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Ctrip_Excel_Text_Ext_Imp_Segment_Tool
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frm_main());
        }
    }
}
