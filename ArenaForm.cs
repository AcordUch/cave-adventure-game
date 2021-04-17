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
        public Image gladiatorImage;
        Player player;

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
            //InitializeComponent();
            Init();
            KeyDown += OnPress;
            KeyUp += OnKeyUp;
            var levels = LoadLevels().ToArray();
            _painter = new ArenaPainter(levels);
            
            _timer = new Timer { Interval = 30 };
            _timer.Tick += Update;
            _timer.Start();
        }
        
        public void Init()
        {
            gladiatorImage = Properties.Resources.Gladiator;
            player = new Player(Point.Empty, Hero.idleFrames, Hero.runFrames, Hero.attackFrames, Hero.deathFrames, gladiatorImage);
        }
        
        public void Update(object sender, EventArgs e)
        {
            if (player.isMoving)
                player.Move();

            Invalidate();
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            player.DirX = 0;
            player.DirY = 0;
            player.isMoving = false;
            player.SetAnimationConfiguration(0);
        }

        public void OnPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.DirY = -5;
                    player.isMoving = true;
                    player.SetAnimationConfiguration(1);
                    break;
                case Keys.S:
                    player.DirY = 5;
                    player.isMoving = true;
                    player.SetAnimationConfiguration(1);
                    break;
                case Keys.A:
                    player.DirX = -5;
                    player.isMoving = true;
                    player.SetAnimationConfiguration(1);
                    player.Flip = -1;
                    break;
                case Keys.D:
                    player.DirX = 5;
                    player.isMoving = true;
                    player.SetAnimationConfiguration(1);
                    player.Flip = 1;
                    break;
                case Keys.Space:
                    player.DirX = 0;
                    player.DirY = 0;
                    player.isMoving = false;
                    player.SetAnimationConfiguration(2);
                    break;
            }

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
            
            e.Graphics.ResetTransform();
            player.PlayAnimation(e.Graphics);
        }
    }
}