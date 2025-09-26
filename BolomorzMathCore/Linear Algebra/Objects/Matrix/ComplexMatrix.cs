using BolomorzMathCore.Basics;
using BolomorzMathCore.LinearAlgebra.Generics;

namespace BolomorzMathCore.LinearAlgebra.Matrix;

/// <summary>
/// <code>
/// Matrix of Complex Numbers
/// 
/// Matrix NxM = Complex[N, M]
/// Element(ij) = Complex(i,j)
/// Diagonals = [Element(ij) where i=j]
/// 
/// Special Types:
/// - special quadratic (NxN):
///     zero: all Elements are 0+0i
///     identity: Diagonals are 1+0i, rest are 0+0i
/// - matrix array (N):
///     diagonals: (NxN) Diagonals are the elements of the matrix array, rest are 0+0i
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
/// - GetValues: Complex[N,M]
/// - GetValue: Element(i,j)
/// - GetDeterminant: |Matrix|
/// 
/// Transformation on Matrix A:
/// - SubMatrix(i,j): Matrix B
///     deleting i.row and/or j.column from A
/// - Transpose: Matrix B
///     B.Element(i,j) = A.Element(j,i)
/// - Conjugate: Matrix B
///     B.Element(i,j) = A.Element(i,j).Conjugate()
/// - ConjugateTranspose: Matrix B
///     B = A.Conjugate().Transpose()
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
/// - IsHermitic: IsQuadratic AND A = ConjugateTranpose(A) ?
/// - IsSkewHermitic: IsQuadratic AND A = -ConjugateTranspose(A) ?
/// - IsUnitary: IsQuadratic AND A * ConjugateTranpose(A) = Identity ?
/// - IsRegular: IsQuadratic AND |A| is not 0 ?
/// - Rank: Number | A is regular: N | else RankRecursion: less than N (can be slow)
/// 
/// Operations of Matrix A, Matrix B, Number N, Complex C:
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
/// <see cref="Complex"/>
/// <see cref="CDeterminant"/> 
/// <see cref="SpecialQuadratic"/> 
/// <see cref="MatrixArray"/> 
public class CMatrix : MatrixBase<Complex>
{
    /// <summary>
    /// <code>
    /// Matrix (Values.N)x(Values.M)
    /// </code>
    /// </summary>
    public CMatrix(Complex[,] values) :
    base(values.GetLength(0), values.GetLength(1), values) { }

    /// <summary>
    /// <code>
    /// Special Types:
    /// - special quadratic (NxN):
    ///     zero: all Elements are 0+0i
    ///     identity: Diagonals are 1+0i, rest are 0+0i
    /// </code>
    /// </summary>
    public CMatrix(SpecialQuadratic sq, int n) :
    base(n, n, new Complex[n,n])
    {

        switch (sq)
        {
            case SpecialQuadratic.Zero:
                for (int row = 0; row < Rows; row++)
                    for (int col = 0; col < Cols; col++)
                        Values[row, col] = new Complex();
                break;
            case SpecialQuadratic.Identity:
                for (int row = 0; row < Rows; row++)
                    for (int col = 0; col < Cols; col++)
                        Values[row, col] = row != col ?
                            new Complex() :
                            new Complex(1);
                break;
        }

    }

    /// <summary>
    /// <code>
    /// Special Types:
    /// - matrix array (N):
    ///     diagonals: (NxN) Diagonals are the elements of the matrix array, rest are 0+0i
    ///     vector: (Nx1) Elements are the elements of the matrix array
    /// </code>
    /// </summary>
    public CMatrix(MatrixArray ma, Complex[] values) :
    base(
        values.Length,
        ma == MatrixArray.Diagonals ? values.Length :
        ma == MatrixArray.Vector ? 1 :
        0,
        new Complex[
            values.Length,
            ma == MatrixArray.Diagonals ? values.Length :
            ma == MatrixArray.Vector ? 1 :
            0
        ]
    )
    {

        switch (ma)
        {
            case MatrixArray.Diagonals:
                for (int row = 0; row < Rows; row++)
                    for (int col = 0; col < Cols; col++)
                        Values[row, col] = row == col ?
                            values[row] :
                            new Complex();
                break;

            case MatrixArray.Vector:
                for (int row = 0; row < Rows; row++)
                    Values[row, 0] = values[row];
                break;

        }

    }

