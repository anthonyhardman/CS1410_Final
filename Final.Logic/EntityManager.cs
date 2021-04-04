using System;
using System.Collections.Generic;

namespace Final.Logic
{
    public class EntityManager
    {
        public Window Window;
        public List<Entity> Entities = new List<Entity>();

        public void Update()
        {
            foreach (Entity entity in Entities)
            {
                bool hasRenderComponent = false;

                foreach (Type t in entity.Components)
                {
                    if (t == typeof(RenderComponent))
                    {
                        hasRenderComponent = true;
                    }
                }
                
                if (entity.State == EntityState.Enabled && hasRenderComponent)
                {
                    try
                    {
                        Window.renderComponents.Add(Entity.ComponentManager_.GetComponent<RenderComponent>(entity.ID));
                    }
                    catch (KeyNotFoundException)
                    {

                    }  
                }
            }
        }
    }
}