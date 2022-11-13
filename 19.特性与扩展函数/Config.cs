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
        public int Number { get; set; }
        [Range(0,256), DisplayName("灰度值")]
        public int GrayValue { get; set; }

        public Config(int _number, int _gray_value)
        {
            Number = _number;
            GrayValue = _gray_value;
        }
    }
}
