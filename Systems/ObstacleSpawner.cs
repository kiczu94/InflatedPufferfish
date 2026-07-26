using Godot;
using InflatedPufferfish.Events;
using InflatedPufferfish.TkoUtilities.Utilities;
using TkoUtilities.EventBus;
using TkoUtilities.Utilities;

public partial class ObstacleSpawner : Node
{
    private bool timeToSpawn = true;

    private PackedScene obstacle;

    private EventBinding<ObstacleOutOfFieldView> obstacleOutOfFieldViewEventBinding;
    private EventBinding<SpawnObstacleEvent> spawnObstacleEventBinding;

    private ObstacleResource redObstacleResource;
    private ObstacleResource yellowObstacleResource;
    private ObstacleResource whiteObstacleResource;
    private Pool<Obstacle> obstaclePool = new();

    public override void _Ready()
    {
        obstacle = ResourceLoader.Load("res://Entities/Obstacles/Obstacle.tscn") as PackedScene;
        redObstacleResource = ResourceLoader.Load("res://Entities/Obstacles/Resources/RedObstacleResource.tres") as ObstacleResource;
        yellowObstacleResource = ResourceLoader.Load("res://Entities/Obstacles/Resources/YellowObstacleResource.tres") as ObstacleResource;
        whiteObstacleResource = ResourceLoader.Load("res://Entities/Obstacles/Resources/WhiteObstacleResource.tres") as ObstacleResource;
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
        var resource = RandomGenerator<ObstacleResource>.PickRandom([redObstacleResource, yellowObstacleResource, whiteObstacleResource]);
        obstacle.SetObstacleResource(resource);
        AddChild(obstacle);
        return obstacle;
    }
}
