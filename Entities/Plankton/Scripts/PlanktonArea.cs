using Godot;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class PlanktonArea : Area2D
{
    Sprite2D MainSprite;

    public override void _Ready()
    {
        MainSprite = GetParent<Sprite2D>();
        BodyEntered += OnBodyEntered;
        base._Ready();
    }

    public void OnBodyEntered(Node2D body)
    {
        if (body.GetGroups().Contains("Player"))
        {
            EventBus<PlanktonEatenEvent>.Raise(new PlanktonEatenEvent(MainSprite.GetInstanceId()));
        }
    }
}
