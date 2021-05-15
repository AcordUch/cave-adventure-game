using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Cave_Adventure
{
    public static class GlobalConst
    {
        public const int AssetsSize = 64;
        
        // ===Weapon===
        public const double SwordFactor = 1;
        public const double BowFactor = 0.7;
        public const double FireBallFactor = 2.5;
        public const double FangsAndClawsFactor = 1;

        public const int SwordRadius = 1;
        public const int BowRadius = 7;
        public const int FireBallRadius = 4;
        public const int FangsAndClawsRadius = 1;
        
        // ===Entity===
        public const int PlayerAP = 4;
        public const int SpiderAP = 2;
        public const int SnakeAP = 2;
        
        public const int PlayerAttack = 9;
        public const int SpiderAttack = 4;
        public const int SnakeAttack = 6;
        
        public const int PlayerHP = 70;
        public const int SpiderHP = 20;
        public const int SnakeHP = 25;

        public const int PlayerDamage = 15;
        public const int SpiderDamage = 3;
        public const int SnakeDamage = 7;
        
        public const int PlayerDefence = 9;
        public const int SpiderDefence = 7;
        public const int SnakeDefence = 2;

        // ===Supporting===
        public static readonly List<Size> PossibleDirections = 
            new()
            {
                new Size(-1, 0),
                new Size(0, 1),
                new Size(1, 0),
                new Size(0, -1),
            };
        
        public static readonly List<Size> PossibleDirectionsExtended = 
            new()
            { 
                new Size(-1, -1), 
                new Size(-1, 0), 
                new Size(-1, 1),
                new Size(0, 1),
                new Size(1, 1),
                new Size(1, 0),
                new Size(1, -1),
                new Size(0, -1),
            };
    }
}