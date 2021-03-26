using System;

namespace Final.Logic
{
    public class Entity 
    {
        private static uint NumberOfEntities;
        public static ComponentManager ComponentManager_;
        public uint ID {get; init;}
        
        static Entity()
        {
            NumberOfEntities = 0;
            ComponentManager_ = new ComponentManager();
        }

        public Entity()
        {
            ID = NumberOfEntities++;
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
