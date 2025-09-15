using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Pong_1.Scripts.EventBus;

[GlobalClass]
public partial class EventBusLoader : Node
{
    public static IReadOnlyList<Type> EventTypes { get; set; }

    public static IReadOnlyList<Type> EventBuses { get; set; }

    public override void _Ready()
    {
        EventTypes = GetEventTypes(typeof(IEvent));
        EventBuses = InitilizeEventBuses();
    }

    private List<Type> GetEventTypes(Type interfaceType) =>
        AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(y =>
                y.GetTypes()
                .Where(x => x != interfaceType && interfaceType.IsAssignableFrom(x)))
            .ToList();


    private List<Type> InitilizeEventBuses()
    {
        var createdEventBuses = new List<Type>();
        var typeDef = typeof(EventBus<>);
        foreach (var type in EventTypes)
        {
            var busType = typeDef.MakeGenericType(type);
            createdEventBuses.Add(busType);
            GD.Print($"Initialized EventBus<{type.Name}>");
        }

        return createdEventBuses;
    }
}