using System;
using Godot;

public partial class Hand : HBoxContainer
{
    [Export] private CharacterStats _characterStats;
    public CharacterStats CharStats
    {
        get => _characterStats;
        set => SetStats(value);
    }
    void SetStats(CharacterStats value)
    {
        if (value == null) { return; }
        _characterStats = value;
    }
    PackedScene cardUI;
    private int cardsPlayedThisTurn = 0;

    public override void _Ready()
    {
        cardUI = GD.Load<PackedScene>("res://Scenes/UI/Card UI/card_ui.tscn");
        CardUI.OnReparentRequest += HandleReparentRequest;
        GameEvents.OnCardPlayed += (card) => cardsPlayedThisTurn++;
        SetStats(_characterStats);
    }

    public void AddCard(Card card)
    {
        var newCardUI = cardUI.Instantiate<CardUI>();
        AddChild(newCardUI);
        newCardUI.card = card;
        newCardUI.CharStats = CharStats;
    }

    public void DiscardCard(CardUI cardUI)
    {
        cardUI.QueueFree();
    }

    public void DisableHand()
    {
        foreach (CardUI item in GetChildren())
        {
            item.disabled = true;
        }   
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
