namespace BolomorzMathCore.LinearAlgebra.Base;


public abstract class VectorBase<T>(int n, T[] values, VectorType type) where T : class
{
    public int N = n;
    public T[] Values = values;
    public VectorType Type = type;

    public abstract T? GetValue(int index);

    public abstract T Magnitude();
    public abstract VectorBase<T> Normalize();
    public abstract VectorBase<T> Direction();
    public abstract bool AreOrthogonal(VectorBase<T> other);
    public abstract bool AreCollinear(VectorBase<T> other);
    public abstract bool IsZero();
    public abstract bool IsUnit();

    public abstract VectorBase<T> CrossProduct(VectorBase<T> other);
    public abstract VectorBase<T> Projection(VectorBase<T> U);

    public static VectorBase<T> operator +(VectorBase<T> A) { throw new NotImplementedException(); }
    public static VectorBase<T> operator -(VectorBase<T> A) { throw new NotImplementedException(); }
    public static VectorBase<T> operator +(VectorBase<T> A, VectorBase<T> B) { throw new NotImplementedException(); }
    public static VectorBase<T> operator -(VectorBase<T> A, VectorBase<T> B) { throw new NotImplementedException(); }
    public static VectorBase<T> operator *(T scalar, VectorBase<T> vector) { throw new NotImplementedException(); }
    public static T operator *(VectorBase<T> A, VectorBase<T> B) { throw new NotImplementedException(); }
    public static bool operator ==(VectorBase<T> A, VectorBase<T> B) { throw new NotImplementedException(); }
    public static bool operator !=(VectorBase<T> A, VectorBase<T> B) { throw new NotImplementedException(); }
    public static VectorBase<T> operator *(VectorBase<T> A, MatrixBase<T> B) { throw new NotImplementedException(); }
    public static VectorBase<T> operator *(MatrixBase<T> A, VectorBase<T> B) { throw new NotImplementedException(); }


    public abstract override bool Equals(object? obj);
    public abstract override string ToString();
    public abstract override int GetHashCode();
}