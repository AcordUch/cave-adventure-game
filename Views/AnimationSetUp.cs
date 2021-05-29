using System;
using System.Drawing;

namespace Cave_Adventure.EntitiesFrames
{
    public static class AnimationSetUp
    {
        public static void SetUp(Entity entity, out int frameLimit, out Image entityImage, out int imageSize)
        {
            imageSize = GlobalConst.EntityTextureSize;
            switch (entity)
            {
                case Player:
                    entityImage = Properties.Resources.Gladiator;
                    SetUpPlayer(entity.CurrentStates, out frameLimit);
                    break;
                case Slime:
                    entityImage = Properties.Resources.Slime;
                    SetUpSlime(entity.CurrentStates, out frameLimit);
                    break;
                case Spider:
                    entityImage = Properties.Resources.Spider;
                    SetUpSpider(entity.CurrentStates, out frameLimit);
                    break;
                case Snake:
                    entityImage = Properties.Resources.Cobra;
                    SetUpSnake(entity.CurrentStates, out frameLimit);
                    break;
                case Golem:
                    entityImage = Properties.Resources.Mini_Golem;
                    SetUpGolem(entity.CurrentStates, out frameLimit);
                    break;
                case Ghoul:
                    entityImage = Properties.Resources.Ghoul;
                    SetUpGhoul(entity.CurrentStates, out frameLimit);
                    break;
                case Witch:
                    entityImage = Properties.Resources.Witch;
                    SetUpWitch(entity.CurrentStates, out frameLimit);
                    break;
                case Minotaur:
                    entityImage = Properties.Resources.Minotaur;
                    SetUpMinotaur(entity.CurrentStates, out frameLimit);
                    imageSize = GlobalConst.BossTextureSize;
                    break;
                default:
                    throw new Exception("Ошибка в настройке анимации");
            }
        }

        private static void SetUpPlayer(StatesOfAnimation currentAnimation, out int frameLimit)
        {
            switch (currentAnimation)
            {
                case StatesOfAnimation.Idle:
                    frameLimit = AmountHeroFrames.IdleFrames;
                    break;
                case StatesOfAnimation.Run:
                    frameLimit = AmountHeroFrames.RunFrames;
                    break;
                case StatesOfAnimation.Attack:
                    frameLimit = AmountHeroFrames.AttackFrames;
                    break;
                case StatesOfAnimation.Death:
                    frameLimit = AmountHeroFrames.DeathFrames;
                    break;
                default:
                    frameLimit = AmountHeroFrames.IdleFrames;
                    break;
            }
        }

        private static void SetUpSlime(StatesOfAnimation currentAnimation, out int frameLimit)
        {
            switch (currentAnimation)
            {
                case StatesOfAnimation.Idle:
                    frameLimit = AmountSlimeFrames.IdleFrames;
                    break;
                case StatesOfAnimation.Run:
                    frameLimit = AmountSlimeFrames.RunFrames;
                    break;
                case StatesOfAnimation.Attack:
                    frameLimit = AmountSlimeFrames.AttackFrames;
                    break;
                case StatesOfAnimation.Death:
                    frameLimit = AmountSlimeFrames.DeathFrames;
                    break;
                default:
                    frameLimit = AmountSlimeFrames.IdleFrames;
                    break;
            }
        }

        private static void SetUpSpider(StatesOfAnimation currentAnimation, out int frameLimit)
        {
            switch (currentAnimation)
            {
                case StatesOfAnimation.Idle:
                    frameLimit = AmountSpiderFrames.IdleFrames;
                    break;
                case StatesOfAnimation.Run:
                    frameLimit = AmountSpiderFrames.RunFrames;
                    break;
                case StatesOfAnimation.Attack:
                    frameLimit = AmountSpiderFrames.AttackFrames;
                    break;
                case StatesOfAnimation.Death:
                    frameLimit = AmountSpiderFrames.DeathFrames;
                    break;
                default:
                    frameLimit = AmountSpiderFrames.IdleFrames;
                    break;
            }
        }
        
        private static void SetUpSnake(StatesOfAnimation currentAnimation, out int frameLimit)
        {
            switch (currentAnimation)
            {
                case StatesOfAnimation.Idle:
                    frameLimit = AmountSnakeFrames.IdleFrames;
                    break;
                case StatesOfAnimation.Run:
                    frameLimit = AmountSnakeFrames.RunFrames;
                    break;
                case StatesOfAnimation.Attack:
                    frameLimit = AmountSnakeFrames.AttackFrames;
                    break;
                case StatesOfAnimation.Death:
                    frameLimit = AmountSnakeFrames.DeathFrames;
                    break;
                default:
                    frameLimit = AmountSnakeFrames.IdleFrames;
                    break;
            }
        }

        private static void SetUpGolem(StatesOfAnimation currentAnimation, out int frameLimit)
        {
            switch (currentAnimation)
            {
                case StatesOfAnimation.Idle:
                    frameLimit = AmountGolemFrames.IdleFrames;
                    break;
                case StatesOfAnimation.Run:
                    frameLimit = AmountGolemFrames.RunFrames;
                    break;
                case StatesOfAnimation.Attack:
                    frameLimit = AmountGolemFrames.AttackFrames;
                    break;
                case StatesOfAnimation.Death:
                    frameLimit = AmountGolemFrames.DeathFrames;
                    break;
                default:
                    frameLimit = AmountGolemFrames.IdleFrames;
                    break;
            }
        }

        private static void SetUpGhoul(StatesOfAnimation currentAnimation, out int frameLimit)
        {
            switch (currentAnimation)
            {
                case StatesOfAnimation.Idle:
                    frameLimit = AmountGhoulFrames.IdleFrames;
                    break;
                case StatesOfAnimation.Run:
                    frameLimit = AmountGhoulFrames.RunFrames;
                    break;
                case StatesOfAnimation.Attack:
                    frameLimit = AmountGhoulFrames.AttackFrames;
                    break;
                case StatesOfAnimation.Death:
                    frameLimit = AmountGhoulFrames.DeathFrames;
                    break;
                default:
                    frameLimit = AmountGhoulFrames.IdleFrames;
                    break;
            }
        }

        private static void SetUpWitch(StatesOfAnimation currentAnimation, out int frameLimit)
        {
            switch (currentAnimation)
            {
                case StatesOfAnimation.Idle:
                    frameLimit = AmountWitchFrames.IdleFrames;
                    break;
                case StatesOfAnimation.Run:
                    frameLimit = AmountWitchFrames.RunFrames;
                    break;
                case StatesOfAnimation.Attack:
                    frameLimit = AmountWitchFrames.AttackFrames;
                    break;
                case StatesOfAnimation.Death:
                    frameLimit = AmountWitchFrames.DeathFrames;
                    break;
                default:
                    frameLimit = AmountWitchFrames.IdleFrames;
                    break;
            }
        }

        private static void SetUpMinotaur(StatesOfAnimation currentAnimation, out int frameLimit)
        {
            switch (currentAnimation)
            {
                case StatesOfAnimation.Idle:
                    frameLimit = AmountMinotaurFrames.IdleFrames;
                    break;
                case StatesOfAnimation.Run:
                    frameLimit = AmountMinotaurFrames.RunFrames;
                    break;
                case StatesOfAnimation.Attack:
                    frameLimit = AmountMinotaurFrames.AttackFrames;
                    break;
                case StatesOfAnimation.Death:
                    frameLimit = AmountMinotaurFrames.DeathFrames;
                    break;
                default:
                    frameLimit = AmountMinotaurFrames.IdleFrames;
                    break;
            }
        }
    }
}