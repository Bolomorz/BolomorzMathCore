namespace BolomorzMathCore.Matrices.Algorithms;

public class QRTransform
{
    
    protected CMatrix Matrix { get; set; }

    internal QRTransform(HessenbergTransform hessenberg)
    {
        CMatrix hbt = hessenberg.GetResult();
        throw new NotImplementedException();
    }

    public CMatrix GetResult() => Matrix; 

}