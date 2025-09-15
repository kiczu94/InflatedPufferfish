using Godot;
using InflatedPufferfish.Scripts.StateMachine;

namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States;

internal class Inflating : State
{
    readonly PlayerContext context;

    public Inflating(StateMachine stateMachine, State parent, PlayerContext playerContext) : base(stateMachine, parent)
    {
        context = playerContext;
    }

    protected override State GetInitialState() => null;
    protected override State GetTransition() => context.MovingUp ? null : ((PlayerRoot)Parent).Deflated;

    protected override void OnEnter()
    {
        GD.Print("On enter Inflating");
    }
}