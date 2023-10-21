using System;

namespace Frame
{
    public abstract class IEntitySystem : ILogic
    {
        public World World { get; set; }

        public virtual void Tick() { }
        public virtual void Init() { }
        public virtual void Enter() { }
        public virtual void Exit() { }
    }
}
