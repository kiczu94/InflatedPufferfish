using Godot;
using InflatedPufferfish.Events;
using InflatedPufferfish.TkoUtilities.Utilities;
using TkoUtilities.EventBus;

public partial class SpawnCoordinator : Node
{
    private bool spawnObstacle = true;
    private bool spawnPlankton = true;
    private bool spawnEnemy = false;
    private Random random = new Random();

    private CancellationTokenSource _spawnCts = new CancellationTokenSource();

    private EventBinding<GameLost> gameLostEventBinding;

    public override void _Ready()
    {
        gameLostEventBinding = new EventBinding<GameLost>(OnGameLost);
        EventBus<GameLost>.Register(gameLostEventBinding);
        _ = Wait.For(6000, () => { spawnEnemy = true; });
        base._Ready();
    }

    public override void _Process(double delta)
    {
        if (spawnObstacle)
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
            _ = Wait.For(10000, () => { spawnPlankton = true; }, _spawnCts.Token);
            spawnPlankton = false;
            EventBus<SpawnPlanktonEvent>.Raise(new SpawnPlanktonEvent(planktonPosition));
        }
    }

    private void SpawnObstacle(int upObstacleYPosition, int downObstacleYPosition)
    {
        if (spawnObstacle)
        {
            _ = Wait.For(2000, () => { spawnObstacle = true; }, _spawnCts.Token);
            spawnObstacle = false;
            EventBus<SpawnObstacleEvent>.Raise(new SpawnObstacleEvent(upObstacleYPosition, downObstacleYPosition));
        }
    }

    private void SpawnEnemy(int positionY)
    {
        if (spawnEnemy)
        {
            _ = Wait.For(8000,
                () =>
                {
                    spawnEnemy = true;
                    EventBus<SpawnEnemyEvent>.Raise(new SpawnEnemyEvent(positionY));
                },
                _spawnCts.Token);
            spawnEnemy = false;
        }
    }

    private (int upObstacleY, int downObstacleY, int gapCenter) GetCoordinates()
    {
        var distanceBetweenPipes = random.Next(160, 200);
        var upObstacleYPosition = random.Next(-30, 21);
        var downObstacleYPosition = upObstacleYPosition + distanceBetweenPipes;
        var gapCenter = upObstacleYPosition + distanceBetweenPipes / 2;

        return (upObstacleYPosition, downObstacleYPosition, gapCenter);
    }

    private void OnGameLost()
    {
        _spawnCts.Cancel();
    }
}
