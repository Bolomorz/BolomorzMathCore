using System.Drawing;
using BolomorzMathCore.Charting.Algorithms;

namespace BolomorzMathCore.Charting;

public class Series
{
    private List<SeriesPoint> _Values;
    public SeriesPoint[] GetValues() => [.. _Values];

    public Axis Axis { get; private set; }
    public bool Active { get; set; }
    public int Precision { get; set; }
    public Function Function { get; private set; }
    public Color Color { get; set; }

    public Series(string name, string unit, Color color)
    {
        Axis = new(name, unit, double.MaxValue, double.MinValue);
        Active = true;
        Function = Function.NaF();
        Precision = 5;
        _Values = [];
        Color = color;
    }

    public void AddPoint(double x, double y)
    {
        _Values.Add(new() { X = x, Y = y });
        Axis.CompareMax(y);
        Axis.CompareMin(y);
    }

    public void Regression(FunctionType type, int order)
    {
        var xvalues = new List<double>();
        var yvalues = new List<double>();
        foreach (var value in _Values.OrderBy(value => value.X))
        {
            xvalues.Add(value.X);
            yvalues.Add(value.Y);
        }
        var reg = new Regression(xvalues, yvalues);

        switch (type)
        {
            case FunctionType.Line:
                Function = reg.LinearRegression(); break;
            case FunctionType.Polynomial:
                var preg = reg.PolynomialRegression(order);
                Function = double.IsNaN(preg.GetValues()[0]) ? Function.NaF() : preg; break;
            case FunctionType.Logarithm:
                Function = reg.LogarithmicRegression(); break;
            case FunctionType.Power:
                Function = reg.PowerRegression(); break;
            case FunctionType.Exponential:
                Function = reg.ExponentialRegression(); break;
            default:
                Function = Function.NaF(); break; 
        }
    }

    public FunctionStringCollection GetFunction()
    {
        return new();
    }
}