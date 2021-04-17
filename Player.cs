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

        public StatesOfAnimation CurrentStates { get; private set; } = StatesOfAnimation.Idle;
        public ViewDirection ViewDirection { get; set; } = ViewDirection.Right;
        public bool IsMoving { get; private set; }
        public int DirX { get; set; }
        public int DirY { get; set; }
        public Point Position
        {
            get => _position;
            set => _position = value;
        }

        public Player(Point position)
        {
            _position = position;
        }

        public void Move()
        {
            _position.X += DirX;
            _position.Y += DirY;
        }
        
        public void SetAnimationConfiguration(StatesOfAnimation currentAnimation)
        {
            CurrentStates = currentAnimation;
            IsMoving = currentAnimation == StatesOfAnimation.Run;
        }
    }
}