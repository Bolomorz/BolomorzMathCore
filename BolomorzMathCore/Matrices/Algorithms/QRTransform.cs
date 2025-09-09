namespace BolomorzMathCore.Matrices.Algorithms;

public class QRTransform
{
    
    protected Matrix Matrix { get; set; }

    internal QRTransform(HessenbergTransform hessenberg)
    {
        Matrix hbt = hessenberg.GetResult();
        throw new NotImplementedException();
    }

    internal Matrix GetResult() => Matrix; 

}