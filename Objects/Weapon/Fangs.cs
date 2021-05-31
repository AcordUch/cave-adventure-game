namespace Cave_Adventure
{
    public class Fangs : Weapon
    {
        public Fangs() : base(GlobalConst.FangsFactor, GlobalConst.FangsRadius)
        {
        }

        public override double GetDamage(in Entity entity)
        {
            entity.ReduceAP(entity.AP);
            return base.GetDamage(in entity);
        }
        
        public override string ToString()
        {
            return "Клыки";
        }
    }
}