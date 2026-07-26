using TkoUtilities.EventBus;

namespace inflatedpufferfish.Events;

public record PlayerEnteredEatingArea(ulong InstanceId) : IEvent;
