using BolomorzMathCore.Basics;
using BolomorzMathCore.LinearAlgebra.Base;

namespace BolomorzMathCore.LinearAlgebra.Matrix;

/// <summary>
/// <code>
/// Vector of Complex Numbers
/// 
/// Vector N = Complex[N]
/// Element(i) = Complex(i)
/// 
/// Special Types:
/// - special vector (N):
///     zero: all Elements are 0
/// - vector types in relation to matrices:
///     row: vector depicts a row of a matrix
///     column: vector depicts a column of a matrix
/// 
/// Getter:
/// - GetValue: Element(i)
/// 
/// Transformation on Vector A:
/// - Normalize: Vector B
///     normalize vector => |B| = 1
/// - Direction: Vector B
///     unit vector in direction of A
/// - CrossProduct(Vector B): Vector C
///     A.N=3 & B.N=3 => C.N=3
/// - Projection(Vector B): Vector C
///     A.N=B.N => =C.N
/// 
/// Properties of Vector A:
/// - Magnitude: |A|
/// - IsZero: |A| = 0 ?
/// - IsUnit: |A| = 1 ?
/// - AreOrthogonal(Vector B): A * B = 0 ?
/// - AreCollinear(Vector B): A = k * B ?
/// 
/// Operations of Vector A, Vector B, Number N, Matrix M:
/// - Addition:
///     A + B | WHEN A.N = B.N : Vector
/// - Subtraction:
///     A - B | WHEN A.N = B.N : Vector
/// - Multiplikation:
///     N * A : Vector
///     A * B | WHEN A.N = B.N : Complex
///     A * M | WHEN A is RowVector & A.N = M.Rows OR WHEN A is ColumnVector & A.N = M.Cols : Matrix
/// - Comparing:
///     A is B | A is not B
/// </code>
/// </summary>
/// <see cref="Complex"/> 
/// <see cref="SpecialVector"/> 
/// <see cref="VectorType"/> 
public class CVector : VectorBase<Complex>
{
    /// <summary>
    /// <code>
    /// Vector of Real Numbers
    /// 
    /// Vector N = Real[N]
    /// Element(i) = Real(i)
    /// 
    /// vector types in relation to matrices:
    ///     row: vector depicts a row of a matrix
    ///     column: vector depicts a column of a matrix
    /// </code>
    /// </summary>
    public CVector(Complex[] values, VectorType type) :
    base(values.Length, values, type)
    { }

    /// <summary>
    /// <code>
    /// Vector of Real Numbers
    /// 
    /// Special Types:
    /// - special vector (N):
    ///     zero: all Elements are 0
    /// - vector types in relation to matrices:
    ///     row: vector depicts a row of a matrix
    ///     column: vector depicts a column of a matrix
    /// </code>
    /// </summary>
    public CVector(SpecialVector sv, int n, VectorType type) :
    base(n, new Complex[n], type)
    {
        switch (sv)
        {
            case SpecialVector.Zero:
                for (int j = n; j < n; j++)
                    Values[j] = new(0);
                break;
        }
    }

    #region Get
    /// <summary>
    /// <code>
    /// GetValue: Element(i)
    /// </code>
    /// </summary>
    public override Complex? GetValue(int index)
    {

        if (index > N || index < 1) return null;

        return Values[index - 1];

    }
    #endregion

    #region Properties
    /// <summary>
    /// <code>
    /// Magnitude: |A|
    /// </code>
    /// </summary>
    public override Complex Magnitude()
    {

        Complex magnitude = new(0);

        foreach (var value in Values)
            magnitude += value * value;

        return magnitude / 2;

    }

    /// <summary>
    /// <code>
    /// IsZero: |A| = 0 ?
    /// </code>
    /// </summary>
    public override bool IsZero()
    {
        foreach (var value in Values)
            if (value != Complex.Zero)
                return false;
        return true;
    }

