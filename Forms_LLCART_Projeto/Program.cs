using System;
using System.Windows.Forms;

namespace Forms_LLCART_Projeto
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Views.frmMain());
        }
    }
}