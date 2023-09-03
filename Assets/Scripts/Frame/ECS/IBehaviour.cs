using System;

namespace Frame
{
    public abstract class IBehaviour : ILogic
    {
        public Simulation Simulation { get; set; }
        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void Init()
        {
        }

        public virtual void Tick()
        {
        }
    }
}
