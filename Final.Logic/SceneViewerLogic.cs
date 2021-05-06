using System;
using System.Collections.Generic;

namespace Final.Logic
{
    public class SceneViewerLogic
    {
        private readonly EntityManager EntityManager_;
        public List<string> EntityTypes = new List<string>()
        {
            "Entity", "Cube", "LightCube", "Tree Type 1", "Tree Type 2", "Tree Type 3",
            "Terrain", "Camera"
        };
        public List<string> ComponentTypes = new List<string>()
        {
            "Transform", "Render", "Material", "Light", "Shader", "Camera"
        };

        public List<IImguiWidget> ComponentWidgets;
        public IEnumerable<RenderComponent> RenderComponents;

        public SceneViewerLogic(EntityManager entityManager)
        {
            EntityManager_ = entityManager;
        }

        public IEnumerable<RenderComponent> GetRenderComponents()
        {
            return EntityManager_.GetRenderComponents();
        }

        public IEnumerable<string> GetEntityNames()
        {
            return EntityManager_.GetEntityNames();
        }

        public IEnumerable<Entity> GetEntities()
        {
            return EntityManager_.GetEntities();
        }

        public void AddEntity(int entityTypeIndex)
        {
            switch (EntityTypes[entityTypeIndex])
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
                case "Tree Type 1":
                    Tree tree1 = new Tree(1);
                    break;
                case "Tree Type 2":
                    Tree tree2 = new Tree(2);
                    break;
                case "Tree Type 3":
                    Tree tree3 = new Tree(3);
                    break;
                case "Terrain":
                    Terrain terrain = new Terrain();
                    break;
                case "Camera":
                    Camera camera = new Camera();
                    break;
            }
        }

        public string AddComponent(int ComponentTypeIndex, Entity entity)
        {
            string ErrorText = "";

            switch (ComponentTypes[ComponentTypeIndex])
            {
                case "Transform":
                    if (!entity.Components.Contains(typeof(TransformComponent)))
                    {
                        entity.AddComponent<TransformComponent>();
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
                        if (!entity.Components.Contains(typeof(RenderComponent)))
                        {
                            entity.AddComponent<RenderComponent>();
                            entity.GetComponent<RenderComponent>().Model_
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
                        if (!entity.Components.Contains(typeof(MaterialComponent)))
                        {
                            entity.AddComponent<MaterialComponent>();
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
                        if (!entity.Components.Contains(typeof(LightComponent)))
                        {
                            entity.AddComponent<LightComponent>();
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
                        if (!entity.Components.Contains(typeof(ShaderComponent)))
                        {
                            entity.AddComponent<ShaderComponent>();
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

                case "Camera":
                    try
                    {
                        if (!entity.Components.Contains(typeof(CameraComponent)))
                        {
                            entity.AddComponent<CameraComponent>();
                            ErrorText = "";
                        }
                        else
                        {
                            ErrorText = "Entity already has a camera component";
                        }
                    }
                    catch
                    {
                        ErrorText = "Must have a transform component to add a camera!";
                    }
                    break;
            }

            return ErrorText;
        }

        public CameraComponent GetActiveCameraComponent()
        {
            return Entity.ComponentManager_.ActiveCamera;
        }

        public List<IImguiWidget> GetComponentWidgets(Entity entity)
        {
            return entity.GetWidgets();
        }
    }
}