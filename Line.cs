using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsEngine
{
    public class Line 
    {
        public Vector startPoint {  get; set; }
        public Vector endPoint { get; set; }
        public Color color { get; set; }
        public Line(Vector point1, Vector point2, Color color)
        {
            startPoint = point1;
            endPoint = point2;
            this.color = color;
        }
        public void Draw(Graphics graphics)
        {
            graphics.DrawLine(new Pen(color, 5), startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
        }
    }
}
