using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cave_Adventure
{
    public class ArenaPanel : Panel
    {
        private const int ShiftFromUpAndDownBorder = 10;
        
        private readonly ArenaPainter _arenaPainter;
        private readonly PlayerPainter _playerPainter;
        private int _zoomScale;
        private Player _player;
        private PointF _logicalCenterPos;

        public ArenaPanel(ArenaPainter arenaPainter, PlayerPainter playerPainter)
        {
            _arenaPainter = arenaPainter;
            _playerPainter = playerPainter;
            _player = new Player(Point.Empty);
        }
        
        protected override void InitLayout()
        {
            base.InitLayout();
            ResizeRedraw = true;
            DoubleBuffered = true;
        }
        
        public new void Update()
        {
            if (_player.IsMoving)
                _player.UpdatePosition();

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