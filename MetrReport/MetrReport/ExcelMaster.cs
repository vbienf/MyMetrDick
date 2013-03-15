using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;
using OfficeOpenXml.Style.XmlAccess;
using OfficeOpenXml.Table.PivotTable;
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Collections;
using PfLb.ReportMaker;

namespace PerformanceLab.Utils.ReportMaker
{
    /// <summary>
    /// Класс содержит методы для работы с Excel-файлом отчета. Является оберткой над библиотекой EPPlus.
    /// </summary>
    class ExcelMaster
    {
        private readonly JMeterLogDictionary dictExcel;

        private readonly ExcelPackage _excelFile;
        private readonly ExcelNamedStyleXml styleHHMMSS;
        private readonly ExcelNamedStyleXml styleHHMM;
        private readonly ExcelNamedStyleXml stylePercent;
        private readonly ExcelNamedStyleXml styleDouble;
        private readonly ExcelNamedStyleXml styleHyperLink;
        private readonly ExcelNamedStyleXml styleBlock;

        private static readonly string AUTHOR = "ReportMaker";
        private static readonly string TITLE = "Report Sample";
        private static readonly string COMPANY = "Performance Lab";
        private static ArrayList DiskList = new ArrayList();
        private static ArrayList Cores = new ArrayList();
        private static ArrayList CPUzagolov = new ArrayList();
        private static ArrayList InfoList = new ArrayList();

        //string[] headers = new string[2] {"Сервер приложений, узел1;Сервер приложений, узел2;Балансировщик сервера приложений;БД, узел1;БД, узел1;Шина, узел1; Шина, узел2;Балансировшик шины",
          //  "ra-bx07;ra-bx08;ra-vx71;dbperf1;dbperf2;ra-vx42;ra-vx43;ra-vx44"};

        /// <summary>
        /// Инициализирует объект с указанием результирующего XLSX-файла.
        /// </summary>
        /// <param name="excelFile">Имя результирующего файла.</param>
        /// <exception cref="ArgumentNullException">Исключение возникает, если результирующий файл не задан.</exception>
        public ExcelMaster(FileInfo excelFile,JMeterLogDictionary dict)
        {
            if (excelFile == null) throw new ArgumentNullException("excelFile", "XLSX-файл не указан!");

            dictExcel = dict;
            _excelFile = new ExcelPackage(excelFile);
            _excelFile.Workbook.Properties.Author = AUTHOR;
            _excelFile.Workbook.Properties.Title = TITLE;
            _excelFile.Workbook.Properties.Company = COMPANY;

            styleHHMMSS = _excelFile.Workbook.Styles.CreateNamedStyle("MyTime");
            styleHHMMSS.Style.Numberformat.Format = "dd.mm.yy hh:mm:ss";

            styleHHMM = _excelFile.Workbook.Styles.CreateNamedStyle("AbsTime");
            styleHHMM.Style.Numberformat.Format = "hh:mm:ss";

            stylePercent = _excelFile.Workbook.Styles.CreateNamedStyle("MyPercent");
            stylePercent.Style.Numberformat.Format = "0.00%";

            styleDouble = _excelFile.Workbook.Styles.CreateNamedStyle("MyDouble");
            styleDouble.Style.Numberformat.Format = "#.00";

            styleHyperLink = _excelFile.Workbook.Styles.CreateNamedStyle("MyHLink");
            styleHyperLink.Style.Font.UnderLine = true;
            styleHyperLink.Style.Font.Color.SetColor(Color.Blue);

            styleBlock = _excelFile.Workbook.Styles.CreateNamedStyle("MyBlock");
            styleBlock.Style.Font.Bold = true;            
            styleBlock.Style.Fill.PatternType = ExcelFillStyle.Solid;
            styleBlock.Style.Fill.BackgroundColor.SetColor(Color.Gray);
            styleBlock.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
        }

        /// <summary>
        /// Добавить лист с Aggregate Report, используя поток для получения данных.
        /// </summary>
        /// <param name="sheetName">Имя создаваемого листа.</param>
        /// <param name="inputStream">Поток, содержащий данные для содаваемого листа.</param>
        /// <param name="delimiter">Разделитель значений в строках потока.</param>
        /// <returns>Ссылка на созданный лист.</returns>
        public ExcelWorksheet AddAggregateReportWorksheet(string sheetName, StreamReader inputStream, 
                                                          char delimiter = '\t')
        {
            
            var newSheet = _excelFile.Workbook.Worksheets.Add(sheetName);
            newSheet.Cells.Style.Font.Size = 11;
            newSheet.Cells.Style.Font.Name = "Calibri";
            newSheet.View.FreezePanes(2, 1);      
            
            var format = new ExcelTextFormat();
            format.Delimiter = delimiter;
            format.Culture = new CultureInfo("ru-RU");
            format.Culture.NumberFormat.NumberDecimalSeparator = ",";      

            var row = 1;
            try
            {
                while (!inputStream.EndOfStream)
                    newSheet.Cells["A" + row++].LoadFromText(inputStream.ReadLine(), format);
            }

            catch (ObjectDisposedException ode)
            {
                Popup.ShowException(ode);
            }
            catch (OutOfMemoryException oome)
            {
                Popup.ShowException(oome);
            }
            catch (IOException ioe)
            {
                Popup.ShowException(ioe);
            }

            newSheet.Column(8).StyleName = stylePercent.Name;
            if (newSheet.Dimension != null) 
            {
                newSheet.Cells[newSheet.Dimension.Address].AutoFitColumns(); 
                var border = newSheet.Cells[newSheet.Dimension.Address].Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
            }
              
            return newSheet;
        }

