using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
namespace blqw
{
    public class StringConverter : BaseConverter<string>
    {
        static readonly NumberStyles _hexstyle = NumberStyles.HexNumber;
        static readonly NumberFormatInfo _numformat = NumberFormatInfo.InvariantInfo;
        /// <summary> 检查字符串是否有可能是16进制的数字
        /// </summary>
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
                        return c >= '0' && c <= '9';
                }
            }
            return c >= '0' && c <= '9'; ;
        }
        public override bool TryToBoolean(string input, out bool value)
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
        public override bool TryToBytes(string input, out byte[] value)
        {
            throw new NotImplementedException();
        }
        public override bool TryToChar(string input, out char value)
        {
            throw new NotImplementedException();
        }
        public override bool TryToDateTime(string input, out DateTime value)
        {
            return DateTime.TryParse(input, out  value);
        }
        public override bool TryToTimeSpan(string input, out TimeSpan value)
        {
            return TimeSpan.TryParse(input, out  value);
        }
        public override bool TryToDateTime(string input, string format, out DateTime value)
        {
            return DateTime.TryParseExact(input, format, null, DateTimeStyles.None, out  value);
        }
        public override bool TryToTimeSpan(string input, string format, out TimeSpan value)
        {
            return TimeSpan.TryParseExact(input, format, null, out  value);
        }
        public override bool TryToByte(string input, out byte value)
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
        public override bool TryToDecimal(string input, out decimal value)
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
        public override bool TryToDouble(string input, out double value)
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
        public override bool TryToInt16(string input, out short value)
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
        public override bool TryToInt32(string input, out int value)
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
        public override bool TryToInt64(string input, out long value)
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
        public override bool TryToSByte(string input, out sbyte value)
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
        public override bool TryToSingle(string input, out float value)
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
        public override bool TryToUInt16(string input, out ushort value)
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
        public override bool TryToUInt32(string input, out uint value)
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
        public override bool TryToUInt64(string input, out ulong value)
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
        public override bool TryToGuid(string input, out Guid value)
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
        public override bool TryToEnum<TEnum>(string input, out TEnum value)
        {
            return Enum.TryParse<TEnum>(input, out value);
        }
        public override bool TryToEnum(string input, Type enumType, out Enum value)
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
    }
}