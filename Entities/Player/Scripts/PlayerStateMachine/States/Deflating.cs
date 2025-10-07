using TkoUtilities.Hsm;

namespace inflatedpufferfish.Entities.Player.Scripts.PlayerStateMachine.States;

public class Deflating : State
{
    public Deflating(StateMachine stateMachine, State parent = null) : base(stateMachine, parent)
    {
    }
}
