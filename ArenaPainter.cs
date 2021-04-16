using System.Drawing;

namespace Cave_Adventure
{
    public class ArenaPainter
    {
        public Size ArenaSize => new Size(currentArena.Arena.GetLength(0), currentArena.Arena.GetLength(1));

        private ArenaMap currentArena;
        private Bitmap _arenaImage;

        public ArenaPainter(ArenaMap[] arenas)
        {
            currentArena = arenas[0];
            CreateArena();
        }

        public void Paint(Graphics graphics)
        {
            graphics.DrawImage(_arenaImage, 0, 0, ArenaSize.Width, ArenaSize.Width);
            TypeEntity(graphics);
        }

        private void TypeEntity(Graphics graphics)
        {
            foreach (var monster in currentArena.Monsters)
            {
                var coodr = new Point(monster.Position.X * 64, monster.Position.Y * 64);
                var testPoint = new PointF(50, 50);
                graphics.DrawString("M", new Font(SystemFonts.DefaultFont.FontFamily, 100),
                    Brushes.Black, testPoint);
            }
            graphics.DrawString("P", new Font(SystemFonts.DefaultFont.FontFamily, 32),
                Brushes.Black, new Point(currentArena.Player.Position.X * 64, currentArena.Player.Position.Y * 64));
        }

        private void CreateArena()
        {
            const int cellWidth = 64;
            const int cellHeight = 64;
            _arenaImage = new Bitmap(ArenaSize.Width * cellWidth, ArenaSize.Height * cellHeight);
            using (var graphics = Graphics.FromImage(_arenaImage))
            {
                for (int x = 0; x < ArenaSize.Width; x++)
                for (int y = 0; y < ArenaSize.Height; y++)
                {
                    var rec = new Rectangle(x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                    graphics.FillRectangle(ChooseBrushForCell(currentArena.Arena[x, y]), rec);
                    graphics.DrawRectangle(Pens.Black, rec);
                }
            }
        }

        private static Brush ChooseBrushForCell(CellType cell)
        {
            return cell == CellType.Floor ? Brushes.DimGray : Brushes.Firebrick;
        }
    }
}