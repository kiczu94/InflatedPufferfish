using inflatedpufferfish.Entities.Player.Scripts.PlayerStateMachine.States;
using InflatedPufferfish.Events;
using InflatedPufferfish.TkoUtilities.Utilities;
using TkoUtilities.EventBus;
using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States;

internal class Blocking : State
{
    public readonly Deflated deflated;
    public readonly Deflating deflating;
    public readonly Inflated inflated;
    public readonly Inflating inflating;
 
    private bool animiationFinished = false;
    private readonly PlayerContext playerContext;

    public Blocking(StateMachine stateMachine, State parent, PlayerContext playerContext) : base(stateMachine, parent)
    {
        this.playerContext = playerContext;
        this.Name = "Blocking";
        deflated = new(stateMachine, this, playerContext);
        deflating = new(stateMachine, this, playerContext);
        inflated = new(stateMachine, this, playerContext);
        inflating = new(stateMachine, this, playerContext);
    }

    protected override State GetInitialState() 
    {
        switch (playerContext.Player.Velocity.Y)
        {
            case float x when x >= playerContext.MaximumSpeedDeflating:
                return deflated;
            case float x when x <= playerContext.MaximumSpeedInflating:
                return inflated;
            default:
                if (playerContext.KeyToInflateIsPressed)
                {
                    return inflating;
                }
                return deflating;
        }
    }

    protected override State GetTransition() => animiationFinished ? ((PlayerRoot)Parent).Idle : null;

    protected override void OnEnter()
    {
        _ =  Wait.For(4000, () => { animiationFinished = true; });
    }

    protected override void OnExit()
    {
        animiationFinished = false;
    }

    protected override void OnUpdate(double deltaTime)
    {
        var overlappingAreas = playerContext.Player.blockingArea.GetOverlappingAreas().ToList();
        var enemyArea = overlappingAreas.SingleOrDefault(x => x.GetGroups().Contains("EnemyArea"));
        if (enemyArea != null )
        {
            if (enemyArea.GlobalPosition.X> playerContext.Player.GlobalPosition.X)
            {
                EventBus<EnemyBlocked>.Raise(new EnemyBlocked(enemyArea.GetParent<Enemy>().GetInstanceId()));
            }
        }
    }
}
