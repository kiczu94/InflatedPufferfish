using TkoUtilities.EventBus;

namespace InflatedPufferfish.Events;

public record PlanktonOutOfFieldView(ulong Id) : IEvent;
