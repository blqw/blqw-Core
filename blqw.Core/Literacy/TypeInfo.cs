﻿using System;
using System.Collections.Generic;
using System.Text;

namespace blqw
{
    /// <summary> 用于拓展系统Type对象的属性和方法
    /// </summary>
    public sealed class TypeInfo
    {
        /// <summary> 构造用于拓展系统Type对象的属性和方法的对象
        /// </summary>
        /// <param name="type">用于构造TypeInfo的Type对象实例,不可为null</param>
        internal TypeInfo(System.Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            Type = type;
            TypeCode = System.Type.GetTypeCode(type);
            IsArray = type.IsArray;
            IsMakeGenericType = type.IsGenericType && !type.IsGenericTypeDefinition;
            var valueType = Nullable.GetUnderlyingType(type);
            if (valueType != null)
            {
                IsNullable = true;
                UnderlyingType = TypesHelper.GetTypeInfo(valueType);
                IsNumberType = UnderlyingType.IsNumberType;
            }
            else if (type.IsEnum)
            {
                IsNumberType = true;
            }
            else
            {
                IsNumberType = (TypeCode >= TypeCode.SByte && TypeCode <= TypeCode.Decimal);
            }
        }

        #region Literacy

        private Literacy _literacy;

        /// <summary> 获取严格区分大小写的Literacy对象
        /// </summary>
        public Literacy Literacy
        {
            get
            {
                if (_literacy != null)
                {
                    return _literacy;
                }
                lock (this)
                {
                    if (_literacy == null)
                    {
                        _literacy = new Literacy(this, false);
                    }
                }
                return _literacy;
            }
        }

        private Literacy _ignoreCaseLiteracy;

        /// <summary> 获取不区分大小写的Literacy对象
        /// </summary>
        public Literacy IgnoreCaseLiteracy
        {
            get
            {
                if (_ignoreCaseLiteracy != null)
                {
                    return _ignoreCaseLiteracy;
                }
                lock (this)
                {
                    if (_ignoreCaseLiteracy == null)
                    {
                        _ignoreCaseLiteracy = new Literacy(this, true);
                    }
                }
                return _ignoreCaseLiteracy;
            }
        }

        #endregion

        /// <summary> 获取当前类型
        /// </summary>
        public readonly System.Type Type;
        /// <summary> 如果IsNullable为true, 该字段获取可空值类型的实际类型, 否则为空
        /// </summary>
        public readonly TypeInfo UnderlyingType;
        /// <summary> 是否为可空值类型
        /// </summary>
        public readonly bool IsNullable;
        /// <summary> 获取一个值，通过该值指示 System.Type 是否为数组。
        /// </summary>
        public readonly bool IsArray;
        /// <summary> 获取一个值，该值指示当前类型是否是已经构造完成的泛型类型。
        /// </summary>
        public readonly bool IsMakeGenericType;

        /// <summary> 指示当前类型是否是数字类型(不考虑溢出的情况下是否可以强转成任意数字类型)
        /// </summary>
        public readonly bool IsNumberType;

        /// <summary> 获取类型枚举
        /// </summary>
        public readonly TypeCode TypeCode;

        private TypeCodes _typeCodes = (blqw.TypeCodes)(-1);
        /// <summary> 获取类型的拓展枚举
        /// </summary>
        public TypeCodes TypeCodes
        {
            get
            {
                if (_typeCodes < 0)
                {
                    _typeCodes = GetTypeCodes();
                }
                return _typeCodes;
            }
        }

        private int _isSpecialType;
        /// <summary> 获取一个值，通过该值指示类型是否属于特殊类型
        /// 除了基元类型以外,Guid,TimeSpan,DateTime,DBNull,所有指针,以及这些类型的可空值类型,都属于特殊类型
        /// </summary>
        public bool IsSpecialType
        {
            get
            {
                if (_isSpecialType == 0)
                {
                    if (Type.IsPrimitive)
                    {
                        _isSpecialType = 1;
                    }
                    else
                    {
                        var codes = (int)TypeCodes;
                        if (codes > 2 && codes < 100)
                        {
                            _isSpecialType = 1;
                        }
                        else
                        {
                            _isSpecialType = 2;
                        }
                    }
                }
                return _isSpecialType == 1;
            }
        }

        private string _displayName;
        /// <summary> 获取当前类型名称的友好展现形式
        /// </summary>
        public string DisplayName
        {
            get
            {
                if (_displayName == null)
                {
                    _displayName = GetDisplayName();
                }
                return _displayName;
            }
        }

        private TypeInfo[] _genericArgumentsTypeInfo;

