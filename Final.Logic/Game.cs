using System;
using glfw3;
using GlmSharp;

namespace Final.Logic
{
    public class Game : IGame
    {
        OpenGLRenderer renderer = new OpenGLRenderer(800, 600);
        public void Run()
        {
            Entity entity =  new Entity();

            while (Glfw.WindowShouldClose(renderer.Window) < 1)
            {
                renderer.Draw();
            }
        }
    }
}