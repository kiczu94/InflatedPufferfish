using TkoUtilities.EventBus;

namespace InflatedPufferfish.Events;

internal record EnemyBlocked(ulong id): IEvent;
