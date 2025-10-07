using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Analysis.Function;

/// <summary>
/// <code>
/// Function F
/// 
/// function f(x) = y assigning each input from a set of x-values exactly one output from a set of y-values
/// 
/// Constant: f(x) = a; a is Number
/// </code>
/// </summary>
public class FConstant(Number coeff) :
FunctionBase<Number, Number>(coeff, FunctionType.Constant)
{
    public override Number? GetValue(Number xvalue)
        => Values == Number.NaN ? null : Values;

    public override FunctionStringCollection GetStringCollection(int precision, CompositionType type)
    {
        var fscoll = new FunctionStringCollection(type);
        if (Values != Number.NaN)
        {
            var value = Values.Round(precision);
            fscoll.Add(new FunctionString($"{value}", Script.Baseline));
        }
        return fscoll;
    }

    public static readonly FConstant NaF = new(Number.NaN);
}