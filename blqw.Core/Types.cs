using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace blqw
{
    public static class Types
    {
        public static readonly Type XmlDocument = typeof(System.Xml.XmlDocument);
        public static readonly Type Object = typeof(Object);
        public static readonly Type Guid = typeof(Guid);
        public static readonly Type DateTime = typeof(DateTime);
        public static readonly Type TimeSpan = typeof(TimeSpan);
        public static readonly Type TimeZone = typeof(TimeZone);
        public static readonly Type String = typeof(String);
        public static readonly Type Char = typeof(Char);
        public static readonly Type Binary = typeof(Byte[]);
        public static readonly Type Boolean = typeof(Boolean);
        public static readonly Type Byte = typeof(Byte);
        public static readonly Type Decimal = typeof(Decimal);
        public static readonly Type Double = typeof(Double);
        public static readonly Type Int16 = typeof(Int16);
        public static readonly Type Int32 = typeof(Int32);
        public static readonly Type Int = typeof(Int32);
        public static readonly Type Int64 = typeof(Int64);
        public static readonly Type Long = typeof(Int64);
        public static readonly Type SByte = typeof(SByte);
        public static readonly Type Single = typeof(Single);
        public static readonly Type Float = typeof(Single);
        public static readonly Type UInt16 = typeof(UInt16);
        public static readonly Type UInt32 = typeof(UInt32);
        public static readonly Type UInt64 = typeof(UInt64);
        public static readonly Type Array = typeof(Array);
        public static readonly Type DataTable = typeof(DataTable);
        public static readonly Type DataView = typeof(DataView);
        public static readonly Type DataSet = typeof(DataSet);
        public static readonly Type DataRow = typeof(DataRow);
        public static readonly Type DataColumn = typeof(DataColumn);
        public static readonly Type IEnumerable = typeof(IEnumerable);
        public static readonly Type IEnumerableT = typeof(IEnumerable<>);
        public static readonly Type ICollection = typeof(ICollection);
        public static readonly Type ICollectionT = typeof(ICollection<>);
        public static readonly Type Uri = typeof(Uri);
        public static readonly Type Nullable = typeof(Nullable<>);
        public static readonly Type ListT = typeof(List<>);
        public static readonly Type DictionaryT = typeof(Dictionary<,>);
        public static readonly Type ArrayList = typeof(ArrayList);
        public static readonly Type IList = typeof(IList);
        public static readonly Type IDictionary = typeof(IDictionary);
        public static readonly Type HashSetT = typeof(HashSet<>);
        public static readonly Type Hashtable = typeof(Hashtable);
    }
}
