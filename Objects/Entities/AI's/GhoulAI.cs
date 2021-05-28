using System.Drawing;

namespace Cave_Adventure
{
    public class GhoulAI : AI
    {
        public GhoulAI(Monster monster) : base(monster)
        {
        }

        public override Point LookTargetMovePoint(int range = 25)
        {
            return base.LookTargetMovePoint(GlobalConst.GhoulDetectionRadius);
        }
    }
}
