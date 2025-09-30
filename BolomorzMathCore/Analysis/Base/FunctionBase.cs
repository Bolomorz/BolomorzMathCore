using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Analysis;

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
/// - GetValues: values representing the function
///     Line:           Values[0] + Values[1] * x;
///     Polynomial:     Sum[i from 0 to n](Values[i] * x^i)
///     Logarithm:      Values[0] + Values[1] * Math.Log(x);
///     Power:          Values[0] * Math.Pow(x, Values[1]);
///     Exponential:    Values[0] * Math.Pow(Values[1], x);
/// - GetValue(x): Number | value = f(x)
/// - GetStringCollection(int precision): FunctionStringCollection | string represenation of function
/// </code>
/// </summary>
public interface IFunction<T> where T : class
{
    FunctionType Type { get; }
    bool ShowFunction { get; set; }
    T GetValue(T xvalue);
    FunctionStringCollection GetStringCollection(int precision);
}
public abstract class FunctionBase<T, U>(U values, FunctionType type) : IFunction<T> where T : class
{
    protected U Values = values;
    public U GetValues() => Values;
    public FunctionType Type { get; init; } = type;
    public bool ShowFunction { get; set; } = true;

    public abstract T GetValue(T xvalue);
    public abstract FunctionStringCollection GetStringCollection(int precision);
}