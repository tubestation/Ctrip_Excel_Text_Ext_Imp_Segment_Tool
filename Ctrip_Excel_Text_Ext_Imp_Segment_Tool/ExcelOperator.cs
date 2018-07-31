using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;

namespace Ctrip_Excel_Text_Ext_Imp_Segment_Tool
{
    public class ExcelOperator
    {
        public string FilePath { get; set; }

        public Excel.Application App { get; set; }

        public Excel.Workbook Wbk { get; set; }

        public Excel.Workbook TempWbk { get; set; }

        public DelShowStatus ShowStatus { get; set; }

        //public delegate void ActiveSheetChanged();
        //private event ActiveSheetChanged OnActiveSheetChangedByExcel;

        public ExcelOperator()
        { }

        public ExcelOperator(DelShowStatus showStatus) : this()
        {
            this.ShowStatus = showStatus;
        }

        public ExcelOperator(string filePath) : this()
        {
            this.FilePath = filePath;
            OpenApplication();
        }



        /// <summary>
        /// 打开应用程序
        /// </summary>
        public void OpenApplication()
        {
            if (this.App == null)
            {
                this.App = new Excel.Application();
                this.App.Visible = true;
            }
        }
        /// <summary>
        /// 打开文件
        /// </summary>
        public void OpenFile(string filePath)
        {
            OpenApplication();
            this.FilePath = filePath;
            if (File.Exists(this.FilePath))
            {
                this.Wbk = App.Workbooks.Open(this.FilePath);
                this.Wbk.BeforeClose += Wbk_BeforeClose;
            }
        }

        private void Wbk_BeforeClose(ref bool Cancel)
        {
            this.Wbk = null;
            this.App.Quit();
            StaticValues.ExcelOperator = null;
        }


        //private void ExcelOperator_OnActiveSheetChangedByExcel()
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// 获取所有sheetnames
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllSheetNames()
        {
            if (Wbk == null)
                return null;
            List<string> nl = new List<string>();
            foreach (Excel.Worksheet sht in Wbk.Sheets)
                nl.Add(sht.Name);
            return nl;
        }
        /// <summary>
        /// 切换到该sheet
        /// </summary>
        /// <param name="shtName"></param>
        public void ActivateSheet(string shtName)
        {
            if (Wbk.Worksheets[shtName] == null)
                return;
            (Wbk.Worksheets[shtName] as Excel.Worksheet).Activate();
        }
        /// <summary>
        /// 获取当前选择的cell的背景色值
        /// </summary>
        /// <returns></returns>
        public string GetActiveCellBackColorValue()
        {
            string val = "";
            if (App == null || Wbk == null)
            {
                System.Windows.Forms.MessageBox.Show("请先打开Excel");
                return val;
            }
            val = ((App.ActiveCell as Excel.Range).Interior.Color).ToString();
            return val;
        }
        /// <summary>
        /// 获取当前单元格的字体颜色
        /// </summary>
        /// <returns></returns>
        public string GetActiveCellFontColorValue()
        {
            string val = "";
            if (App == null || Wbk == null)
            {
                System.Windows.Forms.MessageBox.Show("请先打开Excel");
                return val;
            }
            val = ((App.ActiveCell as Excel.Range).Font.Color).ToString();
            return val;
        }
        /// <summary>
        /// 给当前的range 上背景色
        /// </summary>
        /// <param name="colVal"></param>
        public void FillColor2ActiveRange(double colVal)
        {
            App.ActiveCell.Interior.Color = colVal;
        }





