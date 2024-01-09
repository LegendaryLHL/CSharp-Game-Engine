using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Vector2
    {
        public float x { get; set; }
        public float y { get; set; }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2 Add(Vector2 v)
        {
            return new Vector2(x + v.x, y + v.y);
        }
        public Vector2 Subtract(Vector2 v)
        {
            return new Vector2(x - v.x, y - v.y);
        }
        public Vector2 Multiply(float f)
        {
            return new Vector2(x * f, y * f);
        }
        public Vector2 Divide(float f)
        {
            return new Vector2(x / f, y / f);
        }

        public float Magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y);
        }

        public Vector2 Normalized()
        {
            return new Vector2(x / Magnitude(), y / Magnitude());
        }

        public static Vector2 Zero()
        {
            return new Vector2(0, 0);
        }
    }
}
