using Godot;
using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States;

internal class Inflated : State
{
    readonly PlayerContext playerContext;

    public Inflated(StateMachine stateMachine, State parent, PlayerContext playerContext) : base(stateMachine, parent)
    {
        this.playerContext = playerContext;
        this.Name = "Inflated";
    }

    protected override State GetTransition()
    {
        if(playerContext.KeyToFastDeflateJustPressed)
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
        playerContext.Player.animatedSprite.Frame = 0;
        playerContext.Player.playerCollisionShape.Scale = Vector2.One;
        GD.Print("OnEnter Inflated");
    }
}
