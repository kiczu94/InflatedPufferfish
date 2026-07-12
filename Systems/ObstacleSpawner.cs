using Godot;
using InflatedPufferfish.Events;
using InflatedPufferfish.TkoUtilities.Utilities;
using TkoUtilities.EventBus;

public partial class ObstacleSpawner : Node
{
    private bool timeToSpawn = true;

    private PackedScene obstacle;

    private EventBinding<ObstacleOutOfFieldView> obstacleOutOfFieldViewEventBinding;
    private EventBinding<SpawnObstacleEvent> spawnObstacleEventBinding;
    private Pool<Obstacle> obstaclePool = new();

    public override void _Ready()
    {
        obstacle = ResourceLoader.Load("res://Entities/Obstacle/Obstacle.tscn") as PackedScene;
        obstacleOutOfFieldViewEventBinding = new EventBinding<ObstacleOutOfFieldView>(OnObstacleOutOfFieldViewEvent);
        spawnObstacleEventBinding = new EventBinding<SpawnObstacleEvent>(OnSpawnObstacleEvent);
        EventBus<SpawnObstacleEvent>.Register(spawnObstacleEventBinding);
        EventBus<ObstacleOutOfFieldView>.Register(obstacleOutOfFieldViewEventBinding);
        base._Ready();
    }

    private void OnObstacleOutOfFieldViewEvent(ObstacleOutOfFieldView @event)
    {
        obstaclePool.AddToPool(@event.Id);
    }

    private void OnSpawnObstacleEvent(SpawnObstacleEvent @event)
    {
        var upObstacle = obstaclePool.GetFromPool(SpawnNewObstacle);
        upObstacle.SetPosition(new Vector2(400, @event.upObstaclePosition));
        upObstacle.Rotation = Mathf.DegToRad(180);
        var downObstacle = obstaclePool.GetFromPool(SpawnNewObstacle);
        downObstacle.SetPosition(new Vector2(400, @event.downObstaclePosition));
        downObstacle.Rotation = 0;
    }

    private Obstacle SpawnNewObstacle()
    {
        var obstacle = this.obstacle.Instantiate() as Obstacle;
        AddChild(obstacle);
        return obstacle;
    }
}
