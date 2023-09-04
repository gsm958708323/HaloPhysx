using System;

namespace Frame
{
    public abstract class IComponent
    {
        public Guid EntityId { get; set; }
        public virtual IComponent Clone() { return null; }
        public virtual int GetCommand() { return 0; }
    }
}