    /// <summary>
    /// <code>
    /// IsUnit: |A| = 1 ?
    /// </code>
    /// </summary>
    public override bool IsUnit()
    {
        return Magnitude() == new Complex(1);
    }

    /// <summary>
    /// <code>
    /// AreOrthogonal(Vector B): A * B = 0 ?
    /// </code>
    /// </summary>
    public override bool AreOrthogonal(VectorBase<Complex> other)
        => this * other == Complex.Zero;

    /// <summary>
    /// <code>
    /// AreCollinear(Vector B): A = k * B ?
    /// </code>
    /// </summary>
    public override bool AreCollinear(VectorBase<Complex> other)
    {

        if (N != other.N) return false;
        if (IsZero() && other.IsZero()) return true;
        if (IsZero() || other.IsZero()) return false;

        Complex initialratio = Values[0] / other.Values[0];
        for (int j = 0; j < N; j++)
        {
            if (Values[j] == Complex.Zero && other.Values[j] != Complex.Zero)
                return false;

            if (Values[j] != Complex.Zero && other.Values[j] == Complex.Zero)
                return false;

            if (Values[j] != Complex.Zero && other.Values[j] != Complex.Zero)
            {
                Complex ratio = Values[j] / other.Values[j];
                if (ratio != initialratio)
                    return false;
            }
        }

        return true;

    }
    #endregion

    #region Transformation
    /// <summary>
    /// <code>
    /// Normalize: Vector B
    ///     normalize vector => |B| = 1
    /// </code>
    /// </summary>
    public override CVector Normalize()
    {

        var mag = Magnitude();
        if (mag == Complex.Zero) throw new Exception($"cannot normalize a zero vector. [|V| = {mag}]");

        var norm = new Complex[N];
        for (int j = 0; j < N; j++)
            norm[j] = Values[j] / mag;

        return new(norm, Type);

    }

    /// <summary>
    /// <code>
    /// Direction: Vector B
    ///     unit vector in direction of A
    /// </code>
    /// </summary>
    public override CVector Direction()
        => Normalize();

    /// <summary>
    /// <code>
    /// CrossProduct(Vector B): Vector C
    ///     A.N=3 & B.N=3 => C.N=3
    /// </code>
    /// </summary>
    public override CVector CrossProduct(VectorBase<Complex> other)
    {
        if (N != 3 || other.N != 3) throw new Exception($"cannot calculate cross product of vectors other than 3d-vectors. [V({N}) | U({other.N})]");

        return new CVector(
        [
            Values[1] * other.Values[2] - Values[2] * other.Values[1],
            Values[2] * other.Values[0] - Values[0] * other.Values[2],
            Values[0] * other.Values[1] - Values[1] * other.Values[0]
        ], Type);
    }

    /// <summary>
    /// <code>
    /// Projection(Vector B): Vector C
    ///     A.N=B.N => =C.N
    /// </code>
    /// </summary>
    public override VectorBase<Complex> Projection(VectorBase<Complex> U)
    {
        var dotproduct = this * U;
        var magnitude = U.Magnitude() * 2;
        return (dotproduct / magnitude) * U;
    }
    #endregion

