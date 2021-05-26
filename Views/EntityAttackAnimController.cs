using System.Timers;

namespace Cave_Adventure.Views
{
    public class EntityAttackAnimController
    {
        private readonly Timer _timer;
        private readonly Entity _entity;

        public EntityAttackAnimController(Entity entity)
        {
            _timer = new Timer() {Interval = GlobalConst.AnimTimerInterval, AutoReset = false};
            _entity = entity;
        }
        
        public void PlayAttackAnimation()
        {
            _timer.Elapsed += (_, __) =>
            {
                _entity.SetAnimation(StatesOfAnimation.Idle);
                _timer?.Stop();
            };
            _timer.Start();
            _entity.SetAnimation(StatesOfAnimation.Attack);
        }
    }
}