using System.Drawing;
using Cave_Adventure.EntitiesFrames;

namespace Cave_Adventure
{
    public class EntityPainter
    {
        private const int ImageSize = 32;

        private int _mirroring = 1;
        private int _currentAnimation;
        private int _currentFrame = 0;
        private int _currentFrameLimit = 0;
        
        public int DisplacementStage { get; set; } = 0;
        
        public void SetUpAndPaint(Graphics graphics, Entity entity)
        {
            var playerPositionReal = GetGraphicPosition(entity);
            
            _mirroring = (int) entity.ViewDirection;
            _currentAnimation = (int) entity.CurrentStates;
            AnimationSetUp.SetUp(entity, out _currentFrameLimit, out var entityImage);
            PlayAnimation(graphics, playerPositionReal, entityImage);
        }
        
        private void PlayAnimation(Graphics graphics, Point playerPosition, Image entityImage)
        {
            if (_currentFrame < _currentFrameLimit - 1)
                _currentFrame++;
            else _currentFrame = 0;
            
            graphics.DrawImage(
                entityImage,
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