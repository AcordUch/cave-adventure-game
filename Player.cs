using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace Cave_Adventure
{
    public class Player
    {
        private const int ImageSize = 33;
        
        private Point _position;
        private int _dX;
        private int _dY;

        public StatesOfAnimation CurrentStates { get; private set; } = StatesOfAnimation.Idle;
        public ViewDirection ViewDirection { get; set; } = ViewDirection.Right;
        public bool IsMoving { get; private set; }
        public bool IsSelected { get; set; }
        public bool IsMovingNow { get; set; }
        public int AnimStage { get; set; } = 0;
        public Point targetPoint { get; private set; }
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
            TempName2();
        }

        public void UpdatePosition3(Point point)
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

        public Point GetDPoint()
        {
            var dPoint = new Point(targetPoint.X - _position.X, targetPoint.Y - _position.Y);
            AnimStage++;
            if (AnimStage == 7)
            {
                AnimStage = 0;
                Move(dPoint.X, dPoint.Y);
                UpdatePosition();
            }
            return dPoint;
        }

        public void TempName2()
        {
            
            if (IsMovingNow && _position == targetPoint)
            {
                IsMovingNow = false;
            }
        }

        public void SetTargetPoint(Point point)
        {
            if(IsSelected)
            {
                targetPoint = point;
                IsMovingNow = true;
            }
        }
        
        public void SetAnimationConfiguration(StatesOfAnimation currentAnimation)
        {
            CurrentStates = currentAnimation;
            IsMoving = currentAnimation == StatesOfAnimation.Run;
        }
    }
}