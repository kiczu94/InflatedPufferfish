using TkoUtilities.EventBus;

namespace InflatedPufferfish.Events;

public record ObstacleOutOfFieldView(ulong Id) : IEvent;
