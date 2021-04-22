using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Cave_Adventure
{
    public partial class ArenaFieldControl : UserControl
    {
        private const int ShiftFromUpAndDownBorder = 10;
        private const int CellWidth = GlobalConst.AssetsSize;
        private const int CellHeight = GlobalConst.AssetsSize;
        
        private int _zoomScale;
        private PointF _logicalCenterPos;
        private ArenaMap _arenaMap;
        private readonly ArenaPainter _arenaPainter;
        private readonly PlayerPainter _playerPainter;
        private Player _player;
        private bool _configured = false;
        private Dictionary<Point, Rectangle> _pointToRectangle;
        
        public ArenaFieldControl(ArenaMap[] levels)
        {
            InitializeComponent();
            DoubleBuffered = true;
            _arenaPainter = new ArenaPainter(levels[0]);
            _playerPainter = new PlayerPainter();
            _player = new Player(new Point(levels[0].Player.Position.X * GlobalConst.AssetsSize,
                                                    levels[0].Player.Position.Y * GlobalConst.AssetsSize));
            // Resize += HandleResize;
            // Click += HandleClick;
            // DoubleClick += HandleClick;
        }
        
        public event Action<Point, MouseEventArgs> ClickOnPoint;

        public void Configure(ArenaMap arenaMap)
        {
            if (_configured)
                throw new InvalidOperationException();
            
            _arenaMap = arenaMap;
            _pointToRectangle = GeneratePointToRectangle(_arenaMap);
            _arenaPainter.Configure(_pointToRectangle);
            _configured = true;
        }

        public new void Update()
        {
            if (_player.IsMoving)
                _player.UpdatePosition();
            
            Invalidate();
        }
        
        public void ChangeLevel(ArenaMap newMap)
        {
            _arenaMap = newMap;
            _pointToRectangle = GeneratePointToRectangle(_arenaMap);
            _arenaPainter.ChangeLevel(newMap, _pointToRectangle);
            Invalidate();
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            _player.Move(0, 0);
            _player.SetAnimationConfiguration(StatesOfAnimation.Idle);
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    _player.Move(0, -5);
                    _player.SetAnimationConfiguration(StatesOfAnimation.Run);
                    break;
                case Keys.S:
                    _player.Move(0, 5);
                    _player.SetAnimationConfiguration(StatesOfAnimation.Run);
                    break;
                case Keys.A:
                    _player.Move(-5, 0);
                    _player.SetAnimationConfiguration(StatesOfAnimation.Run);
                    _player.ViewDirection = ViewDirection.Left;
                    break;
                case Keys.D:
                    _player.Move(5, 0);
                    _player.SetAnimationConfiguration(StatesOfAnimation.Run);
                    _player.ViewDirection = ViewDirection.Right;
                    break;
                case Keys.Space:
                    _player.SetAnimationConfiguration(StatesOfAnimation.Attack);
                    break;
            }
        }
        
        private static Dictionary<Point, Rectangle> GeneratePointToRectangle(ArenaMap arenaMap)
        {
            var result = new Dictionary<Point, Rectangle>();
            for (int x = 0; x < arenaMap.Width; x++)
            for (int y = 0; y < arenaMap.Height; y++)
            {
                var rec = new Rectangle(x * CellWidth, y * CellHeight, CellWidth, CellHeight);
                result.Add(new Point(x, y), rec);
            }
            return result;
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            if (!_configured)
                return;

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
        
        private PointF GetShift()
        {
            return new PointF(ClientSize.Width / 2f - _logicalCenterPos.X * _zoomScale,
                ClientSize.Height / 2f - _logicalCenterPos.Y * _zoomScale);
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
    }
}