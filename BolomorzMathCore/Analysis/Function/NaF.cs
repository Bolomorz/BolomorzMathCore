using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Analysis.Functions;

public class NaF :
FunctionBase<Number, Number>
{
    private NaF() : base(Number.NaN, FunctionType.NaF) { }

    public override Number GetValue(Number xvalue)
        => Number.NaN;

    public override FunctionStringCollection GetStringCollection(int precision)
    {
        var fscoll = new FunctionStringCollection();
        fscoll._FunctionStrings.Add(new("NaF", false));
        return fscoll;
    }

    public static readonly NaF Default = new();
}