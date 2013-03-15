using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PfLb.ReportMaker
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
        }

        public int progrDonePerc = 0; 
        public string Stat = "Начинаем!";
        

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            labelMetrics.Text = "Начинаем!";
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBarThread.Value = progrDonePerc;
            if (progrDonePerc == 100)
            {
                timer1.Enabled = false;
                labelMetrics.Text = "Готово!";
                MessageBox.Show("Готово!");
                this.Close();
            }
            else labelMetrics.Text = Stat;
        }
    }
}
