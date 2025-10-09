using Godot;
using System;

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
