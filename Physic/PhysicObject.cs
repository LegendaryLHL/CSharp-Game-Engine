using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace GameEngine
{
    public class PhysicObject
    {
        static List<PhysicObject> AllPhysicObject = new List<PhysicObject>();
        public GraphicElement GraphicElement;
        public Vector2 Velocity = new Vector2(0, 0);
        public int Mass;
        public Action Update = ()=> {};
        public PhysicObject(GraphicElement graphicElement)
        {
            GraphicElement = graphicElement;

            AllPhysicObject.Add(this);
        }

        public static void PhysicUpdate()
        {
            foreach (var physicObject in AllPhysicObject)
            {
                physicObject.Update();
                Motion.Move(physicObject);
            }
        }

        public void ApplyGravity()
        {
            Vector2 totalForce = new Vector2(0, 0);
            foreach (var other in AllPhysicObject)
            {
                if (other != this)
                {
                    Vector2 force = Motion.FindGravity(this, other);
                    totalForce = totalForce.Add(force);
                }
            }
            Velocity = Velocity.Add(Motion.VelocityChangeFromForce(totalForce, this));
        }

        public void ApplyAirResistance(float k)
        {
            // Calculate the air resistance force
            Vector2 airResistance = Velocity.Multiply(-k);

            Velocity = Velocity.Add(Motion.VelocityChangeFromForce(airResistance, this));
        }


        public PhysicObject closestObject()
        {
            if (AllPhysicObject.Count > 1)
            {
                PhysicObject closestObject = null;
                float distScore = float.MaxValue;
                foreach (var obj in AllPhysicObject)
                {
                    if (obj != this)
                    {
                        float dist = Motion.FindDistance(GraphicElement.Position, obj.GraphicElement.Position);
                        
                        if (dist < distScore)
                        {
                            distScore = dist;
                            closestObject = obj;
                        }
                    }
                }
                return closestObject;
            }
            return null;
        }

        public bool hypotheticalCollision(Vector2 position)
        {
            if (AllPhysicObject.Count > 1) {
                PhysicObject closestObject = null;
                float distScore = float.MaxValue;
                foreach (var obj in AllPhysicObject)
                {
                    if (obj != this)
                    {
                        float dist = Motion.FindDistance(position, obj.GraphicElement.Position);
                        
                        if (dist < distScore)
                        {
                            distScore = dist;
                            closestObject = obj;
                        }
                    }
                }
                if (closestObject != null)
                {
                    if (GraphicElement.Tag == "player")
                    {
                        //Console.WriteLine(position.x < closestObject.GraphicElement.Position.x);
                    }
                        if (position.x < closestObject.GraphicElement.Position.x + closestObject.GraphicElement.Scale.x &&
                        position.x + GraphicElement.Scale.x > closestObject.GraphicElement.Position.x &&
                    position.y < closestObject.GraphicElement.Position.y + closestObject.GraphicElement.Scale.y &&
                        position.y + GraphicElement.Scale.y > closestObject.GraphicElement.Position.y)
                    {
                        return true;
                    }
                }
            }
                return false;
        }
    }
}
