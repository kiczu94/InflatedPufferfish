namespace TkoUtilities.Hsm;

public class TransitionSequencer
{
    public readonly StateMachine StateMachine;

    ISequence Sequencer; // current phase 
    Action NextPhase;
    (State from, State to)? Pending;
    State LastFrom, LastTo;

    public TransitionSequencer(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public void RequestTransition(State from, State to)
    {
        if (from == to || to == null) return;
        if (Sequencer != null) // if we are currently transitioning this one gives us a memory of to which one we should go next
        {
            Pending = (from, to);
            return;
        }

        BeginTransition(from, to);
    }

    void BeginTransition(State from, State to)
    {
        // Deactivate old branch
        Sequencer = new NoopPhase();
        Sequencer.Start();
        NextPhase = () =>
        {
            // Change State
            StateMachine.ChangeState(from, to);
            // Activate the new branch
            Sequencer = new NoopPhase();
            Sequencer.Start();
        };
    }

    void EndTransition()
    {
        Sequencer = null;
        if (Pending.HasValue)
        {
            (State from, State to) pendingTransition = Pending.Value;
            Pending = null;
            BeginTransition(pendingTransition.from, pendingTransition.to);
        }
    }

    public void Tick(double deltaTime)
    {
        if (Sequencer != null) //if not null we are in the middle of transition
        {
            if(Sequencer.Update()) //tells us if activation/ deactivation has finished
            {
                if (NextPhase != null)
                {
                    var phaseToInvoke = NextPhase;
                    NextPhase = null;
                    phaseToInvoke();
                }
                else
                {
                    EndTransition();
                }
            }
            return; // while transitioning, we don't run normal updates
        }
        StateMachine.InternalTick(deltaTime);
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
