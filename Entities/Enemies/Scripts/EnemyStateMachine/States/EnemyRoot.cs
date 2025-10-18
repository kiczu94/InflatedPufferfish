using Godot;
using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Enemies.Scripts.EnemyStateMachine.States;

internal class EnemyRoot : State
{
    public Alive alive;
    public Dead dead;
    private EnemyContext enemyContext;

    public EnemyRoot(StateMachine stateMachine, EnemyContext enemyContext) : base(stateMachine, null)
    {
        this.enemyContext = enemyContext;
        alive = new Alive(stateMachine, this, enemyContext);
        dead = new Dead(stateMachine, this, enemyContext);
    }

    protected override State GetInitialState() => alive;

    protected override State GetTransition() => null;

    protected override void OnEnter()
    {
        GD.Print("Entered EnemyRoot");
    }
}
