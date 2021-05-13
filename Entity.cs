using System.Drawing;
using System.Collections.Generic;

namespace Cave_Adventure
{
    public class Entity: IEntity
    {
        private Point _position;

        public StatesOfAnimation CurrentStates { get; private set; } = StatesOfAnimation.Idle;
        public ViewDirection ViewDirection { get; set; } = ViewDirection.Right;
        public EntityType Tag { get; }
        public bool IsSelected { get; set; }
        public bool IsMoving { get; private set; }
        public Point TargetPoint { get; private set; }
        public double Health { get; protected set; }
        public int AP { get; protected set; }
        public double Attack { get; protected set; }
        public double Defense { get; }
        public double Damage { get; }

        public Point Position
        {
            get => _position;
            set => _position = value;
        }

        public Entity(Point position, EntityType tag)
        {
            Tag = tag;
            _position = position;
        }
        
        public virtual void Attacking(in double heatlh)
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

        public virtual void ResetAP()
        {
            AP = 0;
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

        public List<Point> GetNeighbors()
        {
            var resultList = new List<Point>();

            foreach (var size in GlobalConst.listOfNeighbors)
                 resultList.Add(_position + size);

            return resultList;
        }

        public void SetAnimation(StatesOfAnimation currentAnimation)
        {
            CurrentStates = currentAnimation;
        }
    }
}