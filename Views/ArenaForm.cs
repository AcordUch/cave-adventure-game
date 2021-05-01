using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Cave_Adventure.Properties;

namespace Cave_Adventure
{
    public partial class ArenaForm : Form
    {
        private readonly Timer _timer;
        private readonly ArenaPanel _arenaPanel;
        private readonly Game _game;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
            Size = new Size(800, 600);
            //WindowState = FormWindowState.Maximized;
            Text = "Здесь должны быть бои!";
            KeyPreview = true;
        }
        
        public ArenaForm()
        {
            //InitializeComponent();
            //var levels = LoadLevels().ToArray();
            
            SuspendLayout();
            
            _arenaPanel = new ArenaPanel()
            {
                Dock = DockStyle.Fill,
                Size = new Size(800, 600),
                Location = new Point(0, 0),
                Name = "arenaPanel"
            };

            Controls.Add(_arenaPanel);
            
            ResumeLayout();

            // KeyDown += _arenaPanel.ArenaFieldControl.OnKeyDown;
            // KeyUp += _arenaPanel.ArenaFieldControl.OnKeyUp;

            _game = new Game();
            _game.ScreenChanged += OnScreenChange;
            
            ShowArenas();
            
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
            //TODO
        }

        private void HideScreens()
        {
            _arenaPanel.Hide();
        }
        
        private void TimerTick(object sender, EventArgs e)
        {
            _arenaPanel.Update();
        }
    }
}