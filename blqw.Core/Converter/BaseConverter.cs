using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace blqw
{
    public abstract class BaseConverter<TInput> : IConverter<TInput>
    {
        private readonly static string _tName = TypesHelper.DisplayName(typeof(TInput));

        public bool ToBoolean(TInput input, bool defaultValue = default(bool), bool throwOnError = false)
        {
            bool value;
            if (TryToBoolean(input, out value))
            {
                return value;
            }
            return ReturnOrThrow(input, defaultValue, throwOnError);
        }
        public byte ToByte(TInput input, byte defaultValue = default(byte), bool throwOnError = false)
        {
            byte value;
            if (TryToByte(input, out value))
            {
                return value;
            }
            return ReturnOrThrow(input, defaultValue, throwOnError);
        }
        public char ToChar(TInput input, char defaultValue = default(char), bool throwOnError = false)
        {
            char value;
            if (TryToChar(input, out value))
            {
                return value;
            }
            return ReturnOrThrow(input, defaultValue, throwOnError);
        }
        public DateTime ToDateTime(TInput input, DateTime defaultValue = default(DateTime), bool throwOnError = false)
        {
            DateTime value;
            if (TryToDateTime(input, out value))
            {
                return value;
            }
            return ReturnOrThrow(input, defaultValue, throwOnError);
        }
        public TimeSpan ToTimeSpan(TInput input, TimeSpan defaultValue = default(TimeSpan), bool throwOnError = false)
        {
            TimeSpan value;
            if (TryToTimeSpan(input, out value))
            {
                return value;
            }
            return ReturnOrThrow(input, defaultValue, throwOnError);
        }
        public decimal ToDecimal(TInput input, decimal defaultValue = default(decimal), bool throwOnError = false)
        {
            decimal value;
            if (TryToDecimal(input, out value))
            {
                return value;
            }
            return ReturnOrThrow(input, defaultValue, throwOnError);
        }
        public double ToDouble(TInput input, double defaultValue = default(double), bool throwOnError = false)
        {
            double value;
            if (TryToDouble(input, out value))
            {
                return value;
            }
            return ReturnOrThrow(input, defaultValue, throwOnError);
        }
        public Guid ToGuid(TInput input, Guid defaultValue = default(Guid), bool throwOnError = false)
        {
            Guid value;
            if (TryToGuid(input, out value))
            {
                return value;
            }
            return ReturnOrThrow(input, defaultValue, throwOnError);
        }
        public byte[] ToBytes(TInput input, byte[] defaultValue = default(byte[]), bool throwOnError = false)
        {
            byte[] value;
            if (TryToBytes(input, out value))
            {
                return value;
            }
            return ReturnOrThrow(input, defaultValue, throwOnError);
        }
        public short ToInt16(TInput input, short defaultValue = default(short), bool throwOnError = false)
        {
            short value;
            if (TryToInt16(input, out value))
            {
                return value;
            }
            return ReturnOrThrow(input, defaultValue, throwOnError);
        }
        public int ToInt32(TInput input, int defaultValue = default(int), bool throwOnError = false)
        {
            int value;
            if (TryToInt32(input, out value))
            {
                return value;
            }
            return ReturnOrThrow(input, defaultValue, throwOnError);
        }
        public long ToInt64(TInput input, long defaultValue = default(long), bool throwOnError = false)
        {
            long value;
            if (TryToInt64(input, out value))
            {
                return value;
            }
            return ReturnOrThrow(input, defaultValue, throwOnError);
        }
        public sbyte ToSByte(TInput input, sbyte defaultValue = default(sbyte), bool throwOnError = false)
        {
            sbyte value;
            if (TryToSByte(input, out value))
            {
                return value;
            }
            return ReturnOrThrow(input, defaultValue, throwOnError);
        }
        public float ToSingle(TInput input, float defaultValue = default(float), bool throwOnError = false)
        {
            float value;
            if (TryToSingle(input, out value))
            {
                return value;
            }
            return ReturnOrThrow(input, defaultValue, throwOnError);
        }
        public ushort ToUInt16(TInput input, ushort defaultValue = default(ushort), bool throwOnError = false)
        {
            ushort value;
            if (TryToUInt16(input, out value))
            {
                return value;
            }
            return ReturnOrThrow(input, defaultValue, throwOnError);
        }
        public uint ToUInt32(TInput input, uint defaultValue = default(uint), bool throwOnError = false)
        {
            uint value;
            if (TryToUInt32(input, out value))
            {
                return value;
            }
            return ReturnOrThrow(input, defaultValue, throwOnError);
        }
        public ulong ToUInt64(TInput input, ulong defaultValue = default(ulong), bool throwOnError = false)
        {
            ulong value;
            if (TryToUInt64(input, out value))
            {
                return value;
            }
            return ReturnOrThrow(input, defaultValue, throwOnError);
        }
        public object ToObject(TInput input, Type outputType, object defaultValue = null, bool throwOnError = false)
        {
            if (TypesHelper.IsChild(outputType, input))
            {
                return input;
            }
            return ReturnOrThrow(input, defaultValue, throwOnError);
        }
        public string ToString(TInput input, string defaultValue = null, bool throwOnError = false)
        {
            return (input == null) ? null : input.ToString();
        }
        public TEnum ToEnum<TEnum>(TInput input, TEnum defaultValue, bool throwOnError = false)
                where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            throw new NotSupportedException();
        }
        public Enum ToEnum(TInput input, Enum defaultValue, bool throwOnError = false)
        {
            throw new NotSupportedException();
        }

        public object ChangedType(TInput input, Type outputType, object defaultValue = null, bool throwOnError = false)
        {
            object value;
            if (TryToType(input, outputType, out value))
            {
                return value;
            }
            return ReturnOrThrow(input, defaultValue, throwOnError);
        }

        #region InnerConverter
        public BaseConverter()
        {
            InnerConverter<bool>.Convert = TryToBoolean;
            InnerConverter<byte>.Convert = TryToByte;
            InnerConverter<char>.Convert = TryToChar;
            InnerConverter<DateTime>.Convert = TryToDateTime;
            InnerConverter<TimeSpan>.Convert = TryToTimeSpan;
            InnerConverter<decimal>.Convert = TryToDecimal;
            InnerConverter<double>.Convert = TryToDouble;
            InnerConverter<Guid>.Convert = TryToGuid;
            InnerConverter<short>.Convert = TryToInt16;
            InnerConverter<int>.Convert = TryToInt32;
            InnerConverter<long>.Convert = TryToInt64;
            InnerConverter<sbyte>.Convert = TryToSByte;
            InnerConverter<float>.Convert = TryToSingle;
            InnerConverter<ushort>.Convert = TryToUInt16;
            InnerConverter<uint>.Convert = TryToUInt32;
            InnerConverter<ulong>.Convert = TryToUInt64;

            InnerConverter<byte[]>.Convert = TryToBytes;
            InnerConverter<string>.Convert = TryToString;
            InnerConverter<object>.Convert2 = TryToObject;
            InnerConverter<Enum>.Convert2 = TryToEnum;
        }

        static class InnerConverter<TOutput>
        {
            private static readonly bool _canNull = default(TOutput) == null;
            public static TryConvert<TInput, TOutput> Convert;
            public static TryConvert<TInput, Type, TOutput> Convert2;

            private static bool TryToObject(TInput input, out object value)
            {
                if (input == null && _canNull)
                {
                    value = null;
                    return true;
                }
                TOutput o;
                if (Convert(input, out o))
                {
                    value = o;
                    return true;
                }
                value = null;
                return false;
            }

            private static bool TryToNullable(TInput input, out object value)
            {
                if (input == null)
                {
                    value = null;
                    return true;
                }
                TOutput o;
                if (Convert(input, out o))
                {
                    value = o;
                    return true;
                }
                value = null;
                return false;
            }

            public static TryConvert<TInput, object> CreateDelegate(bool isNullable)
            {
                return isNullable ? (TryConvert<TInput, object>)TryToNullable : TryToObject;
            }

            public static TryConvert<TInput, object> CreateDelegate(bool isNullable, Type outputType)
            {
                ConvertArg arg;
                arg.OutputType = outputType;
                arg.Value = null;
                return isNullable ? (TryConvert<TInput, object>)arg.TryToNullable : arg.TryToObject;
            }

            public static TryConvert<TInput, object> CreateDelegate(object value)
            {
                ConvertArg arg;
                arg.OutputType = null;
                arg.Value = value;
                return arg.TryToValue;
            }

            private struct ConvertArg
            {
                public Type OutputType;

                public object Value;

                public bool TryToObject(TInput input, out object value)
                {
                    if (input == null && _canNull)
                    {
                        value = null;
                        return true;
                    }
                    TOutput o;
                    if (Convert2(input, OutputType, out o))
                    {
                        value = o;
                        return true;
                    }
                    value = null;
                    return false;
                }

                public bool TryToNullable(TInput input, out object value)
                {
                    if (input == null)
                    {
                        value = null;
                        return true;
                    }
                    TOutput o;
                    if (Convert2(input, OutputType, out o))
                    {
                        value = o;
                        return true;
                    }
                    value = null;
                    return false;
                }

                public bool TryToValue(TInput input, out object value)
                {
                    value = Value;
                    return true;
                }
            }
        }

        #endregion

        public bool TryToObject(TInput input, Type outputType, out object value)
        {
            if (TypesHelper.IsChild(outputType, input))
            {
                value = input;
                return true;
            }
            value = null;
            return false;
        }
        public abstract bool TryToBoolean(TInput input, out bool value);
        public abstract bool TryToByte(TInput input, out byte value);
        public abstract bool TryToBytes(TInput input, out byte[] value);
        public abstract bool TryToChar(TInput input, out char value);
        public abstract bool TryToDateTime(TInput input, out DateTime value);
        public abstract bool TryToTimeSpan(TInput input, out TimeSpan value);
        public abstract bool TryToDateTime(TInput input, string format, out DateTime value);
        public abstract bool TryToTimeSpan(TInput input, string format, out TimeSpan value);
        public abstract bool TryToDecimal(TInput input, out decimal value);
        public abstract bool TryToDouble(TInput input, out double value);
        public abstract bool TryToInt16(TInput input, out short value);
        public abstract bool TryToInt32(TInput input, out int value);
        public abstract bool TryToInt64(TInput input, out long value);
        public abstract bool TryToSByte(TInput input, out sbyte value);
        public abstract bool TryToSingle(TInput input, out float value);
        public bool TryToString(TInput input, out string value)
        {
            value = (input == null) ? null : input.ToString();
            return true;
        }
        public abstract bool TryToUInt16(TInput input, out ushort value);
        public abstract bool TryToUInt32(TInput input, out uint value);
        public abstract bool TryToUInt64(TInput input, out ulong value);
        public abstract bool TryToGuid(TInput input, out Guid value);
        public abstract bool TryToEnum(TInput input, Type enumType, out Enum value);
        public abstract bool TryToEnum<TEnum>(TInput input, out TEnum value)
            where TEnum : struct,IComparable, IFormattable, IConvertible;

        public bool TryToType(TInput input, Type outputType, out object result)
        {
            if (TypesHelper.IsChild(outputType, input))
            {
                result = input;
                return true;
            }
            if (outputType.IsAbstract == false && outputType.IsInterface == false)
            {
                var ti = TypesHelper.GetTypeInfo(outputType);
                if (input == null) //处理输出为null的情况
                {
                    if (outputType.IsValueType)
                    {
                        if (ti.IsNullable)
                        {
                            result = null;
                            return true;
                        }
                        else
                        {
                            result = null;
                            return false;
                        }
                    }
                    else if (outputType.IsClass)
                    {
                        result = null;
                        return true;
                    }
                }
                switch (ti.TypeCodes)
                {
                    case TypeCodes.Boolean:
                        {
                            bool value;
                            if (TryToBoolean(input, out value))
                            {
                                result = value;
                                return true;
                            }
                        }
                        break;
                    case TypeCodes.Byte:
                        {
                            Byte value;
                            if (TryToByte(input, out value))
                            {
                                result = value;
                                return true;
                            }
                        }
                        break;
                    case TypeCodes.Char:
                        {
                            Char value;
                            if (TryToChar(input, out value))
                            {
                                result = value;
                                return true;
                            }
                        }
                        break;
                    case TypeCodes.DateTime:
                        {
                            DateTime value;
                            if (TryToDateTime(input, out value))
                            {
                                result = value;
                                return true;
                            }
                        }
                        break;
                    case TypeCodes.Decimal:
                        {
                            Decimal value;
                            if (TryToDecimal(input, out value))
                            {
                                result = value;
                                return true;
                            }
                        }
                        break;
                    case TypeCodes.Double:
                        {
                            Double value;
                            if (TryToDouble(input, out value))
                            {
                                result = value;
                                return true;
                            }
                        }
                        break;
                    case TypeCodes.Int16:
                        {
                            Int16 value;
                            if (TryToInt16(input, out value))
                            {
                                result = value;
                                return true;
                            }
                        }
                        break;
                    case TypeCodes.Int32:
                        {
                            Int32 value;
                            if (TryToInt32(input, out value))
                            {
                                result = value;
                                return true;
                            }
                        }
                        break;
                    case TypeCodes.Int64:
                        {
                            Int64 value;
                            if (TryToInt64(input, out value))
                            {
                                result = value;
                                return true;
                            }
                        }
                        break;
                    case TypeCodes.SByte:
                        {
                            SByte value;
                            if (TryToSByte(input, out value))
                            {
                                result = value;
                                return true;
                            }
                        }
                        break;
                    case TypeCodes.Single:
                        {
                            Single value;
                            if (TryToSingle(input, out value))
                            {
                                result = value;
                                return true;
                            }
                        }
                        break;
                    case TypeCodes.String:
                        {
                            String value;
                            if (TryToString(input, out value))
                            {
                                result = value;
                                return true;
                            }
                        }
                        break;
                    case TypeCodes.UInt16:
                        {
                            UInt16 value;
                            if (TryToUInt16(input, out value))
                            {
                                result = value;
                                return true;
                            }
                        }
                        break;
                    case TypeCodes.UInt32:
                        {
                            UInt32 value;
                            if (TryToUInt32(input, out value))
                            {
                                result = value;
                                return true;
                            }
                        }
                        break;
                    case TypeCodes.UInt64:
                        {
                            UInt64 value;
                            if (TryToUInt64(input, out value))
                            {
                                result = value;
                                return true;
                            }
                        }
                        break;
                    case TypeCodes.Empty:
                        result = null;
                        return true;
                    case TypeCodes.DBNull:
                        result = DBNull.Value;
                        return true;
                    case TypeCodes.Guid:
                        {
                            Guid value;
                            if (TryToGuid(input, out value))
                            {
                                result = value;
                                return true;
                            }
                        }
                        break;
                    case TypeCodes.TimeSpan:
                        {
                            TimeSpan value;
                            if (TryToTimeSpan(input, out value))
                            {
                                result = value;
                                return true;
                            }
                        }
                        break;
                    case TypeCodes.IList:
                        if (ti.Type == typeof(byte[]))
                        {
                            byte[] value;
                            if (TryToBytes(input, out value))
                            {
                                result = value;
                                return true;
                            }
                        }
                        break;
                    case TypeCodes.Enum:
                        {
                            Enum value;
                            if (TryToEnum(input, outputType, out value))
                            {
                                result = value;
                                return true;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            result = null;
            return false;
        }

        public TryConvert<TInput, object> CreateDelegate(Type outputType)
        {
            if (outputType.IsAbstract || outputType.IsInterface)
            {
                //抽象类或者接口只能强转
                return InnerConverter<object>.CreateDelegate(false, outputType);
            }
            var ti = TypesHelper.GetTypeInfo(outputType);

            switch (ti.TypeCodes)
            {
                case TypeCodes.Boolean:
                    return InnerConverter<bool>.CreateDelegate(ti.IsNullable);
                case TypeCodes.Byte:
                    return InnerConverter<Byte>.CreateDelegate(ti.IsNullable);
                case TypeCodes.Char:
                    return InnerConverter<Char>.CreateDelegate(ti.IsNullable);
                case TypeCodes.DateTime:
                    return InnerConverter<DateTime>.CreateDelegate(ti.IsNullable);
                case TypeCodes.Decimal:
                    return InnerConverter<Decimal>.CreateDelegate(ti.IsNullable);
                case TypeCodes.Double:
                    return InnerConverter<Double>.CreateDelegate(ti.IsNullable);
                case TypeCodes.Int16:
                    return InnerConverter<Int16>.CreateDelegate(ti.IsNullable);
                case TypeCodes.Int32:
                    return InnerConverter<Int32>.CreateDelegate(ti.IsNullable);
                case TypeCodes.Int64:
                    return InnerConverter<Int64>.CreateDelegate(ti.IsNullable);
                case TypeCodes.SByte:
                    return InnerConverter<SByte>.CreateDelegate(ti.IsNullable);
                case TypeCodes.Single:
                    return InnerConverter<Single>.CreateDelegate(ti.IsNullable);
                case TypeCodes.String:
                    return (TInput input, out object value) => {
                        string str;
                        if (TryToString(input, out str))
                        {
                            value = str;
                            return true;
                        }
                        value = null;
                        return false;
                    };
                    //return InnerConverter<String>.CreateDelegate(ti.IsNullable);
                case TypeCodes.UInt16:
                    return InnerConverter<UInt16>.CreateDelegate(ti.IsNullable);
                case TypeCodes.UInt32:
                    return InnerConverter<UInt32>.CreateDelegate(ti.IsNullable);
                case TypeCodes.UInt64:
                    return InnerConverter<UInt64>.CreateDelegate(ti.IsNullable);
                case TypeCodes.Empty:
                    return InnerConverter<object>.CreateDelegate(null);
                case TypeCodes.DBNull:
                    return InnerConverter<object>.CreateDelegate(DBNull.Value);
                case TypeCodes.Guid:
                    return InnerConverter<Guid>.CreateDelegate(ti.IsNullable);
                case TypeCodes.TimeSpan:
                    return InnerConverter<TimeSpan>.CreateDelegate(ti.IsNullable);
                case TypeCodes.IList:
                    if (ti.Type == typeof(byte[]))
                    {
                        return InnerConverter<byte[]>.CreateDelegate(ti.IsNullable);
                    }
                    break;
                case TypeCodes.Enum:

                default:
                    break;
            }
            return InnerConverter<object>.CreateDelegate(false, outputType);
        }

        private static TOutput ReturnOrThrow<TOutput>(TInput input, TOutput defaultValue, bool throwOnError)
        {
            if (throwOnError)
            {
                throw new InvalidCastException(string.Concat("值 '", (object)input ?? "<NULL>", "' 无法转为 ", _tName, " 类型"));
            }
            else
            {
                return defaultValue;
            }
        }

    }
}
