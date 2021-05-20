using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cave_Adventure.Views
{
    public partial class CheatMenu : Form
    {
        private readonly TextBox _textBox;
        private ArenaFieldControl _arenaFieldControl;
        private bool _configured = false;

        private readonly AutoCompleteStringCollection _autoCompleteSource = new AutoCompleteStringCollection
        {
            "kill", "killbyconsole", "completelevel"
        };

        public ArenaMap ArenaMap => _arenaFieldControl.ArenaMap;
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
            Size = new Size(450, 175);
            Text = "Запомните дети, консоль нужна для debug'а, а не для баловства";
            KeyPreview = true;
        }
        
        public CheatMenu()
        {
            InitializeComponent();
            _textBox = new TextBox()
            {
                Dock = DockStyle.Fill,
                AutoCompleteSource = AutoCompleteSource.CustomSource,
                AutoCompleteCustomSource = _autoCompleteSource,
                AutoCompleteMode = AutoCompleteMode.Suggest
            };
            _textBox.KeyDown += (sender, args) =>
            {
                if (args.KeyCode == Keys.Enter)
                {
                    ProcessingEnterCheatCode();
                    args.Handled = true;
                    args.SuppressKeyPress = true;
                }
            };
            
            Controls.Add(_textBox);
        }

        public void Configure(ArenaFieldControl arenaFieldControl)
        {
            _arenaFieldControl = arenaFieldControl;
            _configured = true;
        }

        private void ProcessingEnterCheatCode()
        {
            if (!_configured)
            {
                _textBox.Text = "CheatMenu не работает";
                return;
            }
            
            _textBox.ReadOnly = true;
            
            var superMonster = new SuperMonster(new Point(-1, -1));
            switch (_textBox.Text.ToLower())
            {
                case "kill":
                    _arenaFieldControl.ArenaMap.Attacking(superMonster, _arenaFieldControl.Player);
                    break;
                case "killbyconsole":
                    _arenaFieldControl.Player.Defending(superMonster);
                    break;
                case "completelevel":
                    _arenaFieldControl.ArenaMap.CompleteLevel(this);
                    break;
            }
            _textBox.Clear();
            
            _textBox.ReadOnly = false;
        }
    }
}