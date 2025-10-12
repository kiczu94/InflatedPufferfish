using TkoUtilities.EventBus;

namespace InflatedPufferfish.Events;

internal record ObstacleOutOfFieldView(ulong Id) : IEvent;
