using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MathEngine.Utils
{
    public static class StringReaderExtensions
    {
        public static char ReadChar(this StringReader reader)
        {
            int value = reader.Read();
            return value < 0 ? '\0' : (char)value;
        }

        public static char PeekChar(this StringReader reader)
        {
            int value = reader.Peek();
            return value < 0 ? '\0' : (char)value;
        }

        public static char? PeekCharOrNull(this StringReader reader)
        {
            int value = reader.Peek();
            if(value < 0)
            {
                return null;
            }

            return (char)value;
        }

        public static bool HasNext(this StringReader reader)
        {
            return reader.Peek() >= 0;
        }
    }
}
