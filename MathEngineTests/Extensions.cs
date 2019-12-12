﻿using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;

namespace MathEngine.Tests
{
    public static class Extensions
    {
        public static string[] TokensToString(this ICollection<Token> tokens)
        {
            List<string> str = new List<string>(tokens.Count);

            foreach (var t in tokens)
            {
                str.Add(t.Value);
            }

            return str.ToArray();
        }

        public static string CollectionToString<T>(this ICollection<T> collection) => CollectionToString<T>(collection, (s) => s.ToString());

        public static string CollectionToString<T>(this ICollection<T> collection, Func<T, string> converter)
        {
            StringBuilder sb = new StringBuilder("(");
            sb.AppendJoin(',', collection.Select(converter));
            sb.Append(")");
            return sb.ToString();
        }
    }
}