using System;
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
        private readonly ArenaGeneratorPanel _arenaGeneratorPanel;
        private readonly TextShowPanel _storyIntroPanel;
        private readonly TextShowPanel _tutorial1Panel;
        private readonly TextShowPanel _tutorial2Panel;
        private readonly TextShowPanel _endGamePanel;

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
            Game.Instance.ChangedOnCertainArena += ChangedOnCertainArena;
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
            _arenaPanel = ArenaPanel.Instance;
            _arenaGeneratorPanel = new ArenaGeneratorPanel()
            {
                Dock = DockStyle.Fill,
                Name = "arenaGeneratorPanel"
            };
            
            // _levelSelectionMenuPanel.LoadLevel += _arenaPanel.ArenaFieldControl.LoadLevel;
            _levelSelectionMenuPanel.LoadLevel += _arenaPanel.ReConfigure;
            _levelSelectionMenuPanel.SetLevelId += _arenaPanel.OnSetCurrentArenaId;
            
            _tutorial1Panel = TextShowPanelHub.CreateTutorial1Panel();
            _tutorial2Panel = TextShowPanelHub.CreateTutorial2Panel();
            _storyIntroPanel = TextShowPanelHub.CreateStoryIntroPanel();
            _endGamePanel = TextShowPanelHub.CreateEndGamePanel();

            Controls.Add(_arenaPanel);
            Controls.Add(_mainMenuPanel);
            Controls.Add(_levelSelectionMenuPanel);
            Controls.Add(_arenaGeneratorPanel);
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

        private void ChangedOnCertainArena(string arena)
        {
            SuspendLayout();
            HideScreens();
            _arenaPanel.ReConfigure(arena);
            _arenaPanel.Show();
            ResumeLayout();
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
                case GameScreen.ArenaGeneratorMenu:
                    ShowArenaGeneratorMenu();
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

        private void ShowArenaGeneratorMenu()
        {
            HideScreens();
            _arenaGeneratorPanel.Configure();
            _arenaGeneratorPanel.Show();
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