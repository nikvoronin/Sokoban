using System.Collections.Generic;
using System.Drawing;

namespace Sokoban.Core
{
    /// <summary>
    /// Contains and manages data (player, level's cells, statistics)
    /// </summary>
    public class Logic
    {
        public readonly Level Map = null;  // template of the level
        Cell[,] cells = null;              // editable instance of the current level

        int playerHx = 0;
        int playerVy = 0;
        Point playerDir = Point.Empty;

        int steps = 0;
        int movements = 0;
        int inPlace = 0;

        public int Steps        { get { return steps;       } }
        public int Movements    { get { return movements;   } }
        public int InPlace      { get { return inPlace;     } }
        public int PlayerHx     { get { return playerHx;    } }
        public int PlayerVy     { get { return playerVy;    } }
        public Point PlayerDir  { get { return playerDir;   } }

        public readonly List<Point> CellsChanged = new List<Point>();
        private Stack<Action> history = new Stack<Action>();

        public Logic(Level map)
        {
            steps = 0;
            Map = map;
            cells = (Cell[,])Map.Cells.Clone();
            inPlace = Map.InPlace;
            playerHx = Map.StartAt.X;
            playerVy = Map.StartAt.Y;
        }

        public Cell CellAt(int hx, int vy)
        {
            return cells[hx, vy];
        }

        private bool CanPlayerMove(Point dir)
        {
            bool canMove = false;

            int newX = playerHx + dir.X;
            int newY = playerVy + dir.Y;

            if (newX > -1 && newX < Map.WidthHx &&
                newY > -1 && newY < Map.HeightVy)
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

            int newX = playerHx + dir.X;
            int newY = playerVy + dir.Y;

            int nextX = newX + dir.X;
            int nextY = newY + dir.Y;

            if (nextX > -1 && nextX < Map.WidthHx &&
                nextY > -1 && nextY < Map.HeightVy)
            {
                canPush =
                    (cells[newX, newY] != Cell.Wall)
                    &&
                    (cells[nextX, nextY] == Cell.Empty ||
                     cells[nextX, nextY] == Cell.Plate);
            }

            return canPush;
        }

        private WhatsUp MoveObjectAbsolute(Point from, Point to)
        {
            WhatsUp result = WhatsUp.Move;
            movements++;

            int fromHx = from.X;
            int fromVy = from.Y;

            Cell cfrom = cells[fromHx, fromVy];
            if (cfrom != Cell.BarrelOnPlate)
                cells[fromHx, fromVy] = Cell.Empty;
            else
            {
                cfrom = Cell.Barrel;
                cells[fromHx, fromVy] = Cell.Plate;
                inPlace--;
            }

            int toHx = to.X;
            int toVy = to.Y;

            if (cells[toHx, toVy] != Cell.Plate)
                cells[toHx, toVy] = cfrom;
            else
            {
                cells[toHx, toVy] = Cell.BarrelOnPlate;
                inPlace++;
                result = WhatsUp.InPlace;
            }

            CellsChanged.Add(new Point(fromHx, fromVy));
            CellsChanged.Add(new Point(toHx, toVy));

            return result;
        }

        private WhatsUp MoveObjectRelative(Point dir)
        {
            Point from = new Point(
                playerHx + dir.X,
                playerVy + dir.Y);

            Point to = new Point(
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
            Action act = new Action();

            CellsChanged.Add(new Point(playerHx, playerVy));
            if (CanPlayerMove(dir))
            {
                playerHx += dir.X;
                playerVy += dir.Y;
                CellsChanged.Add(new Point(playerHx, playerVy));
                act.PlayerMove = dir;

                steps++;
                result = WhatsUp.Step;
            }
            else
            {
                if (CanPushObject(dir))
                {
                    result = MoveObjectRelative(dir);

                    CellsChanged.Add(new Point(playerHx, playerVy));
                    playerHx += dir.X;
                    playerVy += dir.Y;
                    CellsChanged.Add(new Point(playerHx, playerVy));
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
                G.I.Win();
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
                    new Point(playerHx + act.PlayerMove.X * 2, playerVy + act.PlayerMove.Y * 2),
                    new Point(playerHx + act.PlayerMove.X, PlayerVy + act.PlayerMove.Y));

            return WhatsUp.Undo;
        } // Undo()

        private class Action
        {
            public Point PlayerMove = Point.Empty;
            public bool IsBarrelMovedToo = false;

            public bool IsEmpty { get { return PlayerMove == Point.Empty; } }
        }

    } // class Logic
}
