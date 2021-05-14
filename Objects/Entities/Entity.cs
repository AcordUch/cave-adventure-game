using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace Cave_Adventure
{
    public abstract class Entity: IEntity
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
        public double Attack { get; protected init; }
        public double Defense { get; protected init; }
        public double Damage { get; protected init; }
        public AbstractWeapon Weapon { get; protected init; }
        
        public Point Position
        {
            get => _position;
            set => _position = value;
        }

        public bool IsAlive => Health > 0;

        protected Entity(Point position, EntityType tag)
        {
            Tag = tag;
            _position = position;
        }
        
        public virtual double Attacking()
        {
            return Weapon.GetDamage(this);
        }

        public virtual void Defending(in Entity attacker)
        {
            if (attacker.Attack > this.Defense)
            {
                this.Health -= attacker.Attack >= 1.25 * this.Defense ? attacker.Attacking() * 1.5 : attacker.Attacking();
            }
            else
            {
                this.Health -= attacker.Attack <= 0.75 * this.Defense ? attacker.Attacking() * 0.5 : attacker.Attacking() * 0.75;
            }
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

        public IEnumerable<Point> GetNeighbors()
        {
            return GlobalConst.PossibleDirections.Select(size => _position + size).ToList();
        }

        protected void SetAnimation(StatesOfAnimation currentAnimation)
        {
            CurrentStates = currentAnimation;
        }
    }
}