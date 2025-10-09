using Godot;
using System;

public partial class PlaceholderObstacle : Node
{
    private Vector2 movingSpeed = new(-10, 0);
    private StaticBody2D UpObstacle;
    private StaticBody2D DownObstacle;

    public override void _Ready()
    {
        UpObstacle = GetNode<StaticBody2D>("UpObstacle");
        DownObstacle = GetNode<StaticBody2D>("DownObstacle");
        base._Ready();
    }

    public override void _Process(double delta)
    {
        UpObstacle.Position += movingSpeed * (float)delta;
        DownObstacle.Position += movingSpeed * (float)delta;
        base._Process(delta);
    }
}
