using System;
using System.Drawing;

namespace Sokoban.Core
{
    /// <summary>
    /// Draws levels
    /// </summary>
    public class View : IDisposable
    {
        const int SPRITES_COUNT = 7;
        static FontFamily LETTERS_FONT_FAMILY = SystemFonts.DefaultFont.FontFamily;
        const float LETTERS_FONT_SCALE = 0.9f;

        static Color COLOR_BACKGROUND = Color.FromArgb(0, 0, 40);
        static SolidBrush BRUSH_BACKGROUND = new SolidBrush(COLOR_BACKGROUND);

        Bitmap screen = null;
        Bitmap sprites = null;
        Graphics g = null;
        Font font = null;
        int z;
        int shift = 5;
        public readonly Logic logic;
        public readonly Level Map = null;  // template of the level

        public Image Canvas { get { return screen; } }        
        public int Width { get { return screen.Width; } }
        public int Height { get { return screen.Height; } }

        public View(Level level, Logic logic)
        {
            this.logic = logic;
            Map = level;
        }

        public void Resize(int cellSizePx)
        {
            if (screen != null)
            {
                font?.Dispose();
                screen?.Dispose();
                sprites?.Dispose();
                g?.Dispose();
            }

            z = cellSizePx < 10 ? 10 : cellSizePx;
            shift = z / 4;
            int borderW = z / 2;
            int w = Map.WidthHx * z + borderW;
            int h = Map.HeightVy * z + borderW;
            screen = new Bitmap(w, h);

            font = new Font(LETTERS_FONT_FAMILY, z * LETTERS_FONT_SCALE, GraphicsUnit.Pixel);

            if (g != null)
                g.Dispose();

            g = Graphics.FromImage(screen);

            GenerateSprites();
        }

        private void GenerateSprites()
        {
            sprites = new Bitmap(z * SPRITES_COUNT, z);
            Graphics gs = Graphics.FromImage(sprites);

            // empty
            int sx = 0;
            gs.FillRectangle(BRUSH_BACKGROUND, sx, 0, z, z);

            // wall
            sx += z;
            int dd = (int)(z / 12.5) | 1;
            gs.FillRectangle(Brushes.Red, sx, 0, z, z);
            Pen widePen = new Pen(Brushes.DarkRed, dd);

            gs.DrawLine(widePen, sx, 0, sx + z, 0);
            gs.DrawLine(widePen, sx, z / 2, sx + z, z / 2);

            gs.DrawLine(widePen, sx + z / 4, 0, sx + z / 4, z / 2);
            gs.DrawLine(widePen, sx + z / 4 * 3, 0, sx + z / 4 * 3, z / 2);

            gs.DrawLine(widePen, sx + z / 2, z / 2, sx + z / 2, z);

            // barrel
            sx += z;
            dd = z / 20;
            if (dd < 1)
                dd = 1;
            widePen = new Pen(Brushes.Yellow, dd);
            gs.FillRectangle(Brushes.DarkGoldenrod, sx + dd * 2, dd * 2, z - dd * 4, z - dd * 4);
            gs.DrawRectangle(widePen, sx + dd * 2, dd * 2, z - dd * 4, z - dd * 4);

            // plate
            sx += z;
            gs.FillRectangle(Brushes.Black, sx, 0, z, z);
            gs.FillPolygon(
                Brushes.DarkOrange,
                new Point[]
                {
                    new Point (sx, 0 ),
                    new Point (sx + z / 4, 0 ),
                    new Point (sx, z / 4 ),
                });
            gs.FillPolygon(
                Brushes.DarkOrange,
                new Point[]
                {
                    new Point (sx + z, z ),
                    new Point (sx + z, z - z / 4 ),
                    new Point (sx + z - z / 4, z ),
                });
            gs.FillPolygon(
                Brushes.DarkOrange,
                new Point[]
                {
                    new Point (sx, z ),
                    new Point (sx, z - z / 4 ),
                    new Point (sx + z - z / 4, 0 ),
                    new Point (sx + z, 0 ),
                    new Point (sx + z, z / 4 ),
                    new Point (sx + z / 4, z )
                });

            // barrel on plate
            sx += z;
            gs.FillRectangle(Brushes.DarkGoldenrod, sx + dd * 2, dd * 2, z - dd * 4, z - dd * 4);
            int a = z / 3 + dd;
            int aa = z / 3 * 2;
            gs.DrawLine(widePen, sx + dd * 2, a, sx + z - dd * 2, a);
            gs.DrawLine(widePen, sx + dd * 2, aa, sx + z - dd * 2, aa);
            gs.DrawRectangle(widePen, sx + dd * 2, dd * 2, z - dd * 4, z - dd * 4);

            // player
            sx += z;
            gs.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                // body
            gs.FillEllipse(Brushes.Blue, sx + 2, 2, z - 2, z - 2);
                // tail
            gs.FillEllipse(Brushes.Blue, sx, z - z / 2, z / 2, z / 2);
                // eyes
            int eyesShift = z / 20;
            gs.FillEllipse(Brushes.White, sx + z / 3 + eyesShift, z / 3, z / 5, z / 5);
            gs.FillEllipse(Brushes.White, sx + z / 3 * 2 + eyesShift, z / 3, z / 5, z / 5);

            // flipped player 
            Bitmap mirrored = new Bitmap(z, z, gs);
            Rectangle srcRect = new Rectangle(sx, 0, z, z);
            Graphics gm = Graphics.FromImage(mirrored);
            gm.DrawImage(sprites,
                0, 0,
                srcRect, GraphicsUnit.Pixel);
            mirrored.RotateFlip(RotateFlipType.RotateNoneFlipX);
            sx += z;
            gs.DrawImageUnscaled(mirrored, sx, 0);
        } // GenerateSprites()

