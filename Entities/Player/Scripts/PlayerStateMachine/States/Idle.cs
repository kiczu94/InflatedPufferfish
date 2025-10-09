using Godot;
using inflatedpufferfish.Entities.Player.Scripts.PlayerStateMachine.States;
using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States
{
    internal class Idle : State
    {
        public readonly Deflated deflated;
        public readonly Deflating deflating;
        public readonly Inflated inflated;
        public readonly Inflating inflating;

        readonly PlayerContext playerContext;

        public Idle(StateMachine stateMachine, State parent, PlayerContext playerContext) : base(stateMachine, parent)
        {
            this.playerContext = playerContext;
            deflated = new(stateMachine, this, playerContext);
            deflating = new(stateMachine, this, playerContext);
            inflated = new(stateMachine, this, playerContext);
            inflating = new(stateMachine, this, playerContext);
        }

        protected override State GetInitialState() => deflated;

        protected override State GetTransition() => playerContext.KeyToBlockJustPressed ? ((PlayerRoot)Parent).Blocking : null;


        protected override void OnEnter()
        {
            GD.Print("OnEnter Idle");
        }
    }
}
