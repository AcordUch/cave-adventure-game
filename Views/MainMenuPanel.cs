using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Cave_Adventure.Interfaces;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using ContentAlignment = System.Drawing.ContentAlignment;

namespace Cave_Adventure
{
    public class MainMenuPanel : Panel, IPanel
    {
        private readonly Game _game;
        private bool _configured = false;
        private Panel _imagePanel;
        // private PictureBox _imageBox;

        public event Action<string> LoadLevel;
        public event Action<int> SetLevelId;

        public MainMenuPanel(Game game)
        {
            _game = game;

            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
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
            // try
            // {
            //     _imageBox.Image = new Bitmap(Properties.Resources.mazePicMainMenu, Size);
            // }
            // catch
            // {
            //     //ignore
            // }
        }

        private void ConfigureTable(TableLayoutPanel table)
        {
            var buttonMenu = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Fill,
                AutoSize = true,
                // BackColor = Color.Red,
                Padding = new Padding(25, 10, 0, 0),
                Font = new Font(SystemFonts.DialogFont.FontFamily, 12),
                BackgroundImage = Properties.Resources.grass1
            };
            SetUpButtonMenu(buttonMenu);
            _imagePanel = new Panel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackgroundImage = new Bitmap(Properties.Resources.mazePicMainMenu, Size)
            };

            // _imageBox = new PictureBox()
            // {
            //     Dock = DockStyle.Fill,
            //     AutoSize = true,
            //     Image = new Bitmap(Properties.Resources.mazePicMainMenu, Size)
            // };
            
            var secondColumn = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackgroundImage = Properties.Resources.grass1,
            };
            
            secondColumn.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            secondColumn.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
            secondColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 15));
            secondColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            secondColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 15));
            
            secondColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Properties.Resources.obsidianBackground }, 0, 0);
            secondColumn.Controls.Add(buttonMenu, 1, 0);
            secondColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Properties.Resources.obsidianBackground }, 2, 0);
            secondColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Properties.Resources.obsidianBackground }, 0, 1);
            secondColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Properties.Resources.obsidianBackground }, 1, 1);
            secondColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Properties.Resources.obsidianBackground }, 2, 1);
            
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 82));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18));
            table.Controls.Add(_imagePanel, 0, 0);
            table.Controls.Add(secondColumn, 1, 0);
        }

        private void SetUpButtonMenu(FlowLayoutPanel buttonMenu)
        {
            buttonMenu.Controls.Add(new Label
            {
                Text = "Little TB Game",
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.White,
                Size = new Size(350, 50),
                AutoSize = true,
                Margin = new Padding(30, 25, 0, 0),
                Font = new Font(SystemFonts.DialogFont.FontFamily, 15),
                BackgroundImage = Properties.Resources.grass1,
            });
            
            var Arenas = new LinkLabel
            {
                Text = "Начать приключение",
                TextAlign = ContentAlignment.MiddleCenter,
                LinkColor = Color.White,
                ActiveLinkColor = Color.White,
                Size = new Size(100, 35),
                AutoSize = true,
                Margin = new Padding(0, 20, 0, 5),
                BackgroundImage = Properties.Resources.grass1
            };
            Arenas.LinkClicked += _game.SwitchOnStoryIntroPanel;
            
            var levelSelectionMenu = new LinkLabel
            {
                Text = "Выбор уровней",
                TextAlign = ContentAlignment.MiddleCenter,
                LinkColor = Color.White,
                ActiveLinkColor = Color.White,
                Size = new Size(100, 35),
                AutoSize = true,
                Margin = new Padding(0, 20, 0, 5),
                BackgroundImage = Properties.Resources.grass1
            };
            levelSelectionMenu.LinkClicked += _game.SwitchOnLevelSelectionMenu;

            var tutorialMenu = new LinkLabel
            {
                Text = "Tutorial",
                TextAlign = ContentAlignment.MiddleCenter,
                LinkColor = Color.White,
                ActiveLinkColor = Color.White,
                Size = new Size(100, 35),
                AutoSize = true,
                Margin = new Padding(0, 20, 0, 5),
                BackgroundImage = Properties.Resources.grass1
            };
            tutorialMenu.LinkClicked += _game.SwitchOnTutorialMenu;

            buttonMenu.Controls.Add(Arenas);
            buttonMenu.Controls.Add(levelSelectionMenu);
            buttonMenu.Controls.Add(tutorialMenu);
        }
    }
}