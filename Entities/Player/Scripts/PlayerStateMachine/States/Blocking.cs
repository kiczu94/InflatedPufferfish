using inflatedpufferfish.Entities.Player.Scripts.PlayerStateMachine.States;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;
using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States;

internal class Blocking : State
{
    public readonly Deflated deflated;
    public readonly Deflating deflating;
    public readonly Inflated inflated;
    public readonly Inflating inflating;

    private bool animiationFinished = false;
    private readonly PlayerContext playerContext;

    private EventBinding<AnimationFinished> animationFinishedEventBinding;

    public Blocking(StateMachine stateMachine, State parent, PlayerContext playerContext) : base(stateMachine, parent)
    {
        animationFinishedEventBinding = new EventBinding<AnimationFinished>(OnAnimationFinished);
        EventBus<AnimationFinished>.Register(animationFinishedEventBinding);
        this.playerContext = playerContext;
        this.Name = "Blocking";
        deflated = new(stateMachine, this, playerContext);
        deflating = new(stateMachine, this, playerContext);
        inflated = new(stateMachine, this, playerContext);
        inflating = new(stateMachine, this, playerContext);
    }

    protected override State GetInitialState() => playerContext.Player.Velocity.Y switch
    {
        float x when x >= playerContext.MaximumSpeedDeflating => deflated,
        float x when x <= playerContext.MaximumSpeedInflating => inflated,
        _ => playerContext.KeyToInflateIsPressed ? inflating : deflating,
    };

    protected override State GetTransition() => animiationFinished ? ((PlayerRoot)Parent).Idle : null;

    protected override void OnEnter()
    {
        playerContext.Player.animationPlayer.Play("blocking");
        playerContext.IsBlocking = true;
        animiationFinished = false;
    }

    protected override void OnExit()
    {
        playerContext.IsBlocking = false;
        animiationFinished = false;
    }

    protected override void OnUpdate(double deltaTime)
    {
        var overlappingAreas = playerContext.Player.blockingArea.GetOverlappingAreas().ToList();
        var enemyArea = overlappingAreas.SingleOrDefault(x => x.GetGroups().Contains("EnemyArea"));
        if (enemyArea == null || enemyArea.GlobalPosition.X <= playerContext.Player.GlobalPosition.X)
        {
            return;
        }
        EventBus<EnemyBlocked>.Raise(new EnemyBlocked(enemyArea.GetParent<Enemy>().GetInstanceId()));
    }

    private void OnAnimationFinished()
    {
        animiationFinished = true;
    }
}
