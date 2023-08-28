/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com
	日期: 2021/03/26 15:32
	功能: 运算参数

    //=================*=================\\
           教学官网：www.qiqiker.com
           官方微信服务号: qiqikertuts
           Plane老师微信: PlaneZhong
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using System;

namespace PEMath {
    public struct PEArgs {
        public int value;
        public uint multipler;

        public PEArgs(int value, uint multipler) {
            this.value = value;
            this.multipler = multipler;
        }

        public static PEArgs Zero = new PEArgs(0, 10000);
        public static PEArgs HALFPI = new PEArgs(15708, 10000);
        public static PEArgs PI = new PEArgs(31416, 10000);
        public static PEArgs TWOPI = new PEArgs(62832, 10000);

        public static bool operator >(PEArgs a, PEArgs b) {
            if(a.multipler == b.multipler) {
                return a.value > b.value;
            }
            else {
                throw new System.Exception("multipler is unequal.");
            }
        }
        public static bool operator <(PEArgs a, PEArgs b) {
            if(a.multipler == b.multipler) {
                return a.value < b.value;
            }
            else {
                throw new System.Exception("multipler is unequal.");
            }
        }
        public static bool operator >=(PEArgs a, PEArgs b) {
            if(a.multipler == b.multipler) {
                return a.value >= b.value;
            }
            else {
                throw new System.Exception("multipler is unequal.");
            }
        }
        public static bool operator <=(PEArgs a, PEArgs b) {
            if(a.multipler == b.multipler) {
                return a.value <= b.value;
            }
            else {
                throw new System.Exception("multipler is unequal.");
            }
        }
        public static bool operator ==(PEArgs a, PEArgs b) {
            if(a.multipler == b.multipler) {
                return a.value == b.value;
            }
            else {
                throw new System.Exception("multipler is unequal.");
            }
        }
        public static bool operator !=(PEArgs a, PEArgs b) {
            if(a.multipler == b.multipler) {
                return a.value != b.value;
            }
            else {
                throw new System.Exception("multipler is unequal.");
            }
        }


        /// <summary>
        /// 转化为视图角度，不可再用于逻辑运算
        /// </summary>
        /// <returns></returns>
        public int ConvertViewAngle() {
            float radians = ConvertToFloat();
            return (int)Math.Round(radians / Math.PI * 180);
        }

        /// <summary>
        /// 转化为视图弧度，不可再用于逻辑运算
        /// </summary>
        public float ConvertToFloat() {
            return value * 1.0f / multipler;
        }

        public override bool Equals(object obj) {
            return obj is PEArgs args &&
                value == args.value &&
                multipler == args.multipler;
        }

        public override int GetHashCode() {
            return value.GetHashCode();
        }

        public override string ToString() {
            return $"value:{value} multipler:{multipler}";
        }
    }
}
