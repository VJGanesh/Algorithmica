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

namespace TempDelete
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread.Sleep(3000);
            //var thread = new Thread(new ThreadStart(this.ThreadTask));
            //thread.Name = "Verify task";
            //thread.Start();
        }

        private void ThreadTask()
        {
            Thread.Sleep(3000);
            System.Diagnostics.Debug.Print("Thread Finished");
        }
    }
}
