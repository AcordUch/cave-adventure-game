using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Cave_Adventure.Properties;
using Cave_Adventure.Views;

namespace Cave_Adventure
{
    public partial class ArenaForm : Form
    {
        private readonly Timer _timer;
        private readonly ArenaPanel _arenaPanel;
        private readonly MainMenuPanel _mainMenuPanel;
        private readonly LevelSelectionMenuPanel _levelSelectionMenuPanel;
        private readonly Game _game;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
            Size = new Size(800, 600);
            WindowState = FormWindowState.Maximized;
            Text = "Заходит в бар улитка, говорит...";
            KeyPreview = true;
        }
        
        public ArenaForm()
        {
            _game = new Game();
            _game.ScreenChanged += OnScreenChange;
            
            SuspendLayout();
            
            _mainMenuPanel = new MainMenuPanel(_game)
            {
                Dock = DockStyle.Fill,
                Size = new Size(800, 600),
                Location = new Point(0, 0),
                Name = "mainMenuPanel"
            };
            _levelSelectionMenuPanel = new LevelSelectionMenuPanel(_game)
            {
                Dock = DockStyle.Fill,
                Size = new Size(800, 600),
                Location = new Point(0, 0),
                Name = "levelSelectionMenuPanel"
            };
            _arenaPanel = new ArenaPanel(_game)
            {
                Dock = DockStyle.Fill,
                Size = new Size(800, 600),
                Location = new Point(0, 0),
                Name = "arenaPanel"
            };

            _levelSelectionMenuPanel.LoadLevel += _arenaPanel.ArenaFieldControl.LoadLevel;
            _levelSelectionMenuPanel.SetLevelId += _arenaPanel.OnSetCurrentArenaId;

            Controls.Add(_arenaPanel);
            Controls.Add(_mainMenuPanel);
            Controls.Add(_levelSelectionMenuPanel);
            
            ResumeLayout();

            // KeyDown += _arenaPanel.ArenaFieldControl.OnKeyDown;
            // KeyUp += _arenaPanel.ArenaFieldControl.OnKeyUp;
            
            ShowMainMenu();
            
            _timer = new Timer { Interval = 60 };
            _timer.Tick += TimerTick;
            _timer.Start();
        }
        
        private void OnScreenChange(GameScreen screen)
        {
            switch (screen)
            {
                case GameScreen.Arenas:
                    ShowArenas();
                    break;
                case GameScreen.MainMenu:
                    ShowMainMenu();
                    break;
                case GameScreen.LevelSelectionMenu:
                    ShowLevelSelectionMenu();
                    break;
            }
        }

        private void ShowArenas()
        {
            HideScreens();
            _arenaPanel.Configure();
            _arenaPanel.Show();
        }

        private void ShowMainMenu()
        {
            HideScreens();
            _mainMenuPanel.Configure();
            _mainMenuPanel.Show();
        }

        private void ShowLevelSelectionMenu()
        {
            HideScreens();
            _levelSelectionMenuPanel.Configure();
            _levelSelectionMenuPanel.Show();
        }

        private void HideScreens()
        {
            DropScreens();
            _arenaPanel.Hide();
            _mainMenuPanel.Hide();
            _levelSelectionMenuPanel.Hide();
        }

        private void DropScreens()
        {
            _arenaPanel.Drop();
            _mainMenuPanel.Drop();
            _levelSelectionMenuPanel.Drop();
        }
        
        private void TimerTick(object sender, EventArgs e)
        {
            _arenaPanel.Update();
        }
    }
}