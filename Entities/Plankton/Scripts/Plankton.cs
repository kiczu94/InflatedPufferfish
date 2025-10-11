using Godot;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class Plankton : Sprite2D
{
    private readonly Vector2 movingSpeed = new(-30, 0);

    public override void _Process(double delta)
    {
        base._Process(delta);
        Position += movingSpeed * (float)delta;
        if (Position.X < -30)
        {
            EventBus<PlanktonOutOfFieldView>.Raise(new PlanktonOutOfFieldView(this.GetInstanceId()));
        }
    }
}
