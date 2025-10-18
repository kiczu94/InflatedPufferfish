using Godot;
using InflatedPufferfish.Entities.Enemies.Scripts.EnemyStateMachine.States;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;
using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Enemies.Scripts.EnemyStateMachine;

public partial class EnemyStateDriver : Node
{
    private bool isDead;
    private Enemy enemy;
    private EnemyContext enemyContext;
    private State root;
    private StateMachine stateMachine;
    private EventBinding<EnemyBlocked> enemyBlockedEventBinding;

    public override void _Ready()
    {
        enemy = GetParent<Enemy>();
        enemyContext = new EnemyContext();
        root = new EnemyRoot(null, enemyContext);
        stateMachine = new StateMachineBuilder(root).Build();
        enemyBlockedEventBinding = new EventBinding<EnemyBlocked>(OnEnemyBlocked);
        EventBus<EnemyBlocked>.Register(enemyBlockedEventBinding);
        base._Ready();
    }

    public override void _Process(double delta)
    {
        enemyContext.isDead = isDead;
        enemyContext.enemy = enemy;
        stateMachine.Tick(delta);
        base._Process(delta);
    }
    public void SetIsDead(bool isDead)
    {
        this.isDead = isDead;
    }

    private void OnEnemyBlocked(EnemyBlocked @event)
    {
        if (@event.id == enemy.GetInstanceId())
        {
            isDead = true;
        }
    }

}
