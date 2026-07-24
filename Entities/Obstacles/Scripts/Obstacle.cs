using Godot;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class Obstacle : Node2D
{
    private readonly Vector2 movingSpeed = new(-30, 0);

    private ObstacleResource obstacleResource;

    public override void _Ready()
    {
        base._Ready();
        var collisionPolygon = GetNode<CollisionPolygon2D>("ObstacleSprite/ObstacleArea2D/ObstacleCollisionPolygon2D");
        var sprite2D = GetNode<Sprite2D>("ObstacleSprite") as Sprite2D;
        collisionPolygon.Polygon = obstacleResource.Points;
        sprite2D.Texture = obstacleResource.Texture;
    }

    public override void _Process(double delta)
    {
        ProcessMovement(delta);
        NotifyIfOutOfView();
        base._Process(delta);
    }

    public void SetObstacleResource(ObstacleResource obstacleResource)
    {
        this.obstacleResource = obstacleResource;
    }

    private void NotifyIfOutOfView()
    {
        if (Position.X < -30)
        {
            EventBus<ObstacleOutOfFieldView>.Raise(new ObstacleOutOfFieldView(this.GetInstanceId()));
        }
    }

    private void ProcessMovement(double delta)
    {
        Position += movingSpeed * (float)delta;
    }
}