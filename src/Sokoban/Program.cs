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
            bool avoidMainMenu = args.Length > 0;

            foreach (string name in args)
            {
                Stream stream = LevelLoader.OpenFile(name);
                List<LevelData> levels = LevelLoader.LoadPack(stream);
                G.I.Levels.AddRange(levels);
                stream.Close();
            }

            Stream embStream = LevelLoader.OpenEmbeddedResource(G.EMBEDDED_LEVELS);
            List<LevelData> embLevels = LevelLoader.LoadPack(embStream);
            G.I.Levels.AddRange(embLevels);
            embStream.Close();

            embStream = LevelLoader.OpenEmbeddedResource(G.EMBEDDED_MENU);
            G.I.StartMenu = LevelLoader.LoadPack(embStream)[0];
            embStream.Close();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameForm(avoidMainMenu));
        }
    }
}
