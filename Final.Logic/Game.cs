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
            Entity.EntityManager_.Renderer = renderer;
            //GuiWindow gui = new GuiWindow(new vec3(0.25f, 1.0f, 1.0f), GuiAlignment.LEFT, null);

            while (Glfw.WindowShouldClose(renderer.Window) < 1)
            {
                Entity.EntityManager_.Update();
                renderer.Draw();
            }
        }
    }
}