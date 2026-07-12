using Godot;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class EnemyArea2d : Area2D
{
    public ulong enemyId;

    public override void _Ready()
    {
        enemyId = GetParent<Enemy>().GetInstanceId();
        BodyEntered += OnBodyEntered;
        base._Ready();
    }

    public void OnBodyEntered(Node2D body)
    {
        if (body.GetGroups().Contains("Player"))
        {
            EventBus<PlayerCollidedEvent>.Raise(new PlayerCollidedEvent("Enemy"));
        }
    }
}
