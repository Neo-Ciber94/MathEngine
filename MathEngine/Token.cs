using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ExtraUtils.MathEngine
{
    /// <summary>
    /// Represents the type of a token.
    /// </summary>
    public enum TokenType : byte
    {
        /// <summary>
        /// The token is a number.
        /// </summary>
        Number,
        /// <summary>
        /// The token is either a constant or variable value. e.g. pi, x, y, e.
        /// </summary>
        VariableOrConstant,
        /// <summary>
        /// The token is an operator that operates over 2 arguments.
        /// </summary>
        BinaryOperator,
        /// <summary>
        /// The token is an operator that operates over 1 argument.
        /// </summary>
        UnaryOperator,
        /// <summary>
        /// The token is a function.
        /// </summary>
        Function,
        /// <summary>
        /// The token is a parenthesis.
        /// </summary>
        Parenthesis,
        /// <summary>
        /// The token is a comma.
        /// </summary>
        Comma,
        /// <summary>
        /// The token is used to determine the number of arguments a function takes. This type is used internally.
        /// </summary>
        ArgCount,
        /// <summary>
        /// The type of the token cannot be determined.
        /// </summary>
        Unknown
    }

    /// <summary>
    /// Represents a value of an expression.
    /// </summary>
    /// <seealso cref="System.IEquatable{ExtraUtils.MathEngine.Token}" />
    public partial class Token : IEquatable<Token>
    {
        /// <summary>
        /// Gets the string value of this token.
        /// </summary>
        /// <value>
        /// The string value.
        /// </value>
        public string Value { get; }
        
        /// <summary>
        /// Gets the type of this token.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public TokenType Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> class.
        /// </summary>
        /// <param name="value">The string value of the token.</param>
        /// <param name="type">The type of the token.</param>
        /// <exception cref="ArgumentException">If the value is null or empty.</exception>
        public Token(string value, TokenType type)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Token value cannot be null or empty.");
            }

            Value = value;
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> class.
        /// </summary>
        /// <param name="value">The char value of the token.</param>
        /// <param name="type">The type of the token.</param>
        public Token(char value, TokenType type) : this(value.ToString(), type) { }

        /// <summary>
        /// Converts this token value to double.
        /// </summary>
        /// <returns>The double value of this token string.</returns>
        /// <exception cref="FormatException">If cannot convert this token value to double.</exception>
        public double ToDouble()
        {
            if (double.TryParse(Value, out var result))
            {
                return result;
            }

            throw new FormatException($"Cannot convert the specified value to double: {Value}");
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