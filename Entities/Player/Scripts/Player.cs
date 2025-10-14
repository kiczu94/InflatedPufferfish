using Godot;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class Player : CharacterBody2D
{
    private EventBinding<PlayerCollidedEvent> fishObstacleCollidedEventBinding;

    public override void _Ready()
    {
        fishObstacleCollidedEventBinding = new EventBinding<PlayerCollidedEvent>(OnFishObstacleCollidedEvent);
        EventBus<PlayerCollidedEvent>.Register(fishObstacleCollidedEventBinding);
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
