using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HaxeflixelCrashHandler
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                args = new string[] { "HAXEFLIXELCRASHHANDLER RUNNING IN DEBUG MODE" };
                Console.WriteLine("Running in debug mode");
            }
            if (args.Length < 1)
            {
                Application.Exit();
                return;
            }
            //args = new string[] {"sdasdfsdf s tth \n fsudhfhwthfyh hahahhaha"};
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HaxeflixelCrashHandler(args));
        }
    }
}
