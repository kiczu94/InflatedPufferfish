using Godot;
using InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine;
using InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States;
using TkoUtilities.Hsm;

namespace inflatedpufferfish.Entities.Player.Scripts.PlayerStateMachine.States;

public class Deflating : State
{
    readonly PlayerContext PlayerContext;
    
    public Deflating(StateMachine stateMachine, State parent, PlayerContext playerContext) : base(stateMachine, parent)
    {
        PlayerContext = playerContext;
    }

    protected override State GetTransition()
    {
        if (PlayerContext.KeyToFastDeflateJustPressed)
        {
            return ((Idle)Parent).deflated;
        }

        if (PlayerContext.KeyToInflateIsPressed)
        {
            return ((Idle)Parent).inflating;
        }

        if(PlayerContext.player.Velocity.Y == PlayerContext.MaximumSpeedDeflating)
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
        if (PlayerContext.player.Velocity.Y != PlayerContext.MaximumSpeedDeflating)
        {
            var currentVelocity = PlayerContext.player.Velocity;
            PlayerContext.player.Velocity = currentVelocity + new Vector2(0, 1);
        }
    }
}
