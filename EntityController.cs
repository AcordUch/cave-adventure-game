namespace Cave_Adventure
{
    public class EntityController
    {
        public Player Player { get; set; }
        public Monster[] Monsters { get; set; }
        private bool _configured = false;

        public EntityController()
        {}

        public void Configure(ArenaMap arenaMap)
        {
            Player = arenaMap.Player;
            
        }
    }
}