using System.Drawing;

namespace Cave_Adventure
{
    public class SnakeAI : AI
    {
        public SnakeAI(Monster monster) : base(monster)
        {
        }

        public override Point LookTargetMovePoint(int range = 25)
        {
            return base.LookTargetMovePoint(GlobalConst.SnakeDetectionRadius);
        }
    }
}