    /// <summary>
    /// <code>
    /// Special Types:
    /// - rotation matrix (p, q, x1, x2, N)
    ///     (NxN): identity; 
    ///     Element(p,q) = (x2/x1) / (1.0 + (x2/x1)^2).SquareRoot()
    ///     Element(q,p) = (-1) * (x2/x1) / (1.0 + (x2/x1)^2).SquareRoot()
    ///     Element(p,p) = Element(q,q) = 1.0 / (1.0 + (x2/x1)^2).SquareRoot()
    /// </code>
    /// </summary>
    public CMatrix(int p, int q, Complex x1, Complex x2, int n) :
    base(n, n, new Complex[n,n])
    {

        Complex sin, cos;

        if (x1 == Complex.Zero)
        {
            sin = new(1);
            cos = new();
        }
        else
        {
            Complex tan = x2 / x1;
            sin = tan / (1.0 + tan * tan).SquareRoot();
            cos = 1.0 / (1.0 + tan * tan).SquareRoot();
        }

        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                Values[i, j] = i == j ?
                    new(1) :
                    new();

        Values[p, q] = sin;
        Values[q, p] = -sin;
        Values[p, p] = cos;
        Values[q, q] = cos;

    }

    #region Get
    public override Complex GetValue(int row, int col)
    {
        if (row > Rows || row < 1 || col > Cols || col < 1) throw new IndexOutOfRangeException();
        return Values[row - 1, col - 1];
    }
    public override Complex GetDeterminant()
    {
        CalculateDeterminant();
        return Determinant is not null ? Determinant : new(double.NaN);
    }
    #endregion

    #region Transformation
    /// <summary>
    /// <code>
    /// SubMatrix(i,j): Matrix B
    ///     deleting i.row and/or j.column from A
    /// </code>
    /// </summary>
    public override CMatrix SubMatrix(int i, int k)
    {

        if (i > 0 && k > 0)
        {

            Complex[,] sub = new Complex[Rows - 1, Cols - 1];

            int subrow = 0;
            int subcol = 0;
            i--; k--;

            for (int row = 0; row < Rows; row++)
            {
                if (row != i)
                {
                    subcol = 0;
                    for (int col = 0; col < Cols; col++)
                        if (col != k)
                            sub[subrow, subcol++] = Values[row, col];
                    subrow++;
                }
            }

            return new(sub);

        }
        else if (i > 0 && k < 0)
        {

            Complex[,] sub = new Complex[Rows - 1, Cols];

            int subrow = 0;
            i--;

            for (int row = 0; row < Rows; row++)
            {
                if (row != i)
                {
                    for (int col = 0; col < Cols; col++)
                        sub[subrow, col] = Values[row, col];
                    subrow++;
                }
            }

            return new(sub);

        }
        else if (i < 0 && k > 0)
        {

            Complex[,] sub = new Complex[Rows, Cols - 1];

            int subcol = 0;
            k--;

            for (int row = 0; row < Rows; row++)
            {
                subcol = 0;
                for (int col = 0; col < Cols; col++)
                    if (col != k)
                        sub[row, subcol++] = Values[row, col];
            }

            return new(sub);

        }
        else return new(Values);

    }
    /// <summary>
    /// <code>
    /// Transpose: Matrix B
    ///     B.Element(i,j) = A.Element(j,i)
    /// </code>
    /// </summary>
    public override CMatrix Transpose()
    {

        Complex[,] transpose = new Complex[Cols, Rows];

        for (int row = 0; row < Rows; row++)
            for (int col = 0; col < Cols; col++)
                transpose[col, row] = Values[row, col];

        return new(transpose);

    }
    /// <summary>
    /// <code>
    /// Inverse: Matrix B
    ///     A is regular | A is quadratic
    ///     B.Element(i,j) = +-|SubMatrix(i,j)| / |A|
    /// </code>
    /// </summary>
    public override CMatrix Inverse()
    {

        if (!IsRegular()) throw new Exception("cannot calculate inverse of non regular matrix.");
        if (!IsQuadratic()) throw new Exception("cannot calculate inverse of non quadratic matrix.");

        CalculateDeterminant();
        if (Determinant is null) throw new Exception("failed to calculate determinant");
        Complex determinant = Determinant;

        Complex[,] inverse = new Complex[Rows, Cols];

        for (int row = 1; row <= Rows; row++)
        {
            for (int col = 1; col <= Cols; col++)
            {
                Complex subdeterminant = SubMatrix(row, col).GetDeterminant();
                Complex adjunct = (row + col) % 2 == 0 ? subdeterminant : -subdeterminant;
                inverse[row - 1, col - 1] = adjunct / determinant;
            }
        }

        return new(inverse);

    }
    /// <summary>
    /// <code>
    /// Conjugate: Matrix B
    ///     B.Element(i,j) = A.Element(i,j).Conjugate()
    /// </code>
    /// </summary>
    public override CMatrix Conjugate()
    {

        Complex[,] conjugate = new Complex[Rows, Cols];

        for (int row = 0; row < Rows; row++)
            for (int col = 0; col < Cols; col++)
                conjugate[row, col] = Values[row, col].Conjugate();

        return new(conjugate);

    }
    /// <summary>
    /// <code>
    /// ConjugateTranspose: Matrix B
    ///     B = A.Conjugate().Transpose()
    /// </code>
    /// </summary>
    public override CMatrix ConjugateTranspose()
        => Conjugate().Transpose();
    #endregion

