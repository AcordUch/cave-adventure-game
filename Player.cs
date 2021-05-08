using System.Drawing;

namespace Cave_Adventure
{
    public class Player: Entity
    {
        public Player(Point position) : base(position, EntityType.Player)
        {
        }

        public override void ResetAP()
        {
            AP = GlobalConst.PlayerAP;
        }
    }
}