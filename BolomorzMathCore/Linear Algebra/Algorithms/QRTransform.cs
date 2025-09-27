using BolomorzMathCore.Basics;
using BolomorzMathCore.LinearAlgebra.Algorithms;

namespace BolomorzMathCore.Matrices.Algorithms;

public class QRTransform : AlgorithmBase<HessenbergTransform, Number>
{
    internal QRTransform(HessenbergTransform hessenberg) : base(hessenberg, new())
    {
        throw new NotImplementedException();
    }

}