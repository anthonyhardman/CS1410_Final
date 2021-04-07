using System;
using glfw3;
using GlmSharp;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace Final.Logic
{
    public class Game : IGame
    {
        public void Run()
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(1920, 1080),
                Title = "Hello!"
            };

            Window window = new Window(GameWindowSettings.Default, nativeWindowSettings);
            window.VSync = OpenTK.Windowing.Common.VSyncMode.Off;

            Entity.EntityManager_.Window = window;

            Cube cube = new Cube();
            Entity entity = new Entity();
            LightCube lightCube = new LightCube();
            LightCube lightCube1 = new LightCube();
            
            window.Run();
        }
    }
}