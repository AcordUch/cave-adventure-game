using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Cave_Adventure
{
    public class AStarPF
    {
        private const double GrafCost = 1;
        
        public static SinglyLinkedList<Point> FindPathToPlayer(ArenaMap map, Point start, int range)
        {
            var breakWhile = false;
            
            var track = new Dictionary<Point, ((Point previous, int range) prevRangePair, (double priority, double cost) priorCostPair)>
            {
                [start] = ((new Point(-1, -1), -1), (0d, 0d))
            };
            var visitedPoints = new HashSet<Point>();
            var lastPoint = new Point().NegativePoint();

            while (true)
            {
                var toOpen = new Point(-1, -1);
                var bestPrise = double.MaxValue;
                foreach (var pair in track.Where(p => !visitedPoints.Contains(p.Key)))
                {
                    if (pair.Value.prevRangePair.range < range && pair.Value.priorCostPair.priority < bestPrise)
                    {
                        bestPrise = pair.Value.priorCostPair.priority;
                        toOpen = pair.Key;
                    }
                }
                
                if(toOpen.X == -1 || track[toOpen].prevRangePair.range + 1 >= range)
                {
                    lastPoint = toOpen;
                    break;
                }
                
                for (int dy = -1; dy < 2; dy++)
                {
                    for (int dx = -1; dx < 2; dx++)
                    {
                        if ((dy == 0 && dx == 0) || (Math.Abs(dy) == 1 && Math.Abs(dx) == 1)) continue;
                        var nextPoint = new Point(toOpen.X + dx, toOpen.Y + dy);
                        if (visitedPoints.Contains(nextPoint) || !map.InBounds(nextPoint) ||
                            map.Arena[nextPoint.X, nextPoint.Y] != CellType.Floor ||
                            map.GetListOfEntities().Any(p => p.Position == nextPoint && p.IsAlive)) continue;
                        var currentPrice = track[toOpen].priorCostPair.cost + GrafCost;
                        if (!track.ContainsKey(nextPoint) || track[nextPoint].priorCostPair.cost > currentPrice)
                        {
                            var priority = currentPrice + Heuristic(nextPoint, map.Player.Position);
                            track[nextPoint] = ((toOpen, track[toOpen].prevRangePair.range + 1),
                                (priority, currentPrice));
                            lastPoint = nextPoint;
                            if (GlobalConst.PossibleDirections.Any(p => map.Player.Position + p == nextPoint))
                            {
                                breakWhile = true;
                                break;
                            }
                        }
                    }
                    if(breakWhile)
                        break;
                }
                visitedPoints.Add(toOpen);
                if(breakWhile)
                    break;
            }

            return GetPathList(track, lastPoint);
        }
        
        private static double Heuristic(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
        
        private static SinglyLinkedList<Point> GetPathList
        (Dictionary<Point, ((Point previous, int range) prevRangePair, (double priority, double cost) priorCostPair)> track,
            Point end)
        {
            var result = new SinglyLinkedList<Point>(end);
            var first = true;
            while (end.X != -1)
            {
                if (first)
                {
                    end = track[end].prevRangePair.previous;
                    first = false;
                }

                result = new SinglyLinkedList<Point>(end, result);
                end = track[end].prevRangePair.previous;
            }
            
            return result;
        }
    }
}