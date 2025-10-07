using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States;

internal partial class PlayerRoot : State
{
    public readonly Idle Idle;
    public readonly Blocking Blocking;

    readonly PlayerContext context;

    public PlayerRoot(StateMachine stateMachine, PlayerContext playerContext) : base(stateMachine, null)
    {
        this.context = playerContext;
        Idle = new Idle(stateMachine, this, context);
        Blocking = new Blocking(stateMachine, this, context);
    }

    protected override State GetInitialState() => Idle;
    protected override State GetTransition() => context.KeyToBlockJustPressed ? Blocking : null;
}
