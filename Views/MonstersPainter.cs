﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cave_Adventure
{
    class MonstersPainter
    {
        private readonly Image _spider = Properties.Resources.Spider;
        private readonly Image _snake = Properties.Resources.Cobra;
        private const int ImageSize = 32;

        private int _mirroring = 1;
        private int _currentAnimation;
        private int _currentFrame = 0;
        private int _currentFrameLimit = 0;

        public void SetUpAndPaint(Graphics graphics, Monster monster)
        {
            var monsterPosition = new Point(monster.Position.X * GlobalConst.AssetsSize,
                    monster.Position.Y * GlobalConst.AssetsSize);
            _mirroring = (int)monster.ViewDirection;
            _currentAnimation = (int)monster.CurrentStates;
            SetFrameLimit(monster.CurrentStates);
            Image monsterImage = _snake;
            switch (monster.Tag)
            {
                case MonsterType.Snake:
                    monsterImage = _snake;
                    break;
                case MonsterType.Spider:
                    monsterImage = _spider;
                    break;
            }
            PlayAnimation(graphics, monsterPosition, monsterImage);
        }

        private void PlayAnimation(Graphics graphics, Point monsterPosition, Image image)
        {
            if (_currentFrame < _currentFrameLimit - 1)
                _currentFrame++;
            else _currentFrame = 0;

            graphics.DrawImage(
                image,
                new Rectangle(
                    monsterPosition.X - _mirroring * ImageSize / 2,
                    monsterPosition.Y,
                    _mirroring * ImageSize * 2,
                    ImageSize * 2
                    ),
                32 * _currentFrame,
                32 * _currentAnimation,
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
