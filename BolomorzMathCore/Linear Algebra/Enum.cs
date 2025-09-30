namespace BolomorzMathCore.LinearAlgebra;

/// <summary>
/// <code>
/// Special Types:
/// - special quadratic (NxN):
///     zero: all Elements are 0+0i|0
///     identity: Diagonals are 1+0i|1, rest are 0+0i|0
/// </code>
/// </summary>
public enum SpecialQuadratic
{
    /// <summary>
    /// <code>
    /// zero: all Elements are 0+0i|0
    /// </code>
    /// </summary>
    Zero,
    /// <summary>
    /// <code>
    /// identity: Diagonals are 1+0i|1, rest are 0+0i|1
    /// </code>
    /// </summary>
    Identity
}

/// <summary>
/// <code>
/// Special Types:
/// - matrix array (N):
///     diagonals: (NxN) Diagonals are the elements of the matrix array, rest are 0+0i|0
///     vector: (Nx1) Elements are the elements of the matrix array
/// </code>
/// </summary>
public enum MatrixArray
{
    /// <summary>
    /// <code>
    /// diagonals: (NxN) Diagonals are the elements of the matrix array, rest are 0+0i|0
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

public enum SpecialVector
{
    Zero
}

public enum VectorType
{
    Row,
    Column
}