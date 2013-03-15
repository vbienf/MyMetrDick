using System;
using System.Windows.Forms;

namespace PerformanceLab.Utils.ReportMaker
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
        static class Datapr
        {
            public static int Value { get; set; }
        }
    }
}
