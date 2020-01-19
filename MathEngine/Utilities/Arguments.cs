using System;
using System.Runtime.CompilerServices;

namespace ExtraUtils.MathEngine.Utilities
{
    /// <summary>
    /// Provides assertion utilities.
    /// </summary>
    public static class Arguments
    {
        /// <summary>
        /// Checks that the number of arguments is correct.
        /// </summary>
        /// <param name="expected">The expected number of arguments.</param>
        /// <param name="actual">The actual number of arguments.</param>
        /// <exception cref="ArgumentException">If hte number of arguments is incorrect</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Count(int expected, int actual)
        {
            if (expected != actual)
            {
                throw new ArgumentException($"Invalid number of arguments, expected {expected} but {actual} was get.");
            }
        }

        /// <summary>
        /// Checks that the number of arguments is correct.
        /// </summary>
        /// <param name="min">The minimum number of arguments.</param>
        /// <param name="max">The maximum number of arguments.</param>
        /// <param name="actual">The actual.</param>
        /// <exception cref="ArgumentException">If hte number of arguments is incorrect</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Count(int min, int max, int actual)
        {
            if(actual < min || actual > max)
            {
                throw new ArgumentException($"Invalid number of arguments, expected {min} to {max} arguments but {actual} was get.");
            }
        }

        /// <summary>
        /// Checks that the number of arguments is at least equals or greater than min.
        /// </summary>
        /// <param name="min">The minimum number of arguments.</param>
        /// <param name="actual">The actual.</param>
        /// <exception cref="ArgumentException">Invalid number of arguments, expected at least {min} arguments but {actual} was get.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MinCount(int min, int actual)
        {
            if(actual < min)
            {
                throw new ArgumentException($"Invalid number of arguments, expected at least {min} arguments but {actual} was get.");
            }
        }

        /// <summary>
        /// Checks that the number of arguments is not more than max.
        /// </summary>
        /// <param name="max">The maximum number of arguments.</param>
        /// <param name="actual">The actual.</param>
        /// <exception cref="ArgumentException">Invalid number of arguments, expected at least {min} arguments but {actual} was get.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MaxCount(int max, int actual)
        {
            if (actual > max)
            {
                throw new ArgumentException($"Invalid number of arguments, expected less than {max} arguments but {actual} was get.");
            }
        }
    }
}
