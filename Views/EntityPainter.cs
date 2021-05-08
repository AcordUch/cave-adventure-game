using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Cave_Adventure.EntitiesFrames;

namespace Cave_Adventure
{
    public class EntityPainter
    {
        private const int ImageSize = 32;

        private bool _configured = false;
        private Dictionary<Entity, int> _currentFrames;
        private Entity _currentEntity;
        private int _mirroring = 1;
        private int _currentAnimation;
        private int _currentFrameLimit = 0;
        
        public int DisplacementStage { get; set; } = 0;

        public void Configure(List<Entity> entities)
        {
            if (_configured)
                throw new InvalidOperationException();
            ReConfigure(entities);
            _configured = true;
        }

        public void Drop()
        {
            _currentFrames = null;
            _configured = false;
        }

        public void ReConfigure(List<Entity> entities)
        {
            _currentFrames = entities.ToDictionary(k => k, v => 0);
        }
        
        public void SetUpAndPaint(Graphics graphics, Entity entity)
        {
            if(!_configured)
                return;
            
            var playerPositionReal = GetGraphicPosition(entity);
            _currentEntity = entity;
            _mirroring = (int) entity.ViewDirection;
            _currentAnimation = (int) entity.CurrentStates;
            AnimationSetUp.SetUp(entity, out _currentFrameLimit, out var entityImage);
            PlayAnimation(graphics, playerPositionReal, entityImage);
        }
        
        private void PlayAnimation(Graphics graphics, Point playerPosition, Image entityImage)
        {
            if (_currentFrames[_currentEntity] < _currentFrameLimit - 1)
                _currentFrames[_currentEntity]++;
            else _currentFrames[_currentEntity] = 0;
            
            graphics.DrawImage(
                entityImage,
                new Rectangle(
                    playerPosition.X - _mirroring * ImageSize / 2,
                    playerPosition.Y,
                    _mirroring * ImageSize * 2,
                    ImageSize * 2
                    ), 
                32*_currentFrames[_currentEntity],
                32*_currentAnimation,
                ImageSize,
                ImageSize,
                GraphicsUnit.Pixel
                );
        }

        private Point GetGraphicPosition(Entity entity)
        {
            var dPoint = Point.Empty;
            if(entity.IsMoving)
            {
                dPoint = entity.GetDeltaPoint();
                DisplacementStage++;
                if (DisplacementStage == 15)
                {
                    DisplacementStage = 0;
                    entity.Move(dPoint.X, dPoint.Y);
                }
            }
            
            return new Point(entity.Position.X * GlobalConst.AssetsSize + DisplacementStage * dPoint.X * GlobalConst.AssetsSize / 16,
                entity.Position.Y * GlobalConst.AssetsSize + DisplacementStage * dPoint.Y * GlobalConst.AssetsSize / 16);
        }
    }
}