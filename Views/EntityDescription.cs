using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cave_Adventure.Views
{
    public partial class EntityDescription : Form
    {
        private readonly Entity _entity;
        private bool _configured = false;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
            Size = new Size(500, 650);
            Text = "А ты, любопытный с:";
            KeyPreview = true;
        }
        
        public EntityDescription(Entity entity)
        {
            _entity = entity;
            InitializeComponent();
        }
        
        public void Configure(ArenaFieldControl arenaFieldControl)
        {
            _configured = true;
        }
    }
}