using Godot;
using inflatedpufferfish.Events;
using System;
using TkoUtilities.EventBus;

public partial class EatingArea : Area2D
{
    private void OnBodyEntered(Node2D body)
    {
        if (body.GetGroups().Contains("Player"))
        {
            EventBus<PlayerEnteredEatingArea>.Raise(new PlayerEnteredEatingArea(GetParent().GetParent().GetInstanceId()));
        }
    }
}
