using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States;

internal partial class PlayerRoot : State
{
    public readonly Idle Idle;
    public readonly Blocking Blocking;
    public readonly Lost Lost;

    private readonly PlayerContext playerContext;

    public PlayerRoot(StateMachine stateMachine, PlayerContext playerContext) : base(stateMachine, null)
    {
        this.playerContext = playerContext;
        Idle = new Idle(stateMachine, this, this.playerContext);
        Blocking = new Blocking(stateMachine, this, this.playerContext);
        Lost = new Lost(stateMachine, this, playerContext);
        this.Name = "PlayerRoot";
    }

    protected override State GetInitialState() => Idle;

    protected override State GetTransition()
    {
        if (playerContext.PlayerLost)
        {
            return Lost;
        }

        if (playerContext.KeyToBlockJustPressed)
        {
            return Blocking; 
        }

        return null;
    }
}
