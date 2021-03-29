using System.Collections.Generic;

namespace Final.Logic
{
    public class EntityManager
    {
        public IRenderer Renderer;
        public List<Entity> Entities = new List<Entity>();

        public void Update()
        {
            foreach (Entity entity in Entities)
            {
                if (entity.State == EntityState.Enabled)
                {
                    try
                    {
                        Renderer.RenderComponents.Add(Entity.ComponentManager_.GetComponent<RenderComponent>(entity.ID));
                    }
                    catch (KeyNotFoundException)
                    {

                    }  
                }
            }
        }
    }
}