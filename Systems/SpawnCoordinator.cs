using Godot;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class SpawnCoordinator : Node
{
    private bool gameRunning = true;
    private bool spawnObstacle = true; 
    private bool spawnPlankton = true;
    private EventBinding<FishObstacleCollidedEvent> fishObstacleCollidedEventBinding;
    private Random Random = new Random();

    public override void _Ready()
    {
        fishObstacleCollidedEventBinding = new EventBinding<FishObstacleCollidedEvent>(OnFishObstacleCollidedEvent);
        EventBus<FishObstacleCollidedEvent>.Register(fishObstacleCollidedEventBinding);
        base._Ready();
    }

    public override void _Process(double delta)
    {
        if (gameRunning && spawnObstacle)
        {
            var (upObstacleYPosition, downObstacleYPosition, planktonPosition) = GetCoordinates();
            SpawnObstacle(upObstacleYPosition, downObstacleYPosition);
            SpawnPlankton(planktonPosition);
        }
        base._Process(delta);
    }

    private void SpawnPlankton(int planktonPosition)
    {
        if (spawnPlankton)
        {
            _ = Wait(10000, () => { spawnPlankton = !spawnPlankton; });
            spawnPlankton = !spawnPlankton;
            EventBus<SpawnPlanktonEvent>.Raise(new SpawnPlanktonEvent(planktonPosition));
        }
    }

    private void SpawnObstacle(int upObstacleYPosition, int downObstacleYPosition)
    {
        if (spawnObstacle)
        {
            _ = Wait(2000, () => { spawnObstacle = !spawnObstacle; });
            spawnObstacle = !spawnObstacle;
            EventBus<SpawnObstacleEvent>.Raise(new SpawnObstacleEvent(upObstacleYPosition, downObstacleYPosition));
        }
    }

    private async Task Wait(int miliseconds, Action action)
    {
        await Task.Delay(miliseconds);
        action.Invoke();
    }

    private (int upObstacleY, int downObstacleY, int planktonY) GetCoordinates()
    {
        var distanceBetweenPipes = Random.Next(150, 181);
        var upObstacleYPosition = Random.Next(-30, 21);
        var downObstacleYPosition = upObstacleYPosition + distanceBetweenPipes;
        var planktonPosition = upObstacleYPosition + distanceBetweenPipes / 2;

        return (upObstacleYPosition, downObstacleYPosition, planktonPosition);
    }

    private void OnFishObstacleCollidedEvent()
    {
        gameRunning = false;
    }
}
