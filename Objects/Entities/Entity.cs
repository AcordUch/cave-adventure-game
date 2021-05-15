using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Cave_Adventure.Views;

namespace Cave_Adventure
{
    public abstract class Entity: IEntity
    {
        private Point _position;
        private double _health;
        private readonly EntityAttackAnimController _attackAnimController;

        public StatesOfAnimation CurrentStates { get; private set; } = StatesOfAnimation.Idle;
        public ViewDirection ViewDirection { get; set; } = ViewDirection.Right;
        public EntityType Tag { get; }
        public bool IsSelected { get; set; }
        public bool IsMoving { get; private set; }
        public Point TargetPoint { get; private set; }
        public int AP { get; protected set; }
        public double Attack { get; protected init; }
        public double Defense { get; protected init; }
        public double Damage { get; protected init; }
        public Weapon Weapon { get; protected init; }

        public event Action<Entity> ContrattackMustStart;
        
        public Point Position
        {
            get => _position;
            set => _position = value;
        }

        public double Health
        {
            get => _health;
            protected set => _health = value >= 0 ? value : 0;
        }

        public bool IsAlive => Health > 0;

        protected Entity(Point position, EntityType tag)
        {
            Tag = tag;
            _position = position;
            _attackAnimController = new EntityAttackAnimController(this);
        }
        
        public virtual double Attacking()
        {
            _attackAnimController.PlayAttackAnimation();
            return Weapon.GetDamage(this);
        }

        public virtual void Defending(in Entity attacker, bool isfirstAttack = true)
        {
            if (attacker.Attack > this.Defense)
            {
                this.Health -= attacker.Attack >= 1.25 * this.Defense ? attacker.Attacking() * 1.5 : attacker.Attacking();
            }
            else
            {
                this.Health -= attacker.Attack <= 0.75 * this.Defense ? attacker.Attacking() * 0.5 : attacker.Attacking() * 0.75;
            }
            if (isfirstAttack)
                Counterattack(attacker);
        }

        private void Counterattack(Entity attacker)
        {
            var timer = new Timer() {Interval = 2100};
            timer.Elapsed += (_, __) =>
            {
                attacker.Defending(this, false);
                timer.Stop();
            };
            timer.Start();
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

        public void ReduceAP(int dAP)
        {
            AP = AP - dAP > 0 ? AP - dAP : 0;
        }

        public void IncreaseAP(int dAP)
        {
            AP = AP + dAP > 0 ? AP + dAP : 0;
        }

        public bool CheckIsAliveAndChangeState()
        {
            if(Health <= 0)
                SetAnimation(StatesOfAnimation.Death);
            return Health > 0;
        }
        
        public IEnumerable<Point> GetNeighbors()
        {
            return GlobalConst.PossibleDirections.Select(size => _position + size).ToList();
        }

        public void SetAnimation(StatesOfAnimation currentAnimation)
        {
            CurrentStates = currentAnimation;
        }
    }
}