using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Sokoban
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            foreach (string name in args)
            {
                Stream stream = Loader.OpenFile(name);
                List<Level> levels = Loader.LoadPack(stream);
                G.I.Levels.AddRange(levels);
                stream.Close();
            }

            Stream embStream = Loader.OpenEmbeddedResource(G.EMBEDDED_LEVELS);
            List<Level> embLevels = Loader.LoadPack(embStream);
            G.I.Levels.AddRange(embLevels);
            embStream.Close();

            embStream = Loader.OpenEmbeddedResource(G.EMBEDDED_MENU);
            G.I.SplashLevel = Loader.LoadPack(embStream)[0];
            embStream.Close();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameForm(args.Length > 0));
        }
    }
}
