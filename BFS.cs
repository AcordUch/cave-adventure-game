using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;

namespace Cave_Adventure
{
    public class BFS
    {
        public static IEnumerable<SinglyLinkedList<Point>> FindPaths(ArenaMap map, Point start, int range)
        {
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
                    if (currentPoint.distance >= range || (dy == 0 && dx == 0) || (Math.Abs(dy) == 1 && Math.Abs(dx) == 1)) continue;
                    var nextPoint = new Point(currentPoint.point.Value.X + dx, currentPoint.point.Value.Y + dy);
                    if (usedPoint.Contains(nextPoint) || !map.InBounds(nextPoint) ||
                        map.Arena[nextPoint.X, nextPoint.Y] != CellType.Floor ||
                        map.Monsters.Any(p => p.Position == nextPoint)) continue;
                    // if(Math.Abs(dy) == 1 && Math.Abs(dx) == 1
                    //     && map.Arena[nextPoint.X, currentPoint.point.Value.Y] == CellType.Wall
                    //     && map.Arena[currentPoint.point.Value.X, nextPoint.Y] == CellType.Wall) continue;
                    queue.Enqueue((new SinglyLinkedList<Point>(nextPoint, currentPoint.point), currentPoint.distance + 1));
                    usedPoint.Add(nextPoint);
                }
            }
        }
    }
}