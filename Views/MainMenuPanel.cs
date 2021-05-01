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
        private bool _configured = false;
        private readonly Game _game;
        
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
                FlowDirection = FlowDirection.RightToLeft,
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
                Margin = new Padding(30, 25, 0, 0)
            });
            
            var link = new LinkLabel
            {
                Text = "Arenas",
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(100, 35),
                Margin = new Padding(0, 20, 0, 5),
            };
            link.LinkClicked += _game.SwitchOnArenas;
            buttonMenu.Controls.Add(link);
        }
    }
}