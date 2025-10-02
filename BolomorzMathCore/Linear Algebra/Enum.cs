namespace BolomorzMathCore.LinearAlgebra;

/// <summary>
/// <code>
/// Special Matrix Types:
/// - special quadratic (NxN):
///     zero: all Elements are 0+0i|0
///     identity: Diagonals are 1+0i|1, rest are 0+0i|0
/// </code>
/// </summary>
public enum SpecialMatrix
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
/// Special Matrix Types:
/// - vector matrix (N):
///     diagonals: (NxN) Diagonals are the elements of the matrix array, rest are 0+0i|0
///     vector: (Nx1) Elements are the elements of the matrix array
/// </code>
/// </summary>
public enum VectorMatrix
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

/// <summary>
/// <code>
/// Special Vector Types:
/// - zero: all Elements are 0+0i|0
/// </code>
/// </summary>
public enum SpecialVector
{
    /// <summary>
    /// <code>
    /// zero: all Elements are 0+0i|0
    /// </code>
    /// </summary>
    Zero
}

/// <summary>
/// <code>
/// Vector Types in relation to matrices:
/// - row: vector depicts a row of a matrix
/// - column: vector depicts a column of a matrix
/// </code>
/// </summary>
public enum VectorType
{
    /// <summary>
    /// <code>
    /// row: vector depicts a row of a matrix
    /// </code>
    /// </summary>
    Row,
    /// <summary>
    /// <code>
    /// column: vector depicts a column of a matrix
    /// </code>
    /// </summary>
    Column
}