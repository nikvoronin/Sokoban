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
        Cell[,] cells = null;              // current instance of level, editable

        int playerHx = 0;
        int playerVy = 0;
        Point playerDir = Point.Empty;

        int steps = 0;
        int movements = 0;
        int inPlace = 0;

        public int Steps    { get { return steps; } }
        public int Movements { get { return movements; } }
        public int InPlace  { get { return inPlace; } }
        public int PlayerHx  { get { return playerHx; } }
        public int PlayerVy  { get { return playerVy; } }
        public Point PlayerDir { get { return playerDir; } }

        public readonly List<Point> CellsChanged = new List<Point>();

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
                    (cells[newX, newY] == Cell.Barrel ||
                     cells[newX, newY] == Cell.BarrelOnPlate ||
                     (byte)cells[newX, newY] > 7)
                    &&
                    (cells[nextX, nextY] == Cell.Empty ||
                     cells[nextX, nextY] == Cell.Plate);
            }

            return canPush;
        }

        private WhatsUp MoveObject(Point dir)
        {
            WhatsUp result = WhatsUp.Move;
            movements++;

            int fromHx = playerHx + dir.X;
            int fromVy = playerVy + dir.Y;

            Cell cfrom = cells[fromHx, fromVy];
            if (cfrom != Cell.BarrelOnPlate)
                cells[fromHx, fromVy] = Cell.Empty;
            else
            {
                cfrom = Cell.Barrel;
                cells[fromHx, fromVy] = Cell.Plate;
                inPlace--;
            }

            int toHx = fromHx + dir.X;
            int toVy = fromVy + dir.Y;

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

        public WhatsUp MovePlayer(Point dir)
        {
            CellsChanged.Clear();
            WhatsUp result = WhatsUp.Nothing;

            CellsChanged.Add(new Point(playerHx, playerVy));
            if (CanPlayerMove(dir))
            {
                playerHx += dir.X;
                playerVy += dir.Y;
                CellsChanged.Add(new Point(playerHx, playerVy));

                steps++;
                result = WhatsUp.Step;
            }
            else
            {
                if (CanPushObject(dir))
                {
                    result = MoveObject(dir);

                    CellsChanged.Add(new Point(playerHx, playerVy));
                    playerHx += dir.X;
                    playerVy += dir.Y;
                    CellsChanged.Add(new Point(playerHx, playerVy));

                    steps++;
                }
            }

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
    }
}
