namespace BolomorzMathCore.Basics;

/// <summary>
/// <code>
/// base class for algorithms
/// 
/// type {T}: input type
/// type {U}: output type
/// 
/// GetResult(): U
/// </code>
/// </summary>
/// <typeparam name="T">input type of algorithm</typeparam>
/// <typeparam name="U">output type of algorithm</typeparam>
public abstract class AlgorithmBase<T, U>(T input, U init) where T : class where U : class
{
    protected T Input = input;
    protected U Result = init;
    public U GetResult() => Result;
}