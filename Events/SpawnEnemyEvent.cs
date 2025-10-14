using TkoUtilities.EventBus;

namespace InflatedPufferfish.Events;

public record SpawnEnemyEvent(int positionY) : IEvent;
