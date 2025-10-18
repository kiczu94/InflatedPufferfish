using Godot;
using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States;

public class Lost : State
{
    private PlayerContext playerContext;
    
    public Lost(StateMachine stateMachine, State parent, PlayerContext playerContext) : base(stateMachine, parent)
    {
        this.playerContext = playerContext;
        this.Name = "Lost";
    }

    protected override void OnEnter()
    {
        playerContext.Player.Velocity = new Vector2(0, playerContext.MaximumSpeedDeflating);
    }
}
