using Godot;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class Enemy : Sprite2D
{
    private readonly Vector2 movingSpeed = new(-90  , 0);
    private EventBinding<PlayerCollidedEvent> fishObstacleCollidedEventBinding;

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
            EventBus<EnemyOutOfFieldView>.Raise(new EnemyOutOfFieldView(this.GetInstanceId()));
        }
    }

    private void ProcessMovement(double delta)
    {
        
            Position += movingSpeed * (float)delta;
        
    }
}
