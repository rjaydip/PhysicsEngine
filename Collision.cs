using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PhysicsEngine
{
    public class Collision
    {
        public static void ScreenCollision(Shape shape,Vector ScreenSize)
        {
            if(shape.position.X <= 0 || shape.position.X >= ScreenSize.X)
            {
                shape.Velocity = new Vector(-shape.Velocity.X, shape.Velocity.Y);
                shape.position.X = Clamp(shape.position.X,0, ScreenSize.X);
            }
            if (shape.position.Y <= 0 || shape.position.Y >= ScreenSize.Y)
            {
                shape.Velocity = new Vector(shape.Velocity.X, -shape.Velocity.Y);
                shape.position.Y = Clamp(shape.position.Y, 0, ScreenSize.Y);
            }
        }
        //public static void ShapeCollision(Rectangle rectangle1, Rectangle rectangle2)
        //{
        //    Vector delta = rectangle2.position - rectangle1.position;

        //    float overlapx = rectangle1.Scale.X / 2 + rectangle2.Scale.X / 2 - Math.Abs(delta.X);
        //    float overlapy = rectangle1.Scale.Y / 2 + rectangle2.Scale.Y / 2 - Math.Abs(delta.Y);

        //    if(Math.Abs(overlapx) < Math.Abs(overlapy))
        //    {
        //        if(delta.X > 0)
        //        {
        //            rectangle1.position.X -= overlapx / 2;
        //            rectangle2.position.X += overlapx / 2;
        //        }
        //        else
        //        {
        //            rectangle1.position.X += overlapx / 2;
        //            rectangle2.position.X -= overlapx / 2;
        //        }
        //    }
        //    else
        //    {
        //        if(delta.Y > 0)
        //        {
        //            rectangle1.position.Y -= overlapy / 2;
        //            rectangle2.position.Y += overlapy / 2;
        //        }
        //        else
        //        {
        //            rectangle1.position.Y += overlapy / 2;
        //            rectangle2.position.Y -= overlapy / 2;
        //        }
        //    }

        //    ResolveVelocities(rectangle1, rectangle2);
        //}
        public static void ShapeCollision(Circle circle1,Circle circle2)
        {
            Vector delta = circle2.position - circle1.position;

            float distance = delta.Length();
            float overlap = circle1.Radius + circle2.Radius - distance;

            if(overlap > 0)
            {
                Vector collisionNormal = delta.Normalize();
                Vector relativeVelocity = circle2.Velocity - circle1.Velocity;

                float velocityAlongNormal = PhysicsMath.Dot(relativeVelocity, collisionNormal);

                if (velocityAlongNormal > 0)
                    return;

                float restitution = 0.5f;

                float impulseScalar = -(1 + restitution) * velocityAlongNormal;
                impulseScalar /= (1 / circle1.Mass + 1 / circle2.Mass);

                Vector impulse = impulseScalar * collisionNormal;
                
                circle1.Velocity -= (1/circle1.Mass) * impulse;
                circle2.Velocity += (1/circle2.Mass) * impulse;

                float percent = 0.2f;
                float correction = (overlap / (1/circle1.Mass + 1 / circle2.Mass)) * percent;

                Vector correctionVector = correction * collisionNormal;

                circle1.position -= (1 / circle1.Mass) * correctionVector;
                circle2.position += (1 / circle2.Mass) * collisionNormal;
            }

            //ResolveVelocities(circle1,circle2);
        }
        //public static void ShapeCollision(Rectangle rectangle, Circle circle)
        //{
        //    float closetx = Clamp(circle.position.X, rectangle.position.X, rectangle.position.X + rectangle.Scale.X);
        //    float closety = Clamp(circle.position.Y, rectangle.position.Y, rectangle.position.Y + rectangle.Scale.Y); 

        //    Vector closetPoint = new Vector(closetx, closety);
        //    Vector delta = circle.position - closetPoint;

        //    float distance = delta.Length();
        //    float overlap = circle.Radius - distance;

        //    if(distance != 0)
        //    {
        //        Vector separation = delta * (overlap / distance);
        //        circle.position += separation;
        //    }

        //    ResolveVelocities(rectangle, circle);
        //}
        private static void ResolveVelocities(Circle shape1, Circle shape2)
        {
            Vector normal = shape2.position - shape1.position;
            normal = normal / normal.Length();

            float relativeVelocity = PhysicsMath.Dot(shape2.Velocity - shape1.Velocity, normal);

            if (relativeVelocity > 0)
            {
                float impulse = 2 * relativeVelocity / (shape1.Radius + shape2.Radius);
                shape1.Velocity += normal * (impulse);
                shape2.Velocity -= normal * (impulse);
            }
        }
        public static float Clamp(float value, float min, float max)
        {
            if (min > max)
            {
                throw new Exception("Min is Greater Than Maximun");
            }

            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }
        public static bool AreColliding(Rectangle rectangle, Circle circle)
        {
            float closestX = Clamp(circle.position.X, rectangle.position.X, rectangle.position.X + rectangle.Scale.X);
            float closestY = Clamp(circle.position.Y, rectangle.position.Y, rectangle.position.Y + rectangle.Scale.Y);
            float distanceX = circle.position.X - closestX;
            float distanceY = circle.position.Y - closestY;

            float distanceSquared = (distanceX * distanceX) + (distanceY * distanceY);
            return distanceSquared < (circle.Radius * circle.Radius);
        }
    }
}
