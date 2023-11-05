using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Policy;

namespace GameEngine
{
    public class Sprite : GraphicElement
    {
        public string Directory = null;
        public Bitmap SpriteImg = null;
        public bool IsRefrence = false;

        public Sprite(Vector2 position, Vector2 scale, string Directory, string Tag)
        {
            Position = position;
            Scale = scale;
            this.Directory = Directory;
            this.Tag = Tag;

            Image temp = Image.FromFile($"../../Assets/Images/{Directory}.png");
            Bitmap sprite = new Bitmap(temp, (int)this.Scale.x, (int)this.Scale.y);
            SpriteImg = sprite;

            GameEngine.RegisterGraphicElement(this);
        }
        public Sprite(string Directory, bool IsRefrence)
        {
            this.IsRefrence = IsRefrence;
            this.Directory = Directory;

            Image temp = Image.FromFile($"../../Assets/Images/{Directory}.png");
            Bitmap sprite = new Bitmap(temp);
            SpriteImg = sprite;

            GameEngine.RegisterGraphicElement(this);
        }
        public Sprite(Vector2 position, Vector2 scale, Bitmap Refrence, string Tag)
        {
            Position = position;
            Scale = scale;
            this.Tag = Tag;

            SpriteImg = Refrence;

            GameEngine.RegisterGraphicElement(this);
        }

        public override void Draw(Graphics g)
        {
            if (!IsRefrence)
            {
                g.DrawImage(SpriteImg, Position.x, Position.y, Scale.x, Scale.y);
            }
        }
    }
}
