using System;
using System.Drawing;

namespace Cave_Adventure
{
    public static class PointExtension
    {
        public static Point NegativePoint(this Point point)
        {
            return new Point(-1, -1);
        }

        public static double RangeToPoint(this Point firstPoint, Point secondPoint)
        {
            return Math.Sqrt(Math.Pow(firstPoint.X - secondPoint.X, 2) + Math.Pow(firstPoint.Y - secondPoint.Y, 2));
        }
    }
}