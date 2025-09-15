using System.Collections.Generic;

namespace InflatedPufferfish.Scripts.StateMachine;

internal partial class TransitionSequencer
{
    public readonly StateMachine StateMachine;

    public TransitionSequencer(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public void RequestTransition(State from, State to)
    {
        StateMachine.ChangeState(from, to);
    }

    // Get Lowest Common Ancestor
    public static State GetLowestCommonAncestor(State stateA, State stateB)
    {
        var parentsOfA = new HashSet<State>();
        for (var state = stateA; state != null; state = state.Parent)
        {
            parentsOfA.Add(state);
        }

        for (var state = stateB; state != null; state = state.Parent)
        {
            if (parentsOfA.Contains(state))
            { 
                return state;
            }
        }

        return null;
    }
}
