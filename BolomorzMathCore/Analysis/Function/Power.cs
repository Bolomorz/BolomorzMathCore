using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Analysis.Functions;

public class FPower(Number coeff, Number expo, Number c) :
FunctionBase<Number, (Number Coeff, Number Expo, Number C)>((coeff, expo, c), FunctionType.Power)
{
    public override FunctionStringCollection GetStringCollection(int precision)
    {
        var fscoll = new FunctionStringCollection();
        var coeff = Values.Coeff.Round(precision);
        var expo = Values.Expo.Round(precision);
        var c = Values.C.Round(precision);
        fscoll._FunctionStrings.Add(new($"y = {coeff} * (x", false));
        fscoll._FunctionStrings.Add(new($"{expo}", true));
        fscoll._FunctionStrings.Add(new($")", false));
        if (c != 0)
            fscoll._FunctionStrings.Add(new(c < 0 ? $" - {-c}" : $" + {c}", false));
        return fscoll;
    }

    public override Number GetValue(Number xvalue)
        => Values.Coeff * xvalue.Pow(Values.Expo) + Values.C;

    public static FPower Regression(Number coeff, Number expo)
        => new(coeff, expo, new(0));
}