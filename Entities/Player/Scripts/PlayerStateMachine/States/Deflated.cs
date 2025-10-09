using Godot;
using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States;

internal class Deflated : State
{
    readonly PlayerContext playerContext;

    public Deflated(StateMachine stateMachine, State parent, PlayerContext playerContext) : base(stateMachine, parent)
    {
        this.playerContext = playerContext;
    }

    protected override State GetTransition() => playerContext.KeyToInflateIsPressed ? ((Idle)Parent).inflating : null;


    protected override void OnEnter()
    {
        playerContext.player.Velocity = new Vector2(0, playerContext.MaximumSpeedDeflating);
        GD.Print("OnEnter Deflated");
    }
}
