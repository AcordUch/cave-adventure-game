using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace Cave_Adventure
{
    public class ArenaMap
    {
        public CellType[,] Arena { get; private set; }
        public Player Player { get; private set; }
        public Monster[] Monsters { get; private set; }
        public bool PlayerSelected { get; set; }

        public int Width => Arena.GetLength(0);
        public int Height => Arena.GetLength(1);

        public ArenaMap(CellType[,] arena, Player player, Monster[] monsters)
        {
            Arena = arena;
            Player = player;
            Monsters = monsters;
        }
        
        

        #region CreatingArenaMap
        public static ArenaMap CreateNewArenaMap(string textMap)
        {
            return CreateNewArenaMap(ArenaParser.ParsingMap(textMap));
        }
        
        public static ArenaMap CreateNewArenaMap(string[] textMap)
        {
            return CreateNewArenaMap(ArenaParser.ParsingMap(textMap));
        }

        private static ArenaMap CreateNewArenaMap(
            (CellType[,] arenaMap, Point playerPosition, Point[] monstersPosition) arenaInfo)
        {
            var newPlayer = new Player(arenaInfo.playerPosition);
            var newMonsters = arenaInfo.monstersPosition.Select(e => new Monster(e)).ToArray();
            //Переделать под фабрику, что бы получать классы монстров
            return new ArenaMap(arenaInfo.arenaMap, newPlayer, newMonsters);
        }
        #endregion
        
        public bool InBounds(Point point)
        {
            var bounds = new Rectangle(0, 0, Arena.GetLength(0), Arena.GetLength(1));
            return bounds.Contains(point);
        }
    }
}