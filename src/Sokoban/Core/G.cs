using System;
using System.Collections.Generic;
using System.IO;

namespace Sokoban
{
    public sealed class G : IDisposable
    {
        public const string APP_NAME = "Sokoban";
        public const string EMBEDDED_LEVELS = "Sokoban.levels.pack";
        public const string EMBEDDED_MENU = "Sokoban.menu.pack";

        private static G _instance = new G();
        public static G I { get { return _instance; } }
        private G() { }

        private Level level;
        public Level Level { get { return level; } }
        private Logic logic;
        public Logic Logic { get { return logic; } }
        private View view;
        public View View { get { return view; } }

        public readonly List<Level> Levels = new List<Level>();
        private Level splashLevel;
        bool isSplashLevel = false;
        public bool IsSplashLevel { get { return isSplashLevel; } }

        public void StartLevel(Level level)
        {
            isSplashLevel = false;
            this.level = level;
            logic = new Logic(level);
            view = new View(logic);
        }

        public void StartSplashLevel()
        {
            isSplashLevel = true;
            level = splashLevel;
            logic = new Logic(level);
            view = new View(logic);
        }

        public void Load(string[] args)
        {
            foreach (string name in args)
            {
                Stream stream = Loader.OpenFile(name);
                List<Level> levels = Loader.LoadPack(stream);
                Levels.AddRange(levels);
                stream.Close();
            }

            Stream embStream = Loader.OpenEmbeddedResource(EMBEDDED_LEVELS);
            List<Level> embLevels = Loader.LoadPack(embStream);
            Levels.AddRange(embLevels);
            embStream.Close();

            embStream = Loader.OpenEmbeddedResource(EMBEDDED_MENU);
            splashLevel = Loader.LoadPack(embStream)[0];
            embStream.Close();
        }

        public void Dispose()
        {
            view.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
