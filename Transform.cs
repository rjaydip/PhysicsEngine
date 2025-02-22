using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhysicsEngine
{
    public class Transform
    {
        public double PositionX;
        public double PositionY;
        public double Sin; 
        public double Cos;

        public readonly static Transform Zero = new Transform(0, 0, 0);
        public Transform(Vector position, double angle)
        {
            this.PositionX = position.X;
            this.PositionY = position.Y;
            this.Sin = Math.Sin(angle);
            this.Cos = Math.Cos(angle);
        }
        public Transform(double x, double y, double angle)
        {
            this.PositionX = x; 
            this.PositionY = y;
            this.Sin = Math.Sin(angle);
            this.Cos = Math.Cos(angle);
        }
       
    }
}
