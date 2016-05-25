using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sokoban
{
    public class Logic
    {
        public readonly Level LevelMap = null;  // template of the level
        public readonly DateTime StartTime;
        Cell[,] cells = null;                   // current instance of level, editable

        int playerHx = 0;
        int playerVy = 0;
        Point playerDir = Point.Empty;

        int steps = 0;
        int inPlace = 0;
        int plates = 0;

        public int Plates   { get { return plates; } }
        public int Steps    { get { return steps; } }
        public int InPlace  { get { return inPlace; } }
        public int PlayerHx  { get { return playerHx; } }
        public int PlayerVy  { get { return playerVy; } }
        public Point PlayerDir { get { return playerDir; } }

        public readonly List<Point> CellsChanged = new List<Point>();

        public Logic(Level levelMap)
        {
            StartTime = DateTime.Now;
            steps = 0;
            LevelMap = levelMap;
            cells = (Cell[,])LevelMap.Cells.Clone();
            inPlace = LevelMap.InPlace;
            plates  = LevelMap.TotalPlates;
            playerHx = LevelMap.StartAt.X;
            playerVy = LevelMap.StartAt.Y;
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

            if (newX > -1 && newX < LevelMap.CellsHx &&
                newY > -1 && newY < LevelMap.CellsVy)
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

            if (nextX > -1 && nextX < LevelMap.CellsHx &&
                nextY > -1 && nextY < LevelMap.CellsVy)
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

        private WhatHappend MoveObject(Point dir)
        {
            WhatHappend result = WhatHappend.Move;

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
                result = WhatHappend.InPlace;
            }

            CellsChanged.Add(new Point(fromHx, fromVy));
            CellsChanged.Add(new Point(toHx, toVy));

            return result;
        }

        public string ElapsedTimeLongString
        {
            get
            {
                TimeSpan span = TimeSpan.FromTicks(DateTime.Now.Ticks - StartTime.Ticks);
                return
                    string.Format("{0}{1}:{2}:{3}",
                        span.Days > 0 ? span.Days.ToString() + "d " : "",
                        span.Hours,
                        span.Minutes.ToString("00"),
                        span.Seconds.ToString("00")
                        );
            }
        }

        public WhatHappend MovePlayer(Point dir)
        {
            CellsChanged.Clear();
            WhatHappend result = WhatHappend.Nothing;

            CellsChanged.Add(new Point(playerHx, playerVy));
            if (CanPlayerMove(dir))
            {
                playerHx += dir.X;
                playerVy += dir.Y;
                CellsChanged.Add(new Point(playerHx, playerVy));

                steps++;
                result = WhatHappend.Step;
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

            if (inPlace == plates)
                result = WhatHappend.Win;

            return result;
        }
    }
}
