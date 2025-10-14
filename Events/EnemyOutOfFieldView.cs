using TkoUtilities.EventBus;

namespace InflatedPufferfish.Events;

public record EnemyOutOfFieldView(ulong Id) : IEvent;
