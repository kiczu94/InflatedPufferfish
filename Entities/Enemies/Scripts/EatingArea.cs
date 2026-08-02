using Godot;
using inflatedpufferfish.Events;
using TkoUtilities.EventBus;

public partial class EatingArea : Area2D
{
    public ulong enemyId;

    public override void _Ready()
    {
        enemyId = GetNode<Node2D>("../../../Enemy").GetInstanceId();
        BodyEntered += OnBodyEntered;
        base._Ready();
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body.GetGroups().Contains("Player"))
        {
            EventBus<PlayerEnteredEatingArea>.Raise(new PlayerEnteredEatingArea(enemyId));
        }
    }
}
