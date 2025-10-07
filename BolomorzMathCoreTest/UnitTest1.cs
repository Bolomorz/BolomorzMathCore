using BolomorzMathCore.Basics;

namespace BolomorzMathCore.UnitTest;

public class ComplexTest
{
    public static TheoryData<Complex, Complex, Complex> Addition
        => new()
        {
            { new Complex(1, 1), new Complex(2, 2), new Complex(3, 3) },
            { new Complex(1.5, 1.5), new Complex(1.5, -3.5), new Complex(3, -2)}
        };


    [Theory]
    [MemberData(nameof(Addition))]
    public void TestAddition(Complex A, Complex B, Complex Result)
    {
        Assert.True(A + B == Result, $"{A} + {B} = {Result}");
    }
}
