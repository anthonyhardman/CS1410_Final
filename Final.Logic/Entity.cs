using System;

namespace Final.Logic
{
    public class Entity 
    {
        private static uint NumberOfEntities;
        public static ComponentManager ComponentManager_;
        public static EntityManager EntityManager_;
        public uint ID {get; init;}
        public EntityState State = EntityState.Enabled;
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

        }

        public void AddComponent<T>() where T : Component
        {
            ComponentManager_.AddComponent<T>(ID);
        }

        public T GetComponent<T>() where T : Component
        {
            return ComponentManager_.GetComponent<T>(ID);
        }
    }
}
