using Godot;

public partial class Effects : RefCounted
{
    public int amount;
    public virtual void Execute(Node[] targets) {}
}
