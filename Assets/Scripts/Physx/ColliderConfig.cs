using PEMath;

namespace Physx
{
    public class ColliderConfig
    {
        public string Name { get; internal set; }
        public PEVector3 Pos { get; internal set; }
        public ColliderType Type { get; internal set; }
        /// <summary>
        /// 半径
        /// </summary>
        public PEInt Radius { get; internal set; }

        /// <summary>
        /// 轴向
        /// </summary>
        /// <value></value>
        public PEVector3[] Axis { get; internal set; }
    }

    public enum ColliderType
    {
        Box,
        Cylinder,
        Capsule,
    }
}
