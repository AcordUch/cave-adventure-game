using System;
using System.Collections;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cave_Adventure.Objects.Items;
using Cave_Adventure.Views;
using Timer = System.Timers.Timer;

namespace Cave_Adventure
{
    public class ArenaMap
    {
        private Entity _currentAttacker;
        private Entity _currentDefender;
        
        public (CellType cellType, CellSubtype cellSubtype)[,] Arena { get; private set; }
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
        
        public ArenaMap((CellType, CellSubtype)[,] arena, Player player, Monster[] monsters)
        {
            Arena = arena;
            Player = player;
            Monsters = monsters;
            foreach (var monster in Monsters)
            {
                monster.AI.Configure(this);
            }
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
            Player.IsSelected = false;
            PlayerPaths = null;
            PlayerSelected = false;
            var monsters = Monsters.OrderByDescending(m => m.Initiative);
            await MonsterMoveControl(monsters);
            await MonsterAttackController(monsters);
            foreach (var monster in monsters)
            {
                if(monster.IsAlive)
                    monster.ResetAP();
            }
            Player.ResetAP();
            Step += 1;
            BlockUnblockUI();
        }

        public void Attacking(Entity attacker, Point targetPoint)
        {
            var target = GetListOfEntities().FirstOrDefault(p => p.Position == targetPoint);
            if(target != null)
                Attacking(attacker, target);
        }

        public void Attacking(Entity attacker, Entity target)
        {
            _currentAttacker = attacker;
            _currentDefender = target;
            _currentAttacker.EntityDied += AddHeal;
            _currentDefender.EntityDied += AddHeal;
            if(attacker.AP > 0)
            {
                target.Defending(attacker);
                if(Player.IsDead)
                {
                    PlayerDead?.Invoke();
                    return;
                }
                CheckOnWinning();
            }
        }

        public void AddHeal()
        {
            var baseRandom = new Random();
            var rnd = new Random(baseRandom.Next() + 54356237);
            var rndNext = rnd.NextDouble();
            switch (rndNext)
            {
                case > 0.9:
                    Player.Inventory.AddHeals(new HealthPotionBig());
                    break;
                case > 0.75:
                    Player.Inventory.AddHeals(new HealthPotionMedium());
                    break;
                case > 0.55:
                    Player.Inventory.AddHeals(new HealthPotionSmall());
                    break;
            }
        }

        public void CheckOnWinning()
        {
            if(Monsters.All(m => m.IsDead) || Monsters.Length == 0)
                AllMonsterDead?.Invoke();
        }

        private void BlockUnblockUI()
        {
            IsPlayerTurnNow = !IsPlayerTurnNow;
            if(IsPlayerTurnNow)
                CheckOnWinning();
            ChangeStateOfUI?.Invoke();
        }

        private Task MonsterAttackController(IEnumerable<Entity> entities)
        {
            var task = new Task(() =>
            {
                foreach (var entity in entities)
                {
                    if(entity.IsDead || entity.AP == 0 
                                     || !GlobalConst.PossibleDirections.Any(p => entity.Position + p == Player.Position))
                        continue;
                    var flag = true;
                    var timer = new Timer {Interval = 2 * GlobalConst.AnimTimerInterval + 200, AutoReset = false};
                    timer.Elapsed += (_, __) =>
                    {
                        flag = false;
                        timer.Stop();
                    };
                    timer.Start();
                    Attacking(entity, Player);
                    while (flag)
                    {
                        
                    }
                }
            });
            task.Start();
            return task;
        }
        
        public async void MovePlayerAlongThePath(Point targetPoint)
        {
            if (PlayerSelected && Player.AP > 0)
            {
                ChangeStateOfUI?.Invoke();
                var path = (PlayerPaths
                        .FirstOrDefault(p => p.Value == targetPoint) 
                            ?? throw new InvalidOperationException("Среди доступных точек нет необходимой. В методе откуда вызов нет проверки?"))
                    .Select(p => p).Reverse().ToArray();
                await StartMoveEntity(path, Player);
                ChangeStateOfUI?.Invoke();
            }
        }
        
        private Task MonsterMoveControl(IEnumerable<Monster> monsters)
        {
            var task = new Task(() =>
            {
                foreach (var monster in monsters)
                {
                    if(monster.IsDead)
                        continue;
                    monster.IsSelected = true;
                    MoveEntityAlongThePath(monster.AI.LookTargetMovePoint(), monster);
                    while (true)
                    {
                        if(!monster.IsSelected)
                            break;
                    }
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
            ((CellType, CellSubtype)[,] arenaMap, Player player, Monster[] monsters) arenaInfo)
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