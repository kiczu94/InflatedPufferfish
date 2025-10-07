using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States
{
    internal class Blocking : State
    {
        private readonly PlayerContext _playerContext;

        public Blocking(StateMachine stateMachine, State parent, PlayerContext playerContext) : base(stateMachine, parent)
        {
            _playerContext = playerContext;
        }
    }
}
