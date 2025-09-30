using BolomorzMathCore.Basics;
using BolomorzMathCore.LinearAlgebra.Base;

namespace BolomorzMathCore.LinearAlgebra.Matrix;

public class NVector : VectorBase<Number>
{
    public NVector(Number[] values, VectorType type) :
    base(values.Length, values, type)
    { }

    public NVector(SpecialVector sv, int n, VectorType type) :
    base(n, new Number[n], type)
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
    public override Number? GetValue(int index)
    {
        if (index > N || index < 1) return null;

        return Values[index - 1];
    }
    #endregion

    #region Properties
    public override Number Magnitude()
    {
        Number magnitude = new(0);

        foreach (var value in Values)
            magnitude += value * value;

        return magnitude / 2;
    }
    public override NVector Normalize()
    {
        var mag = Magnitude();
        if (mag == Number.Zero) throw new Exception($"cannot normalize a zero vector. [|V| = {mag}]");

        var norm = new Number[N];
        for (int j = 0; j < N; j++)
            norm[j] = Values[j] / mag;

        return new(norm, Type);
    }
    public override NVector Direction()
    {
        return Normalize();
    }
    public override bool IsZero()
    {
        foreach (var value in Values)
            if (value != Number.Zero)
                return false;
        return true;
    }
    public override bool IsUnit()
    {
        return Magnitude() == new Number(1);
    }
    public override bool AreOrthogonal(VectorBase<Number> other)
        => this * other == Number.Zero;
    public override bool AreCollinear(VectorBase<Number> other)
    {
        if (N != other.N) return false;
        if (IsZero() && other.IsZero()) return true;
        if (IsZero() || other.IsZero()) return false;

        Number initialratio = Values[0] / other.Values[0];
        for (int j = 0; j < N; j++)
        {
            if (Values[j] == Number.Zero && other.Values[j] != Number.Zero)
                return false;

            if (Values[j] != Number.Zero && other.Values[j] == Number.Zero)
                return false;

            if (Values[j] != Number.Zero && other.Values[j] != Number.Zero)
            {
                Number ratio = Values[j] / other.Values[j];
                if (ratio != initialratio)
                    return false;
            }
        }

        return true;
    }
    #endregion

    #region Transformation
    public override NVector CrossProduct(VectorBase<Number> other)
    {
        if (N != 3 || other.N != 3) throw new Exception($"cannot calculate cross product of vectors other than 3d-vectors. [V({N}) | U({other.N})]");

        return new NVector(
        [
            Values[1] * other.Values[2] - Values[2] * other.Values[1],
            Values[2] * other.Values[0] - Values[0] * other.Values[2],
            Values[0] * other.Values[1] - Values[1] * other.Values[0]
        ], Type);
    }
    public override VectorBase<Number> Projection(VectorBase<Number> U)
    {
        var dotproduct = this * U;
        var magnitude = U.Magnitude() * 2;
        return (dotproduct / magnitude) * U;
    }
    #endregion

    #region Operators
    public static NVector operator +(NVector A)
        => A;
    public static NVector operator -(NVector A)
    {
        Number[] negatives = new Number[A.N];
        for (int j = 0; j < A.N; j++)
            negatives[j] = -A.Values[j];
        return new(negatives, A.Type);
    }
    public static NVector operator +(NVector A, NVector B)
    {
        if (A.N != B.N) throw new Exception($"cannot add vectors of these dimensions. [V({A.N}) | U({B.N})]");
        Number[] sum = new Number[A.N];
        for (int j = 0; j < A.N; j++)
            sum[j] = A.Values[j] + B.Values[j];
        return new(sum, A.Type);
    }
    public static NVector operator -(NVector A, NVector B)
    {
        if (A.N != B.N) throw new Exception($"cannot subtract vectors of these dimensions. [V({A.N}) | U({B.N})]");
        Number[] sub = new Number[A.N];
        for (int j = 0; j < A.N; j++)
            sub[j] = A.Values[j] - B.Values[j];
        return new(sub, A.Type);
    }
    public static NVector operator *(Number A, NVector B)
    {
        Number[] prod = new Number[B.N];
        for (int j = 0; j < B.N; j++)
            prod[j] = A * B.Values[j];
        return new(prod, B.Type);
    }
    public static Number operator *(NVector A, NVector B)
    {
        if (A.N != B.N) throw new Exception($"cannot calculate dot product vectors of these dimensions.  [V({A.N}) | U({B.N})]");
        var sum = new Number(0);
        for (int j = 0; j < A.N; j++)
            sum += A.Values[j] * B.Values[j];
        return sum;
    }
    public static bool operator ==(NVector A, NVector B)
    {
        if (A.N != B.N) return false;
        for (int j = 0; j < A.N; j++)
            if (A.Values[j] != B.Values[j])
                return false;
        return true;
    }
    public static bool operator !=(NVector A, NVector B)
        =>!(A == B);
    public static NVector operator *(NVector A, NMatrix B)
    {
        Number[] result;
        switch (A.Type)
        {
            case VectorType.Column:
                if (A.N != B.Cols) throw new Exception($"cannot multiply column-vector and matrix of those dimensions. [V({A.N}) | M({B.Rows}x{B.Cols})]");
                result = new Number[B.Rows];
                for (int j = 0; j < B.Rows; j++)
                {
                    Number sum = new();
                    for (int k = 0; k < B.Cols; k++)
                        sum += A.Values[k] * B.Values[j, k];
                    result[j] = sum;
                }
                return new(result, VectorType.Column);

            case VectorType.Row:
                if (A.N != B.Rows) throw new Exception($"cannot multiply row-vector and matrix of those dimensions. [V({A.N}) | M({B.Rows}x{B.Cols})]");
                result = new Number[B.Cols];
                for (int j = 0; j < B.Cols; j++)
                {
                    Number sum = new();
                    for (int k = 0; k < B.Rows; k++)
                        sum += A.Values[k] * B.Values[k, j];
                    result[j] = sum;
                }
                return new(result, VectorType.Row);

            default:
                throw new Exception("unknown vector type.");
        }
    }
    public static NVector operator *(NMatrix A, NVector B)
    {
        Number[] result;
        switch (B.Type)
        {
            case VectorType.Column:
                if (B.N != A.Cols) throw new Exception($"cannot multiply column-vector and matrix of those dimensions. [V({B.N}) | M({A.Rows}x{A.Cols})]");
                result = new Number[A.Rows];
                for (int j = 0; j < A.Rows; j++)
                {
                    Number sum = new();
                    for (int k = 0; k < A.Cols; k++)
                        sum += B.Values[k] * A.Values[j, k];
                    result[j] = sum;
                }
                return new(result, VectorType.Column);

            case VectorType.Row:
                if (B.N != A.Rows) throw new Exception($"cannot multiply row-vector and matrix of those dimensions. [V({B.N}) | M({A.Rows}x{A.Cols})]");
                result = new Number[A.Cols];
                for (int j = 0; j < A.Cols; j++)
                {
                    Number sum = new();
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
        => $"NVector [{N}]";
    public override int GetHashCode()
    {
        int sum = 0;
        foreach (var t in Values)
            sum += t.GetHashCode();
        return sum;
    }
    public override bool Equals(object? obj)
        => obj is not null && obj is NVector && this == (NVector)obj;
    #endregion
}