        /// <summary>
        /// 退出程序
        /// </summary>
        public void QuitApp()
        {
            if (App != null)
            {
                if (App.Workbooks.Count > 0)
                {
                    foreach (Excel.Workbook wk in App.Workbooks)
                    {
                        wk.Close(Excel.XlSaveAction.xlDoNotSaveChanges);
                    }
                }
                App.Quit();
            }
            StaticValues.ExcelOperator = null;
        }
        /// <summary>
        /// 无保存关闭 文档
        /// </summary>
        public void CloseWbk()
        {
            Wbk.Close(Excel.XlSaveAction.xlDoNotSaveChanges);
        }
        /// <summary>
        /// 抽取文字内容和信息
        /// </summary>
        /// <param name="ros"></param>
        /// <returns></returns>
        public List<CellSegment> ExtractContentInfo(ReadingOptions ros)
        {
            List<CellSegment> csList = new List<CellSegment>();

            if (ros.SheetName == "<<全部>>")
            {
                foreach (Excel.Worksheet sht in Wbk.Sheets)
                {
                    csList.AddRange(ExctactContentInfoBySheet(ros, sht.Name));
                }
            }
            else
            {
                csList = ExctactContentInfoBySheet(ros, ros.SheetName);
            }
            if (ros.MergeSameText)       //如果需要合并同类项
            {
                Dictionary<string, CellSegment> infoDic = new Dictionary<string, CellSegment>();
                foreach (CellSegment cs in csList)
                {
                    if (infoDic.ContainsKey(cs.Text))
                    {
                        infoDic[cs.Text].CellPosition += "," + cs.CellPosition;
                    }
                    else
                    {
                        infoDic.Add(cs.Text, cs.Clone());
                    }
                }
                List<CellSegment> MergedList = new List<CellSegment>();
                foreach (KeyValuePair<string, CellSegment> kvp in infoDic)
                {
                    MergedList.Add(kvp.Value);
                }
                return MergedList;
            }
            else
                return csList;
        }

        /// <summary>
        /// 单独抽取表单中的内容
        /// </summary>
        /// <param name="ros"></param>
        /// <param name="shtName"></param>
        /// <returns></returns>
        public List<CellSegment> ExctactContentInfoBySheet(ReadingOptions ros, string shtName)
        {
            List<CellSegment> csList = new List<CellSegment>();

            Excel.Worksheet sht = Wbk.Sheets[shtName];
            sht.Activate();
            int startRow = 1;
            int lastRow = GetLastRow(shtName);

            if (ros.NotReadHeader && ros.HeaderRowList != null && ros.HeaderRowList.Count > 0)
                startRow = ros.HeaderRowList.Last() + 1;

            for (int col = 1; col <= ColumnToIndex(GetLastColumn(shtName)); col++)      //遍历 column
            {
                for (int rw = startRow; rw <= lastRow; rw++)        //遍历row
                {
                    Excel.Range rng = sht.Range[IndexToColumn(col) + rw.ToString()];
                    string cellText = rng.Text;
                    if (string.IsNullOrEmpty(cellText.Trim()))      //不读取空字符
                        continue;
                    if (ros.NotReadHiddenCells && (rng.Height <= 0 || rng.Width <= 0))      //不读取隐藏
                        continue;
                    if (ros.NotReadBackColor && rng.Interior.Color == ros.BackColorValue)       //不读取有背景色
                        continue;
                    if (ros.NotReadFontColor && rng.Font.Color == ros.FontColorValue)           //不读取字体颜色
                        continue;

                    csList.Add(new CellSegment(cellText, shtName, IndexToColumn(col) + rw.ToString()));
                }
            }

            return csList;
        }

        /// <summary>
        /// 获取sheet的真正最好有一行
        /// </summary>
        /// <param name="shtName"></param>
        /// <returns></returns>
        public int GetLastRow(string shtName)
        {
            Excel.Worksheet sht = Wbk.Sheets[shtName];
            int maxRow = sht.Range["a" + sht.Rows.Count.ToString()].End[Excel.XlDirection.xlUp].Row;
            for (int c = 2; c <= ColumnToIndex(GetLastColumn(shtName)); c++)
            {
                int rowNum = sht.Range[IndexToColumn(c) + sht.Rows.Count.ToString()].End[Excel.XlDirection.xlUp].Row;
                if (rowNum > maxRow)
                    maxRow = rowNum;
            }
            return maxRow;
        }
        /// <summary>
        /// 获取sheet的最后一列，取前100行作为标准
        /// </summary>
        /// <param name="shtName"></param>
        /// <returns></returns>
        public string GetLastColumn(string shtName)
        {
            Excel.Worksheet sht = Wbk.Sheets[shtName];
            int maxCol = sht.Range["zz1"].End[Excel.XlDirection.xlToLeft].Column;
            for (int r = 2; r < 100; r++)
            {
                int colNum = sht.Range["zz" + r.ToString()].End[Excel.XlDirection.xlToLeft].Column;
                if (colNum > maxCol)
                    maxCol = colNum;
            }
            return IndexToColumn(maxCol);
        }

