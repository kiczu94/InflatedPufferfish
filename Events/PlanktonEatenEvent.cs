using TkoUtilities.EventBus;

namespace InflatedPufferfish.Events;

public record PlanktonEatenEvent(ulong Id): IEvent;
