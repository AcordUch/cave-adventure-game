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

        public virtual Point LookTargetMovePoint(int range = 25, bool entityBlockingPath = false)
        {
            if (_monster.Health < 10)
            {
                var pretenderPoint =  BFS.FindFarPoint(_arenaMap, Player.Position, _monster);
                return Player.Position.RangeToPoint(pretenderPoint) > 8 ? _monster.Position : pretenderPoint;
            }
            if (NotHavePlayerInDetectionRange(range, entityBlockingPath))
                return LookRandomPoint();
            var rnd = new Random();
            if (rnd.NextDouble() > 0.15 && GlobalConst.PossibleDirections.Any(p => Player.Position + p == _monster.Position))
                return _monster.Position;
            return AStarPF.FindPathToPlayer(_arenaMap, _monster.Position, _monster.AP, false).ToList()[^1];
        }

        private bool NotHavePlayerInDetectionRange(int range, bool entityBlockingPath)
        {
            return BFS.FindPaths(_arenaMap, _monster.Position, _monster.DetectionRange, entityBlockingPath)
                .All(p => p.Value != Player.Position);
        }

        private Point LookRandomPoint()
        {
            var rnd = new Random();
            var paths = BFS.FindPaths(_arenaMap, _monster.Position, _monster.AP);
            var count = 0;
            while (true)
            {
                if (count > 750 || rnd.NextDouble() > 0.35)
                    return _monster.Position;
                foreach (var path in paths)
                {
                    if (rnd.NextDouble() > 0.90)
                        return path.Value;
                    count++;
                }
            }
        }
    }
}