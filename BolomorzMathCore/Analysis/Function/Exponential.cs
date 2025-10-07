using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Analysis.Function;

/// <summary>
/// <code>
/// Function F
/// 
/// function f(x) = y assigning each input from a set of x-values exactly one output from a set of y-values
/// 
/// Exponential: f(x) = a(x) * b(x)^x + c(x);
/// </code>
/// </summary>
public class FExponential(IFunction<Number> coeff, IFunction<Number> base0, IFunction<Number> c) :
FunctionBase<Number, (IFunction<Number> Coeff, IFunction<Number> Base, IFunction<Number> C)>((coeff, base0, c), FunctionType.Exponential)
{
    public override FunctionStringCollection GetStringCollection(int precision, CompositionType type)
    {
        var fscoll = new FunctionStringCollection(type);
        var coeff = Values.Coeff.GetStringCollection(precision, CompositionType.SubFunction);
        var expo = Values.Base.GetStringCollection(precision, CompositionType.SubFunction);
        var c = Values.C.GetStringCollection(precision, CompositionType.SubFunction);
        if (!coeff.IsEmpty())
        {
            fscoll.Add(coeff);
            fscoll.Add(new FunctionString(" * (", Script.Baseline));
        }
        if (!expo.IsEmpty())
        {
            fscoll.Add(expo);
            fscoll.Add(new FunctionString("x", Script.Superscript));
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
        var b = Values.Base.GetValue(xvalue);
        var c = Values.C.GetValue(xvalue);
        if (c1 is null && b is null && c is null) return null;
        return (c1 is not null ? c1 : Number.One) * (b is not null ? b.Pow(xvalue) : Number.One) + (c is not null ? c : Number.Zero);
    }

    public static FExponential Regression(Number coeff, Number base0)
        => new(new FConstant(coeff), new FConstant(base0), FConstant.NaF);
}