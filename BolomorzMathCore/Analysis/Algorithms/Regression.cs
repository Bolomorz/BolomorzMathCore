using BolomorzMathCore.Basics;

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
/// <see cref="Function"/> 
public class Regression(List<SeriesPoint> points) : AlgorithmBase<List<SeriesPoint>, Function>(points, Function.NaF())
{
    public Regression PolynomialRegression(int order)
    {
        var reg = RegressionAlgorithms.PolynomialRegression(order, Input);
        Result = reg is not null ? Function.Polynomial(reg) : Function.NaF();
        return this;
    }
    public Regression LinearRegression()
    {
        var reg = RegressionAlgorithms.LinearRegression(Input);
        Result = reg is not null ? Function.Line(reg[0], reg[1]) : Function.NaF();
        return this;
    }
    public Regression PowerRegression()
    {
        var reg = RegressionAlgorithms.PowerRegression(Input);
        Result = reg is not null ? Function.Power(reg[0], reg[1]) : Function.NaF();
        return this;
    }
    public Regression LogarithmicRegression()
    {
        var reg = RegressionAlgorithms.LogarithmicRegression(Input);
        Result = reg is not null ? Function.Logarithm(reg[0], reg[1]) : Function.NaF();
        return this;
    }
    public Regression ExponentialRegression()
    {
        var reg = RegressionAlgorithms.ExponentialRegression(Input);
        Result = reg is not null ? Function.Exponential(reg[0], reg[1]) : Function.NaF();
        return this;
    }
}

internal static class RegressionAlgorithms
{

    internal static double[]? LinearRegression(List<SeriesPoint> points)
    {
        int n = points.Count;

        double sumX = 0;
        double sumXX = 0;
        double sumY = 0;
        double sumXY = 0;

        for (int i = 0; i < n; i++)
        {
            sumX += points[i].X;
            sumXX += points[i].X * points[i].X;
            sumY += points[i].Y;
            sumXY += points[i].X * points[i].Y;
        }

        double b = (n * sumXY - sumX * sumY) / (n * sumXX - sumX * sumX);
        double a = (sumY - b * sumX) / n;

        return [a, b];
    }

    internal static double[]? PowerRegression(List<SeriesPoint> points)
    {
        int n = points.Count;

        double sumX = 0;
        double sumXX = 0;
        double sumY = 0;
        double sumXY = 0;

        for (int i = 0; i < n; i++)
        {
            sumX += Math.Log(points[i].X);
            sumXX += Math.Log(points[i].X) * Math.Log(points[i].X);
            sumY += Math.Log(points[i].Y);
            sumXY += Math.Log(points[i].X) * Math.Log(points[i].Y);
        }

        double b = (n * sumXY - sumX * sumY) / (n * sumXX - sumX * sumX);
        double a = (sumY - b * sumX) / n;

        return [Math.Exp(a), b];
    }

    internal static double[]? ExponentialRegression(List<SeriesPoint> points)
    {
        int n = points.Count;

        double sumX = 0;
        double sumXX = 0;
        double sumY = 0;
        double sumXY = 0;

        for (int i = 0; i < n; i++)
        {
            sumX += points[i].X;
            sumXX += points[i].X * points[i].X;
            sumY += Math.Log(points[i].Y);
            sumXY += points[i].X * Math.Log(points[i].Y);
        }

        double b = (n * sumXY - sumX * sumY) / (n * sumXX - sumX * sumX);
        double a = (sumY - b * sumX) / n;

        return [Math.Exp(a), Math.Exp(b)];
    }

    internal static double[]? LogarithmicRegression(List<SeriesPoint> points)
    {
        int n = points.Count;

        double sumX = 0;
        double sumXX = 0;
        double sumY = 0;
        double sumXY = 0;

        for (int i = 0; i < n; i++)
        {
            sumX += Math.Log(points[i].X);
            sumXX += Math.Log(points[i].X) * Math.Log(points[i].X);
            sumY += points[i].Y;
            sumXY += Math.Log(points[i].X) * points[i].Y;
        }

        double b = (n * sumXY - sumX * sumY) / (n * sumXX - sumX * sumX);
        double a = (sumY - b * sumX) / n;

        return [a, b];
    }

    internal static double[]? PolynomialRegression(int order, List<SeriesPoint> points)
    {
        return new AugmentedCoefficientMatrix(order, points).GetSolution();
    }
    
    private class AugmentedCoefficientMatrix
    {
        private double[,] Matrix;
        private int n;
        private int m;
        public AugmentedCoefficientMatrix(int order, List<SeriesPoint> points)
        {
            m = points.Count;
            n = order + 1;
            Matrix = new double[n, n+1];
            for(int i = 1; i <=n; i++)
            {
                double sum;
                for(int j = 1; j <= i; j++)
                {
                    int k = i + j - 2;
                    sum = 0;
                    for(int l = 1; l <= m; l++) sum += Math.Pow(points[l-1].X, k);
                    Matrix[i-1, j-1] = sum;
                    Matrix[j-1, i-1] = sum;
                }
                sum = 0;
                for(int l = 1; l <= m; l++) sum += points[l-1].Y * Math.Pow(points[l-1].X, i-1);
                Matrix[i-1, n] = sum;
            }
        }

        public double[]? GetSolution()
        {
            try
            {
                if(m < n+1) return null;
                ApplyGaussJordanElimination();
                double[] solution = new double[n];
                for(int i = 0; i < n; i++) solution[i] = Matrix[i, n]/Matrix[i, i];
                return solution;
            }
            catch
            {
                return null;
            }
        }

        private void ApplyGaussJordanElimination()
        {
            for(int i = 0; i < n; i++)
            {
                if(Matrix[i, i] == 0) throw new DivideByZeroException();
                for(int j = 0; j < n; j++)
                {
                    if(i != j)
                    {
                        double Ratio = Matrix[j, i] / Matrix[i, i];
                        for(int k = 0; k <= n; k++) Matrix[j, k] -= Ratio * Matrix[i, k];
                    }
                }
            }
        }
    }
}