        public void DrawCell(int hx, int vy)
        {
            if (g == null) return;

            Rectangle srcRect = new Rectangle(0, 0, z, z);
            Cell cell = logic.CellAt(hx, vy);

            bool strangeCell = false;

            switch (cell)
            {
                case Cell.Empty:
                    srcRect.X = 0;
                    g.DrawImage(sprites, shift + hx * z, shift + vy * z, srcRect, GraphicsUnit.Pixel);
                    break;

                case Cell.Wall:
                    srcRect.X = z;
                    g.DrawImage(sprites, shift + hx * z, shift + vy * z, srcRect, GraphicsUnit.Pixel);
                    break;

                case Cell.Barrel:
                    srcRect.X = 0;
                    g.DrawImage(sprites, shift + hx * z, shift + vy * z, srcRect, GraphicsUnit.Pixel);
                    srcRect.X = z * 2;
                    g.DrawImage(sprites, shift + hx * z, shift + vy * z, srcRect, GraphicsUnit.Pixel);
                    break;

                case Cell.Plate:
                    srcRect.X = z * 3;
                    g.DrawImage(sprites, shift + hx * z, shift + vy * z, srcRect, GraphicsUnit.Pixel);
                    break;

                case Cell.BarrelOnPlate:
                    // plate
                    srcRect.X = z * 3;
                    g.DrawImage(sprites, shift + hx * z, shift + vy * z, srcRect, GraphicsUnit.Pixel);

                    // barrel on plate
                    srcRect.X = z * 4;
                    g.DrawImage(sprites, shift + hx * z, shift + vy * z, srcRect, GraphicsUnit.Pixel);
                    break;
                default:
                    strangeCell = true;
                    break;
            } // switch

            if (strangeCell)
                if ((byte)cell > 7 && (byte)cell < 255)
                {
                    string str = "" + Convert.ToChar(cell);
                    srcRect.X = 0;
                    g.DrawImage(sprites, shift + hx * z, shift + vy * z, srcRect, GraphicsUnit.Pixel);
                    g.DrawString(str, font, Brushes.White, shift + hx * z, shift + vy * z);
                }
        } // DrawCell()

        public void DrawPlayer()
        {
            if (g == null) return;

            DrawCell(logic.PlayerHx, logic.PlayerVy);

            Rectangle srcRect =
                logic.PlayerDir.X > -1 ?
                    new Rectangle(z * 5, 0, z, z) :
                    new Rectangle(z * 6, 0, z, z);

            g.DrawImage(
                sprites,
                shift + logic.PlayerHx * z, shift + logic.PlayerVy * z,
                srcRect, GraphicsUnit.Pixel);
        }

        public void Update()
        {
            if (g == null) return;

            foreach(Point p in logic.CellsChanged)
                DrawCell(p.X, p.Y);

            DrawPlayer();
        }

        public void DrawField()
        {
            if (g == null) return;

            g.Clear(COLOR_BACKGROUND);

            Rectangle srcRect = new Rectangle(0, 0, z, z);
            int chx = Map.WidthHx;
            int cvy = Map.HeightVy;

            for (int vy = 0; vy < cvy; vy++)
                for (int hx = 0; hx < chx; hx++)
                    DrawCell(hx, vy);

            DrawPlayer();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                screen?.Dispose();
                sprites?.Dispose();
                font?.Dispose();
                g?.Dispose();
            }
        }
    } // class
}
