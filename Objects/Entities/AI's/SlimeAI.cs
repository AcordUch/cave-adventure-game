using System.Drawing;

namespace Cave_Adventure
{
    public class SlimeAI : AI
    {
        public SlimeAI(Monster monster) : base(monster)
        {
        }

        public override Point LookTargetMovePoint(int range = 25)
        {
            return base.LookTargetMovePoint(GlobalConst.SlimeDetectionRadius);
        }
    }
}
