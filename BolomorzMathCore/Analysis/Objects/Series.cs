using System.Drawing;
using BolomorzMathCore.Analysis.Algorithms;
using BolomorzMathCore.Analysis.Function;
using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Analysis;

/// <summary>
/// <code>
/// Series S
/// 
/// series assigning a y-value to a x-value inside chart C
/// 
/// Properties:
/// - Axis: Axis | axis of C in either y direction
/// - Active: Bool | is series shown in C
/// - Precision: Number | precision of values in digits
/// - Function: Function | function representing regression of values
/// - Color: Color | color of series inside C
/// 
/// Getters:
/// - GetValues: SeriesPoint[] | values of S
/// - GetFunction: FunctionStringCollection | get regression-function as collection of strings
/// 
/// Methods:
/// - AddPoint(x, y): add point to values
/// - Regression(type, order): calculate regression-function of type and order
///     Types: Line, Polynomial(order), Exponential, Power, Logarithm
/// </code>
/// </summary>
/// <see cref="Chart"/> 
/// <see cref="SeriesPoint"/> 
/// <see cref="Analysis.Axis"/> 
/// <see cref="Algorithms.Regression"/> 
/// <see cref="Analysis.FunctionBase{T, U}"/> 
/// <see cref="FunctionType"/> 
/// <see cref="FunctionStringCollection"/> 
/// <see cref="FunctionString"/> 
public class Series(string name, string unit, Color color)
{
    private List<SeriesPoint> _Values = [];
    /// <summary>
    /// <code>
    /// GetValues: SeriesPoint[] | values of S
    /// </code>
    /// </summary>
    public SeriesPoint[] GetValues() => [.. _Values];

    public Axis Axis { get; private set; } = new(name, unit, new(double.MaxValue), new(double.MinValue));
    public bool Active { get; set; } = true;
    public int Precision { get; set; } = 5;
    public IFunction<Number> Function { get; private set; } = FConstant.NaF;
    public Color Color { get; set; } = color;

    /// <summary>
    /// <code>
    /// AddPoint(x, y): add point to values
    /// </code>
    /// </summary>
    public void AddPoint(Number x, Number y)
    {
        _Values.Add(new() { X = x, Y = y });
        Axis.CompareMax(y);
        Axis.CompareMin(y);
    }

    /// <summary>
    /// <code>
    /// Regression(type, order): calculate regression-function of type and order
    ///     Types: Line, Polynomial(order), Exponential, Power, Logarithm
    /// </code>
    /// </summary>
    public void Regression(FunctionType type, int order)
    {
        var reg = new Regression(_Values);

        switch (type)
        {
            case FunctionType.Line:
                Function = reg.LinearRegression().GetResult(); break;
            case FunctionType.Polynomial:
                Function = reg.PolynomialRegression(order).GetResult();
                break;
            case FunctionType.Logarithm:
                Function = reg.LogarithmicRegression().GetResult(); break;
            case FunctionType.Power:
                Function = reg.PowerRegression().GetResult(); break;
            case FunctionType.Exponential:
                Function = reg.ExponentialRegression().GetResult(); break;
            default:
                Function = FConstant.NaF; break;
        }
    }

    /// <summary>
    /// <code>
    /// GetFunction: FunctionStringCollection | get regression-function as collection of strings
    /// </code>
    /// </summary>
    public FunctionStringCollection GetFunction()
        => Function.GetStringCollection(Precision, CompositionType.CompositeFunction);
}