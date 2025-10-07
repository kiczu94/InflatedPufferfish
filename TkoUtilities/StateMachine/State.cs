namespace TkoUtilities.Hsm;

public class State
{
    public readonly StateMachine StateMachine;
    public readonly State Parent;
    public string Name;
    public State ActiveChild;

    public State(StateMachine stateMachine, State parent = null)
    {
        StateMachine = stateMachine;
        Parent = parent;
    }

    protected virtual State GetInitialState() => null; // Child to enter when starts (null = leaf)
    protected virtual State GetTransition() => null; //Target state to switch to this frame (null = stay in current state)

    protected virtual void OnEnter() { }
    protected virtual void OnExit() { }
    protected virtual void OnUpdate(double deltaTime) { }

    internal void Enter()
    {
        if (Parent != null)
        {
            Parent.ActiveChild = this; //set myself as active child of a parent
        }
        OnEnter();
        var initialState = GetInitialState();
        if (initialState != null)
        {
            initialState.Enter();
        }
        if (initialState == null)
        {
            StateMachine.CurrentState = Name;
        }
    }

    internal void Exit()
    {
        if (ActiveChild != null)
        {
            ActiveChild.Exit();
        }
        ActiveChild = null;
        OnExit();
    }

    internal void Update(double deltaTime)
    {
        State stateToTransition = GetTransition();
        if (stateToTransition != null)
        {
            StateMachine.Sequencer.RequestTransition(this, stateToTransition);
            return;
        }

        if (ActiveChild != null)
        {
            ActiveChild.Update(deltaTime);
        }

        OnUpdate(deltaTime);
    }

    // Returns deepest state
    public State Leaf()
    {
        State state = this;
        while (state.ActiveChild != null)
        {
            state = state.ActiveChild;
        }
        return state;
    }

    public IEnumerable<State> PathToRoot()
    {
        for (State state = this; state != null; state = state.Parent)
            yield return state;
    }
}
