namespace BolomorzMathCore.Analysis;

/// <summary>
/// <code>
/// FunctionString
/// 
/// string representing on part of function F
/// 
/// Properties:
/// - Content: String | part of F
/// - SuperScript: Bool | content is displayed as superscript ?
/// </code>
/// </summary>
/// <see cref="Function"/> 
public class FunctionString
{
    public string Content { get; private set; }
    public bool SuperScript { get; private set; }

    internal FunctionString(string content, bool superscript)
    {
        Content = content;
        SuperScript = superscript;
    }
}

/// <summary>
/// <code>
/// FunctionStringCollection
/// 
/// collection of strings representing parts of function F
/// 
/// Getters:
/// - GetFunctionStrings: FunctionString[] | collection of strings representing parts of function F
/// </code>
/// </summary>
/// <see cref="FunctionString"/> 
/// <see cref="Function"/> 
public class FunctionStringCollection
{
    internal List<FunctionString> _FunctionStrings { get; set; } = [];
    public FunctionString[] GetFunctionStrings() => [.. _FunctionStrings];
}