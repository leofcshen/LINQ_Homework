using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinQ_Homework
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Frm作業_1());
            //Application.Run(new Frm作業_2_AdventureWorksDataSet());
            //Application.Run(new Frm作業_3());
            Application.Run(new Frm考試());
        }
    }
}
