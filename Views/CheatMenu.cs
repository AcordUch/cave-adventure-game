using System;
using System.Drawing;
using System.Linq;
using System.Text;
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
            "kill", "killbyconsole", "completelevel", "heal", "zatva", "makemehurt", "makemezerohp", "tdebug", "help",
            "simpledrawmode", "tp"
        };

        public ArenaMap ArenaMap => _arenaFieldControl.ArenaMap;

        public event Action ChangeDebug;
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
            Size = new Size(450, 575);
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
                AutoCompleteMode = AutoCompleteMode.Suggest,
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
            var label = new Label()
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                Location = new Point(0, _textBox.Height + 25),
                Text = MakeListOfCommand(false),
            };
            
            Controls.Add(_textBox);
            Controls.Add(label);
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

            var needClear = true;
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
                case "heal":
                    _arenaFieldControl.Player.Health = GlobalConst.PlayerHP;
                    break;
                case "zatva":
                    foreach (var monster in _arenaFieldControl.Monsters)
                    {
                        monster.Defending(superMonster);
                    }
                    break;
                case "makemehurt":
                    _arenaFieldControl.Player.Health = 1;
                    break;
                case "makemezerohp":
                    _arenaFieldControl.Player.Health = 0;
                    break;
                case "tdebug":
                    ChangeDebug?.Invoke();
                    break;
                case "help":
                    needClear = false;
                    _textBox.Text = MakeListOfCommand();
                    break;
                case "simpledrawmode":
                    _arenaFieldControl.ArenaPainter.OnSimpleMode();
                    break;
                case "tp":
                    _arenaFieldControl.ArenaMap.ChangeMoveMode(this);
                    break;
            }
            if(needClear)
                _textBox.Clear();
            
            _textBox.ReadOnly = false;
        }

        private string MakeListOfCommand(bool inLine = true)
        {
            var appendChar = inLine ? " " : "\n";
            var strBld = new StringBuilder();
            foreach (var com in _autoCompleteSource)
            {
                strBld.Append(com.ToString()).Append(appendChar);
            }
            return strBld.ToString();
        }
    }
}