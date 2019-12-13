using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using MathEngine.Functions;

namespace MathEngine
{
    public interface IMathContext
    {
        private static IMathContext _default;

        public static IMathContext Default
        {
            get
            {
                if(_default == null)
                {
                    _default = new MathContext();
                }

                return _default;
            }
        }

        public bool IsFunction(string functionName);
        public bool IsBinaryOperator(string symbol);
        public bool IsUnaryOperator(string symbol);
        public bool IsVariableOrConstant(string name);
        public bool IsBinaryOperator(char symbol) => IsBinaryOperator(symbol.ToString());
        public bool IsUnaryOperator(char symbol) => IsUnaryOperator(symbol.ToString());
        public bool TryGetFunction(string functionName, [NotNullWhen(returnValue: true)] out IFunction? func);
        public bool TryGetBinaryOperator(string symbol, [NotNullWhen(returnValue: true)] out IBinaryOperator? op);
        public bool TryGetUnaryOperator(string symbol, [NotNullWhen(returnValue: true)]  out IUnaryOperator? op);
        public IFunction GetFunction(string functionName)
        {
            if (TryGetFunction(functionName, out var func))
            {
                return func!;
            }

            throw new Exception($"Cannot find the specified function: {functionName}.");
        }
        public IBinaryOperator GetBinaryOperator(string symbol)
        {
            if (TryGetBinaryOperator(symbol, out var op))
            {
                return op!;
            }

            throw new Exception($"Cannot find the specified operator: {symbol}.");
        }
        public IUnaryOperator GetUnaryOperator(string symbol)
        {
            if (TryGetUnaryOperator(symbol, out var op))
            {
                return op!;
            }

            throw new Exception($"Cannot find the specified operator: {symbol}.");
        }
        public double GetValue(string name);
    }
}
