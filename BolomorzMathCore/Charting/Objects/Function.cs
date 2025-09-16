namespace BolomorzMathCore.Charting;

public class Function
{
    protected double[] Values { get; set; }
    public double[] GetValues() => [.. Values];
    public FunctionType Type { get; private set; }
    public bool ShowFunction { get; set; }


    internal Function(double[] values, FunctionType type)
    {
        Values = values;
        Type = type;
        ShowFunction = false;
    }

    public double GetYValue(double xvalue)
    {
        switch (Type)
        {
            case FunctionType.Line:
                return Values[0] + Values[1] * xvalue;
            case FunctionType.Polynomial:
                double exp = 0; double y = 0;
                foreach (var coeff in Values)
                    y += coeff * Math.Pow(xvalue, exp++);
                return y;
            case FunctionType.Logarithm:
                return Values[0] + Values[1] * Math.Log(xvalue);
            case FunctionType.Power:
                return Values[0] * Math.Pow(xvalue, Values[1]);
            case FunctionType.Exponential:
                return Values[0] * Math.Pow(Values[1], xvalue);
            default:
                return double.NaN;
        }
    }

    /// <summary>
    /// f(x) = not a function
    /// </summary>
    /// <returns></returns>
    public static Function NaF()
        => new([double.NaN], FunctionType.NaF);

    /// <summary>
    /// f(x) = coeff0 + coeff1 * x
    /// </summary>
    /// <param name="coeff0"></param>
    /// <param name="coeff1"></param>
    /// <returns></returns>
    public static Function Line(double coeff0, double coeff1)
        => new([coeff0, coeff1], FunctionType.Line);

    /// <summary>
    /// f(x) = Sum[i from 0 to n](coeff[i] * x^i)
    /// </summary>
    /// <param name="coeff"></param>
    /// <returns></returns>
    public static Function Polynomial(double[] coeff)
        => new(coeff, FunctionType.Polynomial);

    /// <summary>
    /// f(x) = coeff * base0^x
    /// </summary>
    /// <param name="coeff"></param>
    /// <param name="base0"></param>
    /// <returns></returns>
    public static Function Exponential(double coeff, double base0)
        => new([coeff, base0], FunctionType.Exponential);

    /// <summary>
    /// f(x) = coeff * x^expo
    /// </summary>
    /// <param name="coeff"></param>
    /// <param name="expo"></param>
    /// <returns></returns>
    public static Function Power(double coeff, double expo)
        => new([coeff, expo], FunctionType.Power);

    /// <summary>
    /// f(x) = coeff0 + coeff1 * Log(x)
    /// </summary>
    /// <param name="coeff0"></param>
    /// <param name="coeff1"></param>
    /// <returns></returns>
    public static Function Logarithm(double coeff0, double coeff1)
        => new([coeff0, coeff1], FunctionType.Logarithm);
}

public class FunctionString
{
    public string Content { get; private set; }
    public bool SuperScript { get; private set; }

    internal FunctionString(string content, bool superscript)
    {
        Content = content;
        SuperScript = superscript;
    }
}

public class FunctionStringCollection
{
    internal List<FunctionString> _FunctionStrings { get; set; } = [];
    public FunctionString[] GetFunctionStrings() => [.. _FunctionStrings];
}