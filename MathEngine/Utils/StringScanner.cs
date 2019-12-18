using System;

namespace MathEngine.Utils
{
    public ref struct StringScanner
    {
        private readonly ReadOnlySpan<char> str;
        private readonly int length;
        private int pos;

        public StringScanner(string s)
        {
            str = s ?? throw new ArgumentNullException(nameof(s));
            length = s.Length;

            Current = null;
            Prev = null;
            pos = 0;
        }

        public bool HasNext => pos != length;
        
        public char? Current { get; private set; }
        
        public char? Prev { get; private set; }
        
        public char? Next
        {
            get
            {
                if (pos == length)
                {
                    return null;
                }

                return str[pos];
            }
        }

        public char? Read()
        {
            if (pos == length)
            {
                return Current;
            }

            Prev = Current;
            Current = str[pos++];
            return Current;
        }
    }

    public static class StringScannerExtensions
    {
        public static char ReadChar(this ref StringScanner reader)
        {
            return !reader.HasNext ? default : (char)reader.Read()!;
        }
    }
}
