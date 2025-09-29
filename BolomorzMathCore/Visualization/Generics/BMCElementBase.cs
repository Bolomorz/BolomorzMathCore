namespace BolomorzMathCore.Visualization.Base;

public interface IBMCElement
{
    bool Equals(IBMCElement? other);
    void SetAttributes(Dictionary<string, object> attributes);
    void SetPosition(BMCPoint center);
    bool IsPointInGeometry(BMCPoint point);
    bool QueryBy(Dictionary<string, object> query);
    BMCCollection GetBMCCollection();
    void Remove();
    void Update();
}

public abstract class BMCElementBase<T>(T value) : IBMCElement
{
    protected T Value = value;
    protected BMCCollection Collection = new();
    internal void AddGeometry(BMCGeometryBase geometry)
    {
        Collection.Geometries.Add(geometry);
    }
    internal void AddPoint(BMCPoint point)
    {
        Collection.Points.Add(point);
    }
    public abstract bool Equals(IBMCElement? other);
    public abstract void SetAttributes(Dictionary<string, object> attributes);
    public abstract bool IsPointInGeometry(BMCPoint point);
    public abstract bool QueryBy(Dictionary<string, object> query);
    public abstract BMCCollection GetBMCCollection();
    public abstract void Remove();
    public abstract void Update();
    public abstract void SetPosition(BMCPoint center);
}