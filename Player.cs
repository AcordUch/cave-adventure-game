using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace Cave_Adventure
{
    public class Player : IPlayer
    {
        private Point _position;
        private int _dX;
        private int _dY;

        public StatesOfAnimation CurrentStates { get; private set; } = StatesOfAnimation.Idle;
        public ViewDirection ViewDirection { get; set; } = ViewDirection.Right;
        public bool IsSelected { get; set; }
        public int AnimStage { get; set; }
        public bool IsMoving { get; set; }
        public Point TargetPoint { get; private set; }
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

        public Player(Point position)
        {
            _position = position;
            AP = 2;
        }

        public void UpdatePosition()
        {
            _position.X += _dX;
            _position.Y += _dY;
            StopIfInTargetPoint();
        }
        
        public void Move(int dx, int dy)
        {
            _dX = dx;
            _dY = dy;
        }

        public void TeleportToPoint(Point point)
        {
            _position = point;
        }

        public Point GetDeltaPoint()
        {
            return new Point(TargetPoint.X - _position.X, TargetPoint.Y - _position.Y);
        }

        private void StopIfInTargetPoint()
        {
            if (IsMoving && _position == TargetPoint)
            {
                IsMoving = false;
                SetAnimation(StatesOfAnimation.Idle);
            }
        }

        public void SetTargetPoint(Point point)
        {
            if(IsSelected)
            {
                TargetPoint = point;
                IsMoving = true;
                SetAnimation(StatesOfAnimation.Run);
            }
        }
        
        public void SetAnimation(StatesOfAnimation currentAnimation)
        {
            CurrentStates = currentAnimation;
            IsMoving = currentAnimation == StatesOfAnimation.Run;
        }
    }
}