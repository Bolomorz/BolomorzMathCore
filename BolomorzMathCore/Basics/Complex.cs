namespace BolomorzMathCore.Basics;

/// <summary>
/// <code>
/// Complex Number
/// 
/// Complex = Re + Im * i
/// Re = Number | Im = Number
/// 
/// Operations on Complex A:
/// - Conjugate:        
///     Conjugate(A) = Complex | A.Re - A.Im * i
/// - Square:           
///     Square(A) = Number | A.Re^2 + A.Im^2
/// - Absolute:         
///     Absolute(A) = Number | Sqrt(Square(A))
/// - Sign:             
///     Sign(A) = Complex | A.Re/Absolute(A) + A.Im/Absolute(A) * i
/// - SquareRoot:       
///     SquareRoot(A) = Complex | Sqrt((A.Re + Absolute(A))/2) + Im/Abs(Im) * Sqrt((-A.Re + Absolute(A))/2) * i
/// 
/// Operators Complex A, Complex B, Number N:
/// - Addition:         
///     A + B | A + N | N + A = Complex
/// - Subtraction:      
///     A - B | A - N | N - A = Complex
/// - Multiplikation:   
///     A * B | A * N | N * A = Complex
/// - Division:         
///     A / B | A / N | N / A = Complex
/// - Comparison:       
///     A greaterthan|lessthan|equalto B = bool
/// </code>
/// </summary>
public class Complex
{
    public double Re { get; set; }
    public double Im { get; set; }

    /// <summary>
    /// epsilon + 0 * i
    /// </summary>
    public static readonly Complex Tolerance = new(double.Epsilon);
    /// <summary>
    /// 0 + 0 * i
    /// </summary>
    public static readonly Complex Zero = new();

    /// <summary>
    /// <code>
    /// Complex Number
    /// 
    /// Complex = Re + Im * i
    /// Re = Number | Im = Number
    /// </code>
    /// </summary>
    public Complex(double re, double im)
    {

        Re = re;
        Im = im;

    }
    public Complex(Complex complex)
    {
        Re = complex.Re;
        Im = complex.Im;
    }
    public Complex(Number re)
    {
        Re = re.Re;
        Im = 0;
    }
    /// <summary>
    /// <code>
    /// Complex Number
    /// 
    /// Complex = Re + 0 * i
    /// Re = Number
    /// </code>
    /// </summary>
    public Complex(double re)
    {

        Re = re;
        Im = 0;

    }
    /// <summary>
    /// <code>
    /// Complex Number
    /// 
    /// Complex = 0 + 0 * i
    /// </code>
    /// </summary>
    public Complex()
    {

        Re = 0;
        Im = 0;

    }

    #region Operations
    /// <summary>
    /// <code>
    /// Conjugate:  Conjugate(A) = Complex | A.Re - A.Im * i
    /// </code>
    /// </summary>
    public Complex Conjugate()
        => new(Re, -Im);

    /// <summary>
    /// <code>
    /// Absolute:   Absolute(A) = Number | Sqrt(Square(A))
    /// </code>
    /// </summary>
    public Number Absolute()
        => new(Math.Sqrt(Square().Re));

     /// <summary>
    /// <code>
    /// Square:     Square(A) = Number | A.Re^2 + A.Im^2
    /// </code>
    /// </summary>
    public Number Square()
        => new(Re * Re + Im * Im);

     /// <summary>
    /// <code>
    /// Sign:       Sign(A) = Complex | A.Re/Absolute(A) + A.Im/Absolute(A) * i
    /// </code>
    /// </summary>
    public Complex Sign()
        => new(Re / Absolute().Re, Im / Absolute().Re);
        
     /// <summary>
    /// <code>
    /// SquareRoot: SquareRoot(A) = Complex | Sqrt((A.Re + Absolute(A))/2) + Im/Abs(Im) * Sqrt((-A.Re + Absolute(A))/2) * i
    /// </code>
    /// </summary>
    public Complex SquareRoot()
    {

        if (Im == 0)
        {
            return Re < 0 ?
                new(0, Math.Sqrt(-Re)) :
                new(Math.Sqrt(Re));
        }
        else
        {
            double re = Math.Sqrt((Re + Absolute().Re) / 2);
            double im = (Im / Math.Abs(Im)) * Math.Sqrt((-Re + Absolute().Re) / 2);
            return new(re, im);
        }

    }
    #endregion

