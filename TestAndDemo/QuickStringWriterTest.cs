using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using blqw;
using System.Text;

namespace TestAndDemo
{
    [TestClass]
    public class QuickStringWriterTest
    {
        [TestMethod]
        public void AppendResultTest()
        {
            var sw = new QuickStringWriter();
            Assert.AreEqual("true", sw.Append(true).ToString());
            sw.Clear();
            Assert.AreEqual("false", sw.Append(false).ToString());
            sw.Clear();
            Assert.AreEqual(byte.MaxValue.ToString(), sw.Append(byte.MaxValue).ToString());
            sw.Clear();
            Assert.AreEqual(byte.MinValue.ToString(), sw.Append(byte.MinValue).ToString());
            sw.Clear();
            Assert.AreEqual(char.MaxValue.ToString(), sw.Append(char.MaxValue).ToString());
            sw.Clear();
            Assert.AreEqual(char.MinValue.ToString(), sw.Append(char.MinValue).ToString());
            sw.Clear();
            Assert.AreEqual(DateTime.MaxValue.ToString("yyyy-MM-dd HH:mm:ss"), sw.Append(DateTime.MaxValue).ToString());
            sw.Clear();
            Assert.AreEqual(DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss"), sw.Append(DateTime.MinValue).ToString());
            sw.Clear();
            Assert.AreEqual(decimal.MaxValue.ToString(), sw.Append(decimal.MaxValue).ToString());
            sw.Clear();
            Assert.AreEqual(decimal.MinValue.ToString(), sw.Append(decimal.MinValue).ToString());
            sw.Clear();
            Assert.AreEqual(decimal.MinusOne.ToString(), sw.Append(decimal.MinusOne).ToString());
            sw.Clear();
            Assert.AreEqual(decimal.One.ToString(), sw.Append(decimal.One).ToString());
            sw.Clear();
            Assert.AreEqual(double.MaxValue.ToString(), sw.Append(double.MaxValue).ToString());
            sw.Clear();
            Assert.AreEqual(double.MinValue.ToString(), sw.Append(double.MinValue).ToString());
            sw.Clear();
            Assert.AreEqual(float.MaxValue.ToString(), sw.Append(float.MaxValue).ToString());
            sw.Clear();
            Assert.AreEqual(float.MinValue.ToString(), sw.Append(float.MinValue).ToString());
            sw.Clear();
            Assert.AreEqual(Guid.Empty.ToString(), sw.Append(Guid.Empty).ToString());
            sw.Clear();
            Assert.AreEqual(int.MaxValue.ToString(), sw.Append(int.MaxValue).ToString());
            sw.Clear();
            Assert.AreEqual(int.MinValue.ToString(), sw.Append(int.MinValue).ToString());
            sw.Clear();
            Assert.AreEqual(long.MaxValue.ToString(), sw.Append(long.MaxValue).ToString());
            sw.Clear();
            Assert.AreEqual(long.MinValue.ToString(), sw.Append(long.MinValue).ToString());
            sw.Clear();
            Assert.AreEqual(sbyte.MaxValue.ToString(), sw.Append(sbyte.MaxValue).ToString());
            sw.Clear();
            Assert.AreEqual(sbyte.MinValue.ToString(), sw.Append(sbyte.MinValue).ToString());
            sw.Clear();
            Assert.AreEqual(short.MaxValue.ToString(), sw.Append(short.MaxValue).ToString());
            sw.Clear();
            Assert.AreEqual(short.MinValue.ToString(), sw.Append(short.MinValue).ToString());
            sw.Clear();
            Assert.AreEqual("234567890pokjhgvfcvbnjkiu876trd", sw.Append("234567890pokjhgvfcvbnjkiu876trd").ToString());
            sw.Clear();
            Assert.AreEqual(uint.MaxValue.ToString(), sw.Append(uint.MaxValue).ToString());
            sw.Clear();
            Assert.AreEqual(uint.MinValue.ToString(), sw.Append(uint.MinValue).ToString());
            sw.Clear();
            Assert.AreEqual(ulong.MaxValue.ToString(), sw.Append(ulong.MaxValue).ToString());
            sw.Clear();
            Assert.AreEqual(ulong.MinValue.ToString(), sw.Append(ulong.MinValue).ToString());
            sw.Clear();
            Assert.AreEqual(ushort.MaxValue.ToString(), sw.Append(ushort.MaxValue).ToString());
            sw.Clear();
            Assert.AreEqual(ushort.MinValue.ToString(), sw.Append(ushort.MinValue).ToString());
        }


        [TestMethod]
        public void StringBuilderTimer()
        {
            for (int j = 0; j < 100; j++)
            {
                var sw = new StringBuilder(2048);
                for (int i = 0; i < 1000; i++)
                {
                    sw.Append(135456);
                    sw.Append("aaaaaaaa");
                    sw.Append(DateTime.MinValue);
                    sw.Append(Guid.Empty);
                    sw.Append(true);
                    sw.Append(123456.45678);
                }
                string str = sw.ToString();
            }
        }


        [TestMethod]
        public void QuickStringWriterTimer()
        {
            for (int j = 0; j < 100; j++)
            {
                var sw = new QuickStringWriter();
                for (int i = 0; i < 1000; i++)
                {
                    sw.Append(135456);
                    sw.Append("aaaaaaaa");
                    sw.Append(DateTime.MinValue);
                    sw.Append(Guid.Empty);
                    sw.Append(true);
                    sw.Append(123456.45678);
                }
                string str = sw.ToString();
            }
        }
    }
}
