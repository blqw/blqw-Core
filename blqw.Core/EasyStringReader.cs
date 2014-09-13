using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace blqw
{
    /// <summary> 读取模式
    /// </summary>
    [Flags, Serializable]
    public enum ReadMode
    {
        None = 0,
        /// <summary> 移除起始字符
        /// </summary>
        RemoveStart = 1 << 0,
        /// <summary> 移除终止字符
        /// </summary>
        RemoveStop = 1 << 1,
        /// <summary> 移除起始字符和终止字符
        /// </summary>
        RemoveAll = RemoveStart | RemoveStop,
        /// <summary> 读取字符中保留终止字符
        /// ,对于ReadNext(ReadMode mode)来说 这个枚举指定是否从当前位置搜索,还是从下一个位置开始搜索
        /// </summary>
        ReserveStop = 1 << 2,
        /// <summary> 允许读取到字符串结束
        /// </summary>
        AllowToEnd = 1 << 3,
        /// <summary> 解析转义符
        /// </summary>
        ParseEscapeChar = 1 << 4,
        /// <summary> 解释Unicode字符
        /// </summary>
        ParseUnicode = 1 << 5,
        /// <summary> 解析转义符和Unicode字符
        /// </summary>
        ParseAll = ParseEscapeChar | ParseUnicode,
        /// <summary> 跳过位于起始处的空白字符
        /// </summary>
        SkipStartWrite = 1 << 6,
        /// <summary> 跳过位于起始处的回车换行符
        /// </summary>
        SkipStartCrlf = 1 << 7,
        /// <summary> 跳过位于起始处的空白字符和回车换行符
        /// </summary>
        SkipAll = SkipStartWrite | SkipStartCrlf,
    }

    [DebuggerDisplay("长度:{Length} 当前位置:{Position} 字符:{DebugCurrent}")]
    [Obsolete("目前设计有缺陷,有时间再改")]
    public sealed class EasyStringReader
    {
        #region 静态

        /// <summary> Unicode编码
        /// </summary>
        private readonly static sbyte[] _UnicodeMaps = InitUnicodeMaps();
        /// <summary> 初始化 _UnicodeMaps 对象中的数据
        /// </summary>
        private static sbyte[] InitUnicodeMaps()
        {
            var arr = new sbyte[123];
            for (int i = 0; i < 123; i++)
            {
                arr[i] = -1;
            }

            for (char c = 'a'; c <= 'z'; c++)
            {
                arr[c] = (sbyte)(c - 'a' + 10);
            }
            for (char c = 'A'; c <= 'Z'; c++)
            {
                arr[c] = (sbyte)(c - 'A' + 10);
            }

            for (char c = '0'; c <= '9'; c++)
            {
                arr[c] = (sbyte)(c - '0');
            }
            return arr;
        }

        /// <summary> 字符标记
        /// <para>1 空白</para>
        /// <para>2 回车换行</para>
        /// <para>4 转义符</para>
        /// </summary>
        private static int[] _Flags = InitFlags();
        /// <summary> 初始化 _Flags 对象中的数据
        /// </summary>
        /// <returns></returns>
        private static int[] InitFlags()
        {
            var arr = new int[char.MaxValue];
            //空白
            for (int i = char.MinValue; i <= char.MaxValue; i++)
            {
                if (char.IsWhiteSpace((char)i))
                {
                    arr[i] |= 1;
                }
            }

            //回车换行
            arr['\r'] |= 2;
            arr['\n'] |= 2;

            //转义符
            arr['t'] |= 4;
            arr['r'] |= 4;
            arr['n'] |= 4;
            arr['f'] |= 4;
            arr['0'] |= 4;
            arr['"'] |= 4;
            arr['u'] |= 4;
            arr['x'] |= 4;
            arr['\''] |= 4;
            arr['\\'] |= 4;
            return arr;
        }

        #endregion
        
        /// <summary> 构造函数,初始化EasyStringReader对象
        /// </summary>
        /// <param name="str">待处理字符串</param>
        public EasyStringReader(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }
            RawString = str;
            _rawCharArray = RawString.ToCharArray();
            Length = RawString.Length;
            _end = Length - 1;
            Position = -1;
        }


        #region 私有

        /// <summary> 用以提供DebuggerDisplay特性展示用的数据
        /// </summary>
        private char? DebugCurrent
        {
            get
            {
                return (Position == -1) ? null : new char?(Current);
            }
        }
        /// <summary> 字符串结束位置,他的值等于 Length - 1
        /// </summary>
        private int _end;
        /// <summary> 字符串中的char数组
        /// </summary>
        private char[] _rawCharArray;

        /// <summary> 验证2个ReadMode枚举是否有and关系
        /// </summary>
        /// <param name="mode1">第一个枚举</param>
        /// <param name="mode2">第二个枚举</param>
        /// <returns></returns>
        private bool CheckAnd(ReadMode mode1, ReadMode mode2)
        {
            return (mode1 & mode2) != 0;
        }

        private sealed class InnerMethod
        {
            public char[] CharArray;
            public char StartChar;
            public char StopChar;
            public int Mode;
            public Func<char, char> GetStopChar;

            public bool EqualsStopChar(char c)
            {
                return StopChar == c;
            }

            public bool ExistsChar(char c)
            {
                var length = CharArray.Length;
                for (int i = 0; i < length; i++)
                {
                    if (CharArray[i] == c)
                    {
                        return true;
                    }
                }
                return false;
            }

            public Func<char, bool> Start(char c)
            {
                switch (Mode)
                {
                    case 0:
                        if (c == StartChar)
                        {
                            return EqualsStopChar;
                        }
                        break;
                    case 1:
                        if (ExistsChar(c))
                        {
                            StopChar = c;
                            return EqualsStopChar;
                        }
                        break;
                    case 2:
                        if (ExistsChar(c))
                        {
                            if (GetStopChar != null)
                            {
                                StopChar = GetStopChar(c);
                                return EqualsStopChar;
                            }
                            return ExistsChar;
                        }
                        break;
                }
                return null;
            }
        }

        private InnerMethod _inner = new InnerMethod();

        private IEnumerable<string> ReadParseString(Func<char, bool> stopCharMatch, ReadMode mode)
        {
            var index = Position;
            do
            {
                char curr;
                if (Current == '\\')
                {
                    if (index < Position)
                    {
                        yield return new string(_rawCharArray, index, Position - index);
                        index = Position;
                    }
                    if (ReadNext() == false && (_Flags[Current] & 4) == 0)
                    {
                        throw new FormatException("错误的转义字符串");
                    }
                    switch (Current)
                    {
                        case 't':
                            curr = '\t';
                            index += 2;
                            break;
                        case 'n':
                            curr = '\n';
                            index += 2;
                            break;
                        case 'r':
                            curr = '\r';
                            index += 2;
                            break;
                        case '0':
                            curr = '\0';
                            index += 2;
                            break;
                        case 'f':
                            curr = '\f';
                            index += 2;
                            break;
                        case 'u':
                            var i = ReadUnicode();
                            if (i > -1)
                            {
                                curr = (char)i;
                                index += 6;
                            }
                            else
                            {
                                Position--;
                                curr = '\\';
                            }
                            break;
                        case 'x':
                        default:
                            curr = Current;
                            break;
                    }
                }
                else
                {
                    curr = Current;
                }

                if (stopCharMatch(curr))
                {
                    if (index < Position)
                    {
                        yield return new string(_rawCharArray, index, Position - index);
                    }
                    if (CheckAnd(mode, ReadMode.RemoveStop) == false)
                    {
                        yield return curr.ToString();
                    }
                    if (CheckAnd(mode, ReadMode.ReserveStop) == false)
                    {
                        ReadNext();
                    }
                    break;
                }
                else if (curr != Current)
                {
                    yield return curr.ToString();
                }
            } while (ReadNext());
        }

        private int ReadUnicode()
        {
            sbyte n1 = 0, n2 = 0, n3 = 0, n4 = 0;
            if (ReadNext() == false
                || ((Current > 122 || (n1 = _UnicodeMaps[Current]) == -1)
                && (Position -= 1) > -100))
            {
                return -1;
            }

            if (ReadNext() == false
                || ((Current > 122 || (n2 = _UnicodeMaps[Current]) == -1)
                && (Position -= 2) > -100))
            {
                return -1;
            }
            if (ReadNext() == false
                || ((Current > 122 || (n3 = _UnicodeMaps[Current]) == -1)
                && (Position -= 3) > -100))
            {
                return -1;
            }
            if (ReadNext() == false
                || ((Current > 122 || (n4 = _UnicodeMaps[Current]) == -1)
                && (Position -= 4) > -100))
            {
                return -1;
            }
            return (n1 * 0x1000 + n2 * 0x100 + n3 * 0x10 + n4);
        }

        #endregion

        /// <summary> 源字符串
        /// </summary>
        public string RawString { get; private set; }
        /// <summary> 字符串长度
        /// </summary>
        public int Length { get; private set; }
        /// <summary> 当前位置
        /// </summary>
        public int Position { get; set; }
        /// <summary> 当前字符
        /// </summary>
        public char Current { get { return _rawCharArray[Position]; } }
        /// <summary> 是否已经到结尾
        /// </summary>
        public bool IsEnd
        {
            get
            {
                if (Position < _end)
                {
                    return false;
                }
                else if (Position == _end)
                {
                    return true;
                }
                else
                {
                    Position = _end;
                    return true;
                }
            }
        }

        /// <summary> 读取从开始字符到终止字符之间的数据,如果下一个有效字符不是开始字符,将抛出异常
        /// </summary>
        /// <param name="start">开始字符</param>
        /// <param name="stop">终止字符</param>
        /// <param name="mode">读取模式</param>
        public string ReadStartToStop(char start, char stop, ReadMode mode = ReadMode.SkipAll| ReadMode.RemoveAll)
        {
            _inner.StartChar = start;
            _inner.StopChar = stop;
            _inner.Mode = 0;
            return ReadStartToStop(_inner.Start, mode);
        }

        /// <summary> 读取从开始字符到终止字符之间的数据,如果下一个有效字符不在字符集中,将抛出异常
        /// </summary>
        /// <param name="start">开始字符集</param>
        /// <param name="sameStart">终止字符是否与开始字符一致,为true则终止字符和开始字符一致,为false,则终止字符依然使用start字符集</param>
        /// <param name="mode">读取模式</param>
        public string ReadStartToStop(char[] start, bool sameStart, ReadMode mode = ReadMode.SkipAll| ReadMode.RemoveAll)
        {
            _inner.CharArray = start;
            _inner.Mode = sameStart ? 1 : 2;
            return ReadStartToStop(_inner.Start, mode);
        }

        /// <summary> 读取从开始字符到终止字符之间的数据,如果下一个有效字符不在字符集中,将抛出异常
        /// </summary>
        /// <param name="start">开始字符集</param>
        /// <param name="getStopChar">根据匹配的开始字符,返回结束字符的委托</param>
        /// <param name="mode">读取模式</param>
        public string ReadStartToStop(char[] start, Func<char, char> getStopChar, ReadMode mode = ReadMode.SkipAll| ReadMode.RemoveAll)
        {
            _inner.CharArray = start;
            _inner.Mode = 2;
            _inner.GetStopChar = getStopChar;
            return ReadStartToStop(_inner.Start, mode);
        }

        /// <summary> 读取从开始字符到终止字符之间的数据,如果下一个有效字符不在字符集中,将抛出异常
        /// </summary>
        /// <param name="startCharMatch">开始字符匹配委托,匹配成功返回终止字符匹配委托</param>
        /// <param name="mode">读取模式</param>
        public string ReadStartToStop(Func<char, Func<char, bool>> startCharMatch, ReadMode mode = ReadMode.SkipAll| ReadMode.RemoveAll)
        {
            Func<char, bool> stopFunc;
            if (ReadNext(mode | ReadMode.ReserveStop) == false || (stopFunc = startCharMatch(Current)) == null)
            {
                throw new NotSupportedException("没有找到匹配的字符");
            }
            if (CheckAnd(mode, ReadMode.RemoveStart))
            {
                ReadNext();
                return ReadToStop(stopFunc, mode ^ ReadMode.RemoveStart);
            }
            else
            {
                var c = Current;
                ReadNext();
                return c + ReadToStop(stopFunc, mode);
            }
        }

        /// <summary> 读取从当前位置到终止字符之间的数据
        /// </summary>
        /// <param name="stop">终止字符</param>
        /// <param name="mode">读取模式</param>
        public string ReadToStop(char stop, ReadMode mode = ReadMode.SkipAll| ReadMode.RemoveStop | ReadMode.AllowToEnd)
        {
            _inner.StopChar = stop;
            return ReadToStop(_inner.EqualsStopChar, mode);
        }

        /// <summary> 读取从当前位置到终止字符之间的数据
        /// </summary>
        /// <param name="stop">终止字符集</param>
        /// <param name="mode">读取模式</param>
        public string ReadToStop(char[] stop, ReadMode mode = ReadMode.SkipAll| ReadMode.RemoveStop| ReadMode.AllowToEnd)
        {
            _inner.CharArray = stop;
            return ReadToStop(_inner.ExistsChar, mode);
        }

        /// <summary> 读取从当前位置到终止字符之间的数据
        /// </summary>
        /// <param name="stop">终止字符</param>
        /// <param name="mode">读取模式</param>
        public string ReadToStop(Func<char, bool> stopCharMatch, ReadMode mode = ReadMode.SkipAll| ReadMode.RemoveStop| ReadMode.AllowToEnd)
        {
            ReadNext(mode | ReadMode.ReserveStop);

            int startIndex = Position;
            int stopIndex;
            var parse = 0 != (mode & ReadMode.ParseAll);

            do
            {
                if (parse && Current == '\\')
                {
                    var str = new QuickStringWriter();
                    str.Append(_rawCharArray, startIndex, Position - startIndex);
                    foreach (var s in ReadParseString(stopCharMatch, mode))
                    {
                        str.Append(s);
                    }
                    return str.ToString();
                }
                if (stopCharMatch(Current))
                {
                    if (CheckAnd(mode, ReadMode.RemoveStop))
                    {
                        stopIndex = Position;
                    }
                    else
                    {
                        stopIndex = Position + 1;
                    }
                    if (CheckAnd(mode, ReadMode.ReserveStop) == false)
                    {
                        ReadNext();
                    }
                    if (CheckAnd(mode, ReadMode.RemoveStart))
                    {
                        startIndex++;
                    }
                    return new string(_rawCharArray, startIndex, stopIndex - startIndex);
                }
            } while (ReadNext());

            if ((mode & ReadMode.AllowToEnd) == 0)
            {
                throw new NotSupportedException("已达字符串结尾");
            }
            else
            {
                return new string(_rawCharArray, startIndex, Length - startIndex);
            }
        }

        /// <summary> 读取一行文本,从当前位置一直读取到 回车或换行符 终止 ,不返回回车换行符
        /// </summary>
        /// <returns></returns>
        public string ReadLine()
        {
            var str = ReadToStop(new[] { '\n', '\r' }, ReadMode.ReserveStop | ReadMode.RemoveStop | ReadMode.AllowToEnd);
            if (IsEnd == false)
            {
                if (Current == '\r')
                {
                    ReadNext();
                    if (Current == '\n')
                    {
                        ReadNext();
                    }
                }
                else if (Current == '\n')
                {
                    ReadNext();
                }
            }
            return str;
        }

        /// <summary> 读取并移动到字符串的下一个位置,返回是否成功,如成功后可在Current中获取字符
        /// </summary>
        public bool ReadNext()
        {
            return ReadNext(ReadMode.None);
        }

        /// <summary> 读取并移动到字符串的下一个位置,返回是否成功,如成功后可在Current中获取字符
        /// </summary>
        /// <param name="mode">读取模式,默认跳过空白和回车换行符</param>
        public bool ReadNext(ReadMode mode)
        {
            if (IsEnd)
            {
                return false;
            }

            int flag;

            switch (mode & ReadMode.SkipAll)
            {
                case ReadMode.SkipStartWrite:
                    flag = 1;
                    break;
                case ReadMode.SkipStartCrlf:
                    flag = 2;
                    break;
                case ReadMode.SkipAll:
                    flag = 3;
                    break;
                default:
                    if (Position == -1 || CheckAnd(mode, ReadMode.ReserveStop) == false)
                    {
                        Position++;
                    }
                    return true;
            }
            if (Position == -1 || CheckAnd(mode, ReadMode.ReserveStop) == false)
            {
                Position++;
            }
            for (int i = Position; i < Length; i++)
            {
                if ((_Flags[_rawCharArray[i]] & flag) == 0)
                {
                    Position = i;
                    return true;
                }
            }
            Position = _end;
            return false;
        }

        /// <summary> 跳过所有空白字符
        /// </summary>
        /// <param name="checkCurrent">是否检查当前字符,如果为false,则从下一个字符开始判断</param>
        /// <returns></returns>
        public bool SkipWhiteSpace(bool checkCurrent)
        {
            if (checkCurrent)
            {
                return ReadNext(ReadMode.SkipAll | ReadMode.ReserveStop);
            }
            else
            {
                return ReadNext(ReadMode.SkipAll);
            }
        }

        /// <summary> 读取指定个数的字符
        /// <para>如果剩余字符串不足,则判断读取模式,如果不允许读取到字符结尾,则抛出异常,否则返回全部字符串</para>
        /// </summary>
        /// <param name="count">字符个数</param>
        /// <param name="mode">读取模式</param>
        /// <returns></returns>
        public string Read(int count, ReadMode mode = ReadMode.SkipAll)
        {
            if (count > Length - Position && (mode & ReadMode.AllowToEnd) == 0)
            {
                throw new NotSupportedException("已达字符串结尾");
            }
            else if (mode == ReadMode.None)
            {
                var position = Position;
                Position = _end;
                return new string(_rawCharArray, position, Length - position);
            }
            int i = 0;
            Func<char, bool> func = c => ++i == count;
            return ReadToStop(func, mode);
        }

        /// <summary> 读取剩余所有字符串
        /// </summary>
        /// <param name="mode">读取模式</param>
        public string ReadToEnd(ReadMode mode = ReadMode.SkipAll)
        {
            return Read(Length - Position, mode);
        }
    }
}
