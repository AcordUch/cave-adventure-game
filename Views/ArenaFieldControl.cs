using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using Cave_Adventure.Objects.Items;

namespace Cave_Adventure
{
    public partial class ArenaFieldControl : UserControl
    {
        private const int ShiftFromUpAndDownBorder = 10;
        private const int CellWidth = GlobalConst.AssetsSize;
        private const int CellHeight = GlobalConst.AssetsSize;
        
        private readonly EntityPainter _entityPainter;
        private PointF _logicalCenterPos;
        private bool _configured = false;
        private Dictionary<Point, Rectangle> _pointToRectangle;
        
        public ArenaMap ArenaMap { get; private set; }
        public ArenaPainter ArenaPainter { get; }
        
        public Player Player => ArenaMap?.Player;

        public Monster[] Monsters => ArenaMap.Monsters;

        public event Action BindEvent;
        
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
            
            LoadLevel(arenaMap);
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
        
        public void LoadLevel(string newMap)
        {
            ArenaMap = ArenaMap.CreateNewArenaMap(newMap);
            _pointToRectangle = GeneratePointToRectangle(this, ArenaMap);
            ArenaPainter.Configure(ArenaMap, _pointToRectangle);
            _entityPainter.Configure(ArenaMap.GetListOfEntities());
            BindEvent?.Invoke();
            for (int i = 0; i < 3; i++)
            {
                Player.Inventory.AddHeals(new HealthPotionSmall());
            }
            Invalidate();
        }
        
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
            
            _logicalCenterPos = new PointF(sceneSize.Width / 2f, sceneSize.Height / 2f);
            
            ArenaPainter.Paint(e.Graphics);
            
            _entityPainter.SetUpAndPaint(e.Graphics, ArenaMap.Player);
            ArenaPainter.Update();       
            
            foreach (var monster in ArenaMap.Monsters)
                _entityPainter.SetUpAndPaint(e.Graphics, monster);
        }

        #region Не используемое
        /*
        private int _zoomScale;
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
        */
        #endregion
        
        public string PlayerInfoToString()
        {
            return ArenaMap == null ? "null" : 
$@"Текущий ход: {ArenaMap.Step}
Имеющиеся очки действия: {Player.AP}
Здоровье игрока: {Player.Health}";
        }

        public string DebugInfo()
        {
            return ArenaMap == null ? "null" : 
$@"   ||DEBUG||
ArenaLogPos: {_logicalCenterPos}
PlayerPos: {Player.Position} | PlayerTarget: {Player.TargetPoint}
IsSelected: {Player.IsSelected} | IsMoving: {Player.IsMoving}
State: {Player.CurrentStates} | Monster: {GetMonsterInfo()}
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