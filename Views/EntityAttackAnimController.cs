using System.Timers;

namespace Cave_Adventure.Views
{
    public class EntityAttackAnimController
    {
        private readonly Timer _timer;
        private readonly Entity _entity;

        public EntityAttackAnimController(Entity entity)
        {
            _timer = new Timer() {Interval = 2000};
            _entity = entity;
        }
        
        public void PlayAttackAnimation()
        {
            _timer.Elapsed += OnTimedEvent;
            _timer.Start();
            _entity.SetAnimation(StatesOfAnimation.Attack);
        }
        
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            _entity.SetAnimation(StatesOfAnimation.Idle);
            _timer?.Stop();
        }
    }
}