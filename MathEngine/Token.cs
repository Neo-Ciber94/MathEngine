using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MathEngine
{
    public enum TokenType { Number, BinaryOperator, UnaryOperator, Function, Variable, Parenthesis, Comma, Unknown }

    public partial class Token : IEquatable<Token>
    {
        public string Value { get; }
        public TokenType Type { get; }

        public Token(string value, TokenType type)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Token value cannot be null or empty.");
            }

            Value = value;
            Type = type;
        }

        public Token(char value, TokenType type) : this(value.ToString(), type) { }

        public double ToDouble()
        {
            if (double.TryParse(Value, out var result))
            {
                return result;
            }

            throw new FormatException($"Cannot convert the specified value to double: {Value}.");
        }

        public override string ToString()
        {
            return $"Token({Value}, {Type})";
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Token);
        }

        public bool Equals([AllowNull] Token other)
        {
#pragma warning disable CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
            return other != null &&
#pragma warning restore CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
                   Value == other.Value &&
                   Type == other.Type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Type);
        }

        public static bool operator ==(Token left, Token right)
        {
            return EqualityComparer<Token>.Default.Equals(left, right);
        }

        public static bool operator !=(Token left, Token right)
        {
            return !(left == right);
        }
    }
}