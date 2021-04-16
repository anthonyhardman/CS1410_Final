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

namespace Final.Logic
{
    public class Window : GameWindow
    {
        static int EntityIndex = 0;
        static int EntityTypeIndex = 0;
        static int ComponentTypeIndex = 0;
        private mat4 View;
        private mat4 projection;
        private Camera Camera_ = new Camera();
        public List<RenderComponent> renderComponents = new List<RenderComponent>();
        private ImGuiController imGuiController;
        private List<IImguiWidget> Widgets = new List<IImguiWidget>();
        private string ErrorText = "";

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
            List<string> EntityTypes = new List<string>()
            {
                "Entity", "Cube", "LightCube"
            };
            List<string> ComponentTypes = new List<string>()
            {
                "Transform", "Render", "Material", "Light", "Shader"
            };

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
            ImGui.ListBox("Entities", ref EntityIndex, EntityID.ToArray(), Entity.EntityManager_.Entities.Count, 10);
            if (ImGui.BeginCombo("", EntityTypes[EntityTypeIndex]))
            {
                for (int i = 0; i < EntityTypes.Count; ++i)
                {
                    if (ImGui.Selectable(EntityTypes[i]))
                    {
                        EntityTypeIndex = i;
                    }
                }
                ImGui.EndCombo();
            }

            ImGui.SameLine();
            if (ImGui.Button("Add Entity"))
            {
                switch (EntityTypes[EntityTypeIndex])
                {
                    case "Entity":
                        Entity entity = new Entity();
                        break;

                    case "Cube":
                        Cube cube = new Cube();
                        break;

                    case "LightCube":
                        LightCube lightCube = new LightCube();
                        break;
                }
            }
            if (ImGui.CollapsingHeader("Help!"))
            {
                string helpText = File.ReadAllText("TextFiles\\help.txt");
                ImGui.TextWrapped(helpText);
            }
            ImGui.End();

            foreach (Entity entity in Entity.EntityManager_.Entities)
            {
                if (entity.Name == EntityID[EntityIndex])
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
                        else if (t == typeof(ShaderComponent))
                        {
                            Widgets.Add(new ShaderWidget(entity.GetComponent<ShaderComponent>()));
                        }
                        else if (t == typeof(MaterialComponent))
                        {
                            Widgets.Add(new MaterialWidget(entity.GetComponent<RenderComponent>(), entity.GetComponent<MaterialComponent>()));
                        }
                    }
                }
            }

            ImGui.Begin("Components");
            foreach (IImguiWidget widget in Widgets)
            {
                widget.Run();
            }
            ImGui.Separator();
            ImGui.PushID("Combo");
            if (ImGui.BeginCombo("", ComponentTypes[ComponentTypeIndex]))
            {
                for (int i = 0; i < ComponentTypes.Count; ++i)
                {
                    if (ImGui.Selectable(ComponentTypes[i]))
                    {
                        ComponentTypeIndex = i;
                    }
                }
                ImGui.EndCombo();
            }
            ImGui.SameLine();
            if (ImGui.Button("Add Component"))
            {
                switch (ComponentTypes[ComponentTypeIndex])
                {
                    case "Transform":
                        if (!Entity.EntityManager_.Entities[EntityIndex].Components.Contains(typeof(TransformComponent)))
                        {
                            Entity.EntityManager_.Entities[EntityIndex].AddComponent<TransformComponent>();
                            ErrorText = "";
                        }
                        else
                        {
                            ErrorText = "Entity already has a transform component!";
                        }
                        break;

                    case "Render":
                        try
                        {
                            if (!Entity.EntityManager_.Entities[EntityIndex].Components.Contains(typeof(RenderComponent)))
                            {
                                Entity.EntityManager_.Entities[EntityIndex].AddComponent<RenderComponent>();
                                Entity.EntityManager_.Entities[EntityIndex].GetComponent<RenderComponent>().Model_
                                 = new Model("Models\\cube.dae", new Shader("Shaders\\basicVert.glsl", "Shaders\\basicFrag.glsl"));
                                ErrorText = "";
                            }
                            else
                            {
                                ErrorText = "Entity already has a render component!";
                            }
                        }
                        catch
                        {
                            ErrorText = "Entity Must have a transform component to add a render component";
                        }
                        break;

                    case "Material":
                        try
                        {
                            if (!Entity.EntityManager_.Entities[EntityIndex].Components.Contains(typeof(MaterialComponent)))
                            {
                                Entity.EntityManager_.Entities[EntityIndex].AddComponent<MaterialComponent>();
                                ErrorText = "";
                            }
                            else
                            {
                                ErrorText = "Entity already has a material component!";
                            }
                        }
                        catch
                        {
                            ErrorText = "Must have a render component to add a material!";
                        }
                        break;

                    case "Light":
                        try
                        {
                            if (!Entity.EntityManager_.Entities[EntityIndex].Components.Contains(typeof(LightComponent)))
                            {
                                Entity.EntityManager_.Entities[EntityIndex].AddComponent<LightComponent>();
                                ErrorText = "";
                            }
                            else
                            {
                                ErrorText = "Entity already has a light component!";
                            }
                        }
                        catch
                        {
                            ErrorText = "Must have a transform component to add a light!";
                        }
                        break;

                    case "Shader":
                        try
                        {
                            if (!Entity.EntityManager_.Entities[EntityIndex].Components.Contains(typeof(ShaderComponent)))
                            {
                                Entity.EntityManager_.Entities[EntityIndex].AddComponent<ShaderComponent>();
                                ErrorText = "";
                            }
                            else
                            {
                                ErrorText = "Entity already has a light component!";
                            }
                        }
                        catch
                        {
                            ErrorText = "Must have a render component to add a shader!";
                        }
                        break;
                }
            }
            ImGui.TextColored(new Vector4(1.0f, 0.0f, 0.0f, 1.0f), ErrorText);
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