using Godot;
using InflatedPufferfish.Scripts.StateMachine;

namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States;

internal class Deflated : State
{
    readonly PlayerContext playerContext;

    public Deflated(StateMachine stateMachine, State parent, PlayerContext playerContext) : base(stateMachine, parent)
    {
        this.playerContext = playerContext;
    }

    protected override State GetTransition() => playerContext.MovingUp ? ((PlayerRoot)Parent).Inflating : null;

    protected override void OnEnter()
    {
        GD.Print("On enter Deflated");
    }
}
