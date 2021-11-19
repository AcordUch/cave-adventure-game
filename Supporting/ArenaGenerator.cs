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
        
        private static readonly Dictionary<int, string> EntityByDigitCode =
            new()
            {
                [0] = "Sl",
                [1] = "Sp",
                [2] = "Sn",
                [3] = "Go",
                [4] = "Gh",
                [5] = "Wi",
                [6] = "Mi",
            };
        
        private Random _rnd;
        private string[,] _draftArray;
        private int _width = 12;
        private int _height = 7;
        private int _monstersAmount = 3;
        private List<Monster> _monstersToPlace = new List<Monster>();
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
        
        private string LookCellType(int x, int y)
        {
            if (y > 1 && y <= _height - 2 && _rnd.Next(0, 100) < 15)
                return "#1";
            return "  ";
        }

        private void PutPlayer()
        {
            while(!_playerWasPlaced)
            {
                var plPnt = new Point(_rnd.Next(0, _width), _rnd.Next(0, 3));
                if(_draftArray[plPnt.Y, plPnt.X] == "  ")
                {
                    _draftArray[plPnt.Y, plPnt.X] = "P ";
                    _playerWasPlaced = true;
                }
            }
        }

        private void PutMonsters()
        {
            throw new NotImplementedException();
        }
    }
}