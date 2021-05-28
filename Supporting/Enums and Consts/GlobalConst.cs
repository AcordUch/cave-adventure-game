using System;
using System.Drawing;
using System.Collections.Generic;

namespace Cave_Adventure
{
    public static class GlobalConst
    {
        public const int AssetsSize = 64;
        public const int MainTimerInterval = 60;
        public const int AnimTimerInterval = 2000;
        
        // ===Weapon===
        public const double SwordFactor = 1;
        public const double BowFactor = 0.7;
        public const double FireBallFactor = 2.5;
        public const double FangsAndClawsFactor = 1;
        public const double StickyBodyFactor = 1;
        public const double StonePawsFactor = 1;
        public const double VampireSwingFactor = 1;
        public const double SpellFactor = 1;
        public const double BattleAxeFactor = 1;

        public const int SwordRadius = 1;
        public const int BowRadius = 7;
        public const int FireBallRadius = 4;
        public const int FangsAndClawsRadius = 1;
        public const int StickyBodyRadius = 1;
        public const int StonePawsRadius = 1;
        public const int VampireSwingRadius = 1;
        public const int SpellRadius = 1;
        public const int BattleAxeRadius = 1;

        // ===Entity===
        public const int PlayerAP = 3;
        public const int SlimeAP = 3;
        public const int SpiderAP = 3;
        public const int SnakeAP = 2;
        public const int GolemAP = 2;
        public const int GhoulAP = 2;
        public const int WitchAP = 2;
        public const int MinotaurAP = 2;

        public const int PlayerAttack = 9;
        public const int SlimeAttack = 2;
        public const int SpiderAttack = 4;
        public const int SnakeAttack = 6;
        public const int GolemAttack = 8;
        public const int GhoulAttack = 10;
        public const int WitchAttack = 12;
        public const int MinotaurAttack = 30;

        public const int PlayerHP = 70;
        public const int SlimeHP = 10;
        public const int SpiderHP = 20;
        public const int SnakeHP = 25;
        public const int GolemHP = 40;
        public const int GhoulHP = 50;
        public const int WitchHP = 60;
        public const int MinotaurHP = 200;

        public const int PlayerDamage = 15;
        public const int SlimeDamage = 2;
        public const int SpiderDamage = 3;
        public const int SnakeDamage = 7;
        public const int GolemDamage = 8;
        public const int GhoulDamage = 9;
        public const int WitchDamage = 10;
        public const int MinotaurDamage = 11;

        public const int PlayerDefence = 9;
        public const int SlimeDefence = 8;
        public const int SpiderDefence = 7;
        public const int SnakeDefence = 2;
        public const int GolemDefence = 5;
        public const int GhoulDefence = 4;
        public const int WitchDefence = 3;
        public const int MinotaurDefence = 2;

        public const double SlimeHealDropChange = 0.1;
        public const double SpiderHealDropChange = 0.2;
        public const double SnakeHealDropChange = 0.3;
        public const double GolemHealDropChange = 0.4;
        public const double GhoulHealDropChange = 0.5;
        public const double WitchHealDropChange = 0.6;
        public const double MinotaurHealDropChange = 0.7;

        // ===AI===
        public const int SlimeDetectionRadius = 2;
        public const int SpiderDetectionRadius = 4;
        public const int SnakeDetectionRadius = 5;
        public const int GolemDetectionRadius = 4;
        public const int GhoulDetectionRadius = 5;
        public const int WitchDetectionRadius = 5;
        public const int MinotaurDetectionRadius = 6;

        // ===Supporting===
        public static IEnumerable<String> LoadLevels()
        {
            yield return Properties.Resources.Arena1;
            yield return Properties.Resources.Arena2;
            yield return Properties.Resources.Arena3;
            yield return Properties.Resources.Arena4;
            yield return Properties.Resources.Arena5;
            yield return Properties.Resources.Arena6;
            yield return Properties.Resources.Arena7;
            yield return Properties.Resources.Arena8;
            yield return Properties.Resources.Arena9;
            yield return Properties.Resources.Arena10;
        }
        
        public static IEnumerable<String> LoadDebugLevels()
        {
            foreach (var level in LoadLevels())
            {
                yield return level;
            }
            yield return Properties.Resources.Arena1Debug;
            yield return Properties.Resources.Arena2Debug;
            yield return Properties.Resources.Arena3Debug;
            yield return Properties.Resources.Arena4Debug;
            yield return Properties.Resources.Arena5Debug;
            yield return Properties.Resources.Arena6Debug;
            yield return Properties.Resources.Arena7Debug;
        }
        
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