using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackgroundWorkerExample
{
    public partial class Form1 : Form
    {
        private Cursor _oldCursor;
        public Form1()
        {
            InitializeComponent();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            int N = 10000;
            for (int i = 0; i < N; i++)
            {
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                bw.ReportProgress((int) ((double)i/N*100));
                Thread.Sleep(10);
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string str = e.Cancelled ? "Задача отменена" : "Задача завершена до конца!";
            pgb.Value = 0;
            Cursor.Current = _oldCursor;
             MessageBox.Show(str);
            // Application.Exit();
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            _oldCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            
            backgroundWorker.RunWorkerAsync();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            backgroundWorker.CancelAsync();
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pgb.Value = e.ProgressPercentage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _oldCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            Thread.Sleep(10000);
            Invalidate();
        }
    }
}