    #region Operator
    public static Complex operator +(Complex A)
        => A;
    public static Complex operator -(Complex A)
        => new(-A.Re, -A.Im);

    public static Complex operator +(Complex A, Complex B)
        => new(A.Re + B.Re, A.Im + B.Im);
    public static Complex operator +(double A, Complex B)
        => new(A + B.Re, B.Im);
    public static Complex operator +(Complex A, double B)
        => new(A.Re + B, A.Im);
    public static Complex operator +(Number A, Complex B)
        => new(A.Re + B.Re, B.Im);
    public static Complex operator +(Complex A, Number B)
        => new(A.Re + B.Re, A.Im);

    public static Complex operator -(Complex A, Complex B)
        => new(A.Re - B.Re, A.Im - B.Im);
    public static Complex operator -(double A, Complex B)
        => new(A - B.Re, B.Im);
    public static Complex operator -(Complex A, double B)
        => new(A.Re - B, A.Im);
    public static Complex operator -(Number A, Complex B)
        => new(A.Re - B.Re, B.Im);
    public static Complex operator -(Complex A, Number B)
        => new(A.Re - B.Re, A.Im);

    public static Complex operator *(Complex A, Complex B)
        => new(A.Re * B.Re - A.Im * B.Im, A.Re * B.Im + B.Re * A.Im);
    public static Complex operator *(double A, Complex B)
        => new(A * B.Re, A * B.Im);
    public static Complex operator *(Complex A, double B)
        => new(A.Re * B, B * A.Im);
    public static Complex operator *(Number A, Complex B)
        => new(A.Re * B.Re, A.Re * B.Im);
    public static Complex operator *(Complex A, Number B)
        => new(A.Re * B.Re, B.Re * A.Im);

    public static Complex operator /(Complex A, Complex B)
    {
        if (B.Re == 0 && B.Im == 0) throw new DivideByZeroException();
        return new((A.Re * B.Re + A.Im * B.Im) / B.Square().Re, (B.Re * A.Im - A.Re * B.Im) / B.Square().Re);
    }
    public static Complex operator /(double A, Complex B)
    {
        if (B.Re == 0 && B.Im == 0) throw new DivideByZeroException();
        return new(A * B.Re / B.Square().Re, A * B.Im / B.Square().Re);
    }
    public static Complex operator /(Complex A, double B)
    {
        if (B == 0) throw new DivideByZeroException();
        return new(A.Re / B, A.Im / B);
    }
    public static Complex operator /(Number A, Complex B)
    {
        if (B.Re == 0 && B.Im == 0) throw new DivideByZeroException();
        return new(A.Re * B.Re / B.Square().Re, A.Re * B.Im / B.Square().Re);
    }
    public static Complex operator /(Complex A, Number B)
    {
        if (B.Re == 0) throw new DivideByZeroException();
        return new(A.Re / B.Re, A.Im / B.Re);
    }

    public static bool operator ==(Complex A, Complex B)
        => A.Re == B.Re && A.Im == B.Im;
    public static bool operator !=(Complex A, Complex B)
        => !(A == B);

    public static bool operator >=(Complex A, Complex B)
        => A.Re >= B.Re;
    public static bool operator <=(Complex A, Complex B)
        => A.Re <= B.Re;

    public static bool operator >(Complex A, Complex B)
        => A.Re > B.Re;
    public static bool operator <(Complex A, Complex B)
        => A.Re < B.Re;
    #endregion

    #region ObjectOverrides
    public override string ToString()
        => Im == 0 ?
            $"{Math.Round(Re, 5)}" :
            $"{Math.Round(Re, 5)} + i * {Math.Round(Im, 5)}";
    public override int GetHashCode()
        => Re.GetHashCode() + Im.GetHashCode();
    public override bool Equals(object? obj)
        => obj != null && obj is Complex && this == (Complex)obj;
    #endregion
}