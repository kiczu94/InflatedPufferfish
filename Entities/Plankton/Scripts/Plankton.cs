using Godot;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class Plankton : Sprite2D
{
    private readonly Vector2 movingSpeed = new(-30, 0);

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _Process(double delta)
    {
        ProcessMovement(delta);
        NotifyIfOutOfView();
        base._Process(delta);
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
