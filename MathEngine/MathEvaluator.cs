using MathEngine.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MathEngine
{
    public static class MathEvaluator
    {
        public static double Evaluate(string expression) => Evaluate(expression, IMathContext.Default);
        public static double Evaluate(string expression, IMathContext context)
        {
            var tokens = Tokenizer.GetTokens(expression, context);
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
                if (type == TokenType.Number || type == TokenType.Variable)
                {
                    values.Push(t.ToDouble());
                }
                else if (type == TokenType.UnaryOperator)
                {
                    char operation = t.Value[0];
                    double value = values.Pop();
                    double result = context.Evaluate(value, operation);

                    values.Push(result);
                }
                else if (type == TokenType.BinaryOperator)
                {
                    char operation = t.Value[0];
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

                        // Should avoid 0-arity functions?
                        if (arity == 0)
                        {
                            var noArgs = Array.Empty<double>();
                            values.Push(func.Call(noArgs));
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

            Debug.Assert(values.Count == 1);
            return values.Pop();
        }
    }
}
