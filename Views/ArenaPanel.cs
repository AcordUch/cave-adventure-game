using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Cave_Adventure.Interfaces;
using Cave_Adventure.Views;

namespace Cave_Adventure
{
    public class ArenaPanel : Panel, IPanel
    {
        public readonly ArenaFieldControl ArenaFieldControl;
        private readonly List<Keys> _pressedKeys = new List<Keys>();
        private readonly string[] _levels;
        private readonly Game _game;
        private readonly HealBar _healBar;
        private InventoryPanel _inventoryPanel;
        private Label _infoLabel;
        private Button _nextTurnButton;
        private Button _attackMonsterButton;
        private Button _nextLevelButton;
        private Button _inspectEntityButton;
        private bool _configured = false;
        private bool _UIBlocked = false;
        private bool _needInspect = false;
        private int _currentArenaId;

        public int CurrentArenaId
        {
            get => _currentArenaId;
            private set => _currentArenaId = value >= _levels.Length ? 0 : value;
        }

        public ArenaPanel(Game game)
        {
            _game = game;
            _levels = LoadLevels().ToArray();

            ArenaFieldControl = new ArenaFieldControl();
            ArenaFieldControl.BindEvent += OnBindArenaMapEvent;
            ArenaFieldControl.ClickOnPoint += ArenaFieldControl_ClickOnPoint;
            
            _healBar = new HealBar()
            {
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            ConfigureTables(table);

            ArenaFieldControl.KeyDown += OnKeyDown;
            ArenaFieldControl.KeyUp += OnKeyUp;
            
            Controls.Add(table);
        }

        protected override void InitLayout()
        {
            base.InitLayout();
            ResizeRedraw = true;
            DoubleBuffered = true;
        }

        void IPanel.Configure() => Configure();

        public void Configure(string arenaMap = null)
        {
            if (_configured)
                throw new InvalidOperationException();

            arenaMap ??= _levels[0];
            ArenaFieldControl.Configure(arenaMap);
            _healBar.Configure(ArenaFieldControl.ArenaMap);
            CurrentArenaId = Array.IndexOf(_levels, arenaMap);
            if(_UIBlocked)
                OnBlockUnblockUI();
            _configured = true;
        }

        public void Drop()
        {
            _configured = false;
            ArenaFieldControl.Drop();
        }

        public new void Update()
        {
            if (!_configured)
                return;

            ArenaFieldControl.Update();
            _healBar.Invalidate();
            _inventoryPanel.Update();
            //Вынести в метод OnSizeChange
            var zoom = GetZoomForController();
            ArenaFieldControl.Size =
                new Size((int)(ArenaFieldControl.Width * zoom), (int)(ArenaFieldControl.Height * zoom));

            _infoLabel.Size = new Size((int)(Width * 0.25), (int)(Height * 0.4));
            _infoLabel.Text = ArenaFieldControl.PlayerInfoToString();
        }

        #region ClickOnPointHandler
        
        private void ArenaFieldControl_ClickOnPoint(Point point, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                if (!ArenaFieldControl.ArenaMap.IsPlayerTurnNow)
                    return;
                
                var actionCompleted = false;

                if (_needInspect)
                {
                   InspectMonster(point);
                    actionCompleted = true;
                }

                if (point == ArenaFieldControl.Player.Position && !actionCompleted)
                {
                    if (ArenaFieldControl.Player.IsSelected)
                    {
                        UnselectPlayer();
                    }
                    else
                    {
                        SelectPlayer();
                    }
                    actionCompleted = true;
                }
                
                if (ArenaFieldControl.Player.IsSelected && !actionCompleted)
                {
                    if (!actionCompleted && ArenaFieldControl.ArenaMap.Monsters.Any(m => m.Position == point && m.IsAlive))
                    {
                        AttackMonster(point);
                        actionCompleted = true;
                    }
                    
                    if (!actionCompleted && ArenaFieldControl.ArenaMap.PlayerPaths.Any(p => p.Contains(point)))
                    {
                        MovePlayer(point);
                        actionCompleted = true;
                    }
                }
            }
        }

        private void InspectMonster(Point point)
        {
            var entity = ArenaFieldControl.ArenaMap.GetListOfEntities()
                .Where(m => m.Position == point)
                .Select(m => m)
                .FirstOrDefault();
            if(entity != null)
            {
                var entityDescription = new EntityDescription(entity);
                entityDescription.Show();
            }

            _needInspect = false;
        }
        
        private void UnselectPlayer()
        {
            ArenaFieldControl.ArenaMap.PlayerSelected = false;
            ArenaFieldControl.Player.IsSelected = false;
            ArenaFieldControl.ArenaPainter.Update();        
        }

        private void SelectPlayer()
        {
            if (ArenaFieldControl.Player.GetNeighbors()
                .Any(neighborsPos => ArenaFieldControl.Monsters.Any(monster => monster.Position == neighborsPos)))
            {
                _attackMonsterButton.Enabled = true;
            }

            var path = BFS.FindPaths(
                ArenaFieldControl.ArenaMap,
                ArenaFieldControl.Player.Position,
                ArenaFieldControl.Player.AP).ToArray();
            ArenaFieldControl.ArenaMap.SetPlayerPaths(path);

            ArenaFieldControl.Player.IsSelected = true;
            ArenaFieldControl.ArenaPainter.Update();
        }

