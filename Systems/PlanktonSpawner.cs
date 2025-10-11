using Godot;
using InflatedPufferfish.Events;
using InflatedPufferfish.TkoUtilities.Pooling;
using TkoUtilities.EventBus;

public partial class PlanktonSpawner : Node
{
    private EventBinding<SpawnPlanktonEvent> SpawnPlanktonEventBinding;
    private EventBinding<PlanktonOutOfFieldView> PlanktonOutOfFieldViewEventBinding;
    private EventBinding<PlanktonEatenEvent> PlanktonEatenEventBinding;

    PackedScene Plankton;
    Pool<Plankton> planktonPool = new();

    public override void _Ready()
    {
        Plankton = ResourceLoader.Load("res://Entities/Plankton/Plankton.tscn") as PackedScene;
        SpawnPlanktonEventBinding = new EventBinding<SpawnPlanktonEvent>(OnSpawnPlanktonEvent);
        PlanktonOutOfFieldViewEventBinding = new EventBinding<PlanktonOutOfFieldView>(OnPlanktonOutOfFieldView);
        PlanktonEatenEventBinding = new EventBinding<PlanktonEatenEvent>(OnPlanktonEatenEvent);
        EventBus<SpawnPlanktonEvent>.Register(SpawnPlanktonEventBinding);
        EventBus<PlanktonOutOfFieldView>.Register(PlanktonOutOfFieldViewEventBinding);
        EventBus<PlanktonEatenEvent>.Register(PlanktonEatenEventBinding);
        base._Ready();
    }

    private void OnSpawnPlanktonEvent(SpawnPlanktonEvent @event)
    {
        var plankton = planktonPool.GetFromPool(Instantiate);
        plankton.SetPosition(new Vector2(320,@event.positionY));
    }

    private void OnPlanktonOutOfFieldView(PlanktonOutOfFieldView @event)
    {
        planktonPool.AddToPool(@event.Id);
    }

    private void OnPlanktonEatenEvent(PlanktonEatenEvent @event)
    {
        planktonPool.AddToPool(@event.Id);
    }

    private Plankton Instantiate()
    {
        var plankton = Plankton.Instantiate() as Plankton;
        AddChild(plankton);
        return plankton;
    }
}
