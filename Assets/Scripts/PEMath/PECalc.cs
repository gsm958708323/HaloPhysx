/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com
	日期: 2021/03/25 16:19
	功能: 常用定点数数学运算

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

namespace PEMath {
    public class PECalc {

        public static PEInt Sqrt(PEInt value, int interatorCount = 8) {
            if(value == PEInt.zero) {
                return 0;
            }
            if(value < PEInt.zero) {
                throw new Exception();
            }

            PEInt result = value;
            PEInt history;
            int count = 0;
            do {
                history = result;
                result = (result + value / result) >> 1;
                ++count;
            } while(result != history && count < interatorCount);
            return result;
        }

        public static PEArgs Acos(PEInt value) {
            PEInt rate = (value * AcosTable.HalfIndexCount) + AcosTable.HalfIndexCount;
            rate = Clamp(rate, PEInt.zero, AcosTable.IndexCount);
            return new PEArgs(AcosTable.table[rate.RawInt], AcosTable.Multipler);
        }


        public static PEInt Clamp(PEInt input, PEInt min, PEInt max) {
            if(input < min) {
                return min;
            }
            if(input > max) {
                return max;
            }
            return input;
        }
    }
}
