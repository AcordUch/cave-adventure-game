using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Cave_Adventure.EntitiesFrames;

namespace Cave_Adventure
{
    public class EntityPainter
    {
        private const int MaxStg = 16;

        private bool _configured = false;
        private Dictionary<Entity, int> _currentFrames;
        private Dictionary<Entity, int> _displacementStage;
        private Dictionary<Entity, bool> _animationShouldStop;
        private Dictionary<Entity, StatesOfAnimation> _prevState;
        private Entity _currentEntity;
        private int _imageSize = 32;
        private int _mirroring = 1;
        private int _currentAnimation;
        private int _currentFrameLimit = 0;
        
        public void Configure(List<Entity> entities)
        {
            _currentFrames = entities.ToDictionary(k => k, v => 0);
            _displacementStage = entities.ToDictionary(k => k, v => 0);
            _animationShouldStop = entities.ToDictionary(k => k, v => false);
            _prevState = entities.ToDictionary(k => k, v => StatesOfAnimation.Idle);
            _configured = true;
        }

        public void Drop()
        {
            _configured = false;
            _currentFrames = null;
            _displacementStage = null;
            _animationShouldStop = null;
        }

        public void SetUpAndPaint(Graphics graphics, Entity entity)
        {
            if (!_configured)
                return;

            var playerPositionReal = GetGraphicPosition(entity);
            _currentEntity = entity;
            _mirroring = (int)entity.ViewDirection;
            _currentAnimation = (int)entity.CurrentStates;
            AnimationSetUp.SetUp(entity, out _currentFrameLimit, out var entityImage, out _imageSize);
            if (entity.CurrentStates != _prevState[entity])
            {
                _prevState[entity] = entity.CurrentStates;
                _currentFrames[entity] = 0;
            }
            PlayAnimation(graphics, playerPositionReal, entityImage);
        }

        private void PlayAnimation(Graphics graphics, Point playerPosition, Image entityImage)
        {
            ChangeCurrentFrame();

            if (_imageSize == GlobalConst.BossTextureSize)
            {
                graphics.DrawImage(
                    entityImage,
                    new Rectangle(
                        playerPosition.X,
                        playerPosition.Y - GlobalConst.BlockTextureSize,
                        GlobalConst.BlockTextureSize * 2,
                        GlobalConst.BlockTextureSize * 2
                    ),
                    _imageSize * _currentFrames[_currentEntity],
                    _imageSize * _currentAnimation,
                    _imageSize,
                    _imageSize,
                    GraphicsUnit.Pixel
                );
                return;
            }

            graphics.DrawImage(
                entityImage,
                new Rectangle(
                    playerPosition.X,
                    playerPosition.Y,
                    GlobalConst.BlockTextureSize,
                    GlobalConst.BlockTextureSize
                    ),
                _imageSize * _currentFrames[_currentEntity],
                _imageSize * _currentAnimation,
                _imageSize,
                _imageSize,
                GraphicsUnit.Pixel
                );
        }

        private void ChangeCurrentFrame()
        {
            if (_animationShouldStop[_currentEntity])
                return;

            if (_currentFrames[_currentEntity] < _currentFrameLimit - 1)
                _currentFrames[_currentEntity]++;
            else
            {
                if (_currentEntity.IsDead && !_animationShouldStop[_currentEntity])
                {
                    _animationShouldStop[_currentEntity] = true;
                    return;
                }
                _currentFrames[_currentEntity] = 0;
            }
        }

        private Point GetGraphicPosition(Entity entity)
        {
            var dPoint = Point.Empty;
            lock (new object())
            {
                if (entity.IsMoving)
                {
                    dPoint = entity.GetDeltaPoint();
                    _displacementStage[entity]++;
                    if (_displacementStage[entity] == MaxStg - 1)
                    {
                        _displacementStage[entity] = 0;
                        entity.Move(dPoint.X, dPoint.Y);
                    }
                }
            }

            return new Point(entity.Position.X * GlobalConst.BlockTextureSize + _displacementStage[entity] * dPoint.X * GlobalConst.BlockTextureSize / MaxStg,
                entity.Position.Y * GlobalConst.BlockTextureSize + _displacementStage[entity] * dPoint.Y * GlobalConst.BlockTextureSize / MaxStg);
        }
    }
}