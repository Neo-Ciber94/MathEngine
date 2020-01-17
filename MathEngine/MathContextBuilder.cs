using ExtraUtils.MathEngine.Functions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ExtraUtils.MathEngine
{
    /// <summary>
    /// Provides a way for create custom <see cref="IMathContext"/>.
    /// </summary>
    public sealed class MathContextBuilder : ICloneable
    {
        private class BuildedMathContext : IMathContext
        {
            private readonly IReadOnlyDictionary<string, double> _values;
            private readonly IReadOnlyDictionary<string, IFunction> _functions;
            private readonly IReadOnlyDictionary<string, IBinaryOperator> _binaryOperators;
            private readonly IReadOnlyDictionary<string, IUnaryOperator> _unaryOperators;
            private readonly IReadOnlyDictionary<string, IInfixFunction> _infixFunctios;

            public BuildedMathContext(IReadOnlyDictionary<string, double> values,
                                     IReadOnlyDictionary<string, IFunction> functions,
                                     IReadOnlyDictionary<string, IBinaryOperator> binaryOperators,
                                     IReadOnlyDictionary<string, IUnaryOperator> unaryOperators,
                                     IReadOnlyDictionary<string, IInfixFunction> infixFunctios)
            {
                _values = values;
                _functions = functions;
                _binaryOperators = binaryOperators;
                _unaryOperators = unaryOperators;
                _infixFunctios = infixFunctios;
            }

            public double GetValue(string name) => _values[name];

            public bool IsBinaryOperator(string symbol) => _binaryOperators.ContainsKey(symbol);

            public bool IsFunction(string functionName) => _functions.ContainsKey(functionName);

            public bool IsUnaryOperator(string symbol) => _infixFunctios.ContainsKey(symbol);

            public bool IsValue(string name) => _values.ContainsKey(name);

            public bool TryGetBinaryOperator(string symbol, [NotNullWhen(true)] out IBinaryOperator? op)
            {
                return _binaryOperators.TryGetValue(symbol, out op);
            }

            public bool TryGetFunction(string functionName, [NotNullWhen(true)] out IFunction? func)
            {
                return _functions.TryGetValue(functionName, out func);
            }

            public bool TryGetUnaryOperator(string symbol, [NotNullWhen(true)] out IUnaryOperator? op)
            {
                return _unaryOperators.TryGetValue(symbol, out op);
            }
        }

        private readonly Dictionary<string, double> _values;
        private readonly Dictionary<string, IFunction> _functions;
        private readonly Dictionary<string, IBinaryOperator> _binaryOperators;
        private readonly Dictionary<string, IUnaryOperator> _unaryOperators;
        private readonly Dictionary<string, IInfixFunction> _infixFunctios;

        /// <summary>
        /// Initializes a new instance of the <see cref="MathContextBuilder"/> class.
        /// </summary>
        public MathContextBuilder()
        {
            _values = new Dictionary<string, double>();
            _functions = new Dictionary<string, IFunction>();
            _binaryOperators = new Dictionary<string, IBinaryOperator>();
            _unaryOperators = new Dictionary<string, IUnaryOperator>();
            _infixFunctios = new Dictionary<string, IInfixFunction>();
        }

        private MathContextBuilder(
            Dictionary<string, double> values,
            Dictionary<string, IFunction> functions,
            Dictionary<string, IBinaryOperator> binaryOperators,
            Dictionary<string, IUnaryOperator> unaryOperators,
            Dictionary<string, IInfixFunction> infixFunctios)
        {
            _values = values;
            _functions = functions;
            _binaryOperators = binaryOperators;
            _unaryOperators = unaryOperators;
            _infixFunctios = infixFunctios;
        }


        /// <summary>
        /// Adds or update the value of a variable.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">The value.</param>
        /// <returns>This <see cref="MathContextBuilder"/> instance.</returns>
        public MathContextBuilder AddOrUpdateVariable(string name, double value)
        {
            if(_values.TryAdd(name, value) is false)
            {
                _values[name] = value;
            }

            return this;
        }

        /// <summary>
        /// Adds a constant.
        /// </summary>
        /// <param name="name">The name of the constant.</param>
        /// <param name="value">The value.</param>
        /// <returns>This <see cref="MathContextBuilder"/> instance.</returns>
        /// <exception cref="ArgumentException">If the constant already exists.</exception>
        public MathContextBuilder AddConstant(string name, double value)
        {
            if (_values.TryAdd(name, value) is false)
            {
                throw new ArgumentException($"A constant '{name}' already exists.");
            }

            return this;
        }

        /// <summary>
        /// Adds a binary operator.
        /// </summary>
        /// <param name="name">The name of the operator.</param>
        /// <param name="binaryOperator">The binary operator.</param>
        /// <returns>This <see cref="MathContextBuilder"/> instance.</returns>
        /// <exception cref="ArgumentException">If the binary operator already exists.</exception>
        public MathContextBuilder AddBinaryOperator(string name, IBinaryOperator binaryOperator)
        {
            if(_binaryOperators.TryAdd(name, binaryOperator) is false)
            {
                throw new ArgumentException($"A binary operator named '{name}' already exists.");
            }

            return this;
        }

        /// <summary>
        /// Adds an unary operator.
        /// </summary>
        /// <param name="name">The name of the operator.</param>
        /// <param name="unaryOperator">The unary operator.</param>
        /// <returns>This <see cref="MathContextBuilder"/> instance.</returns>
        /// <exception cref="ArgumentException">If unary operator already exists.</exception>
        public MathContextBuilder AddUnaryOperator(string name, IUnaryOperator unaryOperator)
        {
            if (_unaryOperators.TryAdd(name, unaryOperator) is false)
            {
                throw new ArgumentException($"An unary operator named '{name}' already exists.");
            }

            return this;
        }

        /// <summary>
        /// Adds a function.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <param name="function">The function.</param>
        /// <returns>This <see cref="MathContextBuilder"/> instance.</returns>
        /// <exception cref="ArgumentException">If function already exists.</exception>
        public MathContextBuilder AddFunction(string name, IFunction function)
        {
            Debug.Assert(function is IBinaryOperator is false, "Binary operators should be added using 'AddBinaryOperator'");
            Debug.Assert(function is IUnaryOperator is false, "Unary operators should be added using 'AddUnaryOperator'");
            Debug.Assert(function is IInfixFunction is false, "Infix functions should be added using 'AddInfixFunction'");

            if (_functions.TryAdd(name, function) is false)
            {
                throw new ArgumentException($"A function named '{name}' already exists.");
            }

            return this;
        }

        /// <summary>
        /// Adds a infix function.
        /// </summary>
        /// <param name="name">The name of the infix function.</param>
        /// <param name="infixFunction">The infix function.</param>
        /// <returns>This <see cref="MathContextBuilder"/> instance.</returns>
        /// <exception cref="ArgumentException">If infix function already exists.</exception>
        public MathContextBuilder AddInfixFunction(string name, IInfixFunction infixFunction)
        {
            if (_infixFunctios.TryAdd(name, infixFunction) is false)
            {
                throw new ArgumentException($"An infix function named '{name}' already exists.");
            }

            return this;
        }

        /// <summary>
        /// Builds a <see cref="IMathContext"/> using this instance values.
        /// </summary>
        /// <returns>A new math context.</returns>
        public IMathContext Build()
        {
            return new BuildedMathContext(_values, _functions, _binaryOperators, _unaryOperators, _infixFunctios);
        }

        /// <summary>
        /// Clears the content of this instance.
        /// </summary>
        public void Clear()
        {
            _values.Clear();
            _functions.Clear();
            _infixFunctios.Clear();
            _binaryOperators.Clear();
            _unaryOperators.Clear();
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>A clone of this instance.</returns>
        public MathContextBuilder Clone()
        {
            var values = _values.ToDictionary(e => e.Key, e => e.Value);
            var functions = _functions.ToDictionary(e => e.Key, e => e.Value);
            var binaryOperators = _binaryOperators.ToDictionary(e => e.Key, e => e.Value);
            var unaryOperators = _unaryOperators.ToDictionary(e => e.Key, e => e.Value);
            var infixFunctios = _infixFunctios.ToDictionary(e => e.Key, e => e.Value);

            return new MathContextBuilder(values, functions, binaryOperators, unaryOperators, infixFunctios);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
