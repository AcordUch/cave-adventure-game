using System.Drawing;

namespace Cave_Adventure
{
    public class Monster: IMonster
    {
        private Point _position;
        private int _dX;
        private int _dY;

        public StatesOfAnimation CurrentStates { get; private set; } = StatesOfAnimation.Idle;
        public ViewDirection ViewDirection { get; set; } = ViewDirection.Right;
        public bool IsMoving { get; set; }
        public bool IsSelected { get; set; }
        public int AnimStage { get; set; }
        public Point TargetPoint { get; }
        public double Health { get; }
        public int AP { get; }
        public double Attack { get; }
        public double Defense { get; }
        public double Damage { get; }

        public Point Position
        {
            get => _position;
            set => _position = value;
        }

        public Monster(Point position)
        {
            _position = position;
        }

        public void UpdatePosition()
        {
            _position.X += _dX;
            _position.Y += _dY;
        }

        public void UpdatePosition2(Point point)
        {
            _position = point;
        }

        public void Move(int dx, int dy)
        {
            _dX = dx;
            _dY = dy;
        }

        public void MoveToPoint(Point point)
        {
            _position = point;
        }

        public void SetAnimationConfiguration(StatesOfAnimation currentAnimation)
        {
            CurrentStates = currentAnimation;
            IsMoving = currentAnimation == StatesOfAnimation.Run;
        }
    }
}