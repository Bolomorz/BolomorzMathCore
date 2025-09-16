using System.Drawing;
using BolomorzMathCore.Charting.Algorithms;

namespace BolomorzMathCore.Charting;

public class Series(string name, string unit, Color color)
{
    private List<SeriesPoint> _Values = [];
    public SeriesPoint[] GetValues() => [.. _Values];

    public Axis Axis { get; private set; } = new(name, unit, double.MaxValue, double.MinValue);
    public bool Active { get; set; } = true;
    public int Precision { get; set; } = 5;
    public Function Function { get; private set; } = Function.NaF();
    public Color Color { get; set; } = color;

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
        var fscoll = new FunctionStringCollection();
        var values = Function.GetValues();

        switch (Function.Type)
        {
            case FunctionType.Line:
                var l1 = Math.Round(values[0], Precision);
                var l2 = Math.Round(values[1], Precision);
                var lo = l2 < 0 ? "-" : "+";
                if (l2 < 0) l2 *= -1;
                var line = $"y = {l1} {lo} {l2}";
                fscoll._FunctionStrings.Add(new(line, false));
                break;

            case FunctionType.Exponential:
                var e1 = Math.Round(values[0], Precision);
                var e2 = Math.Round(values[1], Precision);
                var expbase = $"y = {e1} * {e2}";
                var expsuper = "x";
                fscoll._FunctionStrings.Add(new(expbase, false));
                fscoll._FunctionStrings.Add(new(expsuper, true));
                break;

            case FunctionType.Logarithm:
                var log1 = Math.Round(values[0], Precision);
                var log2 = Math.Round(values[1], Precision);
                var logo = log2 < 0 ? "-" : "+";
                if (log2 < 0) log2 *= -1;
                var log = $"y = {log1} {logo} {log2} * ln(x)";
                fscoll._FunctionStrings.Add(new(log, false));
                break;

            case FunctionType.Power:
                var pow1 = Math.Round(values[0], Precision);
                var pow2 = Math.Round(values[1], Precision);
                var powbase = $"y = {pow1} * x";
                var powsuper = $"{pow2}";
                fscoll._FunctionStrings.Add(new(powbase, false));
                fscoll._FunctionStrings.Add(new(powsuper, true));
                break;

            case FunctionType.Polynomial:
                var pol = $"y = {Math.Round(values[0], Precision)}";
                fscoll._FunctionStrings.Add(new(pol, false));
                for (int i = 1; i < values.Length; i++)
                {
                    var p1 = Math.Round(values[i], Precision);
                    var po = p1 < 0 ? "-" : "+";
                    if (p1 < 0) p1 *= -1;
                    var pbase = $" {po} {p1} * x";
                    var psuper = $"{i}";
                    fscoll._FunctionStrings.Add(new(pbase, false));
                    fscoll._FunctionStrings.Add(new(psuper, true));
                }
                break;

            default:
                fscoll._FunctionStrings.Add(new("NaF", false));
                break;
        }

        return fscoll;
    }
}