using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Analysis;

/// <summary>
/// <code>
/// Point{T} T: Number/Complex
/// 
/// point of relation R
/// 
/// Properties:
/// - X: Number | x-value
/// - Y: Number | y-value
/// </code>
/// </summary>
/// <see cref="Series"/> 
public abstract class Point<T>(T x, T y) where T : class
{
    public T X { get; set; } = x;
    public T Y { get; set; } = y;
}


public class NPoint(Number x, Number y) :
Point<Number>(x, y);


public class CPoint(Complex x, Complex y) :
Point<Complex>(x, y);