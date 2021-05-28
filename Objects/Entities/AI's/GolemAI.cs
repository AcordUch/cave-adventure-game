using System.Drawing;

namespace Cave_Adventure
{
    public class GolemAI : AI
    {
        public GolemAI(Monster monster) : base(monster)
        {
        }

        public override Point LookTargetMovePoint(int range = 25)
        {
            return base.LookTargetMovePoint(GlobalConst.GolemDetectionRadius);
        }
    }
}
