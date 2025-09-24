namespace BolomorzMathCore.Matrices;

/// <summary>
/// <code>
/// Matrix of T
/// 
/// Matrix NxM = Complex[N, M]
/// Element(ij) = Complex(i,j)
/// Diagonals = [Element(ij) where i=j]
/// 
/// Special Types:
/// - special quadratic (NxN):
///     zero: all Elements are 0
///     identity: Diagonals are 1, rest are 0
/// - matrix array (N):
///     diagonals: (NxN) Diagonals are the elements of the matrix array, rest are 0
///     vector: (Nx1) Elements are the elements of the matrix array
/// - rotation matrix (p, q, x1, x2, N)
///     (NxN): identity; 
///     Element(p,q) = (x2/x1) / (1.0 + (x2/x1)^2).SquareRoot()
///     Element(q,p) = (-1) * (x2/x1) / (1.0 + (x2/x1)^2).SquareRoot()
///     Element(p,p) = Element(q,q) = 1.0 / (1.0 + (x2/x1)^2).SquareRoot()
/// 
/// Getter:
/// - GetRows: N
/// - GetCols: M
/// - GetValues: T[N,M]
/// - GetValue: Element(i,j)
/// - GetDeterminant: |Matrix|
/// 
/// Transformation on Matrix A:
/// - SubMatrix(i,j): Matrix B
///     deleting i.row and/or j.column from A
/// - Transpose: Matrix B
///     B.Element(i,j) = A.Element(j,i)
/// - Inverse: Matrix B
///     A is regular | A is quadratic
///     B.Element(i,j) = +-|SubMatrix(i,j)| / |A|
/// 
/// Properties of Matrix A:
/// - IsQuadratic: N = M ?
/// - Trace: Sum(Diagonals(A))
/// - IsSymmetric: IsQuadratic AND A = Transpose(A) ?
/// - IsSkewSymmetric: IsQuadratic AND A = -Tranpose(A) ?
/// - IsComplexMatrix: at least one Element(i,j) with Element(i,j).Im is not 0 ?
/// - IsOrthogonal: IsQuadratic AND A * Transpose(A) = Identity ?
/// - IsRegular: IsQuadratic AND |A| is not 0 ?
/// - Rank: Number | A is regular: N | else RankRecursion: less than N (can be slow)
/// 
/// Operations of Matrix A, Matrix B, Number N, T C:
/// - Addition:
///     A + B | WHEN A.Rows = B.Rows AND A.Cols = B.Cols
/// - Subtraction:
///     A - B | WHEN A.Rows = B.Rows AND A.Cols = B.Cols
/// - Multiplikation:
///     N * A | C * A
///     A * B | WHEN A.Cols = B.Rows
/// - Comparing:
///     A is B | A is not B
/// </code>
/// </summary>
/// <see cref="Determinant{T}"/> 
/// <see cref="SpecialQuadratic"/> 
/// <see cref="MatrixArray"/> 
public abstract class Matrix<T> where T : class
{
    protected int Rows;
    protected int Cols;
    protected T[,] Values;
    protected Determinant<T>? Determinant;

    #region Getters
    /// <summary>
    /// <code>
    /// Matrix NxM: N
    /// </code>
    /// </summary>
    public int GetRows() => Rows;
    /// <summary>
    /// <code>
    /// Matrix NxM: M
    /// </code>
    /// </summary>
    public int GetCols() => Cols;
    /// <summary>
    /// <code>
    /// T[N,M]
    /// </code>
    /// </summary>
    public T[,] GetValues() => Values;
    /// <summary>
    /// <code>
    /// Element(row,col)
    /// </code>
    /// </summary>
    public T GetValue(int row, int col)
    {
        if (row > Rows || col > Cols || row < 1 || col < 1) throw new IndexOutOfRangeException();
        return Values[row - 1, col - 1];
    }
    /// <summary>
    /// <code>
    /// |Matrix|
    /// </code>
    /// </summary>
    public T GetDeterminant()
        => Determinant is not null ?
            Determinant.Value :
            CalculateDeterminant();
    #endregion

