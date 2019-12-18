using System;
using System.Diagnostics.CodeAnalysis;
using MathEngine.Functions;

namespace MathEngine
{
    /// <summary>
    /// Provides all the functions, operators and values used during at math expression evaluation.
    /// </summary>
    public interface IMathContext
    {
        /// <summary>
        /// Determines whether the specified function exists within this context.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <returns>
        ///   <c>true</c> if the specified function exists in this context; otherwise, <c>false</c>.
        /// </returns>
        public bool IsFunction(string functionName);
        /// <summary>
        /// Determines whether a binary operator with the specified symbol exists in this context.
        /// </summary>
        /// <param name="symbol">The symbol of the operator.</param>
        /// <returns>
        ///   <c>true</c> if the binery operator exists; otherwise, <c>false</c>.
        /// </returns>
        public bool IsBinaryOperator(string symbol);
        /// <summary>
        /// Determines whether an unary operator with the specified symbol exists in this context.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <returns>
        ///   <c>true</c> if exist an unary operator with the given symbol; otherwise, <c>false</c>.
        /// </returns>
        public bool IsUnaryOperator(string symbol);
        /// <summary>
        /// Determines if exists a constant or variable with the specified name.
        /// </summary>
        /// <param name="name">The name of the value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value exists; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValue(string name);
        /// <summary>
        /// Determines whether a binary operator with the specified symbol exists in this context.
        /// </summary>
        /// <param name="symbol">The symbol of the operator.</param>
        /// <returns>
        ///   <c>true</c> if the binery operator exists; otherwise, <c>false</c>.
        /// </returns>
        public bool IsBinaryOperator(char symbol) => IsBinaryOperator(symbol.ToString());
        /// <summary>
        /// Determines whether an unary operator with the specified symbol exists in this context.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <returns>
        ///   <c>true</c> if exist an unary operator with the given symbol; otherwise, <c>false</c>.
        /// </returns>
        public bool IsUnaryOperator(char symbol) => IsUnaryOperator(symbol.ToString());
        /// <summary>
        /// Tries get a function with the specified name from this context.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="func">The function.</param>
        /// <returns><c>true</c>If the function exists; otherwise, <c>false</c></returns>
        public bool TryGetFunction(string functionName, [NotNullWhen(returnValue: true)] out IFunction? func);
        /// <summary>
        /// Tries get a binary operator with the specified symbol from this context.
        /// </summary>
        /// <param name="symbol">Name of the operator.</param>
        /// <param name="op">The operator.</param>
        /// <returns><c>true</c>If the operator exists; otherwise, <c>false</c></returns>
        public bool TryGetBinaryOperator(string symbol, [NotNullWhen(returnValue: true)] out IBinaryOperator? op);
        /// <summary>
        /// Tries get an unary operator with the specified symbol from this context.
        /// </summary>
        /// <param name="symbol">Name of the operator.</param>
        /// <param name="op">The operator.</param>
        /// <returns><c>true</c>If the operator exists; otherwise, <c>false</c></returns>
        public bool TryGetUnaryOperator(string symbol, [NotNullWhen(returnValue: true)]  out IUnaryOperator? op);
        /// <summary>
        /// Gets a function from this context with the specified name.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <returns>The function with the specified name.</returns>
        /// <exception cref="InvalidOperationException">If cannot find a function with the given name.</exception>
        public IFunction GetFunction(string functionName)
        {
            if (TryGetFunction(functionName, out var func))
            {
                return func!;
            }

            throw new InvalidOperationException($"Cannot find the specified function: {functionName}.");
        }
        /// <summary>
        /// Gets a binary operator from this context with the specified name.
        /// </summary>
        /// <param name="symbol">Name of the operator.</param>
        /// <returns>The operator with the specified name.</returns>
        /// <exception cref="InvalidOperationException">If cannot find a operator with the given symbol.</exception>
        public IBinaryOperator GetBinaryOperator(string symbol)
        {
            if (TryGetBinaryOperator(symbol, out var op))
            {
                return op!;
            }

            throw new InvalidOperationException($"Cannot find the specified operator: {symbol}.");
        }
        /// <summary>
        /// Gets an unary operator from this context with the specified name.
        /// </summary>
        /// <param name="symbol">Name of the operator.</param>
        /// <returns>The operator with the specified name.</returns>
        /// <exception cref="InvalidOperationException">If cannot find a operator with the given symbol.</exception>
        public IUnaryOperator GetUnaryOperator(string symbol)
        {
            if (TryGetUnaryOperator(symbol, out var op))
            {
                return op!;
            }

            throw new InvalidOperationException($"Cannot find the specified operator: {symbol}.");
        }
        /// <summary>
        /// Gets a value from this context with the specified name.
        /// </summary>
        /// <param name="name">The name of the value.</param>
        /// <returns>The double value of the constant or variable.</returns>
        public double GetValue(string name);
    }
}
