using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace blqw
{


    public static class Convert2
    {
        public static bool TryParseBoolean(object input, out bool result)
        {
            if (input is bool)
            {
                result = (bool)input;
                return true;
            }
            var str = input as string;
            if (str != null)
            {
                return StringToBoolean(str, out result);
            }
            var conv = input as IConvertible;
            if (conv != null)
            {
                switch (conv.GetTypeCode())
                {
                    case TypeCode.Boolean:
                        result = conv.ToBoolean(null);
                        return true;
                    case TypeCode.Empty:
                    case TypeCode.DBNull:
                    case TypeCode.DateTime:
                        result = false;
                        return false;
                    case TypeCode.Byte: result = conv.ToByte(null) != 0; return true;
                    case TypeCode.Char: result = conv.ToChar(null) != 0; return true;
                    case TypeCode.Int16: result = conv.ToInt16(null) != 0; return true;
                    case TypeCode.Int32: result = conv.ToInt32(null) != 0; return true;
                    case TypeCode.Int64: result = conv.ToInt64(null) != 0; return true;
                    case TypeCode.SByte: result = conv.ToSByte(null) != 0; return true;
                    case TypeCode.Double: result = conv.ToDouble(null) != 0; return true;
                    case TypeCode.Single: result = conv.ToSingle(null) != 0; return true;
                    case TypeCode.UInt16: result = conv.ToUInt16(null) != 0; return true;
                    case TypeCode.UInt32: result = conv.ToUInt32(null) != 0; return true;
                    case TypeCode.UInt64: result = conv.ToUInt64(null) != 0; return true;
                    case TypeCode.Decimal: result = conv.ToDecimal(null) != 0; return true;
                    default:
                        break;
                }
            }
            else if (input == null)
            {
                result = false;
                return false;
            }
            else
            {
                var bs = input as byte[];
                if (bs != null)
                {
                    if (bs.Length != 1)
                    {
                        result = false;
                        return false;
                    }
                    result = BitConverter.ToBoolean(bs, 0);
                    return true;
                }
            }
            return StringToBoolean(input.ToString(), out result);
        }
        public static bool TryParseByte(object input, out byte result)
        {
            if (input is byte)
            {
                result = (byte)input;
                return true;
            }
            var str = input as string;
            if (str != null)
            {
                return StringToByte(str, out result);
            }
            var conv = input as IConvertible;
            if (conv != null)
            {
                switch (conv.GetTypeCode())
                {
                    case TypeCode.Boolean:
                        result = conv.ToBoolean(null) ? (byte)1 : (byte)0;
                        return true;
                    case TypeCode.Empty:
                    case TypeCode.DBNull:
                    case TypeCode.DateTime:
                        result = 0;
                        return false;
                    case TypeCode.Byte:
                        result = conv.ToByte(null);
                        return true;
                    case TypeCode.Char:
                        result = (byte)conv.ToChar(null);
                        return true;
                    case TypeCode.Int16:
                        {
                            var a = conv.ToInt16(null);
                            if (a < 0 || a > 255)
                            {
                                result = 0;
                                return false;
                            }
                            result = (byte)a;
                            return true;
                        }
                    case TypeCode.Int32:
                        {
                            var a = conv.ToInt32(null);
                            if (a < 0 || a > 255)
                            {
                                result = 0;
                                return false;
                            }
                            result = (byte)a;
                            return true;
                        }
                    case TypeCode.Int64:
                        {
                            var a = conv.ToInt64(null);
                            if (a < 0 || a > 255)
                            {
                                result = 0;
                                return false;
                            }
                            result = (byte)a;
                            return true;
                        }
                    case TypeCode.SByte:
                        {
                            var a = conv.ToSByte(null);
                            if (a < 0)
                            {
                                result = 0;
                                return false;
                            }
                            result = (byte)a;
                            return true;
                        }
                    case TypeCode.Double:
                        {
                            var a = conv.ToDouble(null);
                            if (a < 0 || a > 255)
                            {
                                result = 0;
                                return false;
                            }
                            result = (byte)a;
                            return true;
                        }
                    case TypeCode.Single:
                        {
                            var a = conv.ToSingle(null);
                            if (a < 0 || a > 255)
                            {
                                result = 0;
                                return false;
                            }
                            result = (byte)a;
                            return true;
                        }
                    case TypeCode.UInt16:
                        {
                            var a = conv.ToUInt16(null);
                            if (a > 255)
                            {
                                result = 0;
                                return false;
                            }
                            result = (byte)a;
                            return true;
                        }
                    case TypeCode.UInt32:
                        {
                            var a = conv.ToUInt32(null);
                            if (a > 255)
                            {
                                result = 0;
                                return false;
                            }
                            result = (byte)a;
                            return true;
                        }
                    case TypeCode.UInt64:
                        {
                            var a = conv.ToUInt64(null);
                            if (a > 255)
                            {
                                result = 0;
                                return false;
                            }
                            result = (byte)a;
                            return true;
                        }
                    case TypeCode.Decimal:
                        {
                            var a = conv.ToDecimal(null);
                            if (a < 0 || a > 255)
                            {
                                result = 0;
                                return false;
                            }
                            result = (byte)a;
                            return true;
                        }
                    default:
                        break;
                }
            }
            else if (input == null)
            {
                result = 0;
                return false;
            }
            else
            {
                var bs = input as byte[];
                if (bs != null)
                {
                    if (bs.Length != 1)
                    {
                        result = 0;
                        return false;
                    }
                    result = bs[0];
                    return true;
                }
            }
            return StringToByte(input.ToString(), out result);
        }
        public static bool TryParseChar(object input, out char result)
        {
            if (input is char)
            {
                result = (char)input;
                return true;
            }
            var str = input as string;
            if (str != null)
            {
                if (str.Length == 0)
                {
                    result = default(char);
                    return false;
                }
                result = str[0];
                return true;
            }
            var conv = input as IConvertible;
            if (conv != null)
            {
                switch (conv.GetTypeCode())
                {
                    case TypeCode.Boolean:
                        result = conv.ToBoolean(null) ? 'T' : 'F';
                        return true;
                    case TypeCode.Empty:
                    case TypeCode.DBNull:
                    case TypeCode.DateTime:
                        result = default(char);
                        return false;
                    case TypeCode.Byte:
                        result = (char)conv.ToByte(null);
                        return true;
                    case TypeCode.Char:
                        result = conv.ToChar(null);
                        return true;
                    case TypeCode.Int16:
                        {
                            var a = conv.ToInt16(null);
                            if (a < 0 || a > 255)
                            {
                                result = default(char);
                                return false;
                            }
                            result = (char)a;
                            return true;
                        }
                    case TypeCode.Int32:
                        {
                            var a = conv.ToInt32(null);
                            if (a < 0 || a > 255)
                            {
                                result = default(char);
                                return false;
                            }
                            result = (char)a;
                            return true;
                        }
                    case TypeCode.Int64:
                        {
                            var a = conv.ToInt64(null);
                            if (a < 0 || a > 255)
                            {
                                result = default(char);
                                return false;
                            }
                            result = (char)a;
                            return true;
                        }
                    case TypeCode.SByte:
                        {
                            var a = conv.ToSByte(null);
                            if (a < 0)
                            {
                                result = default(char);
                                return false;
                            }
                            result = (char)a;
                            return true;
                        }
                    case TypeCode.Double:
                        {
                            var a = conv.ToDouble(null);
                            if (a < 0 || a > 255)
                            {
                                result = default(char);
                                return false;
                            }
                            result = (char)a;
                            return true;
                        }
                    case TypeCode.Single:
                        {
                            var a = conv.ToSingle(null);
                            if (a < 0 || a > 255)
                            {
                                result = default(char);
                                return false;
                            }
                            result = (char)a;
                            return true;
                        }
                    case TypeCode.UInt16:
                        {
                            var a = conv.ToUInt16(null);
                            if (a > 255)
                            {
                                result = default(char);
                                return false;
                            }
                            result = (char)a;
                            return true;
                        }
                    case TypeCode.UInt32:
                        {
                            var a = conv.ToUInt32(null);
                            if (a > 255)
                            {
                                result = default(char);
                                return false;
                            }
                            result = (char)a;
                            return true;
                        }
                    case TypeCode.UInt64:
                        {
                            var a = conv.ToUInt64(null);
                            if (a > 255)
                            {
                                result = default(char);
                                return false;
                            }
                            result = (char)a;
                            return true;
                        }
                    case TypeCode.Decimal:
                        {
                            var a = conv.ToDecimal(null);
                            if (a < 0 || a > 255)
                            {
                                result = default(char);
                                return false;
                            }
                            result = (char)a;
                            return true;
                        }
                    default:
                        break;
                }
            }
            else
            {
                var bs = input as byte[];
                if (bs != null)
                {
                    if (bs.Length == 0 || bs.Length > 2)
                    {
                        result = default(char);
                        return false;
                    }
                    result = BitConverter.ToChar(bs, 0);
                    return true;
                }
            }
            result = default(char);
            return false;
        }
        public static bool TryParseDateTime(object input, out DateTime result)
        {
            if (input is DateTime)
            {
                result = (DateTime)input;
                return true;
            }
            var str = input as string;
            if (str != null)
            {
                return StringToDateTime(str, out result);
            }
            var conv = input as IConvertible;
            if (conv != null)
            {
                switch (conv.GetTypeCode())
                {
                    case TypeCode.DateTime:
                        result = conv.ToDateTime(null);
                        return true;
                    case TypeCode.Byte:
                    case TypeCode.Boolean:
                    case TypeCode.Empty:
                    case TypeCode.DBNull:
                    case TypeCode.Char:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.SByte:
                    case TypeCode.Double:
                    case TypeCode.Single:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                    case TypeCode.Decimal:
                        result = default(DateTime);
                        return false;
                    default:
                        break;
                }
            }
            else if (input == null)
            {
                result = default(DateTime);
                return false;
            }
            return StringToDateTime(input.ToString(), out result);
        }
        public static bool TryParseTimeSpan(object input, out TimeSpan result)
        {
            if (input is TimeSpan)
            {
                result = (TimeSpan)input;
                return true;
            }
            var str = input as string;
            if (str != null)
            {
                return StringToTimeSpan(str, out result);
            }
            else if (input == null)
            {
                result = default(TimeSpan);
                return false;
            }
            return StringToTimeSpan(input.ToString(), out result);
        }
        public static bool TryParseDecimal(object input, out decimal result)
        {
            if (input is Decimal)
            {
                result = (Decimal)input;
                return true;
            }
            var str = input as string;
            if (str != null)
            {
                return StringToDecimal(str, out result);
            }
            var conv = input as IConvertible;
            if (conv != null)
            {
                switch (conv.GetTypeCode())
                {
                    case TypeCode.Boolean:
                        result = conv.ToBoolean(null) ? (Decimal)1 : (Decimal)0;
                        return true;
                    case TypeCode.Empty:
                    case TypeCode.DBNull:
                    case TypeCode.DateTime:
                        result = 0;
                        return false;
                    case TypeCode.Byte: result = conv.ToByte(null); return true;
                    case TypeCode.Char: result = (Decimal)conv.ToChar(null); return true;
                    case TypeCode.Int16: result = (Decimal)conv.ToInt16(null); return true;
                    case TypeCode.Int32: result = (Decimal)conv.ToInt32(null); return true;
                    case TypeCode.Int64: result = (Decimal)conv.ToInt64(null); return true;
                    case TypeCode.SByte: result = (Decimal)conv.ToSByte(null); return true;
                    case TypeCode.Double: result = (Decimal)conv.ToDouble(null); return true;
                    case TypeCode.Single: result = (Decimal)conv.ToSingle(null); return true;
                    case TypeCode.UInt16: result = (Decimal)conv.ToUInt16(null); return true;
                    case TypeCode.UInt32: result = (Decimal)conv.ToUInt32(null); return true;
                    case TypeCode.UInt64: result = (Decimal)conv.ToUInt64(null); return true;
                    case TypeCode.Decimal: result = (Decimal)conv.ToDecimal(null); return true;
                    default:
                        break;
                }
            }
            else if (input == null)
            {
                result = 0;
                return false;
            }
            return StringToDecimal(input.ToString(), out result);
        }
        public static bool TryParseDouble(object input, out double result)
        {
            if (input is Double)
            {
                result = (Double)input;
                return true;
            }
            var str = input as string;
            if (str != null)
            {
                return StringToDouble(str, out result);
            }
            var conv = input as IConvertible;
            if (conv != null)
            {
                switch (conv.GetTypeCode())
                {
                    case TypeCode.Boolean:
                        result = conv.ToBoolean(null) ? (Double)1 : (Double)0;
                        return true;
                    case TypeCode.Empty:
                    case TypeCode.DBNull:
                    case TypeCode.DateTime:
                        result = 0;
                        return false;
                    case TypeCode.Byte: result = conv.ToByte(null); return true;
                    case TypeCode.Char: result = (Double)conv.ToChar(null); return true;
                    case TypeCode.Int16: result = (Double)conv.ToInt16(null); return true;
                    case TypeCode.Int32: result = (Double)conv.ToInt32(null); return true;
                    case TypeCode.Int64: result = (Double)conv.ToInt64(null); return true;
                    case TypeCode.SByte: result = (Double)conv.ToSByte(null); return true;
                    case TypeCode.Double: result = (Double)conv.ToDouble(null); return true;
                    case TypeCode.Single: result = (Double)conv.ToSingle(null); return true;
                    case TypeCode.UInt16: result = (Double)conv.ToUInt16(null); return true;
                    case TypeCode.UInt32: result = (Double)conv.ToUInt32(null); return true;
                    case TypeCode.UInt64: result = (Double)conv.ToUInt64(null); return true;
                    case TypeCode.Decimal: result = (Double)conv.ToDecimal(null); return true;
                    default:
                        break;
                }
            }
            else if (input == null)
            {
                result = 0;
                return false;
            }
            else
            {
                var bs = input as byte[];
                if (bs != null)
                {
                    if (bs.Length == 0 || bs.Length > 8)
                    {
                        result = 0;
                        return false;
                    }
                    result = BitConverter.ToDouble(bs, 0);
                    return true;
                }
            }
            return StringToDouble(input.ToString(), out result);
        }
        public static bool TryParseSingle(object input, out float result)
        {
            if (input is Single)
            {
                result = (Single)input;
                return true;
            }
            var str = input as string;
            if (str != null)
            {
                return StringToSingle(str, out result);
            }
            var conv = input as IConvertible;
            if (conv != null)
            {
                switch (conv.GetTypeCode())
                {
                    case TypeCode.Boolean:
                        result = conv.ToBoolean(null) ? (Single)1 : (Single)0;
                        return true;
                    case TypeCode.Empty:
                    case TypeCode.DBNull:
                    case TypeCode.DateTime:
                        result = 0;
                        return false;
                    case TypeCode.Byte:
                        result = conv.ToByte(null);
                        return true;
                    case TypeCode.Char:
                        result = (Single)conv.ToChar(null);
                        return true;
                    case TypeCode.Int16: result = (Single)conv.ToInt16(null); return true;
                    case TypeCode.Int32: result = (Single)conv.ToInt32(null); return true;
                    case TypeCode.Int64: result = (Single)conv.ToInt64(null); return true;
                    case TypeCode.SByte: result = (Single)conv.ToSByte(null); return true;
                    case TypeCode.Double:
                        {
                            var a = conv.ToDouble(null);
                            if (a < Single.MinValue || a > Single.MaxValue)
                            {
                                result = 0;
                                return false;
                            }
                            result = (Single)a;
                            return true;
                        }
                    case TypeCode.Single: result = (Single)conv.ToSingle(null); return true;
                    case TypeCode.UInt16: result = (Single)conv.ToUInt16(null); return true;
                    case TypeCode.UInt32: result = (Single)conv.ToUInt32(null); return true;
                    case TypeCode.UInt64: result = (Single)conv.ToUInt64(null); return true;
                    case TypeCode.Decimal: result = (Single)conv.ToDecimal(null); return true;
                    default:
                        break;
                }
            }
            else if (input == null)
            {
                result = 0;
                return false;
            }
            else
            {
                var bs = input as byte[];
                if (bs != null)
                {
                    if (bs.Length == 0 || bs.Length > 4)
                    {
                        result = 0;
                        return false;
                    }
                    result = BitConverter.ToSingle(bs, 0);
                    return true;
                }
            }
            return StringToSingle(input.ToString(), out result);
        }
        public static bool TryParseInt16(object input, out short result)
        {
            if (input is short)
            {
                result = (short)input;
                return true;
            }
            var str = input as string;
            if (str != null)
            {
                return StringToInt16(str, out result);
            }
            var conv = input as IConvertible;
            if (conv != null)
            {
                switch (conv.GetTypeCode())
                {
                    case TypeCode.Boolean:
                        result = conv.ToBoolean(null) ? (short)1 : (short)0;
                        return true;
                    case TypeCode.Empty:
                    case TypeCode.DBNull:
                    case TypeCode.DateTime:
                        result = 0;
                        return false;
                    case TypeCode.Byte: result = conv.ToByte(null); return true;
                    case TypeCode.Char: result = (short)conv.ToChar(null); return true;
                    case TypeCode.Int16: result = conv.ToInt16(null); return true;
                    case TypeCode.Int32:
                        {
                            var a = conv.ToInt32(null);
                            if (a < -32768 || a > 32767)
                            {
                                result = 0;
                                return false;
                            }
                            result = (short)a;
                            return true;
                        }
                    case TypeCode.Int64:
                        {
                            var a = conv.ToInt64(null);
                            if (a < -32768 || a > 32767)
                            {
                                result = 0;
                                return false;
                            }
                            result = (short)a;
                            return true;
                        }
                    case TypeCode.SByte: result = (short)conv.ToSByte(null); return true;
                    case TypeCode.Double:
                        {
                            var a = conv.ToDouble(null);
                            if (a < -32768 || a > 32767)
                            {
                                result = 0;
                                return false;
                            }
                            result = (short)a;
                            return true;
                        }
                    case TypeCode.Single:
                        {
                            var a = conv.ToSingle(null);
                            if (a < -32768 || a > 32767)
                            {
                                result = 0;
                                return false;
                            }
                            result = (short)a;
                            return true;
                        }
                    case TypeCode.UInt16:
                        {
                            var a = conv.ToUInt16(null);
                            if (a > 32767)
                            {
                                result = 0;
                                return false;
                            }
                            result = (short)a;
                            return true;
                        }
                    case TypeCode.UInt32:
                        {
                            var a = conv.ToUInt32(null);
                            if (a > 32767)
                            {
                                result = 0;
                                return false;
                            }
                            result = (short)a;
                            return true;
                        }
                    case TypeCode.UInt64:
                        {
                            var a = conv.ToUInt64(null);
                            if (a > 32767)
                            {
                                result = 0;
                                return false;
                            }
                            result = (short)a;
                            return true;
                        }
                    case TypeCode.Decimal:
                        {
                            var a = conv.ToDecimal(null);
                            if (a < -32768 || a > 32767)
                            {
                                result = 0;
                                return false;
                            }
                            result = (short)a;
                            return true;
                        }
                    default:
                        break;
                }
            }
            else if (input == null)
            {
                result = 0;
                return false;
            }
            else
            {
                var bs = input as byte[];
                if (bs != null)
                {
                    if (bs.Length == 0 || bs.Length > 2)
                    {
                        result = 0;
                        return false;
                    }
                    result = BitConverter.ToInt16(bs, 0);
                    return true;
                }
            }
            return StringToInt16(input.ToString(), out result);
        }
        public static bool TryParseInt32(object input, out int result)
        {
            if (input is int)
            {
                result = (int)input;
                return true;
            }
            var str = input as string;
            if (str != null)
            {
                return StringToInt32(str, out result);
            }
            var conv = input as IConvertible;
            if (conv != null)
            {
                switch (conv.GetTypeCode())
                {
                    case TypeCode.Boolean:
                        result = conv.ToBoolean(null) ? (int)1 : (int)0;
                        return true;
                    case TypeCode.Empty:
                    case TypeCode.DBNull:
                    case TypeCode.DateTime:
                        result = 0;
                        return false;
                    case TypeCode.Byte: result = conv.ToByte(null); return true;
                    case TypeCode.Char: result = conv.ToChar(null); return true;
                    case TypeCode.Int16: result = conv.ToInt16(null); return true;
                    case TypeCode.Int32: result = conv.ToInt32(null); return true;
                    case TypeCode.Int64:
                        {
                            var a = conv.ToInt64(null);
                            if (a < -2147483648 || a > 2147483647)
                            {
                                result = 0;
                                return false;
                            }
                            result = (int)a;
                            return true;
                        }
                    case TypeCode.SByte: result = (int)conv.ToSByte(null); return true;
                    case TypeCode.Double:
                        {
                            var a = conv.ToDouble(null);
                            if (a < -2147483648 || a > 2147483647)
                            {
                                result = 0;
                                return false;
                            }
                            result = (int)a;
                            return true;
                        }
                    case TypeCode.Single:
                        {
                            var a = conv.ToSingle(null);
                            if (a < -2147483648 || a > 2147483647)
                            {
                                result = 0;
                                return false;
                            }
                            result = (int)a;
                            return true;
                        }
                    case TypeCode.UInt16: result = (int)conv.ToUInt16(null); return true;
                    case TypeCode.UInt32:
                        {
                            var a = conv.ToUInt32(null);
                            if (a > 2147483647)
                            {
                                result = 0;
                                return false;
                            }
                            result = (int)a;
                            return true;
                        }
                    case TypeCode.UInt64:
                        {
                            var a = conv.ToUInt64(null);
                            if (a > 2147483647)
                            {
                                result = 0;
                                return false;
                            }
                            result = (int)a;
                            return true;
                        }
                    case TypeCode.Decimal:
                        {
                            var a = conv.ToDecimal(null);
                            if (a < -2147483648 || a > 2147483647)
                            {
                                result = 0;
                                return false;
                            }
                            result = (int)a;
                            return true;
                        }
                    default:
                        break;
                }
            }
            else if (input == null)
            {
                result = 0;
                return false;
            }
            else if (input is IntPtr)
            {
                var a = ((IntPtr)input).ToInt64();
                if (a < -2147483648 || a > 2147483647)
                {
                    result = 0;
                    return false;
                }
                result = (int)a;
                return true;
            }
            else if (input is UIntPtr)
            {
                var a = ((UIntPtr)input).ToUInt64();
                if (a > 2147483647)
                {
                    result = 0;
                    return false;
                }
                result = (int)a;
                return true;
            }
            else
            {
                var bs = input as byte[];
                if (bs != null)
                {
                    if (bs.Length == 0 || bs.Length > 4)
                    {
                        result = 0;
                        return false;
                    }
                    result = BitConverter.ToInt32(bs, 0);
                    return true;
                }
            }
            return StringToInt32(input.ToString(), out result);
        }
        public static bool TryParseInt64(object input, out long result)
        {
            if (input is long)
            {
                result = (long)input;
                return true;
            }
            var str = input as string;
            if (str != null)
            {
                return StringToInt64(str, out result);
            }
            var conv = input as IConvertible;
            if (conv != null)
            {
                switch (conv.GetTypeCode())
                {
                    case TypeCode.Boolean:
                        result = conv.ToBoolean(null) ? (long)1 : (long)0;
                        return true;
                    case TypeCode.Empty:
                    case TypeCode.DBNull:
                    case TypeCode.DateTime:
                        result = 0;
                        return false;
                    case TypeCode.Byte: result = (long)conv.ToByte(null); return true;
                    case TypeCode.Char: result = (long)conv.ToChar(null); return true;
                    case TypeCode.Int16: result = (long)conv.ToInt16(null); return true;
                    case TypeCode.Int32: result = (long)conv.ToInt32(null); return true;
                    case TypeCode.Int64: result = (long)conv.ToInt64(null); return true;
                    case TypeCode.SByte: result = (long)conv.ToSByte(null); return true;
                    case TypeCode.Double:
                        {
                            var a = conv.ToDouble(null);
                            if (a < -9223372036854775808L || a > 9223372036854775807L)
                            {
                                result = 0;
                                return false;
                            }
                            result = (long)a;
                            return true;
                        }
                    case TypeCode.Single:
                        {
                            var a = conv.ToSingle(null);
                            if (a < -9223372036854775808L || a > 9223372036854775807L)
                            {
                                result = 0;
                                return false;
                            }
                            result = (long)a;
                            return true;
                        }
                    case TypeCode.UInt16: result = (long)conv.ToUInt16(null); return true;
                    case TypeCode.UInt32: result = (long)conv.ToUInt32(null); return true;
                    case TypeCode.UInt64:
                        {
                            var a = conv.ToUInt64(null);
                            if (a > 9223372036854775807L)
                            {
                                result = 0;
                                return false;
                            }
                            result = (long)a;
                            return true;
                        }
                    case TypeCode.Decimal:
                        {
                            var a = conv.ToDecimal(null);
                            if (a < -9223372036854775808L || a > 9223372036854775807L)
                            {
                                result = 0;
                                return false;
                            }
                            result = (long)a;
                            return true;
                        }
                    default:
                        break;
                }
            }
            else if (input == null)
            {
                result = 0;
                return false;
            }
            else if (input is IntPtr)
            {
                result = ((IntPtr)input).ToInt64();
                return true;
            }
            else if (input is UIntPtr)
            {
                var a = ((UIntPtr)input).ToUInt64();
                if (a > 9223372036854775807L)
                {
                    result = 0;
                    return false;
                }
                result = (long)a;
                return true;
            }
            else
            {
                var bs = input as byte[];
                if (bs != null)
                {
                    if (bs.Length == 0 || bs.Length > 8)
                    {
                        result = 0;
                        return false;
                    }
                    result = BitConverter.ToInt64(bs, 0);
                    return true;
                }
            }
            return StringToInt64(input.ToString(), out result);
        }
        public static bool TryParseSByte(object input, out sbyte result)
        {
            if (input is sbyte)
            {
                result = (sbyte)input;
                return true;
            }
            var str = input as string;
            if (str != null)
            {
                return StringToSByte(str, out result);
            }
            var conv = input as IConvertible;
            if (conv != null)
            {
                switch (conv.GetTypeCode())
                {
                    case TypeCode.Boolean:
                        result = conv.ToBoolean(null) ? (sbyte)1 : (sbyte)0;
                        return true;
                    case TypeCode.Empty:
                    case TypeCode.DBNull:
                    case TypeCode.DateTime:
                        result = 0;
                        return false;
                    case TypeCode.Byte:
                        {
                            var a = conv.ToByte(null);
                            if (a > 127)
                            {
                                result = 0;
                                return false;
                            }
                            result = (sbyte)a;
                            return true;
                        }
                    case TypeCode.Char:
                        {
                            var a = conv.ToChar(null);
                            if (a > 127)
                            {
                                result = 0;
                                return false;
                            }
                            result = (sbyte)a;
                            return true;
                        }
                    case TypeCode.Int16:
                        {
                            var a = conv.ToInt16(null);
                            if (a < -128 || a > 127)
                            {
                                result = 0;
                                return false;
                            }
                            result = (sbyte)a;
                            return true;
                        }
                    case TypeCode.Int32:
                        {
                            var a = conv.ToInt32(null);
                            if (a < -128 || a > 127)
                            {
                                result = 0;
                                return false;
                            }
                            result = (sbyte)a;
                            return true;
                        }
                    case TypeCode.Int64:
                        {
                            var a = conv.ToInt64(null);
                            if (a < -128 || a > 127)
                            {
                                result = 0;
                                return false;
                            }
                            result = (sbyte)a;
                            return true;
                        }
                    case TypeCode.SByte:
                        {
                            var a = conv.ToSByte(null);
                            if (a < -128 || a > 127)
                            {
                                result = 0;
                                return false;
                            }
                            result = (sbyte)a;
                            return true;
                        }
                    case TypeCode.Double:
                        {
                            var a = conv.ToDouble(null);
                            if (a < -128 || a > 127)
                            {
                                result = 0;
                                return false;
                            }
                            result = (sbyte)a;
                            return true;
                        }
                    case TypeCode.Single:
                        {
                            var a = conv.ToSingle(null);
                            if (a < -128 || a > 127)
                            {
                                result = 0;
                                return false;
                            }
                            result = (sbyte)a;
                            return true;
                        }
                    case TypeCode.UInt16:
                        {
                            var a = conv.ToUInt16(null);
                            if (a > 127)
                            {
                                result = 0;
                                return false;
                            }
                            result = (sbyte)a;
                            return true;
                        }
                    case TypeCode.UInt32:
                        {
                            var a = conv.ToUInt32(null);
                            if (a > 127)
                            {
                                result = 0;
                                return false;
                            }
                            result = (sbyte)a;
                            return true;
                        }
                    case TypeCode.UInt64:
                        {
                            var a = conv.ToUInt64(null);
                            if (a > 127)
                            {
                                result = 0;
                                return false;
                            }
                            result = (sbyte)a;
                            return true;
                        }
                    case TypeCode.Decimal:
                        {
                            var a = conv.ToDecimal(null);
                            if (a < -128 || a > 127)
                            {
                                result = 0;
                                return false;
                            }
                            result = (sbyte)a;
                            return true;
                        }
                    default:
                        break;
                }
            }
            else if (input == null)
            {
                result = 0;
                return false;
            }
            return StringToSByte(input.ToString(), out result);
        }
        public static bool TryParseUInt16(object input, out ushort result)
        {
            if (input is ushort)
            {
                result = (ushort)input;
                return true;
            }
            var str = input as string;
            if (str != null)
            {
                return StringToUInt16(str, out result);
            }
            var conv = input as IConvertible;
            if (conv != null)
            {
                switch (conv.GetTypeCode())
                {
                    case TypeCode.Boolean:
                        result = conv.ToBoolean(null) ? (ushort)1 : (ushort)0;
                        return true;
                    case TypeCode.Empty:
                    case TypeCode.DBNull:
                    case TypeCode.DateTime:
                        result = 0;
                        return false;
                    case TypeCode.Byte: result = (ushort)conv.ToByte(null); return true;
                    case TypeCode.Char: result = (ushort)conv.ToChar(null); return true;
                    case TypeCode.Int16: result = (ushort)conv.ToInt16(null); return true;
                    case TypeCode.Int32:
                        {
                            var a = conv.ToInt32(null);
                            if (a < 0 || a > 65535)
                            {
                                result = 0;
                                return false;
                            }
                            result = (ushort)a;
                            return true;
                        }
                    case TypeCode.Int64:
                        {
                            var a = conv.ToInt64(null);
                            if (a < 0 || a > 65535)
                            {
                                result = 0;
                                return false;
                            }
                            result = (ushort)a;
                            return true;
                        }
                    case TypeCode.SByte:
                        {
                            var a = conv.ToSByte(null);
                            if (a < 0)
                            {
                                result = 0;
                                return false;
                            }
                            result = (ushort)a;
                            return true;
                        }
                    case TypeCode.Double:
                        {
                            var a = conv.ToDouble(null);
                            if (a < 0 || a > 65535)
                            {
                                result = 0;
                                return false;
                            }
                            result = (ushort)a;
                            return true;
                        }
                    case TypeCode.Single:
                        {
                            var a = conv.ToSingle(null);
                            if (a < 0 || a > 65535)
                            {
                                result = 0;
                                return false;
                            }
                            result = (ushort)a;
                            return true;
                        }
                    case TypeCode.UInt16: result = conv.ToUInt16(null); return true;
                    case TypeCode.UInt32:
                        {
                            var a = conv.ToUInt32(null);
                            if (a > 65535)
                            {
                                result = 0;
                                return false;
                            }
                            result = (ushort)a;
                            return true;
                        }
                    case TypeCode.UInt64:
                        {
                            var a = conv.ToUInt64(null);
                            if (a > 65535)
                            {
                                result = 0;
                                return false;
                            }
                            result = (ushort)a;
                            return true;
                        }
                    case TypeCode.Decimal:
                        {
                            var a = conv.ToDecimal(null);
                            if (a < 0 || a > 65535)
                            {
                                result = 0;
                                return false;
                            }
                            result = (ushort)a;
                            return true;
                        }
                    default:
                        break;
                }
            }
            else if (input == null)
            {
                result = 0;
                return false;
            }
            else
            {
                var bs = input as byte[];
                if (bs != null)
                {
                    if (bs.Length == 0 || bs.Length > 2)
                    {
                        result = 0;
                        return false;
                    }
                    result = BitConverter.ToUInt16(bs, 0);
                    return true;
                }
            }
            return StringToUInt16(input.ToString(), out result);
        }
        public static bool TryParseUInt32(object input, out uint result)
        {
            if (input is uint)
            {
                result = (uint)input;
                return true;
            }
            var str = input as string;
            if (str != null)
            {
                return StringToUInt32(str, out result);
            }
            var conv = input as IConvertible;
            if (conv != null)
            {
                switch (conv.GetTypeCode())
                {
                    case TypeCode.Boolean:
                        result = conv.ToBoolean(null) ? (uint)1 : (uint)0;
                        return true;
                    case TypeCode.Empty:
                    case TypeCode.DBNull:
                    case TypeCode.DateTime:
                        result = 0;
                        return false;
                    case TypeCode.Byte: result = (uint)conv.ToByte(null); return true;
                    case TypeCode.Char: result = (uint)conv.ToChar(null); return true;
                    case TypeCode.Int16:
                        {
                            var a = conv.ToInt16(null);
                            if (a < 0)
                            {
                                result = 0;
                                return false;
                            }
                            result = (uint)a;
                            return true;
                        }
                    case TypeCode.Int32:
                        {
                            var a = conv.ToInt32(null);
                            if (a < 0)
                            {
                                result = 0;
                                return false;
                            }
                            result = (uint)a;
                            return true;
                        }
                    case TypeCode.Int64:
                        {
                            var a = conv.ToInt64(null);
                            if (a < 0 || a > 4294967295)
                            {
                                result = 0;
                                return false;
                            }
                            result = (uint)a;
                            return true;
                        }
                    case TypeCode.SByte:
                        {
                            var a = conv.ToSByte(null);
                            if (a < 0)
                            {
                                result = 0;
                                return false;
                            }
                            result = (uint)a;
                            return true;
                        }
                    case TypeCode.Double:
                        {
                            var a = conv.ToDouble(null);
                            if (a < 0 || a > 4294967295)
                            {
                                result = 0;
                                return false;
                            }
                            result = (uint)a;
                            return true;
                        }
                    case TypeCode.Single:
                        {
                            var a = conv.ToSingle(null);
                            if (a < 0 || a > 4294967295)
                            {
                                result = 0;
                                return false;
                            }
                            result = (uint)a;
                            return true;
                        }
                    case TypeCode.UInt16: result = (uint)conv.ToUInt16(null); return true;
                    case TypeCode.UInt32: result = conv.ToUInt32(null); return true;
                    case TypeCode.UInt64:
                        {
                            var a = conv.ToUInt64(null);
                            if (a > 4294967295)
                            {
                                result = 0;
                                return false;
                            }
                            result = (uint)a;
                            return true;
                        }
                    case TypeCode.Decimal:
                        {
                            var a = conv.ToDecimal(null);
                            if (a < 0 || a > 4294967295)
                            {
                                result = 0;
                                return false;
                            }
                            result = (uint)a;
                            return true;
                        }
                    default:
                        break;
                }
            }
            else if (input == null)
            {
                result = 0;
                return false;
            }
            else if (input is IntPtr)
            {
                var a = ((IntPtr)input).ToInt64();
                if (a < 0 || a > 4294967295)
                {
                    result = 0;
                    return false;
                }
                result = (uint)a;
                return true;
            }
            else if (input is UIntPtr)
            {
                var a = ((UIntPtr)input).ToUInt64();
                if (a > 4294967295)
                {
                    result = 0;
                    return false;
                }
                result = (uint)a;
                return true;
            }
            else
            {
                var bs = input as byte[];
                if (bs != null)
                {
                    if (bs.Length == 0 || bs.Length > 4)
                    {
                        result = 0;
                        return false;
                    }
                    result = BitConverter.ToUInt32(bs, 0);
                    return true;
                }
            }
            return StringToUInt32(input.ToString(), out result);
        }
        public static bool TryParseUInt64(object input, out ulong result)
        {
            if (input is ulong)
            {
                result = (ulong)input;
                return true;
            }
            var str = input as string;
            if (str != null)
            {
                return StringToUInt64(str, out result);
            }
            var conv = input as IConvertible;
            if (conv != null)
            {
                switch (conv.GetTypeCode())
                {
                    case TypeCode.Boolean:
                        result = conv.ToBoolean(null) ? (ulong)1 : (ulong)0;
                        return true;
                    case TypeCode.Empty:
                    case TypeCode.DBNull:
                    case TypeCode.DateTime:
                        result = 0;
                        return false;
                    case TypeCode.Byte: result = (ulong)conv.ToByte(null); return true;
                    case TypeCode.Char: result = (ulong)conv.ToChar(null); return true;
                    case TypeCode.Int16:
                        {
                            var a = conv.ToInt16(null);
                            if (a < 0)
                            {
                                result = 0;
                                return false;
                            }
                            result = (ulong)a;
                            return true;
                        }
                    case TypeCode.Int32:
                        {
                            var a = conv.ToInt32(null);
                            if (a < 0)
                            {
                                result = 0;
                                return false;
                            }
                            result = (ulong)a;
                            return true;
                        }
                    case TypeCode.Int64:
                        {
                            var a = conv.ToInt64(null);
                            if (a < 0)
                            {
                                result = 0;
                                return false;
                            }
                            result = (ulong)a;
                            return true;
                        }
                    case TypeCode.SByte:
                        {
                            var a = conv.ToSByte(null);
                            if (a < 0)
                            {
                                result = 0;
                                return false;
                            }
                            result = (ulong)a;
                            return true;
                        }
                    case TypeCode.Double:
                        {
                            var a = conv.ToDouble(null);
                            if (a < 0)
                            {
                                result = 0;
                                return false;
                            }
                            result = (ulong)a;
                            return true;
                        }
                    case TypeCode.Single:
                        {
                            var a = conv.ToSingle(null);
                            if (a < 0)
                            {
                                result = 0;
                                return false;
                            }
                            result = (ulong)a;
                            return true;
                        }
                    case TypeCode.UInt16: result = (ulong)conv.ToUInt16(null); return true;
                    case TypeCode.UInt32: result = (ulong)conv.ToUInt32(null); return true;
                    case TypeCode.UInt64: result = conv.ToUInt64(null); return true;
                    case TypeCode.Decimal:
                        {
                            var a = conv.ToDecimal(null);
                            if (a < 0)
                            {
                                result = 0;
                                return false;
                            }
                            result = (ulong)a;
                            return true;
                        }
                    default:
                        break;
                }
            }
            else if (input == null)
            {
                result = 0;
                return false;
            }
            else if (input is IntPtr)
            {
                var a = ((IntPtr)input).ToInt64();
                if (a < 0)
                {
                    result = 0;
                    return false;
                }
                result = (ulong)a;
                return true;
            }
            else if (input is UIntPtr)
            {
                result = ((UIntPtr)input).ToUInt64();
                return true;
            }
            else
            {
                var bs = input as byte[];
                if (bs != null)
                {
                    if (bs.Length == 0 || bs.Length > 8)
                    {
                        result = 0;
                        return false;
                    }
                    result = BitConverter.ToUInt64(bs, 0);
                    return true;
                }
            }
            return StringToUInt64(input.ToString(), out result);
        }
        public static bool TryParseGuid(object input, out Guid result)
        {
            if (input is Guid)
            {
                result = (Guid)input;
                return true;
            }
            var str = input as string;
            if (str != null)
            {
                return StringToGuid(str, out result);
            }
            var bs = input as byte[];
            if (bs != null)
            {
                if (bs.Length != 16)
                {
                    result = new Guid(bs);
                    return true;
                }
                else
                {
                    result = default(Guid);
                    return false;
                }
            }
            else if (input == null)
            {
                result = default(Guid);
                return false;
            }
            return StringToGuid(input.ToString(), out result);
        }
        public static bool TryParseString(object input, out string result)
        {
            var str = input as string;
            if (str != null)
            {
                result = str;
            }
            else if (input == null)
            {
                result = null;
            }
            else
            {
                var bs = input as byte[];
                if (bs != null)
                {
                    result = Encoding.UTF8.GetString(bs);
                }
                else
                {
                    result = input.ToString();
                }
            }
            return true;
        }
        public static bool TryParseBytes(object input, out byte[] result)
        {
            if (input is byte[])
            {
                result = (byte[])input;
                return true;
            }
            var str = input as string;
            if (str != null)
            {
                if (str.Length == 0)
                {
                    result = new byte[0];
                    return true;
                }
                result = Encoding.UTF8.GetBytes(str);
                return true;
            }
            var conv = input as IConvertible;
            if (conv != null)
            {
                switch (conv.GetTypeCode())
                {
                    case TypeCode.Empty:
                    case TypeCode.DBNull:
                    case TypeCode.DateTime:
                    case TypeCode.Decimal:
                        result = null;
                        return false;
                    case TypeCode.Boolean: result = BitConverter.GetBytes(conv.ToByte(null)); return true;
                    case TypeCode.Byte: result = BitConverter.GetBytes(conv.ToByte(null)); return true;
                    case TypeCode.Char: result = BitConverter.GetBytes(conv.ToChar(null)); return true;
                    case TypeCode.Int16: result = BitConverter.GetBytes(conv.ToInt16(null)); return true;
                    case TypeCode.Int32: result = BitConverter.GetBytes(conv.ToInt32(null)); return true;
                    case TypeCode.Int64: result = BitConverter.GetBytes(conv.ToInt64(null)); return true;
                    case TypeCode.SByte:
                        result = null;
                        return false;
                    case TypeCode.Double: result = BitConverter.GetBytes(conv.ToDouble(null)); return true;
                    case TypeCode.Single: result = BitConverter.GetBytes(conv.ToSingle(null)); return true;
                    case TypeCode.UInt16: result = BitConverter.GetBytes(conv.ToUInt16(null)); return true;
                    case TypeCode.UInt32: result = BitConverter.GetBytes(conv.ToUInt32(null)); return true;
                    case TypeCode.UInt64: result = BitConverter.GetBytes(conv.ToUInt64(null)); return true;
                    default:
                        break;
                }
            }
            else if (input == null)
            {
                result = null;
                return true;
            }
            result = null;
            return false;
        }
        public static bool TryParseIntPtr(object input, out IntPtr result)
        {
            if (input is IntPtr)
            {
                result = (IntPtr)input;
                return true;
            }
            long l;
            if (TryParseInt64(input, out l))
            {
                result = new IntPtr(l);
                return true;
            }
            result = default(IntPtr);
            return false;
        }
        public static bool TryParseUIntPtr(object input, out UIntPtr result)
        {
            if (input is UIntPtr)
            {
                result = (UIntPtr)input;
                return true;
            }
            ulong l;
            if (TryParseUInt64(input, out l))
            {
                result = new UIntPtr(l);
                return true;
            }
            result = default(UIntPtr);
            return false;
        }


        public static bool TryParseEnum(object input, Type enumType, out Enum result)
        {
            var em = input as Enum;
            if (em != null)
            {
                result = em;
                return true;
            }
            var str = input as string;
            if (str != null)
            {
                return StringToEnum(str, enumType, out result);
            }
            var conv = input as IConvertible;
            if (conv != null)
            {
                switch (conv.GetTypeCode())
                {
                    case TypeCode.Empty:
                    case TypeCode.DBNull:
                    case TypeCode.DateTime:
                    case TypeCode.Decimal:
                    case TypeCode.Boolean:
                        result = null;
                        return false;
                    case TypeCode.Byte: result = (Enum)Enum.ToObject(enumType, conv.ToByte(null)); return true;
                    case TypeCode.Char: result = (Enum)Enum.ToObject(enumType, conv.ToChar(null)); return true;
                    case TypeCode.Int16: result = (Enum)Enum.ToObject(enumType, conv.ToInt16(null)); return true;
                    case TypeCode.Int32: result = (Enum)Enum.ToObject(enumType, conv.ToInt32(null)); return true;
                    case TypeCode.Int64: result = (Enum)Enum.ToObject(enumType, conv.ToInt64(null)); return true;
                    case TypeCode.SByte: result = (Enum)Enum.ToObject(enumType, conv.ToInt64(null)); return true;
                    case TypeCode.Double: result = (Enum)Enum.ToObject(enumType, conv.ToDouble(null)); return true;
                    case TypeCode.Single: result = (Enum)Enum.ToObject(enumType, conv.ToSingle(null)); return true;
                    case TypeCode.UInt16: result = (Enum)Enum.ToObject(enumType, conv.ToUInt16(null)); return true;
                    case TypeCode.UInt32: result = (Enum)Enum.ToObject(enumType, conv.ToUInt32(null)); return true;
                    case TypeCode.UInt64: result = (Enum)Enum.ToObject(enumType, conv.ToUInt64(null)); return true;
                    default:
                        break;
                }
            }
            else if (input == null)
            {
                result = null;
                return false;
            }
            return StringToEnum(input.ToString(), enumType, out result);
        }

        public static bool TryParseEnum<T>(object input, out T result)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            if (typeof(T).IsEnum == false)
            {
                throw new ArgumentOutOfRangeException("T", "类型不是枚举");
            }
            if (input is Enum)
            {
                result = (T)input;
                return true;
            }
            var str = input as string;
            if (str != null)
            {
                return Enum.TryParse<T>(str, out result);
            }
            if (input == null)
            {
                result = default(T);
                return false;
            }
            switch (Type.GetTypeCode(input.GetType()).GetTypeCode())
            {
                case TypeCode.Empty:
                case TypeCode.DBNull:
                case TypeCode.DateTime:
                case TypeCode.Decimal:
                case TypeCode.Boolean:
                    result = default(T);
                    return false;
                case TypeCode.Byte: result = (T)input; return true;
                case TypeCode.Char: result = (T)input; return true;
                case TypeCode.Int16: result = (T)input; return true;
                case TypeCode.Int32: result = (T)input; return true;
                case TypeCode.Int64: result = (T)input; return true;
                case TypeCode.SByte: result = (T)input; return true;
                case TypeCode.Double: result = (T)input; return true;
                case TypeCode.Single: result = (T)input; return true;
                case TypeCode.UInt16: result = (T)input; return true;
                case TypeCode.UInt32: result = (T)input; return true;
                case TypeCode.UInt64: result = (T)input; return true;
                default:
                    break;
            }
            return Enum.TryParse<T>(input.ToString(), out result);
        }

        public static bool TryParseObject(object input, Type outputType, out object result)
        {
            return TypesHelper.GetTypeInfo(outputType).TryParse(input, out result);
        }
        public static LiteracyTryParse CreateDelegate(Type outputType)
        {
            return TypesHelper.GetTypeInfo(outputType).TryParse;
        }




        #region StringToAny
        public static bool StringToBoolean(string input, out bool value)
        {
            if (input == null)
            {
                value = false;
                return false;
            }
            else
            {
                switch (input.Length)
                {
                    case 1:
                        switch (input[0])
                        {
                            case '1':
                            case 'T':
                            case 't':
                            case '对':
                            case '對':
                            case '真':
                            case '是':
                                value = true;
                                return true;
                            case '0':
                            case 'F':
                            case 'f':
                            case '错':
                            case '錯':
                            case '假':
                            case '否':
                                value = false;
                                return true;
                            default:
                                break;
                        }
                        break;
                    case 2:
                        if (input[0] == '-' && input[1] == '1')
                        {
                            value = true;
                            return true;
                        }
                        break;
                    case 4:
                        if (input.Equals("true", StringComparison.OrdinalIgnoreCase))
                        {
                            value = true;
                            return true;
                        }
                        break;
                    case 5:
                        if (input.Equals("false", StringComparison.OrdinalIgnoreCase))
                        {
                            value = true;
                            return true;
                        }
                        break;
                    default:
                        break;
                }
            }
            value = false;
            return false;
        }
        public static bool StringToDateTime(string input, out DateTime value)
        {
            return DateTime.TryParse(input, out  value);
        }
        public static bool StringToTimeSpan(string input, out TimeSpan value)
        {
            return TimeSpan.TryParse(input, out  value);
        }
        public static bool StringToDateTime(string input, string format, out DateTime value)
        {
            return DateTime.TryParseExact(input, format, null, DateTimeStyles.None, out  value);
        }
        public static bool StringToTimeSpan(string input, string format, out TimeSpan value)
        {
            return TimeSpan.TryParseExact(input, format, null, out  value);
        }
        public static bool StringToByte(string input, out byte value)
        {
            if (byte.TryParse(input, out value))
            {
                return true;
            }
            if (HexString(ref input))
            {
                return byte.TryParse(input, _hexstyle, _numformat, out value);
            }
            value = 0;
            return false;
        }
        public static bool StringToDecimal(string input, out decimal value)
        {
            if (decimal.TryParse(input, out value))
            {
                return true;
            }
            if (HexString(ref input))
            {
                return decimal.TryParse(input, _hexstyle, _numformat, out value);
            }
            value = 0;
            return false;
        }
        public static bool StringToDouble(string input, out double value)
        {
            if (double.TryParse(input, out value))
            {
                return true;
            }
            if (HexString(ref input))
            {
                return double.TryParse(input, _hexstyle, _numformat, out value);
            }
            value = 0;
            return false;
        }
        public static bool StringToInt16(string input, out short value)
        {
            if (short.TryParse(input, out value))
            {
                return true;
            }
            if (HexString(ref input))
            {
                return short.TryParse(input, _hexstyle, _numformat, out value);
            }
            value = 0;
            return false;
        }
        public static bool StringToInt32(string input, out int value)
        {
            if (int.TryParse(input, out value))
            {
                return true;
            }
            if (HexString(ref input))
            {
                return int.TryParse(input, _hexstyle, _numformat, out value);
            }
            value = 0;
            return false;
        }
        public static bool StringToInt64(string input, out long value)
        {
            if (long.TryParse(input, out value))
            {
                return true;
            }
            if (HexString(ref input))
            {
                return long.TryParse(input, _hexstyle, _numformat, out value);
            }
            value = 0;
            return false;
        }
        public static bool StringToSByte(string input, out sbyte value)
        {
            if (sbyte.TryParse(input, out value))
            {
                return true;
            }
            if (HexString(ref input))
            {
                return sbyte.TryParse(input, _hexstyle, _numformat, out value);
            }
            value = 0;
            return false;
        }
        public static bool StringToSingle(string input, out float value)
        {
            if (float.TryParse(input, out value))
            {
                return true;
            }
            if (HexString(ref input))
            {
                return float.TryParse(input, _hexstyle, _numformat, out value);
            }
            value = 0;
            return false;
        }
        public static bool StringToUInt16(string input, out ushort value)
        {
            if (ushort.TryParse(input, out value))
            {
                return true;
            }
            if (HexString(ref input))
            {
                return ushort.TryParse(input, _hexstyle, _numformat, out value);
            }
            value = 0;
            return false;
        }
        public static bool StringToUInt32(string input, out uint value)
        {
            if (uint.TryParse(input, out value))
            {
                return true;
            }
            if (HexString(ref input))
            {
                return uint.TryParse(input, _hexstyle, _numformat, out value);
            }
            value = 0;
            return false;
        }
        public static bool StringToUInt64(string input, out ulong value)
        {
            if (ulong.TryParse(input, out value))
            {
                return true;
            }
            if (HexString(ref input))
            {
                return ulong.TryParse(input, _hexstyle, _numformat, out value);
            }
            value = 0;
            return false;
        }
        public static bool StringToGuid(string input, out Guid value)
        {
            if (input == null)
            {
                value = Guid.Empty;
                return false;
            }
            if (input.Length > 30)
            {
#if NF2
                try 
	            {	
                    value = new Guid(input);
                    return true;
	            }
	            catch { }
#else
                if (Guid.TryParse(input, out value))
                {
                    return true;
                }
#endif
            }
            else
            {
                try
                {
                    var bs = Convert.FromBase64String(input);
                    if (bs.Length != 16)
                    {
                        value = Guid.Empty;
                        return false;
                    }
                    value = new Guid(bs);
                    return true;
                }
                catch { }
            }
            value = Guid.Empty;
            return false;
        }
        public static bool StringToEnum(string input, Type enumType, out Enum value)
        {
            try
            {
                value = (Enum)Enum.Parse(enumType, input, false);
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }
        private static bool HexString(ref string s)
        {
            if (s == null)
            {
                return false;
            }
            var c = s[0];
            if (char.IsWhiteSpace(c)) //有空格去空格
            {
                s = s.TrimStart();
            }
            if (s.Length > 2) //判断是否是0x 或者 &h 开头
            {
                switch (c)
                {
                    case '0':
                        switch (s[1])
                        {
                            case 'x':
                            case 'X':
                                s = s.Remove(0, 2);
                                return true;
                            default:
                                return true;
                        }
                    case '&':
                        switch (s[1])
                        {
                            case 'h':
                            case 'H':
                                s = s.Remove(0, 2);
                                return true;
                            default:
                                return false;
                        }
                    default:
                        return false;
                }
            }
            return false;
        }
        private static bool TryToHex(string s, out decimal value, decimal max)
        {
            if (s == null)
            {
                value = 0;
                return false;
            }
            if (char.IsWhiteSpace(s, 0))
            {
                s = s.Trim();
            }
            if (s.Length > 2)
            {
                switch (s[0])
                {
                    case '0':
                        switch (s[1])
                        {
                            case 'x':
                            case 'X':
                                s = s.Remove(0, 2);
                                break;
                        }
                        break;
                    case '&':
                        switch (s[1])
                        {
                            case 'h':
                            case 'H':
                                s = s.Remove(0, 2);
                                break;
                            default:
                                value = 0;
                                return false;
                        }
                        break;
                }
            }
            decimal i;
            if (decimal.TryParse(s, NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo, out i))
            {
                if (i <= max)
                {
                    value = i;
                    return true;
                }
            }
            value = 0;
            return false;
        }
        private static readonly NumberStyles _hexstyle = NumberStyles.HexNumber;
        static readonly NumberFormatInfo _numformat = NumberFormatInfo.InvariantInfo;
        #endregion
    }
}
