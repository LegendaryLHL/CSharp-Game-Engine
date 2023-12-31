﻿using System.Drawing;
using System.Windows.Forms;

namespace GameEngine
{
    public abstract class GraphicElement
    {
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public string Tag = "";

        public void DestroySelf()
        {
            GameEngine.UnRegisterGraphicElement(this);
        }

        public static bool IsCursorOnGraphicElement(string tag)
        {
            foreach (GraphicElement e in GameEngine.AllGraphicElements)
            {
                if (e.Tag == tag)
                {
                    if (Cursor.Position.X < e.Position.x + e.Scale.x &&
                        Cursor.Position.Y < e.Position.y + e.Scale.y &&
                        Cursor.Position.X > e.Position.x &&
                        Cursor.Position.Y > e.Position.y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool IsCursorOnGraphicElement()
        {
            return Cursor.Position.X < Position.x + Scale.x &&
                        Cursor.Position.Y - 28 < Position.y + Scale.y &&
                        Cursor.Position.X > Position.x &&
                        Cursor.Position.Y - 28 > Position.y;
        }

        // tag is other element
        public bool IsColiding(string tag)
        {
            foreach (GraphicElement b in GameEngine.AllGraphicElements)
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

        public abstract void Draw(Graphics g);
    }
}
