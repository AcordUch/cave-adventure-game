using System.Drawing;

namespace Cave_Adventure
{
    public class ArenaPainter
    {
        private const int CellWidth = GlobalConst.AssetsSize;
        private const int CellHeight = GlobalConst.AssetsSize;
        
        public Size ArenaSize => new Size(_currentArena.Arena.GetLength(0), _currentArena.Arena.GetLength(1));

        private ArenaMap _currentArena;
        private Bitmap _arenaImage;

        public ArenaPainter(ArenaMap arena)
        {
            _currentArena = arena;
            CreateArena();
        }

        public void Paint(Graphics graphics)
        {
            TypeEntity();
            graphics.DrawImage(_arenaImage, new Rectangle(0, 0, ArenaSize.Width,
                                                                        ArenaSize.Height));
        }
        
        private void TypeEntity()
        {
            using (var graphics = Graphics.FromImage(_arenaImage))
            {
                foreach (var monster in _currentArena.Monsters)
                {
                    graphics.DrawString("M", new Font(SystemFonts.DefaultFont.FontFamily, 32),
                        Brushes.Black, new Point(monster.Position.X * CellWidth,
                                                    monster.Position.Y * CellHeight));
                }
                graphics.DrawString("P", new Font(SystemFonts.DefaultFont.FontFamily, 32),
                    Brushes.Black, new Point(_currentArena.Player.Position.X * CellWidth,
                                                _currentArena.Player.Position.Y * CellHeight));
            }
        }
        
        public void ChangeLevel(ArenaMap newArena)
        {
            _currentArena = newArena;
            CreateArena();
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
                    graphics.FillRectangle(ChooseBrushForCell(_currentArena.Arena[x, y]), rec);
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