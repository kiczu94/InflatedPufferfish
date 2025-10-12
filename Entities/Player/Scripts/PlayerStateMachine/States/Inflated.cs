using Godot;
using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States;

internal class Inflated : State
{
    readonly PlayerContext playerContext;

    public Inflated(StateMachine stateMachine, State parent, PlayerContext playerContext) : base(stateMachine, parent)
    {
        this.playerContext = playerContext;
    }

    protected override State GetTransition()
    {
        if(playerContext.KeyToFastDeflateJustPressed)
        {
            return ((Idle)Parent).deflated;
        }

        if (!playerContext.KeyToInflateIsPressed)
        {
            return ((Idle)Parent).deflating;
        }

        return null;
    }

    protected override void OnEnter()
    {
        GD.Print("OnEnter Inflated");
    }
}
