using System;

namespace Frame
{
    public interface IComponent
    {
        Guid EntityId { get; set; }
        IComponent Clone();
        int GetCommand();
    }
}
