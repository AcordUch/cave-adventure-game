using System;
using System.Collections;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cave_Adventure.Views;

namespace Cave_Adventure
{
    public class ArenaMap
    {
        public CellType[,] Arena { get; private set; }
        public Player Player { get; private set; }
        public Monster[] Monsters { get; private set; }
        public bool PlayerSelected { get; set; }
        public bool IsPlayerTurnNow { get; private set; } = true;
        public bool AttackButtonPressed { get; set; }
        public SinglyLinkedList<Point>[] PlayerPaths { get; private set; }
        
        public int Step { get; private set; } = 1;

        public int Width => Arena.GetLength(0);
        public int Height => Arena.GetLength(1);
        public event Action ChangeStateOfUI;
        public event Action AllMonsterDead;
        public event Action PlayerDead;
        
        public ArenaMap(CellType[,] arena, Player player, Monster[] monsters)
        {
            Arena = arena;
            Player = player;
            Monsters = monsters;
        }

        public void SetPlayerPaths(SinglyLinkedList<Point>[] paths)
        {
            if(!PlayerSelected)
            {
                PlayerPaths = paths;
                PlayerSelected = true;
            }
        }

        public async void NextTurn()
        {
            BlockUnblockUI();
            Player.ResetAP();
            Player.IsSelected = false;
            PlayerPaths = null;
            PlayerSelected = false;
            await MonsterTurnController();
            Step += 1;
            BlockUnblockUI();
            // IsPlayerTurnNow = !IsPlayerTurnNow;

        }
        
        public void Attacking(Entity attacker, Point targetPoint)
        {
            var target = GetListOfEntities().FirstOrDefault(p => p.Position == targetPoint);
            if(target != null)
                Attacking(attacker, target);
        }

        public void Attacking(Entity attacker, Entity target)
        {
            if(attacker.AP > 0)
            {
                target.Defending(attacker);
                if(Player.IsDead)
                {
                    PlayerDead?.Invoke();
                    return;
                }
                if(Monsters.All(m => m.IsDead))
                    AllMonsterDead?.Invoke();
            }
        }

        private void BlockUnblockUI()
        {
            IsPlayerTurnNow = !IsPlayerTurnNow;
            ChangeStateOfUI?.Invoke();
        }

        private Task MonsterTurnController()
        {
            var monsters = Monsters.ToList().OrderBy(m => m.Initiative);
            return MoveEntityControl(monsters);
        }

        public async void MovePlayerAlongThePath(Point targetPoint)
        {
            if (PlayerSelected && Player.AP > 0)
            {
                var path = (PlayerPaths
                        .FirstOrDefault(p => p.Value == targetPoint) 
                            ?? throw new InvalidOperationException("Среди доступных точек нет необходимой. В методе откуда вызов нет проверки?"))
                    .Select(p => p).Reverse().ToArray();
                await StartMoveEntity(path, Player);
            }
        }
        
        private Task MoveEntityControl(IEnumerable<Entity> entities)
        {
            var task = new Task(() =>
            {
                foreach (var entity in entities)
                {
                    entity.IsSelected = true;
                    MoveEntityAlongThePath(entity.Position + new Size(0, 1), entity);
                    while (true)
                    {
                        if(!entity.IsSelected)
                            break;
                    }
                    entity.ResetAP();
                }
            });
            task.Start();
            return task;
        }

        private async void MoveEntityAlongThePath(Point targetPoint, Entity entity)
        {
            if(entity.IsSelected)
            {
                var path = new Point[0];
                try
                {
                    path = (BFS.FindPaths(this, entity.Position, entity.AP)
                                .FirstOrDefault(p => p.Value == targetPoint)
                            ?? throw new InvalidOperationException(
                                "Среди доступных точек нет необходимой. В методе откуда вызов нет проверки?"))
                        .Select(p => p).Reverse().ToArray();
                }
                catch
                {
                    // ignored
                }

                await StartMoveEntity(path, entity);
            }
        }
        
        private Task StartMoveEntity(IEnumerable<Point> path, Entity entity)
        {
            var task = new Task(() =>
            {
                foreach (var point in path)
                {
                    if (point != entity.Position)
                        entity.SetTargetPoint(point);
                    
                    while (entity.IsMoving)
                    {
                    }
                }
                entity.IsSelected = false;
            });
            task.Start();
            return task;
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
            (CellType[,] arenaMap, Player player, Monster[] monsters) arenaInfo)
        {
            return new ArenaMap(arenaInfo.arenaMap, arenaInfo.player, arenaInfo.monsters);
        }
        #endregion
        
        public bool InBounds(Point point)
        {
            var bounds = new Rectangle(0, 0, Arena.GetLength(0), Arena.GetLength(1));
            return bounds.Contains(point);
        }
        
        public List<Entity> GetListOfEntities()
        {
            var entities = new List<Entity> { Player };
            entities.AddRange(Monsters);
            return entities;
        }

        public void CompleteLevel(CheatMenu cheatMenu)
        {
            if(cheatMenu.ArenaMap == this)
            {
                AllMonsterDead?.Invoke();
            }
        }
    }
}