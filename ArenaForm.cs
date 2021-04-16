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
        private int _zoomScale;
        private const int ShiftFromUpAndDownBorder = 10;
        
        private readonly Timer _timer;
        private ArenaPainter _painter;
        private PointF _logicalCenterPos;

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
            yield return ArenaMap.CreatNewArenaMap(Properties.Resources.Arena1);
            //TODO
        }
        
        private PointF ToLogical(Point point)
        {
            var shift = GetShift();
            return new PointF(
                (point.X - shift.X) / _zoomScale,
                (point.Y - shift.Y) / _zoomScale);
        }

        private PointF ToGraphic(Point point)
        {
            var shift = GetShift();
            return new PointF(
                point.X * _zoomScale + shift.X,
                point.Y * _zoomScale + shift.Y);
        }

        private PointF GetShift()
        {
            return new PointF(ClientSize.Width / 2f - _logicalCenterPos.X * _zoomScale,
                ClientSize.Height / 2f - _logicalCenterPos.Y * _zoomScale);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(Color.White);
            var sceneSize = _painter.ArenaSize;
            
            _zoomScale = ClientSize.Height / sceneSize.Height - ShiftFromUpAndDownBorder;
            _logicalCenterPos = new PointF(sceneSize.Width / 2f, sceneSize.Height / 2f);
            
            var shift = GetShift();
            
            e.Graphics.ResetTransform();
            e.Graphics.TranslateTransform(shift.X, shift.Y);
            e.Graphics.ScaleTransform(_zoomScale, _zoomScale);
            _painter.Paint(e.Graphics);
        }
    }
}