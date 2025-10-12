using TkoUtilities.EventBus;

namespace InflatedPufferfish.Events;

internal record PlanktonOutOfFieldView(ulong Id) : IEvent;
