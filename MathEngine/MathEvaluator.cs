using System;
using System.Collections.Generic;
using MathEngine.Functions;
using MathEngine.Utils;

namespace MathEngine
{
    public static class MathEvaluator
    {
        public static double Evaluate(string expression) => Evaluate(expression, Tokenizer.Default);

        public static double Evaluate(string expression, ITokenizer tokenizer)
        {
            var context = tokenizer.Context;
            var tokens = tokenizer.GetTokens(expression);
            var rpn = InfixToRPN(tokens, context);
            return Evaluate(rpn, context);
        }

        public static double Evaluate(string expression, IMathContext context)
        {
            var tokenizer = new Tokenizer(context);
            var tokens = tokenizer.GetTokens(expression);
            var rpn = InfixToRPN(tokens);
            return Evaluate(rpn, context);
        }

        public static double Evaluate(string expression, params (string, double)[] values)
        {
            var context = new MathContext(values);
            var tokenizer = new Tokenizer(context);
            var tokens = tokenizer.GetTokens(expression);
            var rpn = InfixToRPN(tokens);
            return Evaluate(rpn, context);
        }

        public static double Evaluate(Token[] tokens) => Evaluate(tokens, MathContext.Default);

        private static double _Evaluate(Token[] tokens, IMathContext context)
        {
            Stack<double> values = new Stack<double>();

            foreach (var t in tokens)
            {
                TokenType type = t.Type;
                if (type == TokenType.Number)
                {
                    values.Push(t.ToDouble());
                }
                if (type == TokenType.Value)
                {
                    values.Push(context.GetValue(t.Value));
                }
                else if (type == TokenType.UnaryOperator)
                {
                    var op = context.GetUnaryOperator(t.Value);
                    if (values.TryPop(out double value))
                    {
                        double result = op.Evaluate(value);
                        values.Push(result);
                    }
                    else
                    {
                        throw new ExpressionEvaluationException(values, tokens);
                    }
                }
                else if (type == TokenType.BinaryOperator)
                {
                    var op = context.GetBinaryOperator(t.Value);
                    if (values.TryPop(out double b) && values.TryPop(out double a))
                    {
                        double result = op.Evaluate(a, b);
                        values.Push(result);
                    }
                    else
                    {
                        throw new ExpressionEvaluationException(values, tokens);
                    }
                }
                else if (type == TokenType.Function)
                {
                    if (context.TryGetFunction(t.Value, out var func))
                    {
                        int arity = func!.Arity;

                        if (arity == 0)
                        {
                            var empty = ReadOnlySpan<double>.Empty;
                            values.Push(func.Call(empty));
                        }
                        else
                        {
                            unsafe
                            {
                                int length = arity < 0 ? values.Count : arity;
                                int i = length - 1;
                                Span<double> args = stackalloc double[length];

                                while (i >= 0)
                                {
                                    if (values.TryPop(out double d))
                                    {
                                        args[i--] = d;
                                    }
                                    else
                                    {
                                        throw new ExpressionEvaluationException(values, tokens);
                                    }
                                }

                                double result = func.Call(args);
                                values.Push(result);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception($"Cannot find the specified function: {t.Value}.");
                    }
                }
                else if (type == TokenType.Unknown)
                {
                    throw new ExpressionEvaluationException($"Invalid token: {t}");
                }
            }

            if (values.Count > 1)
            {
                throw new ExpressionEvaluationException(values, tokens);
            }

            return values.Pop();
        }

        public static double Evaluate(Token[] tokens, IMathContext context) //"(3 plus 2) times 10 divided 2"
        {
            Stack<double> values = new Stack<double>();
            using var enumerator = (tokens as IEnumerable<Token>).GetEnumerator();
            int argCount = -1;

            while(enumerator.MoveNext())
            {
                Token t = enumerator.Current;
                TokenType type = t.Type;

                if(type == TokenType.ArgCount)
                {
                    argCount = int.Parse(t.Value);
                    enumerator.MoveNext();
                    t = enumerator.Current;
                    type = t.Type;
                }

                if (type == TokenType.Number)
                {
                    values.Push(t.ToDouble());
                }
                if (type == TokenType.Value)
                {
                    values.Push(context.GetValue(t.Value));
                }
                else if (type == TokenType.UnaryOperator)
                {
                    var op = context.GetUnaryOperator(t.Value);
                    if (values.TryPop(out double value))
                    {
                        double result = op.Evaluate(value);
                        values.Push(result);
                    }
                    else
                    {
                        throw new ExpressionEvaluationException(values, tokens);
                    }
                }
                else if (type == TokenType.BinaryOperator)
                {
                    var op = context.GetBinaryOperator(t.Value);
                    if (values.TryPop(out double b) && values.TryPop(out double a))
                    {
                        double result = op.Evaluate(a, b);
                        values.Push(result);
                    }
                    else
                    {
                        throw new ExpressionEvaluationException(values, tokens);
                    }
                }
                else if (type == TokenType.Function)
                {
                    if (context.TryGetFunction(t.Value, out var func))
                    {
                        int arity = argCount < 0? func!.Arity: argCount;

                        if (arity == 0)
                        {
                            var empty = ReadOnlySpan<double>.Empty;
                            values.Push(func.Call(empty));
                        }
                        else
                        {
                            unsafe
                            {
                                int i = arity - 1;
                                Span<double> args = stackalloc double[arity];

                                while (i >= 0)
                                {
                                    if(values.TryPop(out double d))
                                    {
                                        args[i--] = d;
                                    }
                                    else
                                    {
                                        throw new ExpressionEvaluationException(values, tokens);
                                    }
                                }

                                double result = func.Call(args);
                                values.Push(result);
                                argCount = -1;
                            }
                        }
                    }
                    else
                    {
                        throw new Exception($"Cannot find the specified function: {t.Value}.");
                    }
                }
                else if (type == TokenType.Unknown)
                {
                    throw new ExpressionEvaluationException($"Invalid token: {t}");
                }
            }

            if (values.Count > 1)
            {
                throw new ExpressionEvaluationException(values, tokens);
            }

            return values.Pop();
        }

        public static Token[] InfixToRPN(Token[] tokens) => InfixToRPN(tokens, MathContext.Default);

        public static Token[] InfixToRPN(Token[] tokens, IMathContext context)
        {
            if (tokens.Length == 0)
            {
                return Array.Empty<Token>();
            }

            Stack<Token> output = new Stack<Token>();
            Stack<Token> operators = new Stack<Token>();
            Token? prevToken = null;

            bool isVarArgs = false;
            int argCount = 0;

            foreach (var t in tokens)
            {
                TokenType type = t.Type;
                switch (type)
                {
                    case TokenType.Number:
                    case TokenType.Value:
                        PushNumber(output, operators, t);
                        break;
                    case TokenType.Function:
                        PushFunction(output, operators, context, t);
                        break;
                    case TokenType.UnaryOperator:
                        if (!PushUnaryOperator(output, operators, context, t))
                            throw new ExpressionEvaluationException($"Misplaced unary operator: {t}.", tokens);
                        break;
                    case TokenType.BinaryOperator:
                        PushBinaryOperator(output, operators, context, t);
                        break;
                    case TokenType.Parenthesis:
                        if (!PushParentheses(output, operators, t))
                            throw new ExpressionEvaluationException("Parentheses mismatch.", tokens);
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
                        throw new ExpressionEvaluationException($"Invalid token: {t}.", tokens);
                }

                CheckArgCount(context, output, prevToken, ref isVarArgs, ref argCount, t, type);
                prevToken = t;
            }

            while (operators.TryPop(out Token? op))
            {
                if (op.Type == TokenType.Parenthesis)
                {
                    throw new ExpressionEvaluationException("Parentheses mismatch.", tokens);
                }

                output.Push(op);
            }

            var result = output.ToArray();
            Array.Reverse(result);
            return result;
        }

        private static void CheckArgCount(IMathContext context, Stack<Token> output, Token? prevToken, ref bool isVarArgs, ref int argCount, Token t, TokenType type)
        {
            if (type == TokenType.Function && context.TryGetFunction(t.Value, out var func))
            {
                if (!isVarArgs)
                {
                    isVarArgs = func.Arity < 0;
                }
            }
            else if (type == TokenType.Comma && isVarArgs)
            {
                argCount++;
            }
            else if (type == TokenType.Parenthesis && isVarArgs && t.Value == ")")
            {
                if (prevToken!.Value == "(")
                {
                    output.Push(Token.ArgCount(argCount));
                }
                else
                {
                    output.Push(Token.ArgCount(argCount + 1));
                }

                isVarArgs = false;
                argCount = 0;
            }
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

        private static void PushFunction(Stack<Token> output, Stack<Token> operators, IMathContext context, Token t)
        {
            if (context.TryGetFunction(t.Value, out var func))
            {
                if (func is IInfixFunction)
                {
                    PushBinaryOperator(output, operators, context, t);
                }
                else
                {
                    operators.Push(t);
                }
            }
        }

        private static bool PushUnaryOperator(Stack<Token> output, Stack<Token> operators, IMathContext context, Token t)
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
                        // throw new ExpressionEvaluationException($"Misplaced unary operator: {t}.");
                        return false;
                    }
                }
                else if (op!.Notation == OperatorNotation.Prefix)
                {
                    operators.Push(t);
                }
            }

