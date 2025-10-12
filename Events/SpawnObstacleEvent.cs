using TkoUtilities.EventBus;

namespace InflatedPufferfish.Events;

internal record SpawnObstacleEvent(int upObstaclePosition, int downObstaclePosition) : IEvent;
