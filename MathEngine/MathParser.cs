using MathEngine.Functions;
using System;
using System.Collections.Generic;

namespace MathEngine
{
    public static class MathParser
    {
        public static Token[] InfixToRPN(Token[] tokens) => InfixToRPN(tokens, IMathContext.Default);

        public static Token[] InfixToRPN(Token[] tokens, IMathContext context)
        {
            if (tokens.Length == 0)
            {
                return Array.Empty<Token>();
            }

            Stack<Token> output = new Stack<Token>();
            Stack<Token> operators = new Stack<Token>();

            foreach (var t in tokens)
            {
                TokenType type = t.Type;
                switch (type)
                {
                    case TokenType.Number:
                    case TokenType.Variable:
                        PushNumber(output, operators, t);
                        break;
                    case TokenType.Function:
                    case TokenType.UnaryOperator:
                        operators.Push(t);
                        break;
                    case TokenType.BinaryOperator:
                        PushBinaryOperator(context, output, operators, t);
                        break;
                    case TokenType.Parenthesis:
                        PushParentheses(output, operators, t);
                        break;
                    case TokenType.Comma:
                        continue;
                    default:
                        throw new ArithmeticException($"Invalid token: {t}");
                }
            }

            while (operators.TryPop(out Token? op))
            {
                if (op.Type == TokenType.Parenthesis)
                {
                    throw new ArithmeticException("Parentheses mismatch.");
                }

                output.Push(op);
            }

            var result = output.ToArray();
            Array.Reverse(result);
            return result;
        }

        private static void PushNumber(Stack<Token> output, Stack<Token> operators, Token t)
        {
            output.Push(t);

            if (operators.TryPeek(out Token? top))
            {
                if (top.Type == TokenType.UnaryOperator)
                {
                    output.Push(operators.Pop());
                }
            }
        }

        private static void PushBinaryOperator(IMathContext context, Stack<Token> output, Stack<Token> operators, Token t)
        {
            while (operators.TryPeek(out Token? top))
            {
                string topOperatorName = top.Value;
                if (topOperatorName == "(")
                {
                    break;
                }

                if (context.IsFunction(topOperatorName))
                {
                    output.Push(operators.Pop());
                }
                else if (context.TryGetBinaryOperator(topOperatorName[0], out var topOperator) && context.TryGetBinaryOperator(t.Value[0], out var binaryOperator))
                {
                    int topOperatorPrecedence = topOperator!.Precedence;
                    int operatorPrecedence = binaryOperator!.Precedence;

                    if ((topOperatorPrecedence > operatorPrecedence) || (topOperatorPrecedence == operatorPrecedence && topOperator.Associativity == Association.Left))
                    {
                        output.Push(operators.Pop());
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            operators.Push(t);
        }

        private static void PushParentheses(Stack<Token> output, Stack<Token> operators, Token t)
        {
            string value = t.Value;
            if (value == "(")
            {
                operators.Push(t);
            }
            else if (value == ")")
            {
                bool closedParentheses = false;
                while (operators.TryPop(out Token? top))
                {
                    if (top.Value != "(")
                    {
                        output.Push(top);
                    }
                    else
                    {
                        closedParentheses = true;
                        break;
                    }
                }

                if (!closedParentheses)
                {
                    throw new ArithmeticException("Parentheses mismatch.");
                }
            }
        }
    }
}
