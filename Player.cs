using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace Cave_Adventure
{
    public class Player
    {
        private const int ImageSize = 33;

        private readonly Image _gladiatorImage = Properties.Resources.Gladiator;
        private Point _position;
        private int _currentAnimation;
        private int _currentFrame;
        private int _currentLimit;
        
        public int Mirroring { get; set; }
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
            _currentAnimation = 0;
            _currentFrame = 0;
            _currentLimit = AmountHeroFrames.IdleFrames;
            Mirroring = 1;
        }

        public void Move()
        {
            _position.X += DirX;
            _position.Y += DirY;
        }
        
        public void PlayAnimation(Graphics g)
        {
            if (_currentFrame < _currentLimit - 1)
                _currentFrame++;
            else _currentFrame = 0;
            
            g.DrawImage
                (_gladiatorImage, new Rectangle(new Point(_position.X - Mirroring * ImageSize / 2, _position.Y),
                new Size(Mirroring * ImageSize * 2, ImageSize * 2)), 32*_currentFrame, 32*_currentAnimation, ImageSize, ImageSize, GraphicsUnit.Pixel);
        }

        public void SetAnimationConfiguration(StatesOfAnimation currentAnimation)
        {
            _currentAnimation = (int)currentAnimation;

            switch (currentAnimation)
            {
                case StatesOfAnimation.Idle:
                    IsMoving = false;
                    _currentLimit = AmountHeroFrames.IdleFrames;
                    break;
                case StatesOfAnimation.Run:
                    IsMoving = true;
                    _currentLimit = AmountHeroFrames.RunFrames;
                    break;
                case StatesOfAnimation.Attack:
                    IsMoving = false;
                    _currentLimit = AmountHeroFrames.AttackFrames;
                    break;
                case StatesOfAnimation.Death:
                    IsMoving = false;
                    _currentLimit = AmountHeroFrames.DeathFrames;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currentAnimation), currentAnimation, "Незапланированное состояние");
            }
        }
    }
}