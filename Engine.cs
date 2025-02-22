using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Drawing;
using System.Windows;
using Application = System.Windows.Forms.Application;
using System.Windows.Input;

namespace PhysicsEngine
{
    public class Canvas : Form
    {
        public Canvas()
        {
            this.DoubleBuffered = true;
        }
    }
    public abstract class Engine
    {
        public static Vector ScreenSize = new Vector(500, 500);
        public string Title = "";
        public static Canvas window = null;
        private Thread GameLoopThread = null;
        public static List<Shape> RenderStack = new List<Shape>();
        private static Line[] Boundary = new Line[4];
        public Engine(Vector vector, string Title)
        {
            ScreenSize = vector;
            this.Title = Title;
            window = new Canvas();
            window.Size = new System.Drawing.Size((int)ScreenSize.X, (int)ScreenSize.Y);
            window.Text = Title;
            CreateBoundary();
            window.Paint += Renderer;
            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.SetApartmentState(ApartmentState.STA);
            GameLoopThread.Start();
            Application.Run(window);
        }
        public static void RegisterShape(Shape shape)
        {
            if(shape != null)
            {
                RenderStack.Add(shape);
            }
        }
        public abstract void OnLoad();
        public abstract void OnUpdate();
        public static List<Shape> GetShapes()
        {
            return RenderStack.ToList();
        }
        private void Renderer(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            List<Shape> Render = new List<Shape>(RenderStack);
            DrawBoundary(e.Graphics);
            foreach(Shape s in Render)
            {
                s.ApplyForce(new Vector(0,0));
                s.Update(0.001f);
                s.ResolveScreenCollision(ScreenSize.X,ScreenSize.Y);
                foreach(Shape s2 in Render)
                {
                    if(s != s2)
                    {
                        s.ResolveCollision(s2);
                    }
                }
                s.Fill(e.Graphics); 
            }
        }
        private void GameLoop()
        {
            OnLoad();
            while (true)
            {
                try
                {
                    window.BeginInvoke((MethodInvoker)delegate { window.Refresh(); });
                    OnUpdate();
                    Thread.Sleep(10);
                }
                catch (Exception ex) 
                {

                }
            }
        }
        private void DrawBoundary(Graphics graphics)
        {
            for (int i = 0; i < Boundary.Length; i++)
            {
                Boundary[i].Draw(graphics);
            }
        }
        private void CreateBoundary()
        {
            Boundary[0] = new Line(new Vector(0, 0), new Vector(0, ScreenSize.Y), Color.Black);
            Boundary[1] = new Line(new Vector(0, 0), new Vector(ScreenSize.X, 0), Color.Black);
            Boundary[2] = new Line(new Vector(ScreenSize.X, 0), new Vector(ScreenSize.X, ScreenSize.X), Color.Black);
            Boundary[3] = new Line(new Vector(0, ScreenSize.Y), new Vector(ScreenSize.X, ScreenSize.Y), Color.Black);
        }
    }
}