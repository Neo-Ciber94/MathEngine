using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace ExtraUtils.MathEngine.Utilities
{
    /// <summary>
    /// Represents a counter.
    /// </summary>
    public class Counter : IEquatable<Counter>
    {
        /// <summary>
        /// Gets the current value of this counter.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value 
        { 
            get;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private set; 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Counter"/> class.
        /// </summary>
        /// <param name="initialValue">The initial value.</param>
        public Counter(int initialValue)
        {
            Value = initialValue;
        }

        /// <summary>
        /// Increments the value of this counter.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Increment() { ++Value; }

        /// <summary>
        /// Decrements the value of this counter.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Decrement() { --Value; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Counter);
        }

        public bool Equals([AllowNull] Counter other)
        {
#pragma warning disable CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
            return other != null &&
#pragma warning restore CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
                   Value == other.Value;
        }

        public override string ToString()
        {
            return $"Counter({Value})";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        public static implicit operator Counter(int value)
        {
            return new Counter(value);
        }

        public static bool operator ==(Counter left, Counter right)
        {
            return EqualityComparer<Counter>.Default.Equals(left, right);
        }

        public static bool operator !=(Counter left, Counter right)
        {
            return !(left == right);
        }
    }
}