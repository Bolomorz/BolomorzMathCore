using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Analysis.Functions;

public class FLogarithm(Number coeff1, Number coeff2, Number innerCoeff1, Number innerCoeff2, Number c) :
FunctionBase<Number, (Number Coeff1, Number Coeff2, Number InnerCoeff1, Number InnerCoeff2, Number C)>((coeff1, coeff2, innerCoeff1, innerCoeff2, c), FunctionType.Logarithm)
{
    public override FunctionStringCollection GetStringCollection(int precision)
    {
        var fscoll = new FunctionStringCollection();
        var coeff1 = Values.Coeff1.Round(precision);
        var coeff2 = Values.Coeff2.Round(precision);
        var inner1 = Values.InnerCoeff1.Round(precision);
        var inner2 = Values.InnerCoeff2.Round(precision);
        var c = Values.C.Round(precision);
        var outer = coeff1 == 0 ?
                        $"{coeff2}" :
                        $"{coeff1} {(coeff2 < 0 ? "-" : "+")} {(coeff2 < 0 ? -coeff2 : coeff2)} * ";
        var inner = inner1 == 0 ?
                        $"ln({(inner2 == 1 ? "x" : $"{inner2} * x")})" :
                        $"ln({inner1} {(inner2 < 0 ? "-" : "+")} {(inner2 < 0 ? -inner2 : inner2 == 1 ? "" : inner2)} * x)";
        fscoll._FunctionStrings.Add(new($"y = {outer}{inner}", false));
        if (c != 0)
            fscoll._FunctionStrings.Add(new(c < 0 ? $" - {-c}" : $" + {c}", false));
        return fscoll;
    }

    public override Number GetValue(Number xvalue)
        => Values.Coeff1 + Values.Coeff2 * Math.Log((Values.InnerCoeff1 + Values.InnerCoeff2 * xvalue).Re) + Values.C;

    public static FLogarithm Regression(Number coeff1, Number coeff2)
        => new(coeff1, coeff2, new(0), new(1), new(0));
}
