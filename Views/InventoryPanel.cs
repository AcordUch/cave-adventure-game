using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Cave_Adventure.Interfaces;
using Cave_Adventure.Properties;

namespace Cave_Adventure.Views
{
    public class InventoryPanel : Panel
    {
        private const int _shift = 15;

        private readonly ArenaFieldControl _arenaFieldControl;

        private Label _infoLabel;
        private Label _detailInfoLabel;
        private Button _healButton;

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
            _detailInfoLabel.Text = MakeTextForDetailInfoLabel();
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
            _detailInfoLabel = new Label
            {
                TextAlign = ContentAlignment.MiddleLeft,
                Text = MakeTextForDetailInfoLabel(),
                ForeColor = Color.Black,
                Size = this.Size,
                AutoSize = true
            };
            _healButton = new Button()
            {
                Text = $"Полечись",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Size = this.Size,
                AutoSize = true,
                Enabled = true
            };
            _healButton.Click += (sender, args) =>
            {
                Player.UseHealthPotionFromInventory();
            };

            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 80));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 15));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 15));
            
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 0, 0);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 1, 0);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 2, 0);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 0, 1);
            table.Controls.Add(_infoLabel, 1, 1);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 2, 1);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 0, 2);
            table.Controls.Add(_healButton, 1, 2);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 2, 2);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 0, 3);
            table.Controls.Add(_detailInfoLabel, 1, 3);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 2, 3);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 0, 4);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 1, 4);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 2, 4);
        }

        public void OnBlockUnblockUI()
        {
            _healButton.Enabled = !_healButton.Enabled;
        }

        private string MakeTextForInfoLabel()
        {
            if (_arenaFieldControl.Player == null)
                return "";
            return $"В карманах зелий лечения: {Player.Inventory.AmountOfPotion}";
        }

        private string MakeTextForDetailInfoLabel()
        {
            if (_arenaFieldControl.Player == null)
                return "";
            return 
$@"Малых флаконов: {Player.Inventory.AmountSmallPotion}
Средних флаконов: {Player.Inventory.AmountMediumPotion}
Больших флаконов: {Player.Inventory.AmountBigPotion}";
        }
    }
}