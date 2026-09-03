namespace Sokoban.Core;

/// <summary>
/// Contains and manages data (player, level's cells, statistics)
/// </summary>
public class Logic
{
    public readonly Level Map;  // template of the level
    Cell[,] cells;              // editable instance of the current level

    int playerX = 0;
    int playerY = 0;
    Point playerDir = Point.Empty;

    int steps = 0;
    int movements = 0;
    int inPlace = 0;

    public int Steps => steps;
    public int Movements => movements;
    public int InPlace => inPlace;
    public int PlayerX => playerX;
    public int PlayerY => playerY;
    public Point PlayerDir => playerDir;

    public readonly List<Point> CellsChanged = [];
    private readonly Stack<Action> history = new();

    public Logic(Level map)
    {
        steps = 0;
        Map = map;
        cells = (Cell[,])Map.Cells.Clone();
        inPlace = Map.InPlace;
        playerX = Map.StartAt.X;
        playerY = Map.StartAt.Y;
    }

    public Cell CellAt(int x, int y)
    {
        return cells[x, y];
    }

    private bool CanPlayerMove(Point dir)
    {
        bool canMove = false;

        int newX = playerX + dir.X;
        int newY = playerY + dir.Y;

        if (newX > -1 && newX < Map.Width &&
            newY > -1 && newY < Map.Height)
        {
            canMove =
                cells[newX, newY] == Cell.Empty ||
                cells[newX, newY] == Cell.Plate;
        }

        return canMove;
    }

    private bool CanPushObject(Point dir)
    {
        bool canPush = false;

        int newX = playerX + dir.X;
        int newY = playerY + dir.Y;

        int nextX = newX + dir.X;
        int nextY = newY + dir.Y;

        if (nextX > -1 && nextX < Map.Width &&
            nextY > -1 && nextY < Map.Height)
        {
            canPush =
                (cells[newX, newY] != Cell.Wall)
                &&
                (cells[nextX, nextY] == Cell.Empty
                    || cells[nextX, nextY] == Cell.Plate);
        }

        return canPush;
    }

    private WhatsUp MoveObjectAbsolute(Point from, Point to)
    {
        WhatsUp result = WhatsUp.Move;
        movements++;

        int fromX = from.X;
        int fromY = from.Y;

        Cell cfrom = cells[fromX, fromY];
        if (cfrom != Cell.BarrelOnPlate)
            cells[fromX, fromY] = Cell.Empty;
        else
        {
            cfrom = Cell.Barrel;
            cells[fromX, fromY] = Cell.Plate;
            inPlace--;
        }

        int toX = to.X;
        int toY = to.Y;

        if (cells[toX, toY] != Cell.Plate)
            cells[toX, toY] = cfrom;
        else
        {
            cells[toX, toY] = Cell.BarrelOnPlate;
            inPlace++;
            result = WhatsUp.InPlace;
        }

        CellsChanged.Add(new Point(fromX, fromY));
        CellsChanged.Add(new Point(toX, toY));

        return result;
    }

    private WhatsUp MoveObjectRelative(Point dir)
    {
        Point from = new(
            playerX + dir.X,
            playerY + dir.Y);

        Point to = new(
            from.X + dir.X,
            from.Y + dir.Y);

        return MoveObjectAbsolute(from, to);
    }

    public WhatsUp MovePlayer(Point dir)
    {
        return MovePlayer(dir, false);
    }

    private WhatsUp MovePlayer(Point dir, bool undoMove)
    {
        CellsChanged.Clear();
        WhatsUp result = WhatsUp.Nothing;
        Action act = new();

        CellsChanged.Add(new Point(playerX, playerY));
        if (CanPlayerMove(dir))
        {
            playerX += dir.X;
            playerY += dir.Y;
            CellsChanged.Add(new Point(playerX, playerY));
            act.PlayerMove = dir;

            steps++;
            result = WhatsUp.Step;
        }
        else
        {
            if (CanPushObject(dir))
            {
                result = MoveObjectRelative(dir);

                CellsChanged.Add(new Point(playerX, playerY));
                playerX += dir.X;
                playerY += dir.Y;
                CellsChanged.Add(new Point(playerX, playerY));
                act.PlayerMove = dir;
                act.IsBarrelMovedToo = true;

                steps++;
            }
        }

        if (!undoMove && !act.IsEmpty)
            history.Push(act);

        if (dir.X != 0)
            playerDir.X = dir.X;

        if (dir.Y != 0)
            playerDir.Y = dir.Y;

        if (inPlace == Map.Plates ||
            inPlace == Map.Barrels)
        {
            result = WhatsUp.Win;
        }

        return result;
    }

    public WhatsUp Undo()
    {
        if (history.Count == 0)
            return WhatsUp.Nothing;

        Action act = history.Pop();

        MovePlayer(
            new Point(-act.PlayerMove.X, -act.PlayerMove.Y),
            true);

        if (act.IsBarrelMovedToo)
            MoveObjectAbsolute(
                new Point(playerX + act.PlayerMove.X * 2, playerY + act.PlayerMove.Y * 2),
                new Point(playerX + act.PlayerMove.X, PlayerY + act.PlayerMove.Y));

        return WhatsUp.Undo;
    }

    private class Action
    {
        public Point PlayerMove = Point.Empty;
        public bool IsBarrelMovedToo = false;

        public bool IsEmpty => PlayerMove == Point.Empty;
    }

}
