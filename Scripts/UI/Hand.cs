using System;
using Godot;

public partial class Hand : HBoxContainer
{
    [Export] private CharacterStats _characterStats;
    public CharacterStats CharStats
    {
        get => _characterStats;
        set
        {
            _characterStats = value;
        }
    }
    PackedScene cardUI;
    private int cardsPlayedThisTurn = 0;

    public override void _Ready()
    {
        cardUI = ResourceLoader.Load<PackedScene>("res://Scenes/Card UI/card_ui.tscn");
        CardUI.OnReparentRequest += HandleReparentRequest;
        GameEvents.OnCardPlayed += (card) => cardsPlayedThisTurn++;
    }

    public void AddCard(Card card)
    {
        var newCardUI = cardUI.Instantiate<CardUI>();
        AddChild(newCardUI);
        newCardUI.card = card;
        newCardUI.CharStats = CharStats;
    }

    private void HandleReparentRequest(CardUI child)
    {
        child.disabled = true;
        child.Reparent(this);
        var newIndex = Math.Clamp(child.origIndex - cardsPlayedThisTurn, 0, GetChildCount());
        CallDeferred("move_child", new Variant[] {child, newIndex});
        child.SetDeferred("disabled", false);
    }

}
