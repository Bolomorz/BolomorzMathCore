using BolomorzMathCore.Basics;
using BolomorzMathCore.LinearAlgebra.Base;
using BolomorzMathCore.LinearAlgebra.Matrix;

namespace BolomorzMathCore.LinearAlgebra.Algorithms;

public class QRResult<T, U> where T : MatrixBase<U> where U : class
{
    public required T Q { get; set; }
    public required T R { get; set; }
}
public class CQRDecomposition : AlgorithmBase<CMatrix, QRResult<CMatrix, Complex>>
{
    public CQRDecomposition(CMatrix matrix) : base(matrix, new() { Q = new(SpecialMatrix.Zero, 0), R = new(SpecialMatrix.Zero, 0) })
    {
        Result = QRDecomposition();
    }

    private QRResult<CMatrix, Complex> QRDecomposition()
    {
        int m = Input.Rows;
        int n = Input.Cols;

        CMatrix Q = new(m, n);
        CMatrix R = new(n, n);

        for (int i = 1; i <= n; i++)
        {
            var Ii = Input.GetColumn(i);

            for (int j = 1; j <= i - 1; j++)
            {
                var Qj = Q.GetColumn(j);
                R.SetValue(j, i, Ii * Qj);
                Ii -= R.GetValue(j, i) * Qj;
            }

            R.SetValue(i, i, Ii.Magnitude());
            Q.SetColumn(i, (1 / R.GetValue(i, i)) * Ii);
        }

        return new()
        {
            Q = Q,
            R = R
        };
    }
}

public class NQRDecomposition : AlgorithmBase<NMatrix, QRResult<NMatrix, Number>>
{
    public NQRDecomposition(NMatrix matrix) : base(matrix, new() { Q = new(SpecialMatrix.Zero, 0), R = new(SpecialMatrix.Zero, 0) })
    {
        Result = QRDecomposition();
    }

    private QRResult<NMatrix, Number> QRDecomposition()
    {
        int m = Input.Rows;
        int n = Input.Cols;

        NMatrix Q = new(m, n);
        NMatrix R = new(n, n);

        for (int i = 1; i <= n; i++)
        {
            var Ii = Input.GetColumn(i);

            for (int j = 1; j <= i - 1; j++)
            {
                var Qj = Q.GetColumn(j);
                R.SetValue(j, i, Ii * Qj);
                Ii -= R.GetValue(j, i) * Qj;
            }

            R.SetValue(i, i, Ii.Magnitude());
            Q.SetColumn(i, (1 / R.GetValue(i, i)) * Ii);
        }

        return new()
        {
            Q = Q,
            R = R
        };
    }
}