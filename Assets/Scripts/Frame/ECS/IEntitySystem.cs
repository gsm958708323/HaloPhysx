using System;

namespace Frame
{
    public abstract class IEntitySystem
    {
        public World World { get; set; }
        public virtual void Tick() { }
    }
}