        public ExcelWorksheet AddMainSheet(string sheetName, int effort,string fromTime, string toTime, string serv, char delimiter = '\t')
        {            
            int[] indicators = { 1050, 106, 300, 46, 176, 100, 90, 90, 10, 10, 300, 0, 138 };

            //progr.Stat = sheetName;

            var newSheet = _excelFile.Workbook.Worksheets.Add(sheetName);
            
            newSheet.Cells.Style.Font.Size = 11;
            newSheet.Cells.Style.Font.Name = "Calibri";

            var format = new ExcelTextFormat();
            format.Delimiter = delimiter;
            format.Culture = new CultureInfo("ru-RU");
            format.Culture.NumberFormat.NumberDecimalSeparator = ",";

            using (ExcelRange col = newSheet.Cells["A1:B4,D6:D8,D10:D13,D15:D19,D21"])
            {
                var border = col.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                //col.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            }

            //MessageBox.Show(InfoList.Count.ToString());
            int cout = 5;
            if (InfoList.Count < 5)
                for (int i = InfoList.Count; i < cout; i++)
                    InfoList.Add("0");
            
            //левый верхний блок
            newSheet.Cells["A1"].Value = "Дата теста";
            newSheet.Cells["A2"].Value = "Профиль";
            newSheet.Cells["B1"].Value = fromTime.Substring(0,(fromTime.IndexOf(' ')));
            newSheet.Cells["B2"].Value = effort + "%";
            newSheet.Cells["A3"].Value = "Время начала";
            newSheet.Cells["B3"].Value = fromTime.Substring((fromTime.IndexOf(' ')) + 1);
            newSheet.Cells["B4"].Value = toTime.Substring((toTime.IndexOf(' ')) + 1);
            newSheet.Cells["B3,B4"].Style.Numberformat.Format = "##:##:##00:00:00";
            newSheet.Cells["A4"].Value = "Время завершения";

            //Блок тестируемая среда            
            newSheet.Cells["D6"].Value = "Тестируемая среда " + serv;
            newSheet.Cells["D6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            newSheet.Cells[7,3].Value = "Имя сервера";
            newSheet.Cells["D7"].Value = InfoList[1];
            newSheet.Cells[8,3].Value = "Операционная система";
            newSheet.Cells["D8"].Value = InfoList[0];
            newSheet.Cells["C7:C8"].StyleName = "MyBlock";

            //Блок Процессор            
            newSheet.Cells["D10"].Value = "Процессор";
            newSheet.Cells["D10"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            newSheet.Cells[11,3].Value = "Общее количество активных ядер";
            if (InfoList[4].ToString().Contains("---"))
                InfoList[4] = Cores.Count;
            newSheet.Cells["D11"].Value = InfoList[4];
            newSheet.Cells[12,3].Value = "Модель процессора";
            newSheet.Cells["D12"].Value = InfoList[3];
            newSheet.Cells[13,3].Value = "Средняя загрузка, %";
            newSheet.Cells["D13"].Formula = _excelFile.Workbook.Worksheets["CPU"].Cells["H3"].FullAddress;
            newSheet.Cells["C11:C13"].StyleName = "MyBlock";

            //Блок Память            
            newSheet.Cells["D15"].Value = "Оперативная память";
            newSheet.Cells["D15"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            newSheet.Cells[16,3].Value = "Общее количество памяти, Mb";
            /*if (!(InfoList[2].ToString() == "0"))
                newSheet.Cells["D16"].Value = doMemTot(InfoList[2].ToString());
            else*/
                newSheet.Cells["D16"].Value = InfoList[2];
            newSheet.Cells[17,3].Value = "Средняя загрузка памяти, %";
            newSheet.Cells["D17"].Formula = _excelFile.Workbook.Worksheets["Mem"].Cells["E3"].FullAddress; ;
            newSheet.Cells[18,3].Value = "Свободная  память, Мb";
            newSheet.Cells["D18"].Formula = newSheet.Cells["D16"].Address + "*" + newSheet.Cells["D19"].Address + "/100";
            newSheet.Cells["D18"].Style.Numberformat.Format = "###000";
            newSheet.Cells[19,3].Value = "Свободная память, %";
            newSheet.Cells["D19"].Formula = "100-" + newSheet.Cells["D17"].Address;

            newSheet.Cells["D13,D17,D19"].Style.Numberformat.Format = "#,##0.00";
            newSheet.Cells["C16:C19"].StyleName = "MyBlock";

            //Блок диски
            newSheet.Cells["D21"].Value = "Диски";
            newSheet.Cells["D21"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            for (int i = 0; i < DiskList.Count; i++)
            {
                newSheet.Cells["C" + (22 + i)].Value = i + 1;
                newSheet.Cells["D" + (22 + i)].Value = DiskList[i];
            }
            newSheet.Cells["C22:C" + newSheet.Dimension.End.Row].StyleName = "MyBlock";
            newSheet.Cells["C22:C" + newSheet.Dimension.End.Row].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            using (ExcelRange col = newSheet.Cells["D22:D" + newSheet.Dimension.End.Row])
            {
                var border = col.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                //col.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            }

            newSheet.Cells["F6"].Hyperlink = new ExcelHyperLink(_excelFile.Workbook.Worksheets["CPU_All"].Name + "!A1", "График загрузки по всем ядрам");
            newSheet.Cells["F10"].Hyperlink = new ExcelHyperLink(_excelFile.Workbook.Worksheets["CPU"].Name + "!A1", "Данные по процессору");
            newSheet.Cells["F15"].Hyperlink = new ExcelHyperLink(_excelFile.Workbook.Worksheets["Mem"].Name + "!A1", "Данные по памяти");
            newSheet.Cells["F21"].Hyperlink = new ExcelHyperLink(_excelFile.Workbook.Worksheets["Disk_use"].Name + "!A1", "Данные по дискам (нагрузка)");
            newSheet.Cells["F22"].Hyperlink = new ExcelHyperLink(_excelFile.Workbook.Worksheets["Disk_io"].Name + "!A1", "Данные по дискам (время отклика)");
            newSheet.Cells["F6,F10,F15,F22,F21"].StyleName = "MyHLink";
                         
            if (newSheet.Dimension != null) 
            {
                newSheet.Cells[newSheet.Dimension.Address].AutoFitColumns();
            }
            
            _excelFile.Workbook.Worksheets.MoveToStart(_excelFile.Workbook.Worksheets.Count);
            return newSheet;
        }

        private int doMemTot(string MemString)
        {            
            if (!InfoList[0].ToString().Contains("inux"))
                return int.Parse(MemString.Remove(MemString.IndexOf(" ")));
            return int.Parse(MemString.Remove(MemString.IndexOf(" "))) / 1024;            
        }

        /// <summary>
        /// Добавить лист с метриками 
        /// </summary>
        /// <param name="sheetName">Имя создаваемого листа.</param>
        /// <param name="inputStream">Поток, содержащий данные для содаваемого листа.</param>
        /// <param name="delimiter">Разделитель значений в строках потока.</param>
        /// <returns>Ссылка на созданный лист.</returns>
        public ExcelWorksheet AddMetricWorksheet(string sheetName, StreamReader inputStream, string fromTime, string toTime, char delimiter = '\t')
        {
            var newSheet = _excelFile.Workbook.Worksheets.Add(sheetName);
            newSheet.Cells.Style.Font.Size = 11;
            newSheet.Cells.Style.Font.Name = "Calibri";

            var format = new ExcelTextFormat();
            format.Delimiter = delimiter;
            format.Culture = new CultureInfo("ru-RU");
            format.Culture.NumberFormat.NumberDecimalSeparator = ",";
            int row = 1;
            newSheet.Cells["A" + row].LoadFromText(inputStream.ReadLine(), format);
            row++;
            int next = 0;
            if (sheetName.Contains("sys_info"))
            {
                while (!inputStream.EndOfStream)
                {
                    string buff = inputStream.ReadLine();
                    if ((row % 3) == 0)
                    {
                        InfoList.Add(buff);
                        //MessageBox.Show(InfoList[next].ToString());
                        next++;
                    }
                    newSheet.Cells["A" + row].LoadFromText(buff, format);
                    row++;                    
                }
            }
            else
            {
                newSheet.Column(1).StyleName = "MyTime";
                while (!inputStream.EndOfStream)
                {
                    newSheet.Cells["A" + row].LoadFromText(inputStream.ReadLine(), format);
                    if (!((row > 1) && (DateTime.Parse(newSheet.Cells["A" + row].Text) >= (DateTime.Parse(fromTime)) &&
                                (DateTime.Parse(newSheet.Cells["A" + row].Text) <= (DateTime.Parse(toTime))))))
                    {
                        newSheet.DeleteRow(row, 1);
                    }
                    else row++;
                }
            }
            if (sheetName.Contains("disk"))
            {
                for (int i = 2; i < newSheet.Dimension.End.Row; i++)
                {
                    DiskList.Add(newSheet.Cells["B" + i].Value);
                }
                DiskList = RemoveDuplicate(DiskList);
            }
            if (sheetName.Contains("cpu_a"))
            {
                for (int i = 2; i < newSheet.Dimension.End.Row; i++)
                {
                    Cores.Add((newSheet.Cells["B" + i].Value).ToString());
                   // MessageBox.Show((newSheet.Cells["B" + i].Value).ToString());
                }
                Cores = RemoveDuplicate(Cores);
                for (int i = 3; i < 7; i++)
                {
                    CPUzagolov.Add((newSheet.Cells[1, i].Value).ToString());
                    //MessageBox.Show((newSheet.Cells[1, i].Value).ToString());
                }
            }
            if (newSheet.Dimension != null) 
            {
                newSheet.Cells[newSheet.Dimension.Address].AutoFitColumns();
            }
            return newSheet;
        }

        private static ArrayList RemoveDuplicate(ArrayList sourceList)
        {
            ArrayList list = new ArrayList();
            foreach (string item in sourceList)
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public ExcelWorksheet AddPivotTables(string sheetName,ExcelRangeBase source, char delimiter = '\t')
        {
            ///ProgressForm FormProgr = new ProgressForm();            FormProgr.Stat = sheetName;

            var newSheet = _excelFile.Workbook.Worksheets.Add(sheetName);
            newSheet.Cells.Style.Font.Size = 11;
            newSheet.Cells.Style.Font.Name = "Calibri";

            var format = new ExcelTextFormat();
            format.Delimiter = delimiter;
            format.Culture = new CultureInfo("ru-RU");
            format.Culture.NumberFormat.NumberDecimalSeparator = ",";

            var pivotTable = newSheet.PivotTables.Add(newSheet.Cells["A1"], source, "Pivot "+sheetName);
            pivotTable.RowFields.Add(pivotTable.Fields[0]);
            

            if (_excelFile.Workbook.Worksheets[newSheet.Index].Name.Contains("pivot") && _excelFile.Workbook.Worksheets[newSheet.Index].Name.Contains("cpu2"))
            {
                var usrField = pivotTable.DataFields.Add(pivotTable.Fields[1]);
                usrField.Name = source.Worksheet.Cells[1, 2].Text;
                usrField.Function = DataFieldFunctions.Average;

                var sysField = pivotTable.DataFields.Add(pivotTable.Fields[2]);
                sysField.Name = source.Worksheet.Cells[1, 3].Text;
                sysField.Function = DataFieldFunctions.Average;

                var wioField = pivotTable.DataFields.Add(pivotTable.Fields[3]);
                wioField.Name = source.Worksheet.Cells[1, 4].Text;
                wioField.Function = DataFieldFunctions.Average;

                var loadField = pivotTable.DataFields.Add(pivotTable.Fields[4]);
                loadField.Name = source.Worksheet.Cells[1, 5].Text;
                loadField.Function = DataFieldFunctions.Average;

            }
            if (_excelFile.Workbook.Worksheets[newSheet.Index].Name.Contains("pivot") && _excelFile.Workbook.Worksheets[newSheet.Index].Name.Contains("mem"))
            {
                var dataField = pivotTable.DataFields.Add(pivotTable.Fields[3]);
                dataField.Name = source.Worksheet.Cells[1, 4].Text;
                dataField.Function = DataFieldFunctions.Average;
            }
            if (_excelFile.Workbook.Worksheets[newSheet.Index].Name.Contains("pivot") && _excelFile.Workbook.Worksheets[newSheet.Index].Name.Contains("disk_io"))
            {
                var dataName = pivotTable.ColumnFields.Add(pivotTable.Fields[1]);
                dataName.Name = source.Worksheet.Cells[1, 2].Text;
                /*/pivotTable.Fields[1].Sort = eSortType.Ascending;
                if (_excelFile.Workbook.Worksheets[newSheet.Index].Name.Contains("db"))
                {*/
                    var dataField2 = pivotTable.DataFields.Add(pivotTable.Fields[6]);
                    dataField2.Name = source.Worksheet.Cells[1, 7].Text;
                    dataField2.Function = DataFieldFunctions.Average;
                /*}
                else
                {
                    var dataField2 = pivotTable.DataFields.Add(pivotTable.Fields[4]);
                    dataField2.Name = source.Worksheet.Cells[1, 5].Text;
                    dataField2.Function = DataFieldFunctions.Average;
                }*/
            }
            if (_excelFile.Workbook.Worksheets[newSheet.Index].Name.Contains("pivot") && _excelFile.Workbook.Worksheets[newSheet.Index].Name.Contains("sar-disk"))
            {
                var dataName = pivotTable.ColumnFields.Add(pivotTable.Fields[1]);
                dataName.Name = source.Worksheet.Cells[1, 2].Text;
                //pivotTable.Fields[1].Sort = eSortType.Ascending;

                var dataField = pivotTable.DataFields.Add(pivotTable.Fields[2]);
                dataField.Name = source.Worksheet.Cells[1, 3].Text;
                dataField.Function = DataFieldFunctions.Average;

            }
            if (_excelFile.Workbook.Worksheets[newSheet.Index].Name.Contains("pivot") && _excelFile.Workbook.Worksheets[newSheet.Index].Name.Contains("cpu_"))
            {
                var dataName = pivotTable.ColumnFields.Add(pivotTable.Fields[1]);
                dataName.Name = source.Worksheet.Cells[1, 2].Text;
                pivotTable.Fields[1].Sort = eSortType.Ascending;
                var dataField = pivotTable.DataFields.Add(pivotTable.Fields[5]);
                dataField.Name = source.Worksheet.Cells[1, 6].Text;
                dataField.Function = DataFieldFunctions.Average;
            }

            pivotTable.DataOnRows = false;  
            return newSheet;
        }

        public ExcelWorksheet AddCPUTable(string sheetName, int minutes, int interval, string serv,char delimiter = '\t')
        {
            _excelFile.Workbook.Worksheets.Add(sheetName);
            var newSheet = _excelFile.Workbook.Worksheets[_excelFile.Workbook.Worksheets.Count];

            var format = new ExcelTextFormat();
            format.Delimiter = delimiter;
            format.Culture = new CultureInfo("ru-RU");
            format.Culture.NumberFormat.NumberDecimalSeparator = ",";
            newSheet.Cells["A:R"].Style.Numberformat.Format = "General";
            newSheet.Cells["A2"].Value = "Time";
            newSheet.Cells["B2"].Value = "Absol. time";
            newSheet.Column(1).StyleName = "MyTime";
            newSheet.Column(2).StyleName = "AbsTime";
            newSheet.Cells.Style.Font.Name = "Calibri";
            newSheet.Cells.Style.Font.Size = 8;
            newSheet.DefaultColWidth = 50;


            int column;
            int row;
            int indexCPU = 0;
            int indexCPUA = 0;

            
            try 
            {
                if (sheetName == "CPU_All")
                {
                    for (int page = 1; page < _excelFile.Workbook.Worksheets.Count; page++)
                        if (_excelFile.Workbook.Worksheets[page].Name.Contains("pivot") && _excelFile.Workbook.Worksheets[page].Name.Contains("cpu_"))
                        {
                            row = 0;
                            indexCPUA = page;
                            int n = Cores.Count + 2;
                            for (column = 2; column < n; column++)
                            {
                                newSheet.Cells[2, column + 1].Value = "Core #" + Convert.ToString(column - 2);
                                for (row = 3; row < 5 + interval; row++)
                                {
                                    newSheet.Cells[row, column + 1].Formula = _excelFile.Workbook.Worksheets[indexCPUA].Cells[row, column].FullAddress;
                                }
                            }
                            using (ExcelRange col = newSheet.Cells[1, 1, newSheet.Dimension.End.Row, n])
                            {
                                var border = col.Style.Border;
                                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                            }
                        }
                }
                else if (sheetName == "CPU")
                {

                    for (int page = 1; page < _excelFile.Workbook.Worksheets.Count; page++)
                    {
                        column = 6;
                        row = 0;
                        if (_excelFile.Workbook.Worksheets[page].Name.Contains("pivot") && _excelFile.Workbook.Worksheets[page].Name.Contains("cpu2"))
                        {
                            indexCPU = page;
                            //string header = dictExcel.search(_excelFile.Workbook.Worksheets[index].Name);
                            for (column = 3; column < 7; column++)
                                for (row = 2; row < 5 + interval; row++)
                                {                                    
                                    newSheet.Cells[row, column].Formula = _excelFile.Workbook.Worksheets[indexCPU].Cells[row, column - 1].FullAddress;
                                }
                            for (int i = 0; i < 4; i++)
                            {
                                newSheet.Cells[2, i + 3].Value = CPUzagolov[i];
                            }

                            newSheet.Cells[3, column + 1].Formula = newSheet.Cells["F"+newSheet.Dimension.End.Row].FullAddress;// String.Format("AVERAGE({0}:{1})", _excelFile.Workbook.Worksheets[indexCPU].Cells[3 + minutes, 3].Address, _excelFile.Workbook.Worksheets[indexCPU].Cells[row - 1, 3].Address);
                            //column++;
                            using (ExcelRange col = newSheet.Cells["H2:H3"])
                            {
                                var border = col.Style.Border;
                                newSheet.Cells["H2"].Value = "AVERAGE";
                                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                            }
                            using (ExcelRange col = newSheet.Cells["A1:F" + newSheet.Dimension.End.Row])
                            {
                                var border = col.Style.Border;
                                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                            }
                            newSheet.Cells["F2"].Value = serv;
                        }
                    }
                }
                if (indexCPUA == 0) //отрезок определяющий "Absol. time"
                {                    
                    for (row = 3; row < newSheet.Dimension.End.Row + 1; row++)
                    {
                        newSheet.Cells[row, 1].Formula = _excelFile.Workbook.Worksheets[indexCPU].Cells[row, 1].FullAddress;
                        if (row != newSheet.Dimension.End.Row)
                            newSheet.Cells[row, 2].Formula = newSheet.Cells[row, 1].Address + "-" + newSheet.Cells[3, 1].Address;
                    }
                }
                else
                {                    
                    for (row = 3; row < newSheet.Dimension.End.Row + 1; row++)
                    {
                        newSheet.Cells[row, 1].Formula = _excelFile.Workbook.Worksheets[indexCPUA].Cells[row, 1].FullAddress;
                        if (row != newSheet.Dimension.End.Row)
                            newSheet.Cells[row, 2].Formula = newSheet.Cells[row, 1].Address + "-" + newSheet.Cells[3, 1].Address;
                    }
                }

                

                if (indexCPUA == 0) //отрезок кода рисующий график
                {
                    var loadChart = newSheet.Drawings.AddChart("CPU-Load", eChartType.Line);
                    loadChart.Title.Text = serv + " CPU Load, %";
                    loadChart.Title.Font.Size = 12;
                    loadChart.SetPosition(200, 300);
                    loadChart.Legend.Position = eLegendPosition.Bottom;                  
                    loadChart.Legend.Add();
                    loadChart.SetSize(1000, 600);
                    loadChart.YAxis.Format = "#,##0";
                    loadChart.YAxis.MaxValue = 100;
                    loadChart.YAxis.MinValue = 0;

                    for (int i = 3; i < 4; i++)
                    {
                        var serie = loadChart.Series.Add(ExcelRange.GetAddress(3, i + 3, newSheet.Dimension.End.Row - 1, i + 3), ExcelRange.GetAddress(3, 2, newSheet.Dimension.End.Row - 1, 2));
                        serie.Header = newSheet.Cells[2, i + 3].Text;
                    }

                    var OstChart = newSheet.Drawings.AddChart("CPU-use", eChartType.AreaStacked);
                    OstChart.Title.Text = serv + " CPU usage, %";
                    OstChart.Title.Font.Size = 12;
                    OstChart.SetPosition(1000, 300);
                    OstChart.Legend.Position = eLegendPosition.Bottom;
                    OstChart.Legend.Add();
                    OstChart.SetSize(1000, 600);
                    OstChart.YAxis.Format = "#,##0";
                    loadChart.YAxis.MaxValue = 100;
                    loadChart.YAxis.MinValue = 0;

                    for (int i = 0; i < 3; i++)
                    {
                        var serie = OstChart.Series.Add(ExcelRange.GetAddress(3, i + 3, newSheet.Dimension.End.Row - 1, i + 3), ExcelRange.GetAddress(3, 2, newSheet.Dimension.End.Row - 1, 2));
                        serie.Header = newSheet.Cells[2, i + 3].Text;
                    }

                }
                else
                {
                    var chart = newSheet.Drawings.AddChart("CPU_ALL", eChartType.AreaStacked);
                    chart.Title.Text = serv + " CPU usage cores, %";
                    chart.Title.Font.Size = 12;
                    chart.SetPosition(200, 300);
                    chart.Legend.Position = eLegendPosition.Bottom;
                    chart.Legend.Add();
                    chart.SetSize(1000, 600);
                    chart.YAxis.Format = "#,##0";
                    chart.YAxis.MaxValue = 100 * Cores.Count;                    
                    for (int i = 0; i < Cores.Count; i++)
                    {
                        var serie = chart.Series.Add(ExcelRange.GetAddress(3, i + 3, newSheet.Dimension.End.Row - 1, i + 3), ExcelRange.GetAddress(3, 2, newSheet.Dimension.End.Row - 1, 2));
                        serie.Header = Convert.ToString(newSheet.Cells[2, i + 3].Value);
                    }   
                }                
            }
            catch (Exception ex)
            {
                Popup.ShowException(ex);

            }
            return newSheet;
        }
        
        public ExcelWorksheet AddMemTable(string sheetName,int minutes, int interval, string serv, char delimiter = '\t')
        {
            //ProgressForm FormProgr = new ProgressForm();            FormProgr.Stat = sheetName;

            _excelFile.Workbook.Worksheets.Add(sheetName);
            var newSheet = _excelFile.Workbook.Worksheets[_excelFile.Workbook.Worksheets.Count];
            //newSheet.DefaultColWidth = 50;

            var format = new ExcelTextFormat();
            format.Delimiter = delimiter;
            format.Culture = new CultureInfo("ru-RU");
            format.Culture.NumberFormat.NumberDecimalSeparator = ",";

            try
            {
                newSheet.Cells["A2"].Value = "Time";
                newSheet.Cells["B2"].Value = "Absol. time";
                newSheet.Cells["C2"].Value = serv;

                int index = 0;
                int column = 3;
                for (int page = 1; page < _excelFile.Workbook.Worksheets.Count; page++)
                {
                    int row = 0;
                    if (_excelFile.Workbook.Worksheets[page].Name.Contains("pivot") && _excelFile.Workbook.Worksheets[page].Name.Contains("mem"))
                    {
                        index = page;
                        string header = dictExcel.search(_excelFile.Workbook.Worksheets[page].Name);
                       // newSheet.Cells[1, column + 9].Value = newSheet.Cells[1, column].Value = header.Split('_')[0];
                        //newSheet.Cells[2, column + 9].Value = newSheet.Cells[2, column].Value = header.Split('_')[1];
                        for (row = 3; row < 4 + interval; row++)
                        {
                            if (_excelFile.Workbook.Worksheets[page].Name.Contains("app"))
                                newSheet.Cells[row, column].Formula = _excelFile.Workbook.Worksheets[page].Cells[row, getFieldNum(_excelFile.Workbook.Worksheets[page].PivotTables[0], "%")].FullAddress;
                            else
                                newSheet.Cells[row, column].Formula = _excelFile.Workbook.Worksheets[page].Cells[row, getFieldNum(_excelFile.Workbook.Worksheets[page].PivotTables[0], "%")].FullAddress;
                        }
                        newSheet.Cells[row, column].Formula = newSheet.Cells[3, column + 2].Formula = String.Format("AVERAGE({0}:{1})", _excelFile.Workbook.Worksheets[page].Cells[3 + minutes, column].Address, _excelFile.Workbook.Worksheets[page].Cells[row - 1, column].Address);
                        column++;
                    }
                }

                for (int row = 3; row < newSheet.Dimension.End.Row + 1; row++)
                {
                    newSheet.Cells[row, 1].Formula = _excelFile.Workbook.Worksheets[index].Cells[row, 1].FullAddress;
                    if (row != newSheet.Dimension.End.Row)
                        newSheet.Cells[row, 2].Formula = newSheet.Cells[row, 1].Address + "-" + newSheet.Cells[3, 1].Address;
                }

                newSheet.Column(1).StyleName = "MyTime";
                newSheet.Column(2).StyleName = "AbsTime";
                newSheet.Cells.Style.Font.Name = "Calibri";
                newSheet.Cells.Style.Font.Size = 8;
                newSheet.Cells["C:X"].Style.Numberformat.Format = "#,##0.00";
                newSheet.Cells["E2"].Value = "AVERAGE";
                using (ExcelRange col = newSheet.Cells["E2:E3,A1:C"+newSheet.Dimension.End.Row])
                {
                    var border = col.Style.Border;
                    border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                }

                var chart = newSheet.Drawings.AddChart("Memory", eChartType.Line);
                chart.Title.Text = serv + " Memory usage, %";
                chart.Title.Font.Size = 12;
                chart.SetPosition(200, 300);
                chart.Legend.Position = eLegendPosition.Bottom;
                chart.Legend.Add();
                chart.SetSize(1000, 600);
                chart.YAxis.MaxValue = 100;
                chart.YAxis.Format = "#0";
                chart.YAxis.MinValue = 0;
                for (int i = 0; i < 1; i++)
                {
                    var serie = chart.Series.Add(ExcelRange.GetAddress(3, i + 3, newSheet.Dimension.End.Row - 1, i + 3), ExcelRange.GetAddress(3, 2, newSheet.Dimension.End.Row - 1, 2));
                    serie.Header = newSheet.Cells[2, i + 3].Text;
                }
            }
            catch (Exception ex)
            {
                Popup.ShowException(ex);
            }
            if (newSheet.Dimension != null)
            {
                newSheet.Cells[newSheet.Dimension.Address].AutoFitColumns();
            }
            return newSheet;
        }
        public ExcelWorksheet AddDiskTable(string sheetName, int minutes, int interval, string serv, char delimiter = '\t')
        {
            //ProgressForm FormProgr = new ProgressForm();            FormProgr.Stat = sheetName;

            _excelFile.Workbook.Worksheets.Add(sheetName);
            var newSheet = _excelFile.Workbook.Worksheets[_excelFile.Workbook.Worksheets.Count];


            var format = new ExcelTextFormat();
            format.Delimiter = delimiter;
            format.Culture = new CultureInfo("ru-RU");
            format.Culture.NumberFormat.NumberDecimalSeparator = ",";

            int column = 3;
            try
            {
                newSheet.Cells["A2"].Value = "Time";
                newSheet.Cells["B2"].Value = "Absol. time";

                int index = 0;
                int indexA = 0;
                int row = 0;

                if (sheetName == "Disk_io")
                {
                    for (int page = 1; page < _excelFile.Workbook.Worksheets.Count; page++)
                    {
                        row = 0;
                        if (_excelFile.Workbook.Worksheets[page].Name.Contains("pivot") && _excelFile.Workbook.Worksheets[page].Name.Contains("disk_io"))
                        {
                            index = page;
                            int n = DiskList.Count + 2;/*
                            if (_excelFile.Workbook.Worksheets[page].Name.Contains("app"))
                                n = 4;
                            else n = 15;*/
                            for (column = 2; column < n; column++)
                            {
                                 newSheet.Cells[2, column + 1].Value = DiskList[column-2];
                                for (row = 3; row < 5 + interval; row++)
                                {
                                    newSheet.Cells[row, column + 1].Formula = _excelFile.Workbook.Worksheets[index].Cells[row, column].FullAddress;
                                }
                            }
                            for (row = 3; row < newSheet.Dimension.End.Row + 1; row++)
                            {
                                newSheet.Cells[row, 1].Formula = _excelFile.Workbook.Worksheets[index].Cells[row, 1].FullAddress;
                                if (row != newSheet.Dimension.End.Row)
                                    newSheet.Cells[row, 2].Formula = newSheet.Cells[row, 1].Address + "-" + newSheet.Cells[3, 1].Address;
                            }
                            column = n + 1;
                            newSheet.Cells[2, column].Value = "20 ms";
                            for (row = 3; row < 4 + interval; row++)
                            {
                                newSheet.Cells[row, column].Value = 20;
                            }
                        }
                    }
                } else if (sheetName == "Disk_use")
                {
                    for (int page = 1; page < _excelFile.Workbook.Worksheets.Count; page++)
                    {
                        row = 0;
                        if (_excelFile.Workbook.Worksheets[page].Name.Contains("pivot") && _excelFile.Workbook.Worksheets[page].Name.Contains("sar-disk"))
                        {
                            indexA = page;
                            int n = DiskList.Count + 2;/*
                            if (_excelFile.Workbook.Worksheets[page].Name.Contains("app"))
                                n = 4;
                            else n = 15;*/
                            for (column = 2; column < n; column++)
                            {
                                newSheet.Cells[2, column + 1].Value = DiskList[column - 2];
                                for (row = 3; row < 5 + interval; row++)
                                {
                                    newSheet.Cells[row, column + 1].Formula = _excelFile.Workbook.Worksheets[indexA].Cells[row, column].FullAddress;
                                }
                            }
                            for (row = 3; row < newSheet.Dimension.End.Row + 1; row++)
                            {
                                newSheet.Cells[row, 1].Formula = _excelFile.Workbook.Worksheets[indexA].Cells[row, 1].FullAddress;
                                if (row != newSheet.Dimension.End.Row)
                                    newSheet.Cells[row, 2].Formula = newSheet.Cells[row, 1].Address + "-" + newSheet.Cells[3, 1].Address;
                            }
                        }
                    }
                }
                
                

                newSheet.Column(1).StyleName = "MyTime";
                newSheet.Column(2).StyleName = "AbsTime";
                newSheet.Cells.Style.Font.Name = "Calibri";
                newSheet.Cells.Style.Font.Size = 8;
                newSheet.Cells["C:X"].Style.Numberformat.Format = "#,##0.00";

                if (indexA == 0) //отрезок кода рисующий график
                {
                    var chart = newSheet.Drawings.AddChart("Disk_io", eChartType.Line);
                    chart.Title.Text = serv + " Disk iotime, ms";
                    chart.Title.Font.Size = 12;
                    chart.SetPosition(200, 300);
                    chart.Legend.Position = eLegendPosition.Bottom;
                    chart.Legend.Add();
                    chart.SetSize(1000, 600);
                    chart.YAxis.Format = "#0";
                    int n = DiskList.Count;
                    using (ExcelRange col = newSheet.Cells[1, 1, newSheet.Dimension.End.Row, n + 3])
                    {
                        var border = col.Style.Border;
                        border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                    }
                    for (int i = 0; i < n + 1; i++)
                    {
                        var serie = chart.Series.Add(ExcelRange.GetAddress(3, i + 3, newSheet.Dimension.End.Row - 1, i + 3), ExcelRange.GetAddress(3, 2, newSheet.Dimension.End.Row - 1, 2));
                        serie.Header = newSheet.Cells[2, i + 3].Text;
                         if  (i==n+1) chart.Border.LineStyle = eLineStyle.Dash;
                    }
                }
                else
                {
                    var chart = newSheet.Drawings.AddChart("Disk_use", eChartType.Line);
                    chart.Title.Text = serv + " Disk usage, %";
                    chart.Title.Font.Size = 12;
                    chart.SetPosition(200, 300);
                    chart.Legend.Position = eLegendPosition.Bottom;
                    chart.Legend.Add();
                    chart.SetSize(1000, 600);
                    chart.YAxis.Format = "#0";
                    chart.YAxis.MaxValue = 100;
                    chart.YAxis.MinValue = 0;
                    int n = DiskList.Count;
                    using (ExcelRange col = newSheet.Cells[1, 1, newSheet.Dimension.End.Row, n + 2])
                    {
                        var border = col.Style.Border;
                        border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                    }
                    for (int i = 0; i < n; i++)
                    {
                        var serie = chart.Series.Add(ExcelRange.GetAddress(2, i + 3, newSheet.Dimension.End.Row - 1, i + 3), ExcelRange.GetAddress(3, 2, newSheet.Dimension.End.Row - 1, 2));
                        serie.Header = newSheet.Cells[2, i + 3].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Popup.ShowException(ex);

            }
            if (newSheet.Dimension != null)
            {
                newSheet.Cells[newSheet.Dimension.Address].AutoFitColumns();
            }
            return newSheet;
        }
       

        public int getFieldNum(ExcelPivotTable table, string toFind)
        {
            for (int i = 0; i < table.DataFields.Count; i++)
            {
                if (table.DataFields[i].Name.Contains(toFind)) return i + 2;
            }
            return -1;
        }
       
        public void hideLists(string bf)
        {
            for (int page = 1; page <= _excelFile.Workbook.Worksheets.Count; page++)
            {
                if (_excelFile.Workbook.Worksheets[page].Name.Contains(bf.ToLower()) || _excelFile.Workbook.Worksheets[page].Name.Contains("pivot") || _excelFile.Workbook.Worksheets[page].Name.Contains("201")) _excelFile.Workbook.Worksheets[page].Hidden = eWorkSheetHidden.Hidden;
                if (_excelFile.Workbook.Worksheets[page].Name.Contains("sys_info")) _excelFile.Workbook.Worksheets.Delete(page);
            }
        }

        /// <summary>
        /// После того, как все вкладки созданы, <b>ОБЯЗАТЕЛЬНО</b> надо вызвать этот метод,
        /// чтобы сохранить изменения в файле.
        /// </summary>
        public void Finish()
        {
            //if (_excelFile.File.Length > 0) _excelFile.Save();
            try
            {
                _excelFile.Save();
            }
            catch (InvalidOperationException)
            {
            }
            DiskList.Clear();
            InfoList.Clear();
            CPUzagolov.Clear();
            Cores.Clear();
        }
    }
}