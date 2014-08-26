using System;

namespace blqw
{
    public delegate bool TryConvert<in TInput, TOutput>(TInput input, out TOutput result);
    public delegate bool TryConvert<in TInput, TType, TOutput>(TInput input, TType outputType, out TOutput result);
    public interface IConverter<TInput>
    {
        bool TryToBoolean(TInput input, out bool value);
        bool TryToByte(TInput input, out byte value);
        bool TryToBytes(TInput input, out byte[] value);
        bool TryToChar(TInput input, out char value);
        bool TryToDateTime(TInput input, out DateTime value);
        bool TryToTimeSpan(TInput input, out TimeSpan value);
        bool TryToDateTime(TInput input, string format, out DateTime value);
        bool TryToTimeSpan(TInput input, string format, out TimeSpan value);
        bool TryToDecimal(TInput input, out decimal value);
        bool TryToDouble(TInput input, out double value);
        bool TryToInt16(TInput input, out short value);
        bool TryToInt32(TInput input, out int value);
        bool TryToInt64(TInput input, out long value);
        bool TryToSByte(TInput input, out sbyte value);
        bool TryToSingle(TInput input, out float value);
        bool TryToString(TInput input, out string value);
        bool TryToUInt16(TInput input, out ushort value);
        bool TryToUInt32(TInput input, out uint value);
        bool TryToUInt64(TInput input, out ulong value);
        bool TryToGuid(TInput input, out Guid value);
        bool TryToEnum(TInput input, Type enumType, out Enum value);

        bool TryToType(TInput input, Type outputType, out object value);

        TryConvert<TInput, object> CreateDelegate(Type outputType);
    }
}
