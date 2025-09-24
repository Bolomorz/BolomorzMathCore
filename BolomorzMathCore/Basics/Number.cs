namespace BolomorzMathCore.Basics;

public class Number
{
    public double Re { get; set; }

    public static readonly Number Tolerance = new(double.Epsilon);
    public static readonly Number Zero = new();

    public Number(double re)
    {
        Re = re;
    }

    public Number()
    {
        Re = 0;
    }

    public Number Square()
        => new(Re * Re);
    public Number Absolute()
        => new(Math.Sqrt(Square().Re));
    public Number Sign()
        => new(Re / Absolute().Re);
    public Number SquareRoot()
        => new(Math.Sqrt(Re));

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