using System.Windows.Forms;
using Cave_Adventure.Properties;

namespace Cave_Adventure.Views
{
    public class HealBarPanel : Panel
    {
        private readonly HealBar _healBar;
        private bool _configured = false;

        public HealBarPanel()
        {
            _healBar = new HealBar()
            {
                AutoSize = true,
                Dock = DockStyle.Fill
            };
            var table = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            ConfigureTable(table);
            
            Controls.Add(table);
        }

        public void Configure(ArenaMap arenaMap)
        {
            _healBar.Configure(arenaMap);
            _configured = true;
        }
        
        public void Drop()
        {
            _healBar.Drop();
            _configured = false;
        }

        private void ConfigureTable(TableLayoutPanel table)
        {
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 15));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 15));
            
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 0, 0);
            table.Controls.Add(_healBar, 1, 0);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 2, 0);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 0, 1);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 1, 1);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 2, 1);
        }

        public new void Invalidate()
        {
            _healBar.Invalidate();
        }
    }
}