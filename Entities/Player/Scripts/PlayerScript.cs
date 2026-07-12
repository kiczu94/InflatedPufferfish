using Godot;

public partial class PlayerScript : CharacterBody2D
{
    public string lastStatePath; 
    public Area2D blockingArea;
    public AnimatedSprite2D animatedSprite;
    public AnimationPlayer animationPlayer;
    public CollisionShape2D playerCollisionShape;

    public override void _Ready()
    {
        blockingArea = GetNode<Area2D>("BlockArea");
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        playerCollisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
        Velocity = new Vector2 (0, 10);
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        MoveAndSlide();
        base._PhysicsProcess(delta);
    }
}
