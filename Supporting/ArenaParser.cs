using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;

namespace Cave_Adventure
{
    public class ArenaParser
    {
        private static readonly Dictionary<string, Func<Point, Monster>> StringCodeToEntity =
            new()
            {
                ["Sp"] = point => new Spider(point){ Tag = MonsterType.Spider },
                ["Sn"] = point => new Snake(point){ Tag = MonsterType.Snake }
            };
        
        public static (CellType[,] arenaMap, Player player, Monster[] monsters) ParsingMap(string arena)
        {
            var lines = arena.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            return ParsingMap(lines);
        }

        public static (CellType[,] arenaMap, Player player, Monster[] monsters) ParsingMap(string[] arena)
        {
            return ParsingMap(SplitOnCell(arena));
        }

        public static (CellType[,] arenaMap, Player player, Monster[] monsters) ParsingMap(string[,] map)
        {
            var arena = new CellType[map.GetLength(1), map.GetLength(0)];
            var player = new Player(new Point().NegativePoint());
            var monsters = new List<Monster>();
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
                        player = new Player(new Point(x, y));
                        break;
                    case "Sp":
                    case "Sn":
                        arena[x, y] = CellType.Floor;
                        monsters.Add(StringCodeToEntity[cell].Invoke(new Point(x, y)));
                        break;
                    default:
                        throw new ArgumentException("Неизвестный тип клетки");
                }
            }

            // if (player.Position.X < 0) throw new WarningException("На карте нет игрока");
            return (arenaMap: arena, player: player, monsters: monsters.ToArray());
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