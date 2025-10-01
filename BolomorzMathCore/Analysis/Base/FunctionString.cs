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
public class FunctionString
{
    public string Content { get; init; }
    public Script Script { get; init; }

    internal FunctionString(string content, Script script)
    {
        Content = content;
        Script = script;
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
public class FunctionStringCollection(CompositionType fc)
{
    internal List<FunctionString> _FunctionStrings { get; set; } =
        fc == CompositionType.CompositeFunction ?
            [
                new("f(x) = ", Script.Baseline),
                new(";", Script.Baseline)
            ] :
            [
                new("{", Script.Baseline),
                new("}", Script.Baseline)
            ];

    internal bool IsEmpty()
        => _FunctionStrings.Count == 0;
    internal void Add(FunctionStringCollection fscoll)
    {
        foreach (var fs in fscoll._FunctionStrings)
            _FunctionStrings.Add(fs);
    }
    internal void Add(FunctionStringCollection fscoll, Script script)
    {
        foreach (var fs in fscoll._FunctionStrings)
            _FunctionStrings.Add(new(fs.Content, script));
    }
    internal void Add(FunctionString fs)
    {
        _FunctionStrings.Insert(_FunctionStrings.Count - 2, fs);
    }
    public void Reduce()
    {
        List<FunctionString> reduced = [];
        string current = "";
        Script prevScript = Script.Baseline;
        for (int i = 0; i < _FunctionStrings.Count; i++)
        {
            var fs = _FunctionStrings[i];
            if (fs.Script == prevScript)
            {
                current += fs.Content;
            }
            else
            {
                reduced.Add(new(current, prevScript));
                current = fs.Content;
                prevScript = fs.Script;
            }
        }
        _FunctionStrings = reduced;
    }

    public FunctionString[] GetFunctionStrings() => [.. _FunctionStrings];
}