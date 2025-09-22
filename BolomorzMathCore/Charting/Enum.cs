namespace BolomorzMathCore.Charting;

/// <summary>
/// <code>
/// FunctionType | type of function
/// - NaF:          f(x) = not a function
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
    /// NaF:            f(x) = not a function
    /// </code>
    /// </summary>
    NaF,

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