using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Cave_Adventure.Interfaces;

namespace Cave_Adventure
{
    public class MainMenuPanel : Panel, IPanel
    {
        private readonly Game _game;
        private bool _configured = false;

        public MainMenuPanel(Game game)
        {
            _game = game;
            
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
                BackColor = Color.Red,
                Padding = new Padding(25, 10, 0, 0),
                Font = new Font(SystemFonts.DialogFont.FontFamily, 12)
            };
            SetUpButtonMenu(buttonMenu);
            
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
            table.Controls.Add(buttonMenu, 1, 0);
        }

        private void SetUpButtonMenu(FlowLayoutPanel buttonMenu)
        {
            buttonMenu.Controls.Add(new Label
            {
                Text = "Little TB Game",
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.Black,
                Size = new Size(350, 50),
                AutoSize = true,
                Margin = new Padding(30, 25, 0, 0),
                Font = new Font(SystemFonts.DialogFont.FontFamily, 15)
            });
            
            var Arenas = new LinkLabel
            {
                Text = "Start Play Arena Mode",
                TextAlign = ContentAlignment.MiddleCenter,
                LinkColor = Color.Black,
                ActiveLinkColor = Color.White,
                Size = new Size(100, 35),
                AutoSize = true,
                Margin = new Padding(0, 20, 0, 5),
            };
            Arenas.LinkClicked += _game.SwitchOnArenas;
            
            var levelSelectionMenu = new LinkLabel
            {
                Text = "Levels",
                TextAlign = ContentAlignment.MiddleCenter,
                LinkColor = Color.Black,
                ActiveLinkColor = Color.White,
                Size = new Size(100, 35),
                AutoSize = true,
                Margin = new Padding(0, 20, 0, 5),
            };
            levelSelectionMenu.LinkClicked += _game.SwitchOnLevelSelectionMenu;
            
            buttonMenu.Controls.Add(Arenas);
            buttonMenu.Controls.Add(levelSelectionMenu);
        }
    }
}