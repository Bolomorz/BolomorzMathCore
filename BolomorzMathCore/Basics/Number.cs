using System.Reflection;

namespace BolomorzMathCore.Basics;

/// <summary>
/// <code>
/// Real Number
/// 
/// Number = Re
/// 
/// Operations on Number A with Number B:
/// - Round:
///     A.Round: double | number rounded to precision
/// - Mod:
///     A.Mod(B): Number | A mod B
/// - Pow:
///     A.Pow(B): Number | A to the power of B
/// - Exp:
///     A.Exp: Number | e to the power of A
/// - Square:
///     A.Square: Number | A * A
/// - Absolute:
///     A.Absolute: Number | Sqrt(A.Square)
/// - Sign:
///     A.Sign: Number | A / A.Absolute
/// - SquareRoot:
///     A.SquareRoot: Complex | A < 0 ? Sqrt(A) + 0i | 0 + Sqrt(A)i
/// 
/// Operators Number A, Number B, double N:
/// - Addition:
///     A + B | A + N : Number
/// - Subtraction:
///     A - B | A - N : Number
/// - Multiplikation:
///     A * B | A * N : Number
/// - Division:
///     A / B | A / N | N / A : Number
/// - Comparison:       
///     A greaterthan|lessthan|equalto B : bool
/// </code>
/// </summary>
public class Number
{
    public double Re { get; set; }

    /// <summary>
    /// <code>
    /// Number = epsilon
    /// </code>
    /// </summary>
    public static readonly Number Tolerance = new(double.Epsilon);
    /// <summary>
    /// <code>
    /// Number = 0
    /// </code>
    /// </summary>
    public static readonly Number Zero = new();
    /// <summary>
    /// <code>
    /// Number = 1
    /// </code>
    /// </summary>
    public static readonly Number One = new(1);
    /// <summary>
    /// <code>
    /// Number = -1
    /// </code>
    /// </summary>
    public static readonly Number MinusOne = new(-1);
    /// <summary>
    /// <code>
    /// Number = NaN
    /// </code>
    /// </summary>
    public static readonly Number NaN = new(double.NaN);
    public static readonly Number Min = new(double.MinValue);
    public static readonly Number Max = new(double.MaxValue);

    /// <summary>
    /// <code>
    /// Real Number
    /// 
    /// Number = Re
    /// </code>
    /// </summary>
    public Number(double re)
    {
        Re = re;
    }
    /// <summary>
    /// <code>
    /// Real Number
    /// 
    /// Number = number.Re
    /// </code>
    /// </summary>
    public Number(Number number)
    {
        Re = number.Re;
    }
    /// <summary>
    /// <code>
    /// Real Number
    /// 
    /// Number = 0
    /// </code>
    /// </summary>
    public Number()
    {
        Re = 0;
    }

    /// <summary>
    /// <code>
    /// A.Round: double | number rounded to precision
    /// </code>
    /// </summary>
    public double Round(int precision)
        => Math.Round(Re, precision);

    /// <summary>
    /// <code>
    /// A.Mod(B): Number | A mod B
    /// </code>
    /// </summary>
    public Number Mod(Number other)
    {
        Number a = this < Zero ? new(-this) : new(this);
        Number b = other < Zero ? new(-other) : new(other);

        Number mod = new(a);
        while (mod >= b)
            mod -= b;

        return a < Zero ? -mod : mod;
    }

    /// <summary>
    /// <code>
    /// A.Pow(B): Number | A to the power of B
    /// </code>
    /// </summary>
    public Number Pow(Number expo)
        => new(Math.Pow(Re, expo.Re));

    /// <summary>
    /// <code>
    /// A.Exp: Number | e to the power of A
    /// </code>
    /// </summary>
    public Number Exp()
        => new(Math.Exp(Re));

    /// <summary>
    /// <code>
    /// A.Square: Number | A * A
    /// </code>
    /// </summary>
    public Number Square()
        => new(Re * Re);

    /// <summary>
    /// <code>
    /// A.Absolute: Number | Sqrt(A.Square)
    /// </code>
    /// </summary>
    public Number Absolute()
        => new(Math.Sqrt(Square().Re));

    /// <summary>
    /// <code>
    /// A.Sign: Number | A / A.Absolute
    /// </code>
    /// </summary>
    public Number Sign()
        => new(Re / Absolute().Re);

    /// <summary>
    /// <code>
    /// A.SquareRoot: Complex | A < 0 ? Sqrt(A) + 0i | 0 + Sqrt(A)i
    /// </code>
    /// </summary>
    public Complex SquareRoot()
        => Re < 0 ?
                new(0, Math.Sqrt(-Re)) :
                new(Math.Sqrt(Re));

    #region Operator
    public static Number operator +(Number A)
        => A;
    public static Number operator -(Number A)
        => new(-A.Re);

    public static Number operator +(Number A, Number B)
        => new(A.Re + B.Re);
    public static Number operator +(double A, Number B)
        => new(A + B.Re);
    public static Number operator +(Number A, double B)
        => new(A.Re + B);

    public static Number operator -(Number A, Number B)
        => new(A.Re - B.Re);
    public static Number operator -(double A, Number B)
        => new(A - B.Re);
    public static Number operator -(Number A, double B)
        => new(A.Re - B);

    public static Number operator *(Number A, Number B)
        => new(A.Re * B.Re);
    public static Number operator *(double A, Number B)
        => new(A * B.Re);
    public static Number operator *(Number A, double B)
        => new(A.Re * B);

    public static Number operator /(Number A, Number B)
    {
        if (B.Re == 0) throw new DivideByZeroException();
        return new(A.Re / B.Re);
    }
    public static Number operator /(double A, Number B)
    {
        if (B.Re == 0) throw new DivideByZeroException();
        return new(A * B.Re);
    }
    public static Number operator /(Number A, double B)
    {
        if (B == 0) throw new DivideByZeroException();
        return new(A.Re / B);
    }

    public static bool operator ==(Number A, Number B)
        => A.Re == B.Re;
    public static bool operator !=(Number A, Number B)
        => !(A == B);

    public static bool operator >=(Number A, Number B)
        => A.Re >= B.Re;
    public static bool operator <=(Number A, Number B)
        => A.Re <= B.Re;

    public static bool operator >(Number A, Number B)
        => A.Re > B.Re;
    public static bool operator <(Number A, Number B)
        => A.Re < B.Re;

    public static implicit operator Complex(Number A)
    {
        Complex B = new(A.Re);
        return B;
    }
    #endregion

    #region ObjectOverrides
    public override string ToString()
        => $"{Math.Round(Re, 5)}";
    public override int GetHashCode()
        => Re.GetHashCode();
    public override bool Equals(object? obj)
        => obj != null && obj is Number && this == (Number)obj;
    #endregion
}