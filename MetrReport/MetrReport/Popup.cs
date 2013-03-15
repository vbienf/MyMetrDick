using System;
using System.Windows.Forms;

namespace PerformanceLab.Utils.ReportMaker
{
    internal static class Popup
    {
        public static DialogResult ShowException(Exception ex)
        {
            return MessageBox.Show("Произошла ошибка:\n" + ex.Message + "\n\nПодробная информация:\n" + ex.StackTrace,
                                   "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}