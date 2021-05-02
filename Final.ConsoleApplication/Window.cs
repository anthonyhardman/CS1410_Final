using System;
using System.Numerics;
using System.Collections.Generic;
using GlmSharp;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ImGuiNET;
using Dear_ImGui_Sample;
using System.IO;
using System.Linq;

namespace Final.Logic
{
    public class Window : GameWindow
    {
        private readonly SceneViewerLogic _SceneViewerLogic;
        private int EntityIndex = 0;
        private int EntityTypeIndex = 0;
        private int ComponentTypeIndex = 0;
        private mat4 View;
        private mat4 projection;
        private Camera Camera_ = new Camera();

        private IEnumerable<RenderComponent> RenderComponents;

        private ImGuiController imGuiController;

        private List<IImguiWidget> Widgets = new List<IImguiWidget>();
        private string ErrorText = "";
        private float LastX;
        private float LastY;
        private bool CameraActive = false;

        private bool DrawLines = false;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, SceneViewerLogic sceneViewerLogic)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            LastX = MouseState.Position.X;
            LastY = MouseState.Position.Y;
            _SceneViewerLogic = sceneViewerLogic;

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
            if (DrawLines)
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            }
            else
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            }

            GL.Enable(EnableCap.DepthTest);

            RenderComponents = _SceneViewerLogic.GetRenderComponents();

            imGuiController.Update(this, (float)args.Time);

            ProcessInput(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            View = Camera_.GetViewMatrix();
            projection = mat4.Perspective(Camera_.Zoom, (float)Size.X / Size.Y, 0.1f, 200.0f);
            
            IEnumerable<string> EntityNames = _SceneViewerLogic.GetEntityNames();

            foreach (RenderComponent component in RenderComponents)
            {
                component.Draw(View, projection);
            }

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            ImGui.Begin("Scene");
            ImGui.ListBox("Entities", ref EntityIndex, EntityNames.ToArray(), Entity.EntityManager_.Entities.Count, 20);
            if (ImGui.BeginCombo("", _SceneViewerLogic.EntityTypes[EntityTypeIndex]))
            {
                for (int i = 0; i <  _SceneViewerLogic.EntityTypes.Count; ++i)
                {
                    if (ImGui.Selectable( _SceneViewerLogic.EntityTypes[i]))
                    {
                        EntityTypeIndex = i;
                    }
                }
                ImGui.EndCombo();
            }

            ImGui.SameLine();
            if (ImGui.Button("Add Entity"))
            {
                _SceneViewerLogic.AddEntity(EntityTypeIndex);
            }
            ImGui.Checkbox("Draw Lines", ref DrawLines);
            if (ImGui.CollapsingHeader("Help!"))
            {
                string helpText = File.ReadAllText("TextFiles\\help.txt");
                ImGui.TextWrapped(helpText);
            }
            ImGui.End();

            Entity selectedEntity = (from e in _SceneViewerLogic.GetEntities()
                         where (e.Name == EntityNames.ToArray()[EntityIndex])
                         select e).First();

            Widgets = _SceneViewerLogic.GetComponentWidgets(selectedEntity);

            ImGui.Begin("Components");
            foreach (IImguiWidget widget in Widgets)
            {
                widget.Run();
            }
            ImGui.Separator();
            ImGui.PushID("Combo");
            if (ImGui.BeginCombo("", _SceneViewerLogic.ComponentTypes[ComponentTypeIndex]))
            {
                for (int i = 0; i < _SceneViewerLogic.ComponentTypes.Count; ++i)
                {
                    if (ImGui.Selectable(_SceneViewerLogic.ComponentTypes[i]))
                    {
                        ComponentTypeIndex = i;
                    }
                }
                ImGui.EndCombo();
            }
            ImGui.SameLine();
            if (ImGui.Button("Add Component"))
            {
               ErrorText = _SceneViewerLogic.AddComponent(ComponentTypeIndex, selectedEntity);
            }
            ImGui.TextColored(new Vector4(1.0f, 0.0f, 0.0f, 1.0f), ErrorText);
            ImGui.End();

            imGuiController.Render();

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
            if (KeyboardState.IsKeyPressed(Keys.F1))
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