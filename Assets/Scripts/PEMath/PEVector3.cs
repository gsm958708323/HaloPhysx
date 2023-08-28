/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com
	日期: 2021/03/24 4:05
	功能: 确定性向量运算

    //=================*=================\\
           教学官网：www.qiqiker.com
           官方微信服务号: qiqikertuts
           Plane老师微信: PlaneZhong
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using System;
using System.Collections.Generic;
using System.Text;

#if UNITY_ENV
using UnityEngine;
#endif

namespace PEMath {
    public struct PEVector3 {
        public PEInt x;
        public PEInt y;
        public PEInt z;
        public PEVector3(PEInt x, PEInt y, PEInt z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

#if UNITY_ENV
        public PEVector3(Vector3 v) {
            this.x = (PEInt)v.x;
            this.y = (PEInt)v.y;
            this.z = (PEInt)v.z;
        }
#endif

        public PEInt this[int index] {
            get {
                switch(index) {
                    case 0:
                        return x;
                    case 1:
                        return y;
                    case 2:
                        return z;
                    default:
                        return 0;
                }
            }
            set {
                switch(index) {
                    case 0:
                        x = value;
                        break;
                    case 1:
                        y = value;
                        break;
                    case 2:
                        z = value;
                        break;
                }
            }
        }

        #region 定义常用向量
        public static PEVector3 zero {
            get {
                return new PEVector3(0, 0, 0);
            }
        }
        public static PEVector3 one {
            get {
                return new PEVector3(1, 1, 1);
            }
        }
        public static PEVector3 forward {
            get {
                return new PEVector3(0, 0, 1);
            }
        }
        public static PEVector3 back {
            get {
                return new PEVector3(0, 0, -1);
            }
        }
        public static PEVector3 left {
            get {
                return new PEVector3(-1, 0, 0);
            }
        }
        public static PEVector3 right {
            get {
                return new PEVector3(1, 0, 0);
            }
        }
        public static PEVector3 up {
            get {
                return new PEVector3(0, 1, 0);
            }
        }
        public static PEVector3 down {
            get {
                return new PEVector3(0, -1, 0);
            }
        }
        #endregion

        #region 运算符
        public static PEVector3 operator +(PEVector3 v1, PEVector3 v2) {
            PEInt x = v1.x + v2.x;
            PEInt y = v1.y + v2.y;
            PEInt z = v1.z + v2.z;
            return new PEVector3(x, y, z);
        }
        public static PEVector3 operator -(PEVector3 v1, PEVector3 v2) {
            PEInt x = v1.x - v2.x;
            PEInt y = v1.y - v2.y;
            PEInt z = v1.z - v2.z;
            return new PEVector3(x, y, z);
        }
        public static PEVector3 operator *(PEVector3 v, PEInt value) {
            PEInt x = v.x * value;
            PEInt y = v.y * value;
            PEInt z = v.z * value;
            return new PEVector3(x, y, z);
        }
        public static PEVector3 operator *(PEInt value, PEVector3 v) {
            PEInt x = v.x * value;
            PEInt y = v.y * value;
            PEInt z = v.z * value;
            return new PEVector3(x, y, z);
        }
        public static PEVector3 operator /(PEVector3 v, PEInt value) {
            PEInt x = v.x / value;
            PEInt y = v.y / value;
            PEInt z = v.z / value;
            return new PEVector3(x, y, z);
        }
        public static PEVector3 operator -(PEVector3 v) {
            PEInt x = -v.x;
            PEInt y = -v.y;
            PEInt z = -v.z;
            return new PEVector3(x, y, z);
        }

        public static bool operator ==(PEVector3 v1, PEVector3 v2) {
            return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;
        }
        public static bool operator !=(PEVector3 v1, PEVector3 v2) {
            return v1.x != v2.x || v1.y != v2.y || v1.z != v2.z;
        }
        #endregion


        /// <summary>
        /// 当前向量长度平方
        /// </summary>
        public PEInt sqrMagnitude {
            get {
                return x * x + y * y + z * z;
            }
        }

        public static PEInt SqrMagnitude(PEVector3 v) {
            return v.x * v.x + v.y * v.y + v.z * v.z;
        }

        public PEInt magnitude {
            get {
                return PECalc.Sqrt(this.sqrMagnitude);
            }
        }

        /// <summary>
        /// 返回当前定点向量的单位向量
        /// </summary>
        public PEVector3 normalized {
            get {
                if(magnitude > 0) {
                    PEInt rate = PEInt.one / magnitude;
                    return new PEVector3(x * rate, y * rate, z * rate);
                }
                else {
                    return zero;
                }
            }
        }

        /// <summary>
        /// 返回传入参数向量的单位向量
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static PEVector3 Normalize(PEVector3 v) {
            if(v.magnitude > 0) {
                PEInt rate = PEInt.one / v.magnitude;
                return new PEVector3(v.x * rate, v.y * rate, v.z * rate);
            }
            else {
                return zero;
            }
        }

        /// <summary>
        /// 规格化当前PE向量为单位向量
        /// </summary>
        public void Normalize() {
            PEInt rate = PEInt.one / magnitude;
            x *= rate;
            y *= rate;
            z *= rate;
        }

        /// <summary>
        /// 点乘
        /// </summary>
        public static PEInt Dot(PEVector3 a, PEVector3 b) {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        /// <summary>
        /// 叉乘
        /// </summary>
        public static PEVector3 Cross(PEVector3 a, PEVector3 b) {
            return new PEVector3(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
        }

        /// <summary>
        /// 向量夹角
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static PEArgs Angle(PEVector3 from, PEVector3 to) {
            PEInt dot = Dot(from, to);
            PEInt mod = from.magnitude * to.magnitude;
            if(mod == 0) {
                return PEArgs.Zero;
            }
            PEInt value = dot / mod;
            //反余弦函数计算
            return PECalc.Acos(value);
        }

#if UNITY_ENV
        /// <summary>
        /// 获取浮点数向量（注意：不可再进行逻辑运算）
        /// </summary>
        public Vector3 ConvertViewVector3() {
            return new Vector3(x.RawFloat, y.RawFloat, z.RawFloat);
        }
#endif

        public long[] CovertLongArray() {
            return new long[] { x.ScaledValue, y.ScaledValue, z.ScaledValue };
        }

        public override bool Equals(object obj) {
            if(obj == null) {
                return false;
            }
            PEVector3 v = (PEVector3)obj;
            return v.x == x && v.y == y && v.z == z;
        }

        public override int GetHashCode() {
            return x.GetHashCode();
        }

        public override string ToString() {
            return string.Format("x:{0} y:{1} z:{2}", x, y, z);
        }
    }
}
