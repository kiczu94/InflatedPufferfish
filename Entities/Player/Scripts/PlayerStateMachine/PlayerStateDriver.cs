using Godot;
using InflatedPufferfish.Constants;
using InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States;
using TkoUtilities.Hsm;
namespace InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine;

internal partial class PlayerStateDriver : Node
{
    [Export]
    public CharacterBody2D player;

    public StateMachine stateMachine;

    public PlayerContext ctx = new PlayerContext();

    private string lastPath;
    private State root;

    public override void _Ready()
    {
        root = new PlayerRoot(null, ctx);
        stateMachine = new StateMachineBuilder(root).Build();
        ctx.player = player;
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
        ctx.KeyToFastDeflateJustPressed = false;
        ctx.KeyToInflateIsPressed = false;
        ctx.KeyToBlockJustPressed = false;
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
            ctx.KeyToInflateIsPressed = true;
        }

        if (Input.IsActionJustPressed(Actions.FastDeflate))
        {
            ctx.KeyToFastDeflateJustPressed = true;
        }

        if (Input.IsActionJustPressed(Actions.Block))
        {
            ctx.KeyToBlockJustPressed = true;
        }
    }
}
