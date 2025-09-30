using System.Reflection;

namespace BolomorzMathCore.Basics;

public class Number
{
    public double Re { get; set; }

    public static readonly Number Tolerance = new(double.Epsilon);
    public static readonly Number Zero = new();
    public static readonly Number One = new(1);
    public static readonly Number MinusOne = new(-1);
    public static readonly Number NaN = new(double.NaN);

    public Number(double re)
    {
        Re = re;
    }
    public Number(Number number)
    {
        Re = number.Re;
    }
    public Number()
    {
        Re = 0;
    }

    public double Round(int precision)
        => Math.Round(Re, precision);

    public Number Mod(Number other)
    {
        Number a = this < Zero ? new(-this) : new(this);
        Number b = other < Zero ? new(-other) : new(other);

        Number mod = new(a);
        while (mod >= b)
            mod -= b;

        return a < Zero ? -mod : mod;
    }

    public Number Pow(Number expo)
        => new(Math.Pow(Re, expo.Re));

    public Number Exp()
        => new(Math.Exp(Re));

    public Number Square()
        => new(Re * Re);
    public Number Absolute()
        => new(Math.Sqrt(Square().Re));
    public Number Sign()
        => new(Re / Absolute().Re);
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