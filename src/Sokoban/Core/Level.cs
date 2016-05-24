using System;
using System.Drawing;
using System.Linq;

namespace Sokoban
{
    public class Level
    {
        public readonly string Name = "";
        public readonly Cell[,] Cells = null;
        public readonly Point StartAt = Point.Empty;
        public readonly int CellsHx = 0;
        public readonly int CellsVy = 0;
        public readonly int TotalPlates = 0;
        public readonly int InPlace = 0;

        public Level(string name, string rawMap)
        {
            Name = name;

            string[] lines = rawMap.Split('\n');
            CellsVy = lines.Length - 1;
            if (CellsVy < 1)
                return;

            CellsHx = lines.OrderByDescending(s => s.Length).First().Length - 1;
            Cells = new Cell[CellsHx, CellsVy];

            int x, y = 0;
            foreach (string line in lines)
            {
                if (y >= CellsVy)
                    break;

                x = 0;
                char[] chars = line.ToCharArray();
                foreach(char ch in chars)
                {
                    if (x >= CellsHx)
                        break;

                    bool strangeCell = false;

                    switch (ch)
                    {
                        //empty
                        case '_': 
                        case ' ':
                            Cells[x, y] = Cell.Empty;
                            break;
                        // wall or bricks
                        case '#':
                            Cells[x, y] = Cell.Wall;
                            break;
                        // barrel or box
                        case '$':
                            Cells[x, y] = Cell.Barrel;
                            break;
                        // plate or target
                        case '.':
                            TotalPlates++;
                            Cells[x, y] = Cell.Plate;
                            break;
                        // barrel on plate, box at the right place
                        case '*':
                            TotalPlates++;
                            InPlace++;
                            Cells[x, y] = Cell.BarrelOnPlate;
                            break;
                        // player starts here
                        case '@':
                            Cells[x, y] = Cell.Empty;
                            StartAt = new Point(x, y);
                            break;
                        // player starts here and he is over the plate
                        case '+':
                            TotalPlates++;
                            Cells[x, y] = Cell.Plate;
                            StartAt = new Point(x, y);
                            break;
                        default:
                            strangeCell = true;
                            break;
                    } // switch ch

                    // we can write letters
                    if (strangeCell)
                    {
                        byte b = Convert.ToByte(ch);
                        Cells[x, y] = (Cell)b;
                    }

                    x++;
                } // foreach chars

                y++;
            } // foreach lines
        } // constructor
    } // class 
}
