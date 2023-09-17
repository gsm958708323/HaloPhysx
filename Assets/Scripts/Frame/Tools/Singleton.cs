using System;
using System.Reflection;

namespace Frame
{
    public abstract class Singleton<T> where T : Singleton<T>
    {
        protected static T instance = null;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    ConstructorInfo[] ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                    // 获取无参的构造函数
                    ConstructorInfo ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
                    if (ctor == null)
                    {
                        throw new Exception("C#单例类没有提供私有无参的构造函数");
                    }
                    else
                    {
                        instance = ctor.Invoke(null) as T;
                    }
                }
                return instance;
            }
        }
    }
}
