using System;
using System.Drawing;

namespace Cave_Adventure.EntitiesFrames
{
    public static class AnimationSetUp
    {
        public static void SetUp(Entity entity, out int frameLimit, out Image entityImage)
        {
            switch (entity)
            {
                case Player:
                    SetUpPlayer(entity.CurrentStates, out frameLimit, out entityImage);
                    break;
                case Spider:
                    SetUpSpider(entity.CurrentStates, out frameLimit, out entityImage);
                    break;
                case Snake:
                    SetUpSnake(entity.CurrentStates, out frameLimit, out entityImage);
                    break;
                default:
                    throw new Exception("Ошибка в настройке анимации");
            }
        }

        private static void SetUpPlayer(StatesOfAnimation currentAnimation, out int frameLimit, out Image entityImage)
        {
            entityImage = Properties.Resources.Gladiator;
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
        
        private static void SetUpSpider(StatesOfAnimation currentAnimation, out int frameLimit, out Image entityImage)
        {
            entityImage = Properties.Resources.Spider;
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
        
        private static void SetUpSnake(StatesOfAnimation currentAnimation, out int frameLimit, out Image entityImage)
        {
            entityImage = Properties.Resources.Cobra;
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
    }
}