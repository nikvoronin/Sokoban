using XInput.Wrapper;

namespace Sokoban.Core;

public sealed class GameContext : IDisposable
{
    public const string APP_NAME = "Sokoban";
    public const string EMBEDDED_LEVELS = "Sokoban.Levels.levels.zip";
    public const string EMBEDDED_MENU = "Sokoban.Levels.menu.pack";

    public static GameContext I { get; } = new();

    private GameContext()
    {
        Gamepad = X.IsAvailable ? X.Gamepad_1 : null;
    }

    private DateTime startTime = DateTime.Now;
    private Logic logic = null!;   // set by StartLevel(), always called before Logic is read
    public Logic Logic => logic;
    private View view = null!;     // set by StartLevel(), always called before View is read
    public View View => view;

    public X.Gamepad? Gamepad;

    public readonly List<Level> Levels = [];
    private Level splashLevel = null!; // set by Load(), always called before Start()
    bool isSplashLevel = false;
    public bool IsSplashLevel => isSplashLevel;

    public void Start(Level? level = null)
    {
        isSplashLevel = level == null;
        StartLevel(level ?? splashLevel);
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
        if (idx < Levels.Count)
            StartLevel(Levels[idx]);
        else
            Start(); // no more levels: back to the splash level instead of crashing
    }

    public string ElapsedTimeLongString
    {
        get
        {
            TimeSpan span = TimeSpan.FromTicks(DateTime.Now.Ticks - startTime.Ticks);
            return string.Format("{0}{1}:{2}:{3}",
                span.Days > 0 ? span.Days.ToString() + "d " : "",
                span.Hours,
                span.Minutes.ToString("00"),
                span.Seconds.ToString("00"));
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

    public void Dispose()
    {
        view.Dispose();
        GC.SuppressFinalize(this);
    }
}
