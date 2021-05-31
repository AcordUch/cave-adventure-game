using System.Drawing;
using System.Windows.Forms;
using Cave_Adventure.Interfaces;

namespace Cave_Adventure.Views
{
    public class HealBar : Panel
    {
        private class HealBarPainter
        {
            private const int Shift = 10;
            
            private readonly HealBar _healBar;
            private ArenaMap _arenaMap;
            private Bitmap _emptyBar;
            private SizeF _labelSize;
            private int _barHeight = 35;
            private bool _configured = false;
            
            public HealBarPainter(HealBar healBar)
            {
                _healBar = healBar;
                CreateEmptyBar();
            }
            
            public void Configure(ArenaMap arenaMap)
            {
                _arenaMap = arenaMap;
                _configured = true;
            }

            public void Drop()
            {
                _configured = false;
            }

            public void Paint(Graphics g)
            {
                if(_healBar == null || !_configured)
                    return;
                
                _barHeight = (int)(_healBar.Height * 0.45);
                CreateEmptyBar();
                FillBar(g);
                var rec = new Rectangle(Shift / 2, Shift, _healBar.Width - 2 * Shift, _barHeight);
                g.DrawImage(_emptyBar, rec);
            }
            
            private void FillBar(Graphics g)
            {
                var recWidth = (_healBar.Width - Shift) * _arenaMap.Player.Health / GlobalConst.PlayerHP;
                var rec = new Rectangle(Shift, 2 * Shift, (int)recWidth, _barHeight);
                using (var graphics = Graphics.FromImage(_emptyBar))
                {
                    graphics.FillRectangle(Brushes.Red, rec);
                    var emSize = PickEmSize(g);
                    graphics.DrawString($"{_arenaMap.Player.Health} / {GlobalConst.PlayerHP}",
                        new Font(SystemFonts.DefaultFont.FontFamily, emSize),
                        Brushes.Black, 
                        new Point((int)((_healBar.Width - 2 * Shift) / 2 - _labelSize.Width / 2),
                            (int)(_barHeight / 2 - _labelSize.Height / 6)));
                }
            }

            private void CreateEmptyBar()
            {
                _emptyBar = new Bitmap(_healBar.Width, _healBar.Height);
                using (var graphics = Graphics.FromImage(_emptyBar))
                {
                    var rec = new Rectangle(Shift, 2 * Shift, _healBar.Width - Shift, _barHeight);
                    graphics.DrawRectangle(new Pen(Color.Black, 2), rec);
                }
            }

            private float PickEmSize(Graphics g)
            {
                var text = $"{_arenaMap.Player.Health} / {GlobalConst.PlayerHP}";
                var emSize = 150f;
                var font = new Font("Arial", emSize);
                _labelSize = g.MeasureString(text, font);

                while (_labelSize.Width > _barHeight * 2 && emSize > 5)
                {
                    emSize -= 5;
                    font = new Font("Arial", emSize);
                    _labelSize = g.MeasureString(text, font);
                }

                return emSize;
            }
        }
        
        private readonly HealBarPainter _healBarPainter;
        private ArenaMap _arenaMap;
        private bool _configured;

        public HealBar()
        {
            DoubleBuffered = true;
            _healBarPainter = new HealBarPainter(this);
        }
        
        public void Configure(ArenaMap arenaMap)
        {
            _arenaMap = arenaMap;
            _healBarPainter.Configure(_arenaMap);
            _configured = true;
        }

        public void Drop()
        {
            _healBarPainter.Drop();
            _configured = false;
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            if(!_configured)
                return;
            
            _healBarPainter.Paint(e.Graphics);
        }
    }
}