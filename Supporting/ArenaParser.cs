using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cave_Adventure
{
    public class ArenaParser
    {
        private static readonly Dictionary<string, Func<Point, Monster>> StringCodeToEntity =
            new()
            {
                ["Sl"] = point => new Slime(point),
                ["Sp"] = point => new Spider(point),
                ["Sn"] = point => new Snake(point),
                ["Go"] = point => new Golem(point),
                ["Gh"] = point => new Ghoul(point),
                ["Wi"] = point => new Witch(point),
                ["Mi"] = point => new Minotaur(point)
            };

        public static string PrepareMap(string arena,
            int maxArenaRow = GlobalConst.MaxArenaRow, int maxArenaColumn = GlobalConst.MaxArenaColumn)
        {
            var lines = arena.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            return PrepareMap(lines, maxArenaRow, maxArenaColumn);
        }
        
        public static string PrepareMap(string[] arena,
            int maxArenaRow = GlobalConst.MaxArenaRow, int maxArenaColumn = GlobalConst.MaxArenaColumn)
        {
            var result = new StringBuilder();
            var amountAddRow = (int) Math.Ceiling((maxArenaRow - arena.Length) / 2d);
            var amountAddColumn = (int) Math.Ceiling((maxArenaColumn - arena[0].Length / 3) / 2d);
            var additionalRow = CreatAdditionalRow(maxArenaColumn);
            
            for (int _ = 0; _ < amountAddRow; _++)
            {
                result.Append(additionalRow);
            }
            foreach (var row_ in arena)
            {
                var row = new StringBuilder();
                for (int _ = 0; _ < amountAddColumn; _++)
                {
                    row.Append("#T.");
                }
                row.Append(row_);
                for (int _ = 0; _ < amountAddColumn; _++)
                {
                    row.Append("#T.");
                }
                row.Append("\r\n");
                result.Append(row);
            }
            for (int _ = 0; _ < amountAddRow; _++)
            {
                result.Append(additionalRow);
            }

            return result.ToString();
        }

        private static string CreatAdditionalRow(int maxArenaColumn)
        {
            var additionalRow = new StringBuilder();
            for (int i = 0; i < maxArenaColumn; i++)
            {
                additionalRow.Append("#T.");
            }
            additionalRow.Append("\r\n");
            return additionalRow.ToString();
        }
        
        public static ((CellType, CellSubtype)[,] arenaMap, Player player, Monster[] monsters) ParsingMap(string arena)
        {
            var lines = arena.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            return ParsingMap(lines);
        }

        public static ((CellType, CellSubtype)[,] arenaMap, Player player, Monster[] monsters) ParsingMap(string[] arena)
        {
            return ParsingMap(SplitOnCell(arena));
        }

        public static ((CellType, CellSubtype)[,] arenaMap, Player player, Monster[] monsters) ParsingMap(string[,] map)
        {
            var arena = new (CellType, CellSubtype)[map.GetLength(1), map.GetLength(0)];
            var player = new Player(new Point().NegativePoint());
            var monsters = new List<Monster>();
            for (int y = 0; y < map.GetLength(0); y++)
            for (int x = 0; x < map.GetLength(1); x++)
            {
                var cell = map[y, x];
                switch (cell)
                {
                    case "#T": arena[x, y] = (CellType.Wall, CellSubtype.transparent); break;
                    case "# ":
                    case "#0": arena[x, y] = (CellType.Wall, CellSubtype.wall0); break;
                    case "#1": arena[x, y] = (CellType.Wall, CellSubtype.wall1); break;
                    case "#2": arena[x, y] = (CellType.Wall, CellSubtype.wall2); break;
                    case "#3": arena[x, y] = (CellType.Wall, CellSubtype.wall3); break;
                    case "#4": arena[x, y] = (CellType.Wall, CellSubtype.wall4); break;
                    case "  ": arena[x, y] = (CellType.Floor, CellSubtype.floorStone2); break;
                    case "P ":
                        arena[x, y] = (CellType.Floor, CellSubtype.floorStone2);
                        player = new Player(new Point(x, y));
                        break;
                    case "Sl":
                    case "Sp":
                    case "Sn":
                    case "Go":
                    case "Gh":
                    case "Wi":
                    case "Mi":
                        arena[x, y] = (CellType.Floor, CellSubtype.floorStone2);
                        monsters.Add(StringCodeToEntity[cell].Invoke(new Point(x, y)));
                        break;
                    default:
                        arena[x, y] = (CellType.Floor, CellSubtype.noTexture);
                        break;
                    }
            }
            
            return (arenaMap: arena, player: player, monsters: monsters.ToArray());
        }

        private static string[,] SplitOnCell(string[] map)
        {
            if (map.Length == 0) return new string[0, 0];
            
            var result = new string[map.Length, map[0].Length / 3];
            for (int row = 0; row < map.Length; row++)
            {
                var index = 0;
                for (int mChar = 0; mChar < map[0].Length; mChar++)
                {
                    var cell = char.ToString(map[row][mChar]) + char.ToString(map[row][++mChar]);
                    mChar++;
                    result[row, index++] = cell;
                }
            }

            return result;
        }

        public static string[,] PublicGetterForTests(string[] map)
        {
            return SplitOnCell(map);
        }
    }
}