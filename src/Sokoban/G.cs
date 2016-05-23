using System.Collections.Generic;

namespace Sokoban
{
    public sealed class G
    {
        public const string EMBEDDED_LEVELS = "Sokoban.levels.pack";
        public const string EMBEDDED_MENU = "Sokoban.menu.pack";

        private static G _instance = new G();
        public static G I { get { return _instance; } }
        private G() { }

        public int Zoom = 20;
        public int CurrentLevelNo = -1;

        public readonly List<LevelData> Levels = new List<LevelData>();
        public LevelData StartMenu;
    }
}
