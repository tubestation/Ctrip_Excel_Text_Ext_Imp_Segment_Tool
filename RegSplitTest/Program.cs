using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RegSplitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = " 1、塞纳河游船 2、英文语音导览 3、饮料或小吃甜点 根据不同套餐  ";
            string[] splits=Regex.Split(text, @"(\d)");
            foreach(string split in splits)
                Console.WriteLine(split);
            Console.Read();
        }
    }
}
