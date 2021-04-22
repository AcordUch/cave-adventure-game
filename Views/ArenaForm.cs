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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
            Size = new Size(750, 450);
            //WindowState = FormWindowState.Maximized;
            Text = "Здесь должны быть бои!";
            KeyPreview = true;
        }
        
        public ArenaForm()
        {
            //InitializeComponent();
            var levels = LoadLevels().ToArray();
            
            _arenaPanel = new ArenaPanel(levels) {Dock = DockStyle.Fill};
            _arenaPanel.Configure(levels[0]);
            
            Controls.Add(_arenaPanel);

            KeyDown += _arenaPanel.ArenaFieldControl.OnKeyDown;
            KeyUp += _arenaPanel.ArenaFieldControl.OnKeyUp;
            
            _timer = new Timer { Interval = 60 };
            _timer.Tick += TimerTick;
            _timer.Start();
        }

        private static IEnumerable<ArenaMap> LoadLevels()
        {
            yield return ArenaMap.CreatNewArenaMap(Properties.Resources.Arena1);
            yield return ArenaMap.CreatNewArenaMap(Properties.Resources.Arena2);
            yield return ArenaMap.CreatNewArenaMap(Properties.Resources.Arena3);
            yield return ArenaMap.CreatNewArenaMap(Properties.Resources.Arena4);
            yield return ArenaMap.CreatNewArenaMap(Properties.Resources.Arena5);
        }
        
        private void TimerTick(object sender, EventArgs e)
        {
            _arenaPanel.Update();
        }
    }
}