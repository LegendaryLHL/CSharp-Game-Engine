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
        public static Vector2 GravitationalAcceleration = new Vector2(0, 9.81f);
        //public static float G = 6.67430f * (float)Math.Pow(10, -11);
        // Amplify effects of gravity
        public static float G = 6.67430f * (float)Math.Pow(10, -3);

        public static Vector2 GetDisplacement(Vector2 velocity, float time)
        {
            return new Vector2(velocity.x * time, velocity.y * time);
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
            if (!obj.hypotheticalCollision(obj.GraphicElement.Position.Add(GetDisplacement(obj.Velocity, GameEngine.DeltaTime))))
            {
                obj.GraphicElement.Position = obj.GraphicElement.Position.Add(GetDisplacement(obj.Velocity, GameEngine.DeltaTime));
            }
        }
    }
}
