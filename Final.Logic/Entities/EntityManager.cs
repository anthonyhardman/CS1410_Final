using System;
using System.Collections.Generic;

namespace Final.Logic
{
    public class EntityManager
    {
        public List<Entity> Entities = new List<Entity>();

        public IEnumerable<RenderComponent> GetRenderComponents()
        {
            return Entity.ComponentManager_.RenderComponents.Values;
        }

        public IEnumerable<string> GetEntityNames()
        {
            List<string> names = new List<string>();

            foreach (Entity entity in Entities)
            {
                names.Add(entity.Name);
            }

            return names;
        }

        public IEnumerable<Entity> GetEntities()
        {
            return Entities;
        }
    }
}