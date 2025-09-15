using Godot;
using InflatedPufferfish.Entities.Player.Scripts.PlayerStateMachine.States;
using InflatedPufferfish.Scripts.StateMachine;
using System.Linq;

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
        base._Ready();
        root = new PlayerRoot(null, ctx);
        var builder = new StateMachineBuilder(root);
        stateMachine = builder.Build();
    }

    public override void _Process(double deltaTime)
    {
        ctx.MovingUp = false;
        if (Input.IsAnythingPressed()) 
        {
            ctx.MovingUp = true;
        }

        stateMachine.Tick(deltaTime);

        var path = StatePath(stateMachine.Root.Leaf());
        if(path != lastPath)
        {
            GD.Print($"State {path}");
            lastPath = path;
        }
        base._Process(deltaTime);
    }

    void FixedUpdate()
    {
        
    }

    static string StatePath(State s) => string.Join(" > ", s.PathToRoot().Reverse().Select(x => x.GetType().Name));
}

public class PlayerContext
{
    public bool MovingUp;
    public CharacterBody2D player;
}
