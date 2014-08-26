using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace blqw
{
    public static class Converter
    {
        public readonly static StringConverter String = new StringConverter();
        public readonly static BaseConverter<Object> Object;
    }
}
