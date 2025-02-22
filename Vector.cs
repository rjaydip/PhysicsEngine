using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsEngine
{
    public class Vector
    {
        public float X;
        public float Y; 
        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }  
        public static Vector Zero()
        {
            return new Vector(0, 0);    
        }
        public override string ToString() => $"{X},{Y}";
        public float Length()
        {
            return MathF.Sqrt((X * X) + (Y * Y));
        }
        public Vector Normalize()
        {
            float len = Length();
            return new Vector(X / len, Y / len);
        }
        public static Vector GetMiddlePoint(Vector position,Vector scale)
        {
            return new Vector(((int)position.X + (int)scale.X) / 2 , ((int)position.Y + (int)scale.Y) / 2);
        }
        public static Vector GetDirection(Vector to,Vector from)
        {
            return new Vector(to.X-from.X,to.Y-from.Y);
        }
        public static Shape GetClosetShape(Vector position,string tag,Shape Exeptioin)
        {
            List<Shape> shapes = Engine.GetShapes();
            if (shapes.Count == 0) return null;
            Shape currentclosetshape = shapes[0];
            foreach (var item in shapes)
            {
                if(item != currentclosetshape)
                {
                    if(GetDistance(item.position,position) < GetDistance(currentclosetshape.position,position))
                    {
                        currentclosetshape = item;
                    }
                }
            }
            return currentclosetshape;
        }
        public static double GetDistance(Vector point1, Vector point2)
        {
            if(point1 != null && point2 != null)
            {
                double x = Math.Abs(point2.X - point1.X);
                double y = Math.Abs(point2.Y - point1.Y);
                return Math.Sqrt((x * x) + (y * y));
            }
            return 0;
        }
        public static Vector operator +(Vector a,Vector b)
        {
            return new Vector(a.X + b.X , a.Y + b.Y );
        }
        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }
        public static Vector operator *(Vector a, float s)
        {
            return new Vector(a.X * s, a.Y * s);
        }
        public static Vector operator *(float s, Vector b)
        {
            return new Vector(b.X * s, b.Y * s);
        }
        public static Vector operator /(float s, Vector b)
        {
            return new Vector(b.X / s, b.Y / s);
        }
        public static Vector operator /(Vector a, float s)
        {
            return new Vector(a.X / s, a.Y / s);
        }
    }
}
