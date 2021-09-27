using System;
using System.Drawing;
using System.Windows.Forms;
using Cave_Adventure.Interfaces;
using Cave_Adventure.Properties;

namespace Cave_Adventure.Views.Screens
{
    public class ArenaGeneratorPanel : Panel, IPanel
    {
        private bool _configured = false;
        private Button _firstButton;
        private Button _thirdButton;
        
        public ArenaGeneratorPanel()
        {
            this.Dock = DockStyle.Fill;
            
            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackgroundImage = Resources.stoneBackground
            };
            ConfigureTable(table);

            Controls.Add(table);
            
            _firstButton.Click += Game.Instance.SwitchOnMainMenu;
            _thirdButton.Click += CreateLevel;
        }

        protected override void InitLayout()
        {
            base.InitLayout();
            ResizeRedraw = true;
            DoubleBuffered = true;
        }

        public void Configure()
        {
            if (_configured)
                throw new InvalidOperationException();

            Invalidate();
            _configured = true;
        }

        public void Drop()
        {
            _configured = false;
        }

        #region Настройка панелей
        
        private void ConfigureTable(TableLayoutPanel table)
        {
            var innerTable = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackgroundImage = Resources.quartzBackground,
            };
            ConfigureInnerTable(innerTable);
            
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 65));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 65));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
            
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 0, 0);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 1, 0);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 2, 0);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 0, 1);
            table.Controls.Add(innerTable, 1, 1);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 2, 1);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 0, 2);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 1, 2);
            table.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 2, 2);
        }
        
        private void ConfigureInnerTable(TableLayoutPanel innerTable)
        {
            var textTable = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackgroundImage = Resources.quartzBackground
            };
            var title = new Label
            {
                Text = "Создание арены",
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
                Margin = new Padding(0, 25, 0, 50),
                Font = new Font(SystemFonts.DialogFont.FontFamily, 17),
                BackgroundImage = Resources.quartzBackground,
            };
            var levelTable = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackgroundImage = Resources.quartzBackground,
            };
            textTable.Controls.Add(title);
            textTable.Controls.Add(levelTable);
            
            var buttonTable = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackgroundImage = Resources.andesiteBackground,
            };
            _firstButton = new Button
            {
                Text = $"Назад в меню",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Size = new Size(350, 50),
                AutoSize = true,
            };
            
            _thirdButton = new Button
            {
                Text = $"Создать арену",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Size = new Size(350, 50),
                AutoSize = true,
            };
            
            buttonTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            buttonTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
            buttonTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            buttonTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            
            buttonTable.Controls.Add(_firstButton, 0, 0);
            //buttonTable.Controls.Add(SecondButton, 1, 0);
            buttonTable.Controls.Add(_thirdButton, 2, 0);
            
            innerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            innerTable.RowStyles.Add(new RowStyle(SizeType.Percent, 94));
            innerTable.RowStyles.Add(new RowStyle(SizeType.Percent, 6));

            innerTable.Controls.Add(textTable, 0, 0);
            innerTable.Controls.Add(buttonTable, 0, 1);
        }
        
        #endregion
        
        public void CreateLevel(object sender, EventArgs e)
        {
            var arenaGen = new ArenaGenerator();
            Game.Instance.SwitchOnArenas(arenaGen.CreateArena());
        }
    }
}