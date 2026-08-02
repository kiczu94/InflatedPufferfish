using Godot;
using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Enemies.Scripts.EnemyStateMachine.States;

internal class Swimming : State
{
    EnemyContext enemyContext;

    public Swimming(StateMachine stateMachine, State parent, EnemyContext enemyContext) : base(stateMachine, parent)
    {
        this.enemyContext = enemyContext;
    }

    protected override State GetInitialState() => null;

    protected override State GetTransition()
    {
        if (enemyContext.isDead)
        {
            return ((EnemyRoot)Parent).Dead;
        }

        if (enemyContext.isEating)
        {
            return ((EnemyRoot)Parent).Eating;
        }

        return null;
    }

    protected override void OnUpdate(double deltaTime)
    {
        enemyContext.enemy.Position += enemyContext.enemySpeed * (float)deltaTime;
    }

    protected override void OnEnter()
    {
        enemyContext.enemy.animatedSprite2D.Animation = "Swimming";
        enemyContext.enemy.animatedSprite2D.Frame = enemyContext.animationFrame;
        enemyContext.enemy.animatedSprite2D.Play();
    }

    protected override void OnExit()
    {
        enemyContext.animationFrame = enemyContext.enemy.animatedSprite2D.Frame;
    }

}
