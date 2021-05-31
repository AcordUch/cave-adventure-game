using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Net;

namespace Cave_Adventure
{
    public class BFS
    {
        public static IEnumerable<SinglyLinkedList<Point>> FindPaths(ArenaMap map, Point start, int range, bool entityBlockingPath = true)
        {
            //В "голове" SinglyLinkedList лежит конечная точка
            var queue = new Queue<(SinglyLinkedList<Point> point, int distance)>();
            var usedPoint = new HashSet<Point>();
            queue.Enqueue((new SinglyLinkedList<Point>(start), 0));
            usedPoint.Add(start);
            while (queue.Count != 0)
            {
                var currentPoint = queue.Dequeue();
                if(currentPoint.distance != 0)
                    yield return currentPoint.point;
                
                for(var dy = -1; dy <= 1; dy++)
                for (var dx = -1; dx <= 1; dx++)
                {
                    if (currentPoint.distance >= range || (dy == 0 && dx == 0) || (Math.Abs(dy) == 1 && Math.Abs(dx) == 1))
                            continue;
                    var nextPoint = new Point(currentPoint.point.Value.X + dx, currentPoint.point.Value.Y + dy);
                    if (usedPoint.Contains(nextPoint) || !map.InBounds(nextPoint) ||
                        map.Arena[nextPoint.X, nextPoint.Y].cellType != CellType.Floor) 
                        continue;
                    if (entityBlockingPath && map.GetListOfEntities().Any(p => p.Position == nextPoint && p.IsAlive))
                        continue;
                    queue.Enqueue((new SinglyLinkedList<Point>(nextPoint, currentPoint.point), currentPoint.distance + 1));
                    usedPoint.Add(nextPoint);
                }
            }
        }
        
        public static Point FindFarPoint(ArenaMap map, Point enemyPos, Entity entity)
        {
            var paths = FindPaths(map, entity.Position, entity.AP).ToArray();
            double maxDist;
            try
            {
                maxDist = paths.Max(p => enemyPos.RangeToPoint(p.Value));
            }
            catch
            {
                return entity.Position;
            }
            var pretenderPoint =  paths
                .First(p => Math.Abs(enemyPos.RangeToPoint(p.Value) - maxDist) < 1e-9)
                .Value;
            return enemyPos.RangeToPoint(pretenderPoint) > enemyPos.RangeToPoint(entity.Position)
                ? pretenderPoint
                : entity.Position;
        }
    }
}