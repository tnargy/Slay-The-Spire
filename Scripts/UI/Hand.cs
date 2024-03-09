using System;
using Godot;

public partial class Hand : HBoxContainer
{
    private int cardsPlayedThisTurn = 0;

    public override void _Ready()
    {
        CardUI.OnReparentRequest += HandleReparentRequest;
        GameEvents.OnCardPlayed += (card) => cardsPlayedThisTurn++;
    }

    private void HandleReparentRequest(CardUI child)
    {
        child.Reparent(this);
        var newIndex = Math.Clamp(child.origIndex - cardsPlayedThisTurn, 0, GetChildCount());
        CallDeferred("move_child", new Variant[] {child, newIndex});
    }

}
