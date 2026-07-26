using Godot;
using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Enemies.Scripts.EnemyStateMachine.States;

internal class Alive : State
{
    EnemyContext enemyContext;

    public Alive(StateMachine stateMachine, State parent, EnemyContext enemyContext) : base(stateMachine, parent)
    {
        this.enemyContext = enemyContext;
    }

    protected override State GetInitialState() => null;

    protected override State GetTransition() => enemyContext.isDead ? ((EnemyRoot)Parent).dead : null;


    protected override void OnUpdate(double deltaTime)
    {
        enemyContext.enemy.Position += enemyContext.enemySpeed * (float)deltaTime;
        var animationFrame = enemyContext.enemy.animatedSprite2D.Frame;
        if (enemyContext.changeToEatingAnimation)
        {
            enemyContext.enemy.animatedSprite2D.Animation = "Eating";
            enemyContext.enemy.animatedSprite2D.Frame = animationFrame;
        }
    }

    protected override void OnEnter()
    {
        enemyContext.enemy.animatedSprite2D.Animation = "Swimming";
        enemyContext.enemy.animatedSprite2D.Play();
    }
}
