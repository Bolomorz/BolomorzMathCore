using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Analysis.Functions;

public class FPolynomial(IFunction<Number>[] coeff) :
FunctionBase<Number, IFunction<Number>[]>(coeff, FunctionType.Polynomial)
{
    public override FunctionStringCollection GetStringCollection(int precision, CompositionType type)
    {
        var fscoll = new FunctionStringCollection(type);
        var coeff1 = Values[0].GetStringCollection(precision, CompositionType.SubFunction);
        if (!coeff1.IsEmpty())
        {
            fscoll.Add(coeff1);
        }
        for (int i = 1; i < Values.Length; i++)
        {
            var coeff = Values[i].GetStringCollection(precision, CompositionType.SubFunction);
            if (!coeff.IsEmpty())
            {
                fscoll.Add(new FunctionString(" + ", Script.Baseline));
                fscoll.Add(coeff);
                fscoll.Add(new FunctionString(" * x", Script.Baseline));
                fscoll.Add(new FunctionString($"{i}", Script.Superscript));
            }
        }
        if (type == CompositionType.CompositeFunction)
            fscoll.Reduce();
        return fscoll;
    }

    public override Number? GetValue(Number xvalue)
    {
        Number exp = new(0);
        Number y = new(0);
        var count = 0;
        foreach (var coeff in Values)
        {
            var c = coeff.GetValue(xvalue);
            if (c is not null)
            {
                y += c * xvalue.Pow(exp);
                count++;
            }
            exp += 1;
        }
        return count > 0 ? y : null;
    }

    public static FPolynomial Regression(Number[] coeffn)
    {
        List<IFunction<Number>> coeff = [];
        foreach (var coeffi in coeffn)
            coeff.Add(new FConstant(coeffi));
        return new([.. coeff]);
    }
}