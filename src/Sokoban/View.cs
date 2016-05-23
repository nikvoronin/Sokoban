using System;
using System.Drawing;

namespace Sokoban
{
    public class View
    {
        const int SPRITES_COUNT = 6;

        Bitmap screen = null;
        Bitmap sprites = null;
        Graphics g = null;
        Font font = null;
        int zoom = 20;
        public int Zoom { get { return zoom; } }
        readonly Warehouse warehouse;
        public Warehouse GetWarehouse() { return warehouse; }

        public Image Canvas { get { return screen; } }        
        public int Width { get { return screen.Width; } }
        public int Height { get { return screen.Height; } }

        public View(Warehouse warehouse)
        {
            this.warehouse = warehouse;
        }

        public void Resize(int zoom)
        {
            if (screen != null)
            {
                screen.Dispose();
                sprites.Dispose();
            }

            this.zoom = zoom < 10 ? 10 : zoom;
            int borderW = zoom / 2;
            int w = warehouse.Level.CellsHx * zoom + borderW;
            int h = warehouse.Level.CellsVy * zoom + borderW;
            screen = new Bitmap(w, h);

            font = new Font(SystemFonts.DefaultFont.FontFamily, zoom, GraphicsUnit.Pixel);

            if (g != null)
            {
                g.Dispose();
            }
            g = Graphics.FromImage(screen);

            GenerateSprites();
        }

        private void GenerateSprites()
        {
            sprites = new Bitmap(zoom * SPRITES_COUNT, zoom);
            Graphics gs = Graphics.FromImage(sprites);

            // empty
            int sx = 0;
            gs.FillRectangle(Brushes.Black, sx, 0, zoom, zoom);

            // wall
            sx += zoom;
            int dd = zoom / 15 | 1;
            gs.FillRectangle(Brushes.Red, sx, 0, zoom, zoom);
            Pen widePen = new Pen(Brushes.DarkRed, dd);
            gs.DrawLine(widePen, sx, 0, sx + zoom, 0);
            gs.DrawLine(widePen, sx, zoom / 2, sx + zoom, zoom / 2);

            gs.DrawLine(widePen, sx + zoom / 4, 0, sx + zoom / 4, zoom / 2);
            gs.DrawLine(widePen, sx + zoom / 4 * 3, 0, sx + zoom / 4 * 3, zoom / 2);

            gs.DrawLine(widePen, sx + zoom / 2, zoom / 2, sx + zoom / 2, zoom);

            // barrel
            sx += zoom;
            gs.FillRectangle(Brushes.DarkGoldenrod, sx + 1, 1, zoom - 3, zoom - 3);
            gs.DrawRectangle(Pens.Yellow, sx + 1, 1, zoom - 3, zoom - 3);

            // plate
            sx += zoom;
            gs.FillRectangle(Brushes.Black, sx, 0, zoom, zoom);
            gs.FillPolygon(Brushes.DarkOrange, new Point[]
            {
                new Point (sx, 0 ),
                new Point (sx + zoom / 4, 0 ),
                new Point (sx, zoom / 4 ),
            });
            gs.FillPolygon(Brushes.DarkOrange, new Point[]
            {
                new Point (sx + zoom, zoom ),
                new Point (sx + zoom, zoom - zoom / 4 ),
                new Point (sx + zoom - zoom / 4, zoom ),
            });
            gs.FillPolygon(Brushes.DarkOrange, new Point[]
            {
                new Point (sx, zoom ),
                new Point (sx, zoom - zoom / 4 ),
                new Point (sx + zoom - zoom / 4, 0 ),
                new Point (sx + zoom, 0 ),
                new Point (sx + zoom, zoom / 4 ),
                new Point (sx + zoom / 4, zoom )
            });

            // barrel on plate
            sx += zoom;
            gs.FillRectangle(Brushes.DarkGoldenrod, sx + 1, 1, zoom - 3, zoom - 3);
            gs.DrawRectangle(Pens.Yellow, sx + 1, 1, zoom - 3, zoom - 3);
            int d = zoom / 10 | 1;
            Pen fatPen = new Pen(Brushes.Gold, d);
            gs.DrawLine(fatPen, sx + 2, 2, sx + zoom - 3, zoom - 3);
            gs.DrawLine(fatPen, sx + 2, zoom - 3, sx + zoom - 3, 2);

            // player
            sx += zoom;
            gs.FillRectangle(Brushes.Blue, sx, 0, zoom, zoom);
        }

        public void Update()
        {
            if (g == null) { return; }

            g.Clear(Color.FromArgb(0, 0, 40));

            int shift = zoom / 4;
            Rectangle srcRect = new Rectangle(0, 0, zoom, zoom);
            SolidBrush shadeBrush = new SolidBrush(Color.FromArgb(200, Color.Black));
            int chx = warehouse.Level.CellsHx;
            int cvy = warehouse.Level.CellsVy;
            for (int vy = 0; vy < cvy; vy++)
            {
                for (int hx = 0; hx < chx; hx++)
                {
                    Cells cell = warehouse.Cell(hx, vy);

                    switch (cell)
                    {
                        case Cells.Empty:
                            srcRect.X = 0;
                            break;

                        case Cells.Wall:
                            // shadow
                            g.FillRectangle(shadeBrush, shift + hx * zoom + zoom / 5, shift + vy * zoom + zoom / 5, zoom, zoom);

                            // wall
                            srcRect.X = zoom;
                            g.DrawImage(sprites, shift + hx * zoom, shift + vy * zoom, srcRect, GraphicsUnit.Pixel);
                            break;

                        case Cells.Barrel:
                            srcRect.X = zoom * 2;
                            g.DrawImage(sprites, shift + hx * zoom, shift + vy * zoom, srcRect, GraphicsUnit.Pixel);
                            break;

                        case Cells.Plate:
                            srcRect.X = zoom * 3;
                            g.DrawImage(sprites, shift + hx * zoom, shift + vy * zoom, srcRect, GraphicsUnit.Pixel);
                            break;

                        case Cells.BarrelOnPlate:
                            // plate
                            srcRect.X = zoom * 3;
                            g.DrawImage(sprites, shift + hx * zoom, shift + vy * zoom, srcRect, GraphicsUnit.Pixel);

                            // barrel on plate
                            srcRect.X = zoom * 4;
                            g.DrawImage(sprites, shift + hx * zoom, shift + vy * zoom, srcRect, GraphicsUnit.Pixel);
                            break;
                    } // switch

                    // letters
                    if ((byte)cell > 47 && (byte)cell < 127)
                    {
                        string str = "" + Convert.ToChar(cell);
                        g.DrawString(str, font, Brushes.White, shift + hx * zoom, shift + vy * zoom);
                    }
                } // for hx
            } // for vy

            // draw player
            srcRect.X = zoom * 5;
            g.DrawImage(
                sprites,
                shift + warehouse.PlayerX * zoom, shift + warehouse.PlayerY * zoom,
                srcRect, GraphicsUnit.Pixel);
        } // Update
    } // class
}
