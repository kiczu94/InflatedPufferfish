using Godot;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class GameOverLabel : Label
{
    private EventBinding<FishObstacleCollidedEvent> fishObstacleCollidedEventBinding;

    public override void _Ready()
    {
        fishObstacleCollidedEventBinding = new EventBinding<FishObstacleCollidedEvent>(OnFishObstacleCollidedEvent);
        EventBus<FishObstacleCollidedEvent>.Register(fishObstacleCollidedEventBinding);
        base._Ready();
    }

    private void OnFishObstacleCollidedEvent()
    {
        Visible = true;
    }
}
