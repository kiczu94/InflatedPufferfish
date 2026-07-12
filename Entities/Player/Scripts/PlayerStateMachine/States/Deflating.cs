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
        this.Name = "Deflating";
    }

    protected override State GetTransition()
    {
        if (playerContext.KeyToFastDeflateJustPressed)
        {
            return Parent.Name switch
            {
                "Idle" => ((Idle)Parent).deflated,
                _ => ((Blocking)Parent).deflated,
            };
        }

        if (playerContext.KeyToInflateIsPressed)
        {
            return Parent.Name switch
            {
                "Idle" => ((Idle)Parent).inflating,
                _ => ((Blocking)Parent).inflating,
            };
        }

        if (playerContext.Player.Velocity.Y == playerContext.MaximumSpeedDeflating)
        {
            return Parent.Name switch
            {
                "Idle" => ((Idle)Parent).deflated,
                _ => ((Blocking)Parent).deflated,
            };
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
        SetAnimationFrame(playerContext.Player.Velocity);
    }

    private void SetAnimationFrame(Vector2 velocity)
    {
        if (velocity.Y < playerContext.MaximumSpeedInflating && velocity.Y > 59)
        {
            playerContext.Player.animatedSprite.Frame = 6;
            playerContext.Player.playerCollisionShape.Scale = new Vector2(1.0f, 0.4f);
            return;
        }

        if (velocity.Y < 60 && velocity.Y > 29)
        {
            playerContext.Player.animatedSprite.Frame = 5;
            playerContext.Player.playerCollisionShape.Scale = new Vector2(1.0f, 0.5f);
            return;
        }

        if (velocity.Y < 30 && velocity.Y > 0)
        {
            playerContext.Player.animatedSprite.Frame = 4;
            playerContext.Player.playerCollisionShape.Scale = new Vector2(1.0f, 0.6f);
            return;
        }

        if (velocity.Y < 1 && velocity.Y > -30)
        {
            playerContext.Player.animatedSprite.Frame = 3;
            playerContext.Player.playerCollisionShape.Scale = new Vector2(1.0f, 0.7f);
            return;

        }

        if (velocity.Y < -29 && velocity.Y > -60)
        {
            playerContext.Player.animatedSprite.Frame = 2;
            playerContext.Player.playerCollisionShape.Scale = new Vector2(1.0f, 0.8f);
            return;
        }

        if (velocity.Y < -59 && velocity.Y > -90)
        {
            playerContext.Player.animatedSprite.Frame = 1;
            playerContext.Player.playerCollisionShape.Scale = new Vector2(1.0f, 0.9f);
            return;
        }
    }
}
