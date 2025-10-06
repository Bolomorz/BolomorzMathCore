using BolomorzMathCore.Basics;
using BolomorzMathCore.LinearAlgebra.Matrix;

namespace BolomorzMathCore.LinearAlgebra.Algorithms;

public class LIResult
{
    public required bool AreLinearlyIndependent { get; set; }
}
public class LinearIndependency : AlgorithmBase<Complex, LIResult>
{

    public LinearIndependency(List<CVector> vectors) :
    base(new(1, 1), new(){ AreLinearlyIndependent = false})
    {
        if (vectors.Count < 1) throw new Exception("cannot calculate linear independency of 0 vectors");
        var n = vectors[0].N;
        CMatrix vectormatrix = new(vectors.Count, n);
        for (int i = 0; i < vectors.Count; i++)
        {
            if (vectors[i].N == n)
                vectormatrix.SetColumn(i + 1, vectors[i]);
            else
                throw new Exception("cannot calculate linear independency of vectors with different dimensions");
        }

        var qr = new CQRDecomposition(vectormatrix).GetResult();

        Result = new() { AreLinearlyIndependent = NonZeroDiagonal(qr.R) };
    }

    public LinearIndependency(List<NVector> vectors) :
    base(new(1), new(){ AreLinearlyIndependent = false})
    {
        if (vectors.Count < 1) throw new Exception("cannot calculate linear independency of 0 vectors");
        var n = vectors[0].N;
        NMatrix vectormatrix = new(vectors.Count, n);
        for (int i = 0; i < vectors.Count; i++)
        {
            if (vectors[i].N == n)
                vectormatrix.SetColumn(i + 1, vectors[i]);
            else
                throw new Exception("cannot calculate linear independency of vectors with different dimensions");
        }

        var qr = new NQRDecomposition(vectormatrix).GetResult();

        Result = new() { AreLinearlyIndependent = NonZeroDiagonal(qr.R) };
    }

    private static bool NonZeroDiagonal(CMatrix matrix)
    {
        if (!matrix.IsQuadratic()) return false;
        for (int i = 1; i <= matrix.Rows; i++)
            if (matrix.GetValue(i, i) == Complex.Zero)
                return false;
        return true;
    }

    private static bool NonZeroDiagonal(NMatrix matrix)
    {
        if (!matrix.IsQuadratic()) return false;
        for (int i = 1; i <= matrix.Rows; i++)
            if (matrix.GetValue(i, i) == Number.Zero)
                return false;
        return true;
    }
}