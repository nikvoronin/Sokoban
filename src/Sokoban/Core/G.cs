using System;
using System.Collections.Generic;
using System.IO;
using XInput.Wrapper;

namespace Sokoban.Core
{
    public sealed class G : IDisposable
    {
        public const string APP_NAME = "Sokoban";
        public const string EMBEDDED_LEVELS = "Sokoban.levels.pack";
        public const string EMBEDDED_MENU = "Sokoban.menu.pack";

        private static G _instance = new G();
        public static G I { get { return _instance; } }
        private G()
        {
            Gamepad = X.IsAvailable ? X.Gamepad_1 : null;
        }

        private DateTime startTime = DateTime.Now;
        private Logic logic;
        public Logic Logic { get { return logic; } }
        private View view;
        public View View { get { return view; } }

        public X.Gamepad Gamepad = null;

        public readonly List<Level> Levels = new List<Level>();
        private Level splashLevel;
        bool isSplashLevel = false;
        public bool IsSplashLevel { get { return isSplashLevel; } }

        public void Start(Level level = null)
        {
            isSplashLevel = level == null;
            StartLevel(
                isSplashLevel ?
                    splashLevel :
                    level);
        }

        private void StartLevel(Level level)
        {
            startTime = DateTime.Now;
            logic = new Logic(level);
            view = new View(level, logic);
        }

        public void StartNextLevel()
        {
            int idx = Levels.IndexOf(logic.Map) + 1;
            StartLevel(
                idx < Levels.Count ?
                    Levels[idx] :
                    null);
        }

        public string ElapsedTimeLongString
        {
            get
            {
                TimeSpan span = TimeSpan.FromTicks(DateTime.Now.Ticks - startTime.Ticks);
                return
                    string.Format("{0}{1}:{2}:{3}",
                        span.Days > 0 ? span.Days.ToString() + "d " : "",
                        span.Hours,
                        span.Minutes.ToString("00"),
                        span.Seconds.ToString("00")
                        );
            }
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

        public void Win()
        {
            // STUB
            SaveRecordTable();
        }

        private void SaveRecordTable()
        {
            // STUB
        }

        public void Dispose()
        {
            view.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
