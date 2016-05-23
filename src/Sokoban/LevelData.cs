using System;
using System.Drawing;
using System.Linq;

namespace Sokoban
{
    public class LevelData
    {
        public readonly string Name = "";
        public readonly Cells[,] Map = null;
        public readonly Point StartAt = Point.Empty;
        public readonly int CellsHx = 0;
        public readonly int CellsVy = 0;
        public readonly int TotalPlates = 0;
        public readonly int InPlace = 0;

        public LevelData(string name, string rawMap)
        {
            Name = name;

            string[] lines = rawMap.Split('\n');
            CellsVy = lines.Length - 1;
            if (CellsVy < 1)
            {
                return;
            }
            CellsHx = lines.OrderByDescending(s => s.Length).First().Length - 1;
            Map = new Cells[CellsHx, CellsVy];

            int x, y = 0;
            foreach (string line in lines)
            {
                if (y >= CellsVy)
                {
                    break;
                }

                x = 0;
                char[] chars = line.ToLower().ToCharArray();
                foreach(char ch in chars)
                {
                    if (x >= CellsHx)
                        break;

                    switch (ch)
                    {
                        //empty
                        case '_': 
                        case ' ':
                            Map[x, y] = Cells.Empty;
                            break;
                        // wall or bricks
                        case '#':
                            Map[x, y] = Cells.Wall;
                            break;
                        // barrel or box
                        case '$':
                            Map[x, y] = Cells.Barrel;
                            break;
                        // plate or target
                        case '.':
                            TotalPlates++;
                            Map[x, y] = Cells.Plate;
                            break;
                        // barrel on plate, box at the right place
                        case '*':
                            TotalPlates++;
                            InPlace++;
                            Map[x, y] = Cells.BarrelOnPlate;
                            break;
                        // player starts here
                        case '@':
                            Map[x, y] = Cells.Empty;
                            StartAt = new Point(x, y);
                            break;
                        // player starts here and he is over the plate
                        case '+':
                            TotalPlates++;
                            Map[x, y] = Cells.Plate;
                            StartAt = new Point(x, y);
                            break;
                    } // switch ch

                    // we can write letters
                    if (char.IsLetterOrDigit(ch))
                        Map[x, y] = (Cells)Convert.ToByte(ch);

                    x++;
                } // foreach chars

                y++;
            } // foreach lines
        } // constructor
    } // class 
}
