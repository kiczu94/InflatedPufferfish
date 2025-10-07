using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States
{
    internal class Idle : State
    {
        public Idle(StateMachine stateMachine, State parent, PlayerContext playerContext) : base(stateMachine, parent)
        {
        }
    }
}
