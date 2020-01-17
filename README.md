## ExtraUtils.MathEngine

A library for evaluate math expressions.

### Implementation

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

### Usage
Import the library
```csharp
using ExtraUtils.MathEngine;
```

An expression can be evaluate as follow
```csharp
double result = MathEvaluator.Evaluate("5 + 3 * 2");
Console.WriteLine(result); // 10
```