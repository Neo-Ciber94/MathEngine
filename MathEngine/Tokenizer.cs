﻿using System;
using System.Collections.Generic;
using System.Text;
using MathEngine.Utils;

namespace MathEngine
{
    public class Tokenizer : ITokenizer
    {
        public static Tokenizer Default { get; }

        static Tokenizer()
        {
            Default = new Tokenizer(MathContext.Default);
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
            StringScanner scanner = new StringScanner(expression.RemoveWhiteSpaces()); //TODO: handle whitespaces in a more eficient way
            IMathContext context = Context;
            char curChar;

            while (scanner.HasNext)
            {
                curChar = (char)scanner.Read()!;

                //while (char.IsWhiteSpace(curChar))
                //{
                //    curChar = (char)scanner.Read()!;
                //}

                if (context.IsUnaryOperator(curChar))
                {
                    if (IsUnaryOperator(scanner.Prev, scanner.Next))
                    {
                        tokens.Add(new Token(curChar, TokenType.UnaryOperator));
                        continue;
                    }
                }

                if (context.IsBinaryOperator(curChar))
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
                        var n when context.IsVariableOrConstant(n) => TokenType.Variable,
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

                        if (nextChar == null || !(char.IsDigit(nextChar.Value) || (nextChar == '.' && !hasDecimalPoint)))
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

        private bool IsUnaryOperator(char? prevChar, char? nextChar)
        {
            if (prevChar != null && prevChar != ',' && (char.IsLetterOrDigit(prevChar.Value) || prevChar == '.' || prevChar == ')'))
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