    #region Transformations
    /// <summary>
    /// <code>
    /// SubMatrix(i,j): Matrix B
    ///     deleting i.row and/or j.column from A
    /// </code>
    /// </summary>
    public abstract Matrix<T> SubMatrix(int row, int col);
    /// <summary>
    /// <code>
    /// Transpose: Matrix B
    ///     B.Element(i,j) = A.Element(j,i)
    /// </code>
    /// </summary>
    public abstract Matrix<T> Transpose();
    /// <summary>
    /// <code>
    /// Inverse: Matrix B
    ///     A is regular | A is quadratic
    ///     B.Element(i,j) = +-|SubMatrix(i,j)| / |A|
    /// </code>
    /// </summary>
    public abstract Matrix<T> Inverse();
    #endregion

    #region Properties
    /// <summary>
    /// <code>
    /// IsQuadratic: N = M ?
    /// </code>
    /// </summary>
    public bool IsQuadratic() => Rows == Cols;
    /// <summary>
    /// <code>
    /// Trace: Sum(Diagonals(A))
    /// </code>
    /// </summary>
    public abstract T Trace();
    /// <summary>
    /// <code>
    /// IsSymmetric: IsQuadratic AND A = Transpose(A) ?
    /// </code>
    /// </summary>
    public abstract bool IsSymmetric();
    /// <summary>
    /// <code>
    /// IsSkewSymmetric: IsQuadratic AND A = -Tranpose(A) ?
    /// </code>
    /// </summary>
    public abstract bool IsSkewSymmetric();
    /// <summary>
    /// <code>
    /// IsOrthogonal: IsQuadratic AND A * Transpose(A) = Identity ?
    /// </code>
    /// </summary>
    public abstract bool IsOrthogonal();
    /// <summary>
    /// <code>
    /// IsRegular: IsQuadratic AND |A| is not 0 ?
    /// </code>
    /// </summary>
    public abstract bool IsRegular();
    /// <summary>
    /// <code>
    /// Rank: Number | A is regular: N | else RankRecursion: less than N (can be slow)
    /// </code>
    /// </summary>
    public abstract int Rank();
    protected abstract T CalculateDeterminant();
    #endregion

    #region ObjectOverrides
    public abstract override string ToString();
    public abstract override int GetHashCode();
    public abstract override bool Equals(object? obj);
    #endregion

    #region  Helper
    protected static int MaxInList(int[] ints)
    {
        int max = int.MinValue;

        foreach (var i in ints)
            if (i > max)
                max = i;

        return max;
    }
    #endregion
}

/// <summary>
/// <code>
/// Determinant[T] of Matrix[T] NxN (Quadratic Matrix)
/// 
/// Determinant[T] = det(A) or |A| = [T]
/// 
/// Matrix A 2x2 = ((a b) (c d)) => det(A) = ad - bc
/// 
/// Properties of Matrix A depending on |A|:
/// - invertibility:    
///     |A| is not zero => Inverse(A) exists | A is invertible
/// - area/volume:      
///     |A| represents parallelogram.area formed by its row- or col-vectors for A=2x2         
///     |A| is area of figure formed by its row or col-vectors in a N-dimensional system for A=NxN
/// - linear dependence: 
///     rows or columns of A are linearly dependent => |A| is zero
/// - operations:
///     multiplying a row or column by a scalar k => |Result| = k * |Original|
///     adding a multiple of row or col to other row or col => |Result| = |Original|
/// - transpose:
///     |Transpose(A)| = |A|
/// 
/// Calculation of |A|:
/// - Decomposition of Matrix with Complex.Tolerance for LUP-Algorithm
/// - Decomposition successful      => LUPDeterminant of LUP-Algorithm | faster
/// - Decomposition not successful  => SubDeterminant-Algorithm | very slow
/// </code>
/// </summary>
/// <see cref="NDeterminant"/>
/// <see cref="CDeterminant"/>
public abstract class Determinant<T> where T : class
{
    protected struct Decomposition
    {
        public required bool Success { get; set; }
        public required T[,] Decompose { get; set; }
        public required int[] P { get; set; }
    }
    public abstract T Value { get; protected set; }

    protected abstract T LUPDeterminant(Decomposition decomposition, int n);
    protected abstract Decomposition LUPDecompose(T[,] A, int n, T Tol);
    protected abstract T[,] SubMatrix(T[,] m, int j, int k, int n);
    protected abstract T CalculateDeterminant(T[,] m, int n);
}