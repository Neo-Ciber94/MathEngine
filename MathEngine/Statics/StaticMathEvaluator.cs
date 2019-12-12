using MathEngine.Functions;
using MathEngine.Utils;
using System;
using System.Collections.Generic;

namespace MathEngine
{
    public static class StaticMathEvaluator
    {
        public static double Evaluate(string expression) => Evaluate(expression, IMathContext.Default);
        
        public static double Evaluate(string expression, IMathContext context)
        {
            var tokens = StaticTokenizer.GetTokens(expression, context);
            var rpn = MathParser.InfixToRPN(tokens, context);
            return Evaluate(rpn, context);
        }

        public static double Evaluate(Token[] tokens) => Evaluate(tokens, IMathContext.Default);

        public static double Evaluate(Token[] tokens, IMathContext context)
        {
            Stack<double> values = new Stack<double>();

            foreach (var t in tokens)
            {
                TokenType type = t.Type;
                if (type == TokenType.Number)
                {
                    values.Push(t.ToDouble());
                }
                if(type == TokenType.Variable)
                {
                    values.Push(context.GetValue(t.Value));
                }
                else if (type == TokenType.UnaryOperator)
                {
                    string operation = t.Value;
                    double value = values.Pop();
                    double result = context.Evaluate(value, operation);
                    values.Push(result);
                }
                else if (type == TokenType.BinaryOperator)
                {
                    string operation = t.Value;
                    double a = values.Pop();
                    double b = values.Pop();
                    double result = context.Evaluate(a, b, operation);
                    values.Push(result);
                }
                else if(type == TokenType.Function)
                {
                    if(context.TryGetFunction(t.Value, out var func))
                    {
                        int arity = func!.Arity;

                        if (arity == 0)
                        {
                            var empty = Array.Empty<double>();
                            values.Push(func.Call(empty));
                        }
                        else
                        {
                            double[] args = values.PopAll();
                            double result = func.Call(args);
                            values.Push(result);
                        }
                    }
                    else
                    {
                        throw new Exception($"Cannot find the specified function: {t.Value}.");
                    }
                }
                else if(type == TokenType.Unknown)
                {
                    throw new ArgumentException($"Invalid token: {t}");
                }
            }

            if(values.Count > 1)
            {
                throw new ArgumentException("Expression evaluation have failed.");
            }

            return values.Pop();
        }

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
                        PushFunction(context, output, operators, t);
                        break;
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
                        while (operators.TryPeek(out Token? op))
                        {
                            if (op.Value == "(")
                            {
                                break;
                            }

                            output.Push(operators.Pop());
                        }
                        break;
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

        private static void PushFunction(IMathContext context, Stack<Token> output, Stack<Token> operators, Token t)
        {
            string name = t.Value;

            if (context.TryGetFunction(name, out var func))
            {
                if (func is IInfixFunction)
                {
                    PushBinaryOperator(context, output, operators, t);
                }
                else
                {
                    operators.Push(t);
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
                else if (context.TryGetBinaryOperator(topOperatorName, out var topOperator) && context.TryGetBinaryOperator(t.Value, out var binaryOperator))
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