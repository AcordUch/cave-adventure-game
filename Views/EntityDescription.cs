using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cave_Adventure.Views
{
    public partial class EntityDescription : Form
    {
        private readonly Entity _entity;
        private readonly Label _infoLabel;
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
            _infoLabel = new Label
            {
                Text = $"{WriteEntityDescription()}",
                ForeColor = Color.Black,
                Size = this.Size,
                // Margin = new Padding(10, 0, 0, 0)
            };
            
            Controls.Add(_infoLabel);
        }
        
        public void Configure(ArenaFieldControl arenaFieldControl)
        {
            _configured = true;
        }

        private string WriteEntityDescription()
        {
            return $@"
Название: {_entity.ToString()}
Описание: {_entity.Description}
Оружие: {_entity.Weapon.ToString()}
Жизни: {_entity.Health} из {_entity.MaxHealth}
ОД: {_entity.AP} из {_entity.MaxAP}
Инициатива {_entity.Initiative}
Атака: {_entity.Attack}
Защита: {_entity.Defense}
Урон: {_entity.Damage}
";
        }
    }
}