using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsEngine
{
    public class PhysicsMath
    {
        public static float Distance(Vector a,Vector b)
        {
            float dx = a.X - b.X;
            float dy = a.Y - b.Y;
            return MathF.Sqrt(dx * dx + dy * dy);
        }
        public static float Dot(Vector a,Vector b)
        {
            return a.X * b.X + a.Y * b.Y;
        }
        public static float Cross(Vector a,Vector b)
        {
            return a.X * b.Y - a.Y * b.X; 
        }
    }
}
