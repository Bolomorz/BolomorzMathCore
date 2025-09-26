namespace BolomorzMathCore.LinearAlgebra.Generics;

public abstract class MatrixBase<T>(int rows, int cols, T[,] values) where T : class
{
    public int Rows = rows;
    public int Cols = cols;
    public T[,] Values = values;
    public T? Determinant = null;

    #region Get
    public abstract T GetValue(int row, int col);
    public abstract T GetDeterminant();
    #endregion

    protected abstract void CalculateDeterminant();

    #region Transformations
    public abstract MatrixBase<T> SubMatrix(int row, int col);
    public abstract MatrixBase<T> Transpose();
    public abstract MatrixBase<T> Inverse();
    public abstract MatrixBase<T> Conjugate();
    public abstract MatrixBase<T> ConjugateTranspose();
    #endregion

    public abstract bool IsQuadratic();
    public abstract T Trace();
    public abstract bool IsSymmetric();
    public abstract bool IsSkewSymmetric();
    public abstract bool IsOrthogonal();
    public abstract bool IsRegular();
    public abstract bool IsHermitic();
    public abstract bool IsSkewHermitic();
    public abstract bool IsUnitary();
    public abstract int Rank();

    protected static int MaxInList(int[] ints)
    {
        int max = int.MinValue;

        foreach (var i in ints)
            if (i > max)
                max = i;

        return max;
    }


    public static MatrixBase<T> operator +(MatrixBase<T> A) { throw new NotImplementedException(); }
    public static MatrixBase<T> operator -(MatrixBase<T> A) { throw new NotImplementedException(); }
    public static MatrixBase<T> operator +(MatrixBase<T> A, MatrixBase<T> B) { throw new NotImplementedException(); }
    public static MatrixBase<T> operator -(MatrixBase<T> A, MatrixBase<T> B) { throw new NotImplementedException(); }
    public static MatrixBase<T> operator *(double A, MatrixBase<T> B) { throw new NotImplementedException(); }
    public static MatrixBase<T> operator *(T A, MatrixBase<T> B) { throw new NotImplementedException(); }
    public static MatrixBase<T> operator *(MatrixBase<T> A, MatrixBase<T> B) { throw new NotImplementedException(); }
    public static bool operator ==(MatrixBase<T> A, MatrixBase<T> B) { throw new NotImplementedException(); }
    public static bool operator !=(MatrixBase<T> A, MatrixBase<T> B) { throw new NotImplementedException(); }

    public abstract override bool Equals(object? obj);
    public abstract override string ToString();
    public abstract override int GetHashCode();
}

