using System;
using System.Drawing;
using System.Linq;

namespace Cave_Adventure
{
    public class AI
    {
        private readonly Monster _monster;
        private ArenaMap _arenaMap;
        private bool _configured = false;

        public AI(Monster monster)
        {
            _monster = monster;
        }
        
        public void Configure(ArenaMap arenaMap)
        {
            _arenaMap = arenaMap;
            _configured = true;
        }

        public Point LookTargetMovePoint()
        {
            return AStarPF.FindPathToPlayer(_arenaMap, _monster.Position, _monster.AP).ToList()[^1];
        }
    }
}