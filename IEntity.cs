using System.Drawing;

namespace Cave_Adventure
{
    public interface IEntity
    {
        Point Position { get; set; }
        StatesOfAnimation CurrentStates { get; }
        ViewDirection ViewDirection { get; set; }
        bool IsMoving { get; set; }
        bool IsSelected { get; set; }
        int AnimStage { get; set; }
        Point TargetPoint { get; }
        double Health { get; }
        int AP { get; }
        double Attack { get; }
        double Defense { get; }
        double Damage { get; }
        
        void Move(int dx, int dy);
    }
}