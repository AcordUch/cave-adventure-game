using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Cave_Adventure
{
    public class MainMenuPanel : Panel
    {
        
        public MainMenuPanel()
        {
            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true
            };
        }
        
        protected override void InitLayout()
        {
            base.InitLayout();
            ResizeRedraw = true;
            DoubleBuffered = true;
        }

        private void ConfigureTables(TableLayoutPanel table)
        {
            var buttonMenu = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackColor = Color.Red,
                Padding = new Padding(25, 10, 0, 0),
                Font = new Font(SystemFonts.DialogFont.FontFamily, 12)
            };
            SetUpButtonMenu(buttonMenu);
            
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
        }

        private void SetUpButtonMenu(FlowLayoutPanel buttonMenu)
        {
            buttonMenu.Controls.Add(new Label
            {
                Text = "Choose arena:",
                ForeColor = Color.Black,
                Size = new Size(350, 50),
                Margin = new Padding(0, 25, 0, 0)
            });
            
            var link = new LinkLabel
            {
                Text = "Arenas",
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(100, 35),
                Margin = new Padding(0, 20, 0, 5),
            };
            link.LinkClicked += (sender, args) =>
            {
                //TODO
            };
            buttonMenu.Controls.Add(link);
        }
    }
}