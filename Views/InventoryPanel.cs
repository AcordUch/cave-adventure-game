using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Cave_Adventure.Interfaces;

namespace Cave_Adventure.Views
{
    public class InventoryPanel : Panel
    {
        private const int _shift = 15;

        private readonly ArenaFieldControl _arenaFieldControl;

        private Label _infoLabel;

        private Player Player => _arenaFieldControl.Player;

        public InventoryPanel(ArenaFieldControl arenaFieldControl)
        {
            _arenaFieldControl = arenaFieldControl;

            var table = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            ConfigureTable(table);
            
            Controls.Add(table);
        }

        public new void Update()
        {
            _infoLabel.Text = MakeTextForInfoLabel();
        }

        private void ConfigureTable(TableLayoutPanel table)
        {
            _infoLabel = new Label
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Text = MakeTextForInfoLabel(),
                ForeColor = Color.Black,
                Size = this.Size,
                AutoSize = true
            };
            var healButton = new Button()
            {
                Text = $"Полечись",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Size = this.Size,
                AutoSize = true,
                Enabled = true
            };
            healButton.Click += (sender, args) =>
            {
                var healKit = Player.Inventory.FirstOrDefault(i => i.Tag == ItemType.HealthPotion);
                if(healKit == null) return;
                Player.Health += 35;
                Player.Inventory.Remove(healKit);
            };
            
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 80));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 15));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 15));
            
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackColor = Color.Black }, 0, 0);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackColor = Color.Red }, 1, 0);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackColor = Color.Black }, 2, 0);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackColor = Color.Black }, 0, 1);
            table.Controls.Add(_infoLabel, 1, 1);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackColor = Color.Black }, 2, 1);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackColor = Color.Black }, 0, 2);
            table.Controls.Add(healButton, 1, 2);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackColor = Color.Black }, 2, 2);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackColor = Color.Black }, 0, 3);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackColor = Color.White }, 1, 3);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackColor = Color.Black }, 2, 3);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackColor = Color.Black }, 0, 4);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackColor = Color.Red }, 1, 4);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackColor = Color.Black }, 2, 4);
        }

        private string MakeTextForInfoLabel()
        {
            if (_arenaFieldControl.Player == null)
                return "";
            return $"В ваших карманах лежит {Player.Inventory.Count(i => i.Tag == ItemType.HealthPotion).ToString()} хилки";
        }
    }
}