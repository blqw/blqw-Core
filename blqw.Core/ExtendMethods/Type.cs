﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace blqw
{
    public static partial class MetendMethod
    {
        /// <summary> 检查当前参数所表示的类型是否为数字
        /// </summary>
        public static bool IsNumber(Type t)
        {
            if (t == null)
            {
                return false;
            }
            var code = Type.GetTypeCode(t);
            return (code >= TypeCode.SByte && code <= TypeCode.Decimal) || t.IsEnum;
        }

        /// <summary> 检查一个类型是否为可空值类型
        /// </summary>
        public static bool IsNullable(this Type t)
        {
            return (t.IsValueType && t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary> 检查指定类型是否是当前类型的子类
        /// </summary>
        /// <param name="t">当前类型(父类)</param>
        /// <param name="child">指定类型(子类)</param>
        public static bool IsChild(this Type t, Type child)
        {
            return t != null && t.IsAssignableFrom(child);
        }

        /// <summary> 检查指定对象是否是当前类型(或其子类类型)的实例
        /// </summary>
        /// <param name="t">当前类型(父类)</param>
        /// <param name="obj">指定对象</param>
        /// <returns>存在继承关系返回true,否则返回false</returns>
        public static bool IsChild(this Type t, object obj)
        {
            return t != null && t.IsInstanceOfType(obj);
        }

        ///<summary> 获取一个类型名称的友好展现形式
        /// </summary>
        /// <param name="t"></param>
        public static string DisplayName(this Type t)
        {
            if (t == null)
            {
                return "null";
            }

            string name;
            switch (t.Namespace)
            {
                case "System":
                case "System.Collections":
                case "System.Collections.Generic":
                case "System.Data":
                    name = t.Name;
                    break;
                default:
                    name = t.Namespace + "." + t.Name;
                    break;
            }
            if (name.Length > 2)
            {
                if (name[name.Length - 2] == '`')
                {
                    name = name.Remove(name.Length - 2);
                }
                else if (name[name.Length - 3] == '`')
                {
                    name = name.Remove(name.Length - 3);
                }
            }

            if (t.IsGenericType)
            {
                var arr = t.GetGenericArguments();
                if (arr.Length == 1)
                {
                    return name + "<" + DisplayName(arr[0]) + ">";
                }
                StringBuilder sb = new StringBuilder(name);
                sb.Append("<");
                var length = arr.Length;
                sb.Append(DisplayName(arr[0]));
                for (int i = 1; i < length; i++)
                {
                    sb.Append(',');
                    sb.Append(DisplayName(arr[i]));
                }
                sb.Append('>');
                return sb.ToString();
            }

            return name;
        }
    }
}