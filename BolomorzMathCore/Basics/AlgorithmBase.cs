namespace BolomorzMathCore.Basics;

public abstract class AlgorithmBase<T, U>(T input, U init) where T : class where U : class
{
    protected T Input = input;
    protected U Result = init;
    public U GetResult() => Result;
}