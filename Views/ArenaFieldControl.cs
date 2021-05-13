using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Cave_Adventure
{
    public partial class ArenaFieldControl : UserControl
    {
        private const int ShiftFromUpAndDownBorder = 10;
        private const int CellWidth = GlobalConst.AssetsSize;
        private const int CellHeight = GlobalConst.AssetsSize;
        
        private readonly EntityPainter _entityPainter;
        private int _zoomScale;
        private PointF _logicalCenterPos;
        private bool _configured = false;
        private Dictionary<Point, Rectangle> _pointToRectangle;

        public int ArenaId { get; set; }
        public ArenaMap ArenaMap { get; private set; }
        public ArenaPainter ArenaPainter { get; }
        
        public Player Player => ArenaMap.Player;

        public Monster[] Monsters => ArenaMap.Monsters;
        

        public ArenaFieldControl()
        {
            InitializeComponent();
            DoubleBuffered = true;
            ArenaPainter = new ArenaPainter();
            _entityPainter = new EntityPainter();

            Click += HandleClick;
        }
        
        public event Action<Point, MouseEventArgs> ClickOnPoint;

        public void Configure(string arenaMap)
        {
            if (_configured)
                throw new InvalidOperationException();
            
            ArenaMap = ArenaMap.CreateNewArenaMap(arenaMap);
            _pointToRectangle = GeneratePointToRectangle(this, ArenaMap);
            ArenaPainter.Configure(ArenaMap, _pointToRectangle);
            _entityPainter.Configure(ArenaMap.GetListOfEntities());
            _configured = true;
        }

        public void Drop()
        {
            ArenaPainter.Drop();
            _entityPainter.Drop();
            _configured = false;
        }
        
        public new void Update()
        {
            Invalidate();
        }
        
        protected override void InitLayout()
        {
            base.InitLayout();
            ResizeRedraw = true;
            DoubleBuffered = true;
        }
        
        public void ChangeLevel(string newMap)
        {
            ArenaMap = ArenaMap.CreateNewArenaMap(newMap);
            _pointToRectangle = GeneratePointToRectangle(this, ArenaMap);
            ArenaPainter.ChangeLevel(ArenaMap, _pointToRectangle);
            _entityPainter.ReConfigure(ArenaMap.GetListOfEntities());
            Invalidate();
        }

        #region KeyControl
        // public void OnKeyUp(object sender, KeyEventArgs e)
        // {
        //     _player.Move(0, 0);
        //     _player.SetAnimationConfiguration(StatesOfAnimation.Idle);
        // }

        // public void OnKeyDown(object sender, KeyEventArgs e)
        // {
        //     switch (e.KeyCode)
        //     {
        //         case Keys.W:
        //             _player.Move(0, -5);
        //             _player.SetAnimationConfiguration(StatesOfAnimation.Run);
        //             break;
        //         case Keys.S:
        //             _player.Move(0, 5);
        //             _player.SetAnimationConfiguration(StatesOfAnimation.Run);
        //             break;
        //         case Keys.A:
        //             _player.Move(-5, 0);
        //             _player.SetAnimationConfiguration(StatesOfAnimation.Run);
        //             _player.ViewDirection = ViewDirection.Left;
        //             break;
        //         case Keys.D:
        //             _player.Move(5, 0);
        //             _player.SetAnimationConfiguration(StatesOfAnimation.Run);
        //             _player.ViewDirection = ViewDirection.Right;
        //             break;
        //         case Keys.Space:
        //             _player.SetAnimationConfiguration(StatesOfAnimation.Attack);
        //             break;
        //     }
        // }
        #endregion
        
        private void HandleClick(object sender, EventArgs e)
        {
            if (!_configured)
                return;

            var args = e as MouseEventArgs;
            var pairs = _pointToRectangle
                .Where(it => it.Value.Contains(args.Location))
                .ToList();
            if (pairs.Count > 0)
                ClickOnPoint?.Invoke(pairs[0].Key, args);
        }
        
        private static Dictionary<Point, Rectangle> GeneratePointToRectangle(ArenaFieldControl arenaFieldControl, ArenaMap arenaMap)
        {
            var result = new Dictionary<Point, Rectangle>();
            for (int x = 0; x < arenaMap.Width; x++)
            for (int y = 0; y < arenaMap.Height; y++)
            {
                var rec = new Rectangle(x * CellWidth, y * CellHeight, CellWidth, CellHeight);
                result.Add(new Point(x, y), rec);
            }
            return result;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!_configured)
                return;
            
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            
            var sceneSize = ArenaPainter.ArenaSize;
            
            UpdateZoomScale();
            _logicalCenterPos = new PointF(sceneSize.Width / 2f, sceneSize.Height / 2f);
            
            ArenaPainter.Paint(e.Graphics);
            
            _entityPainter.SetUpAndPaint(e.Graphics, ArenaMap.Player);
            ArenaPainter.Update();       
            
            foreach (var monster in ArenaMap.Monsters)
                _entityPainter.SetUpAndPaint(e.Graphics, monster);
        }
        
        private PointF GetShift()
        {
            return new PointF(ClientSize.Width / 2f - _logicalCenterPos.X * _zoomScale,
                ClientSize.Height / 2f - _logicalCenterPos.Y * _zoomScale);
        }
        
        private void UpdateZoomScale()
        {
            _zoomScale = ClientSize.Height / ArenaPainter.ArenaSize.Height - ShiftFromUpAndDownBorder;
        }
        
        private static PointF ToLogical(ArenaFieldControl arenaFieldControl, Point point)
        {
            var shift = arenaFieldControl.GetShift();
            return new PointF(
                (point.X - shift.X) / arenaFieldControl._zoomScale,
                (point.Y - shift.Y) / arenaFieldControl._zoomScale);
        }

        private static Point ToGraphic(ArenaFieldControl arenaFieldControl, Point point)
        {
            var shift = arenaFieldControl.GetShift();
            return new Point(
                (int)(point.X * arenaFieldControl._zoomScale + shift.X),
                (int)(point.Y * arenaFieldControl._zoomScale + shift.Y));
        }
        
        public string PlayerInfoToString()
        {
            return ArenaMap == null ? "null" : 
$@"Health: {Player.Health} | Step: {ArenaMap.Step}
Attack: {Player.Attack} | Defense: {Player.Defense}
Damage: {Player.Damage} | AP: {Player.AP}
||DEBUG||
Zoom: {_zoomScale} | ArenaLogPos: {_logicalCenterPos}
Position: {Player.Position} | Target: {Player.TargetPoint}
IsSelected: {Player.IsSelected} | IsMoving: {Player.IsMoving}
State: {Player.CurrentStates} | AttackButtonPres: {ArenaMap.AttackButtonPressed} 
Monster: {GetMonsterInfo()}
";
        }

        private string MonsterPositionsToString()
        {
            var result = new StringBuilder();
            var counter = 0;
            foreach (var monster in ArenaMap.Monsters)
            {
                result.Append($"{monster.Position} ");
                counter++;
                if (counter == 3)
                {
                    result.Append("\n");
                    counter = 0;
                }
            }
            return result.ToString();
        }

        private string GetMonsterInfo()
        {
            var result = new StringBuilder();
            foreach (var monster in ArenaMap.Monsters)
            {
                result.Append($"{monster.ToString()}: HP: {monster.Health}; {monster.Position}\n");
            }

            return result.ToString();
        }
    }
}