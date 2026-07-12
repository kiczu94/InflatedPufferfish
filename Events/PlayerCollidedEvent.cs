using TkoUtilities.EventBus;

namespace InflatedPufferfish.Events;

internal record PlayerCollidedEvent(string Reason) : IEvent;