        private void AttackMonster(Point point)
        {
            ArenaFieldControl.ArenaMap.Attacking(ArenaFieldControl.Player, point);
            ArenaFieldControl.ArenaMap.AttackButtonPressed = false;
            ArenaFieldControl.ArenaMap.PlayerSelected = false;
            ArenaFieldControl.Player.IsSelected = false;
            _attackMonsterButton.Enabled = false;
        }

        private void MovePlayer(Point point)
        {
            ArenaFieldControl.ArenaMap.MovePlayerAlongThePath(point);
            ArenaFieldControl.ArenaMap.PlayerSelected = false;
            _attackMonsterButton.Enabled = false;
        }

        #endregion
        
        private static IEnumerable<String> LoadLevels()
        {
            yield return Properties.Resources.Arena1;
            yield return Properties.Resources.Arena2;
            yield return Properties.Resources.Arena3;
            yield return Properties.Resources.Arena4;
            yield return Properties.Resources.Arena5;
            yield return Properties.Resources.Arena6;
            yield return Properties.Resources.Arena7;
        }

        #region Настройка Панелей

        private void ConfigureTables(TableLayoutPanel table)
        {
            var levelMenu = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackColor = Color.Red,
                Padding = new Padding(25, 10, 0, 0),
                Font = new Font(SystemFonts.DialogFont.FontFamily, 12)
            };
            SetUpLevelSwitch(levelMenu);

            _nextTurnButton = new Button()
            {
                Text = $"Следующий ход",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Size = new Size(350, 50),
                AutoSize = true
            };
            _nextTurnButton.Click += ClickOnNextTurnButton;

            var backToMenuButton = new Button()
            {
                Text = $"Назад в меню",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Size = new Size(350, 50),
                AutoSize = true
            };
            backToMenuButton.Click += _game.SwitchOnMainMenu;
            
            _nextLevelButton = new Button()
            {
                Text = $"Следующий уровень",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Size = new Size(350, 50),
                AutoSize = true,
                Enabled = false
            };
            _nextLevelButton.Click += OnNextLevelButtonClick;

            _attackMonsterButton = new Button()
            {
                Text = $"Атака!",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Size = new Size(350, 50),
                AutoSize = true,
                Enabled = false
            };
            _attackMonsterButton.Click += OnAttackButtonClick;

            _inspectEntityButton = new Button()
            {
                Text = $"Осмотреть",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Size = new Size(350, 50),
                AutoSize = true,
                Enabled = true
            };
            _inspectEntityButton.Click += OnInspectEntityButtonClick;

            var infoPanel = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.LeftToRight,
                Dock = DockStyle.Fill,
                AutoSize = true,
                Padding = new Padding(20, 0, 0, 0),
                Font = new Font(SystemFonts.DialogFont.FontFamily, 10)
            };
            SetUpInfoPanel(infoPanel);

