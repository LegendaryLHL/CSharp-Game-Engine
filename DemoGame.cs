using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace GameEngine
{
    class DemoGame : GameEngine
    { 
        Sprite Player;
        PhysicObject PlayerObject;

        bool left;
        bool right;
        bool up;
        bool down;

        Vector2 LastPos = new Vector2(0, 0);

        // Create two dimentional array to place sprite
        string[,] Map =
        {
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "." },
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "." },
            {".", ".", "p", ".", ".", ".", ".", ".", ".", ".", ".", "." },
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "." },
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "." },
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "." },
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "." },
            {".", "g", "g", "g", "g", "g", ".", ".", ".", ".", ".", "." },
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "." },
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "." },
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "." },
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "." },
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "." }

        };

        public DemoGame() : base(new Vector2(615, 512), ("demo")) { }

        public override void OnLoad()
        {
            BackgroundColour = Color.White;

            CameraPositon.x = 120;

            for (int i = 0; i < Map.GetLength(1); i++)
            {
                for (int j = 0; j < Map.GetLength(0); j++)
                {
                    if (Map[j, i] == "g")
                    {
                        new PhysicObject(new Sprite(new Vector2(i * 50, j * 50), new Vector2(50, 50), "ground", "ground"))
                        {
                            Mass = 3000000
                        };
                    }
                    if (Map[j, i] == "p")
                    {
                        Player = new Sprite(new Vector2(i * 50, j * 50), new Vector2(50, 50), "Player", "player");
                    }
                }
            }
            new Button(new Vector2(50, 50), new Vector2(50, 50), "test", new Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Pixel), Color.Red, () => { Console.WriteLine("click"); }, "tag");

            PlayerObject = new PhysicObject(Player);
            PlayerObject.Mass = 100;
            PlayerObject.Update = () =>
            {
                PlayerObject.ApplyGravity();
            };
        }
        public override void OnDraw()
        {

        }

        public override void OnUpdate()
        {
            if (up)
            {
                PlayerObject.Velocity.y = 50f;
            }
            if (down)
            {
                PlayerObject.Velocity.y = -50f;
            }
            if (left)
            {
                PlayerObject.Velocity.x = 50f;
            }
            if (right)
            {
                PlayerObject.Velocity.x = -50f;
            }

            PhysicObject.PhysicUpdate();
        }

        public override void GetKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    up = true;
                    break;
                case Keys.A:
                    left = true;
                    break;
                case Keys.S:
                    down = true;
                    break;
                case Keys.D:
                    right = true;
                    break;
            }
        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    up = false;
                    break;
                case Keys.A:
                    left = false;
                    break;
                case Keys.S:
                    down = false;
                    break;
                case Keys.D:
                    right = false;
                    break;
            }
        }

        public override void OnInitialise()
        {
        }

        public override void GetKeyPress(KeyPressEventArgs e)
        {
        }

        public override void GetMouseDown(MouseEventArgs e)
        {
        }

        public override void GetMouseUp(MouseEventArgs e)
        {
            // Handle button
            if (e.Button == MouseButtons.Left)
            {
                foreach (GraphicElement graphicElement in AllGraphicElements)
                {
                    if (graphicElement is Button && graphicElement.IsCursorOnGraphicElement())
                    {
                        Button button = (Button)graphicElement;
                        button.RunAction();
                        break;
                    }
                }
            }
        }

        public override void GetMouseHover(EventArgs e)
        {
        }

        public override void GetMouseMove(EventArgs e)
        {
            // Handle button
            foreach (GraphicElement graphicElement in AllGraphicElements)
            {
                // Not Hover
                if (graphicElement is Button)
                {
                    Button button = (Button)graphicElement;
                    if (graphicElement.IsCursorOnGraphicElement() && !button.IsHover)
                    {
                        button.IsHover = true;
                        break;
                    }
                    else if (!graphicElement.IsCursorOnGraphicElement() && button.IsHover)
                    {
                        button.IsHover = false;
                        break;
                    }
                }
            }
        }
    }
}
