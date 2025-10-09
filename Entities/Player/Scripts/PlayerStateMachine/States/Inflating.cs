using Godot;
using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States;

internal class Inflating : State
{
    readonly PlayerContext playerContext;

    public Inflating(StateMachine stateMachine, State parent, PlayerContext playerContext) : base(stateMachine, parent)
    {
        this.playerContext = playerContext;
        this.Name = "Inflating";
    }

    protected override State GetInitialState() => null;
    protected override State GetTransition()
    {
        if (playerContext.player.Velocity.Y == playerContext.MaximumSpeedInflating)
        {
            return ((Idle)Parent).inflated;
        }

        if (playerContext.KeyToFastDeflateJustPressed)
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
        GD.Print("OnEnter Inflating");
    }

    protected override void OnUpdate(double deltaTime)
    {
        if (playerContext.player.Velocity.Y != playerContext.MaximumSpeedInflating)
        {
            var currentVelocity = playerContext.player.Velocity;
            playerContext.player.Velocity = currentVelocity + new Vector2(0, -1);
        }
    }
}