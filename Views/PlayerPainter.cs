using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace Cave_Adventure
{
    public class PlayerPainter
    {
        private readonly Image _gladiatorSheet = Properties.Resources.Gladiator;
        // private const int ImageSize = GlobalConstants.AssetsSize;
        private const int ImageSize = 32;

        private int _mirroring = 1;
        private int _currentAnimation;
        private int _currentFrame = 0;
        private int _currentFrameLimit = 0;
        
        public void SetUpAndPaint(Graphics graphics, Player player)
        {
            var playerPosition = new Point(player.Position.X * GlobalConst.AssetsSize,
                player.Position.Y * GlobalConst.AssetsSize);
            
            _mirroring = (int) player.ViewDirection;
            _currentAnimation = (int) player.CurrentStates;
            SetFrameLimit(player.CurrentStates);
            PlayAnimation(graphics, playerPosition);
        }
        
        private void PlayAnimation(Graphics graphics, Point playerPosition)
        {
            if (_currentFrame < _currentFrameLimit - 1)
                _currentFrame++;
            else _currentFrame = 0;
            
            graphics.DrawImage(
                _gladiatorSheet,
                new Rectangle(
                    playerPosition.X - _mirroring * ImageSize / 2,
                    playerPosition.Y,
                    _mirroring * ImageSize * 2,
                    ImageSize * 2
                    ), 
                32*_currentFrame,
                32*_currentAnimation,
                ImageSize,
                ImageSize,
                GraphicsUnit.Pixel
                );
        }
        
        private void SetFrameLimit(StatesOfAnimation currentAnimation)
        {
            switch (currentAnimation)
            {
                case StatesOfAnimation.Idle:
                    _currentFrameLimit = AmountHeroFrames.IdleFrames;
                    break;
                case StatesOfAnimation.Run:
                    _currentFrameLimit = AmountHeroFrames.RunFrames;
                    break;
                case StatesOfAnimation.Attack:
                    _currentFrameLimit = AmountHeroFrames.AttackFrames;
                    break;
                case StatesOfAnimation.Death:
                    _currentFrameLimit = AmountHeroFrames.DeathFrames;
                    break;  
            }
        }
    }
}