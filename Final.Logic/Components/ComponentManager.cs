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
            {typeof(MaterialComponent), 4}
        };

        public Dictionary<uint, TransformComponent> TransformComponents = new Dictionary<uint, TransformComponent>();
        public Dictionary<uint, RenderComponent> RenderComponents = new Dictionary<uint, RenderComponent>();
        public Dictionary<uint, LightComponent> LightComponents = new Dictionary<uint, LightComponent>();
        public Dictionary<uint, ShaderComponent> ShaderComponents = new Dictionary<uint, ShaderComponent>();
        public Dictionary<uint, MaterialComponent> MaterialComponents = new Dictionary<uint, MaterialComponent>();
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
                TransformComponents.Add(entityID, new TransformComponent());
                break;

                case 1:
                RenderComponents.Add(entityID, new RenderComponent(GetComponent<TransformComponent>(entityID)));
                break;

                case 2:
                LightComponents.Add(entityID, new LightComponent(GetComponent<TransformComponent>(entityID)));
                break;

                case 3:
                ShaderComponents.Add(entityID, new ShaderComponent(GetComponent<RenderComponent>(entityID)));
                break;

                case 4:
                MaterialComponents.Add(entityID, new MaterialComponent(GetComponent<RenderComponent>(entityID)));
                break;
            }
        }


        public List<IImguiWidget> GetWidgets(uint entityID)
        {
            return Widgets[entityID];
        }

        public void AddWidget(uint entityID, IImguiWidget widget)
        {
            
        }
    }
}