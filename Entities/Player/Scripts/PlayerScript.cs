using Godot;

public partial class PlayerScript : CharacterBody2D
{
    public string lastStatePath; 
    public Area2D blockingArea;

    public override void _Ready()
    {
        blockingArea = GetNode<Area2D>("BlockArea");
        Velocity = new Vector2 (0, 10);
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        MoveAndSlide();
        base._PhysicsProcess(delta);
    }
}
