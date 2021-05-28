using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Cave_Adventure.Properties;

namespace Cave_Adventure
{
    public class ArenaPainter
    {
        private const int CellWidth = GlobalConst.AssetsSize;
        private const int CellHeight = GlobalConst.AssetsSize;
        
        private ArenaMap _currentArena;
        private Bitmap _arenaImage;
        private Dictionary<Point, Rectangle> _pointToRectangle;
        private bool _configured = false;
        private bool _debugMode = false;
        private bool _simpleMode = false;
        
        public Size ArenaSize => new Size(_currentArena.Width, _currentArena.Height);
        
        public ArenaPainter()
        {
        }
        
        public void Configure(ArenaMap arena, Dictionary<Point, Rectangle> pointToRectangle)
        {
            ChangeLevel(arena, pointToRectangle);
            _configured = true;
        }

        public void Drop()
        {
            _configured = false;
        }
        
        public void Update()
        {
            CreateArena();
        }
        
        public void ChangeLevel(ArenaMap newArena, Dictionary<Point, Rectangle> pointToRectangle)
        {
            _currentArena = newArena;
            _pointToRectangle = pointToRectangle;
            CreateArena();
        }
        
        public void Paint(Graphics graphics)
        {
            if(!_configured)
                return;
            
            if(_debugMode)
                TypeEntity();
            if(_currentArena.PlayerSelected)
                PaintPath();
            graphics.DrawImage(_arenaImage, new Rectangle(0, 0, ArenaSize.Width * GlobalConst.AssetsSize,
                                                                        ArenaSize.Height * GlobalConst.AssetsSize));
        }
        
        private void TypeEntity()
        {
            using (var graphics = Graphics.FromImage(_arenaImage))
            {
                foreach (var monster in _currentArena.Monsters)
                {
                    var label = "";
                    switch (monster.Tag)
                    {
                        case EntityType.Snake:
                            label = "Sn";
                            break;
                        case EntityType.Spider:
                            label = "Sp";
                            break;
                    }
                    graphics.DrawString(label, new Font(SystemFonts.DefaultFont.FontFamily, 22),
                        Brushes.Black, new Point(monster.Position.X * CellWidth,
                            monster.Position.Y * CellHeight));
                }
                graphics.DrawString(_currentArena.Player.IsSelected ? "P!" : "P", new Font(SystemFonts.DefaultFont.FontFamily, 30),
                    Brushes.Black, new Point(_currentArena.Player.Position.X * CellWidth,
                        _currentArena.Player.Position.Y * CellHeight));
            }
        }

        private void PaintPath()
        {
            using (var graphics = Graphics.FromImage(_arenaImage))
            {
                foreach (var path in _currentArena.PlayerPaths)
                {
                    var temp = path.Value;
                    var brush = new SolidBrush(Color.FromArgb(25, Color.White));
                    graphics.FillRectangle(brush, temp.X * CellWidth, temp.Y * CellHeight, CellWidth, CellHeight);
                }
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
                    var rec = _pointToRectangle[new Point(x, y)];
                    if(_simpleMode)
                    {
                        graphics.FillRectangle(ChooseBrushForCell(_currentArena.Arena[x, y]), rec);
                        graphics.DrawRectangle(Pens.Black, rec);
                    }
                    else
                        graphics.DrawImage(ChooseImage(_currentArena.Arena[x, y]), rec);
                }
            }
        }

        private static Brush ChooseBrushForCell((CellType cellType, CellSubtype cellSubtype) cell)
        {
            return cell.cellType == CellType.Floor ? Brushes.DimGray : Brushes.Firebrick;
        }

        private static Image ChooseImage((CellType cellType, CellSubtype cellSubtype) cell)
        {
            Bitmap image;
            switch (cell.cellSubtype)
            {
                case CellSubtype.wall0: image = Resources.wall0; break;
                case CellSubtype.wall1: image = Resources.wall1; break;
                case CellSubtype.wall2: image = Resources.wall2; break;
                case CellSubtype.wall3: image = Resources.wall3; break;
                case CellSubtype.wall4: image = Resources.wall4; break;
                case CellSubtype.floorStone1: image = Resources.floorStone1; break;
                case CellSubtype.floorStone2: image = Resources.floorStone2; break;
                case CellSubtype.floorStoneBroken: image = Resources.floorStoneBroken; break;
                case CellSubtype.transparent: image = Resources.transparent; break;
                default: image = Resources.noTexture; break;
            }
            return image;
        }

        public void OnDebugChange()
        {
            _debugMode = !_debugMode;
        }

        public void OnSimpleMode()
        {
            _simpleMode = !_simpleMode;
        }
    }
}