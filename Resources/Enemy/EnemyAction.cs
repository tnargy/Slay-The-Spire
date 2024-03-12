using Godot;

public partial class EnemyAction : Node
{
    public enum Type { CONDITIONAL, CHANCE_BASED }

    [Export] public Type ActionType { get; private set; }
    [Export(PropertyHint.Range, "0.0, 10.0, 0.1")] public float chanceWeight = 0;

    public Enemy enemy;
    public Node2D target;
    public float accumulatedWeight = 0;

    public virtual bool IsPerformable() { return false; }
    public virtual void PerformAction() {}
}
