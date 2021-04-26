using System;
using System.Collections.Generic;
using System.Drawing;

namespace Cave_Adventure
{
    public class ArenaPainter
    {
        private const int CellWidth = GlobalConst.AssetsSize;
        private const int CellHeight = GlobalConst.AssetsSize;
        
        public Size ArenaSize => new Size(_currentArena.Width, _currentArena.Height);

        private ArenaMap _currentArena;
        // private Player _player;
        // private Monster[] _monsters = new Monster[2];
        private Bitmap _arenaImage;
        private Dictionary<Point, Rectangle> _pointToRectangle;
        private bool _configured;
        public ArenaPainter(ArenaMap arena)
        {
            _currentArena = arena;
        }
        
        public void Configure(Dictionary<Point, Rectangle> pointToRectangle)
        {
            if (_configured)
                throw new InvalidOperationException();

            _pointToRectangle = pointToRectangle;
            CreateArena();
            _configured = true;
        }

        // public void SetPlayer(Player player)
        // {
        //     _player = player;
        // }
        //
        // public void SetMonster(Monster[] monsters)
        // {
        //     for (var i = 0; i < monsters.Length; i++)
        //         _monsters[i] = monsters[i];
        // }

        public void Paint(Graphics graphics)
        {
            TypeEntity();
            graphics.DrawImage(_arenaImage, new Rectangle(0, 0, ArenaSize.Width * GlobalConst.AssetsSize,
                                                                        ArenaSize.Height * GlobalConst.AssetsSize));
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
                // if(!_player.IsSelected)
                // {
                //     graphics.DrawString("P", new Font(SystemFonts.DefaultFont.FontFamily, 32),
                //         Brushes.Black, new Point(_player.Position.X * CellWidth,
                //             _player.Position.Y * CellHeight));
                // }
                // else
                // {
                //     graphics.DrawString("P!", new Font(SystemFonts.DefaultFont.FontFamily, 32),
                //         Brushes.Black, new Point(_player.Position.X * CellWidth,
                //             _player.Position.Y * CellHeight));
                // }
                graphics.DrawString(_currentArena.Player.IsSelected ? "P!" : "P", new Font(SystemFonts.DefaultFont.FontFamily, 32),
                    Brushes.Black, new Point(_currentArena.Player.Position.X * CellWidth,
                        _currentArena.Player.Position.Y * CellHeight));
            }
        }
        
        public void ChangeLevel(ArenaMap newArena, Dictionary<Point, Rectangle> pointToRectangle)
        {
            _currentArena = newArena;
            _pointToRectangle = pointToRectangle;
            CreateArena();
        }

        public void Update()
        {
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
                    var rec = _pointToRectangle[new Point(x, y)];
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