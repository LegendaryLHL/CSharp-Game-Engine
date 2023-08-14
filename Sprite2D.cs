using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.CodeDom.Compiler;

namespace GameEngine
{
    public class Sprite2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string Directory = null;
        public string Tag = "";
        public Bitmap Sprite = null;
        public bool IsRefrence = false;

        public Sprite2D(Vector2 Position, Vector2 Scale, string Directory, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Directory = Directory;
            this.Tag = Tag;

            Image temp = Image.FromFile($"Assets/{Directory}.png");
            Bitmap sprite = new Bitmap(temp, (int)this.Scale.x, (int)this.Scale.y);
            Sprite = sprite;

            GameEngine.RegisterSprite(this);
        }
        public Sprite2D(string Directory)
        {
            this.IsRefrence = true;
            this.Directory = Directory;

            Image temp = Image.FromFile($"Assets/{Directory}.png");
            Bitmap sprite = new Bitmap(temp);
            Sprite = sprite;

            GameEngine.RegisterSprite(this);
        }
        public Sprite2D(Vector2 Position, Vector2 Scale, Bitmap Refrence, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Tag = Tag;

            Sprite = Refrence;

            GameEngine.RegisterSprite(this);
        }

        public bool IsColiding(Sprite2D a, Sprite2D b)
        {
            if (a.Position.x < b.Position.x + b.Scale.x && 
                a.Position.x + a.Scale.x > b.Position.x &&
                a.Position.y < b.Position.y + b.Scale.y &&
                a.Position.y + a.Scale.y > b.Position.y)
            {
                return true;
            }
            return false;
        }
        public bool IsColiding(string tag)
        {
            foreach (Sprite2D b in GameEngine.AllSprite)
            {
                if (b.Tag == tag)
                {
                    if (Position.x < b.Position.x + b.Scale.x &&
                        Position.x + Scale.x > b.Position.x &&
                        Position.y < b.Position.y + b.Scale.y &&
                        Position.y + Scale.y > b.Position.y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void DestroySelf()
        {
            GameEngine.UnRegisterSprite(this);
        }
    }
}
