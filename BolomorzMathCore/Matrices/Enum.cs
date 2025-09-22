namespace BolomorzMathCore.Matrices;

/// <summary>
/// <code>
/// Special Types:
/// - special quadratic (NxN):
///     zero: all Elements are 0+0i
///     identity: Diagonals are 1+0i, rest are 0+0i
/// </code>
/// </summary>
public enum SpecialQuadratic
{
    /// <summary>
    /// <code>
    /// zero: all Elements are 0+0i
    /// </code>
    /// </summary>
    Zero,
    /// <summary>
    /// <code>
    /// identity: Diagonals are 1+0i, rest are 0+0i
    /// </code>
    /// </summary>
    Identity
}

/// <summary>
/// <code>
/// Special Types:
/// - matrix array (N):
///     diagonals: (NxN) Diagonals are the elements of the matrix array, rest are 0+0i
///     vector: (Nx1) Elements are the elements of the matrix array
/// </code>
/// </summary>
public enum MatrixArray
{
    /// <summary>
    /// <code>
    /// diagonals: (NxN) Diagonals are the elements of the matrix array, rest are 0+0i
    /// </code>
    /// </summary>
    Diagonals,
    /// <summary>
    /// <code>
    /// vector: (Nx1) Elements are the elements of the matrix array
    /// </code>
    /// </summary>
    Vector
}