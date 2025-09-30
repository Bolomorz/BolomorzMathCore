using BolomorzMathCore.Basics;
using BolomorzMathCore.LinearAlgebra.Matrix;

namespace BolomorzMathCore.LinearAlgebra.Algorithms;

public class CharacteristicPolynomial : AlgorithmBase<Complex, Complex[]>
{
    public CharacteristicPolynomial(CMatrix matrix) : base(new(1, 1), [])
    {
        if (!matrix.IsQuadratic()) throw new Exception($"cannot calculate CharacteristicPolynomial of non quadratic matrix. [M({matrix.Rows}x{matrix.Cols})]");
        Result = FaddeevLeVerrier(matrix);
    }
    public CharacteristicPolynomial(NMatrix matrix) : base(new(1), [])
    {
        if (!matrix.IsQuadratic()) throw new Exception($"cannot calculate CharacteristicPolynomial of non quadratic matrix. [M({matrix.Rows}x{matrix.Cols})]");
        Result = FaddeevLeVerrier(matrix);
    }

    private static Complex[] FaddeevLeVerrier(CMatrix H)
    {

        int n = H.Rows;

        Complex[] C = new Complex[n];
        CMatrix[] M = new CMatrix[n];

        int k = 2;

        C[n - 1] = new(1);
        C[n - 2] = -H.Trace();
        M[0] = new(SpecialQuadratic.Identity, n);

        while (k <= n)
        {
            M[k - 1] = H * M[k - 2] + C[n - k] * new CMatrix(SpecialQuadratic.Identity, n);
            C[n - k - 1] = -(1 / k) * (H * M[k - 1]).Trace();
            k++;
        }

        return C;
    }
    
    private static Complex[] FaddeevLeVerrier(NMatrix H)
    {

        int n = H.Rows;

        Complex[] C = new Complex[n];
        CMatrix[] M = new CMatrix[n];

        int k = 2;

        C[n - 1] = new(1);
        C[n - 2] = -H.Trace();
        M[0] = new(SpecialQuadratic.Identity, n);

        while (k <= n)
        {
            M[k - 1] = H * M[k - 2] + C[n - k] * new CMatrix(SpecialQuadratic.Identity, n);
            C[n - k - 1] = -(1 / k) * (H * M[k - 1]).Trace();
            k++;
        }

        return C;
    }

}