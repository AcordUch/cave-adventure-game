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
        private Player Player => _arenaMap.Player;

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
            //var distantToPlayer = (int)Math.Ceiling(_monster.Position.RangeToPoint(Player.Position));
            if (_monster.Health < 10)
            {
                return BFS.FindFarPoint(_arenaMap, Player.Position, _monster);
            }
            var rnd = new Random();
            if (rnd.NextDouble() > 0.3 && GlobalConst.PossibleDirections.Any(p => Player.Position + p == _monster.Position))
                return _monster.Position;
            return AStarPF.FindPathToPlayer(_arenaMap, _monster.Position, _monster.AP).ToList()[^1];
        }
    }
}