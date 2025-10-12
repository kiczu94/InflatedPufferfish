using Godot;
using InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine;
using InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States;
using TkoUtilities.Hsm;

namespace inflatedpufferfish.Entities.Player.Scripts.PlayerStateMachine.States;

internal class Deflating : State
{
    private readonly PlayerContext playerContext;
    
    public Deflating(StateMachine stateMachine, State parent, PlayerContext playerContext) : base(stateMachine, parent)
    {
        this.playerContext = playerContext;
    }

    protected override State GetTransition()
    {
        if (playerContext.KeyToFastDeflateJustPressed)
        {
            return ((Idle)Parent).deflated;
        }

        if (playerContext.KeyToInflateIsPressed)
        {
            return ((Idle)Parent).inflating;
        }

        if(playerContext.Player.Velocity.Y == playerContext.MaximumSpeedDeflating)
        {
            return ((Idle)Parent).deflated;
        }

        return null;
    }

    protected override void OnEnter()
    {
        GD.Print("OnEnter Deflating");
    }

    protected override void OnUpdate(double deltaTime)
    {
        if (playerContext.Player.Velocity.Y != playerContext.MaximumSpeedDeflating)
        {
            var currentVelocity = playerContext.Player.Velocity;
            playerContext.Player.Velocity = currentVelocity + new Vector2(0, 1);
        }
    }
}
