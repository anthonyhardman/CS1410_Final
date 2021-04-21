using System;
using Final.Logic;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace Final.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(1920, 1080),
                Title = "Scene Viewer"
            };

            Window window = new Window(GameWindowSettings.Default, nativeWindowSettings, new SceneViewerLogic(Entity.EntityManager_));
            window.VSync = OpenTK.Windowing.Common.VSyncMode.Off;
            
            Cube cube = new Cube();
            Entity entity = new Entity();
            LightCube lightCube = new LightCube();
            LightCube lightCube1 = new LightCube();

            window.Run();
        }
    }
}
