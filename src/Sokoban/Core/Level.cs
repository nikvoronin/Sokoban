using System;
using System.Drawing;
using System.Linq;

namespace Sokoban.Core
{
    public class Level
    {
        public readonly string Name = "";
        public readonly Cell[,] Cells = null;
        public readonly Point StartAt = Point.Empty;
        public readonly int WidthHx = 0;
        public readonly int HeightVy = 0;
        public readonly int Plates = 0;
        public readonly int Barrels = 0;
        public readonly int InPlace = 0;

        public Level(string name, string rawMap)
        {
            Name = name;

            string[] lines = rawMap.Split('\n');
            HeightVy = lines.Length - 1;
            if (HeightVy < 1)
                return;

            WidthHx = lines.OrderByDescending(s => s.Length).First().Length - 1;
            Cells = new Cell[WidthHx, HeightVy];

            int x, y = 0;
            foreach (string line in lines)
            {
                if (y >= HeightVy)
                    break;

                x = 0;
                char[] chars = line.ToCharArray();
                foreach(char ch in chars)
                {
                    if (x >= WidthHx)
                        break;

                    bool strangeCell = false;

                    Cells[x, y] = Cell.Empty;
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
                            Barrels++;
                            Cells[x, y] = Cell.Barrel;
                            break;
                        // plate or target
                        case '.':
                            Plates++;
                            Cells[x, y] = Cell.Plate;
                            break;
                        // barrel on plate, box at the right place
                        case '*':
                            Plates++;
                            InPlace++;
                            Barrels++;
                            Cells[x, y] = Cell.BarrelOnPlate;
                            break;
                        // player starts here
                        case '@':
                            Cells[x, y] = Cell.Empty;
                            StartAt = new Point(x, y);
                            break;
                        // player starts here and he is over the plate
                        case '+':
                            Plates++;
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
        } // ctor

        public override string ToString()
        {
            return Name;
        }
    } // class 
}
