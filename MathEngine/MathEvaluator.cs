using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ExtraUtils.MathEngine.Functions;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine
{
    /// <summary>
    /// Provides methods for evaluate math expressions.
    /// </summary>
    public static class MathEvaluator
    {
        /// <summary>
        /// Evaluates the specified math expression.
        /// </summary>
        /// <param name="expression">The expression to evaluate.</param>
        /// <returns>The result of the evaluation.</returns>
        public static double Evaluate(string expression) => Evaluate(expression, Tokenizer.Default);

        /// <summary>
        /// Evaluates the specified math expression.
        /// </summary>
        /// <param name="expression">The expression to evaluate.</param>
        /// <param name="tokenizer">The tokenizer for tokenize the expression.</param>
        /// <returns>The result of the evaluation.</returns>
        public static double Evaluate(string expression, ITokenizer tokenizer)
        {
            var context = tokenizer.Context;
            var tokens = tokenizer.GetTokens(expression);
            var rpn = InfixToRPN(tokens, context);
            return Evaluate(rpn, context);
        }

        /// <summary>
        /// Evaluates the specified math expression.
        /// </summary>
        /// <param name="expression">The expression to evaluate.</param>
        /// <param name="context">The context used for extract the tokens.</param>
        /// <returns>The result of the evaluation</returns>
        public static double Evaluate(string expression, IMathContext context)
        {
            var tokenizer = new Tokenizer(context);
            var tokens = tokenizer.GetTokens(expression);
            var rpn = InfixToRPN(tokens);
            return Evaluate(rpn, context);
        }

        /// <summary>
        /// Evaluates the specified math expression.
        /// </summary>
        /// <param name="expression">The expression to evaluate.</param>
        /// <param name="values">Additional values (variables or constants) to be used in the <see cref="IMathContext"/> during the expression evaluation.</param>
        /// <returns>The result of the evaluation.</returns>
        public static double Evaluate(string expression, params (string, double)[] values)
        {
            var context = new MathContext(values);
            var tokenizer = new Tokenizer(context);
            var tokens = tokenizer.GetTokens(expression);
            var rpn = InfixToRPN(tokens);
            return Evaluate(rpn, context);
        }

        /// <summary>
        /// Evaluates the specified expression tokens.
        /// </summary>
        /// <param name="tokens">The tokens of the expression in RPN (Reverse Polish Notation).
        /// <para></para>
        /// Also see: <see href="https://en.wikipedia.org/wiki/Reverse_Polish_notation"/>.</param>
        /// <returns>The result of the evaluation.</returns>
        public static double Evaluate(Token[] tokens) => Evaluate(tokens, MathContext.Default);

        /// <summary>
        /// Evaluates the specified expression tokens.
        /// </summary>
        /// <param name="tokens">The tokens of the expression in RPN (Reverse Polish Notation).
        /// <para></para>
        /// Also see: <see href="https://en.wikipedia.org/wiki/Reverse_Polish_notation"/>.</param>
        /// <param name="context">The context used for the evaluation.</param>
        /// <returns>The result of the evaluation.</returns>
        /// <exception cref="ExpressionEvaluationException">If there is a misplace token, or cannot find a function, operator or value.</exception>
        /// <exception cref="FormatException">When parsing an invalid value.</exception>
        public static double Evaluate(Token[] tokens, IMathContext context)
        {
            Stack<double> values = new Stack<double>();
            using var enumerator = (tokens as IEnumerable<Token>).GetEnumerator();
            int argCount = -1;

            while (enumerator.MoveNext())
            {
                Token t = enumerator.Current;
                TokenType type = t.Type;

                if (type == TokenType.ArgCount)
                {
                    argCount = int.Parse(t.Value);
                    enumerator.MoveNext();
                    t = enumerator.Current;
                    type = t.Type;

                    if(type != TokenType.Function)
                    {
                        throw new ExpressionEvaluationException($"Expected a function, but {t} was get", tokens);
                    }
                }

                if (type == TokenType.Number)
                {
                    values.Push(t.ToDouble());
                }
                else if (type == TokenType.VariableOrConstant)
                {
                    if(!context.IsVariableOrConstant(t.Value))
                        throw new ExpressionEvaluationException($"Cannot find a value named: {t.Value}", tokens);

                    values.Push(context.GetVariableOrConstant(t.Value));
                }
                else if (type == TokenType.UnaryOperator)
                {
                    if(!context.TryGetUnaryOperator(t.Value, out IUnaryOperator? op))
                        throw new ExpressionEvaluationException($"Cannot find an unary operator named: {t.Value}",tokens);

                    if (!values.TryPop(out double value))
                        throw new ExpressionEvaluationException(tokens);

                    double result = op.Evaluate(value);
                    values.Push(result);
                }
                else if (type == TokenType.BinaryOperator)
                {
                    if (!context.TryGetBinaryOperator(t.Value, out IBinaryOperator? op))
                        throw new ExpressionEvaluationException($"Cannot find a binary operator named: {t.Value}", tokens);

                    if (!values.TryPop(out double b) || !values.TryPop(out double a))
                        throw new ExpressionEvaluationException(tokens);

                    double result = op.Evaluate(a, b);
                    values.Push(result);
                }
                else if (type == TokenType.Function)
                {
                    if (context.TryGetFunction(t.Value, out IFunction? func))
                    {
                        int arity = argCount < 0 ? func!.Arity : argCount;

                        if (arity == 0)
                        {
                            ReadOnlySpan<double> empty = ReadOnlySpan<double>.Empty;
                            values.Push(func.Call(empty));
                        }
                        else
                        {
                            Debug.Assert(arity > 0, $"Invalid function arity for {t}, was: {arity}");

                            int i = arity - 1;
                            Span<double> args = stackalloc double[arity];

                            while (i >= 0)
                            {
                                if (!values.TryPop(out double d))
                                    throw new ExpressionEvaluationException(tokens);

                                args[i--] = d;
                            }

                            double result = func.Call(args);
                            values.Push(result);
                            argCount = -1;
                        }
                    }
                    else
                    {
                        throw new ExpressionEvaluationException($"Cannot find the specified function: {t.Value}", tokens);
                    }
                }
                else if (type == TokenType.Unknown)
                {
                    throw new ExpressionEvaluationException($"Invalid token: {t}", tokens);
                }
            }

            if (values.Count > 1)
            {
                throw new ExpressionEvaluationException(tokens);
            }

            return values.Pop();
        }

        /// <summary>
        /// Converts the given tokens in infix notation to RPN (Reverse Polish Notation).
        /// <para></para>
        /// Also see: <see href="https://en.wikipedia.org/wiki/Reverse_Polish_notation"/>.
        /// </summary>
        /// <param name="tokens">The tokens to convert.</param>
        /// <returns>The expression converted to RPN.</returns>
        public static Token[] InfixToRPN(Token[] tokens) => InfixToRPN(tokens, MathContext.Default);

        /// <summary>
        /// Converts the given tokens in infix notation to RPN (Reverse Polish Notation).
        /// <para></para>
        /// Also see: <see href="https://en.wikipedia.org/wiki/Reverse_Polish_notation"/>.
        /// </summary>
        /// <param name="tokens">The tokens to convert.</param>
        /// <param name="context">The context to be used in the conversion.</param>
        /// <returns>The expression converted to RPN.</returns>
        /// <exception cref="ExpressionEvaluationException">If there is a parentheses mismatch, misplace unary operator or an unknown token.</exception>
        public static Token[] InfixToRPN(Token[] tokens, IMathContext context)
        {
            if (tokens.Length == 0)
            {
                return Array.Empty<Token>();
            }

            Stack<Token> output = new Stack<Token>();
            Stack<Token> operators = new Stack<Token>();
            Stack<Counter> argCounter = new Stack<Counter>();


            foreach (Token t in tokens)
            {
                TokenType type = t.Type;
                switch (type)
                {
                    case TokenType.Number:
                    case TokenType.VariableOrConstant:
                        PushNumber(output, operators, t);
                        break;
                    case TokenType.Function:
                        PushFunction(output, operators, argCounter, context, t);
                        break;
                    case TokenType.UnaryOperator:
                        if (!PushUnaryOperator(output, operators, context, t))
                            throw new ExpressionEvaluationException($"Misplaced unary operator: {t}", tokens);
                        break;
                    case TokenType.BinaryOperator:
                        PushBinaryOperator(output, operators, context, t);
                        break;
                    case TokenType.Parenthesis:
                        if (!PushParentheses(output, operators, argCounter, context, t))
                            throw new ExpressionEvaluationException("Parentheses mismatch", tokens);
                        break;
                    case TokenType.Comma:
                        while (operators.TryPeek(out Token? op))
                        {
                            if (op.Value == "(")
                            {
                                // Pop temporaly the parentheses
                                Token temp = operators.Pop();

                                // Check if the previous value in the operator stack is a function
                                // and if is increment that function argument counter
                                if(operators.TryPeek(out Token? func) && context.IsFunction(func.Value))
                                {
                                    argCounter.Peek().Increment();
                                }

                                // Put the parentheses back to the operators stack
                                operators.Push(temp);
                                break;
                            }

                            output.Push(operators.Pop());
                        }
                        break;
                    default:
                        throw new ExpressionEvaluationException($"Invalid token: {t}", tokens);
                }
            }

            while (operators.TryPop(out Token? op))
            {
                if (op.Type == TokenType.Parenthesis)
                {
                    throw new ExpressionEvaluationException("Parentheses mismatch", tokens);
                }

                output.Push(op);
            }

            Token[] result = output.ToArray();
            Array.Reverse(result);
            return result;
        }

        #region Helper Methods
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

        private static void PushFunction(Stack<Token> output, Stack<Token> operators, Stack<Counter> argCounter, IMathContext context, Token t)
        {
            if (context.TryGetFunction(t.Value, out IFunction? func))
            {
                if (func is IInfixFunction)
                {
                    PushBinaryOperator(output, operators, context, t);
                }
                else
                {
                    operators.Push(t);
                    argCounter.Push(new Counter(0));
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

        private static bool PushParentheses(Stack<Token> output, Stack<Token> operators, Stack<Counter> argCounter, IMathContext context, Token t)
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

                        if(argCounter.Count > 0 && operators.TryPeek(out Token? op))
                        {
                            if(context.TryGetFunction(op.Value, out IFunction? func))
                            {
                                int argCount = argCounter.Pop().Value;
                                output.Push(Token.ArgCount(argCount + 1));
                                output.Push(operators.Pop());

                                if(func.Arity >= 0)
                                {
                                    Debug.Assert(func.Arity == argCount,
                                        $"Invalid argCount for {func.Name} expected: {func.Arity} but {argCount} was get");
                                }
                            }
                        }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsVargArgs(IFunction function)
        {
            return function.Arity < 0;
        }
        #endregion
    }
}