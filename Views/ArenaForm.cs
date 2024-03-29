﻿using System;
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
        private readonly StoryIntroPanel _storyIntroPanel;
        private readonly Tutorial1Panel _tutorial1Panel;
        private readonly Tutorial2Panel _tutorial2Panel;
        private readonly EndGamePanel _endGamePanel;
        private readonly Game _game;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
            Size = new Size(1500, 900);
            MinimumSize = new Size(1500, 900);
            MaximumSize = new Size(1920, 1080);
            WindowState = FormWindowState.Maximized;
            Text = GlobalConst.GetSplash();
            KeyPreview = true;
        }

        public ArenaForm()
        {
            _game = new Game();
            _game.ScreenChanged += OnScreenChange;

            //SuspendLayout();

            _mainMenuPanel = new MainMenuPanel(_game)
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                Name = "mainMenuPanel"
            };
            _levelSelectionMenuPanel = new LevelSelectionMenuPanel(_game)
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                Name = "levelSelectionMenuPanel"
            };
            _arenaPanel = new ArenaPanel(_game)
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                Name = "arenaPanel"
            };
            _tutorial1Panel = new Tutorial1Panel(_game)
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                Name = "tutorial1Panel"
            };
            _tutorial2Panel = new Tutorial2Panel(_game)
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                Name = "tutorial2Panel"
            };
            _storyIntroPanel = new StoryIntroPanel(_game)
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                Name = "storyIntroPanel"
            };
            _endGamePanel = new EndGamePanel(_game)
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                Name = "endGamePanel"
            };
            
            _levelSelectionMenuPanel.LoadLevel += _arenaPanel.ArenaFieldControl.LoadLevel;
            _levelSelectionMenuPanel.SetLevelId += _arenaPanel.OnSetCurrentArenaId;

            Controls.Add(_arenaPanel);
            Controls.Add(_mainMenuPanel);
            Controls.Add(_levelSelectionMenuPanel);
            Controls.Add(_storyIntroPanel);
            Controls.Add(_tutorial1Panel);
            Controls.Add(_tutorial2Panel);
            Controls.Add(_endGamePanel);
            
            ShowMainMenu();
            
            _timer = new Timer { Interval = GlobalConst.MainTimerInterval };
            _timer.Tick += TimerTick;
            _timer.Start();
        }

        private void OnScreenChange(GameScreen screen)
        {
            SuspendLayout();
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
                case GameScreen.StoryIntro:
                    ShowStoryIntro();
                    break;
                case GameScreen.TutorialMenu1:
                    ShowTutorial1();
                    break;
                case GameScreen.TutorialMenu2:
                    ShowTutorial2();
                    break;
                case GameScreen.EndGame:
                    ShowEndGame();
                    break;
            }
            ResumeLayout();
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

        private void ShowStoryIntro()
        {
            HideScreens();
            _storyIntroPanel.Configure();
            _storyIntroPanel.Show();
        }

        private void ShowTutorial1()
        {
            HideScreens();
            _tutorial1Panel.Configure();
            _tutorial1Panel.Show();
        }
        
        private void ShowTutorial2()
        {
            HideScreens();
            _tutorial2Panel.Configure();
            _tutorial2Panel.Show();
        }
        
        private void ShowEndGame()
        {
            HideScreens();
            _endGamePanel.Configure();
            _endGamePanel.Show();
        }

        private void HideScreens()
        {
            DropScreens();
            _arenaPanel.Hide();
            _mainMenuPanel.Hide();
            _levelSelectionMenuPanel.Hide();
            _storyIntroPanel.Hide();
            _tutorial1Panel.Hide();
            _tutorial2Panel.Hide();
            _endGamePanel.Hide();
        }

        private void DropScreens()
        {
            _arenaPanel.Drop();
            _mainMenuPanel.Drop();
            _levelSelectionMenuPanel.Drop();
            _storyIntroPanel.Drop();
            _tutorial1Panel.Drop();
            _tutorial2Panel.Drop();
            _endGamePanel.Drop();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            switch (_game.Screen)
            {
                case GameScreen.Arenas:
                    _arenaPanel.Update();
                    break;
                case GameScreen.LevelSelectionMenu:
                    _levelSelectionMenuPanel.Update();
                    break;
            }
        }
    }
}