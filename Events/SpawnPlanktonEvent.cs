using TkoUtilities.EventBus;

namespace InflatedPufferfish.Events;

public record SpawnPlanktonEvent(int positionY) : IEvent;
