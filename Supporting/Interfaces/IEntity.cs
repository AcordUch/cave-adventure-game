using System.Drawing;

namespace Cave_Adventure
{
    public interface IEntity
    {
        Point Position { get; set; }
        StatesOfAnimation CurrentStates { get; }
        ViewDirection ViewDirection { get; set; }
        EntityType Tag { get; }
        bool IsMoving { get; }
        bool IsSelected { get; set; }
        Point TargetPoint { get; }
        double Health { get; }
        int AP { get; }
        double Attack { get; }
        double Defense { get; }
        double Damage { get; }
        
        void Move(int dx, int dy);
        void TeleportToPoint(Point point);
        void ResetAP();
        Point GetDeltaPoint();
        void SetTargetPoint(Point point);

    }
}