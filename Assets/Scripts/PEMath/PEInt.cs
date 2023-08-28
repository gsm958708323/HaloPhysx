/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com
	日期: 2021/03/23 14:41
	功能: 定点数

    //=================*=================\\
           教学官网：www.qiqiker.com
           官方微信服务号: qiqikertuts
           Plane老师微信: PlaneZhong
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using System;

namespace PEMath {
    public struct PEInt {
        private long scaledValue;
        public long ScaledValue {
            get {
                return scaledValue;
            }
            set {
                scaledValue = value;
            }
        }
        //移位计数
        const int BIT_MOVE_COUNT = 10;
        const long MULTIPLIER_FACTOR = 1 << BIT_MOVE_COUNT;

        public static readonly PEInt zero = new PEInt(0);
        public static readonly PEInt one = new PEInt(1);


        #region 构造函数
        //内部使用，已经缩放完成的数据
        private PEInt(long scaledValue) {
            this.scaledValue = scaledValue;
        }
        public PEInt(int val) {
            scaledValue = val * MULTIPLIER_FACTOR;
        }
        public PEInt(float val) {
            scaledValue = (long)Math.Round(val * MULTIPLIER_FACTOR);
        }
        //float损失精度，必须显式转换
        public static explicit operator PEInt(float f) {
            return new PEInt((long)Math.Round(f * MULTIPLIER_FACTOR));
        }
        //int不损失精度，可以隐式转换
        public static implicit operator PEInt(int i) {
            return new PEInt(i);
        }
        #endregion

        #region 运算符
        //加，减，乘，除，取反
        public static PEInt operator +(PEInt a, PEInt b) {
            return new PEInt(a.scaledValue + b.scaledValue);
        }
        public static PEInt operator -(PEInt a, PEInt b) {
            return new PEInt(a.scaledValue - b.scaledValue);
        }
        public static PEInt operator *(PEInt a, PEInt b) {
            long value = a.scaledValue * b.scaledValue;
            if(value >= 0) {
                value >>= BIT_MOVE_COUNT;
            }
            else {
                value = -(-value >> BIT_MOVE_COUNT);
            }
            return new PEInt(value);
        }
        public static PEInt operator /(PEInt a, PEInt b) {
            if(b.scaledValue == 0) {
                throw new Exception();
            }
            return new PEInt((a.scaledValue << BIT_MOVE_COUNT) / b.scaledValue);
        }
        public static PEInt operator -(PEInt value) {
            return new PEInt(-value.scaledValue);
        }
        public static bool operator ==(PEInt a, PEInt b) {
            return a.scaledValue == b.scaledValue;
        }
        public static bool operator !=(PEInt a, PEInt b) {
            return a.scaledValue != b.scaledValue;
        }
        public static bool operator >(PEInt a, PEInt b) {
            return a.scaledValue > b.scaledValue;
        }
        public static bool operator <(PEInt a, PEInt b) {
            return a.scaledValue < b.scaledValue;
        }
        public static bool operator >=(PEInt a, PEInt b) {
            return a.scaledValue >= b.scaledValue;
        }
        public static bool operator <=(PEInt a, PEInt b) {
            return a.scaledValue <= b.scaledValue;
        }

        public static PEInt operator >>(PEInt value, int moveCount) {
            if(value.scaledValue >= 0) {
                return new PEInt(value.scaledValue >> moveCount);
            }
            else {
                return new PEInt(-(-value.scaledValue >> moveCount));
            }
        }
        public static PEInt operator <<(PEInt value, int moveCount) {
            return new PEInt(value.scaledValue << moveCount);
        }
        #endregion

        /// <summary>
        /// 转换完成后，不可再参与逻辑运算
        /// </summary>
        public float RawFloat {
            get {
                return scaledValue * 1.0f / MULTIPLIER_FACTOR;
            }
        }

        public int RawInt {
            get {
                if(scaledValue >= 0) {
                    return (int)(scaledValue >> BIT_MOVE_COUNT);
                }
                else {
                    return -(int)(-scaledValue >> BIT_MOVE_COUNT);
                }
            }
        }

        public override bool Equals(object obj) {
            if(obj == null) {
                return false;
            }
            PEInt vInt = (PEInt)obj;
            return scaledValue == vInt.scaledValue;
        }

        public override int GetHashCode() {
            return scaledValue.GetHashCode();
        }

        public override string ToString() {
            return RawFloat.ToString();
        }
    }
}
