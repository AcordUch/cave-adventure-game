﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Cave_Adventure.Interfaces;
using Cave_Adventure.Properties;
using ContentAlignment = System.Drawing.ContentAlignment;

namespace Cave_Adventure.Views
{
    public class Tutorial1Panel : Panel, IPanel
    {
        private readonly Game _game;
        private Button _nextScreenButton;
        private bool _configured = false;

        public Tutorial1Panel(Game game)
        {
            _game = game;

            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackgroundImage = Resources.stoneBackground
            };
            ConfigureTable(table);

            Controls.Add(table);
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
            var tutorialTitle = new Label
            {
                Text = "Обучение",
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
                Margin = new Padding(0, 25, 0, 50),
                Font = new Font(SystemFonts.DialogFont.FontFamily, 17),
                BackgroundImage = Resources.quartzBackground,
            };

            var tutorial = new Label
            {
                Text = Resources.Tutorial1,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = true,
                Margin = new Padding(10, 25, 15, 25),
                Font = new Font(SystemFonts.DialogFont.FontFamily, 15),
                BackgroundImage = Resources.quartzBackground,
            };
            
            var backToMainMenuButton = new Button
            {
                Text = $"Погодите, хочу назад",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Size = new Size(350, 50),
                AutoSize = true,
            };
            backToMainMenuButton.Click += _game.SwitchOnStoryIntroPanel;
            
            var goToBattleButton = new Button
            {
                Text = $"Хочу уже играть",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Size = new Size(350, 50),
                AutoSize = true,
            };
            goToBattleButton.Click += _game.SwitchOnArenas;
            
            _nextScreenButton = new Button
            {
                Text = $"Хмм, понял, давай дальше",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Size = new Size(350, 50),
                AutoSize = true,
            };
            _nextScreenButton.Click += _game.SwitchOnTutorial2;

            var textTable = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackgroundImage = Resources.quartzBackground
            };
            textTable.Controls.Add(tutorialTitle);
            textTable.Controls.Add(tutorial);
            
            var buttonTable = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackgroundImage = Resources.andesiteBackground,
            };
            
            buttonTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            buttonTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
            buttonTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            buttonTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            
            buttonTable.Controls.Add(backToMainMenuButton, 0, 0);
            buttonTable.Controls.Add(goToBattleButton, 1, 0);
            buttonTable.Controls.Add(_nextScreenButton, 2, 0);
            
            innerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            innerTable.RowStyles.Add(new RowStyle(SizeType.Percent, 94));
            innerTable.RowStyles.Add(new RowStyle(SizeType.Percent, 6));

            innerTable.Controls.Add(textTable, 0, 0);
            innerTable.Controls.Add(buttonTable, 0, 1);
        }
    }
}