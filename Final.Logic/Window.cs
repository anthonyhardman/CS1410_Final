using System;
using System.Collections.Generic;
using GlmSharp;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ImGuiNET;
using Dear_ImGui_Sample;
using System.IO;

namespace Final.Logic
{
    public class Window : GameWindow
    {
        static int ListIndex = 0;
        private mat4 View;
        private mat4 projection;
        private Camera Camera_ = new Camera();
        public List<RenderComponent> renderComponents = new List<RenderComponent>();
        private ImGuiController imGuiController;
        private List<IImguiWidget> Widgets = new List<IImguiWidget>();

        float LastX;
        float LastY;
        bool CameraActive = false;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            LastX = MouseState.Position.X;
            LastY = MouseState.Position.Y;

            Mesh.Camera_ = Camera_;
        }

        protected override void OnLoad()
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            base.OnLoad();

            imGuiController = new ImGuiController(ClientSize.X, ClientSize.Y);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Enable(EnableCap.DepthTest);
            Entity.EntityManager_.Update();
            imGuiController.Update(this, (float)args.Time);
            ProcessInput(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            View = Camera_.GetViewMatrix();
            projection = mat4.Perspective(Camera_.Zoom, (float)Size.X / Size.Y, 0.1f, 200.0f);

            List<string> EntityID = new List<string>();


            foreach (Entity entity in Entity.EntityManager_.Entities)
            {
                EntityID.Add(entity.Name);
            }

            foreach (RenderComponent component in renderComponents)
            {
                component.Draw(View, projection);
            }

            renderComponents.Clear();

            ImGui.Begin("Scene");
            ImGui.ListBox("Entities", ref ListIndex, EntityID.ToArray(), Entity.EntityManager_.Entities.Count, 10);
            ImGui.Text(MouseState.Position.ToString());
            if (ImGui.CollapsingHeader("Help!"))
            {
                string helpText = File.ReadAllText("TextFiles\\help.txt");
                ImGui.TextWrapped(helpText);
            }
            ImGui.End();

            foreach (Entity entity in Entity.EntityManager_.Entities)
            {
                if (entity.Name == EntityID[ListIndex])
                {
                    foreach (Type t in entity.Components)
                    {
                        if (t == typeof(TransformComponent))
                        {
                            Widgets.Add(new TransformWidget(entity.GetComponent<TransformComponent>()));
                        }
                        else if (t == typeof(RenderComponent))
                        {
                            Widgets.Add(new RenderWidget(entity.GetComponent<RenderComponent>()));
                        }
                        else if (t == typeof(LightComponent))
                        {
                            Widgets.Add(new LightWidget(entity.GetComponent<LightComponent>()));
                        }
                    }
                }
            }



            ImGui.Begin("Components");
            foreach (IImguiWidget widget in Widgets)
            {
                widget.Run();
            }
            ImGui.End();

            imGuiController.Render();

            Widgets.Clear();

            SwapBuffers();

            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (KeyboardState.IsKeyPressed(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
            imGuiController.WindowResized(ClientSize.X, ClientSize.Y);
        }

        protected override void OnMaximized(MaximizedEventArgs e)
        {
            base.OnMaximized(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        public void ProcessInput(FrameEventArgs args)
        {
            if (KeyboardState.IsKeyDown(Keys.W))
            {
                Camera_.ProcessKeyboard(Camera_Movement.FORWARD, (float)args.Time);
            }
            if (KeyboardState.IsKeyDown(Keys.S))
            {
                Camera_.ProcessKeyboard(Camera_Movement.BACKWARD, (float)args.Time);
            }
            if (KeyboardState.IsKeyDown(Keys.A))
            {
                Camera_.ProcessKeyboard(Camera_Movement.RIGHT, (float)args.Time);
            }
            if (KeyboardState.IsKeyDown(Keys.D))
            {
                Camera_.ProcessKeyboard(Camera_Movement.LEFT, (float)args.Time);
            }
            if (KeyboardState.IsKeyPressed(Keys.T))
            {
                if (CameraActive)
                {
                    CameraActive = false;
                    CursorGrabbed = false;
                    CursorVisible = true;
                }
                else
                {
                    CameraActive = true;
                    CursorGrabbed = true;
                }
            }

            float xoffset = MouseState.Position.X - LastX;
            float yoffset = MouseState.Position.Y - LastY;
            LastX = MouseState.Position.X;
            LastY = MouseState.Position.Y;

            if (CameraActive)
            {
                if (xoffset != 0 && yoffset != 0)
                {
                    Camera_.ProcessMouseMovement(xoffset, yoffset);
                }

                Camera_.ProcessMouseScroll(MouseState.ScrollDelta.Y);
            }
        }
    }
}