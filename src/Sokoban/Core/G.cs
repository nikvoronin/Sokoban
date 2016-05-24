using System.Collections.Generic;

namespace Sokoban
{
    public sealed class G
    {
        public const string APP_NAME = "Sokoban";
        public const string EMBEDDED_LEVELS = "Sokoban.levels.pack";
        public const string EMBEDDED_MENU = "Sokoban.menu.pack";

        private static G _instance = new G();
        public static G I { get { return _instance; } }
        private G() { }

        public int LevelNo = -1;
        public Level Level { get { return LevelNo > -1 ? Levels[LevelNo] : null; }}

        public readonly List<Level> Levels = new List<Level>();
        public Level SplashLevel;
    }
}
