using Godot;
using inflatedpufferfish.Entities.Enemies.Scripts.EnemyStateMachine.States;
using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Enemies.Scripts.EnemyStateMachine.States;

internal class EnemyRoot : State
{
    public Swimming Swimming;
    
    public Dead Dead;

    public Eating Eating;

    private EnemyContext enemyContext;

    public EnemyRoot(StateMachine stateMachine, EnemyContext enemyContext) : base(stateMachine, null)
    {
        this.enemyContext = enemyContext;
        Swimming = new Swimming(stateMachine, this, enemyContext);
        Dead = new Dead(stateMachine, this, enemyContext);
        Eating = new Eating(stateMachine, this, enemyContext);
    }

    protected override State GetInitialState() => Swimming;

    protected override State GetTransition() => null;

    protected override void OnEnter()
    {
        GD.Print("Entered EnemyRoot");
    }
}
