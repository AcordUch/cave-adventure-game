using System.Drawing;

namespace Cave_Adventure
{
    public interface IMonster : IEntity
    {
        MonsterType Tag { get; set; }
    }
}