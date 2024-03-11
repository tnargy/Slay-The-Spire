using System;
using Godot;

public partial class BattleUI : CanvasLayer
{
    [Export] private CharacterStats _characterStats;
    public CharacterStats CharStats
    {
        get => _characterStats;
        set
        {
            _characterStats = value;
            hand.CharStats = _characterStats;
            mana.CharStats = _characterStats;
        }
    }
    Hand hand;
    ManaUI mana;
    Button endTurn;

    public override void _Ready()
    {
        hand = GetNode<Hand>("Hand");
        mana = GetNode<ManaUI>("ManaUI");
        endTurn = GetNode<Button>("%EndTurn");

        GameEvents.OnHandDrawn += () => endTurn.Disabled = false;
        endTurn.Pressed += HandleEndTurn;
    }

    private void HandleEndTurn()
    {
        endTurn.Disabled = true;
        GameEvents.RaiseEndTurn();
    }

}
