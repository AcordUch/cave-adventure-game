using System.Drawing;

namespace Cave_Adventure
{
    public class Monster: IMonster
    {
        public Point Position { get; set; }
        public StatesOfAnimation CurrentStates { get; }
        public ViewDirection ViewDirection { get; set; }
        public bool IsMoving { get; set; }
        public bool IsSelected { get; set; }
        public int AnimStage { get; set; }
        public Point TargetPoint { get; }
        public double Health { get; }
        public int AP { get; }
        public double Attack { get; }
        public double Defense { get; }
        public double Damage { get; }

        public Monster(Point position)
        {
            Position = position;
        }

        public void Move()
        {
            throw new System.NotImplementedException();
        }
    }
}