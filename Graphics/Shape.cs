﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Shape : GraphicElement
    {
        public Color Color;
        public Shape(Vector2 Position, Vector2 Scale, Color color, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Tag = Tag;

            GameEngine.RegisterGraphicElement(this);
        }


        public override void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color), Position.x, Position.y, Scale.x, Scale.y);
        }
    }
}
