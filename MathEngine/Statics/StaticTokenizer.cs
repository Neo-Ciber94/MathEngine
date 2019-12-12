using MathEngine.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathEngine
{
    public static class StaticTokenizer
    {
        public static Token[] GetTokens(string expression) => GetTokens(expression, IMathContext.Default);

        public static Token[] GetTokens(string expression, IMathContext context)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                if(expression == null)
                {
                    throw new ArgumentNullException(nameof(expression));
                }

                return Array.Empty<Token>();
            }

            List<Token> tokens = new List<Token>();
            StringScanner scanner = new StringScanner(expression.RemoveWhiteSpaces());
            char curChar;

            while (scanner.HasNext)
            {
                curChar = (char)scanner.Read()!;

                if (curChar == '+' || curChar == '-')
                {
                    if (IsUnaryOperator(curChar, scanner.Prev, scanner.Next))
                    {
                        tokens.Add(new Token(curChar, TokenType.UnaryOperator));
                        continue;
                    }
                }

                if (context.IsOperator(curChar))
                {
                    tokens.Add(new Token(curChar, TokenType.BinaryOperator));
                }
                else if (curChar == '(' || curChar == ')')
                {
                    tokens.Add(Token.FromParenthesis(curChar));
                }
                else if (curChar == ',')
                {
                    tokens.Add(Token.Comma);
                }
                else if (char.IsLetter(curChar))
                {
                    StringBuilder sb = new StringBuilder();
                    while (true)
                    {
                        sb.Append(curChar);
                        char? nextChar = scanner.Next;
                        if (nextChar == null || !char.IsLetter(nextChar.Value))
                        {
                            break;
                        }
                        else
                        {
                            curChar = (char)scanner.Read()!;
                        }
                    };

                    string value = sb.ToString();
                    TokenType type = value switch
                    {
                        var n when context.IsFunction(n) => TokenType.Function,
                        var n when context.IsVariable(n) => TokenType.Variable,
                        _ => TokenType.Unknown
                    };

                    tokens.Add(new Token(value, type));
                }
                else if (char.IsDigit(curChar))
                {
                    StringBuilder sb = new StringBuilder();
                    bool hasDecimalPoint = false;
                    while (true)
                    {
                        if (curChar == '.')
                        {
                            hasDecimalPoint = true;
                        }

                        sb.Append(curChar);
                        char? nextChar = scanner.Next;

                        if (nextChar == null || !char.IsDigit(nextChar.Value) || (curChar == '.' || hasDecimalPoint))
                        {
                            break;
                        }
                        else
                        {
                            curChar = (char)scanner.Read()!;
                        }
                    }

                    tokens.Add(new Token(sb.ToString(), TokenType.Number));
                }
                else
                {
                    tokens.Add(new Token(curChar, TokenType.Unknown));
                }
            }

            return tokens.ToArray();
        }

        private static bool IsUnaryOperator(char curChar, char? prevChar, char? nextChar)
        {
            if (!(curChar != '+' || curChar != '-'))
            {
                return false;
            }

            if (prevChar != null && (char.IsLetterOrDigit(prevChar.Value) || prevChar == '.'))
            {
                return false;
            }

            if (nextChar == null || !(char.IsLetterOrDigit(nextChar.Value) || nextChar == '(' || nextChar == '.'))
            {
                return false;
            }

            return true;
        }
    }
}