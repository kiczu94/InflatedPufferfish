using Godot;
using InflatedPufferfish.Events;
using System;
using TkoUtilities.EventBus;

public partial class Player : CharacterBody2D
{
    private EventBinding<FishObstacleCollidedEvent> FishObstacleCollidedEventBinding;

    public override void _Ready()
    {
        FishObstacleCollidedEventBinding = new EventBinding<FishObstacleCollidedEvent>(OnFishObstacleCollidedEvent);
        EventBus<FishObstacleCollidedEvent>.Register(FishObstacleCollidedEventBinding);
        Velocity = new Vector2 (0, 10);
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        MoveAndSlide();
        base._PhysicsProcess(delta);
    }

    public void OnFishObstacleCollidedEvent()
    {
        GD.Print("Fish collided");
    }
}
