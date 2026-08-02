using Godot;
using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Enemies.Scripts.EnemyStateMachine.States;

internal class Dead : State
{
    EnemyContext enemyContext;

    public Dead(StateMachine stateMachine, State parent, EnemyContext enemyContext) : base(stateMachine, parent)
    {
        this.enemyContext = enemyContext;
    }

    protected override State GetInitialState() => null;

    protected override State GetTransition() => enemyContext.isDead ? null : ((EnemyRoot)Parent).Swimming;

    protected override void OnUpdate(double deltaTime)
    {
        enemyContext.enemy.Position += new Vector2(30, 30) * (float)deltaTime;
    }

    protected override void OnEnter()
    {
        GD.Print("Entered Dead");
    }
}
