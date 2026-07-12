using Godot;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class GameOverLabel : Label
{
    private EventBinding<GameLost> gameLostEventBinding;

    public override void _Ready()
    {
        gameLostEventBinding = new EventBinding<GameLost>(OnGameLost);
        EventBus<GameLost>.Register(gameLostEventBinding);
        base._Ready();
    }

    private void OnGameLost()
    {
        Visible = true;
    }
}
