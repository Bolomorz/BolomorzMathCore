using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Analysis.Function;

/// <summary>
/// <code>
/// Function F
/// 
/// function f(x) = y assigning each input from a set of x-values exactly one output from a set of y-values
/// 
/// Power: f(x) = a(x) * x^b(x) + c(x);
/// </code>
/// </summary>
public class FPower(IFunction<Number> coeff, IFunction<Number> expo, IFunction<Number> c) :
FunctionBase<Number, (IFunction<Number> Coeff, IFunction<Number> Expo, IFunction<Number> C)>((coeff, expo, c), FunctionType.Power)
{
    public override FunctionStringCollection GetStringCollection(int precision, CompositionType type)
    {
        var fscoll = new FunctionStringCollection(type);
        var coeff = Values.Coeff.GetStringCollection(precision, CompositionType.SubFunction);
        var expo = Values.Expo.GetStringCollection(precision, CompositionType.SubFunction);
        var c = Values.C.GetStringCollection(precision, CompositionType.SubFunction);
        if (!coeff.IsEmpty())
        {
            fscoll.Add(coeff);
            fscoll.Add(new FunctionString(" * (x", Script.Baseline));

        }
        else
        {
            fscoll.Add(new FunctionString("x", Script.Baseline));
        }
        if (!expo.IsEmpty())
        {
            fscoll.Add(expo, Script.Superscript);
        }
        if (!coeff.IsEmpty())
        {
            fscoll.Add(new FunctionString(")", Script.Baseline));

        }
        if (!c.IsEmpty())
        {
            fscoll.Add(new FunctionString(" + ", Script.Baseline));
            fscoll.Add(c);
        }
        if (type == CompositionType.CompositeFunction)
            fscoll.Reduce();
        return fscoll;
    }

    public override Number? GetValue(Number xvalue)
    {
        var c1 = Values.Coeff.GetValue(xvalue);
        var e = Values.Expo.GetValue(xvalue);
        var c = Values.C.GetValue(xvalue);
        if (c1 is null && e is null && c is null) return null;
        return (c1 is not null ? c1 : Number.One) * (e is not null ? xvalue.Pow(e) : Number.One) + (c is not null ? c : Number.Zero);
    }

    public static FPower Regression(Number coeff, Number expo)
        => new(new FConstant(coeff), new FConstant(expo), FConstant.NaF);
}