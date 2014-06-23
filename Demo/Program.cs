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
            Console.WriteLine("{0,-10}:{1}","message","aaa");
            var a = StringBuilderTimer();
            var b = QuickStringWriterTimer();
            Console.WriteLine(a.Equals(b, StringComparison.OrdinalIgnoreCase));
            CodeTimer.Initialize();

            CodeTimer.Time("A", 1000, () => StringBuilderTimer());
            CodeTimer.Time("B", 1000, () => QuickStringWriterTimer());

        }

        static string StringBuilderTimer()
        {
            var sw = new StringBuilder(2048);
            for (int i = 0; i < 1000; i++)
            {
                sw.Append(135456);
                sw.Append("aaaaaaaa");
                sw.Append(DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss"));
                sw.Append(Guid.Empty);
                sw.Append(true);
                sw.Append(123456.45678);
            }
            return sw.ToString();
        }


        static string QuickStringWriterTimer()
        {
            using (var sw = new QuickStringWriter())
            {
                for (int i = 0; i < 1000; i++)
                {
                    sw.Append(135456);
                    sw.Append("aaaaaaaa");
                    sw.Append(DateTime.MinValue);
                    sw.Append(Guid.Empty);
                    sw.Append(true);
                    sw.Append(123456.45678);
                }
                return sw.ToString();
            }
        }

    }
}
