using Godot;

public partial class Effects : RefCounted
{
    public int amount;
    public AudioStream sound;
    public virtual void Execute(Node[] targets) {}
}