        /// <summary> 获取当前泛型类型的泛型参数信息,如果不是已构造的泛型类型,返回null
        /// </summary>
        public TypeInfo[] GenericArgumentsTypeInfo
        {
            get
            {
                if (!IsMakeGenericType)
                {
                    return null;
                }
                if (_genericArgumentsTypeInfo != null)
                {
                    return _genericArgumentsTypeInfo;
                }
                lock (this)
                {
                    if (IsMakeGenericType && _genericArgumentsTypeInfo == null)
                    {
                        var args = Type.GetGenericArguments();
                        _genericArgumentsTypeInfo = new TypeInfo[args.Length];
                        for (int i = 0; i < args.Length; i++)
                        {
                            _genericArgumentsTypeInfo[i] = TypesHelper.GetTypeInfo(args[i]);
                        }
                    }
                }
                return _genericArgumentsTypeInfo;
            }
        }

        #region 私有方法

        /// <summary> 获取当前类型的 TypeCodes 值
        /// </summary>
        private TypeCodes GetTypeCodes()
        {
            if (IsNullable) //可空值类型
            {
                return UnderlyingType.GetTypeCodes();
            }

            if (IsMakeGenericType) //泛型类型
            {
                if (Type.Name.StartsWith("<>f__AnonymousType"))//判断匿名类
                {
                    return TypeCodes.AnonymousType;
                }

                var args = Type.GetGenericArguments();
                switch (args.Length)
                {
                    case 1:
                        if (typeof(IList<>).MakeGenericType(args).IsAssignableFrom(Type))
                        {
                            return TypeCodes.IListT;
                        }
                        break;
                    case 2:
                        if (typeof(IDictionary<,>).MakeGenericType(args).IsAssignableFrom(Type))
                        {
                            return TypeCodes.IDictionaryT;
                        }
                        break;
                    default:
                        break;
                }
            }

            if (TypeCode == TypeCode.Object)
            {
                if (typeof(System.Collections.IList).IsAssignableFrom(Type))
                {
                    return TypeCodes.IList;
                }
                else if (typeof(System.Collections.IDictionary).IsAssignableFrom(Type))
                {
                    return TypeCodes.IDictionary;
                }
                else if (typeof(System.Data.IDataReader).IsAssignableFrom(Type))
                {
                    return TypeCodes.IDataReader;
                }
                else if (Type == typeof(TimeSpan))
                {
                    return TypeCodes.TimeSpan;
                }
                else if (Type == typeof(Guid))
                {
                    return TypeCodes.Guid;
                }
                else if (Type == typeof(System.Text.StringBuilder))
                {
                    return TypeCodes.StringBuilder;
                }
                else if (Type == typeof(System.Data.DataSet))
                {
                    return TypeCodes.DataSet;
                }
                else if (Type == typeof(System.Data.DataTable))
                {
                    return TypeCodes.DataTable;
                }
                else if (Type == typeof(System.Data.DataView))
                {
                    return TypeCodes.DataView;
                }
                else if (Type == typeof(IntPtr))
                {
                    return TypeCodes.IntPtr;
                }
                else if (Type == typeof(UIntPtr))
                {
                    return TypeCodes.UIntPtr;
                }
            }
            return (TypeCodes)TypeCode;
        }

        ///<summary> 获取类型名称的友好展现形式
        /// </summary>
        private string GetDisplayName()
        {
            if (IsNullable)
            {
                return UnderlyingType.DisplayName + "?";
            }
            if (Type.IsGenericType)
            {
                string[] generic;
                if (Type.IsGenericTypeDefinition) //泛型定义
                {
                    var args = Type.GetGenericArguments();
                    generic = new string[args.Length];
                }
                else
                {
                    var infos = GenericArgumentsTypeInfo;
                    generic = new string[infos.Length];
                    for (int i = 0; i < infos.Length; i++)
                    {
                        generic[i] = infos[i].DisplayName;
                    }
                }
                return GetSimpleName(Type) + "<" + string.Join(", ", generic) + ">";
            }
            else
            {
                return GetSimpleName(Type);
            }
        }

        private static string GetSimpleName(Type t)
        {
            string name;
            switch (t.Namespace)
            {
                case "System":
                case "System.Collections":
                case "System.Collections.Generic":
                case "System.Data":
                case "System.Text":
                    name = t.Name;
                    break;
                default:
                    name = t.Namespace + "." + t.Name;
                    break;
            }
            var index = name.LastIndexOf('`');
            if (index > -1)
            {
                name = name.Remove(index);
            }
            return name;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }
            var info = obj as TypeInfo;
            if (info == null)
            {
                return false;
            }
            return object.ReferenceEquals(this.Type, info.Type);
        }

        public override int GetHashCode()
        {
            return this.Type.GetHashCode();
        }
    }
}
