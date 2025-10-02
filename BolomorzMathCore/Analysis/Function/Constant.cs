using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Analysis.Function;

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
            fscoll._FunctionStrings.Add(new($"{value}", Script.Baseline));
        }
        return fscoll;
    }

    public static readonly FConstant NaF = new(Number.NaN);
}