using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Cave_Adventure.Interfaces;

namespace Cave_Adventure.Views
{
    public class LevelSelectionMenuPanel: Panel, IPanel
    {
        private readonly string[] _levels;
        private readonly Game _game;
        private bool _configured = false;
        public event Action<string> LoadLevel;
        public event Action<int> SetLevelId;

        public LevelSelectionMenuPanel(Game game)
        {
            _game = game;
            _levels = GlobalConst.LoadLevels().ToArray();
            
            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            ConfigureTable(table);
            
            Controls.Add(table);
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
            
            Invalidate();
            _configured = true;
        }

        public void Drop()
        {
            _configured = false;
        }

        private void ConfigureTable(TableLayoutPanel table)
        {
            var buttonMenu = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackColor = Color.Chartreuse,
                Padding = new Padding(25, 10, 0, 0),
                Font = new Font(SystemFonts.DialogFont.FontFamily, 12)
            };
            SetUpLevelSwitch(buttonMenu);
            
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
            table.Controls.Add(buttonMenu, 1, 0);
        }
        
        private void SetUpLevelSwitch(FlowLayoutPanel menuPanel)
        {
            menuPanel.Controls.Add(new Label
            {
                Text = "Little TB Game",
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.Black,
                Size = new Size(350, 50),
                AutoSize = true,
                Margin = new Padding(30, 25, 0, 0),
                Font = new Font(SystemFonts.DialogFont.FontFamily, 15)
            });
            
            for (var i = 0; i < _levels.Length; i++)
            {
                var arenaId = i;
                var link = new LinkLabel
                {
                    Text = $"Арена {i + 1}",
                    ActiveLinkColor = Color.Red,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(100, 35),
                    AutoSize = true,
                    Margin = new Padding(0, 20, 0, 5),
                    Tag = _levels[arenaId]
                };
                link.LinkClicked += (sender, args) =>
                {
                    _game.SwitchOnArenas(sender, args);
                    LoadLevel?.Invoke(_levels[arenaId]);
                    SetLevelId?.Invoke(arenaId);
                };
                
                menuPanel.Controls.Add(link);
            }
        }
    }
}