    #region Properties
    public override bool IsQuadratic()
        => Rows == Cols;
    /// <summary>
    /// <code>
    /// Trace: Sum(Diagonals(A))
    /// </code>
    /// </summary>
    public override Complex Trace()
    {

        if (!IsQuadratic()) throw new Exception("cannot calculate trace of non quadratic matrix.");

        Complex trace = new();

        for (int row = 0; row < Rows; row++)
            trace += Values[row, row];

        return trace;

    }
    /// <summary>
    /// <code>
    /// IsSymmetric: IsQuadratic AND A = Transpose(A) ?
    /// </code>
    /// </summary>
    public override bool IsSymmetric()
        => IsQuadratic() && this == Transpose();
    /// <summary>
    /// <code>
    /// IsSkewSymmetric: IsQuadratic AND A = -Tranpose(A) ?
    /// </code>
    /// </summary>
    public override bool IsSkewSymmetric()
        => IsQuadratic() && this == -this.Transpose();
    /// <summary>
    /// <code>
    /// IsOrthogonal: IsQuadratic AND A * Transpose(A) = Identity ?
    /// </code>
    /// </summary>
    public override bool IsOrthogonal()
        => IsQuadratic() && this * this.Transpose() == new CMatrix(SpecialQuadratic.Identity, this.Rows);
    /// <summary>
    /// <code>
    /// IsHermitic: IsQuadratic AND A = ConjugateTranpose(A) ?
    /// </code>
    /// </summary>
    public override bool IsHermitic()
        => IsQuadratic() && this == this.ConjugateTranspose();
    /// <summary>
    /// <code>
    /// IsSkewHermitic: IsQuadratic AND A = -ConjugateTranspose(A) ?
    /// </code>
    /// </summary>
    public override bool IsSkewHermitic()
        => IsQuadratic() && this == -this.ConjugateTranspose();
    /// <summary>
    /// <code>
    /// IsUnitary: IsQuadratic AND A * ConjugateTranpose(A) = Identity ?
    /// </code>
    /// </summary>
    public override bool IsUnitary()
        => IsQuadratic() && this * this.ConjugateTranspose() == new CMatrix(SpecialQuadratic.Identity, this.Rows);
    /// <summary>
    /// <code>
    /// IsRegular: IsQuadratic AND |A| is not 0 ?
    /// </code>
    /// </summary>
    public override bool IsRegular()
    {

        if (IsQuadratic())
        {
            CalculateDeterminant();
            return Determinant is not null && Determinant != Complex.Zero;
        }
        else return false;

    }
    protected override void CalculateDeterminant()
    {

        Determinant ??= new CDeterminant(this).Value;

    }
    /// <summary>
    /// <code>
    /// Rank: Number | A is regular: N | else RankRecursion: less than N (can be slow)
    /// </code>
    /// </summary>
    public override int Rank()
        => IsRegular() ?
            Rows :
            RankRecursion(this);
    private static int RankRecursion(CMatrix m)
    {

        int r = m.Rows;
        int c = m.Cols;

        if (r == c)
        {

            if (m.IsRegular())
                return r;

            else if (r == 2)

                return m.GetValue(1, 1) != Complex.Zero || m.GetValue(1, 2) != Complex.Zero || m.GetValue(2, 1) != Complex.Zero || m.GetValue(2, 2) != Complex.Zero ?
                    1 :
                    0;

            else
            {

                List<CMatrix> mlist = new();

                for (int i = 1; i <= r; i++)
                    for (int k = 1; k <= c; k++)
                        mlist.Add(m.SubMatrix(i, k));

                List<int> ilist = new();
                foreach (var matrix in mlist)
                    ilist.Add(RankRecursion(matrix));

                return MaxInList([.. ilist]);

            }

        }
        else if (r > c)
        {

            List<CMatrix> mlist = new();

            for (int i = 1; i <= r; i++)
                mlist.Add(m.SubMatrix(i, -1));

            List<int> ilist = new();
            foreach (var matrix in mlist)
                ilist.Add(RankRecursion(matrix));

            return MaxInList([.. ilist]);

        }
        else
        {

            List<CMatrix> mlist = new();

            for (int i = 1; i < c; i++)
                mlist.Add(m.SubMatrix(-1, i));

            List<int> ilist = new();
            foreach (var matrix in mlist)
                ilist.Add(RankRecursion(matrix));

            return MaxInList([.. ilist]);

        }
    }
    #endregion

