using System;

namespace Frame
{
    public interface IEntitySystem
    {
        World World { get; set; }
        void Tick();
    }
}
