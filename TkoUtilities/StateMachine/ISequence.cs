namespace TkoUtilities.Hsm;

public interface ISequence
{
    bool IsDone { get; }
    void Start();
    bool Update();
}

public class NoopPhase : ISequence
{
    public bool IsDone { get; private set; }

    public void Start() => IsDone = true;

    public bool Update() => IsDone;
}
