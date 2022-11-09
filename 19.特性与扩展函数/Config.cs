using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19.特性与扩展函数
{
    internal class Config
    {
        [Number, DisplayName("数字")]
        public double Number { get; set; }
        [Range(0,256), DisplayName("灰度值")]
        public double GrayValue { get; set; }

        public Config(int _number, int _gray_value)
        {
            Number = _number;
            GrayValue = _gray_value;
        }
    }

    internal class Config1
    {
        [Number, DisplayName("数字1")]
        public double Number { get; set; }
        [Range(0, 256), DisplayName("灰度值1")]
        public double GrayValue { get; set; }

        public Config1(int _number, int _gray_value)
        {
            Number = _number;
            GrayValue = _gray_value;
        }
    }
}
