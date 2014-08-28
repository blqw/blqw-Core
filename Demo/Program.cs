using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using blqw;
using System.Diagnostics;
using System.Data;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable dt1 = new DataTable("User");
            dt1.Columns.Add("UID", typeof(Guid));
            dt1.Columns.Add("Name", typeof(string));
            dt1.Columns.Add("Birthday", typeof(DateTime));
            dt1.Columns.Add("Sex", typeof(int));
            dt1.Columns.Add("IsDeleted", typeof(bool));
            dt1.Rows.Add(Guid.NewGuid(), "blqw", DateTime.Parse("1986-10-29"), 1, false);
            dt1.Rows.Add(Guid.NewGuid(), "小明", DateTime.Parse("1990-1-1"), 1, false);
            dt1.Rows.Add(Guid.NewGuid(), "小华", DateTime.Parse("1990-2-2"), 0, false);
            var users = Convert2.ToList<User>(dt1);
        }

        class User
        {
            public Guid UID { get; set; }
            public string Name { get; set; }
            public DateTime Birthday { get; set; }
            public int Sex { get; set; }
            public bool IsDeleted { get; set; }
        }
    }
}
