using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Cave_Adventure.Interfaces;
using Cave_Adventure.Properties;

namespace Cave_Adventure.Views
{
    public class LevelSelectionMenuPanel: Panel, IPanel
    {
        private readonly string[] _levels;
        private PictureBox _imageBox;
        private bool _configured = false;
        public event Action<string> LoadLevel;
        public event Action<int> SetLevelId;

        public LevelSelectionMenuPanel()
        {
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
        
        public new void Update()
        {
            try
            {
                _imageBox.Image = new Bitmap(Properties.Resources.mazePicBackground2, Size);
            }
            catch
            {
                //ignore
            }
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
                Font = new Font(SystemFonts.DialogFont.FontFamily, 12),
                BackgroundImage = Properties.Resources.grass1,
            };
            SetUpLevelSwitch(buttonMenu);
            
            var backToMainMenuButton = new Button
            {
                Text = $"Назад в меню",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Size = new Size(350, 50),
                AutoSize = true,
            };
            backToMainMenuButton.Click += Game.Instance.SwitchOnMainMenu;

            _imageBox = new PictureBox()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                Image = new Bitmap(Properties.Resources.mazePicBackground2, Size)
            };

            var secondColumn = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackgroundImage = Properties.Resources.grass1,
            };
            
            secondColumn.RowStyles.Add(new RowStyle(SizeType.Percent, 95));
            secondColumn.RowStyles.Add(new RowStyle(SizeType.Percent, 5));
            secondColumn.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
            secondColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 15));
            secondColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            secondColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 15));
            
            secondColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 0, 0);
            secondColumn.Controls.Add(buttonMenu, 1, 0);
            secondColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 2, 0);
            secondColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 0, 1);
            secondColumn.Controls.Add(backToMainMenuButton, 1, 1);
            secondColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 2, 1);
            secondColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 0, 2);
            secondColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 1, 2);
            secondColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 2, 2);
            
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 82));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            
            table.Controls.Add(_imageBox, 0, 0);
            table.Controls.Add(secondColumn, 1, 0);
        }
        
        private void SetUpLevelSwitch(FlowLayoutPanel menuPanel)
        {
            menuPanel.Controls.Add(new Label
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
            
            for (var i = 0; i < _levels.Length; i++)
            {
                var arenaId = i;
                var link = new LinkLabel
                {
                    Text = $"Арена {i + 1}",
                    
                    LinkColor = Color.White,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackgroundImage = Properties.Resources.grass1,
                    Size = new Size(100, 35),
                    AutoSize = true,
                    Margin = new Padding(0, 20, 0, 5),
                    Tag = _levels[arenaId]
                };
                link.LinkClicked += (sender, args) =>
                {
                    Game.Instance.SwitchOnArenas(sender, args);
                    LoadLevel?.Invoke(_levels[arenaId]);
                    SetLevelId?.Invoke(arenaId);
                };
                menuPanel.Controls.Add(link);
            }
        }
    }
}