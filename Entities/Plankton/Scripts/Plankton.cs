using Godot;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class Plankton : Sprite2D
{
    private bool gameRunning = true;
    private readonly Vector2 movingSpeed = new(-30, 0);
    private EventBinding<FishObstacleCollidedEvent> fishObstacleCollidedEventBinding;

    public override void _Ready()
    {
        fishObstacleCollidedEventBinding = new EventBinding<FishObstacleCollidedEvent>(OnFishObstacleCollidedEvent);
        EventBus<FishObstacleCollidedEvent>.Register(fishObstacleCollidedEventBinding);
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
        if (gameRunning)
        {
            Position += movingSpeed * (float)delta;
        }
    }

    private void OnFishObstacleCollidedEvent()
    {
        gameRunning = false;
    }
}
