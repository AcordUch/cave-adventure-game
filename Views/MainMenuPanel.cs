using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Cave_Adventure.Interfaces;
using ContentAlignment = System.Drawing.ContentAlignment;

namespace Cave_Adventure
{
    public class MainMenuPanel : Panel, IPanel
    {
        private readonly Game _game;
        private bool _configured = false;
        private Panel _imagePanel;
        private PictureBox _imageBox;
        
        public event Action<string> LoadLevel;
        public event Action<int> SetLevelId;

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

        public new void Update()
        {
            _imageBox.Image = new Bitmap(Properties.Resources.mazePicMainMenu, Size);
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
            _imagePanel = new Panel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackgroundImage = new Bitmap(Properties.Resources.mazePicMainMenu, Size)
            };

            _imageBox = new PictureBox()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                Image = new Bitmap(Properties.Resources.mazePicMainMenu, Size)
            };
            
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
            table.Controls.Add(_imagePanel, 0, 0);
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
            Arenas.LinkClicked += (sender, args) =>
            {
                _game.SwitchOnArenas(sender, args);
                // LoadLevel?.Invoke(_levels[arenaId]);
                // SetLevelId?.Invoke(arenaId);
            };
            
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