using Godot;
using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States;

internal class Deflated : State
{
    readonly PlayerContext playerContext;

    public Deflated(StateMachine stateMachine, State parent, PlayerContext playerContext) : base(stateMachine, parent)
    {
        this.playerContext = playerContext;
        this.Name = nameof(Deflated);
    }

    protected override State GetTransition()
    {
        if (playerContext.KeyToInflateIsPressed)
        {
            return ((PlayerRoot)Parent).Inflating;
        }

        if (playerContext.KeyToBlockJustPressed)
        {
            return ()
        }

        return null;
    }
    protected override void OnEnter()
    {
        var currentVelocity = playerContext.player.Velocity;

        GD.Print("On enter Deflated");
    }
}
