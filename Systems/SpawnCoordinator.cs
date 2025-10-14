using Godot;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class SpawnCoordinator : Node
{
    private bool gameRunning = true;
    private bool spawnObstacle = true; 
    private bool spawnPlankton = true;
    private bool spawnEnemy = false;
    private EventBinding<PlayerCollidedEvent> fishObstacleCollidedEventBinding;
    private Random random = new Random();

    public override void _Ready()
    {
        fishObstacleCollidedEventBinding = new EventBinding<PlayerCollidedEvent>(OnFishObstacleCollidedEvent);
        EventBus<PlayerCollidedEvent>.Register(fishObstacleCollidedEventBinding);
        _ = Wait(16000, () => spawnEnemy = true);
        base._Ready();
    }

    public override void _Process(double delta)
    {
        if (gameRunning && spawnObstacle)
        {
            var (upObstacleYPosition, downObstacleYPosition, gapCenter) = GetCoordinates();
            SpawnObstacle(upObstacleYPosition, downObstacleYPosition);
            SpawnPlankton(gapCenter);
            SpawnEnemy(gapCenter);
        }
        base._Process(delta);
    }

    private void SpawnPlankton(int planktonPosition)
    {
        if (spawnPlankton)
        {
            _ = Wait(10000, () => { spawnPlankton = true; });
            spawnPlankton = false;
            EventBus<SpawnPlanktonEvent>.Raise(new SpawnPlanktonEvent(planktonPosition));
        }
    }

    private void SpawnObstacle(int upObstacleYPosition, int downObstacleYPosition)
    {
        if (spawnObstacle)
        {
            _ = Wait(2000, () => { spawnObstacle = true; });
            spawnObstacle = false;
            EventBus<SpawnObstacleEvent>.Raise(new SpawnObstacleEvent(upObstacleYPosition, downObstacleYPosition));
        }
    }

    private void SpawnEnemy(int positionY)
    {
        if (spawnEnemy)
        {
            _ = Wait(16000,
                () =>
                {
                    spawnEnemy = true;
                    EventBus<SpawnEnemyEvent>.Raise(new SpawnEnemyEvent(positionY));
                });
            spawnEnemy = false;
        }
    }

    private async Task Wait(int miliseconds, Action action)
    {
        await Task.Delay(miliseconds);
        action.Invoke();
    }

    private (int upObstacleY, int downObstacleY, int gapCenter) GetCoordinates()
    {
        var distanceBetweenPipes = random.Next(150, 181);
        var upObstacleYPosition = random.Next(-30, 21);
        var downObstacleYPosition = upObstacleYPosition + distanceBetweenPipes;
        var gapCenter = upObstacleYPosition + distanceBetweenPipes / 2;

        return (upObstacleYPosition, downObstacleYPosition, gapCenter);
    }

    private void OnFishObstacleCollidedEvent()
    {
        gameRunning = false;
    }
}
