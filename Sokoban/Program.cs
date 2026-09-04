using Sokoban.Core;

namespace Sokoban;

static class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        GameContext.I.Load(args);

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run( new GameForm(args.Length > 0) );
    }
}
