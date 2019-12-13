using MathEngine.Functions;
using MathEngine.Utils;
using System;
using System.Collections.Generic;

namespace MathEngine
{
    public class MathEvaluator : IMathEvaluator
    {
        private static MathEvaluator? _evaluator;

        public static MathEvaluator Instance
        {
            get
            {
                if(_evaluator == null)
                {
                    _evaluator = new MathEvaluator(IMathContext.Default);
                }

                return _evaluator;
            }
        }

        public MathEvaluator(IMathContext context) : this(context, MathEngine.Tokenizer.Instance) { }

        public MathEvaluator(IMathContext context, ITokenizer tokenizer)
        {
            Context = context;
            Tokenizer = tokenizer;
        }

        public IMathContext Context { get; }

        public ITokenizer Tokenizer { get; }

        public double Evaluate(string expression)
        {
            var tokens = Tokenizer.GetTokens(expression);
            var rpn = InfixToRPN(tokens);
            return Evaluate(rpn);
        }

        public double Evaluate(Token[] tokens)
        {
            Stack<double> values = new Stack<double>();
            IMathContext context = Context;

            foreach (var t in tokens)
            {
                TokenType type = t.Type;
                if (type == TokenType.Number)
                {
                    values.Push(t.ToDouble());
                }
                if (type == TokenType.Variable)
                {
                    values.Push(context.GetValue(t.Value));
                }
                else if (type == TokenType.UnaryOperator)
                {
                    var op = context.GetUnaryOperator(t.Value);
                    double value = values.Pop();
                    double result = op.Evaluate(value);
                    values.Push(result);
                }
                else if (type == TokenType.BinaryOperator)
                {
                    var op = context.GetBinaryOperator(t.Value);
                    double a = values.Pop();
                    double b = values.Pop();
                    double result = op.Evaluate(a, b);
                    values.Push(result);
                }
                else if (type == TokenType.Function)
                {
                    if (context.TryGetFunction(t.Value, out var func))
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
                else if (type == TokenType.Unknown)
                {
                    throw new ArgumentException($"Invalid token: {t}");
                }
            }

            if (values.Count > 1)
            {
                throw new ArgumentException("Expression evaluation have failed.");
            }

            return values.Pop();
        }

        public Token[] InfixToRPN(Token[] tokens)
        {
            if (tokens.Length == 0)
            {
                return Array.Empty<Token>();
            }

            Stack<Token> output = new Stack<Token>();
            Stack<Token> operators = new Stack<Token>();
            IMathContext context = Context;

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
                        PushUnaryOperator(output, operators, context, t);
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

        private static void PushUnaryOperator(Stack<Token> output, Stack<Token> operators, IMathContext context, Token t)
        {
            if (context.TryGetUnaryOperator(t.Value, out var op))
            {
                if (op!.Notation == OperatorNotation.Postfix)
                {
                    if (output.Count > 0)
                    {
                        output.Push(t);
                    }
                    else
                    {
                        throw new Exception($"Misplaced unary operator: {t}");
                    }
                }
                else if (op!.Notation == OperatorNotation.Prefix)
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

                    if ((topOperatorPrecedence > operatorPrecedence) || (topOperatorPrecedence == operatorPrecedence && topOperator.Associativity == OperatorAssociativity.Left))
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