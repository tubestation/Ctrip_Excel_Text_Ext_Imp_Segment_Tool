using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CharTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = tb_text.Text;
            Encoding iso2 = Encoding.GetEncoding("ISO-8859-2");

            byte[] buff = iso2.GetBytes(text);
            string codeStr = "";
            for (int i = 0; i < buff.Length; i++)
            {
                codeStr += Convert.ToInt32(buff[i]).ToString() + ",";
            }
            MessageBox.Show(codeStr);


        }
    }
}
