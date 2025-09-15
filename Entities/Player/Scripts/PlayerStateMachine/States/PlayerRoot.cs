using InflatedPufferfish.Scripts.StateMachine;

namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States;

internal partial class PlayerRoot : State
{
    public readonly Deflated Deflated;
    public readonly Inflating Inflating;

    readonly PlayerContext context;

    public PlayerRoot(StateMachine stateMachine, PlayerContext playerContext) : base(stateMachine, null)
    {
        this.context = playerContext;
        Deflated = new Deflated(stateMachine, this, context);
        Inflating = new Inflating(stateMachine, this, context);
    }

    protected override State GetInitialState() => Deflated;
    protected override State GetTransition() => context.MovingUp ? Inflating : null;
}
