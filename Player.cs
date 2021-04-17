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
        }

        public void Move(int dx, int dy)
        {
            _dX = dx;
            _dY = dy;
        }
        
        public void SetAnimationConfiguration(StatesOfAnimation currentAnimation)
        {
            CurrentStates = currentAnimation;
            IsMoving = currentAnimation == StatesOfAnimation.Run;
        }
    }
}