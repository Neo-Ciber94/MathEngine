using System;

namespace ExtraUtils.MathEngine.Utilities
{
    /// <summary>
    /// A value type char by char <see cref="string"/> reader.
    /// </summary>
    public ref struct ValueStringReader
    {
        private readonly ReadOnlySpan<char> _str;
        private readonly int _length;
        private int _pos;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueStringReader" /> struct.
        /// </summary>
        /// <param name="str">The string to read.</param>
        public ValueStringReader(string str)
        {
            _str = str;
            _length = str.Length;

            Current = null;
            Prev = null;
            _pos = 0;
        }

        /// <summary>
        /// Determines if there is a next value.
        /// </summary>
        public bool HasNext => _pos != _length;

        /// <summary>
        /// Gets the current value or null if there is not current.
        /// </summary>
        public char? Current { get; private set; }

        /// <summary>
        /// Gets the previous value or null if there is not previous.
        /// </summary>
        public char? Prev { get; private set; }

        /// <summary>
        /// Gets the next value or null if there is not next.
        /// </summary>
        public char? Next => _pos == _length ? default(char?) : _str[_pos];

        /// <summary>
        /// Reads the next <see langword="char"/>.
        /// </summary>
        /// <returns>The next char.</returns>
        public char Read()
        {
            if (_pos == _length)
            {
                return Current.GetValueOrDefault();
            }

            Prev = Current;
            Current = _str[_pos++];
            return (char)Current;
        }
    }
}
