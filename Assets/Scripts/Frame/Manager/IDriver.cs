using System;

namespace Frame
{
    public abstract class IDriver : ILogic
    {
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

        public virtual void Update(float deltaTime)
        {

        }
    }
}
