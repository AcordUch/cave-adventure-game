using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Cave_Adventure.Interfaces;

namespace Cave_Adventure
{
    public class ArenaPanel : Panel, IPanel
    {
        private readonly ArenaFieldControl ArenaFieldControl;
        private Label _infoLabel;
        private readonly string[] _levels;
        private bool _configured = false;
        private readonly Game _game;

        public ArenaPanel(Game game)
        {
            _game = game;
            _levels = LoadLevels().ToArray();
            
            ArenaFieldControl = new ArenaFieldControl();

            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            ConfigureTables(table);
            
            Controls.Add(table);
            ArenaFieldControl.ClickOnPoint += ArenaFieldControl_ClickOnPoint;
        }
        
        protected override void InitLayout()
        {
            base.InitLayout();
            ResizeRedraw = true;
            DoubleBuffered = true;
        }

        public void Configure()
        {
            if (_configured)
                throw new InvalidOperationException();
            
            ArenaFieldControl.Configure(_levels[0]);
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
            
            //Вынести в метод OnSizeChange
            var zoom = GetZoomForController();
            ArenaFieldControl.Size =
                new Size((int)(ArenaFieldControl.Width * zoom), (int)(ArenaFieldControl.Height * zoom));

            _infoLabel.Size = new Size((int)(Width * 0.25), (int) (Height * 0.4));
            _infoLabel.Text = ArenaFieldControl.PlayerInfoToString();
        }

        private void ArenaFieldControl_ClickOnPoint(Point point, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                var actionCompleted = false;
                if (point == ArenaFieldControl.Player.Position && !actionCompleted)
                {
                    if (ArenaFieldControl.Player.IsSelected)
                    {
                        ArenaFieldControl.ArenaMap.PlayerSelected = false;
                        ArenaFieldControl.Player.IsSelected = false;
                        ArenaFieldControl.ArenaPainter.Update();
                    }
                    else
                    {
                        var path = BFS.FindPaths(
                            ArenaFieldControl.ArenaMap,
                            ArenaFieldControl.Player.Position,
                            ArenaFieldControl.Player.AP).ToArray();
                        ArenaFieldControl.ArenaMap.SetPlayerPaths(path);

                        ArenaFieldControl.Player.IsSelected = true;
                        ArenaFieldControl.ArenaPainter.Update();
                    }
                    actionCompleted = true;
                }
                
                if (ArenaFieldControl.Player.IsSelected && !actionCompleted)
                {
                    if (ArenaFieldControl.ArenaMap.PlayerPaths.Any(p => p.Contains(point)))
                    {
                        ArenaFieldControl.ArenaMap.MoveAlongThePath(point);
                        ArenaFieldControl.ArenaMap.PlayerSelected = false;
                    }
                    actionCompleted = true;
                }
            }
        }

        private void ClickOnNextTurnButton(object sender, EventArgs e)
        {
            ArenaFieldControl.ArenaMap.NextTurn();
        }
        
        // private static IEnumerable<ArenaMap> LoadLevels()
        // {
        //     yield return ArenaMap.CreateNewArenaMap(Properties.Resources.Arena1);
        //     yield return ArenaMap.CreateNewArenaMap(Properties.Resources.Arena2);
        //     yield return ArenaMap.CreateNewArenaMap(Properties.Resources.Arena3);
        //     yield return ArenaMap.CreateNewArenaMap(Properties.Resources.Arena4);
        //     yield return ArenaMap.CreateNewArenaMap(Properties.Resources.Arena5);
        // }

        private static IEnumerable<String> LoadLevels()
        {
            yield return Properties.Resources.Arena1;
            yield return Properties.Resources.Arena2;
            yield return Properties.Resources.Arena3;
            yield return Properties.Resources.Arena4;
            yield return Properties.Resources.Arena5;
            yield return Properties.Resources.Arena6;
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

            var nextTurnButton = new Button()
            {
                Text = $"Следующий ход",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Size = new Size(350, 50),
                AutoSize = true
            };
            nextTurnButton.Click += ClickOnNextTurnButton;

            var backToMenuButton = new Button()
            {
                Text = $"Назад в меню",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Size = new Size(350, 50),
                AutoSize = true
            };
            backToMenuButton.Click += _game.SwitchOnMainMenu;

            var infoPanel = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.LeftToRight,
                Dock = DockStyle.Fill,
                AutoSize = true,
                Padding = new Padding(20, 0, 0, 0),
                Font = new Font(SystemFonts.DialogFont.FontFamily, 10)
            };
            SetUpInfoPanel(infoPanel);

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
            
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            secondColumnTable.RowStyles.Add(new RowStyle(SizeType.Percent, 93));
            secondColumnTable.RowStyles.Add(new RowStyle(SizeType.Percent, 7));
            secondColumnTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            thirdColumnTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            thirdColumnTable.RowStyles.Add(new RowStyle(SizeType.Percent, 40));
            thirdColumnTable.RowStyles.Add(new RowStyle(SizeType.Percent, 40));
            thirdColumnTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            bottomTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            bottomTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            bottomTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            bottomTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            
            arenaLayoutPanel.Controls.Add(ArenaFieldControl);
            bottomTable.Controls.Add(backToMenuButton,0, 0);
            bottomTable.Controls.Add(new Panel(){Dock = DockStyle.Fill,BackColor = Color.Black},1, 0);
            bottomTable.Controls.Add(nextTurnButton,2, 0);
            secondColumnTable.Controls.Add(arenaLayoutPanel, 0, 0);
            secondColumnTable.Controls.Add(bottomTable, 0, 1);
            thirdColumnTable.Controls.Add(new Panel(){Dock = DockStyle.Fill,BackColor = Color.Black},0, 0);
            thirdColumnTable.Controls.Add(infoPanel,0, 1);
            thirdColumnTable.Controls.Add(new Panel(){Dock = DockStyle.Fill,BackColor = Color.Black},0, 2);
            table.Controls.Add(levelMenu, 0, 0);
            table.Controls.Add(secondColumnTable, 1, 0);
            table.Controls.Add(thirdColumnTable, 2, 0);
        }
        
        private void SetUpLevelSwitch(Control menuPanel)
        {
            menuPanel.Controls.Add(new Label
            {
                Text = "Выберете Арену:",
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
                    ArenaFieldControl.ChangeLevel(_levels[arenaId]);
                    UpdateLinksColors(_levels[arenaId], linkLabels);
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