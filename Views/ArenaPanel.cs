using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Cave_Adventure
{
    public class ArenaPanel : Panel
    {
        public readonly ArenaFieldControl ArenaFieldControl;

        public ArenaPanel(ArenaMap[] levels)
        {
            var levelMenu = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                Dock = DockStyle.Fill,
                // Width = 150,
                AutoSize = true,
                BackColor = Color.Red,
                Padding = new Padding(25, 10, 0, 0),
                Font = new Font(SystemFonts.DialogFont.FontFamily, 12)
            };
            SetUpLevelSwitch(levels, levelMenu);
            
            ArenaFieldControl = new ArenaFieldControl(levels);

            var table = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            var secondColumnTable = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                Padding = new Padding(100, 30, 0, 50),
            };
            var thirdColumnTable = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            ConfigureTables(table, secondColumnTable, thirdColumnTable, levelMenu);
            
            Controls.Add(table);
            ArenaFieldControl.ClickOnPoint += ArenaFieldControl_ClickOnPoint;
        }

        private void ConfigureTables(TableLayoutPanel table, FlowLayoutPanel secondColumnTable,
            TableLayoutPanel thirdColumnTable, FlowLayoutPanel levelMenu)
        {
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            thirdColumnTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            thirdColumnTable.RowStyles.Add(new RowStyle(SizeType.Percent, 40));
            thirdColumnTable.RowStyles.Add(new RowStyle(SizeType.Percent, 40));
            thirdColumnTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            
            secondColumnTable.Controls.Add(ArenaFieldControl);
            thirdColumnTable.Controls.Add(new Panel(){Dock = DockStyle.Fill,BackColor = Color.Black},0, 0);
            thirdColumnTable.Controls.Add(new Panel(){Dock = DockStyle.Fill,BackColor = Color.Blue},0, 1);
            thirdColumnTable.Controls.Add(new Panel(){Dock = DockStyle.Fill,BackColor = Color.Black},0, 2);
            table.Controls.Add(levelMenu, 0, 0);
            table.Controls.Add(secondColumnTable, 1, 0);
            table.Controls.Add(thirdColumnTable, 2, 0);
        }
        
        protected override void InitLayout()
        {
            base.InitLayout();
            ResizeRedraw = true;
            DoubleBuffered = true;
        }

        public void Configure(ArenaMap arenaMap)
        {
            ArenaFieldControl.Configure(arenaMap);
        }
        
        public new void Update()
        {
            ArenaFieldControl.Update();
            
            var zoom = GetZoomForController();
            ArenaFieldControl.Size =
                new Size((int)(ArenaFieldControl.Width * zoom), (int)(ArenaFieldControl.Height * zoom));
            //Invalidate();
        }

        private void ArenaFieldControl_ClickOnPoint(Point point, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                var actionCompleted = false;
                if (point == ArenaFieldControl.Player.Position && !actionCompleted)
                {
                    var path = BFS.FindPaths(
                        ArenaFieldControl.ArenaMap,
                        ArenaFieldControl.Player.Position,
                        ArenaFieldControl.Player.AP).ToArray();
                    ArenaFieldControl.ArenaMap.SetPlayerPaths(path);
                    
                    ArenaFieldControl.Player.IsSelected = !ArenaFieldControl.Player.IsSelected;
                    actionCompleted = true;
                    ArenaFieldControl.ArenaPainter.Update();
                }

                if (ArenaFieldControl.Player.IsSelected && !actionCompleted)
                {
                    // if(ArenaFieldControl.ArenaMap.Arena[point.X, point.Y] == CellType.Floor
                    //     && ArenaFieldControl.Monsters.All(p => p.Position != point))
                    // {
                    //     ArenaFieldControl.Player.SetTargetPoint(point);
                    //     ArenaFieldControl.Player.IsSelected = false;
                    //     ArenaFieldControl.Update();
                    //     ArenaFieldControl.ArenaPainter.Update();
                    // }
                    if (ArenaFieldControl.ArenaMap.PlayerPaths.Any(p => p.Contains(point)))
                    {
                        //ArenaFieldControl.Player.SetTargetPoint(point);
                        ArenaFieldControl.ArenaMap.TempNameMovePlayerTipa(point);
                        ArenaFieldControl.ArenaMap.PlayerSelected = false;
                        ArenaFieldControl.Update();
                        //ArenaFieldControl.Player.IsSelected = false;
                        ArenaFieldControl.ArenaMap.PlayerSelected = false;
                        //ArenaFieldControl.ArenaPainter.Update();
                    }
                    actionCompleted = true;
                }
            }
        }

        private void SetUpLevelSwitch(ArenaMap[] levels, Control menuPanel)
        {
            menuPanel.Controls.Add(new Label
            {
                Text = "Choose arena:",
                ForeColor = Color.Black,
                Size = new Size(100, 75),
                Margin = new Padding(0, 30, 0, 0)
            });
            
            var linkLabels = new List<LinkLabel>();
            for (var i = 0; i < levels.Length; i++)
            {
                var level = levels[i];
                var link = new LinkLabel
                {
                    Text = $"Arena {i + 1}",
                    ActiveLinkColor = Color.LimeGreen,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(100, 35),
                    Margin = new Padding(0, 20, 0, 5),
                    Tag = level
                };
                link.LinkClicked += (sender, args) =>
                {
                    ArenaFieldControl.ChangeLevel(level);
                    UpdateLinksColors(level, linkLabels);
                };
                menuPanel.Controls.Add(link);
                linkLabels.Add(link);
            }
            UpdateLinksColors(levels[0], linkLabels);
        }
        
        private static void UpdateLinksColors(ArenaMap level, List<LinkLabel> linkLabels)
        {
            foreach (var linkLabel in linkLabels)
            {
                linkLabel.LinkColor = linkLabel.Tag == level ? Color.LimeGreen : Color.Black;
            }
        }
        
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