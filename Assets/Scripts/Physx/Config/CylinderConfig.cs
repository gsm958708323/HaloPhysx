using System;
using PEMath;

namespace Physx
{
    public class CylinderConfig : ColliderConfigBase
    {
        /// <summary>
        /// 半径
        /// </summary>
        public PEInt Radius { get; internal set; }

        public CylinderConfig()
        {
            Type = ColliderType.Cylinder;
        }
    }
}
