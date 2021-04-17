using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace Cave_Adventure
{
    public class Player
    {
        public Point Position;
        public int DirX;
        public int DirY;
        public int IdleFrames;
        public int RunFrames;
        public int AttackFrames;
        public int DeathFrames;
        public Image GladiatorImage;
        public int Size;
        public bool isMoving;
        public int CurrentAnimation;
        public int CurrentFrame;
        public int CurrentLimit;
        public int Flip;

        public Player(Point position, int idleFrames, int runFrames, int attackFrames, int deathFrames, Image spriteSheet)
        {
            Position = position;
            IdleFrames = idleFrames;
            RunFrames = runFrames;
            AttackFrames = attackFrames;
            DeathFrames = deathFrames;
            GladiatorImage = spriteSheet;
            Size = 33;
            CurrentAnimation = 0;
            CurrentFrame = 0;
            CurrentLimit = idleFrames;
            Flip = 1;
        }

        public void Move()
        {
            Position.X += DirX;
            Position.Y += DirY;
        }

        public Player(Player player)
            : this(player.Position)
        { }

        public Player(Point position)
        {
            Position = position;
        }

        public void PlayAnimation(Graphics g)
        {
            if (CurrentFrame < CurrentLimit - 1)
                CurrentFrame++;
            else CurrentFrame = 0;
            
            g.DrawImage
                (GladiatorImage, new Rectangle(new Point(Position.X - Flip * Size / 2, Position.Y),
                new Size(Flip * Size * 2, Size * 2)), 32*CurrentFrame, 32*CurrentAnimation, Size, Size, GraphicsUnit.Pixel);
        }

        public void SetAnimationConfiguration(int currentAnimation)
        {
            CurrentAnimation = currentAnimation;

            switch (currentAnimation)
            {
                case 0:
                    CurrentLimit = IdleFrames;
                    break;
                case 1:
                    CurrentLimit = RunFrames;
                    break;
                case 2:
                    CurrentLimit = AttackFrames;
                    break;
                case 4:
                    CurrentLimit = DeathFrames;
                    break;
                case 5:
                    CurrentLimit = IdleFrames;
                    break;
                case 6:
                    CurrentLimit = RunFrames;
                    break;
                case 7:
                    CurrentLimit = AttackFrames;
                    break;
            }
        }
    }
}