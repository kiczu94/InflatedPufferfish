using TkoUtilities.EventBus;

namespace InflatedPufferfish.Events;

public record SpawnObstacleEvent(int upObstaclePosition, int downObstaclePosition) : IEvent;
