using System.Drawing;

namespace Cave_Adventure
{
    public class ArenaPainter
    {
        private const int CellWidth = GlobalConst.AssetsSize;
        private const int CellHeight = GlobalConst.AssetsSize;
        
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
            TypeEntity();
            graphics.DrawImage(_arenaImage, new Rectangle(0, 0, ArenaSize.Width * CellWidth,
                                                                        ArenaSize.Height* CellHeight));
        }
        
        private void TypeEntity()
        {
            using (var graphics = Graphics.FromImage(_arenaImage))
            {
                foreach (var monster in currentArena.Monsters)
                {
                    graphics.DrawString("M", new Font(SystemFonts.DefaultFont.FontFamily, 32),
                        Brushes.Black, new Point(monster.Position.X * CellWidth,
                                                    monster.Position.Y * CellHeight));
                }
                graphics.DrawString("P", new Font(SystemFonts.DefaultFont.FontFamily, 32),
                    Brushes.Black, new Point(currentArena.Player.Position.X * CellWidth,
                                                currentArena.Player.Position.Y * CellHeight));
            }
        }

        private void CreateArena()
        {
            _arenaImage = new Bitmap(ArenaSize.Width * CellWidth, ArenaSize.Height * CellHeight);
            using (var graphics = Graphics.FromImage(_arenaImage))
            {
                for (int x = 0; x < ArenaSize.Width; x++)
                for (int y = 0; y < ArenaSize.Height; y++)
                {
                    var rec = new Rectangle(x * CellWidth, y * CellHeight, CellWidth, CellHeight);
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