using System;
using System.Collections.Generic;

namespace Final.Logic
{
    public class ComponentManager
    {
        private Dictionary<Type, uint> ComponentTypes = new Dictionary<Type, uint>()
        {
            {typeof(TransformComponent), 0},
            {typeof(RenderComponent), 1},
            {typeof(LightComponent), 2},
            {typeof(ShaderComponent), 3},
            {typeof(MaterialComponent), 4},
            {typeof(CameraComponent), 5}
        };

        public Dictionary<uint, TransformComponent> TransformComponents = new Dictionary<uint, TransformComponent>();
        public Dictionary<uint, RenderComponent> RenderComponents = new Dictionary<uint, RenderComponent>();
        public Dictionary<uint, LightComponent> LightComponents = new Dictionary<uint, LightComponent>();
        public Dictionary<uint, ShaderComponent> ShaderComponents = new Dictionary<uint, ShaderComponent>();
        public Dictionary<uint, MaterialComponent> MaterialComponents = new Dictionary<uint, MaterialComponent>();
        public Dictionary<uint, CameraComponent> CameraComponents = new Dictionary<uint, CameraComponent>();
        public Dictionary<uint, List<IImguiWidget>> Widgets = new Dictionary<uint, List<IImguiWidget>>();

        public T GetComponent<T>(uint entityID) where T : Component
        {
            T component = null;

            switch(ComponentTypes[typeof(T)])
            {
                case 0:
                try 
                {
                    component = TransformComponents[entityID] as T;
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine($"Entity {entityID} does not have a transform component!");
                    throw;
                }
                break;
                
                case 1:
                try
                {
                    component = RenderComponents[entityID] as T;
                }
                catch
                {
                    Console.WriteLine($"Entity {entityID} does not have a render component!");
                    throw;
                }
                break;

                case 2:
                try
                {
                    component = LightComponents[entityID] as T;
                }
                catch
                {
                    Console.WriteLine($"Entity {entityID} does not have a light component!");
                    throw;
                }
                break;

                case 3:
                try
                {
                    component = ShaderComponents[entityID] as T;
                }
                catch
                {
                    Console.WriteLine($"Entity {entityID} does not have a shader component!");
                    throw;
                }
                break;

                case 4:
                try
                {
                    component = MaterialComponents[entityID] as T;
                }
                catch
                {
                    Console.WriteLine($"Entity {entityID} does not have a material component!");
                    throw;
                }
                break;
            }

            return component;
        }

        public void AddComponent<T>(uint entityID) where T : Component
        {
            switch(ComponentTypes[typeof(T)])
            {
                case 0:
                TransformComponent transformComponent = new TransformComponent();
                TransformComponents.Add(entityID, transformComponent);
                AddWidget(entityID, new TransformWidget(transformComponent));
                break;

                case 1:
                RenderComponent renderComponent = new RenderComponent(GetComponent<TransformComponent>(entityID));
                RenderComponents.Add(entityID, renderComponent);
                AddWidget(entityID, new RenderWidget(renderComponent));
                break;

                case 2:
                LightComponent lightComponent = new LightComponent(GetComponent<TransformComponent>(entityID));
                LightComponents.Add(entityID, lightComponent);
                AddWidget(entityID, new LightWidget(lightComponent));
                break;

                case 3:
                ShaderComponent shaderComponent = new ShaderComponent(GetComponent<RenderComponent>(entityID));
                ShaderComponents.Add(entityID, shaderComponent);
                AddWidget(entityID, new ShaderWidget(shaderComponent));
                break;

                case 4:
                MaterialComponent materialComponent= new MaterialComponent(GetComponent<RenderComponent>(entityID));
                MaterialComponents.Add(entityID, materialComponent);
                AddWidget(entityID, new MaterialWidget(GetComponent<RenderComponent>(entityID), materialComponent));
                break;
            }
        }

        public List<IImguiWidget> GetWidgets(uint entityID)
        {
            if (!Widgets.ContainsKey(entityID))
            {
                return new List<IImguiWidget>();
            }

            return Widgets[entityID];
        }

        public void AddWidget(uint entityID, IImguiWidget widget)
        {
            if (Widgets.ContainsKey(entityID))
            {
                Widgets[entityID].Add(widget);
            }
            else
            {
                Widgets.Add(entityID, new List<IImguiWidget>(){
                    widget
                });
            }
        }
    }
}