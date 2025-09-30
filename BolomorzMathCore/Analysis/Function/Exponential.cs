using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Analysis.Functions;

public class FExponential(Number coeff, Number base0, Number c) :
FunctionBase<Number, (Number Coeff, Number Base, Number C)>((coeff, base0, c), FunctionType.Exponential)
{
    public override FunctionStringCollection GetStringCollection(int precision)
    {
        var fscoll = new FunctionStringCollection();
        var coeff = Values.Coeff.Round(precision);
        var expo = Values.Base.Round(precision);
        var c = Values.C.Round(precision);
        fscoll._FunctionStrings.Add(new($"y = {coeff} * ({expo}", false));
        fscoll._FunctionStrings.Add(new($"x", true));
        fscoll._FunctionStrings.Add(new($")", false));
        if (c != 0)
            fscoll._FunctionStrings.Add(new(c < 0 ? $" - {-c}" : $" + {c}", false));
        return fscoll;
    }

    public override Number GetValue(Number xvalue)
        => Values.Coeff * Values.Base.Pow(xvalue) + Values.C;

    public static FExponential Regression(Number coeff, Number base0)
        => new(coeff, base0, new(0));
}