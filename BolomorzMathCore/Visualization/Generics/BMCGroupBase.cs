using BolomorzMathCore.Visualization.Command;

namespace BolomorzMathCore.Visualization.Base;

public abstract class BMCGroupBase<T, U>(T value, BMCSettings settings)
{
    protected T Value = value;
    internal BMCSettings Settings = settings;
    protected IBMCElement? ActiveElement;

    public abstract BMCCanvas GetCanvas();
    public abstract IBMCElement? GetActiveElement();
    public abstract void SetActiveElement(IBMCElement? element);
    public abstract IBMCElement? ElementOfPosition(BMCPoint position);
    public abstract IBMCElement? QueryBy(Dictionary<string, object> query);
    public abstract CommandResult Command(Command<U> command);
}