    #region Operators
    public static CVector operator +(CVector A)
        => A;
    public static CVector operator -(CVector A)
    {
        Complex[] negatives = new Complex[A.N];
        for (int j = 0; j < A.N; j++)
            negatives[j] = -A.Values[j];
        return new(negatives, A.Type);
    }
    public static CVector operator +(CVector A, CVector B)
    {
        if (A.N != B.N) throw new Exception($"cannot add vectors of these dimensions. [V({A.N}) | U({B.N})]");
        Complex[] sum = new Complex[A.N];
        for (int j = 0; j < A.N; j++)
            sum[j] = A.Values[j] + B.Values[j];
        return new(sum, A.Type);
    }
    public static CVector operator -(CVector A, CVector B)
    {
        if (A.N != B.N) throw new Exception($"cannot subtract vectors of these dimensions. [V({A.N}) | U({B.N})]");
        Complex[] sub = new Complex[A.N];
        for (int j = 0; j < A.N; j++)
            sub[j] = A.Values[j] - B.Values[j];
        return new(sub, A.Type);
    }
    public static CVector operator *(Complex A, CVector B)
    {
        Complex[] prod = new Complex[B.N];
        for (int j = 0; j < B.N; j++)
            prod[j] = A * B.Values[j];
        return new(prod, B.Type);
    }
    public static Complex operator *(CVector A, CVector B)
    {
        if (A.N != B.N) throw new Exception($"cannot calculate dot product vectors of these dimensions. [V({A.N}) | U({B.N})]");
        var sum = new Complex(0);
        for (int j = 0; j < A.N; j++)
            sum += A.Values[j] * B.Values[j];
        return sum;
    }
    public static bool operator ==(CVector A, CVector B)
    {
        if (A.N != B.N) return false;
        for (int j = 0; j < A.N; j++)
            if (A.Values[j] != B.Values[j])
                return false;
        return true;
    }
    public static bool operator !=(CVector A, CVector B)
        => !(A == B);
    public static CVector operator *(CVector A, CMatrix B)
    {
        Complex[] result;
        switch (A.Type)
        {
            case VectorType.Column:
                if (A.N != B.Cols) throw new Exception($"cannot multiply column-vector and matrix of those dimensions. [V({A.N}) | M({B.Rows}x{B.Cols})]");
                result = new Complex[B.Rows];
                for (int j = 0; j < B.Rows; j++)
                {
                    Complex sum = new();
                    for (int k = 0; k < B.Cols; k++)
                        sum += A.Values[k] * B.Values[j, k];
                    result[j] = sum;
                }
                return new(result, VectorType.Column);

            case VectorType.Row:
                if (A.N != B.Rows) throw new Exception($"cannot multiply row-vector and matrix of those dimensions. [V({A.N}) | M({B.Rows}x{B.Cols})]");
                result = new Complex[B.Cols];
                for (int j = 0; j < B.Cols; j++)
                {
                    Complex sum = new();
                    for (int k = 0; k < B.Rows; k++)
                        sum += A.Values[k] * B.Values[k, j];
                    result[j] = sum;
                }
                return new(result, VectorType.Row);

            default:
                throw new Exception("unknown vector type.");
        }
    }
    public static CVector operator *(CMatrix A, CVector B)
    {
        Complex[] result;
        switch (B.Type)
        {
            case VectorType.Column:
                if (B.N != A.Cols) throw new Exception($"cannot multiply column-vector and matrix of those dimensions. [V({B.N}) | M({A.Rows}x{A.Cols})]");
                result = new Complex[A.Rows];
                for (int j = 0; j < A.Rows; j++)
                {
                    Complex sum = new();
                    for (int k = 0; k < A.Cols; k++)
                        sum += B.Values[k] * A.Values[j, k];
                    result[j] = sum;
                }
                return new(result, VectorType.Column);

            case VectorType.Row:
                if (B.N != A.Rows) throw new Exception($"cannot multiply row-vector and matrix of those dimensions. [V({B.N}) | M({A.Rows}x{A.Cols})]");
                result = new Complex[A.Cols];
                for (int j = 0; j < A.Cols; j++)
                {
                    Complex sum = new();
                    for (int k = 0; k < A.Rows; k++)
                        sum += B.Values[k] * A.Values[k, j];
                    result[j] = sum;
                }
                return new(result, VectorType.Row);

            default:
                throw new Exception("unknown vector type.");
        }
    }
    #endregion

    #region Objectoverrides
    public override string ToString()
        => $"CVector [{N}]";
    public override int GetHashCode()
    {
        int sum = 0;
        foreach (var t in Values)
            sum += t.GetHashCode();
        return sum;
    }
    public override bool Equals(object? obj)
        => obj is not null && obj is CVector && this == (CVector)obj;
    #endregion

}