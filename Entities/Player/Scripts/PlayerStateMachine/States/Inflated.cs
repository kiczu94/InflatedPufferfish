using Godot;
using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States
{
    internal class Inflated : State
    {
        readonly PlayerContext PlayerContext;

        public Inflated(StateMachine stateMachine, State parent, PlayerContext playerContext) : base(stateMachine, parent)
        {
            PlayerContext = playerContext;
        }

        protected override State GetTransition()
        {
            if(PlayerContext.KeyToFastDeflateJustPressed)
            {
                return ((Idle)Parent).deflated;
            }

            if (!PlayerContext.KeyToInflateIsPressed)
            {
                return ((Idle)Parent).deflating;
            }

            return null;
        }

        protected override void OnEnter()
        {
            GD.Print("OnEnter Inflated");
        }
    }
}
