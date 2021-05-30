using System;
using System.Drawing;
using System.Windows.Forms;
using Cave_Adventure.Interfaces;
using Cave_Adventure.Properties;

namespace Cave_Adventure.Views
{
    public class TutorialMenuPanel : Panel, IPanel
    {
        private readonly Game _game;
        private bool _configured = false;

        public TutorialMenuPanel(Game game)
        {
            _game = game;

            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true
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
            var titleLabel = new Label
            {
                Text = "Little TB Game",
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.White,
                Size = new Size(350, 50),
                AutoSize = true,
                Padding = new Padding(25, 10, 0, 0),
                Margin = new Padding(33, 28, 0, 0),
                Font = new Font(SystemFonts.DialogFont.FontFamily, 15),
                BackgroundImage = Properties.Resources.grass1,
            };

            var backToMainMenuButton = new Button
            {
                Text = $"Назад в меню",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Size = new Size(350, 50),
                AutoSize = true,
            };
            backToMainMenuButton.Click += _game.SwitchOnMainMenu;

            var tutorialTitle = new Label
            {
                Text = "Обучение",
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.White,
                AutoSize = true,
                Margin = new Padding(700, 25, 500, 25),
                Font = new Font(SystemFonts.DialogFont.FontFamily, 17),
                BackgroundImage = Properties.Resources.grass5,
            };

            var tutorial = new Label
            {
                Text = "1) Цель: убей всех монстров, чтобы перейти на следующую арену.\n\n" +
                       "2) Система распределения урона:\n\n" +
                       "    а) Атака нападающего > защиты обороняющегося:\n\n" +
                       "        1. Наносится полный урон;\n\n" +
                       "        2. Если атака нападающего >= 1.25 * защиты обороняющегося, то наносится на 50 % урона больше;\n\n" +
                       "    б) Атака нападающего <= защиты обороняющегося:\n\n" +
                       "        1. Наносится на 25 % урона меньше;\n\n" +
                       "        2. Если атака нападающего <= 0.75 * защиты защищающегося, то наносится на 50 % урона меньше.\n\n" +
                       "3) Очки действия тратятся на атаку (все очки) и на передвижение (в зависимости от того, на сколько клеток произойдёт передвижение).\n\n" +
                       "4) Нажав на кнопку \"Следующий ход\", а затем на героя, можно сделать два действия: " +
                       "атаковать монстра (кликнув на него) или перейти на другую клетку (кликнув на клетку).\n\n" +
                       "5) Нажав на кнопку \"Осмотреть\", а затем на какого-либо персонажа, можно ознакомиться с характеристиками.\n\n" +
                       "6) Нажав на кнопку \"Полечись\", можно пополнить запас здоровья.\n\n" +
                       "7) После того, как все монстры на арене будут поражены, переходи на следующую, нажав на кнопку \"Следующий уровень\".\n\n",
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.White,
                AutoSize = true,
                Margin = new Padding(0, 25, 0, 25),
                Font = new Font(SystemFonts.DialogFont.FontFamily, 13),
                BackgroundImage = Properties.Resources.grass5,
            };

            var firstColumn = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackgroundImage = Properties.Resources.grass5,
            };

            var secondColumn = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackgroundImage = Properties.Resources.grass1,
            };

            #region Добавление контролов

            firstColumn.RowStyles.Add(new RowStyle(SizeType.Absolute, 15));
            firstColumn.RowStyles.Add(new RowStyle(SizeType.Percent, 9));
            firstColumn.RowStyles.Add(new RowStyle(SizeType.Percent, 91));
            firstColumn.RowStyles.Add(new RowStyle(SizeType.Absolute, 15));
            firstColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 15));
            firstColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            firstColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 15));

            secondColumn.RowStyles.Add(new RowStyle(SizeType.Percent, 95));
            secondColumn.RowStyles.Add(new RowStyle(SizeType.Percent, 5));
            secondColumn.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
            secondColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 15));
            secondColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            secondColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 15));

            firstColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.endBackground }, 0, 0);
            firstColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.endBackground }, 1, 0);
            firstColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.endBackground }, 2, 0);
            firstColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.endBackground }, 0, 1);
            firstColumn.Controls.Add(tutorialTitle, 1, 1);
            firstColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.endBackground }, 2, 1);
            firstColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.endBackground }, 0, 2);
            firstColumn.Controls.Add(tutorial, 1, 2);
            firstColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.endBackground }, 2, 2);
            firstColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.endBackground }, 0, 3);
            firstColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.endBackground }, 1, 3);
            firstColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.endBackground }, 2, 3);

            secondColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 0, 0);
            secondColumn.Controls.Add(titleLabel, 1, 0);
            secondColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 2, 0);
            secondColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 0, 1);
            secondColumn.Controls.Add(backToMainMenuButton, 1, 1);
            secondColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 2, 1);
            secondColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 0, 2);
            secondColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 1, 2);
            secondColumn.Controls.Add(new Panel() { Dock = DockStyle.Fill, BackgroundImage = Resources.obsidianBackground }, 2, 2);

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 82));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            table.Controls.Add(firstColumn, 0, 0);
            table.Controls.Add(secondColumn, 1, 0);

            #endregion
        }
    }
}