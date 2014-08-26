using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using blqw;
using System.Diagnostics;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            object obj;
            Convert2.TryParseObject(1, typeof(MyEnum), out obj);
            Console.WriteLine(obj);
            CodeTimer.Initialize();
            Test3();
        }

        public static void Test1()
        {
            CodeTimer.Time("string -> enum : system", 100000, () => {
                Enum.Parse(typeof(MyEnum), "A", false);
            });
            CodeTimer.Time("string -> enum : my", 100000, () => {
                Converter.String.ChangedType("A", typeof(MyEnum), null, false);
            });

            CodeTimer.Time("CreateDelegate", 100000, () => {
                Converter.String.CreateDelegate(typeof(MyEnum));
            });
            var conv = Converter.String.CreateDelegate(typeof(MyEnum));
            CodeTimer.Time("Delegate", 100000, () => {
                object obj;
                conv("A", out obj);
            });
        }

        public static void Test2()
        {
            string str = "1";
            Type type = typeof(int);
            CodeTimer.Time("string -> int : system", 100000, () => {
                int obj;
                int.TryParse(str, out obj);
            });
            CodeTimer.Time("string -> int : system2", 100000, () => {
                Convert.ChangeType(str, type);
            });
            CodeTimer.Time("string -> int : my", 100000, () => {
                Converter.String.ChangedType(str, type, null, false);
            });

            CodeTimer.Time("CreateDelegate", 100000, () => {
                Converter.String.CreateDelegate(type);
            });
            var conv = Converter.String.CreateDelegate(type);
            CodeTimer.Time("Delegate", 100000, () => {
                object obj;
                conv(str, out obj);
            });
        }

        public static void Test3()
        {
            string str = "1";
            Type type = typeof(string);
            CodeTimer.Time("string -> string : system2", 100000, () => {
                Convert.ChangeType(str, type);
            });
            CodeTimer.Time("string -> string : my", 100000, () => {
                Converter.String.ChangedType(str, type, null, false);
            });

            CodeTimer.Time("CreateDelegate", 100000, () => {
                Converter.String.CreateDelegate(type);
            });
            var conv = Converter.String.CreateDelegate(type);
            CodeTimer.Time("Delegate", 100000, () => {
                object obj;
                conv(str, out obj);
            });
        }
    }


    public enum MyEnum
    {
        A = 1,
        B = 2,
        C = 4,
        D = 8,
    }
}
