using Godot;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class Obstacle : Sprite2D
{
    private readonly Vector2 movingSpeed = new(-30, 0);

    public override void _Process(double delta)
    {
        Position += movingSpeed * (float)delta;
        if (Position.X < -30)
        {
            EventBus<ObstacleOutOfFieldView>.Raise(new ObstacleOutOfFieldView(this.GetInstanceId()));
        }
    }
}
