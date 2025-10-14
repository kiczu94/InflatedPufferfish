using Godot;
using InflatedPufferfish.Events;
using System;
using TkoUtilities.EventBus;

public partial class EnemyArea2d : Area2D
{
    
    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
        base._Ready();
    }

    public void OnBodyEntered(Node2D body)
    {
        if (body.GetGroups().Contains("Player"))
        {
            //EventBus<PlayerCollidedEvent>.Raise(new PlayerCollidedEvent());
        }
    }
}
