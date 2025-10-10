using Godot;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class ObstacleSpawner : Node
{
    bool TimeToSpawn = true;
    EventBinding<ObstacleOutOfFieldView> ObstacleOutOfFieldViewEventBinding;
    HashSet<Obstacle> visibleObstacles = new HashSet<Obstacle>();
    HashSet<Obstacle> notVisibleObstacles = new HashSet<Obstacle>();
    PackedScene Obstacle;
    Random Random = new Random();


    public override void _Ready()
    {
        ObstacleOutOfFieldViewEventBinding = new EventBinding<ObstacleOutOfFieldView>(OnObstacleOutOfFieldViewEvent);
        EventBus<ObstacleOutOfFieldView>.Register(ObstacleOutOfFieldViewEventBinding);
        Obstacle = ResourceLoader.Load("res://Entities/Obstacle/Obstacle.tscn") as PackedScene;
        base._Ready();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (TimeToSpawn)
        {
            SpawnObstacles();
            _ = Wait(2000);
            TimeToSpawn = false;
        }
    }

    private void SpawnObstacles()
    {
        var distanceBetweenPipes = Random.Next(150, 181);
        var upObstacleYPosition = Random.Next(-30, 21);
        var downObstacleYPosition = upObstacleYPosition + distanceBetweenPipes;

        if (notVisibleObstacles.Count > 1) // at least two
        {
            SpawnObstacleFromNotVisible(upObstacleYPosition);
            SpawnObstacleFromNotVisible(downObstacleYPosition);
            return;
        }
        SpawnNewObstacle(upObstacleYPosition);
        SpawnNewObstacle(downObstacleYPosition);
    }

    private void SpawnNewObstacle(int obstacleYPosition)
    {
        var obstacle = Obstacle.Instantiate() as Obstacle;
        obstacle.SetPosition(new Vector2(320, obstacleYPosition));
        AddChild(obstacle);
        visibleObstacles.Add(obstacle);
    }

    private void SpawnObstacleFromNotVisible(int obstacleYPosition)
    {
        var obstacle = notVisibleObstacles.First();
        obstacle.SetPosition(new Vector2(320, obstacleYPosition));
        notVisibleObstacles.Remove(obstacle);
        obstacle.SetVisible(true);
        visibleObstacles.Add(obstacle);
    }

    private async Task Wait(int timeInMiliseconds)
    {
        await Task.Delay(timeInMiliseconds);
        TimeToSpawn = !TimeToSpawn;
    }

    private void OnObstacleOutOfFieldViewEvent(ObstacleOutOfFieldView @event)
    {
        var obstacle = visibleObstacles.SingleOrDefault(x => x.GetInstanceId() == @event.Id);
        if (obstacle == null) 
        {
            return; 
        }
        obstacle.SetVisible(false);
        visibleObstacles.Remove(obstacle);
        notVisibleObstacles.Add(obstacle);
    }
}
