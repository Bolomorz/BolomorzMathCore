namespace BolomorzMathCore.LinearAlgebra.Base;

public abstract class MatrixBase<T>(int rows, int cols, T[,] values) where T : class
{
    public int Rows = rows;
    public int Cols = cols;
    public T[,] Values = values;
    public T? Determinant = null;

    #region Get
    public abstract T GetValue(int row, int col);
    public abstract T GetDeterminant();
    public abstract VectorBase<T> GetColumn(int col);
    public abstract VectorBase<T> GetRow(int row);
    #endregion

    #region Set
    public abstract void SetValue(int row, int col, T value);
    public abstract void SetColumn(int col, VectorBase<T> vector);
    public abstract void SetRow(int row, VectorBase<T> vector);
    #endregion

    protected abstract void CalculateDeterminant();

    #region Transformations
    public abstract MatrixBase<T> SubMatrix(int row, int col);
    public abstract MatrixBase<T> Transpose();
    public abstract MatrixBase<T> Inverse();
    public abstract MatrixBase<T> Conjugate();
    public abstract MatrixBase<T> ConjugateTranspose();
    public abstract MatrixBase<T> ApplyGaussJordanElimination();
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

