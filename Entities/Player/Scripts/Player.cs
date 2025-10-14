using Godot;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class Player : CharacterBody2D
{
    public override void _Ready()
    {
        Velocity = new Vector2 (0, 10);
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        MoveAndSlide();
        base._PhysicsProcess(delta);
    }
}
