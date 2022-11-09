using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _19.特性与扩展函数
{
    internal abstract class ValidityAttribute: Attribute
    {
        /// <summary>
        /// 【抽象方法】验证参数有效性的抽象函数
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        public abstract bool CheckValid(object _value);
    }

    public static class ValidityAttributeExtend
    {
        public static bool Valid<T>(this T t)
        {
            Type type = typeof(T);
            foreach (var property in type.GetProperties())
            {
                if (property.IsDefined(typeof(ValidityAttribute), true))
                {
                    object _value = property.GetValue(t);
                    foreach (ValidityAttribute _attribute in property.GetCustomAttributes<ValidityAttribute>())
                    {
                        if (!_attribute.CheckValid(_value))
                        {
                            if (property.IsDefined(typeof(DisplayNameAttribute), true))
                            {
                                Console.WriteLine($"属性{property.GetCustomAttribute<DisplayNameAttribute>().DisplayName}值异常");
                            }
                            else
                            {
                                Console.WriteLine($"属性{property.Name}值异常");
                            }
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    internal class RangeAttribute : ValidityAttribute
    {
        private int mMin;
        private int mMax;
        public RangeAttribute(int _min, int _max)
        {
            mMin = _min;
            mMax = _max;
        }

        public override bool CheckValid(object _value)
        {
            int temp;
            bool _ret = int.TryParse(_value.ToString(), out temp);
            return _ret && mMin <= temp && temp <= mMax;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    internal class NumberAttribute : ValidityAttribute
    {
        public override bool CheckValid(object _value)
        {
            return _value!=null && Regex.IsMatch(_value.ToString(), "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$");
        }
    }




}
