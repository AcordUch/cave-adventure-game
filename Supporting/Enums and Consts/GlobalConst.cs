using System;
using System.Drawing;
using System.Collections.Generic;

namespace Cave_Adventure
{
    public static class GlobalConst
    {
        public const int BlockTextureSize = 64;
        public const int EntityTextureSize = 32;
        public const int BossTextureSize = 75;
        public const int MainTimerInterval = 60;
        public const int AnimTimerInterval = 1000;
        public const int MaxArenaRow = 13;
        public const int MaxArenaColumn = 20;
        
        // ===HealPotion===
        public const int SmallHealPower = 15;
        public const int MediumHealPower = 30;
        public const int BigHealPower = 65;
        
        // ===Weapon===
        public const double SwordFactor = 1;
        public const double BowFactor = 0.7;
        public const double FireBallFactor = 2.5;
        public const double FangsFactor = 1;
        public const double StickyBodyFactor = 1;
        public const double StonePawsFactor = 1;
        public const double VampireSwingFactor = 1;
        public const double SpellFactor = 1;
        public const double BattleAxeFactor = 1;

        public const int SwordRadius = 1;
        public const int BowRadius = 7;
        public const int FireBallRadius = 4;
        public const int FangsRadius = 1;
        public const int StickyBodyRadius = 1;
        public const int StonePawsRadius = 1;
        public const int VampireSwingRadius = 1;
        public const int SpellRadius = 1;
        public const int BattleAxeRadius = 1;

        // ===Entity===
        public const int PlayerDamage = 15;
        public const int SlimeDamage = 2;
        public const int SpiderDamage = 5;
        public const int SnakeDamage = 6;
        public const int GolemDamage = 9;
        public const int GhoulDamage = 12;
        public const int WitchDamage = 20;
        public const int MinotaurDamage = 15;

        public const int PlayerAttack = 11;
        public const int SlimeAttack = 2;
        public const int SpiderAttack = 4;
        public const int SnakeAttack = 5;
        public const int GolemAttack = 7;
        public const int GhoulAttack = 11;
        public const int WitchAttack = 14;
        public const int MinotaurAttack = 30;
        
        public const int PlayerDefence = 12;
        public const int SlimeDefence = 2;
        public const int SpiderDefence = 7;
        public const int SnakeDefence = 4;
        public const int GolemDefence = 15;
        public const int GhoulDefence = 9;
        public const int WitchDefence = 3;
        public const int MinotaurDefence = 14;

        public const int PlayerHP = 90;
        public const int SlimeHP = 10;
        public const int SpiderHP = 20;
        public const int SnakeHP = 25;
        public const int GolemHP = 35;
        public const int GhoulHP = 80;
        public const int WitchHP = 30;
        public const int MinotaurHP = 200;
        
        public const int PlayerAP = 3;
        public const int SlimeAP = 3;
        public const int SpiderAP = 3;
        public const int SnakeAP = 2;
        public const int GolemAP = 1;
        public const int GhoulAP = 2;
        public const int WitchAP = 4;
        public const int MinotaurAP = 2;
        
        public const double SlimeHealDropChange = 0.1;
        public const double SpiderHealDropChange = 0.2;
        public const double SnakeHealDropChange = 0.3;
        public const double GolemHealDropChange = 0.4;
        public const double GhoulHealDropChange = 0.5;
        public const double WitchHealDropChange = 0.6;
        public const double MinotaurHealDropChange = 0.7;

        // ===AI===
        public const int SlimeDetectionRadius = 4;
        public const int SpiderDetectionRadius = 4;
        public const int SnakeDetectionRadius = 5;
        public const int GolemDetectionRadius = 3;
        public const int GhoulDetectionRadius = 4;
        public const int WitchDetectionRadius = 8;
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

        public static string GetSplash()
        {
            var rnd = new Random();
            return Splashes[rnd.Next(0, Splashes.Length - 1)];
        }

        private static readonly string[] Splashes = new[]
        {
            "Заходит в бар улитка, говорит...",
            "Пятый Месяц, Четвёртый день, Третья декады луны в тельце...",
            "Придумывать подписи слоооожна",
            "Как дела?",
            "Ctrl + ` открывает чит-меню :>",
            "Это вообще кто-нибудь когда-нибудь прочитает?",
            "inst: acord_Uch - подпишись, там иногда бывает красиво (⌒_⌒;)",
            "Хээээй |ᐕ)",
            "meow~~~ ฅ^•ﻌ•^ฅ",
            "Эй ты, да ты! Ты потрясающий!",
            "Тут есть разные подписи, соберешь их все?",
            "Матмеху привет, ********* **********",
            "Хочу медовухи",
            "Кофе - единственный напиток, которым могут наслаждаться гули",
            "Купил мужик шляпу, а она ему как раз",
            "Продам гараж",
            "Amongus",
            "Панки хой!",
            "Мегумин самая милая",
            "Который день, который год, спокойно ест и спит народ, ведь щедро охраняем КГБ!",
            "F3 CDC5CED1 D0C9D7CF",
            "По слухам где-то в этом подземелье прячется мёртвый анархист",
            "I don't want to set the world in fire"
        };
    }
}