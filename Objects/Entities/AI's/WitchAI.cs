﻿using System.Drawing;

namespace Cave_Adventure
{
    public class WitchAI : AI
    {
        public WitchAI(Monster monster) : base(monster)
        {
        }

        public override Point LookTargetMovePoint(int range = 25, bool entityBlockingPath = true)
        {
            return base.LookTargetMovePoint(GlobalConst.WitchDetectionRadius, entityBlockingPath);
        }
    }
}
