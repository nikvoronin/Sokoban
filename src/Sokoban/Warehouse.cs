using System.Drawing;

namespace Sokoban
{
    public class Warehouse
    {
        public readonly LevelData Level = null; // template of the level
        Cells[,] map = null;                        // current instance of level, editable
        int playerX = 0;
        int playerY = 0;
        int steps = 0;
        int inPlace = 0;
        int plates = 0;

        public int Plates   { get { return plates; } }
        public int Steps    { get { return steps; } }
        public int InPlace  { get { return inPlace; } }
        public int PlayerX  { get { return playerX; } }
        public int PlayerY  { get { return playerY; } }

        public Warehouse(LevelData level)
        {
            steps = 0;
            Level = level;
            map = (Cells[,])Level.Map.Clone();
            inPlace = Level.InPlace;
            plates  = Level.TotalPlates;
            playerX = Level.StartAt.X;
            playerY = Level.StartAt.Y;
        }

        public Cells Cell(int hx, int vy)
        {
            return map[hx, vy];
        }

        private bool CanPlayerMove(Point dir)
        {
            bool canMove = false;

            int newX = playerX + dir.X;
            int newY = playerY + dir.Y;

            if (newX > -1 && newX < Level.CellsHx &&
                newY > -1 && newY < Level.CellsVy)
            {
                canMove =
                    map[newX, newY] == Cells.Empty ||
                    map[newX, newY] == Cells.Plate;
            }

            return canMove;
        }

        private bool CanPlayerKickBarrel(Point dir)
        {
            bool canKick = false;

            int newX = playerX + dir.X;
            int newY = playerY + dir.Y;

            int nextX = newX + dir.X;
            int nextY = newY + dir.Y;

            if (nextX > -1 && nextX < Level.CellsHx &&
                nextY > -1 && nextY < Level.CellsVy)
            {
                canKick =
                    (map[newX, newY] == Cells.Barrel ||
                     map[newX, newY] == Cells.BarrelOnPlate)
                    &&
                    (map[nextX, nextY] == Cells.Empty ||
                     map[nextX, nextY] == Cells.Plate);
            }

            return canKick;
        }

        private WhatHappend MoveBarrel(Point dir)
        {
            WhatHappend result = WhatHappend.Move;

            int fromX = playerX + dir.X;
            int fromY = playerY + dir.Y;

            if (map[fromX, fromY] == Cells.BarrelOnPlate)
            {
                map[fromX, fromY] = Cells.Plate;
                inPlace--;
            }
            else
            {
                map[fromX, fromY] = Cells.Empty;
            }

            int toX = fromX + dir.X;
            int toY = fromY + dir.Y;

            if (map[toX, toY] == Cells.Plate)
            {
                map[toX, toY] = Cells.BarrelOnPlate;
                inPlace++;
                result = WhatHappend.InPlace;
            }
            else
            {
                map[toX, toY] = Cells.Barrel;
            }

            return result;
        }

        public WhatHappend MovePlayer(Point dir)
        {
            WhatHappend result = WhatHappend.Nothing;

            if (CanPlayerMove(dir))
            {
                playerX += dir.X;
                playerY += dir.Y;

                steps++;
                result = WhatHappend.Walk;
            }
            else
            {
                if (CanPlayerKickBarrel(dir))
                {
                    result = MoveBarrel(dir);

                    playerX += dir.X;
                    playerY += dir.Y;

                    steps++;
                }
            }

            if (inPlace == plates)
                result = WhatHappend.Win;

            return result;
        }

        public enum WhatHappend
        {
            Nothing,
            BarrelOnPlate,
            Win,
            Walk,
            Move,
            InPlace
        }
    }
}
