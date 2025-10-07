using Godot;
using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States;

internal class Inflating : State
{
/*    public readonly Deflating Deflating;

    public readonly Inflated Inflated;*/
    
    readonly PlayerContext context;

    public Inflating(StateMachine stateMachine, State parent, PlayerContext playerContext) : base(stateMachine, parent)
    {
        context = playerContext;
        this.Name = "Inflating";
    }

    protected override State GetInitialState() => null;
    protected override State GetTransition() => context.KeyToInflateIsPressed ? null : ((PlayerRoot)Parent).Deflated;

    protected override void OnEnter()
    {
        GD.Print("On enter Inflating");
    }
}