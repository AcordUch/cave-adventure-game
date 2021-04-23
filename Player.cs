using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace Cave_Adventure
{
    public class Player
    {
        private Point _position;
        private int _dX;
        private int _dY;

        public StatesOfAnimation CurrentStates { get; private set; } = StatesOfAnimation.Idle;
        public ViewDirection ViewDirection { get; set; } = ViewDirection.Right;
        public bool IsSelected { get; set; }
        public bool IsMovingNow { get; set; }
        public Point TargetPoint { get; private set; }
        public Point Position
        {
            get => _position;
            set => _position = value;
        }

        public Player(Point position)
        {
            _position = position;
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

        public void StopIfInTargetPoint()
        {
            if (IsMovingNow && _position == TargetPoint)
            {
                IsMovingNow = false;
                SetAnimation(StatesOfAnimation.Idle);
            }
        }

        public void SetTargetPoint(Point point)
        {
            if(IsSelected)
            {
                TargetPoint = point;
                IsMovingNow = true;
                SetAnimation(StatesOfAnimation.Run);
            }
        }
        
        public void SetAnimation(StatesOfAnimation currentAnimation)
        {
            CurrentStates = currentAnimation;
            IsMovingNow = currentAnimation == StatesOfAnimation.Run;
        }
    }
}