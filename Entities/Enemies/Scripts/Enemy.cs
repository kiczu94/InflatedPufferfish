using Godot;
using InflatedPufferfish.Entities.Enemies.Scripts.EnemyStateMachine;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;

public partial class Enemy : Node2D
{
    public AnimatedSprite2D animatedSprite2D;
    EnemyStateDriver enemyStateDriver;

    public override void _Ready()
    {
        enemyStateDriver = GetNode<EnemyStateDriver>("EnemyStateDriver");
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
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

    public void SetIsEating(bool isEating = false)
    {
        enemyStateDriver.SetIsEating(isEating);
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
