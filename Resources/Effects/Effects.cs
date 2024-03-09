using Godot;

public partial class Effects : RefCounted
{
    public int ammount;
    public virtual void Execute(Node[] targets) {}
}
