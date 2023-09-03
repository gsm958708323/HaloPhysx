using System;
using System.Collections;
using System.Collections.Concurrent;

namespace Frame
{
    public class ObjectPool
    {
        Func<object> m_Constructor;
        Action<object> m_Init;
        Action<object> m_Reset;
        ConcurrentQueue<object> m_Queue;

        public ObjectPool(Func<object> constructor, Action<object> init, Action<object> reset)
        {
            if (constructor == null) throw new ArgumentNullException();
            if (init == null) throw new ArgumentNullException();
            if (reset == null) throw new ArgumentNullException();
            m_Constructor = constructor;
            m_Init = init;
            m_Reset = reset;
            m_Queue = new ConcurrentQueue<object>();
        }

        internal object Get()
        {
            object result;
            if (!m_Queue.TryDequeue(out result))
                result = m_Constructor();
            m_Init(result);
            return result;
        }

        internal void Recycle(object obj)
        {
            m_Reset(obj);
            m_Queue.Enqueue(obj);
        }
    }
}
