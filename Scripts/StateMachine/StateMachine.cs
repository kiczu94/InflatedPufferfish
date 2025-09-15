using Godot;
using System.Collections.Generic;

namespace InflatedPufferfish.Scripts.StateMachine;

internal partial class StateMachine : Node
{
    public readonly State Root;
    public readonly TransitionSequencer Sequencer;
    bool started;

    public StateMachine(State root)
    {
        Root = root;
        Sequencer = new TransitionSequencer(this);
    }

    public void Start()
    {
        if (started)
        {
            return;
        }

        started = true;
        Root.Enter();
    }

    // Method to call form Node which owns that state machine
    public void Tick(double deltaTime)
    {
        if (!started)
        {
            Start();
        }
        InternalTick(deltaTime);
    }

    internal void InternalTick(double deltaTime) => Root.Update(deltaTime);

    public void ChangeState(State from, State to)
    {
        if (from == to || from == null || to == null)
        {
            return;
        }

        // Step 1. Get Lowest Common Ancestor
        State lca = TransitionSequencer.GetLowestCommonAncestor(from, to);

        // Step 2. Exit current brach up to lca
        for (State state = from; state != lca; state = state.Parent)
        {
            state.Exit();
        }

        // Step 3. Enter target branch from LCA
        //Step 3.1 get all the states between target and lca
        var statesToEnter = new Stack<State>();
        for (State state = to; state != lca; state = state.Parent)
        {
            statesToEnter.Push(state);
        }
        // Step 3.2 Enter states from the stack
        while (statesToEnter.Count > 0)
        {
            statesToEnter.Pop().Enter();
        }
    }
}
