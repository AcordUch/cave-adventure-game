using System.Drawing;

namespace Cave_Adventure
{
    public class Player: Entity
    {
        public bool IsMonsterNearby { get; set; }
        
        public Player(Point position) : base(position, EntityType.Player)
        {
            Initiative = 10;
        }

        public override void ResetAP()
        {
            AP = GlobalConst.PlayerAP;
        }
    }
}