using Godot;
using InflatedPufferfish.Events;
using InflatedPufferfish.TkoUtilities.Pooling;
using TkoUtilities.EventBus;

public partial class ObstacleSpawner : Node
{
    bool TimeToSpawn = true;
    EventBinding<ObstacleOutOfFieldView> ObstacleOutOfFieldViewEventBinding;
    EventBinding<SpawnObstacleEvent> SpawnObstacleEventBinding;

    Pool<Obstacle> obstaclePool = new();
    PackedScene Obstacle;

    public override void _Ready()
    {
        Obstacle = ResourceLoader.Load("res://Entities/Obstacle/Obstacle.tscn") as PackedScene;
        ObstacleOutOfFieldViewEventBinding = new EventBinding<ObstacleOutOfFieldView>(OnObstacleOutOfFieldViewEvent);
        SpawnObstacleEventBinding = new EventBinding<SpawnObstacleEvent>(OnSpawnObstacleEvent);
        EventBus<SpawnObstacleEvent>.Register(SpawnObstacleEventBinding);
        EventBus<ObstacleOutOfFieldView>.Register(ObstacleOutOfFieldViewEventBinding);
        base._Ready();
    }

    private void OnObstacleOutOfFieldViewEvent(ObstacleOutOfFieldView @event)
    {
        obstaclePool.AddToPool(@event.Id);
    }

    private void OnSpawnObstacleEvent(SpawnObstacleEvent @event)
    {
        var upObstacle = obstaclePool.GetFromPool(SpawnNewObstacle);
        upObstacle.SetPosition(new Vector2(320, @event.upObstaclePosition));
        var downObstacle = obstaclePool.GetFromPool(SpawnNewObstacle);
        downObstacle.SetPosition(new Vector2(320, @event.downObstaclePosition));
    }

    private Obstacle SpawnNewObstacle()
    {
        var obstacle = Obstacle.Instantiate() as Obstacle;
        AddChild(obstacle);
        return obstacle;
    }
}
