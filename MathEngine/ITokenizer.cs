using System;
using System.Collections.Generic;
using System.Text;
using MathEngine.Utils;

namespace MathEngine
{
    public interface ITokenizer
    {
        public IMathContext Context { get; }

        public Token[] GetTokens(string expression);
    }

    public class Tokenizer : ITokenizer
    {
        private static Tokenizer? _tokenizer;

        public static Tokenizer Instance
        {
            get
            {
                if(_tokenizer == null)
                {
                    _tokenizer = new Tokenizer(IMathContext.Default);
                }

                return _tokenizer;
            }
        }

        public Tokenizer(IMathContext context)
        {
            Context = context;
        }

        public IMathContext Context { get; }

        public Token[] GetTokens(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                if (expression == null)
                {
                    throw new ArgumentNullException(nameof(expression));
                }

                return Array.Empty<Token>();
            }

            List<Token> tokens = new List<Token>();
            StringScanner scanner = new StringScanner(expression.RemoveWhiteSpaces()); //TODO: Handle whitespaces within this method
            IMathContext context = Context;
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