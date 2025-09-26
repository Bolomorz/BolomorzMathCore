namespace BolomorzMathCore.LinearAlgebra.Generics;

public abstract class AlgorithmBase<T, U>(T input) where T : class where U : class
{
    protected T Input = input;
    protected U? Result;
    public U? GetResult() => Result;
}