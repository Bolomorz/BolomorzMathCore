using BolomorzMathCore.Analysis.Function;
using BolomorzMathCore.Basics;
using BolomorzMathCore.LinearAlgebra.Matrix;

namespace BolomorzMathCore.Analysis.Algorithms;

/// <summary>
/// <code>
/// Regression Algorithm
/// 
/// finding the best fit for a function representing a x-y-relation (Series, Scatter, etc.)
/// relation and resulting function belong to the same 2-dimensional system
/// 
/// Function Options:
/// - Line:         f(x) = coeff0 + coeff1 * x
/// - Polynomial:   f(x) = Sum[i from 0 to n](coeff[i] * x^i)
/// - Power:        f(x) = coeff * x^expo
/// - Logarithm:    f(x) = coeff0 + coeff1 * ln(x)
/// - Exponential:  f(x) = coeff * base^x
/// 
/// Input: (x, y)-pairs | relation
/// Output: Function
/// 
/// Algorithm: 
/// number of pairs: N
/// pair i: N(i) [i from 1 to N]
/// resulting function: R(x)
/// 
/// 1. Line, Power, Logarithm, Exponential
/// finding R(x) for Line, Power, Logarithm, Exponential can be done in '6N + 10' operations
/// 
/// 
/// 2. Polynomial
/// 
/// </code>
/// </summary>
/// <see cref="FunctionBase{Number, U}"/> 
public class Regression(List<Point<Number>> points) : AlgorithmBase<List<Point<Number>>, IFunction<Number>>(points, FConstant.NaF)
{
    public Regression PolynomialRegression(int order)
    {
        var reg = RegressionAlgorithms.PolynomialRegression(order, Input);
        Result = reg is not null ? FPolynomial.Regression(reg) : FConstant.NaF;
        return this;
    }
    public Regression LinearRegression()
    {
        var reg = RegressionAlgorithms.LinearRegression(Input);
        Result = reg is not null ? FLine.Regression(reg[0], reg[1]) : FConstant.NaF;
        return this;
    }
    public Regression PowerRegression()
    {
        var reg = RegressionAlgorithms.PowerRegression(Input);
        Result = reg is not null ? FPower.Regression(reg[0], reg[1]) : FConstant.NaF;
        return this;
    }
    public Regression LogarithmicRegression()
    {
        var reg = RegressionAlgorithms.LogarithmicRegression(Input);
        Result = reg is not null ? FLogarithm.Regression(reg[0], reg[1]) : FConstant.NaF;
        return this;
    }
    public Regression ExponentialRegression()
    {
        var reg = RegressionAlgorithms.ExponentialRegression(Input);
        Result = reg is not null ? FExponential.Regression(reg[0], reg[1]) : FConstant.NaF;
        return this;
    }
}

internal static class RegressionAlgorithms
{

    internal static Number[]? LinearRegression(List<Point<Number>> points)
    {
        int n = points.Count;

        Number sumX = new(0);
        Number sumXX = new(0);
        Number sumY = new(0);
        Number sumXY = new(0);

        for (int i = 0; i < n; i++)
        {
            sumX += points[i].X;
            sumXX += points[i].X * points[i].X;
            sumY += points[i].Y;
            sumXY += points[i].X * points[i].Y;
        }

        Number b = (n * sumXY - sumX * sumY) / (n * sumXX - sumX * sumX);
        Number a = (sumY - b * sumX) / n;

        return [a, b];
    }

    internal static Number[]? PowerRegression(List<Point<Number>> points)
    {
        int n = points.Count;

        Number sumX = new(0);
        Number sumXX = new(0);
        Number sumY = new(0);
        Number sumXY = new(0);

        for (int i = 0; i < n; i++)
        {
            sumX += Math.Log(points[i].X.Re);
            sumXX += Math.Log(points[i].X.Re) * Math.Log(points[i].X.Re);
            sumY += Math.Log(points[i].Y.Re);
            sumXY += Math.Log(points[i].X.Re) * Math.Log(points[i].Y.Re);
        }

        Number b = (n * sumXY - sumX * sumY) / (n * sumXX - sumX * sumX);
        Number a = (sumY - b * sumX) / n;

        return [a.Exp(), b];
    }

    internal static Number[]? ExponentialRegression(List<Point<Number>> points)
    {
        int n = points.Count;

        Number sumX = new(0);
        Number sumXX = new(0);
        Number sumY = new(0);
        Number sumXY = new(0);

        for (int i = 0; i < n; i++)
        {
            sumX += points[i].X;
            sumXX += points[i].X * points[i].X;
            sumY += Math.Log(points[i].Y.Re);
            sumXY += points[i].X * Math.Log(points[i].Y.Re);
        }

        Number b = (n * sumXY - sumX * sumY) / (n * sumXX - sumX * sumX);
        Number a = (sumY - b * sumX) / n;

        return [a.Exp(), b.Exp()];
    }

    internal static Number[]? LogarithmicRegression(List<Point<Number>> points)
    {
        int n = points.Count;

        Number sumX = new(0);
        Number sumXX = new(0);
        Number sumY = new(0);
        Number sumXY = new(0);

        for (int i = 0; i < n; i++)
        {
            sumX += Math.Log(points[i].X.Re);
            sumXX += Math.Log(points[i].X.Re) * Math.Log(points[i].X.Re);
            sumY += points[i].Y;
            sumXY += Math.Log(points[i].X.Re) * points[i].Y;
        }

        Number b = (n * sumXY - sumX * sumY) / (n * sumXX - sumX * sumX);
        Number a = (sumY - b * sumX) / n;

        return [a, b];
    }

    internal static Number[]? PolynomialRegression(int order, List<Point<Number>> points)
    {
        int n = order + 1;
        int m = points.Count;
        if(m < n+1) return null;

        Number[,] acmatrix = new Number[n, n + 1];
        for(int i = 1; i <=n; i++)
        {
            Number sum;
            for(int j = 1; j <= i; j++)
            {
                Number k = new(i + j - 2);
                sum = new(0);
                for(int l = 1; l <= m; l++) sum += points[l-1].X.Pow(k);
                acmatrix[i-1, j-1] = sum;
                acmatrix[j-1, i-1] = sum;
            }
            sum = new(0);
            for(int l = 1; l <= m; l++) sum += points[l-1].Y * points[l-1].X.Pow(new(i-1));
            acmatrix[i-1, n] = sum;
        }

        NMatrix matrix = new(acmatrix);
        matrix.ApplyGaussJordanElimination();

        Number[] solution = new Number[n];
        for(int i = 1; i <= n; i++) solution[i] = matrix.GetValue(i, n+1)/matrix.GetValue(i, i);

        return solution;
    }
}