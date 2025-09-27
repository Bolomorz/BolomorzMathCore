using BolomorzMathCore.Basics;
using BolomorzMathCore.LinearAlgebra.Base;

namespace BolomorzMathCore.LinearAlgebra.Matrix;

/// <summary>
/// <code>
/// Determinant[Complex] of Matrix[Complex] NxN (Quadratic Matrix)
/// 
/// Determinant[Complex] = det(A) or |A| = [Complex]
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
/// <see cref="Complex"/>
/// <see cref="CMatrix"/>
public class CDeterminant(CMatrix matrix) : DeterminantBase<Complex, CMatrix>(matrix)
{
    protected override Complex LUPDeterminant(Decomposition decomposition, int n)
    {
        Complex d = decomposition.Decompose[0, 0];

        for (int i = 1; i < n; i++)
            d *= decomposition.Decompose[i, i];

        return (decomposition.P[n] - n) % 2 == 0 ? d : -d;
    }

    protected override Decomposition LUPDecompose(Complex[,] A, int n)
    {
        int i, j, k, imax;
        Complex maxA, absA;
        Complex[] ptr = new Complex[n];

        int[] P = new int[n + 1];
        Complex[,] decompose = new Complex[n, n];

        for (i = 0; i < n; i++)
            for (j = 0; j < n; j++)
                decompose[i, j] = A[i, j];

        for (i = 0; i <= n; i++) P[i] = i;

        for (i = 0; i < n; i++)
        {
            maxA = new Complex();
            imax = i;

            for (k = i; k < n; k++)
            {
                absA = new Complex(decompose[k, i].Absolute());
                if (absA > maxA)
                {
                    maxA = absA;
                    imax = k;
                }
            }

            if (maxA < Complex.Tolerance)
                return new() { Decompose = decompose, P = P, Success = false };

            if (imax != i)
            {
                j = P[i];
                P[i] = P[imax];
                P[imax] = j;

                for (k = 0; k < n; k++) ptr[k] = decompose[i, k];
                for (k = 0; k < n; k++) decompose[i, k] = decompose[imax, k];
                for (k = 0; k < n; k++) decompose[imax, k] = ptr[k];

                P[n]++;
            }

            for (j = i + 1; j < n; j++)
            {
                decompose[j, i] /= decompose[i, i];
                for (k = i + 1; k < n; k++) decompose[j, k] -= decompose[j, i] * decompose[i, k];
            }
        }

        return new() { Decompose = decompose, P = P, Success = true };
    }

    protected override Complex[,] SubMatrix(Complex[,] m, int j, int k, int n)
    {
        Complex[,] sub = new Complex[n - 1, n - 1];

        int subrow = 0;
        int subcol;
        j--; k--;
        for (int row = 0; row < n; row++)
        {
            if (row != j)
            {
                subcol = 0;
                for (int col = 0; col < n; col++)
                    if (col != k)
                        sub[subrow, subcol++] = m[row, col];
                subrow++;
            }
        }

        return sub;
    }

    protected override Complex CalculateDeterminant(Complex[,] m, int n)
    {
        if (n == 2) return m[0, 0] * m[1, 1] - m[0, 1] * m[1, 0];

        Complex sum = new();
        List<Complex> dets = new();

        for (int i = 1; i <= n; i++)
            dets.Add(CalculateDeterminant(SubMatrix(m, 1, i, n), n - 1));

        for (int col = 1; col <= n; col++)
        {
            Complex sub = dets[col - 1];
            sum += (col + 1) % 2 == 0 ?
                m[0, col - 1] * sub :
                -1 * m[0, col - 1] * sub;
        }

        return sum;
    }
}