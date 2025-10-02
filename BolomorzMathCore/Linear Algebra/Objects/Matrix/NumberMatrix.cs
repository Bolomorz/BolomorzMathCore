using BolomorzMathCore.Basics;
using BolomorzMathCore.LinearAlgebra.Base;

namespace BolomorzMathCore.LinearAlgebra.Matrix;

/// <summary>
/// <code>
/// Matrix of Real Numbers
/// 
/// Matrix NxM = Real[N, M]
/// Element(ij) = Real(i,j)
/// Diagonals = [Element(ij) where i=j]
/// 
/// Special Types:
/// - special quadratic (NxN):
///     zero: all Elements are 0
///     identity: Diagonals are 1, 0
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
/// - GetValues: Real[N,M]
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
/// - IsOrthogonal: IsQuadratic AND A * Transpose(A) = Identity ?
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
/// <see cref="NDeterminant"/> 
/// <see cref="SpecialMatrix"/> 
/// <see cref="VectorMatrix"/> 
public class NMatrix : MatrixBase<Number>
{
    /// <summary>
    /// <code>
    /// Matrix (Values.N)x(Values.M)
    /// </code>
    /// </summary>
    public NMatrix(Number[,] values) :
    base(values.GetLength(0), values.GetLength(1), values){ }
    /// <summary>
    /// <code>
    /// Special Types:
    /// - special quadratic (NxN):
    ///     zero: all Elements are 0+0i
    ///     identity: Diagonals are 1+0i, rest are 0+0i
    /// </code>
    /// </summary>
    public NMatrix(SpecialMatrix sq, int n) :
    base(n, n, new Number[n, n])
    {

        switch (sq)
        {
            case SpecialMatrix.Zero:
                for (int row = 0; row < Rows; row++)
                    for (int col = 0; col < Cols; col++)
                        Values[row, col] = new(0);
                break;
            case SpecialMatrix.Identity:
                for (int row = 0; row < Rows; row++)
                    for (int col = 0; col < Cols; col++)
                        Values[row, col] = row != col ?
                            new(0) :
                            new(1);
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
    public NMatrix(VectorMatrix ma, NVector vector) :
    base(
        vector.N,
        ma == VectorMatrix.Diagonals ? vector.N :
        ma == VectorMatrix.Vector ? 1 :
        0,
        new Number[
            vector.N,
            ma == VectorMatrix.Diagonals ? vector.N :
            ma == VectorMatrix.Vector ? 1 :
            0
        ]
    )
    {

        switch (ma)
        {
            case VectorMatrix.Diagonals:
                for (int row = 0; row < Rows; row++)
                    for (int col = 0; col < Cols; col++)
                        Values[row, col] = row == col ?
                            vector.Values[row] :
                            new(0);
                break;

            case VectorMatrix.Vector:
                for (int row = 0; row < Rows; row++)
                    Values[row, 0] = vector.Values[row];
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
    public NMatrix(int p, int q, Number x1, Number x2, int n) :
    base(n, n, new Number[n,n])
    {

        Number sin, cos;

        if (x1 == Number.Zero)
        {
            sin = new(1);
            cos = new(0);
        }
        else
        {
            Number tan = x2 / x1;
            sin = tan / (1.0 + tan * tan).SquareRoot().Re;      //here: Re is positive => SquareRoot(Sqrt(Re) | 0)
            cos = new(1.0 / (1.0 + tan * tan).SquareRoot().Re); //here: Re is positive => SquareRoot(Sqrt(Re) | 0)
        }

        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                Values[i, j] = i == j ?
                    new(1) :
                    new(0);

        Values[p, q] = sin;
        Values[q, p] = -sin;
        Values[p, p] = cos;
        Values[q, q] = cos;

    }

    #region Get
    public override Number GetValue(int row, int col)
    {
        if (row > Rows || row < 1 || col > Cols || col < 1) throw new IndexOutOfRangeException();
        return Values[row - 1, col - 1];
    }
    public override Number GetDeterminant()
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
    public override NMatrix SubMatrix(int i, int k)
    {

        if (i > 0 && k > 0)
        {

            Number[,] sub = new Number[Rows - 1, Cols - 1];

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

            Number[,] sub = new Number[Rows - 1, Cols];

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

            Number[,] sub = new Number[Rows, Cols - 1];

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
    public override NMatrix Transpose()
    {

        Number[,] transpose = new Number[Cols, Rows];

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
    public override NMatrix Inverse()
    {

        if (!IsRegular()) throw new Exception($"cannot calculate inverse of non regular matrix. [|M|={Determinant}]");
        if (!IsQuadratic()) throw new Exception($"cannot calculate inverse of non quadratic matrix. [M({Rows}x{Cols})]");

        CalculateDeterminant();
        if (Determinant is null) throw new Exception("failed to calculate determinant");
        Number determinant = Determinant;

        Number[,] inverse = new Number[Rows, Cols];

        for (int row = 1; row <= Rows; row++)
        {
            for (int col = 1; col <= Cols; col++)
            {
                Number subdeterminant = SubMatrix(row, col).GetDeterminant();
                Number adjunct = (row + col) % 2 == 0 ? subdeterminant : -subdeterminant;
                inverse[row - 1, col - 1] = adjunct / determinant;
            }
        }

        return new(inverse);

    }
    public override NMatrix Conjugate()
        => this;
    public override NMatrix ConjugateTranspose()
        => Transpose(); 
    #endregion

    #region Properties
    public override bool IsQuadratic()
        => Rows == Cols;
    /// <summary>
    /// <code>
    /// Trace: Sum(Diagonals(A))
    /// </code>
    /// </summary>
    public override Number Trace()
    {

        if (!IsQuadratic() ) throw new Exception($"cannot calculate trace of non quadratic matrix. [M({Rows}x{Cols})]");

        Number trace = new();

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
        => IsQuadratic() && this * this.Transpose() == new NMatrix(SpecialMatrix.Identity, this.Rows);
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
            return Determinant is not null && Determinant != Number.Zero;
        }
        else return false;

    }
    public override bool IsHermitic()
        => false;
    public override bool IsSkewHermitic()
        => false;
    public override bool IsUnitary()
        => false;
    protected override void CalculateDeterminant()
    {

        Determinant ??= new NDeterminant(this).Value;

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
    private static int RankRecursion(NMatrix m)
    {

        int r = m.Rows;
        int c = m.Cols;

        if (r == c)
        {

            if (m.IsRegular())
                return r;

            else if (r == 2)

                return m.GetValue(1, 1) != Number.Zero || m.GetValue(1, 2) != Number.Zero || m.GetValue(2, 1) != Number.Zero || m.GetValue(2, 2) != Number.Zero ?
                    1 :
                    0;

            else
            {

                List<NMatrix> mlist = new();

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

            List<NMatrix> mlist = new();

            for (int i = 1; i <= r; i++)
                mlist.Add(m.SubMatrix(i, -1));

            List<int> ilist = new();
            foreach (var matrix in mlist)
                ilist.Add(RankRecursion(matrix));

            return MaxInList([.. ilist]);

        }
        else
        {

            List<NMatrix> mlist = new();

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
    public static NMatrix operator +(NMatrix A) => A;
    public static NMatrix operator -(NMatrix A)
    {

        Number[,] negativ = new Number[A.Rows, A.Cols];

        for (int row = 0; row < A.Rows; row++)
            for (int col = 0; col < A.Cols; col++)
                negativ[row, col] = -A.Values[row, col];

        return new(negativ);

    }
    public static NMatrix operator +(NMatrix A, NMatrix B)
    {

        if (A.Rows != B.Rows || A.Cols != B.Cols) throw new Exception($"cannot add two matrices of these dimensions. [A({A.Rows}x{A.Cols}) | B({B.Rows}x{B.Cols})]");

        Number[,] sum = new Number[A.Rows, A.Cols];

        for (int row = 0; row < A.Rows; row++)
            for (int col = 0; col < A.Cols; col++)
                sum[row, col] = A.Values[row, col] + B.Values[row, col];
        return new(sum);

    }
    public static NMatrix operator -(NMatrix A, NMatrix B)
    {

        if (A.Rows != B.Rows || A.Cols != B.Cols) throw new Exception($"cannot subtract two matrices of these dimensions. [A({A.Rows}x{A.Cols}) | B({B.Rows}x{B.Cols})]");

        Number[,] diff = new Number[A.Rows, A.Cols];

        for (int row = 0; row < A.Rows; row++)
            for (int col = 0; col < A.Cols; col++)
                diff[row, col] = A.Values[row, col] - B.Values[row, col];

        return new(diff);

    }
    public static NMatrix operator *(Number A, NMatrix B)
    {

        Number[,] mult = new Number[B.Rows, B.Cols];

        for (int row = 0; row < B.Rows; row++)
            for (int col = 0; col < B.Cols; col++)
                mult[row, col] = A * B.Values[row, col];

        return new(mult);

    }
    public static NMatrix operator *(NMatrix A, NMatrix B)
    {

        if (A.Cols != B.Rows) throw new Exception($"cannot multiply two matrices of these dimensions. [A({A.Rows}x{A.Cols}) | B({B.Rows}x{B.Cols})]");

        int n = A.Cols;
        Number[,] product = new Number[A.Rows, B.Cols];

        for (int row = 0; row < A.Rows; row++)
        {
            for (int col = 0; col < B.Cols; col++)
            {
                Number rowsum = new(0);
                for (int i = 0; i < n; i++)
                    rowsum += A.Values[row, i] * B.Values[i, col];
                product[row, col] = rowsum;
            }
        }

        return new(product);

    }
    public static bool operator ==(NMatrix A, NMatrix B)
    {

        if (A.Rows != B.Rows || A.Cols != B.Cols) return false;

        for (int row = 0; row < A.Rows; row++)
            for (int col = 0; col < A.Cols; col++)
                if (A.Values[row, col] != B.Values[row, col])
                    return false;

        return true;

    }
    public static bool operator !=(NMatrix A, NMatrix B) => !(A == B);
    #endregion

    #region ObjectOverrides
    public override string ToString()
        => $"DMatrix [{Rows}|{Cols}]";
    public override int GetHashCode()
    {
        int sum = 0;
        foreach (var t in Values)
            sum += t.GetHashCode();
        return sum;
    }
    public override bool Equals(object? obj)
        => obj is not null && obj is NMatrix && this == (NMatrix)obj;
    #endregion
}