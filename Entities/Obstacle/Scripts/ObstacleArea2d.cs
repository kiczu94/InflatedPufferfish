using Godot;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class ObstacleArea2d : Area2D
{
    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
        base._Ready();
    }

    public void OnBodyEntered(Node2D body)
    {
        if (body.GetGroups().Contains("Player"))
        {
            EventBus<FishObstacleCollidedEvent>.Raise(new FishObstacleCollidedEvent());
        }
    }
}
