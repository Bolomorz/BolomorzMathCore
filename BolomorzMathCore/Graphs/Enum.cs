namespace BolomorzMathCore.Graphs;

/// <summary>
/// <code>
/// GraphType: 
///     Directed        edges have a direction from startvertex to endvertex
///     NonDirected     edges are bidirectional
/// </code>
/// </summary>
public enum GraphType
{
    /// <summary>
    /// <code>
    /// edges have a direction from startvertex to endvertex
    /// </code>
    /// </summary>
    Directed,
    /// <summary>
    /// <code>
    /// edges are bidirectional
    /// </code>
    /// </summary>
    Undirected
}

/// <summary>
/// <code>
/// GraphWeighting:
///     Weighted        edges have a weighting | cost traversing edge
///     NonWeighted     edges are weightless
/// </code>
/// </summary>
public enum GraphWeighting
{
    /// <summary>
    /// <code>
    /// edges have a weighting | cost traversing edge
    /// </code>
    /// </summary>
    Weighted,
    /// <summary>
    /// <code>
    /// edges are weightless
    /// </code>
    /// </summary>
    NonWeighted
}
