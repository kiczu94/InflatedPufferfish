using Godot;
using InflatedPufferfish.Entities.Enemies.Scripts.EnemyStateMachine;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class Enemy : Sprite2D
{
    EnemyStateDriver enemyStateDriver;

    public override void _Ready()
    {
        enemyStateDriver = GetNode<EnemyStateDriver>("EnemyStateDriver");
        base._Ready();
    }

    public override void _Process(double delta)
    {
        NotifyIfOutOfView();
        base._Process(delta);
    }

    public void SetIsDead(bool isDead = false)
    {
        enemyStateDriver.SetIsDead(isDead);
    }

    private void NotifyIfOutOfView()
    {
        if (Position.X < -30)
        {
            EventBus<EnemyOutOfFieldView>.Raise(new EnemyOutOfFieldView(this.GetInstanceId()));
        }

        if (Position.Y > 200)
        {
            EventBus<EnemyOutOfFieldView>.Raise(new EnemyOutOfFieldView(this.GetInstanceId()));
        }
    }

}
