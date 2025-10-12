using Godot;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class PlanktonArea : Area2D
{
    private Sprite2D mainSprite;

    public override void _Ready()
    {
        mainSprite = GetParent<Sprite2D>();
        BodyEntered += OnBodyEntered;
        base._Ready();
    }

    public void OnBodyEntered(Node2D body)
    {
        if (body.GetGroups().Contains("Player"))
        {
            EventBus<PlanktonEatenEvent>.Raise(new PlanktonEatenEvent(mainSprite.GetInstanceId()));
        }
    }
}
