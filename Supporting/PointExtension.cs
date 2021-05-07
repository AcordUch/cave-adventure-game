using System.Drawing;

namespace Cave_Adventure
{
    public static class PointExtension
    {
        public static Point NegativePoint(this Point point)
        {
            return new Point(-1, -1);
        }
    }
}