    #region Operations
    public static CMatrix operator +(CMatrix A) => A;
    public static CMatrix operator -(CMatrix A)
    {

        Complex[,] negativ = new Complex[A.Rows, A.Cols];

        for (int row = 0; row < A.Rows; row++)
            for (int col = 0; col < A.Cols; col++)
                negativ[row, col] = -A.Values[row, col];

        return new(negativ);

    }
    public static CMatrix operator +(CMatrix A, CMatrix B)
    {

        if (A.Rows != B.Rows || A.Cols != B.Cols) throw new Exception("cannot add two matrices of different type.");

        Complex[,] sum = new Complex[A.Rows, A.Cols];

        for (int row = 0; row < A.Rows; row++)
            for (int col = 0; col < A.Cols; col++)
                sum[row, col] = A.Values[row, col] + B.Values[row, col];
        return new(sum);

    }
    public static CMatrix operator -(CMatrix A, CMatrix B)
    {

        if (A.Rows != B.Rows || A.Cols != B.Cols) throw new Exception("cannot subtract two matrices of different type.");

        Complex[,] diff = new Complex[A.Rows, A.Cols];

        for (int row = 0; row < A.Rows; row++)
            for (int col = 0; col < A.Cols; col++)
                diff[row, col] = A.Values[row, col] - B.Values[row, col];

        return new(diff);

    }
    public static CMatrix operator *(double A, CMatrix B)
    {

        Complex[,] mult = new Complex[B.Rows, B.Cols];

        for (int row = 0; row < B.Rows; row++)
            for (int col = 0; col < B.Cols; col++)
                mult[row, col] = A * B.Values[row, col];

        return new(mult);

    }
    public static CMatrix operator *(Complex A, CMatrix B)
    {

        Complex[,] mult = new Complex[B.Rows, B.Cols];

        for (int row = 0; row < B.Rows; row++)
            for (int col = 0; col < B.Cols; col++)
                mult[row, col] = A * B.Values[row, col];

        return new(mult);

    }
    public static CMatrix operator *(CMatrix A, CMatrix B)
    {

        if (A.Cols != B.Rows) throw new Exception("cannot multiply two matrices of that type.");

        int n = A.Cols;
        Complex[,] product = new Complex[A.Rows, B.Cols];

        for (int row = 0; row < A.Rows; row++)
        {
            for (int col = 0; col < B.Cols; col++)
            {
                Complex rowsum = new();
                for (int i = 0; i < n; i++)
                    rowsum += A.Values[row, i] * B.Values[i, col];
                product[row, col] = rowsum;
            }
        }

        return new(product);

    }
    public static bool operator ==(CMatrix A, CMatrix B)
    {

        if (A.Rows != B.Rows || A.Cols != B.Cols) return false;

        for (int row = 0; row < A.Rows; row++)
            for (int col = 0; col < A.Cols; col++)
                if (A.Values[row, col] != B.Values[row, col])
                    return false;

        return true;

    }
    public static bool operator !=(CMatrix A, CMatrix B) => !(A == B);
    #endregion

    #region ObjectOverrides
    public override string ToString()
        => $"CMatrix [{Rows}|{Cols}]";
    public override int GetHashCode()
    {
        int sum = 0;
        foreach (var t in Values)
            sum += t.GetHashCode();
        return sum;
    }
    public override bool Equals(object? obj)
        => obj is not null && obj is CMatrix && this == (CMatrix)obj;
    #endregion
}