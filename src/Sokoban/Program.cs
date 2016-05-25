using System;
using System.Windows.Forms;

namespace Sokoban
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            G.I.Load(args);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run( new GameForm(args.Length > 0) );
        }
    }
}
