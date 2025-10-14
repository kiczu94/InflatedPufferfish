using Godot;
using InflatedPufferfish.Constants;
using InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;
using TkoUtilities.Hsm;

namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine;

internal partial class PlayerStateDriver : Node
{
    [Export]
    public CharacterBody2D player;

    private string lastPath;
    private EventBinding<PlayerCollidedEvent> fishObstacleCollidedEventBinding;
    private PlayerContext playerContext = new PlayerContext();
    private State root;
    private StateMachine stateMachine;


    public override void _Ready()
    {
        fishObstacleCollidedEventBinding = new EventBinding<PlayerCollidedEvent>(OnFishObstacleCollidedEvent);
        EventBus<PlayerCollidedEvent>.Register(fishObstacleCollidedEventBinding);
        root = new PlayerRoot(null, playerContext);
        stateMachine = new StateMachineBuilder(root).Build();
        playerContext.Player = player;
        base._Ready();
    }

    public override void _Process(double deltaTime)
    {
        ResetContextButtonsData();
        ProcessUserControls();
        stateMachine.Tick(deltaTime);
        UpdateLastPath();
        base._Process(deltaTime);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }

    private static string StatePath(State s) => string.Join(" > ", s.PathToRoot().Reverse().Select(x => x.GetType().Name));

    private void ResetContextButtonsData()
    {
        playerContext.KeyToFastDeflateJustPressed = false;
        playerContext.KeyToInflateIsPressed = false;
        playerContext.KeyToBlockJustPressed = false;
    }

    private void UpdateLastPath()
    {
        var path = StatePath(stateMachine.Root.Leaf());
        if (path != lastPath)
        {
            GD.Print($"State {path}");
            lastPath = path;
        }
    }

    private void ProcessUserControls()
    {
        if (Input.IsActionPressed(Actions.Inflating))
        {
            playerContext.KeyToInflateIsPressed = true;
        }

        if (Input.IsActionJustPressed(Actions.FastDeflate))
        {
            playerContext.KeyToFastDeflateJustPressed = true;
        }

        if (Input.IsActionJustPressed(Actions.Block))
        {
            playerContext.KeyToBlockJustPressed = true;
        }
    }

    private void OnFishObstacleCollidedEvent()
    {
        playerContext.PlayerLost = true;
    }
}
