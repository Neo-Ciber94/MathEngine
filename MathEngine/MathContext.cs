using Fasterflect.Extensions;
using MathEngine.Functions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace MathEngine
{
    public class MathContext
    {
        private readonly IReadOnlyDictionary<string, IFunction> _functions;
        private readonly IReadOnlyDictionary<string, IBinaryOperator> _binaryOperators;
        private readonly IReadOnlyDictionary<string, IUnaryOperator> _unaryOperators;
        private readonly IReadOnlyDictionary<string, double> _values;

        public MathContext()
        {
            _functions = GetMathContextDataOfType<IFunction>();
            _binaryOperators = GetMathContextDataOfType<IBinaryOperator>();
            _unaryOperators = GetMathContextDataOfType<IUnaryOperator>();
            _values = GetVariablesAndConstantValues();
        }

        private IReadOnlyDictionary<string, double> GetVariablesAndConstantValues()
        {
            var builder = ImmutableDictionary.CreateBuilder<string, double>();
            builder.Add("pi", Math.PI);
            builder.Add("e", Math.E);
            return builder.ToImmutable();
        }

        private static IReadOnlyDictionary<string, T> GetMathContextDataOfType<T>() where T : IFunction
        {
            return GetAllTypesThatDirectlyImplements<T>()
                .Where(t => t.GetConstructor(Type.EmptyTypes) != null)
                .Select(t => (T)Activator.CreateInstance(t)!)
                .ToImmutableDictionary((f) => f!.Name, (f) => f)!;
        }

        private static IEnumerable<Type> GetAllTypesThatDirectlyImplements<T>()
        {
            var types = Assembly.GetExecutingAssembly().TypesImplementing<T>();
            return types.Where(t => t.GetInterfaces().Where(s => typeof(T).IsAssignableFrom(s)).Count() == 1);
        }
    }
}
