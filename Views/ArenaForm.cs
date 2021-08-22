﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Cave_Adventure.Interfaces;
using Cave_Adventure.Properties;
using Cave_Adventure.Views;
using Cave_Adventure.Views.Screens;

namespace Cave_Adventure
{
    public partial class ArenaForm : Form
    {
        private readonly Timer _timer;
        private readonly ArenaPanel _arenaPanel;
        private readonly MainMenuPanel _mainMenuPanel;
        private readonly LevelSelectionMenuPanel _levelSelectionMenuPanel;
        private readonly TextShowPanel _storyIntroPanel;
        private readonly TextShowPanel _tutorial1Panel;
        private readonly TextShowPanel _tutorial2Panel;
        private readonly TextShowPanel _endGamePanel;
        //private readonly Game _game;

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
            Game.Instance.ScreenChanged += OnScreenChange;

            //SuspendLayout();

            _mainMenuPanel = new MainMenuPanel()
            {
                Dock = DockStyle.Fill,
                Name = "mainMenuPanel"
            };
            _levelSelectionMenuPanel = new LevelSelectionMenuPanel()
            {
                Dock = DockStyle.Fill,
                Name = "levelSelectionMenuPanel"
            };
            _arenaPanel = new ArenaPanel()
            {
                Dock = DockStyle.Fill,
                Name = "arenaPanel"
            };

            _tutorial1Panel = new TextShowPanel()
            {
                Title = {Text = "Обучение"},
                InnerTextLabel = {Text = Resources.Tutorial1},
                FirstButton = { Text = "Погодите, хочу назад", Visible = true},
                SecondButton = {Text = "Хочу уже играть", Visible = true},
                ThirdButton = { Text = "Хмм, понял, давай дальше", Visible = true}
            };
            _tutorial1Panel.FirstButton.Click += Game.Instance.SwitchOnStoryIntroPanel;
            _tutorial1Panel.SecondButton.Click += Game.Instance.SwitchOnArenas;
            _tutorial1Panel.ThirdButton.Click += Game.Instance.SwitchOnTutorial2;
            
            _tutorial2Panel = new TextShowPanel()
            {
                Title = {Text = "Обучение"},
                InnerTextLabel = {Text = Resources.Tutorial2},
                FirstButton = { Text = "Погодите, хочу назад", Visible = true},
                ThirdButton = { Text = "И зачем я на это согласился...", Visible = true}
            };
            _tutorial2Panel.FirstButton.Click += Game.Instance.SwitchOnTutorial1;
            _tutorial2Panel.ThirdButton.Click += Game.Instance.SwitchOnArenas;

            _storyIntroPanel = new TextShowPanel()
            {
                Title = {Visible = false},
                InnerTextLabel = {Text = Resources.BeginningOfStory},
                FirstButton = { Text = "Погодите, хочу назад", Visible = true},
                ThirdButton = { Text = "И глубже в тьму", Visible = true}
            };
            _storyIntroPanel.FirstButton.Click += Game.Instance.SwitchOnMainMenu;
            _storyIntroPanel.ThirdButton.Click += Game.Instance.SwitchOnTutorial1;
            
            _endGamePanel = new TextShowPanel()
            {
                Title = {Visible = false},
                InnerTextLabel = {Text = Resources.EndOfStory},
                ThirdButton = { Text = "Удачи, путник!", Visible = true}
            };
            _endGamePanel.ThirdButton.Click += Game.Instance.SwitchOnMainMenu;

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

        #region Неиспользуемое
        // _tutorial1Panel = new Tutorial1Panel(_game)
        // {
        //     Dock = DockStyle.Fill,
        //     Location = new Point(0, 0),
        //     Name = "tutorial1Panel"
        // };
        // _tutorial2Panel = new Tutorial2Panel(_game)
        // {
        //     Dock = DockStyle.Fill,
        //     Location = new Point(0, 0),
        //     Name = "tutorial2Panel"
        // };
        // _storyIntroPanel = new StoryIntroPanel(_game)
        // {
        //     Dock = DockStyle.Fill,
        //     Location = new Point(0, 0),
        //     Name = "storyIntroPanel"
        // };
        // _endGamePanel = new EndGamePanel(_game)
        // {
        //     Dock = DockStyle.Fill,
        //     Location = new Point(0, 0),
        //     Name = "endGamePanel"
        // };
        #endregion

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
            foreach (Control control in Controls)
            {
                control.Hide();
            }
        }

        private void DropScreens()
        {
            foreach (IPanel control in Controls)
            {
                control.Drop();
            }
        }
        
        private void TimerTick(object sender, EventArgs e)
        {
            switch (Game.Instance.CurrentScreen)
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