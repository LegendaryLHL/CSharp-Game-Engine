﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GameEngine
{
    class Canvas : Form
    {
        public Canvas()
        {
            // Reduce flickering
            this.DoubleBuffered = true;
        }
    }
    public abstract class GameEngine
    {
        private Vector2 ScreenSize = new Vector2(512, 512);
        private string Title = "MyGame";
        private Canvas Window = null;
        private Thread GameLoopThread = null;

        public static List<GraphicElement> AllGraphicElements = new List<GraphicElement> { };


        public static Vector2 CursorPosition;
        public static Color BackgroundColour = Color.White;
        public static Vector2 CameraPositon = new Vector2(0, 0);
        public static float CameraAngle = 0f;
        public static Vector2 CameraZoom = new Vector2(1, 1);
        public static FormWindowState WindowStateSize = FormWindowState.Normal;


        public static float DeltaTime = 0;
        public static DateTime TimeNow = DateTime.Now;

        public GameEngine(Vector2 screenSize, string title)
        {
            OnInitialise();
            ScreenSize = screenSize;
            Title = title;
            Window = new Canvas();
            Window.Size = new Size((int)ScreenSize.x, (int)ScreenSize.y);
            Window.Text = Title;
            Window.Paint += Renderer;
            Window.KeyDown += Window_KeyDown;
            Window.KeyUp += Window_KeyUP;
            Window.KeyPress += Window_KeyPress;
            Window.MouseDown += Window_MouseDown;
            Window.MouseUp += Window_MouseUp;
            Window.MouseHover += Window_MouseHover;
            Window.MouseMove += Window_MouseMove;
            Window.FormClosing += Window_FormClosing;
            Window.WindowState = WindowStateSize;
            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            Application.Run(Window);
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            // Offset if needed
            CursorPosition = new Vector2(Window.PointToClient(Cursor.Position).X - CameraPositon.x, Window.PointToClient(Cursor.Position).Y - CameraPositon.y);
            GetMouseMove(e);
        }

        private void Window_MouseHover(object sender, EventArgs e)
        {
            GetMouseHover(e);
        }

        private void Window_MouseUp(object sender, MouseEventArgs e)
        {
            GetMouseUp(e);
        }

        private void Window_MouseDown(object sender, MouseEventArgs e)
        {
            GetMouseDown(e);
        }

        private void Window_KeyPress(object sender, KeyPressEventArgs e)
        {
            GetKeyPress(e);
        }

        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            GameLoopThread.Abort();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            GetKeyDown(e);
        }
        private void Window_KeyUP(object sender, KeyEventArgs e)
        {
            GetKeyUp(e);
        }

        public static void RegisterGraphicElement(GraphicElement graphicElements)
        {
            AllGraphicElements.Add(graphicElements);
        }
        public static void UnRegisterGraphicElement(GraphicElement graphicElements)
        {
            AllGraphicElements.Remove(graphicElements);
        }

        void GameLoop()
        {
            OnLoad();

            while (GameLoopThread.IsAlive)
            {
                /*try
                {
                    OnDraw();
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    OnUpdate();
                    Thread.Sleep(2);
                }
                catch (Exception ex)
                {
                    //log info game is loading
                    Log.Error("An unexpected error occurred while loading: " + ex.Message);
                }
                */
                OnDraw();
                Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });

                DeltaTime = (float)(DateTime.Now - TimeNow).TotalSeconds;
                TimeNow = DateTime.Now;
                OnUpdate();

                Thread.Sleep(2);
            }
        }

            private void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(BackgroundColour);
            g.TranslateTransform(CameraPositon.x, CameraPositon.y);
            g.RotateTransform(CameraAngle);
            g.ScaleTransform(CameraZoom.x, CameraZoom.y);
            foreach (GraphicElement graphicElement in AllGraphicElements)    
            {
                graphicElement.Draw(g);
            }
        }

        public abstract void OnInitialise();
        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void OnDraw();
        public abstract void GetKeyDown(KeyEventArgs e);
        public abstract void GetKeyUp(KeyEventArgs e);
        public abstract void GetKeyPress(KeyPressEventArgs e);
        public abstract void GetMouseDown(MouseEventArgs e);
        public abstract void GetMouseUp(MouseEventArgs e);
        public abstract void GetMouseHover(EventArgs e);
        public abstract void GetMouseMove(EventArgs e);

    }
}