            _inventoryPanel = new InventoryPanel(ArenaFieldControl)
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                Font = new Font(SystemFonts.DialogFont.FontFamily, 10)
            };

            var arenaLayoutPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                Padding = new Padding(100, 30, 0, 50),
            };
            var bottomTable = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            var secondColumnTable = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            var thirdColumnTable = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true
            };

            #region AddingControls

            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            secondColumnTable.RowStyles.Add(new RowStyle(SizeType.Percent, 86));
            secondColumnTable.RowStyles.Add(new RowStyle(SizeType.Percent, 14));
            secondColumnTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            thirdColumnTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            thirdColumnTable.RowStyles.Add(new RowStyle(SizeType.Percent, 40));
            thirdColumnTable.RowStyles.Add(new RowStyle(SizeType.Percent, 40));
            thirdColumnTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            bottomTable.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            bottomTable.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            bottomTable.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            bottomTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            bottomTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            bottomTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));

            arenaLayoutPanel.Controls.Add(ArenaFieldControl);
            bottomTable.Controls.Add(backToMenuButton, 0, 2);
            bottomTable.Controls.Add(_nextLevelButton, 0, 1);
            bottomTable.Controls.Add(_attackMonsterButton, 2, 0);
            bottomTable.Controls.Add(_inspectEntityButton, 2, 1);
            bottomTable.Controls.Add(_nextTurnButton, 2, 2);
            secondColumnTable.Controls.Add(arenaLayoutPanel, 0, 0);
            secondColumnTable.Controls.Add(bottomTable, 0, 1);
            thirdColumnTable.Controls.Add(_healBar, 0, 0);
            thirdColumnTable.Controls.Add(infoPanel, 0, 1);
            thirdColumnTable.Controls.Add(_inventoryPanel, 0, 2);
            table.Controls.Add(levelMenu, 0, 0);
            table.Controls.Add(secondColumnTable, 1, 0);
            table.Controls.Add(thirdColumnTable, 2, 0);

            #endregion
        }

        private void SetUpLevelSwitch(Control menuPanel)
        {
            menuPanel.Controls.Add(new Label
            {
                Text = "Выберите Арену:",
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleLeft,
                Size = new Size(350, 50),
                AutoSize = true,
                Margin = new Padding(0, 25, 0, 0)
            });

            var linkLabels = new List<LinkLabel>();
            for (var i = 0; i < _levels.Length; i++)
            {
                var arenaId = i;
                var link = new LinkLabel
                {
                    Text = $"Арена {i + 1}",
                    ActiveLinkColor = Color.LimeGreen,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(100, 35),
                    AutoSize = true,
                    Margin = new Padding(0, 20, 0, 5),
                    Tag = _levels[arenaId]
                };
                link.LinkClicked += (sender, args) =>
                {
                    ArenaFieldControl.LoadLevel(_levels[arenaId]);
                    UpdateLinksColors(_levels[arenaId], linkLabels);
                    _nextLevelButton.Enabled = false;
                    if(_UIBlocked)
                        OnBlockUnblockUI();
                };
                menuPanel.Controls.Add(link);
                linkLabels.Add(link);
            }
            UpdateLinksColors(_levels[0], linkLabels);
        }

        private static void UpdateLinksColors(string level, List<LinkLabel> linkLabels)
        {
            foreach (var linkLabel in linkLabels)
            {
                linkLabel.LinkColor = (string)linkLabel.Tag == level ? Color.LimeGreen : Color.Black;
            }
        }

        private void SetUpInfoPanel(Control infoPanel)
        {
            infoPanel.Controls.Add(new Label
            {
                Text = "Информация о персонаже:",
                ForeColor = Color.Black,
                Size = new Size(350, 30),
                Margin = new Padding(0, 20, 0, 0)
            });
            _infoLabel = new Label
            {
                Text = $"AP: {ArenaFieldControl.PlayerInfoToString()}",
                ForeColor = Color.Black,
                Size = new Size(450, (int)(Height * 0.2)),
                Margin = new Padding(10, 0, 0, 0)
            };
            infoPanel.Controls.Add(_infoLabel);
        }
        #endregion

        #region EventHandlers
        
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            _pressedKeys.Add(e.KeyCode);
            if (_pressedKeys.Contains(Keys.ControlKey) && _pressedKeys.Contains(Keys.Oemtilde) && _pressedKeys.Count == 2)
            {
                if(!Application.OpenForms.OfType<CheatMenu>().Any())
                {
                    var cheatMenu = new CheatMenu();
                    cheatMenu.Configure(ArenaFieldControl);
                    cheatMenu.Show();
                }
                _pressedKeys.Clear();
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            _pressedKeys.Remove(e.KeyCode);
        }
        
        private void OnBlockUnblockUI()
        {
            _inventoryPanel.OnBlockUnblockUI();
            _nextTurnButton.Enabled = !_nextTurnButton.Enabled;
            _UIBlocked = !_UIBlocked;
        }
        
        private void OnBindArenaMapEvent()
        {
            ArenaFieldControl.ArenaMap.ChangeStateOfUI += OnBlockUnblockUI;
            ArenaFieldControl.ArenaMap.AllMonsterDead += OnAllMonsterDead;
            ArenaFieldControl.ArenaMap.PlayerDead += OnPlayerDead;
            _healBar.Configure(ArenaFieldControl.ArenaMap);
        }

        public void OnSetCurrentArenaId(int arenaId)
        {
            CurrentArenaId = arenaId;
        }
        
        private void ClickOnNextTurnButton(object sender, EventArgs e)
        {
            ArenaFieldControl.ArenaMap.NextTurn();
        }
        
        private void OnAttackButtonClick(object sender, EventArgs e)
        {
            ArenaFieldControl.ArenaMap.AttackButtonPressed = true;
        }

        private void OnNextLevelButtonClick(object sender, EventArgs e)
        {
            CurrentArenaId++;
            ArenaFieldControl.LoadLevel(_levels[_currentArenaId]);
            _nextLevelButton.Enabled = false;
        }

        private void OnInspectEntityButtonClick(object sender, EventArgs e)
        {
            _needInspect = !_needInspect;
        }

        private void OnAllMonsterDead()
        {
            _nextLevelButton.Enabled = true;
            MessageBox.Show(
                "Ты выиграл!\nСледуй дальше",
                "Победа!",
                MessageBoxButtons.OK,
                MessageBoxIcon.None,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
        }

        private void OnPlayerDead()
        {
            var timer = new System.Timers.Timer{Interval = GlobalConst.AnimTimerInterval / 2};
            timer.Elapsed += (_, __) =>
            {
                timer.Stop();
                MessageBox.Show(
                    "Ты умер :с!\nДавай по новой",
                    "Не повезло, не повезло",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                ArenaFieldControl.LoadLevel(_levels[CurrentArenaId]);
                timer.Close();
            };
            timer.Start();
        }
        
        #endregion

        private double GetZoomForController()
        {
            return ArenaFieldControl.Height != 0 && ClientSize.Height != 0
                ? (double)ClientSize.Height / ArenaFieldControl.Height
                : 1;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(Color.White);
        }
    }
}