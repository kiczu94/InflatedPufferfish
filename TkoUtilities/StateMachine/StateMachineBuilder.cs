using System.Reflection;

namespace TkoUtilities.Hsm;

public class StateMachineBuilder
{
    readonly State root;

    public StateMachineBuilder(State root)
    {
        this.root = root;
    }

    internal StateMachine Build()
    {
        var machine = new StateMachine(root);
        Wire(root, machine, new HashSet<State>());
        return machine;
    }

    void Wire(State state, StateMachine stateMachine, HashSet<State> visited)
    {
        if(state == null) return;
        if (!visited.Add(state)) return;

        var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
        var machineFiled = typeof(State).GetField("StateMachine", flags);
        if (machineFiled != null) machineFiled.SetValue(state, stateMachine);

        foreach(var fld in state.GetType().GetFields(flags))
        {
            if(!typeof(State).IsAssignableFrom(fld.FieldType)) continue; // Only fields that are State
            if (fld.Name == "Parent") continue; //Skip back-edge parent

            var child = (State)fld.GetValue(state);
            if (child == null) return;
            if (!ReferenceEquals(child.Parent, state)) continue;

            Wire(child, stateMachine, visited);
        }
    }
}
