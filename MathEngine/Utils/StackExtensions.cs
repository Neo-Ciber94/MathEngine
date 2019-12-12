using System;
using System.Collections.Generic;

namespace MathEngine.Utils
{
    public static class StackExtensions
    {
        public static T[] PopAll<T>(this Stack<T> stack)
        {
            int count = stack.Count;

            if(count == 0)
            {
                return Array.Empty<T>();
            }
            else
            {
                T[] array = new T[count];
                int i = 0;

                while(stack.TryPop(out T item))
                {
                    array[i++] = item;
                }

                return array;
            }
        }
    }
}
