using TkoUtilities.EventBus;

namespace InflatedPufferfish.Events;

internal record PlanktonEatenEvent(ulong Id): IEvent;
