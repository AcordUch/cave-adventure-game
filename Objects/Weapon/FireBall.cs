namespace Cave_Adventure
{
    public class FireBall : Weapon
    {
        public FireBall() : base(GlobalConst.FireBallFactor, GlobalConst.FireBallRadius)
        {
        }

        public override double GetDamage(in Entity entity)
        {
            entity.ReduceAP(2);
            return base.GetDamage(in entity);
        }
    }
}