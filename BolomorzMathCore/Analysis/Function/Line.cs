using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Analysis.Functions;

public class FLine(Number coeff1, Number coeff2) :
FunctionBase<Number, (Number Coeff1, Number Coeff2)>((coeff1, coeff2), FunctionType.Line)
{
    public override FunctionStringCollection GetStringCollection(int precision)
    {
        var fscoll = new FunctionStringCollection();
        var coeff1 = Values.Coeff1.Round(precision);
        var coeff2 = Values.Coeff2.Round(precision);
        var o = coeff2 < 0 ? "-" : "+";
        if (coeff2 < 0) coeff2 *= -1;
        fscoll._FunctionStrings.Add(new($"y = {coeff1} {o} {coeff2} * x", false));
        return fscoll;
    }

    public override Number GetValue(Number xvalue)
        => Values.Coeff1 + Values.Coeff2 * xvalue;

    public static FLine Regression(Number coeff1, Number coeff2)
        => new(coeff1, coeff2);
}