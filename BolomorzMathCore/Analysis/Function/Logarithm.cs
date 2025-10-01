using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Analysis.Functions;

public class FLogarithm(IFunction<Number> coeff1, IFunction<Number> coeff2, IFunction<Number> innerCoeff1, IFunction<Number> innerCoeff2, IFunction<Number> c) :
FunctionBase<Number, (IFunction<Number> Coeff1, IFunction<Number> Coeff2, IFunction<Number> InnerCoeff1, IFunction<Number> InnerCoeff2, IFunction<Number> C)>((coeff1, coeff2, innerCoeff1, innerCoeff2, c), FunctionType.Logarithm)
{
    public override FunctionStringCollection GetStringCollection(int precision, CompositionType type)
    {
        var fscoll = new FunctionStringCollection(type);
        var coeff1 = Values.Coeff1.GetStringCollection(precision, CompositionType.SubFunction);
        var coeff2 = Values.Coeff2.GetStringCollection(precision, CompositionType.SubFunction);
        var inner1 = Values.InnerCoeff1.GetStringCollection(precision, CompositionType.SubFunction);
        var inner2 = Values.InnerCoeff2.GetStringCollection(precision, CompositionType.SubFunction);
        var c = Values.C.GetStringCollection(precision, CompositionType.SubFunction);
        if (!coeff1.IsEmpty())
        {
            fscoll.Add(coeff1);
            fscoll.Add(new FunctionString(" + ", Script.Baseline));
        }
        if (!coeff2.IsEmpty())
        {
            fscoll.Add(coeff2);
            fscoll.Add(new FunctionString(" * ln(", Script.Baseline));
        }
        if (!inner1.IsEmpty())
        {
            fscoll.Add(inner1);
            fscoll.Add(new FunctionString(" + ", Script.Baseline));
        }
        if (!inner2.IsEmpty())
        {
            fscoll.Add(inner2);
            fscoll.Add(new FunctionString(" * x)", Script.Baseline));
        }
        else
        {
            fscoll.Add(new FunctionString("x)", Script.Baseline));
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
        var c1 = Values.Coeff1.GetValue(xvalue);
        var c2 = Values.Coeff2.GetValue(xvalue);
        var i1 = Values.InnerCoeff1.GetValue(xvalue);
        var i2 = Values.InnerCoeff2.GetValue(xvalue);
        var c = Values.C.GetValue(xvalue);
        var log = (i1 is not null ? i1 : Number.Zero) + (i2 is not null ? i2 : Number.Zero) * xvalue;
        if (c1 is null && c2 is null && i1 is null && i2 is null && c is null) return null;
        return (c1 is not null ? c1 : Number.Zero) + (c2 is not null ? c2 : Number.One) * (i1 is not null || i2 is not null ? Math.Log(log.Re) : 1) + (c is not null ? c : Number.Zero);
    }

    public static FLogarithm Regression(Number coeff1, Number coeff2)
        => new(new FConstant(coeff1), new FConstant(coeff2), FConstant.NaF, FConstant.NaF, FConstant.NaF);
}
