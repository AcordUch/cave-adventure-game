using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Cave_Adventure.Properties;

namespace Cave_Adventure
{
    public partial class ArenaForm : Form
    {
        private const int ShiftFromUpAndDownBorder = 10;
        
        private readonly Timer _timer;
        private readonly ArenaPainter _arenaPainter;
        private readonly PlayerPainter _playerPainter;
        private int _zoomScale;
        private Player _player;
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
            _arenaPainter = new ArenaPainter(levels);
            _playerPainter = new PlayerPainter();
            
            _timer = new Timer { Interval = 60 };
            _timer.Tick += Update;
            _timer.Start();
        }

        private void Init()
        {
            _player = new Player(Point.Empty);
        }
        
        private void Update(object sender, EventArgs e)
        {
            if (_player.IsMoving)
                _player.Move();

            Invalidate();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            _player.DirX = 0;
            _player.DirY = 0;
            _player.SetAnimationConfiguration(StatesOfAnimation.Idle);
        }

        private void OnPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    _player.DirY = -5;
                    _player.SetAnimationConfiguration(StatesOfAnimation.Run);
                    break;
                case Keys.S:
                    _player.DirY = 5;
                    _player.SetAnimationConfiguration(StatesOfAnimation.Run);
                    break;
                case Keys.A:
                    _player.DirX = -5;
                    _player.SetAnimationConfiguration(StatesOfAnimation.Run);
                    _player.ViewDirection = ViewDirection.Left;
                    break;
                case Keys.D:
                    _player.DirX = 5;
                    _player.SetAnimationConfiguration(StatesOfAnimation.Run);
                    _player.ViewDirection = ViewDirection.Right;
                    break;
                case Keys.Space:
                    _player.SetAnimationConfiguration(StatesOfAnimation.Attack);
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
            var sceneSize = _arenaPainter.ArenaSize;
            
            _zoomScale = ClientSize.Height / sceneSize.Height - ShiftFromUpAndDownBorder;
            _logicalCenterPos = new PointF(sceneSize.Width / 2f, sceneSize.Height / 2f);
            
            var shift = GetShift();
            
            e.Graphics.ResetTransform();
            e.Graphics.TranslateTransform(shift.X, shift.Y);
            e.Graphics.ScaleTransform(_zoomScale, _zoomScale);
            _arenaPainter.Paint(e.Graphics);
            
            e.Graphics.ResetTransform();
            _playerPainter.SetUpAndPaint(e.Graphics, _player);
        }
    }
}