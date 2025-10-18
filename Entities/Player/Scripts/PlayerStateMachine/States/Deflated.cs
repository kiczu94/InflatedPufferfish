using Godot;
using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States;

internal class Deflated : State
{
    private readonly PlayerContext playerContext;

    public Deflated(StateMachine stateMachine, State parent, PlayerContext playerContext) : base(stateMachine, parent)
    {
        this.playerContext = playerContext;
        this.Name = "Deflated";
    }

    protected override State GetTransition() => Parent.Name switch
    {
        "Idle" => playerContext.KeyToInflateIsPressed ? ((Idle)Parent).inflating : null,
        _ => playerContext.KeyToInflateIsPressed ? ((Blocking)Parent).inflating : null,
    };



    protected override void OnEnter()
    {
        playerContext.Player.Velocity = new Vector2(0, playerContext.MaximumSpeedDeflating);
        GD.Print("OnEnter Deflated");
    }
}
