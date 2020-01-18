# ExtraUtils.MathEngine [Experimental]

A library for evaluate math expressions.

## Implementation

Each expression to be evaluate pass for 3 steps:
- Tokenize
- Convert to RPN (Reverse Polish Notation)
- Evaluation

In this steps are involved the ``ITokenizer``, ``IMathContext`` and ``MathEvaluator``.

The ``ITokenizer`` extracts the tokens from a ``string`` and use the
``IMathContext`` to determines which of the tokens are variables or contants,
functions, infix functions, binary operators and unary operators.

Then the tokens are converted to [RPN (Reverse Polish Notation)](https://en.wikipedia.org/wiki/Reverse_Polish_notation)
using the ``MathEvaluator.InfixToRPN(Token[])`` method and last
the values are evaluated to get the result as ``double``.

A default implementation of ``ITokenizer`` and ``IMathContext``
is provided by ``Tokenizer.Default`` and ``MathContext.Default``.
The default tokenizer can detect *numbers* as ``doubles`` or ``int``,
*binary* and *unary operators* as ``char`` and *variables*, *constants*, *functions* and *infix functions*
as ``string``.

And the default math context provides functions operations as: add, substract,
multiply, divide, factorial, logarithm, natural logaritm, sine, cosine, tangent, and others.

The library contains a large set of functions and operators as:

| Name | Class Name |
| ---- | -------------------------- |
| Sine | SineFunction               |
| Cosine | CosineFunction           |
| Natural Logarithm  | LnFunction   |
| Logarithm | LogFunction           |
| Sqrt  | SqrtFunction              |
| Round | RoundFunction             |
| Factorial | FactorialOperator     |

All these and others are found in ``ExtraUtils.MathEngine.Functions.Common``.

## Usage
Import the library
```csharp
using ExtraUtils.MathEngine;
```

An expression can be evaluate as follow
```csharp
double result = MathEvaluator.Evaluate("5 + 3 * 2");
Console.WriteLine(result); // 11
```

If the evaluation fails an ``ExpressionEvaluationException`` will
be throw.

```csharp
double result = MathEvaluator.Evaluate("5 + 3 ** 2");
Console.WriteLine(result); // ExpressionEvaluationException
```

There is a large set of functions as infix functions
```csharp
double result = MathEvaluator.Evaluate("5 plus 3 times 2");
Console.WriteLine(result); // 11
```

And also can evaluate complicate expressions
```csharp
double result = MathEvaluator.Evaluate("Sin(35) * (2^5 + (3 * 4 / 10))");
Console.WriteLine(result); // 19.042737686854732
```

Also you can use variables by using a custom ``IMathContext``
```csharp
IMathContext context = new MathContext(("x", 10), ("y", 5));

double result = MathEvaluator.Evaluate("x + y", context);
Console.WriteLine(result); // 15

// MathContext is immutable, so you should instantiate a new one
context  = new MathContext(("x", 20), ("y", 5));

result = MathEvaluator.Evaluate("x + y", context);
Console.WriteLine(result); // 25
```

## Extendibility

New functions and operators can be added by implementing
``IFunction``, ``IInfixFunction``, ``IBinaryOperator``, ``IUnaryOperator``,
the system use *reflection* to locate all the implementations.
