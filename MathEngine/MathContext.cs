﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Fasterflect.Extensions;
using MathEngine.Functions;

namespace MathEngine
{
    public class MathContext : IMathContext
    {
        public static MathContext Default { get; }

        static MathContext()
        {
            Default = new MathContext();
        }

        public IReadOnlyDictionary<string, IFunction> Functions { get; }
        public IReadOnlyDictionary<string, IBinaryOperator> BinaryOperators { get; }
        public IReadOnlyDictionary<string, IUnaryOperator> UnaryOperators { get; }
        public IReadOnlyDictionary<string, double> Values { get; }

        private MathContext()
        {
            Functions = GetMathContextFunctions();
            BinaryOperators = GetMathContextDataOfType<IBinaryOperator>();
            UnaryOperators = GetMathContextDataOfType<IUnaryOperator>();
            Values = GetConstantsAndVariables();
        }

        public MathContext(params (string, double)[] values)
        {
            Functions = Default.Functions;
            BinaryOperators = Default.BinaryOperators;
            UnaryOperators = Default.UnaryOperators;
            Values = GetConstantsAndVariables(values);
        }

        private static IReadOnlyDictionary<string, double> GetConstantsAndVariables(params (string, double)[] variables)
        {
            var builder = ImmutableDictionary.CreateBuilder<string, double>(StringIgnoreCaseEqualityComparer.Instance);
            builder.Add("pi", Math.PI);
            builder.Add("e", Math.E);
            builder.Add("infinity", double.PositiveInfinity);

            if(variables.Length > 0)
            {
                foreach(var e in variables)
                {
                    builder.Add(e.Item1, e.Item2);
                }
            }

            return builder.ToImmutable();
        }

        private static IReadOnlyDictionary<string, IFunction> GetMathContextFunctions()
        {
            var types = Assembly
                .GetExecutingAssembly()
                .TypesImplementing<IFunction>()
                .Concat(Assembly.GetEntryAssembly().TypesImplementing<IFunction>());

            return types.Where(t =>
            {
                var interfaces = t.GetInterfaces();
                return interfaces.Where(s => typeof(IFunction).IsAssignableFrom(s)).Count() == 1 || interfaces.Where(s => typeof(IInfixFunction).IsAssignableFrom(s)).Count() == 1;
            })
                .Where(t => t.GetConstructor(Type.EmptyTypes) != null)
                .Select(t => (IFunction)Activator.CreateInstance(t)!)
                .ToImmutableDictionary((f) => f!.Name, (f) => f, StringIgnoreCaseEqualityComparer.Instance)!;
        }

        private static IReadOnlyDictionary<string, T> GetMathContextDataOfType<T>() where T : IFunction
        {
            return GetAllTypesThatDirectlyImplements<T>()
                .Where(t => t.GetConstructor(Type.EmptyTypes) != null)
                .Select(t => (T)Activator.CreateInstance(t)!)
                .ToImmutableDictionary((f) => f!.Name, (f) => f, StringIgnoreCaseEqualityComparer.Instance)!;
        }

        private static IEnumerable<Type> GetAllTypesThatDirectlyImplements<T>()
        {
            var types = Assembly
                .GetExecutingAssembly()
                .TypesImplementing<T>()
                .Concat(Assembly.GetEntryAssembly().TypesImplementing<T>());

            return types.Where(t => t.GetInterfaces().Where(s => typeof(T).IsAssignableFrom(s)).Count() == 1);
        }

        public bool IsFunction(string functionName) => Functions.ContainsKey(functionName);

        public bool IsBinaryOperator(string symbol) => BinaryOperators.ContainsKey(symbol);

        public bool IsUnaryOperator(string symbol) => UnaryOperators.ContainsKey(symbol);

        public bool IsVariableOrConstant(string name) => Values.ContainsKey(name);

        public bool TryGetFunction(string functionName, [NotNullWhen(true)] out IFunction? func)
        {
            return Functions.TryGetValue(functionName, out func);
        }

        public bool TryGetInfixFunction(string functionName, [NotNullWhen(true)] out IInfixFunction? func)
        {
            func = null;
            if (TryGetFunction(functionName, out var outFunc))
            {
                func = outFunc as IInfixFunction;
                return func != null;
            }

            return false;
        }

        public bool TryGetBinaryOperator(string symbol, [NotNullWhen(true)] out IBinaryOperator? op)
        {
            return BinaryOperators.TryGetValue(symbol, out op);
        }

        public bool TryGetUnaryOperator(string symbol, [NotNullWhen(true)] out IUnaryOperator? op)
        {
            return UnaryOperators.TryGetValue(symbol, out op);
        }

        public IFunction GetFunction(string functionName)
        {
            if (TryGetFunction(functionName, out var func))
            {
                return func;
            }

            throw new Exception($"Cannot find the function named: {functionName}");
        }

        public IInfixFunction GetInfixFunction(string functionName)
        {
            if (TryGetFunction(functionName, out var func) && func is IInfixFunction)
            {
                return (IInfixFunction)func;
            }

            throw new Exception($"Cannot find the function named: {functionName}");
        }

        public IBinaryOperator GetBinaryOperator(string symbol)
        {
            if (TryGetBinaryOperator(symbol, out var op))
            {
                return op;
            }

            throw new Exception($"Cannot find the operator named: {symbol}");
        }

        public IUnaryOperator GetUnaryOperator(string symbol)
        {
            if (TryGetUnaryOperator(symbol, out var op))
            {
                return op;
            }

            throw new Exception($"Cannot find the operator named: {symbol}");
        }

        public double GetValue(string name)
        {
            if (Values.TryGetValue(name, out var result))
            {
                return result;
            }

            throw new Exception($"Cannot find the value named: {name}");
        }
    }
}
