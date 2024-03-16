using Godot;

public partial class RunStartup : Resource
{
    public enum RunType { NEW, CONTINUE };

    [Export] public RunType runType;
    [Export] public CharacterStats selectedCharacterStats;
}
