using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Text
    {
        public Vector2 Position = null;
        public string TextString;
        public Font Font;

        public Text(string TextString, Font Font, Vector2 Position)
        {
            this.TextString = TextString;
            this.Font = Font;
            this.Position = Position;

            GameEngine.RegisterText(this);
        }
        public void DestroySelf()
        {
            GameEngine.UnRegisterText(this);
        }
    }
}
