using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Cave_Adventure.Interfaces;
using Cave_Adventure.Properties;
using Cave_Adventure.Views;

namespace Cave_Adventure
{
    public class ArenaPanel : Panel, IPanel
    {
        public readonly ArenaFieldControl ArenaFieldControl;
        private readonly List<Keys> _pressedKeys = new List<Keys>();
        private readonly string[] _levels;
        private readonly Game _game;
        
        private HealBarPanel _healBarPanel;
        private InventoryPanel _inventoryPanel;
        private PlayerInfoPanel _playerInfoPanel;
        private ComboBox _levelMenu;
        private FlowLayoutPanel _arenaInfoPanel;
        private Button _nextTurnButton;
        private Button _attackMonsterButton;
        private Button _nextLevelButton;
        private Button _inspectEntityButton;
        private Button _backToMenuButton;
        private bool _winFormIsDisplayed = false;
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
            // _levels = GlobalConst.LoadLevels().ToArray();
            _levels = GlobalConst.LoadDebugLevels().ToArray();

            ArenaFieldControl = new ArenaFieldControl()
            {
                BackgroundImage = Resources.andesiteBackground
            };
            ArenaFieldControl.BindEvent += OnBindArenaMapEvent;
            ArenaFieldControl.ClickOnPoint += ArenaFieldControl_ClickOnPoint;
            
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
            _healBarPanel.Invalidate();
            _inventoryPanel.Update();
            _playerInfoPanel.Update();
            //Вынести в метод OnSizeChange
            var zoom = GetZoomForController();
            ArenaFieldControl.Size =
                new Size((int)(ArenaFieldControl.Width * zoom.Width), (int)(ArenaFieldControl.Height * zoom.Height));
            
            _arenaInfoPanel.Controls[1].Text = $"Текущая арена:\n  {_currentArenaId + 1} из {_levels.Length}";
        }

        #region ClickOnPointHandler
        
