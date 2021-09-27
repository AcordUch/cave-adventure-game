using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Cave_Adventure
{
    public class ArenaGenerator
    {
        private static readonly Dictionary<CellSubtype, string> CellSubtypeToStringCode =
            new()
            {
                [CellSubtype.Wall1] = "#1",
                [CellSubtype.FloorStone2] = "  "
            };
        
        private static readonly Dictionary<EntityType, string> EntitiesTypeToStringCode =
            new()
            {
                [EntityType.Slime] = "Sl",
                [EntityType.Spider] = "Sp",
                [EntityType.Snake] = "Sn",
                [EntityType.Golem] = "Go",
                [EntityType.Ghoul] = "Gh",
                [EntityType.Witch] = "Wi",
                [EntityType.Minotaur] = "Mi",
                [EntityType.Player] = "P "
            };

        private Random _rnd;
        private int _width = 12;
        private int _height = 7;
        private string[,] _draftArray;
        private bool _playerWasPlaced = false;

        public string CreateArena()
        {
            _rnd = new Random();
            CreateDraftArray();
            var res = new StringBuilder();
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    res.Append(_draftArray[y, x]).Append('.');
                }
                res.Append("\r\n");
            }
            _draftArray = null;
            return res.ToString();
        }

        private void CreateDraftArray()
        {
            _draftArray = new string[_height, _width];
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _draftArray[y, x] = LookCellType(x, y);
                }
            }
            PutPlayer();
        }

        private void PutPlayer()
        {
            while(true)
            {
                var plPnt = new Point(_rnd.Next(0, _width), _rnd.Next(0, 3));
                if(_draftArray[plPnt.Y, plPnt.X] == "  ")
                {
                    _draftArray[plPnt.Y, plPnt.X] = "P ";
                    break;
                }
            }
        }

        private string LookCellType(int x, int y)
        {
            if (y > 1 && y <= _height - 2 && _rnd.Next(0, 100) < 15)
                return "#1";
            return "  ";
        }
    }
}