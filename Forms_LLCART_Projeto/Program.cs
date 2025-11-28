using System;
using System.Windows.Forms;
 using Forms_LLCART_Projeto.Views; 

namespace Forms_LLCART_Projeto
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}