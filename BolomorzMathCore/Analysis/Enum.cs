namespace BolomorzMathCore.Analysis;

/// <summary>
/// <code>
/// FunctionType | type of function
/// - Constant:     f(x) = constant
/// - Line:         f(x) = coeff0 + coeff1 * x
/// - Polynomial:   f(x) = Sum[i from 0 to n](coeff[i] * x^i)
/// - Exponential:  f(x) = coeff * base^x
/// - Power:        f(x) = coeff * x^expo
/// - Logarithm:    f(x) = coeff0 + coeff1 * Log(x)
/// </code>
/// </summary>
public enum FunctionType
{
    /// <summary>
    /// <code>
    /// Constant:            f(x) = not a function or constant
    /// </code>
    /// </summary>
    Constant,

    /// <summary>
    /// <code>
    /// Line:           f(x) = coeff0 + coeff1 * x
    /// </code>
    /// </summary>
    Line,

    /// <summary>
    /// <code>
    /// Polynomial:     f(x) = Sum[i from 0 to n](coeff[i] * x^i)   
    /// </code>
    /// </summary>
    Polynomial,

    /// <summary>
    /// <code>
    /// Logarithm:      f(x) = coeff0 + coeff1 * Log(x)
    /// </code>
    /// </summary>
    Logarithm,

    /// <summary>
    /// <code>
    /// Power:          f(x) = coeff * x^expo
    /// </code>
    /// </summary>
    Power,

    /// <summary>
    /// <code>
    /// Exponential:    f(x) = coeff * base^x
    /// </code>
    /// </summary>
    Exponential
}

public enum CompositionType
{
    CompositeFunction,
    SubFunction
}

public enum Script
{
    Superscript,
    Baseline,
    Subscript
}