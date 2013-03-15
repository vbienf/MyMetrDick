using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace PerformanceLab.Utils.ReportMaker
{
    /// <summary>
    /// Класс содержит обертки над perl-скриптами: запускает их из командной строки,
    /// передает им на обработку файл и обрабатывает их вывод (stdout).
    /// </summary>
    static class PerlMaster
    {
        private static readonly string AGGREGATE_REPORT_PARSER_NAME = "perl jmeter_log_parser.pl";

        private static Process _perl;

        public static TimeSpan MakeAggregateReport(ExcelMaster excelMaster, string sheetName, FileInfo sourceLogFile,
                                                   string fromTime, string toTime)
        {
            var duration = new TimeSpan(-1);
            try
            {
                using (_perl = new Process())
                {
                    _perl.StartInfo.FileName = "cmd.exe";
                    _perl.StartInfo.Arguments = " /c \"cd parser && type \"" + sourceLogFile.FullName +
                                               "\" | " + AGGREGATE_REPORT_PARSER_NAME + " " + fromTime + " " + toTime + "\"";
                    
                    _perl.StartInfo.RedirectStandardError = false;
                    _perl.StartInfo.RedirectStandardInput = false;
                    _perl.StartInfo.RedirectStandardOutput = true;
                    _perl.StartInfo.CreateNoWindow = true;
                    _perl.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                    _perl.StartInfo.Verb = "Open";
                    _perl.StartInfo.UseShellExecute = false;

                    _perl.Start();

                    Thread.Sleep(1000); // время на одупление
                    if (_perl.HasExited && _perl.ExitCode != 0)
                    // если какой-то косяк с парсером (например, неверный формат input-файла)
                    {
                        return duration;
                    }

                    using (var stdout = _perl.StandardOutput)
                    {
                        excelMaster.AddAggregateReportWorksheet(sheetName, stdout);
                    }
                    //excelMaster.AddMainSheet("Main", 3000);
                    duration = _perl.ExitTime - _perl.StartTime;
                }
            }
            catch (InvalidOperationException)
            {
                return duration;
            }
            catch (FileNotFoundException)
            {
                Stop();
                return new TimeSpan(-3);
            }
            catch (ThreadInterruptedException)
            {
                Stop();
                return new TimeSpan(-2);
            }
            return duration;
        }

        public static bool Stop()
        {
            if (_perl != null)
            {
                foreach (var perl in Process.GetProcessesByName("perl"))
                {
                    if (perl.MainModule.FileName.EndsWith(@"parser\perl.exe")) perl.Kill();
                }
                _perl.Close(); // а это на самом деле не perl, а cmd, запустивший perl
                return true;
            }
            return false;
        }
    }
}