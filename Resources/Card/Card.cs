using System.Linq;
using Godot;

public partial class Card : Resource
{
    public enum Type { ATTACK, SKILL, POWER }
    public enum Target { SELF, SINGLE_ENEMY, ALL_ENEMIES, EVERYONE }
 
    [ExportGroup("Card Attributes")]
    [Export] public string id { get; private set; }
    [Export] public Type type { get; private set; }
    [Export] public Target target { get; private set; }
    [Export(PropertyHint.Range, "0,999")] public int cost;
    
    [ExportGroup("Card Visuals")]
    [Export] public Texture2D icon;
    [Export(PropertyHint.MultilineText)] public string tooltopText;
    [Export] public AudioStream sound;

    public bool IsSingleTargeted() => target == Target.SINGLE_ENEMY;

    Node[] GetTargets(Node[] targets)
    {
        if (targets == null) { return null; }
        
        SceneTree tree = targets[0].GetTree();
        switch (target)
        {
            case Target.SELF:
                return tree.GetNodesInGroup("Player").ToArray();
            case Target.ALL_ENEMIES:
                return tree.GetNodesInGroup("Enemies").ToArray();
            case Target.EVERYONE:
                return tree.GetNodesInGroup("Player").Concat(tree.GetNodesInGroup("enemies")).ToArray();
            default:
                return null;
        }
    }

    public void Play(Node[] targets, CharacterStats stats)
    {
        GameEvents.RaiseCardPlayed(this);
        stats.Mana -= cost;
        
        if (IsSingleTargeted())
        {
            ApplyEffects(targets);
        }
        else
        {
            ApplyEffects(GetTargets(targets));
        }
    }

    public virtual void ApplyEffects(Node[] nodes) {}
}
