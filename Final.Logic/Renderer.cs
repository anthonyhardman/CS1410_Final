using System;
using glfw3;
using GlmSharp;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Final.Logic
{
    public class OpenGLRenderer
    {
        public GLFWwindow Window;
        public static int SCR_Width;
        public static int SCR_Height;
        private mat4 View;
        private mat4 Projection;
        private GLFWframebuffersizefun FrameBufferSizeCallbackDelegate = FramebufferSizeCallback;

        public OpenGLRenderer(int scr_width, int scr_height)
        {
            SCR_Width = scr_width;
            SCR_Height = scr_height;

            Setup();
        }

        public void Setup()
        {
            Glfw.WindowHint((int)State.ContextVersionMajor, 4);
            Glfw.WindowHint((int)State.ContextVersionMinor, 6);
            Glfw.WindowHint((int)State.OpenglProfile, (int)State.OpenglCoreProfile);

            Window = Glfw.CreateWindow(SCR_Width, SCR_Height, "FinalProject", null, null);

            Glfw.SetFramebufferSizeCallback(Window, FrameBufferSizeCallbackDelegate);
            Glfw.MakeContextCurrent(Window);
            GL.LoadBindings(new GLFWBindingsContext());
            GL.Enable(EnableCap.DepthTest);
        }

        public void Draw()
        {
            GL.ClearColor(0.5f, 0.25f, 0.25f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            View = mat4.Translate(0.0f, 0.0f, -5.0f);
            Projection = mat4.Perspective(45.0f, (float)SCR_Width / SCR_Height, 0.1f, 200.0f);

            Glfw.SwapBuffers(Window);
            Glfw.PollEvents();
        }

        public static void FramebufferSizeCallback(IntPtr window, int width, int height)
        {
            GL.Viewport(0, 0, width, height);

            SCR_Width = width;
            SCR_Height = height;
        }
    }
}