            return true;
        }

        private static void _PushBinaryOperator(Stack<Token> output, Stack<Token> operators, IMathContext context, Token t)
        {
            while (operators.TryPeek(out Token? top))
            {
                string topOperatorName = top.Value;
                if (topOperatorName == "(")
                {
                    break;
                }

                if (context.IsFunction(topOperatorName)) // Is infix func?
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

        private static void PushBinaryOperator(Stack<Token> output, Stack<Token> operators, IMathContext context, Token t)
        {
            while (operators.TryPeek(out Token? top))
            {
                string topOperatorName = top.Value;
                if (topOperatorName == "(")
                {
                    break;
                }

                IBinaryOperator? topOperator = null;
                IBinaryOperator? currentOperator = null;

                if (context.TryGetFunction(topOperatorName, out var func))
                {
                    if (func is IInfixFunction f)
                    {
                        topOperator = f;
                    }
                    else
                    {
                        output.Push(operators.Pop());
                    }
                }

                if (topOperator == null)
                {
                    if (context.TryGetBinaryOperator(topOperatorName, out var f))
                    {
                        topOperator = f;
                    }
                }

                if (context.TryGetBinaryOperator(t.Value, out var op1) ^ context.TryGetFunction(t.Value, out var op2))
                {
                    currentOperator = (op1, op2) switch
                    {
                        _ when op1 != null => op1,
                        _ when op2 != null => op2 as IInfixFunction,
                        _ => null
                    };
                }


                if (topOperator != null && currentOperator != null)
                {
                    int topOperatorPrecedence = topOperator!.Precedence;
                    int operatorPrecedence = currentOperator!.Precedence;

                    if ((topOperatorPrecedence > operatorPrecedence) || (topOperatorPrecedence == operatorPrecedence && topOperator.Associativity == OperatorAssociativity.Left))
                    {
                        output.Push(operators.Pop());
                    }
                    else
                    {
                        break;
                    }
                }
            }

            operators.Push(t);
        }

        private static bool PushParentheses(Stack<Token> output, Stack<Token> operators, Token t)
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
                    //throw new ExpressionEvaluationException("Parentheses mismatch.");
                    return false;
                }
            }

            return true;
        }
    }
}