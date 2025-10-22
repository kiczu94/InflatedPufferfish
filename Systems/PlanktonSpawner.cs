using Godot;
using InflatedPufferfish.Events;
using InflatedPufferfish.TkoUtilities.Utilities;
using TkoUtilities.EventBus;

public partial class PlanktonSpawner : Node
{
    private PackedScene plankton;
    private EventBinding<SpawnPlanktonEvent> spawnPlanktonEventBinding;
    private EventBinding<PlanktonOutOfFieldView> planktonOutOfFieldViewEventBinding;
    private EventBinding<PlanktonEatenEvent> planktonEatenEventBinding;
    private Pool<Plankton> planktonPool = new();


    public override void _Ready()
    {
        plankton = ResourceLoader.Load("res://Entities/Plankton/Plankton.tscn") as PackedScene;
        spawnPlanktonEventBinding = new EventBinding<SpawnPlanktonEvent>(OnSpawnPlanktonEvent);
        planktonOutOfFieldViewEventBinding = new EventBinding<PlanktonOutOfFieldView>(OnPlanktonOutOfFieldView);
        planktonEatenEventBinding = new EventBinding<PlanktonEatenEvent>(OnPlanktonEatenEvent);
        EventBus<SpawnPlanktonEvent>.Register(spawnPlanktonEventBinding);
        EventBus<PlanktonOutOfFieldView>.Register(planktonOutOfFieldViewEventBinding);
        EventBus<PlanktonEatenEvent>.Register(planktonEatenEventBinding);
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
        var plankton = this.plankton.Instantiate() as Plankton;
        AddChild(plankton);
        return plankton;
    }
}
