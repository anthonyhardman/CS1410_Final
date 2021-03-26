using System;
using System.Collections.Generic;

namespace Final.Logic
{
    public class ComponentManager
    {
        private Dictionary<Type, uint> ComponentTypes = new Dictionary<Type, uint>()
        {
            {typeof(TransformComponent), 0},
            {typeof(RenderComponent), 1}
        };

        public Dictionary<uint, TransformComponent> TransformComponents = new Dictionary<uint, TransformComponent>();
        public Dictionary<uint, RenderComponent> RenderComponents = new Dictionary<uint, RenderComponent>();

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
            }
        }
    }
}