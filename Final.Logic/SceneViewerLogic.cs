using System;
using System.Collections.Generic;

namespace Final.Logic
{
    public class SceneViewerLogic
    {
        private readonly IEntityService EntityService_;
        public List<string> EntityTypes = new List<string>()
        {
            "Entity", "Cube", "LightCube"
        };
        public List<string> ComponentTypes = new List<string>()
        {
            "Transform", "Render", "Material", "Light", "Shader"
        };

        public List<IImguiWidget> ComponentWidgets;
        public IEnumerable<RenderComponent> RenderComponents;

        public SceneViewerLogic(IEntityService entityService)
        {
            EntityService_ = entityService;
        }

        public IEnumerable<RenderComponent> GetRenderComponents()
        {
            return EntityService_.GetRenderComponents();
        }

        public IEnumerable<string> GetEntityNames()
        {
            return EntityService_.GetEntityNames();
        }

        public IEnumerable<Entity> GetEntities()
        {
            return EntityService_.GetEntities();
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
            }

            return ErrorText;
        }

        public List<IImguiWidget> GetComponentWidgets(Entity entity)
        {
            ComponentWidgets = new List<IImguiWidget>();

            foreach (Type t in entity.Components)
            {
                if (t == typeof(TransformComponent))
                {
                    ComponentWidgets.Add(new TransformWidget(entity.GetComponent<TransformComponent>()));
                }
                else if (t == typeof(RenderComponent))
                {
                    ComponentWidgets.Add(new RenderWidget(entity.GetComponent<RenderComponent>()));
                }
                else if (t == typeof(LightComponent))
                {
                    ComponentWidgets.Add(new LightWidget(entity.GetComponent<LightComponent>()));
                }
                else if (t == typeof(ShaderComponent))
                {
                    ComponentWidgets.Add(new ShaderWidget(entity.GetComponent<ShaderComponent>()));
                }
                else if (t == typeof(MaterialComponent))
                {
                    ComponentWidgets.Add(new MaterialWidget(entity.GetComponent<RenderComponent>(), entity.GetComponent<MaterialComponent>()));
                }
            }

            return ComponentWidgets;
        }
    }
}