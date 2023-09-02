using System;
using PEMath;

namespace Physx
{
    public class BoxConfig : ColliderConfigBase
    {
        /// <summary>
        /// 轴向，对应xyz轴
        /// </summary>
        /// <value></value>
        public PEVector3[] Axis { get; internal set; }
        public BoxConfig()
        {
            Type = ColliderType.Box;
        }
    }
}
