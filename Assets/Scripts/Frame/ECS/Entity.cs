using System;
using Unity.VisualScripting;

namespace Frame
{
    public class Entity
    {
        public static ObjectPool ObjectPool = new ObjectPool(
            () => new Entity(),
            (entity) =>
            {
                Entity e = (Entity)entity;
                e.LifeState = 1;
            },
            (entity) =>
            {
                Entity e = (Entity)entity;
                e.LifeState = 0;
                e.Id = Guid.Empty;
            }
        );
        public World World { get; set; }
        public Guid Id { get; internal set; }
        public byte LifeState = 1;

        public Entity(Guid id)
        {
            Id = id;
        }
        public Entity()
        {

        }

        public bool ExistComponent(IComponent comp)
        {
            if (LifeState == 0)
                return false;
            return World.GetComponent(Id, comp.GetType()) != null;
        }

        public Entity AddComponent(IComponent comp)
        {
            if (!ExistComponent(comp))
            {
                comp.EntityId = Id;
                World.AddComponent(comp);
            }
            return this;
        }

        public T GetComponent<T>() where T : IComponent
        {
            if (LifeState == 0)
                return default(T);
            return (T)World.GetComponent(Id, typeof(T));
        }

        public void RemoveComponent(IComponent comp)
        {
            if (ExistComponent(comp))
                return;
            
            World.RemoveComponent(comp);
            comp.EntityId = Guid.Empty;
        }
    }
}
