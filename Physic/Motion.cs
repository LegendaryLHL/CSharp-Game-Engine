using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEngine
{
    public class Motion
    {
        public static float G = 6.67430f * (float)Math.Pow(10, -11);
        public static void ScaleGravity(int scale)
        {
            G *= (float)Math.Pow(10, scale);
        }

        public static Vector2 GetDisplacement(Vector2 velocity, float time)
        {
            return velocity.Multiply(time);
        }

        public static Vector2 FindGravity(PhysicObject self, PhysicObject other)
        {
            Vector2 direction = other.GraphicElement.Position.Subtract(self.GraphicElement.Position);
            float distance = direction.Magnitude();
            float forceMagnitude = (G * self.Mass * other.Mass) / (distance * distance);
            Vector2 force = direction.Normalized().Multiply(forceMagnitude);
            return force;
        }
        public static float FindDistance(PhysicObject self, PhysicObject other)
        {
            return other.GraphicElement.Position.Subtract(self.GraphicElement.Position).Magnitude();
        }
        public static float FindDistance(Vector2 self, Vector2 other)
        {
            return other.Subtract(self).Magnitude();
        }
        public static void Move(PhysicObject obj)
        {
            Vector2 displacement = GetDisplacement(obj.Velocity, GameEngine.DeltaTime);
            PhysicObject collidingObj = obj.hypotheticalCollision(obj.GraphicElement.Position.Add(displacement));
            if (collidingObj != null)
            {
                handleObjCollision(obj, collidingObj);
            }
            displacement = GetDisplacement(obj.Velocity, GameEngine.DeltaTime);
            obj.GraphicElement.Position = obj.GraphicElement.Position.Add(displacement);
        }

        public static void handleObjCollision(PhysicObject obj1, PhysicObject obj2)
        {
            obj1.Velocity = CollisionVelocity(obj1.Mass, obj2.Mass, obj1.Velocity, obj2.Velocity);
            obj2.Velocity = CollisionVelocity(obj2.Mass, obj1.Mass, obj2.Velocity, obj1.Velocity);
        }

        public static Vector2 CollisionVelocity(float m1, float m2, Vector2 v1, Vector2 v2)
        {
            return v1.Multiply(m1 - m2).Add(v2.Multiply(2 * m2)).Divide(m1 + m2);
        }

        public static Vector2 VelocityChangeFromForce(Vector2 totalForce, PhysicObject obj)
        {
            return totalForce.Divide(obj.Mass).Multiply(GameEngine.DeltaTime);
        }
    }
}
