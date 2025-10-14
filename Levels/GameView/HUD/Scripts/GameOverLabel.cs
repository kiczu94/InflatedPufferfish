using Godot;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class GameOverLabel : Label
{
    private EventBinding<PlayerCollidedEvent> fishObstacleCollidedEventBinding;

    public override void _Ready()
    {
        fishObstacleCollidedEventBinding = new EventBinding<PlayerCollidedEvent>(OnFishObstacleCollidedEvent);
        EventBus<PlayerCollidedEvent>.Register(fishObstacleCollidedEventBinding);
        base._Ready();
    }

    private void OnFishObstacleCollidedEvent()
    {
        Visible = true;
    }
}
