using Godot;
using inflatedpufferfish.Events;
using InflatedPufferfish.Entities.Enemies.Scripts.EnemyStateMachine.States;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;
using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Enemies.Scripts.EnemyStateMachine;

public partial class EnemyStateDriver : Node
{
    private bool isDead;
    private bool isEating;
    private Enemy enemy;
    private EnemyContext enemyContext;
    private State root;
    private StateMachine stateMachine;
    private EventBinding<EnemyBlocked> enemyBlockedEventBinding;
    private EventBinding<GameLost> gameLostEventBinding;
    private EventBinding<PlayerEnteredEatingArea> playerEnteredEatingAreaEventBinding;

    public override void _Ready()
    {
        enemy = GetParent<Enemy>();
        enemyContext = new EnemyContext();
        root = new EnemyRoot(null, enemyContext);
        stateMachine = new StateMachineBuilder(root).Build();
        enemyBlockedEventBinding = new EventBinding<EnemyBlocked>(OnEnemyBlocked);
        EventBus<EnemyBlocked>.Register(enemyBlockedEventBinding);
        gameLostEventBinding = new EventBinding<GameLost>(OnGameLost);
        EventBus<GameLost>.Register(gameLostEventBinding);
        playerEnteredEatingAreaEventBinding = new EventBinding<PlayerEnteredEatingArea>(OnPlayerEnteredEatingArea);
        EventBus<PlayerEnteredEatingArea>.Register(playerEnteredEatingAreaEventBinding);
        base._Ready();
    }

    public override void _Process(double delta)
    {
        enemyContext.isDead = isDead;
        enemyContext.enemy = enemy;
        enemyContext.isEating = isEating;
        stateMachine.Tick(delta);
        base._Process(delta);
    }

    public void SetIsDead(bool isDead)
    {
        this.isDead = isDead;
    }

    public void SetIsEating(bool isEating)
    {
        this.isEating = isEating;
    }

    private void OnEnemyBlocked(EnemyBlocked @event)
    {
        if (@event.id == enemy.GetInstanceId())
        {
            isDead = true;
        }
    }

    private void OnGameLost()
    {
        enemyContext.enemySpeed = Vector2.Zero;
    }

    private void OnPlayerEnteredEatingArea(PlayerEnteredEatingArea @event)
    {
        if (@event.InstanceId == enemy.GetInstanceId())
        {
            isEating = true;
        }
    }

}
