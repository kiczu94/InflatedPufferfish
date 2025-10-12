using TkoUtilities.EventBus;

namespace InflatedPufferfish.Events;

internal record SpawnPlanktonEvent(int positionY) : IEvent;
