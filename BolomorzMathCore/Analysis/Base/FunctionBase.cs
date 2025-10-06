namespace BolomorzMathCore.Analysis;

/// <summary>
/// <code>
/// Function F{T} where T Number|Complex
/// 
/// function f(x) = y assigning each input from a set of x-values exactly one output from a set of y-values
/// 
/// Properties:
/// - Type: FunctionType | type of function (Line, Polynomial, Exponential, Power, Logarithm)
/// - ShowFunction: Bool | is function currently shown in chart C ?
/// 
/// Getters:
/// - GetValue(x): Number | value = f(x)
/// - GetStringCollection(int precision): FunctionStringCollection | string representation of function
/// </code>
/// </summary>
/// <typeparam name="T">Number|Complex</typeparam>
public interface IFunction<T> where T : class
{
    FunctionType Type { get; }
    bool ShowFunction { get; set; }
    T? GetValue(T xvalue);
    FunctionStringCollection GetStringCollection(int precision, CompositionType type);
}
/// <summary>
/// <code>
/// FunctionBase F{T, U} where T Number|Complex; U Tuple(coeff|subfunction)
/// 
/// function f(x) = y assigning each input from a set of x-values exactly one output from a set of y-values
/// 
/// Properties:
/// - Type: FunctionType | type of function (Line, Polynomial, Exponential, Power, Logarithm)
/// - ShowFunction: Bool | is function currently shown in chart C ?
/// 
/// Getters:
/// - GetValues: values representing the function
/// - GetValue(x): Number | value = f(x)
/// - GetStringCollection(int precision): FunctionStringCollection | string representation of function
/// </code>
/// </summary>
/// <typeparam name="T">Number|Complex</typeparam>
/// <typeparam name="U">Tuple(coeff|subfunctions)</typeparam>
public abstract class FunctionBase<T, U>(U values, FunctionType type) : IFunction<T> where T : class
{
    protected U Values = values;
    public U GetValues() => Values;
    public FunctionType Type { get; init; } = type;
    public bool ShowFunction { get; set; } = true;

    public abstract T? GetValue(T xvalue);
    public abstract FunctionStringCollection GetStringCollection(int precision, CompositionType type);
}