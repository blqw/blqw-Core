using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace blqw
{
    /// <summary> 加密解密字符串
    /// </summary>
    public class Security
    {
        //默认密钥向量
        private static byte[] _IV = null;
        // 默认DES密钥
        private static byte[] _Key = null;
        private static void TryException(string str, string name)
        {
            if (str == null || str.Length != 8)
            {
                throw new CryptographicException("使用了无效的" + name);
            }
        }
        private static string EncodeToDes(byte[] input, byte[] key, byte[] iv)
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
        private static string DecodeToDes(byte[] input, byte[] key, byte[] iv)
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
            for (int i = offset, j = 0; i < end; i++)
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
        private static byte[] Hash(HashAlgorithm algorithm, byte[] input)
        {
            return algorithm.ComputeHash(input);
        }
        private static byte[] Hash(HashAlgorithm algorithm, byte[] input, int offset, int count)
        {
            return algorithm.ComputeHash(input, offset, count);
        }

        /// <summary> DES加密字符串
        /// </summary>
        /// <param name="input">待加密的字符串</param>
        /// <param name="key">加密密钥,要求为8位</param>
        /// <param name="iv">向量,要求为8位</param>
        public static string EncodeToDes(string input, byte[] key, byte[] iv)
        {
            return EncodeToDes(Encoding.UTF8.GetBytes(input), key, iv);
        }
        /// <summary> DES加密字符串
        /// </summary>
        /// <param name="input">待加密的字符串</param>
        /// <param name="key">加密密钥,要求为8位</param>
        /// <param name="iv">向量,要求为8位</param>
        public static string EncodeToDes(string input, string key = null, string iv = null)
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

            return EncodeToDes(Encoding.UTF8.GetBytes(input), bkey ?? _Key, biv ?? _IV);
        }
        /// <summary> DES解密字符串
        /// </summary>
        /// <param name="ciphertext">待解密的字符串</param>
        /// <param name="key">加密密钥,要求为8位</param>
        /// <param name="iv">向量,要求为8位</param>
        public static string DecodeToDes(string ciphertext, byte[] key, byte[] iv)
        {
            return DecodeToDes(Convert.FromBase64String(ciphertext), key, iv);
        }
        /// <summary> DES解密字符串
        /// </summary>
        /// <param name="ciphertext">待解密的字符串</param>
        /// <param name="key">加密密钥,要求为8位</param>
        /// <param name="iv">向量,要求为8位</param>
        public static string DecodeToDes(string ciphertext, string key = null, string iv = null)
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

            return DecodeToDes(Convert.FromBase64String(ciphertext), bkey ?? _Key, biv ?? _IV);
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
        /// <summary> 使用SHA1加密
        /// </summary>
        /// <param name="input">加密字符串</param>
        public static string SHA1(string input)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var data = Hash(sha1, Encoding.UTF8.GetBytes(input));
            return ByteToString(data);
        }
    }
}
