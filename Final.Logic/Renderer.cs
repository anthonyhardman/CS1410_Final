using System;
using System.Collections.Generic;
using glfw3;
using GlmSharp;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ImGuiNET;

namespace Final.Logic
{
    public class OpenGLRenderer : IRenderer
    {
        public GLFWwindow Window;
        public int SCR_Width {get; set;}
        public int SCR_Height {get; set;}
        private mat4 View;
        private mat4 Projection;
        private GLFWframebuffersizefun FrameBufferSizeCallbackDelegate;
        public List<RenderComponent> RenderComponents {get; set;}

        public OpenGLRenderer(int scr_width, int scr_height)
        {
            SCR_Width = scr_width;
            SCR_Height = scr_height;

            RenderComponents = new List<RenderComponent>();

            Setup();
        }
        
        public void Setup()
        {
            Glfw.WindowHint((int)State.ContextVersionMajor, 4);
            Glfw.WindowHint((int)State.ContextVersionMinor, 6);
            Glfw.WindowHint((int)State.OpenglProfile, (int)State.OpenglCoreProfile);

            Window = Glfw.CreateWindow(SCR_Width, SCR_Height, "FinalProject", null, null);

            FrameBufferSizeCallbackDelegate = FramebufferSizeCallback;

            Glfw.SetFramebufferSizeCallback(Window, FrameBufferSizeCallbackDelegate);
            Glfw.MakeContextCurrent(Window);
        
            GL.LoadBindings(new GLFWBindingsContext());

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            GL.Enable(EnableCap.DepthTest);
        }

        public void Draw()
        {
            GL.ClearColor(0.5f, 0.25f, 0.25f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            View = mat4.Translate(0.0f, 0.0f, -5.0f);
            Projection = mat4.Perspective(45.0f, (float)SCR_Width / SCR_Height, 0.1f, 200.0f);

            foreach (RenderComponent renderComponent in RenderComponents)
            {
                renderComponent.Draw(View, Projection);
            }

            RenderComponents.Clear();

            Glfw.SwapBuffers(Window);
            Glfw.PollEvents();

        }

        public void FramebufferSizeCallback(IntPtr window, int width, int height)
        {
            GL.Viewport(0, 0, width, height);

            SCR_Width = width;
            SCR_Height = height;
        }
    }
}