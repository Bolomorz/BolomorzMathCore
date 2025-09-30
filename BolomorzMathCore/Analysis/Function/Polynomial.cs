using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Analysis.Functions;

public class FPolynomial(Number[] coeff) :
FunctionBase<Number, Number[]>(coeff, FunctionType.Polynomial)
{
    public override FunctionStringCollection GetStringCollection(int precision)
    {
        var fscoll = new FunctionStringCollection();
        var coeff1 = Values[0].Round(precision);
        fscoll._FunctionStrings.Add(new($"y ={(coeff1 == 0 ? "" : $" {coeff1}")}", false));
        for (int i = 1; i < Values.Length; i++)
        {
            var coeff = Values[i].Round(precision);
            if (coeff == 0) continue;
            var o = coeff < 0 ? "-" : "+";
            fscoll._FunctionStrings.Add(new($"{(i == 1 && coeff1 == 0 ? "" : $" {(coeff < 0 ? "-" : "+")} {(coeff < 0 ? -coeff : coeff)} * x")}", false));
            fscoll._FunctionStrings.Add(new($"{i}", true));
        }
        return fscoll;
    }

    public override Number GetValue(Number xvalue)
    {
        Number exp = new(0);
        Number y = new(0);
        foreach (var coeff in Values)
        {
            y += coeff * xvalue.Pow(exp);
            exp += 1;
        }
        return y;
    }

    public static FPolynomial Regression(Number[] coeff)
        => new(coeff);
}