using System;
using PEMath;

namespace Physx
{
    public class ColliderConfigBase
    {
        public string Name { get; internal set; }
        public PEVector3 Pos { get; internal set; }
        public ColliderType Type { get; internal set; }
    }


}
