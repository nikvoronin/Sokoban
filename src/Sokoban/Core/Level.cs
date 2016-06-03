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

                    Cells[x, y] = (Cell)Convert.ToByte(ch);
                    switch (Cells[x, y])
                    {
                        case Cell.EmptySp:
                            Cells[x, y] = Cell.Empty;
                            break;
                        case Cell.Barrel:
                            Barrels++;
                            break;
                        case Cell.Plate:
                            Plates++;
                            break;
                        case Cell.BarrelOnPlate:
                            Plates++;
                            InPlace++;
                            Barrels++;
                            break;
                        // player starts here
                        case Cell.PlayerStartsAt:
                            StartAt = new Point(x, y);
                            Cells[x, y] = Cell.Empty;
                            break;
                        // player starts here and he is over the plate
                        case Cell.PlayerOnPlate:
                            Plates++;
                            Cells[x, y] = Cell.Plate;
                            StartAt = new Point(x, y);
                            break;
                    } // switch ch

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
