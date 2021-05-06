using System;
using System.Collections.Generic;
using System.Drawing;

namespace Cave_Adventure
{
    public class Monster2: IEntity
    {
        private Point _position;
        
        public StatesOfAnimation CurrentStates { get; private set; } = StatesOfAnimation.Idle;
        public ViewDirection ViewDirection { get; set; } = ViewDirection.Right;
        public EntityType Tag { get; }
        public bool IsMoving { get; private set; }
        public bool IsSelected { get; set; }
        public Point TargetPoint { get; private set; }
        public double Health { get; }
        public int AP { get; private set; }
        public double Attack { get; }
        public double Defense { get; }
        public double Damage { get; }

        public Point Position
        {
            get => _position;
            set => _position = value;
        }

        public Monster2(Point position, EntityType tag)
        {
            Tag = tag;
            _position = position;
            AP = 2;
        }
        
        public virtual void Attacking()
        {
        }

        #region Moving
        public void Move(int dx, int dy)
        {
            _position.X += dx;
            _position.Y += dy;
            AP--;
            StopIfInTargetPoint();
        }

        public void TeleportToPoint(Point point)
        {
            _position = point;
        }
        
        public void ResetAP()
        {
            AP = 3;
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
        #endregion
        
        private void SetAnimation(StatesOfAnimation currentAnimation)
        {
            CurrentStates = currentAnimation;
            IsMoving = currentAnimation == StatesOfAnimation.Run;
        }
    }
}