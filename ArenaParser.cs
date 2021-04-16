using System;
using System.Drawing;
using System.Collections.Generic;

namespace Cave_Adventure
{
    public class ArenaParser
    {
        public static (CellType[,] arenaMap, Point playerPosition, Point[] monstersPosition) ParsingMap(string arena)
        {
            var lines = arena.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            return ParsingMap(lines);
        }

        public static (CellType[,] arenaMap, Point playerPosition, Point[] monstersPosition) ParsingMap(string[] arena)
        {
            return ParsingMap(SplitOnCell(arena));
        }

        public static (CellType[,] arenaMap, Point playerPosition, Point[] monstersPosition) ParsingMap(string[,] map)
        {
            var arena = new CellType[map.GetLength(1), map.GetLength(0)];
            var playerPos = Point.Empty;
            var monsters = new List<Point>();
            for (int y = 0; y < map.GetLength(0); y++)
            for (int x = 0; x < map.GetLength(1); x++)
            {
                var cell = map[y, x];
                switch (cell)
                {
                    case "# ": arena[x, y] = CellType.Wall; break;
                    case "  ": arena[x, y] = CellType.Floor; break;
                    case "P ":
                        arena[x, y] = CellType.Floor;
                        playerPos = new Point(x, y);
                        break;
                    case "M ":
                        arena[x, y] = CellType.Floor;
                        monsters.Add(new Point(x, y));
                        break;
                    default:
                        throw new ArgumentException("Неизвестный тип клетки");
                }
            }

            return (arenaMap: arena, playerPosition: playerPos, monstersPosition: monsters.ToArray());
        }

        private static string[,] SplitOnCell(string[] map) //переименовать
        {
            if (map.Length == 0) return new string[0, 0];
            
            var result = new string[map.Length, map[0].Length / 3];
            for (int row = 0; row < map.Length; row++)
            {
                var index = 0;
                for (int mChar = 0; mChar < map[0].Length; mChar++)
                {
                    string cell;
                    cell = char.ToString(map[row][mChar]) + char.ToString(map[row][++mChar]);
                    mChar++;
                    result[row, index++] = cell;
                }
            }

            return result;
        }

        public static string[,] PublicGetterForTestsDaYaDurakChtoTakDelau(string[] map)
        {
            return SplitOnCell(map);
        }
    }
}