        /// <summary>
        /// 用于excel表格中列号字母转成列索引，从1对应A开始
        /// </summary>
        /// <param name="column">列号</param>
        /// <returns>列索引</returns>
        private int ColumnToIndex(string column)
        {
            if (!Regex.IsMatch(column.ToUpper(), @"[A-Z]+"))
            {
                throw new Exception("Invalid parameter");
            }
            int index = 0;
            char[] chars = column.ToUpper().ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                index += ((int)chars[i] - (int)'A' + 1) * (int)Math.Pow(26, chars.Length - i - 1);
            }
            return index;
        }

        /// <summary>
        /// 用于将excel表格中列索引转成列号字母，从A对应1开始
        /// </summary>
        /// <param name="index">列索引</param>
        /// <returns>列号</returns>
        private string IndexToColumn(int index)
        {
            if (index <= 0)
            {
                throw new Exception("Invalid parameter");
            }
            index--;
            string column = string.Empty;
            do
            {
                if (column.Length > 0)
                {
                    index--;
                }
                column = ((char)(index % 26 + (int)'A')).ToString() + column;
                index = (int)((index - index % 26) / 26);
            } while (index > 0);
            return column;
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="csList"></param>
        public void ImportData(List<CellSegment> csList)
        {
            this.Wbk = App.ActiveWorkbook;
            foreach (CellSegment cs in csList)
            {
                string[] poses = cs.CellPosition.Split(',');
                foreach (string pos in poses)
                {
                    Wbk.Worksheets[cs.ShtName].Range[pos].value = cs.Text;
                }
            }
        }

        /// <summary>
        /// 合并到一个Excel中
        /// </summary>
        /// <param name="mainFile"></param>
        /// <param name="files"></param>
        /// <param name="v"></param>
        public void MergeIntoMainExcel(string mainFile, string[] subFiles, string posInfoCol, string diffFile = "")
        {
            //建立一个新的 MainFile
            this.Wbk = this.App.Workbooks.Add();
            Excel.Worksheet mainSht = this.Wbk.ActiveSheet;

            Excel.Workbook diffWbk = null;
            Excel.Worksheet diffSht = null;
            if (diffFile != "")
            {
                diffWbk = App.Workbooks.Add();
                diffSht = diffWbk.ActiveSheet;
            }

            List<CellSegment_Ex> segList = new List<CellSegment_Ex>();

            //遍历子文件获取信息并写入到mainsht中
            string mainPath = Path.GetDirectoryName(mainFile);
            foreach (string subFile in subFiles)
            {
                string relativePath = subFile.Replace(mainPath, "..");
                ShowStatus(string.Format("正在抽取文件：{0}...", relativePath));
                segList.AddRange(ExctractContentInfoByWorkBook(subFile, relativePath, false, diffSht, posInfoCol));
            }

            //写入到主表单    
            ShowStatus("正在写入数据...");
            WriteToMainSht(mainSht, segList, posInfoCol);

            //隐藏位置信息列
            mainSht.Range[posInfoCol + ":" + posInfoCol].ColumnWidth = 0;
            if (diffFile != "")
            {
                //删除第一行 因为第一行是空
                diffSht.Range["1:1"].EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                diffSht.Range[posInfoCol + ":" + posInfoCol].ColumnWidth = 0;
                diffWbk.Title = "DifferentFont";
                diffWbk.SaveAs(diffFile);
                //关闭
                diffWbk.Close(Excel.XlSaveAction.xlDoNotSaveChanges);
            }
            this.Wbk.Title = "Merged";
            this.Wbk.SaveAs(mainFile);
            //关闭
            Wbk.Close(Excel.XlSaveAction.xlDoNotSaveChanges);

            ShowStatus("合并完成");
        }

        /// <summary>
        /// 写入到主表单
        /// </summary>
        /// <param name="mainSht"></param>
        /// <param name="segList"></param>
        /// <param name="posInfoCol"></param>
        public void WriteToMainSht(Excel.Worksheet mainSht, List<CellSegment_Ex> segList, string posInfoCol)
        {
            for (int i = 0; i < segList.Count; i++)
            {
                string posInfo = segList[i].RelativeFilePath + "|" + segList[i].ShtName + "|" + segList[i].CellPosition;
                mainSht.Range[posInfoCol + (i + 1).ToString()].Value = posInfo;
                mainSht.Range[IndexToColumn(ColumnToIndex(posInfoCol) + 1) + (i + 1).ToString()].Value = segList[i].Text;
            }
        }

        /// <summary>
        /// 抽取文件的所有内容信息
        /// </summary>
        /// <param name="subFile"></param>
        /// <returns></returns>
        public List<CellSegment_Ex> ExctractContentInfoByWorkBook(string subFile, string relativePath, bool extractHiddenCells = false, Excel.Worksheet diffSht = null, string posInfoCol = "A")
        {
            List<CellSegment_Ex> csxList = new List<CellSegment_Ex>();

            //先打开文件
            Excel.Workbook subWbk = this.App.Workbooks.Open(subFile);

            //交换wbk和tempWbk
            this.TempWbk = this.Wbk;
            this.Wbk = subWbk;

            foreach (Excel.Worksheet subSht in subWbk.Worksheets)
            {
                int lastColumn = ColumnToIndex(GetLastColumn(subSht.Name));
                int lastRow = GetLastRow(subSht.Name);
                for (int col = 1; col <= lastColumn; col++)
                {
                    for (int rw = 1; rw <= lastRow; rw++)
                    {
                        Excel.Range rng = subSht.Cells[rw, col];
                        if (!extractHiddenCells && (rng.Width <= 0 || rng.Height <= 0))
                            continue;
                        else
                        {
                            if (string.IsNullOrEmpty(rng.Text.Trim()))
                                continue;

                            CellSegment_Ex csx = new CellSegment_Ex(rng.Text, subSht.Name, IndexToColumn(col) + rw.ToString(), relativePath);
                            csxList.Add(csx);
                            //如果需要收集不同字体，并且有不同字体时就copy
                            if (diffSht != null && HasDifferentFont(rng))
                            {
                                int diffLastRow = diffSht.Range[posInfoCol + diffSht.Rows.Count.ToString()].End[Excel.XlDirection.xlUp].Row;
                                diffSht.Range[posInfoCol + (diffLastRow + 1).ToString()].Value = csx.RelativeFilePath + "|" + csx.ShtName + "|" + csx.CellPosition;
                                //Copy
                                subSht.Activate();
                                rng.Copy();
                                //paste
                                diffSht.Activate();
                                diffSht.Range[IndexToColumn(ColumnToIndex(posInfoCol) + 1) + (diffLastRow + 1).ToString()].PasteSpecial(Excel.XlPasteType.xlPasteAll);
                            }
                        }
                    }
                }
            }
            //再交换回来
            this.Wbk = this.TempWbk;
            this.TempWbk = null;
            //关闭文件
            subWbk.Close(Excel.XlSaveAction.xlDoNotSaveChanges);
            return csxList;
        }
        /// <summary>
        /// 判断rng的Character中是否都是同一种字体
        /// </summary>
        /// <param name="rng"></param>
        /// <returns></returns>
        public bool HasDifferentFont(Excel.Range rng)
        {
            //rng的characters.font 参数中有一个是null值的，即表示有不同字体，参数中包括 Bold,Color,Italic, Name, Size, StrikeThrough, Subscript, Superscript, Underline

            if (rng.Characters.Count <= 1)
                return false;

            if (rng.Characters.Font.Bold is DBNull)
                return true;
            else if (rng.Characters.Font.Color is DBNull)
                return true;
            else if (rng.Characters.Font.Italic is DBNull)
                return true;
            else if (rng.Characters.Font.Name is DBNull)
                return true;
            else if (rng.Characters.Font.Size is DBNull)
                return true;
            else if (rng.Characters.Font.Strikethrough is DBNull)
                return true;
            else if (rng.Characters.Font.Subscript is DBNull)
                return true;
            else if (rng.Characters.Font.Superscript is DBNull)
                return true;
            else if (rng.Characters.Font.Underline is DBNull)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 往目录文件导回数据
        /// </summary>
        /// <param name="resFD"></param>
        /// <param name="resDataFile"></param>
        public void RestoreBackData(string folder, string dataFile)
        {
            //打开datafile
            ShowStatus("打开数据文件...");
            OpenFile(dataFile);
            //判断是Merged File 还是DifferentFont File
            if (this.Wbk.Title == "Merged")
                MergedFileRestore(folder,"Merged");
            else
                MergedFileRestore(folder, "DifferentFont");
            this.Wbk.Close();

        }
        /// <summary>
        /// DifferentFont 类型的文件导回方法
        /// </summary>
        /// <param name="folder"></param>
        public void DifferentFontRestore(string folder)
        {
            Excel.Worksheet dataSht = this.Wbk.ActiveSheet;
            string curAbsPath = "";
            Excel.Workbook subWbk = null;
            int LastRow = GetLastRow(dataSht.Name);
            for (int i = 1; i <= LastRow; i++)
            {
                string posInfo= dataSht.Range["a" + i.ToString()].Text;
                string[] splits = posInfo.Split('|');
                string absPath = splits[0].Replace("..", folder);
                if (absPath != curAbsPath)
                {
                    if (i != 1)
                    {
                        subWbk.Save();
                        subWbk.Close();
                    }
                    if (!File.Exists(absPath))
                    {
                        System.Windows.Forms.MessageBox.Show(string.Format("{0} 不存在...", absPath));
                        continue;
                    }
                    subWbk = App.Workbooks.Open(absPath);
                    curAbsPath = absPath;
                }
                Excel.Worksheet subSht = subWbk.Worksheets[splits[1]];
                dataSht.Range["b" + i.ToString()].Copy();
                subSht.Range[splits[2]].PasteSpecial(Excel.XlPasteType.xlPasteAll);

                if (i == LastRow)
                {
                    subWbk.Save();
                    subWbk.Close();
                }
            }
        }

        /// <summary>
        /// Merged File 类型的文件导回方法
        /// </summary>
        /// <param name="folder"></param>
        public void MergedFileRestore(string folder,string restoreMehod)
        {
            #region 字典方法
            ////获取数据字典，key是相对路径
            //Dictionary<string, List<CellSegment_Ex>> csxDic = GetRestoreDataDic();
            ////遍历字典
            //foreach (KeyValuePair<string, List<CellSegment_Ex>> kvp in csxDic)
            //{
            //    string absPath = kvp.Key.Replace("..", folder);
            //    if (!File.Exists(absPath))
            //    {
            //        System.Windows.Forms.MessageBox.Show(string.Format("{0} 不存在", absPath));
            //        continue;
            //    }
            //    ShowStatus(string.Format("{0} 数据导入中...", absPath));
            //    Excel.Workbook subWbk = App.Workbooks.Open(absPath);
            //    //遍历字典中的value List
            //    foreach (CellSegment_Ex csx in kvp.Value)
            //    {
            //        Excel.Worksheet subSht = subWbk.Worksheets[csx.ShtName];
            //        subSht.Range[csx.CellPosition].Value = csx.Text;        //导回数据
            //    }
            //    //关闭
            //    subWbk.Save();
            //    subWbk.Close(Excel.XlSaveAction.xlDoNotSaveChanges);
            //    ShowStatus(string.Format("{0} 数据导入完成", absPath));
            //} 
            #endregion

            //获取数据列表
            //List<CellSegment_Ex> csxList = GetRestoreDataList();
            //遍历dataSht 并导回数据
            Excel.Worksheet dataSht = this.Wbk.ActiveSheet;
            string curAbsPath = "";
            Excel.Workbook subWbk = null;
            int LastRow = GetLastRow(dataSht.Name);
            for (int i = 1; i <= LastRow; i++)
            {
                string posInfo = dataSht.Range["a" + i.ToString()].Text;
                string[] splits = posInfo.Split('|');
                string absPath = splits[0].Replace("..", folder);
                if (absPath != curAbsPath)
                {
                    if (i != 1)
                    {
                        subWbk.Save();
                        subWbk.Close();
                    }
                    if (!File.Exists(absPath))
                    {
                        System.Windows.Forms.MessageBox.Show(string.Format("{0} 不存在...", absPath));
                        continue;
                    }
                    subWbk = App.Workbooks.Open(absPath);
                    curAbsPath = absPath;
                }
                Excel.Worksheet subSht = subWbk.Worksheets[splits[1]];
                if (restoreMehod == "DifferentFont")
                {
                    dataSht.Range["b" + i.ToString()].Copy();
                    subSht.Range[splits[2]].PasteSpecial(Excel.XlPasteType.xlPasteAll);
                }
                else if (restoreMehod== "Merged")
                    subSht.Range[splits[2]].Value = dataSht.Range["b" + i.ToString()].Text;

                if (i == LastRow)
                {
                    subWbk.Save();
                    subWbk.Close();
                }
            }
        }

        /// <summary>
        /// 获取数据字典，key是相对路径
        /// </summary>
        /// <param name="restoreFolder"></param>
        /// <returns></returns>
        public Dictionary<string, List<CellSegment_Ex>> GetRestoreDataDic()
        {
            List<CellSegment_Ex> csxList = new List<CellSegment_Ex>();
            if (this.Wbk == null)
                return null;

            Excel.Worksheet dataSht = this.Wbk.ActiveSheet;
            //获取数据
            ShowStatus("读取数据中...");
            for (int i = 1; i <= GetLastRow(dataSht.Name); i++)
            {
                string posinfo = dataSht.Range["a" + i.ToString()].Text;
                string[] splits = posinfo.Split('|');
                csxList.Add(new CellSegment_Ex(dataSht.Range["b" + i.ToString()].Text, splits[1], splits[2], splits[0]));
            }
            //根据文件名归类
            ShowStatus("数据归类...");
            Dictionary<string, List<CellSegment_Ex>> csxDic = new Dictionary<string, List<CellSegment_Ex>>();
            foreach (CellSegment_Ex csx in csxList)
            {
                if (csxDic.ContainsKey(csx.RelativeFilePath))
                    csxDic[csx.RelativeFilePath].Add(csx);
                else
                    csxDic.Add(csx.RelativeFilePath, new List<CellSegment_Ex>() { csx });
            }
            return csxDic;
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<CellSegment_Ex> GetRestoreDataList()
        {
            List<CellSegment_Ex> csxList = new List<CellSegment_Ex>();
            if (this.Wbk == null)
                return null;

            Excel.Worksheet dataSht = this.Wbk.ActiveSheet;
            //获取数据
            ShowStatus("读取数据中...");
            for (int i = 1; i <= GetLastRow(dataSht.Name); i++)
            {
                string posinfo = dataSht.Range["a" + i.ToString()].Text;
                string[] splits = posinfo.Split('|');
                csxList.Add(new CellSegment_Ex(dataSht.Range["b" + i.ToString()].Text, splits[1], splits[2], splits[0]));
            }

            return csxList;
        }
    }
}
