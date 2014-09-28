/*
 * 名称:Security
 * 依赖:
 * 功能:加密解密字符串
 * 作者:冰麟轻武
 * 日期:2012年1月31日 05:20:49
 * 版本:1.1
 * 最后更新:2012年2月4日 23:42:35
 * DOTO:性能还可以提高
 */

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace blqw
{
    public class Security
    {
        public static byte[] ToByte(string str)
        {
            if (str == null || str.Length == 0)
            {
                return new byte[8];
            }
            str = Convert.ToBase64String(Encoding.ASCII.GetBytes(str));
            byte[] arr = Encoding.UTF8.GetBytes(str);
            var length = (arr[0] * arr[1]) % (1 << 2) + 4;
            for (int i = 0; i < length; i++)
            {
                str = Convert.ToBase64String(Encoding.ASCII.GetBytes(str.Remove(0, i)));
            }
            arr = Encoding.UTF8.GetBytes(str);
            var index = arr[length % 10] % (arr.Length - 8);
            byte[] bytes = new byte[8];
            Array.Copy(arr, index, bytes, 0, 8);
            return bytes;
        }



        //默认密钥向量
        static byte[] _IV = { 0xff, 0x2d, 0x25, 0x7a, 0x88, 0x02, 0x9d, 0x3c };

        // 默认DES密钥
        static byte[] _Key = { 0x88, 0x02, 0x9d, 0x3c, 0xff, 0x2d, 0x25, 0x7a };


        private static void TryException(string str, string name)
        {
            if (str == null || str.Length != 8)
            {
                throw new CryptographicException("使用了无效的" + name);
            }
        }


        private static string Encrypt(byte[] input, byte[] key, byte[] iv)
        {
            if (key == null) TryException(null, "密钥");
            if (iv == null) TryException(null, "向量");
            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            using (MemoryStream mStream = new MemoryStream())
            using (CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(key, iv), CryptoStreamMode.Write))
            {
                cStream.Write(input, 0, input.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
        }
        private static string Decrypt(byte[] input, byte[] key, byte[] iv)
        {
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            using (MemoryStream mStream = new MemoryStream())
            using (CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(key, iv), CryptoStreamMode.Write))
            {
                cStream.Write(input, 0, input.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
        }
        private static string[] CreateKeyAndIv(string str)
        {
            string[] arr = new string[2];
            StringBuilder sb = new StringBuilder(8);
            var e = str.GetEnumerator();
            e.Reset();
            while (sb.Length < 8)
            {
                if (e.MoveNext() == false)
                {
                    e.Reset();
                    e.MoveNext();
                }
                sb.Append(e.Current);
            }
            arr[0] = sb.ToString();
            sb.Remove(0, sb.Length);

            while (sb.Length < 8)
            {
                if (e.MoveNext() == false)
                {
                    e.Reset();
                    e.MoveNext();
                }
                sb.Append(e.Current);
            }
            arr[1] = sb.ToString();
            return arr;
        }
        private static byte[] CreateKeyOrIv(string str)
        {
            var arr = Encoding.Unicode.GetBytes(str);
            var s = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                s[i] = arr[i * 2];
            }
            return s;
        }

        /// <summary> DES加密字符串
        /// </summary>
        /// <param name="input">待加密的字符串</param>
        /// <param name="key">加密密钥,要求为8位</param>
        /// <param name="iv">向量,要求为8位</param>
        public static string Encrypt(string input, byte[] key, byte[] iv)
        {
            return Encrypt(Encoding.UTF8.GetBytes(input), key, iv);
        }
        /// <summary> DES加密字符串
        /// </summary>
        /// <param name="input">待加密的字符串</param>
        /// <param name="key">加密密钥,要求为8位</param>
        /// <param name="iv">向量,要求为8位</param>
        public static string Encrypt(string input, string key = null, string iv = null)
        {
            if (string.IsNullOrEmpty(input)) return "";
            if (input.Trim() == "") return input;
            byte[] bkey = null;
            byte[] biv = null;

            if (key != null)
            {
                TryException(key, "密钥");
                bkey = CreateKeyOrIv(key);
            }
            if (iv != null)
            {
                TryException(iv, "向量");
                biv = CreateKeyOrIv(iv);
            }


            return Encrypt(Encoding.UTF8.GetBytes(input), bkey ?? _Key, biv ?? _IV);
        }
        /// <summary> DES解密字符串
        /// </summary>
        /// <param name="ciphertext">待解密的字符串</param>
        /// <param name="key">加密密钥,要求为8位</param>
        /// <param name="iv">向量,要求为8位</param>
        public static string Decrypt(string ciphertext, byte[] key, byte[] iv)
        {
            return Decrypt(Convert.FromBase64String(ciphertext), key, iv);
        }
        /// <summary> DES解密字符串
        /// </summary>
        /// <param name="ciphertext">待解密的字符串</param>
        /// <param name="key">加密密钥,要求为8位</param>
        /// <param name="iv">向量,要求为8位</param>
        public static string Decrypt(string ciphertext, string key = null, string iv = null)
        {
            if (string.IsNullOrEmpty(ciphertext)) return "";
            if (ciphertext.Trim() == "") return ciphertext;

            byte[] bkey = null;
            byte[] biv = null;
            if (key != null)
            {
                TryException(key, "密钥");
                bkey = CreateKeyOrIv(key);
            }
            if (iv != null)
            {
                TryException(iv, "向量");
                biv = CreateKeyOrIv(iv);
            }

            return Decrypt(Convert.FromBase64String(ciphertext), bkey ?? _Key, biv ?? _IV);
        }

        /// <summary> 单向加密
        /// </summary>
        public static string OneWayEncrypt(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            if (input.Trim() == "") return input;

            string key, iv;
            if (input.Length == 1)
            {
                key = new string(input[0], 8);
                iv = key;
            }
            else
            {
                var arr = CreateKeyAndIv(input);
                key = arr[0];
                iv = arr[1];
            }

            return Encrypt(input, key, iv);
        }

        /// <summary> 使用16位MD5加密
        /// </summary>
        /// <param name="input">加密字符串</param>
        /// <param name="count">加密次数</param>
        public static string MD5x16(string input, int count = 1)
        {
            if (count <= 0)
            {
                return input;
            }
            for (int i = 0; i < count; i++)
            {
                input = MD5x16(input);
            }
            return input;
        }

        /// <summary> 使用16位MD5加密
        /// </summary>
        /// <param name="input">加密字符串</param>
        public static string MD5x16(string input)
        {
            var md5 = new MD5CryptoServiceProvider();
            var data = Hash(md5, Encoding.UTF8.GetBytes(input));
            return ByteToString(data, 4, 8);
        }

        /// <summary> 使用MD5加密
        /// </summary>
        /// <param name="input">加密字符串</param>
        /// <param name="count">加密次数</param>
        public static string MD5(string input, int count = 1)
        {
            if (count <= 0)
            {
                return input;
            }
            for (int i = 0; i < count; i++)
            {
                input = MD5(input);
            }
            return input;
        }

        /// <summary> 使用MD5加密
        /// </summary>
        /// <param name="input">加密字符串</param>
        public static string MD5(string input)
        {
            var md5 = new MD5CryptoServiceProvider();
            var data = Hash(md5, Encoding.UTF8.GetBytes(input));
            return ByteToString(data);
        }

        private static byte[] Hash(HashAlgorithm algorithm, byte[] input)
        {
            return algorithm.ComputeHash(input);
        }

        private static byte[] Hash(HashAlgorithm algorithm, byte[] input, int offset, int count)
        {
            return algorithm.ComputeHash(input,offset,count);
        }

        /// <summary> 使用SHA1加密
        /// </summary>
        /// <param name="input">加密字符串</param>
        public static string SHA1(string input)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var data = Hash(sha1, Encoding.UTF8.GetBytes(input));
            return ByteToString(data);
        }

        /// <summary> 使用SHA1加密
        /// </summary>
        /// <param name="input">加密字符串</param>
        /// <param name="count">加密次数</param>
        public static string SHA1(string input, int count = 1)
        {
            if (count <= 0)
            {
                return input;
            }
            var sha1 = new SHA1CryptoServiceProvider();
            var data = Encoding.UTF8.GetBytes(input);
            for (int i = 0; i < count; i++)
            {
                data = Hash(sha1, data);
            }
            return ByteToString(data);
        }

        private static string ByteToString(byte[] data)
        {
            return ByteToString(data, 0, data.Length);
        }

        private static string ByteToString(byte[] data, int offset, int count)
        {
            if (data == null)
            {
                return null;
            }
            char[] chArray = new char[count * 2];
            var end = offset + count;
            for (int i = offset,j=0; i < end; i++)
            {
                byte num2 = data[i];
                chArray[j++] = NibbleToHex((byte)(num2 >> 4));
                chArray[j++] = NibbleToHex((byte)(num2 & 15));
            }
            return new string(chArray);
        }

        private static char NibbleToHex(byte nibble)
        {
            return ((nibble < 10) ? ((char)(nibble + 0x30)) : ((char)((nibble - 10) + 'a')));
        }





        /// <summary> 设置全局密钥和向量,字符串必须为8位半角字符
        /// </summary>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        public static void SetKeyAndIv(string key, string iv)
        {
            TryException(key, "密钥");
            TryException(iv, "向量");
            _Key = CreateKeyOrIv(key);
            _IV = CreateKeyOrIv(iv);
        }
    }
}
