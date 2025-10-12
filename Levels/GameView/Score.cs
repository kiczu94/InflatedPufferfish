using Godot;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class Score : Label
{
    private int score = 0;
    private EventBinding<PlanktonEatenEvent> PlanktonEatenEventBinding;

    public override void _Ready()
    {
        PlanktonEatenEventBinding = new EventBinding<PlanktonEatenEvent>(OnPlanktonEatenEvent);
        EventBus<PlanktonEatenEvent>.Register(PlanktonEatenEventBinding);
        Text = score.ToString();
        base._Ready();
    }

    private void OnPlanktonEatenEvent(PlanktonEatenEvent @event)
    {
        score += 1;
        Text = score.ToString();
    }
}
