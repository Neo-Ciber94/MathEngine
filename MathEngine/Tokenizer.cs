using System;
using System.Collections.Generic;
using System.Text;
using MathEngine.Functions;
using MathEngine.Utils;

namespace MathEngine
{
    /// <summary>
    /// A default implementation of the <see cref="ITokenizer"/> interface that provides methods for extract the tokens of an expression.
    /// </summary>
    /// <seealso cref="MathEngine.ITokenizer" />
    public sealed class Tokenizer : ITokenizer
    {
        private static Tokenizer? _default;

        /// <summary>
        /// Gets the default <see cref="ITokenizer"/> that use the default <see cref="IMathContext"/>
        /// </summary>
        /// <value>
        /// The default <see cref="ITokenizer"/>.
        /// </value>
        public static Tokenizer Default
        {
            get
            {
                if(_default == null)
                {
                    _default = new Tokenizer(MathContext.Default);
                }

                return _default;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tokenizer"/> class.
        /// </summary>
        /// <param name="context">The context to use within this tokenizer.</param>
        public Tokenizer(IMathContext context)
        {
            Context = context;
        }

        public IMathContext Context { get; }

        public Token[] GetTokens(string expression)
        {
            var array = ToStringArray(expression);
            return GetTokensInternal(array);
        }
        
        private Token[] GetTokensInternal(string[] arr)
        {
            if(arr.Length == 0)
            {
                return Array.Empty<Token>();
            }

            List<Token> tokens = new List<Token>(arr.Length);
            IMathContext context = Context;
            int length = arr.Length;

            for (int i = 0; i < length; i++)
            {
                string current = arr[i];

                if (IsNumber(current))
                {
                    tokens.Add(new Token(current, TokenType.Number));
                }
                else if (context.IsValue(current))
                {
                    tokens.Add(new Token(current, TokenType.Value));
                }
                else if (context.IsFunction(current))
                {
                    tokens.Add(new Token(current, TokenType.Function));
                }
                else if (context.IsBinaryOperator(current) || context.IsUnaryOperator(current))
                {
                    string? prev = i > 0 ? arr[i - 1] : null;
                    string? next = i < length - 1 ? arr[i + 1] : null;

                    if (IsUnaryOperator(current, prev, next))
                    {
                        tokens.Add(new Token(current, TokenType.UnaryOperator));
                    }
                    else
                    {
                        tokens.Add(new Token(current, TokenType.BinaryOperator));
                    }
                }
                else if (current == "(" || current == ")")
                {
                    tokens.Add(Token.FromParenthesis(current[0]));
                }
                else if (current == ",")
                {
                    tokens.Add(Token.Comma);
                }
                else
                {
                    tokens.Add(new Token(current, TokenType.Unknown));
                }

            }

            return tokens.ToArray();
        }

        private bool IsUnaryOperator(string current, string? prev, string? next)
        {
            IMathContext context = Context;

            if (!context.TryGetUnaryOperator(current, out var op))
            {
                return false;
            }

            if (op.Notation == OperatorNotation.Prefix)
            {
                if (prev != null && (prev == ")" 
                    || IsNumber(prev) 
                    || context.IsValue(prev) 
                    || (context.IsUnaryOperator(prev) && !context.IsBinaryOperator(prev))))
                {
                    return false;
                }

                return next != null && !IsValidOperator(next[0]) && (next == "(" || char.IsLetterOrDigit(next[0]));
            }
            else if (op.Notation == OperatorNotation.Postfix)
            {
                return prev != null && (prev == ")" || IsNumber(prev) || context.IsValue(prev));
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets an array with all the tokens of the given expression.
        /// </summary>
        /// <param name="expression">The expression to split.</param>
        /// <returns>An array with the tokens of the expression.</returns>
        public static string[] ToStringArray(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return Array.Empty<string>();
            }

            List<string> tokens = new List<string>();
            StringScanner scanner = new StringScanner(expression);
            char curChar;

            while (scanner.HasNext)
            {
                curChar = scanner.ReadChar();

                while (char.IsWhiteSpace(curChar))
                {
                    curChar = scanner.ReadChar();
                }

                if (char.IsLetter(curChar))
                {
                    StringBuilder sb = new StringBuilder();
                    do
                    {
                        sb.Append(curChar);
                        char? nextChar = scanner.Next;

                        if (nextChar != null && char.IsLetterOrDigit(nextChar.Value))
                        {
                            curChar = scanner.ReadChar();
                        }
                        else
                        {
                            break;
                        }
                    }
                    while (true);

                    tokens.Add(sb.ToString());
                }
                else if (char.IsDigit(curChar))
                {
                    StringBuilder sb = new StringBuilder();
                    bool hasDecimalPoint = false;

                    do
                    {
                        if (curChar == '.' && !hasDecimalPoint)
                        {
                            hasDecimalPoint = true;
                        }

                        sb.Append(curChar);
                        char? nextChar = scanner.Next;
                        if (nextChar != null && (char.IsDigit(nextChar.Value) || (nextChar == '.' && !hasDecimalPoint)))
                        {
                            curChar = scanner.ReadChar();
                        }
                        else
                        {
                            break;
                        }
                    }
                    while (true);

                    tokens.Add(sb.ToString());
                }
                else
                {
                    tokens.Add(curChar.ToString());
                }
            }



            return tokens.ToArray();
        }

        private static bool IsValidOperator(char c)
        {
            return c != '(' 
                && c != ')' 
                && c != '{' 
                && c != '}' 
                && c != '[' 
                && c != ']' 
                && c != '.' 
                && c != ',' 
                && char.IsPunctuation(c);
        }

        private static bool IsNumber(string str)
        {
            bool hasDecimalPoint = false;

            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];

                if (!char.IsDigit(c) && c != '.')
                {
                    return false;
                }

                if (c == '.')
                {
                    if (hasDecimalPoint)
                    {
                        return false;
                    }
                    else
                    {
                        hasDecimalPoint = true;
                    }
                }
            }

            // Number that starts or ends with decimal point as '.10' or '33.' are invalid.
            return !(str.StartsWith('.') || str.EndsWith('.'));
        }
    }
}