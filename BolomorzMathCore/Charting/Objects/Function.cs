namespace BolomorzMathCore.Charting;

/// <summary>
/// <code>
/// Function F
/// 
/// function f(x) = y assigning each input from a set of x-values exactly one output from a set of y-values
/// 
/// Properties:
/// - Type: FunctionType | type of function (Line, Polynomial, Exponential, Power, Logarithm)
/// - ShowFunction: Bool | is function currently shown in chart C ?
/// 
/// Getters:
/// - GetValues: Number[] | values representing the function
///     Line:           Values[0] + Values[1] * x;
///     Polynomial:     Sum[i from 0 to n](Values[i] * x^i)
///     Logarithm:      Values[0] + Values[1] * Math.Log(x);
///     Power:          Values[0] * Math.Pow(x, Values[1]);
///     Exponential:    Values[0] * Math.Pow(Values[1], x);
/// - GetValue(x): Number | value = f(x)
/// 
/// Static:
/// - NaF:                          f(x) = not a function
/// - Line(coeff0, coeff1):         f(x) = coeff0 + coeff1 * x
/// - Polynomial(coeff):            f(x) = Sum[i from 0 to n](coeff[i] * x^i)
/// - Exponential(coeff, base):     f(x) = coeff * base^x
/// - Power(coeff, expo):           f(x) = coeff * x^expo
/// - Logarithm(coeff0, coeff1):    f(x) = coeff0 + coeff1 * Log(x)
/// </code>
/// </summary>
public class Function
{
    protected double[] Values { get; set; }
    /// <summary>
    /// <code>
    /// GetValues: Number[] | values representing the function
    ///     Line:           Values[0] + Values[1] * x;
    ///     Polynomial:     Sum[i from 0 to n](Values[i] * x^i)
    ///     Logarithm:      Values[0] + Values[1] * Math.Log(x);
    ///     Power:          Values[0] * Math.Pow(x, Values[1]);
    ///     Exponential:    Values[0] * Math.Pow(Values[1], x);
    /// </code>
    /// </summary>
    public double[] GetValues() => [.. Values];
    public FunctionType Type { get; private set; }
    public bool ShowFunction { get; set; }


    internal Function(double[] values, FunctionType type)
    {
        Values = values;
        Type = type;
        ShowFunction = false;
    }

    /// <summary>
    /// <code>
    /// GetValue(x): Number | value = f(x)
    /// </code>
    /// </summary>
    public double GetValue(double xvalue)
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
    /// <code>
    /// f(x) = not a function
    /// </summary>
    public static Function NaF()
        => new([double.NaN], FunctionType.NaF);

    /// <summary>
    /// <code>
    /// f(x) = coeff0 + coeff1 * x
    /// </code>
    /// </summary>
    public static Function Line(double coeff0, double coeff1)
        => new([coeff0, coeff1], FunctionType.Line);

    /// <summary>
    /// <code>
    /// f(x) = Sum[i from 0 to n](coeff[i] * x^i)
    /// </code>
    /// </summary>
    public static Function Polynomial(double[] coeff)
        => new(coeff, FunctionType.Polynomial);

    /// <summary>
    /// <code>
    /// f(x) = coeff * base0^x
    /// </code>
    /// </summary>
    public static Function Exponential(double coeff, double base0)
        => new([coeff, base0], FunctionType.Exponential);

    /// <summary>
    /// <code>
    /// f(x) = coeff * x^expo
    /// </code>
    /// </summary>
    public static Function Power(double coeff, double expo)
        => new([coeff, expo], FunctionType.Power);

    /// <summary>
    /// <code>
    /// f(x) = coeff0 + coeff1 * Log(x)
    /// </code>
    /// </summary>
    public static Function Logarithm(double coeff0, double coeff1)
        => new([coeff0, coeff1], FunctionType.Logarithm);
}

/// <summary>
/// <code>
/// FunctionString
/// 
/// string representing on part of function F
/// 
/// Properties:
/// - Content: String | part of F
/// - SuperScript: Bool | content is displayed as superscript ?
/// </code>
/// </summary>
/// <see cref="Function"/> 
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

/// <summary>
/// <code>
/// FunctionStringCollection
/// 
/// collection of strings representing parts of function F
/// 
/// Getters:
/// - GetFunctionStrings: FunctionString[] | collection of strings representing parts of function F
/// </code>
/// </summary>
/// <see cref="FunctionString"/> 
/// <see cref="Function"/> 
public class FunctionStringCollection
{
    internal List<FunctionString> _FunctionStrings { get; set; } = [];
    public FunctionString[] GetFunctionStrings() => [.. _FunctionStrings];
}