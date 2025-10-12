using Godot;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class Score : Label
{
    private int score = 0;
    private EventBinding<PlanktonEatenEvent> planktonEatenEventBinding;

    public override void _Ready()
    {
        planktonEatenEventBinding = new EventBinding<PlanktonEatenEvent>(OnPlanktonEatenEvent);
        EventBus<PlanktonEatenEvent>.Register(planktonEatenEventBinding);
        Text = score.ToString();
        base._Ready();
    }

    private void OnPlanktonEatenEvent(PlanktonEatenEvent @event)
    {
        score += 1;
        Text = score.ToString();
    }
}
