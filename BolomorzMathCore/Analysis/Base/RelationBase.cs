using System.Drawing;
using BolomorzMathCore.Analysis.Algorithms;
using BolomorzMathCore.Analysis.Function;
using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Analysis;

/// <summary>
/// <code>
/// Relation R
/// 
/// relation assigning a y-value to a x-value inside a 2-d chart C
/// 
/// Properties:
/// - Axis: Axis | axis of C in either y direction
/// - ShowRelation: Bool | is series shown in C
/// - Precision: Number | precision of values in digits
/// - RegressionFunction: Function | function representing best-fit-function of relation
/// - Color: Color | color of relation inside C
/// 
/// Getters:
/// - GetPoints: NPoint[] | values of R
/// - GetFunction: FunctionStringCollection | get regression-function as collection of strings
/// 
/// Methods:
/// - AddPoint(x, y): add point to values
/// - Regression(type, order): calculate regression-function of type and order
///     Types: Line, Polynomial(order), Exponential, Power, Logarithm
/// </code>
/// </summary>
/// <see cref="Chart"/> 
/// <see cref="NPoint"/> 
/// <see cref="Analysis.Axis"/> 
/// <see cref="Regression"/> 
/// <see cref="FunctionBase{T, U}"/> 
/// <see cref="FunctionType"/> 
/// <see cref="FunctionStringCollection"/> 
/// <see cref="FunctionString"/> 
public abstract class RelationBase<T>(Color color, AxisBase<T> axis, IFunction<T> func) where T : class
{
    protected List<Point<T>> Points = [];
    public Point<T>[] GetPoints() => [.. Points.OrderBy(point => point.X)];
    public bool ShowRelation { get; set; } = true;
    public int Precision { get; set; } = 5;
    public AxisBase<T> Axis { get; set; } = axis;
    public IFunction<T> RegressionFunction { get; protected set; } = func;
    public Color Color { get; set; } = color;

    /// <summary>
    /// <code>
    /// AddPoint(x, y): add point to values
    /// 
    /// when a new-value is added to the relation this axis belongs to, 
    /// the default min/max values will be recalculated only if new-value is less than Min / more than Max
    /// - Max sets itself to next digit greater than new-value, if new-value > Max
    ///     f.e.    new-value = 15 => Max = DefaultMax = 20
    /// - Min sets itself to next digit less than new-value, but only if its at least 1/10 of Max, else its 0
    ///     f.e.    new-value = 15 AND Max = 20 => Min = DefaultMin = 10
    ///             new-value = 15 AND Max = 200 => Min = DefaultMin = 0 (not 10)
    /// </code>
    /// </summary>
    public abstract void AddPoint(T x, T y);
    /// <summary>
    /// <code>
    /// GetPoint(x): get y value to x value if x value exists
    /// </code>
    /// </summary>
    public abstract T? GetPoint(T xvalue);
    /// <summary>
    /// <code>
    /// Regression(type, order): calculate regression-function of type and order
    ///     Types: Line, Polynomial(order), Exponential, Power, Logarithm
    /// </code>
    /// </summary>
    public abstract void CalculateRegressionFunction(FunctionType type, int order);
    /// <summary>
    /// <code>
    /// GetFunction: FunctionStringCollection | get regression-function as collection of strings
    /// </code>
    /// </summary>
    public FunctionStringCollection GetFunctionAsString()
        => RegressionFunction.GetStringCollection(Precision, CompositionType.CompositeFunction);
}

public class NRelation(string name, string unit, Color color) :
RelationBase<Number>(color, new NAxis(name, unit, Number.Max, Number.Min), FConstant.NaF)
{
    public override void AddPoint(Number x, Number y)
    {
        Points.Add(new NPoint(x, y));
        Axis.CompareMax(y);
        Axis.CompareMin(y);
    }

    public override void CalculateRegressionFunction(FunctionType type, int order)
    {
        var reg = new Regression(Points);
        
        switch (type)
        {
            case FunctionType.Line:
                RegressionFunction = reg.LinearRegression().GetResult(); break;
            case FunctionType.Polynomial:
                RegressionFunction = reg.PolynomialRegression(order).GetResult();
                break;
            case FunctionType.Logarithm:
                RegressionFunction = reg.LogarithmicRegression().GetResult(); break;
            case FunctionType.Power:
                RegressionFunction = reg.PowerRegression().GetResult(); break;
            case FunctionType.Exponential:
                RegressionFunction = reg.ExponentialRegression().GetResult(); break;
            default:
                RegressionFunction = FConstant.NaF; break;
        }
    }

    public override Number? GetPoint(Number x)
    {
        foreach (var point in Points)
            if (point.X == x)
                return point.Y;
        return null;
    }
}
