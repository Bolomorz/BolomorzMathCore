using BolomorzMathCore.Visualization.Base;

namespace BolomorzMathCore.Visualization.Command;

public class CommandResult
{
    internal List<string> Lines { get; set; } = [];
    public string[] GetResult() => [.. Lines];
}

public class Command<T>
{
    public required T DoCommand { get; set; }
    public required IBMCElement? Target { get; set; }
}