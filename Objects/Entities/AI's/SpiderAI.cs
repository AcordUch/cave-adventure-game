using System.Drawing;

namespace Cave_Adventure
{
    public class SpiderAI : AI
    {
        public SpiderAI(Monster monster) : base(monster)
        {
        }
        
        public override Point LookTargetMovePoint(int range = 25, bool entityBlockingPath = false)
        {
            return base.LookTargetMovePoint(GlobalConst.SpiderDetectionRadius, entityBlockingPath);
        }
    }
}