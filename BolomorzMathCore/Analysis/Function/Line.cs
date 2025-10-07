using System.Formats.Asn1;
using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Analysis.Function;

/// <summary>
/// <code>
/// Function F
/// 
/// function f(x) = y assigning each input from a set of x-values exactly one output from a set of y-values
/// 
/// Line: f(x) = a(x) + b(x) * x;
/// </code>
/// </summary>
public class FLine(IFunction<Number> coeff1, IFunction<Number> coeff2) :
FunctionBase<Number, (IFunction<Number> Coeff1, IFunction<Number> Coeff2)>((coeff1, coeff2), FunctionType.Line)
{
    public override FunctionStringCollection GetStringCollection(int precision, CompositionType type)
    {
        var fscoll = new FunctionStringCollection(type);
        var coeff1 = Values.Coeff1.GetStringCollection(precision, CompositionType.SubFunction);
        var coeff2 = Values.Coeff2.GetStringCollection(precision, CompositionType.SubFunction);
        if (!coeff1.IsEmpty())
        {
            fscoll.Add(coeff1);
            fscoll.Add(new FunctionString(" + ", Script.Baseline));
        }
        if (!coeff2.IsEmpty())
        {
            fscoll.Add(coeff2);
            fscoll.Add(new FunctionString(" * x", Script.Baseline));
        }
        else
        {
            fscoll.Add(new FunctionString("x", Script.Baseline));
        }
        if (type == CompositionType.CompositeFunction)
            fscoll.Reduce();
        return fscoll;
    }

    public override Number? GetValue(Number xvalue)
    {
        var c1 = Values.Coeff1.GetValue(xvalue);
        var c2 = Values.Coeff2.GetValue(xvalue);
        if (c1 is null && c2 is null) return null;
        return (c1 is not null ? c1 : Number.Zero) + (c2 is not null ? c2 : Number.One) * xvalue;
    }

    public static FLine Regression(Number coeff1, Number coeff2)
        => new(new FConstant(coeff1), new FConstant(coeff2));
}