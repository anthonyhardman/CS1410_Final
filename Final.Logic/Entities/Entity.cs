using System;
using System.Collections.Generic;

namespace Final.Logic
{
    public class Entity 
    {
        private static uint NumberOfEntities;
        public static ComponentManager ComponentManager_;
        public static EntityManager EntityManager_;
        public uint ID {get; init;}
        public string Name {get; set;}
        public EntityState State = EntityState.Enabled;
        public List<Type> Components =  new List<Type>();
        static Entity()
        {
            NumberOfEntities = 0;
            ComponentManager_ = new ComponentManager();
            EntityManager_ = new EntityManager();
        }

        public Entity()
        {
            ID = NumberOfEntities++;
            EntityManager_.Entities.Add(this);
            Name = $"Entity{ID}";
        }

        public void AddComponent<T>() where T : Component
        {
            ComponentManager_.AddComponent<T>(ID);
            Components.Add(typeof(T));
        }

        public T GetComponent<T>() where T : Component
        {
            return ComponentManager_.GetComponent<T>(ID);
        }

        public List<IImguiWidget> GetWidgets()
        {
            return ComponentManager_.GetWidgets(ID);
        }
    }
}
