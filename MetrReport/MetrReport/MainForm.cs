using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using System.Globalization;
using OfficeOpenXml;
using OfficeOpenXml.Table.PivotTable;
using OfficeOpenXml.Style.XmlAccess;
using OfficeOpenXml.Style;
using PfLb.ReportMaker;

namespace PerformanceLab.Utils.ReportMaker
{
    public partial class MainForm : Form
    {
        //private int progrDonePerc = 0; 
        private readonly JMeterLogDictionary jMeterLogDictionary= new JMeterLogDictionary(new FileInfo(@"dictionary.txt"));
        private readonly JMeterLogDictionary excelDictionary = new JMeterLogDictionary(new FileInfo(@"mpageDict.txt"));

        private string mesStat = null;
        private int viI = 0;

        public MainForm()
        {
            InitializeComponent();
        }
        public Form FormPr;

        private void MainForm_Load(object sender, EventArgs e)
        {
            fromTime.Enabled = toTime.Enabled = true;
            timer1.Enabled = false;
            progressBarThread.Value = 0;
            labelMetrics.Text = "Ожидание...";
            Application.ApplicationExit += Application_Exit;
        }

        private void Application_Exit(object sender, EventArgs e)
        {
            PerlMaster.Stop();
        }

        private void buttonMakeXLSX_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl.SelectedIndex == 0)
                {
                    if ((numericEffort.Value == 0 || numericRump.Value < 0 || numericUpDown1.Value < 0) && checkBoxTime.Checked == false) throw new Exception("Не заданы параметры запуска: время разгона и(или) количество заявок");
                    if (metricsGridView.RowCount == 0) throw new Exception("Не заданы параметры запуска: файлы с метриками отсутствуют!");
                    if (!metricsGridView[0, 1].Value.ToString().Contains("Metrics")) throw new Exception("Не заданы параметры запуска: файлы с метриками должны находиться в папке Metrics, в папке соответствующего сервера!");
                    if (!backgroundWorker.IsBusy)
                    {

                        buttonMakeXLSX.Enabled = false;
                        this.Cursor = Cursors.WaitCursor;
                        mesStat = "Начинаем'c!";
                        progressBarThread.Value = 0;
                        timer1.Enabled = true;
                        FormElemEnabl(false);
                        backgroundWorker.RunWorkerAsync("Unix");/*
                    ProgressForm FormProgr = new ProgressForm();
                    FormPr = FormProgr.Stat;
                    FormProgr.ShowDialog();  */
                    }
                }
            }
            catch (Exception ex)
            { Popup.ShowException(ex); }

        }

        private void FormElemEnabl(bool roger)
        {
            numericUpDown1.Enabled = roger;
            numericEffort.Enabled = roger;
            fromTime.Enabled = roger;
            toTime.Enabled = roger;
            buttonClear.Enabled = roger;
            buttonMakeXLSX.Enabled = roger;
        }


        private void checkBoxLimit_CheckedChanged(object sender, EventArgs e)
        {
            fromTimeJM.Enabled = ToTimeJM.Enabled = checkBoxLimit.Checked;
        }



        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var from = fromTime.Text;
            var to = toTime.Text;
            int rumpMin = (int)numericRump.Value;
            int OneHundPerc = metricsGridView.RowCount * 2 + 7;
            int OnePerc = (OneHundPerc) / 100;
            string buff = metricsGridView[0, 0].Value.ToString();
            string[] Pieces = buff.Split('\\');
            foreach (string str in Pieces)
                if (str.Contains("app") || str.Contains("db"))
                {
                    int r = str.IndexOf("(") + 1;
                    int lr = str.LastIndexOf(")");
                    buff = str.Substring(r, lr - r);
                    buff = buff.ToUpper();
                    //MessageBox.Show(buff);
                    if (buff.Length > 7) buff = buff.Substring(0, 7);
                }

            if (e.Argument.ToString() == "Unix")
            {                
                if (buff == metricsGridView[0, 0].Value.ToString())
                    buff = "";
                mesStat = "Проверка на существующий  Excel-файл";
                backgroundWorker.ReportProgress(1);
                try
                {
                    if (File.Exists("Результат_Теста_" + (int)numericUpDown1.Value + "_"+"(" + buff.ToLower() + ")" + ".xlsx")) throw new IOException("Файл с таким именем уже существует");

                }
                catch (IOException ex)
                {
                    Popup.ShowException(ex);
                    return;
                }
                mesStat = "Создание нового Excel-файла";

                var xlsx = new FileInfo("Результат_Теста_" + (int)numericUpDown1.Value + "_"+"(" + buff.ToLower() + ")" + ".xlsx");
                var em = new ExcelMaster(xlsx, excelDictionary);

                DateTime t1 = new DateTime();
                DateTime t2 = new DateTime();
                for (int row = 0; row < metricsGridView.RowCount; row++)
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(metricsGridView[0, row].Value.ToString()))
                        {
                            string sheetN = getListName(System.IO.Path.GetFullPath(metricsGridView[0, row].Value.ToString()));
                            mesStat = "Обрабатываю " + sheetN;
                            ExcelWorksheet hidden = em.AddMetricWorksheet(sheetN, sr, from, to, ';');
                            backgroundWorker.ReportProgress((row + 1) * 100 / OneHundPerc);

                            //while (!DateTime.TryParse(to, out t1))
                            if (!(metricsGridView[0, row].Value.ToString().Contains("sys_info"))) 
                                em.AddPivotTables("pivot " + hidden.Name, hidden.Cells[hidden.Dimension.Address]);
                            backgroundWorker.ReportProgress((row + 2) * OnePerc / OneHundPerc);
                            if (metricsGridView[0, row].Value.ToString().Contains("disk"))
                            {
                                em.AddPivotTables("pivot (" + buff.ToLower() + ") disk_io", hidden.Cells[hidden.Dimension.Address]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Popup.ShowException(ex);
                    }
                }
                // toolStripStatusLabel1.Text = "Metrics copied";
                mesStat = "Метрики с таблицами добавлены! Создаем графики...";

                int i = metricsGridView.RowCount;
                i++;
                int minutes = 0;
                if (DateTime.TryParse(to, out t2) && DateTime.TryParse(from, out t1))
                    minutes = (int)(t2 - t1).TotalMinutes;
                try
                {                    
                    em.AddCPUTable("CPU_All", rumpMin, minutes, buff);
                    i = changebar("всем ядрам", i * 100 / OneHundPerc, i);
                    em.AddCPUTable("CPU", rumpMin, minutes, buff);
                    i = changebar("процессору", i * 100 / OneHundPerc, i);
                    em.AddMemTable("Mem", rumpMin, minutes, buff);
                    i = changebar("памяти", i * 100 / OneHundPerc, i);
                    em.AddDiskTable("Disk_io", rumpMin, minutes, buff);
                    i = changebar("времени отклику дисков", i * 100 / OneHundPerc, i);
                    em.AddDiskTable("Disk_use", rumpMin, minutes, buff);
                    i = changebar("нагрузке дисков", i * 100 / OneHundPerc, i);
                    em.AddMainSheet("Main", int.Parse(numericEffort.Value.ToString()), from, to, buff);
                    i = changebar("общей информации о системе", i * 100 / OneHundPerc, i);
                    em.hideLists(buff);
                }
                catch (Exception ex)
                {
                    Popup.ShowException(ex);
                }
                em.Finish();
                backgroundWorker.ReportProgress(100);
                try
                {
                    var pi = new ProcessStartInfo(xlsx.FullName);
                    Process.Start(pi);
                }
                catch (Win32Exception)
                {
                }
            }
        }

        private int changebar(string mes, int perc,int i)
        {
            mesStat = "Создаем графики... Добавлен график по " + mes;
            backgroundWorker.ReportProgress(perc);
            return i++;
        }

        private string getListName(string path)
        {
            return path.Split('\\')[path.Split('\\').Length-3]+System.IO.Path.GetFileNameWithoutExtension(path);
        }


        private void buttonClear_Click(object sender, EventArgs e)
        {
            fromTime.Text = "";
            toTime.Text = "";
            fromTimeJM.Text = "";
            ToTimeJM.Text = "";
            for (var i = metricsGridView.Rows.Count - 1; i > -1; --i)
            {
                metricsGridView.Rows.RemoveAt(i);
            }
            checkBoxLimit.Checked = true;
            numericEffort.Value = 0;
            numericEffortJM.Value = 0;
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericRump.Value = 0;
            labelMetrics.Text = "Ожидание...";
            progressBarThread.Value = 0;

        }

        private bool norepeat(string s,DataGridView table)
        {
            for (int i = 0; i < table.RowCount; i++)
                if (table[0, i].Value.Equals(s)) return false;
            return true;
        }

        private void addMetFilesButton_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                if (metrFoldBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (var res in Directory.GetFiles(metrFoldBrowserDialog.SelectedPath, "*cpu_a*", SearchOption.AllDirectories))
                        if (res != null && norepeat(res, metricsGridView)) metricsGridView.Rows.Add(System.IO.Path.GetFullPath(res));
                    foreach (var res in Directory.GetFiles(metrFoldBrowserDialog.SelectedPath, "*cpu2*", SearchOption.AllDirectories))
                        if (res != null && norepeat(res, metricsGridView)) metricsGridView.Rows.Add(System.IO.Path.GetFullPath(res));
                    foreach (var res in Directory.GetFiles(metrFoldBrowserDialog.SelectedPath, "*mem*", SearchOption.AllDirectories))
                        if (res != null && norepeat(res, metricsGridView)) metricsGridView.Rows.Add(System.IO.Path.GetFullPath(res));
                    foreach (var res in Directory.GetFiles(metrFoldBrowserDialog.SelectedPath, "*disk*", SearchOption.AllDirectories))
                        if (res != null && norepeat(res, metricsGridView)) metricsGridView.Rows.Add(System.IO.Path.GetFullPath(res));
                    foreach (var res in Directory.GetFiles(metrFoldBrowserDialog.SelectedPath, "*sys_info.txt*", SearchOption.AllDirectories))
                        if (res != null && norepeat(res, metricsGridView)) metricsGridView.Rows.Add(System.IO.Path.GetFullPath(res));
                }
            }
            else
            {
                if (metrFoldBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (var res in Directory.GetFiles(metrFoldBrowserDialog.SelectedPath, "*day-aggregate*", SearchOption.AllDirectories))
                        if (res != null && norepeat(res, metricsGridView)) JmeterGridView.Rows.Add(System.IO.Path.GetFullPath(res));
                }
            }
        }

        private void metricsGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                for (var i = 0; i < e.RowCount; ++i)
                {
                    metricsGridView[1, i + e.RowIndex].Value =
                        jMeterLogDictionary.Lookup(System.IO.Path.GetFileName(metricsGridView[0, i + e.RowIndex].Value.ToString()));
                }
            }
            else
            {
                for (var i = 0; i < e.RowCount; ++i)
                {
                    JmeterGridView[1, i + e.RowIndex].Value =
                        jMeterLogDictionary.Lookup(System.IO.Path.GetFileName(JmeterGridView[0, i + e.RowIndex].Value.ToString()));
                }
            }
        }


        private void metricsGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                var dataGridViewColumn = metricsGridView.Columns["MDelete"];
                if (dataGridViewColumn != null && !backgroundWorker.IsBusy && e.ColumnIndex == dataGridViewColumn.Index)
                {
                    metricsGridView.Rows.RemoveAt(e.RowIndex);
                }
            }
            else
            {
                var dataGridViewColumn = JmeterGridView.Columns["MDelete"];
                if (dataGridViewColumn != null && !backgroundWorker.IsBusy && e.ColumnIndex == dataGridViewColumn.Index)
                {
                    JmeterGridView.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void deleteMetrButton_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                for (var i = metricsGridView.Rows.Count - 1; i > -1; --i)
                {
                    metricsGridView.Rows.RemoveAt(i);
                }
            }
            else
            {
                for (var i = JmeterGridView.Rows.Count - 1; i > -1; --i)
                {
                    JmeterGridView.Rows.RemoveAt(i);
                }
            }
        }

        private void metricsGridView_DragEnter(object sender, DragEventArgs e)
        {
           if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void metricsGridView_DragDrop(object sender, DragEventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                //var hui = e.Data.GetData(DataFormats.FileDrop, false);
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                foreach (string file in files)
                {
                    getcsvfile("cpu_a", file);
                 //   foreach (string file in files)
                        getcsvfile("cpu2", file);
                   // foreach (string file in files)
                        getcsvfile("mem", file);
                    //foreach (string file in files)
                        getcsvfile("disk", file);
                   // foreach (string file in files)
                        getcsvfile("sys_info.txt", file);
                }
            }
            else
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                foreach (string file in files)
                    if (file != null && norepeat(file, metricsGridView) && file.Contains("day-aggregate")) JmeterGridView.Rows.Add(System.IO.Path.GetFullPath(file));
            }
        }

        private void getcsvfile(string dif, string dir)
        {
            if (dir != null && norepeat(dir, metricsGridView) && dir.Contains(dif)) metricsGridView.Rows.Add(System.IO.Path.GetFullPath(dir));
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Cursor = Cursors.Default;
            FormElemEnabl(true);
        }
        
        

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            viI  = e.ProgressPercentage;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBarThread.Value = viI;
            labelMetrics.Text = mesStat;
            if (viI == 100)
            {
                timer1.Enabled = false;
                FormElemEnabl(true);
                labelMetrics.Text = "Готово!";
                //MessageBox.Show("Готово!");
            }
        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            //tabControl.SelectedIndex = 0;
            buttonClear_Click(sender, e);
        }

        private void checkBoxTime_CheckedChanged(object sender, EventArgs e)
        {
            fromTime.Enabled = toTime.Enabled = !checkBoxTime.Checked;
        }
    }
}
