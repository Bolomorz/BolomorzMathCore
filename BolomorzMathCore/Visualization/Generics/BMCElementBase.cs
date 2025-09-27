namespace BolomorzMathCore.Visualization.Base;

public interface IBMCElement
{
    bool Equals(IBMCElement? other);
    void SetAttributes(Dictionary<string, object> attributes);
    bool IsPointInGeometry(BMCPoint point);
    bool QueryBy(Dictionary<string, object> query);
    BMCCollection GetBMCCollection();
    void Remove();
}

public abstract class BMCElementBase<T>(T value) : IBMCElement
{
    protected T Value = value;
    protected BMCCollection Collection = new();
    public abstract bool Equals(IBMCElement? other);
    public abstract void SetAttributes(Dictionary<string, object> attributes);
    public abstract bool IsPointInGeometry(BMCPoint point);
    public abstract bool QueryBy(Dictionary<string, object> query);
    public abstract BMCCollection GetBMCCollection();
    public abstract void Remove();
}