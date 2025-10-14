using Godot;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class GameView : Node
{
    private EventBinding<PlayerCollidedEvent> playerCollidedEventBinding;
    public override void _Ready()
    {
        playerCollidedEventBinding = new EventBinding<PlayerCollidedEvent>(OnPlayerColididedEvent);
        EventBus<PlayerCollidedEvent>.Register(playerCollidedEventBinding);
        base._Ready();
    }

    private void OnPlayerColididedEvent()
    {
        GetTree().Paused = true;
    }
}
