using System;
using System.Drawing;
using System.Windows.Forms;
using Cave_Adventure.Interfaces;
using Cave_Adventure.Properties;

namespace Cave_Adventure.Views.Screens
{
    public class TextShowPanel : Panel, IPanel
    {
        private bool _configured = false;

        #region Неиспользуемое
        // public bool TitleVisible { get; set; }
        // public string TitleText { get; set; }
        // public bool FirstButtonVisible { get; set; }
        // public bool SecondButtonVisible { get; set; }
        // public bool ThirdButtonVisible { get; set; }
        // public string FirstButtonText { get; set; }
        // public string SecondButtonText { get; set; }
        // public string ThirdButtonText { get; set; }
        // public string InnerTextString { get; set; }
        
        // public event EventHandler FirstButtonClick;
        // public event EventHandler SecondButtonClick;
        // public event EventHandler ThirdButtonClick;
        
        // Title.Visible = TitleVisible;
        // Title.Text = TitleText;
        //
        // InnerTextLabel.Text = InnerTextString;
        //
        // FirstButton.Text = FirstButtonText;
        // FirstButton.Visible = FirstButtonVisible;
        // SecondButton.Text = SecondButtonText;
        // SecondButton.Visible = SecondButtonVisible;
        // ThirdButton.Text = ThirdButtonText;
        // ThirdButton.Visible = ThirdButtonVisible;
        // FirstButton.Click += FirstButtonClick;
        // SecondButton.Click += SecondButtonClick;
        // ThirdButton.Click += ThirdButtonClick;
        #endregion
        
        public Label Title { get; } = new Label
        {
            Text = "Заголовок",
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = true,
            Margin = new Padding(0, 25, 0, 50),
            Font = new Font(SystemFonts.DialogFont.FontFamily, 17),
            BackgroundImage = Resources.quartzBackground,
        };
        
        public Label InnerTextLabel { get; } = new Label
        {
            Text = "Текст",
            TextAlign = ContentAlignment.MiddleLeft,
            AutoSize = true,
            Margin = new Padding(10, 25, 15, 25),
            Font = new Font(SystemFonts.DialogFont.FontFamily, 15),
            BackgroundImage = Resources.quartzBackground,
        };
        
        public Button FirstButton { get; } = new Button
        {
            Text = $"Первая Кнопка",
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Fill,
            Size = new Size(350, 50),
            AutoSize = true,
            Visible = false
        };
        public Button SecondButton { get; } = new Button
        {
            Text = $"Вторая Кнопка",
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Fill,
            Size = new Size(350, 50),
            AutoSize = true,
            Visible = false
        };
        public Button ThirdButton { get; } = new Button
        {
            Text = $"Третья Кнопка",
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Fill,
            Size = new Size(350, 50),
            AutoSize = true,
            Visible = false
        };
        
        /// <remarks>
        /// Действие на нажатие кнопок нужно проставлять после создания объекта
        /// </remarks>
        public TextShowPanel()
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
            var textTable = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackgroundImage = Resources.quartzBackground
            };
            textTable.Controls.Add(Title);
            textTable.Controls.Add(InnerTextLabel);
            
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
            
            buttonTable.Controls.Add(FirstButton, 0, 0);
            buttonTable.Controls.Add(SecondButton, 1, 0);
            buttonTable.Controls.Add(ThirdButton, 2, 0);
            
            innerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            innerTable.RowStyles.Add(new RowStyle(SizeType.Percent, 94));
            innerTable.RowStyles.Add(new RowStyle(SizeType.Percent, 6));

            innerTable.Controls.Add(textTable, 0, 0);
            innerTable.Controls.Add(buttonTable, 0, 1);
        }
    }
}