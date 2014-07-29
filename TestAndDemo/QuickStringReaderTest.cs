using blqw;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestAndDemo
{
    [TestClass]
    public class QuickStringReaderTest
    {
        [TestMethod]
        public void ReadTest()
        {
            string str = "  str =  \"12345\\f\\u000\f789\"";

            var read = new blqw.EasyStringReader(str);

            var res = read.ReadToStop('=', ReadMode.SkipAll | ReadMode.RemoveStop);

            Assert.AreEqual("str ", res);


            res = read.ReadStartToStop('"', '"', ReadMode.SkipAll | ReadMode.ParseAll | ReadMode.RemoveAll);

            Assert.AreEqual("12345\f\\u000\f789", res);

            Assert.AreEqual(true, read.IsEnd);

            str = "12345\\f\\u000\f789";

            read = new blqw.EasyStringReader(str);

            res = read.Read(2, ReadMode.ParseAll);
            Assert.AreEqual("12", res);
            res = read.Read(9, ReadMode.ParseAll);
            Assert.AreEqual("345\f\\u000", res);

            Assert.AreEqual('\f', read.Current);


            str = @"{
    ""Name"":""blqw"",
    'Age':24,
    ""Hobby"":[""Coding"",""Music""]
}";

            read = new blqw.EasyStringReader(str);
            Assert.IsTrue(read.ReadNext());
            Assert.AreEqual('{', read.Current);
            Assert.IsTrue(read.ReadNext());
            res = read.ReadStartToStop(new[] { '"', '\'' }, true);
            Assert.AreEqual("Name", res);
            Assert.IsTrue(read.SkipWhiteSpace(true));
            Assert.AreEqual(':', read.Current);
            Assert.IsTrue(read.ReadNext());
            res = read.ReadStartToStop(new[] { '"', '\'' }, true);
            Assert.AreEqual("blqw", res);
            Assert.IsTrue(read.SkipWhiteSpace(true));
            Assert.AreEqual(',', read.Current);
            Assert.IsTrue(read.ReadNext());
            res = read.ReadStartToStop(new[] { '"', '\'' }, true);
            Assert.AreEqual("Age", res);
            Assert.IsTrue(read.SkipWhiteSpace(true));
            Assert.AreEqual(':', read.Current);
            Assert.IsTrue(read.ReadNext());
            res = read.ReadToStop(',', ReadMode.SkipAll | ReadMode.RemoveStop);
            Assert.AreEqual("24", res);
            res = read.ReadStartToStop(new[] { '"', '\'' }, true);
            Assert.AreEqual("Hobby", res);
            Assert.IsTrue(read.SkipWhiteSpace(true));
            Assert.AreEqual(':', read.Current);
            Assert.IsTrue(read.ReadNext(ReadMode.SkipAll));
            Assert.AreEqual('[', read.Current);
            Assert.IsTrue(read.ReadNext());
            res = read.ReadStartToStop(new[] { '"', '\'' }, true);
            Assert.AreEqual("Coding", res);
            Assert.IsTrue(read.SkipWhiteSpace(true));
            Assert.AreEqual(',', read.Current);
            Assert.IsTrue(read.ReadNext());
            res = read.ReadStartToStop(new[] { '"', '\'' }, true);
            Assert.AreEqual("Music", res);
            Assert.IsTrue(read.SkipWhiteSpace(true));
            Assert.AreEqual(']', read.Current);
            Assert.IsTrue(read.ReadNext());
            Assert.IsTrue(read.SkipWhiteSpace(true));
            Assert.AreEqual('}', read.Current);
            Assert.IsFalse(read.ReadNext());
            Assert.IsTrue(read.IsEnd);



        }
    }
}
