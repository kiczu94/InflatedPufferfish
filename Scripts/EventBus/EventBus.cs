using Godot;
using System.Collections.Generic;

namespace Pong_1.Scripts.EventBus;

public class EventBus<T> where T : IEvent
{
    static readonly HashSet<IEventBinding<T>> eventBindings = new ();

    public static void Register(IEventBinding<T> eventBinding) => eventBindings.Add(eventBinding);

    public static void Unregister(IEventBinding<T> eventBinding) => eventBindings.Remove(eventBinding);

    public static void Raise(T @event)
    {
        foreach (var binding in eventBindings)
        {
            binding.onEvent.Invoke(@event);
            binding.onEventNoArgs.Invoke();
            binding.onEventNoArgsAsync.Invoke();
            binding.onEventAsync.Invoke(@event);
        }
    }

    static void Clear()
    {
        GD.Print($"Clearing {typeof(T).Name} bindings");
        eventBindings.Clear();
    }
}