        private void ArenaFieldControl_ClickOnPoint(Point point, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
               
                //var actionCompleted = false;
                
                if (_needInspect)
                {
                    InspectMonster(point);
                    return;
                }
                
                if (!ArenaFieldControl.ArenaMap.IsPlayerTurnNow)
                    return;
                
                if (point == ArenaFieldControl.Player.Position)
                {
                    if (ArenaFieldControl.Player.IsSelected)
                    {
                        UnselectPlayer();
                    }
                    else
                    {
                        SelectPlayer();
                    }
                    return;
                }
                
                if (ArenaFieldControl.Player.IsSelected)
                {
                    if (ArenaFieldControl.ArenaMap.Monsters.Any(m => m.Position == point && m.IsAlive) &&
                        ArenaFieldControl.ArenaMap.PlayerAttackPoint.Any(p => p.Value == point))
                    {
                        AttackMonster(point);
                        return;
                    }

                    if (ArenaFieldControl.ArenaMap.PlayerPaths.Any(p => p.Contains(point)))
                    {
                        MovePlayer(point);
                        return;
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
                _attackMonsterButton.Enabled = true; //можно убрать?
            }

            var movePaths = BFS.FindPaths(
                ArenaFieldControl.ArenaMap,
                ArenaFieldControl.Player.Position,
                ArenaFieldControl.Player.AP).ToArray();
            var attackPaths = BFS.FindPaths(ArenaFieldControl.ArenaMap,
                        ArenaFieldControl.Player.Position,
                        ArenaFieldControl.Player.Weapon.WeaponRadius, false).ToArray();
            ArenaFieldControl.ArenaMap.SetPlayerPaths(movePaths, attackPaths);

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
        
        #region Настройка Панелей

        private void ConfigureTables(TableLayoutPanel table)
        {
            _levelMenu = new ComboBox()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackColor = Color.Red,
                Padding = new Padding(25, 10, 0, 0),
                Font = new Font(SystemFonts.DialogFont.FontFamily, 12),
                Enabled = false,
                Visible = false
            };
            SetUpComboBox(_levelMenu);
            
            _arenaInfoPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackColor = Color.White,
                Padding = new Padding(15, 10, 0, 0),
                Font = new Font(SystemFonts.DialogFont.FontFamily, 12)
            };
            SetUpArenaInfoPanel(_arenaInfoPanel);

            _nextTurnButton = new Button()
            {
                Text = $"Следующий ход",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Size = new Size(350, 50),
                AutoSize = true
            };
            _nextTurnButton.Click += ClickOnNextTurnButton;

            _backToMenuButton = new Button()
            {
                Text = $"Назад в меню",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Size = new Size(350, 50),
                AutoSize = true
            };
            _backToMenuButton.Click += _game.SwitchOnMainMenu;

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
            
            _playerInfoPanel = new PlayerInfoPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            _playerInfoPanel.Configure(ArenaFieldControl);

            _healBarPanel = new HealBarPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            _healBarPanel.Configure(ArenaFieldControl.ArenaMap);

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
                Padding = new Padding(5, 0, 5, 0),
                BackgroundImage = Resources.dioriteBackground
            };
            var bottomTable = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            var firstColumnTable = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            var secondColumnTable = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackgroundImage = Resources.netherBackground
            };
            var thirdColumnTable = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true
            };

            #region AddingControls

            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            firstColumnTable.RowStyles.Add(new RowStyle(SizeType.Percent, 60));
            firstColumnTable.RowStyles.Add(new RowStyle(SizeType.Percent, 40));
            firstColumnTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
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
            bottomTable.Controls.Add(_backToMenuButton, 0, 2);
            bottomTable.Controls.Add(_nextLevelButton, 0, 1);
            //bottomTable.Controls.Add(_attackMonsterButton, 2, 0);
            bottomTable.Controls.Add(_inspectEntityButton, 2, 1);
            bottomTable.Controls.Add(_nextTurnButton, 2, 2);
            firstColumnTable.Controls.Add(_arenaInfoPanel, 0, 0);
            firstColumnTable.Controls.Add(_levelMenu, 0, 1);
            secondColumnTable.Controls.Add(arenaLayoutPanel, 0, 0);
            secondColumnTable.Controls.Add(bottomTable, 0, 1);
            thirdColumnTable.Controls.Add(_healBarPanel, 0, 0);
            thirdColumnTable.Controls.Add(_playerInfoPanel, 0, 1);
            thirdColumnTable.Controls.Add(_inventoryPanel, 0, 2);
            table.Controls.Add(firstColumnTable, 0, 0);
            table.Controls.Add(secondColumnTable, 1, 0);
            table.Controls.Add(thirdColumnTable, 2, 0);

            #endregion
        }

        private void SetUpComboBox(ComboBox comboBox)
        {
            comboBox.DataSource = _levels;
            comboBox.SelectionChangeCommitted += (sender, args) =>
            {
                var arenaId = comboBox.SelectedIndex;
                ArenaFieldControl.LoadLevel(_levels[arenaId]);
                CurrentArenaId = arenaId;
                _nextLevelButton.Enabled = false;
                if(_UIBlocked)
                    OnBlockUnblockUI();
            };
        }
        
        private void SetUpArenaInfoPanel(FlowLayoutPanel infoPanel)
        {
            infoPanel.Controls.Add(new Label
            {
                // Text = "Информация о текущей арене:",
                Text = "\n",
                ForeColor = Color.Black,
                AutoSize = true,
                Size = new Size(350, 30),
                Margin = new Padding(0, 20, 0, 0)
            });
            infoPanel.Controls.Add(new Label
            {
                Text = $"Текущая арена:\n  {_currentArenaId + 1} из {_levels.Length}",
                ForeColor = Color.Black,
                AutoSize = true,
                Size = new Size(350, 30),
                Margin = new Padding(0, 20, 0, 0)
            });
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
                    cheatMenu.ChangeDebug += OnChangeDebug;
                    cheatMenu.Show();
                }
                _pressedKeys.Clear();
            }
        }

        private void OnChangeDebug()
        {
            _levelMenu.Enabled = !_levelMenu.Enabled;
            _levelMenu.Visible = !_levelMenu.Visible;
            _playerInfoPanel.OnChangeDebug();
            ArenaFieldControl.ArenaPainter.OnDebugChange();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            _pressedKeys.Remove(e.KeyCode);
        }
        
        private void OnBlockUnblockUI()
        {
            _inventoryPanel.OnBlockUnblockUI();
            _nextTurnButton.Enabled = !_nextTurnButton.Enabled;
            _backToMenuButton.Enabled = !_backToMenuButton.Enabled;
            _UIBlocked = !_UIBlocked;
        }
        
        private void OnBindArenaMapEvent()
        {
            ArenaFieldControl.ArenaMap.ChangeStateOfUI += OnBlockUnblockUI;
            ArenaFieldControl.ArenaMap.AllMonsterDead += OnAllMonsterDead;
            ArenaFieldControl.ArenaMap.PlayerDead += OnPlayerDead;
            _healBarPanel.Configure(ArenaFieldControl.ArenaMap);
            _winFormIsDisplayed = false;
        }

        public void OnSetCurrentArenaId(int arenaId)
        {
            CurrentArenaId = arenaId;
        }
        
        private void ClickOnNextTurnButton(object sender, EventArgs e)
        {
            ArenaFieldControl.ArenaMap.CheckOnWinning();
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
            if(_winFormIsDisplayed)
                return;
            
            _nextLevelButton.Enabled = true;
            _winFormIsDisplayed = true;
            MessageBox.Show(
                "Ты выиграл!\nСледуй дальше",
                "Победа!",
                MessageBoxButtons.OK,
                MessageBoxIcon.None,
                MessageBoxDefaultButton.Button1);
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
                    MessageBoxDefaultButton.Button1);
                ArenaFieldControl.LoadLevel(_levels[CurrentArenaId]);
                timer.Close();
            };
            timer.Start();
        }
        
        #endregion

        private (double Width, double Height) GetZoomForController()
        {
            var width = ArenaFieldControl.Width != 0 && ClientSize.Width - 25 > 0
                ? (double) (ClientSize.Width - 25) / ArenaFieldControl.Width
                : 1;
            var height = ArenaFieldControl.Height != 0 && ClientSize.Height - 7 > 0
                ? (double) (ClientSize.Height - 7) / ArenaFieldControl.Height
                : 1;
            return (width, height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(Color.White);
        }
    }
}