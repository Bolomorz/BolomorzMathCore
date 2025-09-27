namespace BolomorzMathCore.LinearAlgebra.Base;

public abstract class DeterminantBase<T , U> where T : class where U : MatrixBase<T>
{
    protected struct Decomposition
    {
        public required bool Success { get; set; }
        public required T[,] Decompose { get; set; }
        public required int[] P { get; set; }
    }

    public DeterminantBase(U matrix)
    {
        if (!matrix.IsQuadratic()) throw new Exception("cannot calculate determinant of non quadratric matrix.");
        var decomposition = LUPDecompose(matrix.Values, matrix.Rows);
        Value = decomposition.Success ?
            LUPDeterminant(decomposition, matrix.Rows) :
            CalculateDeterminant(matrix.Values, matrix.Rows);
    }

    public T Value { get; protected set; }

    protected abstract T LUPDeterminant(Decomposition decomposition, int n);
    protected abstract Decomposition LUPDecompose(T[,] A, int n);
    protected abstract T[,] SubMatrix(T[,] m, int j, int k, int n);
    protected abstract T CalculateDeterminant(T[,] m, int n);
}