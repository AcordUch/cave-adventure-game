using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

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

        public Player Player
        {
            get => _player;
            set => _player = value;
        }

        public ArenaPainter ArenaPainter => _arenaPainter;

        public ArenaMap ArenaMap => _arenaMap;

        public ArenaFieldControl(ArenaMap[] levels)
        {
            InitializeComponent();
            DoubleBuffered = true;
            _arenaPainter = new ArenaPainter(levels[0]);
            _playerPainter = new PlayerPainter();

            Click += HandleClick;
        }
        
        public event Action<Point, MouseEventArgs> ClickOnPoint;

        public void Configure(ArenaMap arenaMap)
        {
            if (_configured)
                throw new InvalidOperationException();
            
            _arenaMap = arenaMap;
            _player = new Player(new Point(_arenaMap.Player.Position.X, _arenaMap.Player.Position.Y));
            _pointToRectangle = GeneratePointToRectangle(this, _arenaMap);
            _arenaPainter.Configure(_pointToRectangle);
            _configured = true;
        }

        public new void Update()
        {
            // if (_player.IsMoving)
            //     _player.UpdatePosition();
            // if(_player.IsMovingNow)
            // {
            //     _player.GetDPoint();
            //     _player.UpdatePosition();
            // }

            // _player = new Player(new Point(_arenaMap.Player.Position.X, _arenaMap.Player.Position.Y));
            // _player.UpdatePosition2(new Point(_arenaMap.Player.Position.X, _arenaMap.Player.Position.Y));
            Invalidate();
        }
        
        protected override void InitLayout()
        {
            base.InitLayout();
            ResizeRedraw = true;
            DoubleBuffered = true;
        }
        
        public void ChangeLevel(ArenaMap newMap)
        {
            _arenaMap = newMap;
            _player = new Player(new Point(_arenaMap.Player.Position.X, _arenaMap.Player.Position.Y));
            _pointToRectangle = GeneratePointToRectangle(this, _arenaMap);
            _arenaPainter.ChangeLevel(newMap, _pointToRectangle);
            Invalidate();
        }

        // public void OnKeyUp(object sender, KeyEventArgs e)
        // {
        //     _player.Move(0, 0);
        //     _player.SetAnimationConfiguration(StatesOfAnimation.Idle);
        // }

        // public void OnKeyDown(object sender, KeyEventArgs e)
        // {
        //     switch (e.KeyCode)
        //     {
        //         case Keys.W:
        //             _player.Move(0, -5);
        //             _player.SetAnimationConfiguration(StatesOfAnimation.Run);
        //             break;
        //         case Keys.S:
        //             _player.Move(0, 5);
        //             _player.SetAnimationConfiguration(StatesOfAnimation.Run);
        //             break;
        //         case Keys.A:
        //             _player.Move(-5, 0);
        //             _player.SetAnimationConfiguration(StatesOfAnimation.Run);
        //             _player.ViewDirection = ViewDirection.Left;
        //             break;
        //         case Keys.D:
        //             _player.Move(5, 0);
        //             _player.SetAnimationConfiguration(StatesOfAnimation.Run);
        //             _player.ViewDirection = ViewDirection.Right;
        //             break;
        //         case Keys.Space:
        //             _player.SetAnimationConfiguration(StatesOfAnimation.Attack);
        //             break;
        //     }
        // }
        
        private void HandleClick(object sender, EventArgs e)
        {
            if (!_configured)
                return;

            var args = e as MouseEventArgs;
            var pairs = _pointToRectangle
                .Where(it => it.Value.Contains(args.Location))
                .ToList();
            if (pairs.Count > 0)
                ClickOnPoint?.Invoke(pairs[0].Key, args);
        }
        
        private static Dictionary<Point, Rectangle> GeneratePointToRectangle(ArenaFieldControl arenaFieldControl, ArenaMap arenaMap)
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
            
            UpdateZoomScale();
            _logicalCenterPos = new PointF(sceneSize.Width / 2f, sceneSize.Height / 2f);
            
            var shift = GetShift();
            
            _arenaPainter.SetPlayer(_player);
            // e.Graphics.ResetTransform();
            // e.Graphics.TranslateTransform(shift.X, shift.Y);
            // e.Graphics.ScaleTransform(_zoomScale, _zoomScale);
            _arenaPainter.Paint(e.Graphics);
            
            e.Graphics.ResetTransform();
            _playerPainter.SetUpAndPaint(e.Graphics, _player);
            _arenaPainter.Update();
        }
        
        private PointF GetShift()
        {
            return new PointF(ClientSize.Width / 2f - _logicalCenterPos.X * _zoomScale,
                ClientSize.Height / 2f - _logicalCenterPos.Y * _zoomScale);
        }
        
        private void UpdateZoomScale()
        {
            _zoomScale = ClientSize.Height / _arenaPainter.ArenaSize.Height - ShiftFromUpAndDownBorder;
        }
        
        private static PointF ToLogical(ArenaFieldControl arenaFieldControl, Point point)
        {
            var shift = arenaFieldControl.GetShift();
            return new PointF(
                (point.X - shift.X) / arenaFieldControl._zoomScale,
                (point.Y - shift.Y) / arenaFieldControl._zoomScale);
        }

        private static Point ToGraphic(ArenaFieldControl arenaFieldControl, Point point)
        {
            var shift = arenaFieldControl.GetShift();
            return new Point(
                (int)(point.X * arenaFieldControl._zoomScale + shift.X),
                (int)(point.Y * arenaFieldControl._zoomScale + shift.Y));
        }
    }
}