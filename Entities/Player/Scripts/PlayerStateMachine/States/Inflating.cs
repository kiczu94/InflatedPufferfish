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
        if (playerContext.Player.Velocity.Y == playerContext.MaximumSpeedInflating)
        {
            return Parent.Name switch
            {
                "Idle" => ((Idle)Parent).inflated,
                _ => ((Blocking)Parent).inflated,
            };
        }

        if (playerContext.KeyToFastDeflateJustPressed)
        {
            return Parent.Name switch
            {
                "Idle" => ((Idle)Parent).deflated,
                _ => ((Blocking)Parent).deflated,
            };
        }

        if (!playerContext.KeyToInflateIsPressed)
        {
            return Parent.Name switch
            {
                "Idle" => ((Idle)Parent).deflating,
                _ => ((Blocking)Parent).deflating,
            };
        }

        return null;
    }

    protected override void OnEnter()
    {
        GD.Print("OnEnter Inflating");
    }

    protected override void OnUpdate(double deltaTime)
    {
        if (playerContext.Player.Velocity.Y != playerContext.MaximumSpeedInflating)
        {
            var currentVelocity = playerContext.Player.Velocity;
            playerContext.Player.Velocity = currentVelocity + new Vector2(0, -1);
        }
    }
}