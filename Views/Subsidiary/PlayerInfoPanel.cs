using System.Drawing;
using System.Windows.Forms;
using Cave_Adventure.Properties;

namespace Cave_Adventure.Views
{
    public class PlayerInfoPanel : Panel
    {
        private readonly FlowLayoutPanel _infoPanel;
        private ArenaFieldControl _arenaFieldControl;
        private Label _infoLabel;
        private Label _debugInfo;
        private bool _configured = false;

        public PlayerInfoPanel()
        {
            _infoPanel = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.LeftToRight,
                Dock = DockStyle.Fill,
                AutoSize = true,
                Padding = new Padding(20, 0, 0, 0),
                Font = new Font(SystemFonts.DialogFont.FontFamily, 10)
            };
            SetUpInfoPanel(_infoPanel);
            
            var table = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            ConfigureTable(table);
            
            Controls.Add(table);
        }

        public void Configure(ArenaFieldControl arenaFieldControl)
        {
            _arenaFieldControl = arenaFieldControl;
            _configured = true;
        }
        
        public void Drop()
        {
            _configured = false;
        }

        public new void Update()
        {
            _infoLabel.Text = _arenaFieldControl.PlayerInfoToString();
            _debugInfo.Text = _arenaFieldControl.DebugInfo();
        }

        private void ConfigureTable(TableLayoutPanel table)
        {
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 15));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 15));
            
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 0, 0);
            table.Controls.Add(_infoPanel, 1, 0);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 2, 0);
        }
        
        private void SetUpInfoPanel(Control infoPanel)
        {
            infoPanel.Controls.Add(new Label
            {
                Text = "Информация о персонаже:",
                ForeColor = Color.Black,
                Size = new Size(350, 30),
                Margin = new Padding(0, 20, 0, 0)
            });
            _infoLabel = new Label
            {
                Text = "",
                ForeColor = Color.Black,
                Size = new Size(450, (int)(Height * 0.1)),
                AutoSize = true,
                Margin = new Padding(10, 0, 0, 0)
            };
            _debugInfo = new Label
            {
                Text = "",
                ForeColor = Color.Black,
                Size = new Size(450, (int)(Height * 0.1)),
                AutoSize = true,
                Margin = new Padding(10, 0, 0, 0),
                Visible = false
            };
            infoPanel.Controls.Add(_infoLabel);
            infoPanel.Controls.Add(_debugInfo);
        }

        public void OnChangeDebug()
        {
            _debugInfo.Visible = !_debugInfo.Visible;
        }
    }
}