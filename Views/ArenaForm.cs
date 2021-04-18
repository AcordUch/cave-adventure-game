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
        private readonly ArenaPainter _arenaPainter;
        private readonly PlayerPainter _playerPainter;
        private readonly ArenaPanel _arenaPanel;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
            WindowState = FormWindowState.Maximized;
            Text = "Здесь должны быть бои!";
            KeyPreview = true;
        }
        
        public ArenaForm()
        {
            //InitializeComponent();
            var levels = LoadLevels().ToArray();
            
            _arenaPainter = new ArenaPainter(levels[0]);
            _playerPainter = new PlayerPainter();
            _arenaPanel = new ArenaPanel(_arenaPainter, _playerPainter) {Dock = DockStyle.Fill};
            
            var levelMenu = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                Dock = DockStyle.Left,
                Width = 150,
                BackColor = Color.Empty,
                Padding = new Padding(25, 10, 0, 0),
                Font = new Font(SystemFonts.DialogFont.FontFamily, 12)
            };
            SetUpLevelSwitch(levels, levelMenu);

            Controls.Add(_arenaPanel);
            Controls.Add(levelMenu);
            
            KeyDown += _arenaPanel.OnKeyDown;
            KeyUp += _arenaPanel.OnKeyUp;
            
            _timer = new Timer { Interval = 60 };
            _timer.Tick += TimerTick;
            _timer.Start();
        }

        private void SetUpLevelSwitch(ArenaMap[] levels, Control menuPanel)
        {
            menuPanel.Controls.Add(new Label
            {
                Text = "Choose arena:",
                ForeColor = Color.Black,
                Size = new Size(100, 75),
                Margin = new Padding(0, 30, 0, 0)
            });
            
            var linkLabels = new List<LinkLabel>();
            for (var i = 0; i < levels.Length; i++)
            {
                var level = levels[i];
                var link = new LinkLabel
                {
                    Text = $"Arena {i + 1}",
                    ActiveLinkColor = Color.LimeGreen,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(100, 35),
                    Margin = new Padding(0, 20, 0, 5),
                    Tag = level
                };
                link.LinkClicked += (sender, args) =>
                {
                    ChangeLevel(level);
                    UpdateLinksColors(level, linkLabels);
                };
                menuPanel.Controls.Add(link);
                linkLabels.Add(link);
            }
            UpdateLinksColors(levels[0], linkLabels);
        }
        
        private static IEnumerable<ArenaMap> LoadLevels()
        {
            yield return ArenaMap.CreatNewArenaMap(Properties.Resources.Arena1);
            yield return ArenaMap.CreatNewArenaMap(Properties.Resources.Arena2);
            yield return ArenaMap.CreatNewArenaMap(Properties.Resources.Arena3);
            yield return ArenaMap.CreatNewArenaMap(Properties.Resources.Arena4);
            yield return ArenaMap.CreatNewArenaMap(Properties.Resources.Arena5);
        }
        
        private void ChangeLevel(ArenaMap newMap)
        {
            _arenaPainter.ChangeLevel(newMap);
            //_timer.Start();
            _arenaPanel.Invalidate();
        }
        
        private static void UpdateLinksColors(ArenaMap level, List<LinkLabel> linkLabels)
        {
            foreach (var linkLabel in linkLabels)
            {
                linkLabel.LinkColor = linkLabel.Tag == level ? Color.LimeGreen : Color.Black;
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            _arenaPanel.Update();
        }
    }
}