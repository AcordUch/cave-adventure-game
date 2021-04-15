using System;
using System.Drawing;
using System.Collections.Generic;

namespace Cave_Adventure
{
    public class ArenaMap
    {
        public readonly CellType[,] Arena;
        public readonly Player Player;
        public readonly IMonster[] Monsters;

        public ArenaMap(CellType[,] arena, Player player, IMonster[] monsters)
        {
            Arena = arena;
            Player = player;
            Monsters = monsters;
        }
        
        //ToDo late ...
        
        public bool InBounds(Point point)
        {
            var bounds = new Rectangle(0, 0, Arena.GetLength(0), Arena.GetLength(1));
            return bounds.Contains(point);
        }
    }
}