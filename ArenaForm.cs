using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cave_Adventure
{
    public partial class ArenaForm : Form
    {
        private const int ZoomScale = 32;
        
        private readonly Timer _timer;
        private ArenaPainter _painter;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
            WindowState = FormWindowState.Maximized;
            Text = "Здесь должны быть бои!";
        }
        
        public ArenaForm()
        {
            // InitializeComponent();
            var levels = LoadLevels().ToArray();
            _painter = new ArenaPainter(levels);
        }

        private static IEnumerable<ArenaMap> LoadLevels()
        {
            yield return ArenaMap.CreatNewArenaMap("# .");
            //TODO
        }

        private Point GetShift()
        {
            return new Point(ClientSize.Width / 3, ClientSize.Height / 30);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(Color.Black);
            var shift = GetShift();
            e.Graphics.TranslateTransform(shift.X, shift.Y);
            e.Graphics.ScaleTransform(ZoomScale, ZoomScale);
            _painter.Paint(e.Graphics);
        }
    }
}